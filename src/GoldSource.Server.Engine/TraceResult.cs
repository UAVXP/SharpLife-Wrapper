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

namespace GoldSource.Server.Engine
{
    //Returned by TraceLine
    public struct TraceResult
    {
        public bool AllSolid; //if true, plane is not valid

        public bool StartSolid; //if true, the initial point was in a solid area

        public bool InOpen;

        public bool InWater;

        public float Fraction; //time completed, 1.0 = didn't hit anything

        public Vector EndPos; //final position

        public float PlaneDist;

        public Vector PlaneNormal;

        public Edict Hit; //entity the surface is on

        public int Hitgroup;
    }
}
