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

namespace GoldSource.Shared.Entities
{
    /// <summary>
    /// entity effects
    /// </summary>
    [Flags]
    public enum EntityEffects
    {
        None = 0,

        /// <summary>
        /// swirling cloud of particles
        /// </summary>
        BrightField = 1,

        /// <summary>
        /// single frame ELIGHT on entity attachment 0
        /// </summary>
        Muzzleflash = 2,

        /// <summary>
        /// DLIGHT centered at entity origin
        /// </summary>
        BrightLight = 4,

        /// <summary>
        /// player flashlight
        /// </summary>
        DimLight = 8,

        /// <summary>
        /// get lighting from ceiling
        /// </summary>
        InvLight = 16,

        /// <summary>
        /// don't interpolate the next frame
        /// </summary>
        NoInterp = 32,

        /// <summary>
        /// rocket flare glow sprite
        /// </summary>
        Light = 64,

        /// <summary>
        /// don't draw entity
        /// </summary>
        NoDraw = 128,

        /// <summary>
        /// player nightvision
        /// </summary>
        NightVision = 256,

        /// <summary>
        /// sniper laser effect
        /// </summary>
        SniperLaser = 512,

        /// <summary>
        /// fiber camera
        /// </summary>
        FiberCamera = 1024,
    }
}
