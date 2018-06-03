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

using GoldSource.Client.Engine.API;
using GoldSource.Client.Engine.Wrapper.API.Interfaces;
using GoldSource.Shared.Wrapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GoldSource.Client.Engine.Wrapper
{
    internal sealed class Wrapper : BaseWrapper
    {
        #region Native Interfaces

        private EngineFuncs EngineFuncs { get; }

        //These need to be stored so their unmanaged references don't call into freed memory at any point
        internal ClientDLLFunctions ClientDLLFunctionsInterface { get; set; }

        internal StudioInterface StudioInterfaceInterface { get; set; }

        internal API.Implementations.ClientDLLFunctions ClientDLLFunctions { get; set; }

        internal API.Implementations.StudioInterface StudioInterface { get; set; }

        #endregion

        public Wrapper(EngineFuncs engineFuncs)
            : base(new EngineShared(engineFuncs))
        {
            EngineFuncs = engineFuncs ?? throw new ArgumentNullException(nameof(engineFuncs));
        }

        protected override IServiceCollection CreateServiceCollection()
        {
            var services = base.CreateServiceCollection();

            //For internal use only, these types are not visible to mods
            services.AddSingleton(this);
            services.AddSingleton(EngineFuncs);

            services.AddSingleton<API.Implementations.ClientDLLFunctions>();

            return services;
        }

        protected override void ResolveModInterfaces(IServiceProvider serviceProvider)
        {
            //ServerInterface = serviceProvider.GetRequiredService<IServerInterface>();

            //Initialized in Program in their respective Get* methods
            ClientDLLFunctions = serviceProvider.GetRequiredService<API.Implementations.ClientDLLFunctions>();
        }

        protected override void LoadMod()
        {
            if (!LoadModAssembly(Configuration.ModInfo.Client))
            {
                throw new InvalidOperationException("Could not load mod");
            }
        }
    }
}
