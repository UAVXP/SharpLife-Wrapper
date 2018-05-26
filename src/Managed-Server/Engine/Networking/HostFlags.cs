﻿/***
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

namespace Server.Engine.Networking
{
    [Flags]
    public enum HostFlags
    {
        None = 0,

        /// <summary>
        /// Don't send entity to local client if the client says it's predicting the entity itself
        /// </summary>
        SkipLocalEnts = 1 << 0
    }
}
