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

namespace GoldSource.Shared.Entities
{
    /// <summary>
    /// edict.deadflag values
    /// </summary>
    public enum DeadFlag
    {
        No = 0, // alive
        Dying = 1, // playing death animation or still falling off of a ledge waiting to hit ground
        Dead = 2, // dead. lying still.
        Respawnable = 3,
        DiscardBody = 4,
    }
}
