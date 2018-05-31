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
    /// edict.movetype values
    /// </summary>
    public enum MoveType
    {
        None            = 0,   // never moves
        //AngleNoclip	= 1,
        //AngleClip     = 2,
        Walk            = 3,   // Player only - moving on the ground
        Step            = 4,   // gravity, special edge handling -- monsters use this
        Fly             = 5,    // No gravity, but still collides with stuff
        Toss            = 6,   // gravity/collisions
        Push            = 7,   // no clip to world, push and crush
        Noclip          = 8, // No gravity, no collisions, still do velocity/avelocity
        FlyMissile      = 9, // extra size to monsters
        Bounce          = 10,    // Just like Toss, but reflect velocity when contacting surfaces
        BounceMissile   = 11, // bounce w/o gravity
        Follow          = 12,    // track movement of aiment
        PushStep        = 13,  // BSP model that needs physics/world collisions (uses nearest hull for world collision)
    }
}
