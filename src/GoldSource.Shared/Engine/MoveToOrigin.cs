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

namespace GoldSource.Shared.Engine
{
    public enum MoveToOrigin
    {
        /// <summary>
        /// Normal move in the direction monster is facing
        /// </summary>
        Normal = 0,

        /// <summary>
        /// Moves in direction specified, no matter which way monster is facing
        /// </summary>
        Strafe = 1
    };
}
