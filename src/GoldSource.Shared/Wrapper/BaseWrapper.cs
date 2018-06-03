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

using GoldSource.FileSystem;
using GoldSource.Registry;
using GoldSource.Shared.Engine;
using GoldSource.Shared.Engine.Config;
using GoldSource.Shared.Engine.FileSystem;
using GoldSource.Shared.Game.API;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace GoldSource.Shared.Wrapper
{
    /// <summary>
    /// Base class for wrappers
    /// </summary>
    public abstract class BaseWrapper
    {
        protected IRegistry Registry { get; }

        protected IFileSystem FileSystem { get; } = new FileSystem.FileSystem();

        protected IEngineShared EngineShared { get; }

        protected EngineConfiguration Configuration { get; set; }

        /// <summary>
        /// The service provider containing all engine and game interfaces
        /// </summary>
        protected IServiceProvider ServiceProvider { get; set; }

        #region Mod Data

        /// <summary>
        /// The mod that has been loaded
        /// </summary>
        protected Assembly Mod { get; private set; }

        /// <summary>
        /// Main interface to the mod
        /// </summary>
        protected BaseMod ModInterface { get; private set; }

        #endregion

        protected BaseWrapper(IEngineShared engineShared)
        {
            Registry = RegistryFactory.Create(Framework.WindowsRegistryPath, $"{Framework.UnixRegistryFileName}{Framework.UnixRegistryFileExtension}", Logger.Instance);
            EngineShared = engineShared ?? throw new ArgumentNullException(nameof(engineShared));
        }

        public bool Initialize()
        {
            LoadConfiguration();

            SetupFileSystem();

            LoadMod();

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

        protected virtual IServiceCollection CreateServiceCollection()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton(EngineShared);

            return services;
        }

        protected virtual void ResolveModInterfaces(IServiceProvider serviceProvider)
        {
        }

        protected virtual void LoadConfiguration()
        {
            using (var stream = new FileStream($"{EngineShared.GameDirectory}/cfg/SharpLife-Wrapper-Managed.xml", FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(EngineConfiguration));

                Configuration = (EngineConfiguration)serializer.Deserialize(stream);
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

            var gameDir = EngineShared.GameDirectory;

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

        protected abstract void LoadMod();

        protected bool LoadModAssembly(ModAssemblyInfo assemblyInfo)
        {
            if (Mod != null)
            {
                throw new InvalidOperationException("Cannot load a mod if one has been loaded already");
            }

            if (assemblyInfo == null)
            {
                throw new ArgumentNullException(nameof(assemblyInfo));
            }

            if (string.IsNullOrWhiteSpace(assemblyInfo.AssemblyName))
            {
                throw new ArgumentException("Assembly name must be valid", nameof(assemblyInfo));
            }

            var path = $"{EngineShared.GameDirectory}/{assemblyInfo.AssemblyName}";

            Logger.Instance.Information($"Loading mod from path \"{path}\"");

            Mod = Assembly.LoadFrom(path);

            if (Mod == null)
            {
                Logger.Instance.Error($"Couldn't load mod {assemblyInfo.AssemblyName}");
                return false;
            }

            //Exclude abstract classes; developers may place common code in abstract base classes
            var modInterfaces = Mod.GetTypes().Where(type => type.IsSubclassOf(typeof(BaseMod)) && !type.IsAbstract).ToList();

            if (modInterfaces.Count == 0)
            {
                Logger.Instance.Error("Couldn't find the mod interface class");
                return false;
            }

            var modInterfaceClass = modInterfaces[0];

            if (modInterfaces.Count > 1)
            {
                Logger.Instance.Warning("Found more than one candidate for the mod interface class");

                Logger.Instance.Warning("The types found are:");

                modInterfaces.ForEach(t => Logger.Instance.Warning(t.FullName));

                Logger.Instance.Warning($"Using the first found class \"{modInterfaceClass.FullName}\"");
            }

            var instance = Activator.CreateInstance(modInterfaceClass);

            ModInterface = (BaseMod)instance;

            Logger.Instance.Information($"Mod \"{assemblyInfo.AssemblyName}\" loaded");

            return true;
        }

        private void InitializeMod()
        {
            //Both the engine and game register their interfaces, and then initialize eachother
            var services = CreateServiceCollection();

            ModInterface.Startup(services);

            ServiceProvider = services.BuildServiceProvider();

            ResolveModInterfaces(ServiceProvider);

            ModInterface.Initialize(ServiceProvider);
        }
    }
}
