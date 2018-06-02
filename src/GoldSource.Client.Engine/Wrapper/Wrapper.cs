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

using GoldSource.Client.Engine.Wrapper.API.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GoldSource.Client.Engine.Wrapper
{
    internal sealed class Wrapper
    {
        #region Native Interfaces

        private EngineFuncs EngineFuncs { get; }

        //These need to be stored so their unmanaged references don't call into freed memory at any point
        internal ClientDLLFunctions ClientDLLFunctionsInterface { get; set; }

        internal StudioInterface StudioInterfaceInterface { get; set; }

        internal API.Implementations.ClientDLLFunctions ClientDLLFunctions { get; set; }

        internal API.Implementations.StudioInterface StudioInterface { get; set; }

        #endregion

        /// <summary>
        /// The service provider containing all engine and game interfaces
        /// </summary>
        private IServiceProvider ServiceProvider { get; set; }

        public Wrapper(EngineFuncs engineFuncs)
        {
            EngineFuncs = engineFuncs ?? throw new ArgumentNullException(nameof(engineFuncs));
        }

        public bool Initialize()
        {
            LoadConfiguration();

            SetupFileSystem();

            //if (!LoadMod(Configuration.ModInfo))
            //{
            //    return false;
            //}

            InitializeMod();

            return true;
        }

        private IServiceCollection CreateServiceCollection()
        {
            IServiceCollection services = new ServiceCollection();

            //For internal use only, these types are not visible to mods
            services.AddSingleton(this);
            services.AddSingleton(EngineFuncs);

            services.AddSingleton<API.Implementations.ClientDLLFunctions>();

            return services;
        }

        private void ResolveModInterfaces()
        {
            if (ServiceProvider == null)
            {
                throw new ArgumentNullException(nameof(ServiceProvider));
            }

            //ServerInterface = ServiceProvider.GetRequiredService<IServerInterface>();

            //Initialized in Program in their respective Get* methods
            ClientDLLFunctions = ServiceProvider.GetRequiredService<API.Implementations.ClientDLLFunctions>();
        }

        private void LoadConfiguration()
        {

        }

        private void SetupFileSystem()
        {

        }

        //private bool LoadMod(ModInfo modInfo)
        //{
        //    return true;
        //}

        private void InitializeMod()
        {
            //Both the engine and server register their interfaces, and then initialize eachother
            var services = CreateServiceCollection();

            //ModInterface.Startup(services);

            ServiceProvider = services.BuildServiceProvider();

            ResolveModInterfaces();

            //ModInterface.Initialize(ServiceProvider);
        }
    }
}
