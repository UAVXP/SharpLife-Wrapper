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

using Microsoft.Extensions.DependencyInjection;
using System;

namespace Server.Game.API
{
    /// <summary>
    /// Base class for mods
    /// </summary>
    public abstract class BaseMod
    {
        /// <summary>
        /// Start the mod, registering all interfaces
        /// </summary>
        /// <param name="services"></param>
        public abstract void Startup(IServiceCollection services);

        /// <summary>
        /// Initializes the mod using the given service provider
        /// </summary>
        /// <param name="serviceProvider"></param>
        public abstract void Initialize(IServiceProvider serviceProvider);

        /// <summary>
        /// Shuts down the mod
        /// The mod can not longer be accessed after this call
        /// </summary>
        public abstract void Shutdown();
    }
}
