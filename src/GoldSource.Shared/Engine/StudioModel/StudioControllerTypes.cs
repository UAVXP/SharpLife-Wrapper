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

namespace GoldSource.Shared.Engine.StudioModel
{
    [Flags]
    public enum StudioControllerTypes
    {
        None = 0,
        X = 0x0001,
        Y = 0x0002,
        Z = 0x0004,
        XR = 0x0008,
        YR = 0x0010,
        ZR = 0x0020,
        LX = 0x0040,
        LY = 0x0080,
        LZ = 0x0100,
        AX = 0x0200,
        AY = 0x0400,
        AZ = 0x0800,
        AXR = 0x1000,
        AYR = 0x2000,
        AZR = 0x4000,
        Types = X | Y | Z | XR | YR | ZR | LX | LY | LZ | AX | AY | AZ | AXR | AYR | AZR,

        /// <summary>
        /// controller that wraps shortest distance
        /// </summary>
        RLoop = 0x8000,
    }
}
