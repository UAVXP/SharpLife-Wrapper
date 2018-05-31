/***
*
*	Copyright (c) 1996-2001, Valve LLC. All rights reserved.
*	
*	This product contains software technology licensed from Id 
*	Software, Inc. ("Id Technology").  Id Technology (c) 1996 Id Software, Inc. 
*	All Rights Reserved.
*
*   This source code contains proprietary and confidential information of
*   Valve LLC and its suppliers.  Access to this code is restricted to
*   persons who have executed a written SDK license with Valve.  Any access,
*   use or distribution of this code by or to any unlicensed person is illegal.
*
****/

using GoldSource.Server.Engine.API;
using GoldSource.Server.Engine.API.Implementations;
using GoldSource.Server.Engine.CVar;
using GoldSource.Server.Engine.Entities;
using GoldSource.Server.Engine.Game.API;
using GoldSource.Server.Engine.Sound;
using GoldSource.Server.Engine.Wrapper.API;
using GoldSource.Server.Engine.Wrapper.API.Implementations;
using GoldSource.Server.Engine.Wrapper.API.Interfaces;
using GoldSource.Server.Engine.Wrapper.Config;
using GoldSource.Shared.Engine;
using GoldSource.Shared.Engine.FileSystem;
using GoldSource.Shared.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace GoldSource.Server.Engine.Wrapper
{
    /// <summary>
    /// Manages the wrapper's state
    /// </summary>
    internal sealed class Wrapper
    {
        /// <summary>
        /// The string pool used for strings that the engine expects to stay alive beyond the method calls that pass them
        /// </summary>
        internal StringPool StringPool { get; }

        private IRegistry Registry { get; }

        #region Native Interfaces

        internal EngineFuncs EngineFuncs { get; }

        internal GlobalVars Globals { get; }

        internal CVar.CVar CVar { get; }

        //These need to be stored so their unmanaged references don't call into freed memory at any point
        internal API.Interfaces.DLLFunctions DLLFunctionsInterface { get; set; }

        internal API.Interfaces.NewDLLFunctions NewDLLFunctionsInterface { get; set; }

        internal API.Interfaces.EngineOverrides EngineOverridesInterface { get; set; }

        internal API.Implementations.DLLFunctions DLLFunctions { get; set; }

        internal API.Implementations.NewDLLFunctions NewDLLFunctions { get; set; }

        internal API.Implementations.EngineOverrides EngineOverrides { get; set; }

        #endregion

        #region Public Interfaces

        internal EntityDictionary EntityDictionary { get; }

        internal EngineServer EngineServer { get; }

        internal IFileSystem FileSystem { get; }

        #endregion

        private ServerWrapperConfiguration Configuration { get; set; }

        /// <summary>
        /// The service provider containing all engine and game interfaces
        /// </summary>
        private IServiceProvider ServiceProvider { get; set; }

        #region Mod Data

        /// <summary>
        /// The mod that has been loaded
        /// </summary>
        public Assembly Mod { get; private set; }

        /// <summary>
        /// Main interface to the mod
        /// </summary>
        public BaseMod ModInterface { get; private set; }

        public IServerInterface ServerInterface { get; private set; }

        #endregion

        internal unsafe Wrapper(EngineFuncs engineFuncs, GlobalVars.Internal* pGlobals)
        {
            Log.Message("Initializing wrapper");

#if WINDOWS_BUILD
            Registry = new WindowsRegistry(Framework.WindowsRegistryPath);
#else
            Registry = new UnixRegistry(Framework.UnixRegistryFileName);
#endif

            EntityDictionary = new EntityDictionary(engineFuncs);

            //Make sure this knows how to convert edicts
            EntVars.EntityDictionary = EntityDictionary;

            EngineFuncs = engineFuncs;

            Globals = new GlobalVars(pGlobals, EntityDictionary);

            EntityDictionary.SetGlobals(Globals);

            StringPool = new StringPool(Globals);

            EngineString.StringPool = StringPool;

            //Create interface implementations
            FileSystem = new FileSystem();

            EngineServer = new EngineServer(EngineFuncs, StringPool, EntityDictionary, Globals, FileSystem);

            CVar = new CVar.CVar(EngineFuncs);
        }

        internal unsafe bool Initialize()
        {
            LoadConfiguration();

            SetupFileSystem();

            if (!LoadMod(Configuration.ModInfo))
            {
                return false;
            }

            InitializeMod();

            return true;
        }

        public void Shutdown()
        {
            if (ModInterface != null)
            {
                ModInterface.Shutdown();
                ModInterface = null;
            }

            if (Mod != null)
            {
                Mod = null;
            }
        }

        private IServiceCollection CreateServiceCollection()
        {
            IServiceCollection services = new ServiceCollection();

            //For internal use only, these types are not visible to mods
            services.AddSingleton(this);
            services.AddSingleton(EngineFuncs);
            services.AddSingleton(EntityDictionary);
            services.AddSingleton(EngineServer);

            services.AddSingleton<IEntityDictionary>(EntityDictionary);
            services.AddSingleton<IStringPool>(StringPool);
            services.AddSingleton<IEngineServer>(EngineServer);
            services.AddSingleton(FileSystem);
            services.AddSingleton<IGlobalVars>(Globals);
            services.AddSingleton<ICVar>(CVar);
            services.AddSingleton<IEngineNetworking, EngineNetworking>();
            services.AddSingleton<ITrace, Trace>();
            services.AddSingleton<IEngineSound, EngineSound>();
            services.AddSingleton<IEngineModel, EngineModel>();
            services.AddSingleton<IEngineEntities, EngineEntities>();

            services.AddSingleton<API.Implementations.DLLFunctions>();
            services.AddSingleton<API.Implementations.NewDLLFunctions>();
            services.AddSingleton<API.Implementations.EngineOverrides>();

            return services;
        }

        private void ResolveModInterfaces()
        {
            if (ServiceProvider == null)
            {
                throw new ArgumentNullException(nameof(ServiceProvider));
            }

            ServerInterface = ServiceProvider.GetRequiredService<IServerInterface>();

            //Initialized in Program in their respective Get* methods
            DLLFunctions = ServiceProvider.GetRequiredService<API.Implementations.DLLFunctions>();
            NewDLLFunctions = ServiceProvider.GetRequiredService<API.Implementations.NewDLLFunctions>();
            EngineOverrides = ServiceProvider.GetRequiredService<API.Implementations.EngineOverrides>();
        }

        private void LoadConfiguration()
        {
            using (var stream = new FileStream($"{EngineServer.GameDirectory}/cfg/SharpLife-Wrapper-Managed.xml", FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(ServerWrapperConfiguration));

                Configuration = (ServerWrapperConfiguration)serializer.Deserialize(stream);
            }
        }

        private void SetupFileSystem()
        {
            //Note: the engine has no-Steam directory paths used for testing, but since this is Steam-only, we won't add those
            FileSystem.RemoveAllSearchPaths();

            var cmdLineArgs = Environment.GetCommandLineArgs();

            //Strip off the exe name
            var baseDir = Path.GetDirectoryName(cmdLineArgs[0]);

            const string defaultGame = Framework.DefaultGame;

            var gameDir = EngineServer.GameDirectory;

            //TODO: get language from SteamWorks
            var language = Framework.DefaultLanguage;

            var addLanguage = language != Framework.DefaultLanguage;

            //TODO: get from SteamWorks
            const bool lowViolence = false;

            var hdModels = !cmdLineArgs.Any(s => s == "-nohdmodels") && Registry.ReadInt("hdmodels", 1) > 0;

            var addons = cmdLineArgs.Any(s => s == "-addons") || Registry.ReadInt("addons_folder", 0) > 0;

            if (lowViolence)
            {
                FileSystem.AddSearchPath($"{baseDir}/{gameDir}{FileSystemConstants.Suffixes.LowViolence}", FileSystemConstants.PathID.Game, false);
            }

            if (addons)
            {
                FileSystem.AddSearchPath($"{baseDir}/{gameDir}{FileSystemConstants.Suffixes.Addon}", FileSystemConstants.PathID.Game, false);
            }

            if (addLanguage)
            {
                FileSystem.AddSearchPath($"{baseDir}/{gameDir}_{language}", FileSystemConstants.PathID.Game, false);
            }

            if (hdModels)
            {
                FileSystem.AddSearchPath($"{baseDir}/{gameDir}{FileSystemConstants.Suffixes.HD}", FileSystemConstants.PathID.Game, false);
            }

            FileSystem.AddSearchPath($"{baseDir}/{gameDir}", FileSystemConstants.PathID.Game);
            FileSystem.AddSearchPath($"{baseDir}/{gameDir}", FileSystemConstants.PathID.GameConfig);

            FileSystem.AddSearchPath($"{baseDir}/{gameDir}{FileSystemConstants.Suffixes.Downloads}", FileSystemConstants.PathID.GameDownload);

            if (lowViolence)
            {
                FileSystem.AddSearchPath($"{baseDir}/{defaultGame}{FileSystemConstants.Suffixes.LowViolence}", FileSystemConstants.PathID.DefaultGame, false);
            }

            if (addons)
            {
                FileSystem.AddSearchPath($"{baseDir}/{defaultGame}{FileSystemConstants.Suffixes.Addon}", FileSystemConstants.PathID.DefaultGame, false);
            }

            if (addLanguage)
            {
                FileSystem.AddSearchPath($"{baseDir}/{defaultGame}_{language}", FileSystemConstants.PathID.DefaultGame, false);
            }

            if (hdModels)
            {
                FileSystem.AddSearchPath($"{baseDir}/{defaultGame}{FileSystemConstants.Suffixes.HD}", FileSystemConstants.PathID.DefaultGame, false);
            }

            FileSystem.AddSearchPath(baseDir, FileSystemConstants.PathID.Base);

            FileSystem.AddSearchPath($"{baseDir}/{defaultGame}", FileSystemConstants.PathID.Game, false);

            FileSystem.AddSearchPath($"{baseDir}/{FileSystemConstants.PlatformDirectory}", FileSystemConstants.PathID.Platform);
        }

        private bool LoadMod(ModInfo modInfo)
        {
            if (Mod != null)
            {
                throw new InvalidOperationException("Cannot load a mod if one has been loaded already");
            }

            if (modInfo == null)
            {
                throw new ArgumentNullException(nameof(modInfo));
            }

            if (string.IsNullOrWhiteSpace(modInfo.AssemblyName))
            {
                throw new ArgumentException("Assembly name must be valid", nameof(modInfo));
            }

            var path = $"{EngineServer.GameDirectory}/{modInfo.AssemblyName}";

            Log.Message($"Loading mod from path \"{path}\"");

            Mod = Assembly.LoadFrom(path);

            if (Mod == null)
            {
                Log.Message($"Couldn't load mod {modInfo.AssemblyName}");
                return false;
            }

            //Exclude abstract classes; developers may place common code in abstract base classes
            var modInterfaces = Mod.GetTypes().Where(type => type.IsSubclassOf(typeof(BaseMod)) && !type.IsAbstract).ToList();

            if (modInterfaces.Count == 0)
            {
                Log.Message("Couldn't find the mod interface class");
                return false;
            }

            var modInterfaceClass = modInterfaces[0];

            if (modInterfaces.Count > 1)
            {
                Log.Message("Found more than one candidate for the mod interface class");

                Log.Message("The types found are:");

                modInterfaces.ForEach(t => Log.Message(t.FullName));

                Log.Message($"Using the first found class \"{modInterfaceClass.FullName}\"");
            }

            var instance = Activator.CreateInstance(modInterfaceClass);

            ModInterface = (BaseMod)instance;

            Log.Message($"Mod \"{modInfo.AssemblyName}\" loaded");

            return true;
        }

        private void InitializeMod()
        {
            //Both the engine and server register their interfaces, and then initialize eachother
            var services = CreateServiceCollection();

            ModInterface.Startup(services);

            ServiceProvider = services.BuildServiceProvider();

            ResolveModInterfaces();

            ModInterface.Initialize(ServiceProvider);
        }
    }
}
