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

namespace Server.Engine
{
    [Flags]
    public enum SoundFlags
    {
        None = 0,

        /// <summary>
        /// duplicated in protocol.h stop sound
        /// </summary>
        Stop = 1 << 5,

        /// <summary>
        /// duplicated in protocol.h change sound vol
        /// </summary>
        ChangeVolume = 1 << 6,

        /// <summary>
        /// duplicated in protocol.h change sound pitch
        /// </summary>
        ChangePitch = 1 << 7,

        /// <summary>
        /// duplicated in protocol.h we're spawing, used in some cases for ambients 
        /// </summary>
        Spawning = 1 << 8
    }
}
