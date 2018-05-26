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

using Server.Engine;
using Server.Engine.PlayerPhysics;

namespace Server.Game.API
{
    //TODO: this interface is shared with the client
    public interface IPlayerPhysics
    {
        void Move(PlayerMove ppmove, IEnginePhysics enginePhysics);

        void Init(PlayerMove ppmove);

        byte FindTextureType(string name);

        bool GetHullBounds(PMHull hullnumber, out Vector mins, out Vector maxs);
    }
}
