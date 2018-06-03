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
using GoldSource.Server.Engine.Wrapper.API.Interfaces;
using GoldSource.Shared.Engine;
using GoldSource.Shared.Entities;
using GoldSource.Shared.Wrapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GoldSource.Server.Engine.Wrapper
{
    /// <summary>
    /// Manages the wrapper's state
    /// </summary>
    internal sealed class Wrapper : BaseWrapper
    {
        /// <summary>
        /// The string pool used for strings that the engine expects to stay alive beyond the method calls that pass them
        /// </summary>
        internal StringPool StringPool { get; }

        #region Native Interfaces

        internal EngineFuncs EngineFuncs { get; }

        internal GlobalVars Globals { get; }

        internal CVar.CVar CVar { get; }

        //These need to be stored so their unmanaged references don't call into freed memory at any point
        internal DLLFunctions DLLFunctionsInterface { get; set; }

        internal NewDLLFunctions NewDLLFunctionsInterface { get; set; }

        internal EngineOverrides EngineOverridesInterface { get; set; }

        internal API.Implementations.DLLFunctions DLLFunctions { get; set; }

        internal API.Implementations.NewDLLFunctions NewDLLFunctions { get; set; }

        internal API.Implementations.EngineOverrides EngineOverrides { get; set; }

        #endregion

        #region Public Interfaces

        internal EntityDictionary EntityDictionary { get; }

        internal EngineServer EngineServer { get; }

        #endregion

        #region Mod Data

        public IServerInterface ServerInterface { get; private set; }

        #endregion

        internal unsafe Wrapper(EngineFuncs engineFuncs, GlobalVars.Internal* pGlobals)
            : base(new EngineShared(engineFuncs))
        {
            Log.Message("Initializing wrapper");

            EntityDictionary = new EntityDictionary(engineFuncs);

            //Make sure this knows how to convert edicts
            EntVars.EntityDictionary = EntityDictionary;

            EngineFuncs = engineFuncs;

            Globals = new GlobalVars(pGlobals, EntityDictionary);

            EntityDictionary.SetGlobals(Globals);

            StringPool = new StringPool(Globals);

            EngineString.StringPool = StringPool;

            //Create interface implementations
            EngineServer = new EngineServer(EngineFuncs, StringPool, EntityDictionary, Globals, FileSystem);

            CVar = new CVar.CVar(EngineFuncs);
        }

        protected override IServiceCollection CreateServiceCollection()
        {
            var services = base.CreateServiceCollection();

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

        protected override void ResolveModInterfaces(IServiceProvider serviceProvider)
        {
            ServerInterface = ServiceProvider.GetRequiredService<IServerInterface>();

            //Initialized in Program in their respective Get* methods
            DLLFunctions = ServiceProvider.GetRequiredService<API.Implementations.DLLFunctions>();
            NewDLLFunctions = ServiceProvider.GetRequiredService<API.Implementations.NewDLLFunctions>();
            EngineOverrides = ServiceProvider.GetRequiredService<API.Implementations.EngineOverrides>();
        }

        protected override void LoadMod()
        {
            if (!LoadModAssembly(Configuration.ModInfo.Server))
            {
                throw new InvalidOperationException("Could not load mod");
            }
        }
    }
}
