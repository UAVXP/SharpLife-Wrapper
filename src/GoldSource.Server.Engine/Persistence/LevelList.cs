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

using GoldSource.Mathlib;
using GoldSource.Shared.Entities;

namespace GoldSource.Server.Engine.Persistence
{
    internal unsafe struct LevelList
    {
        //Field <name> is never assigned to, and will always have its default value 0
#pragma warning disable CS0649
        internal fixed byte mapName[32];
        internal fixed byte landmarkName[32];
        internal Edict.Native* pentLandmark;
        internal Vector vecLandmarkOrigin;
#pragma warning restore CS0649
    }
}
