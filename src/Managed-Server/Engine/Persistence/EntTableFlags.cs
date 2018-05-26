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

namespace Server.Engine.Persistence
{
    [Flags]
    internal enum EntTableFlags : uint
    {
        None = 0,
        Global = 0x10000000,
        Moveable = 0x20000000,
        Removed = 0x40000000,
        Player = 0x80000000
    }
}
