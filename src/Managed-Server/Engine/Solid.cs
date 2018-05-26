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

namespace Server.Engine
{
    /// <summary>
    /// edict.solid values
    /// NOTE: Some movetypes will cause collisions independent of SOLID_NOT/SOLID_TRIGGER when the entity moves
    /// SOLID only effects OTHER entities colliding with this one when they move - UGH!
    /// </summary>
    public enum Solid
    {
        Not				= 0, // no interaction with other objects
        Trigger			= 1, // touch on edge, but not blocking
        BBox			= 2, // touch on edge, block
        SlideBox		= 3, // touch on edge, but not an onground
        BSP				= 4, // bsp clip, touch on edge, block
    }
}
