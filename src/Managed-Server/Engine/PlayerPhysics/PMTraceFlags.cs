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

using System;

namespace Server.Engine.PlayerPhysics
{
    [Flags]
    public enum PMTraceFlags
    {
        None = 0,

        /// <summary>
        /// Skip studio models
        /// </summary>
        StudioIgnore = 0x00000001,

        /// <summary>
        /// Use boxes for non-complex studio models (even in traceline)
        /// </summary>
        StudioBox = 0x00000002,

        /// <summary>
        /// Ignore entities with non-normal rendermode
        /// </summary>
        GlassIgnore = 0x00000004,

        /// <summary>
        /// Only trace against the world
        /// </summary>
        WorldOnly = 0x00000008,
    }
}
