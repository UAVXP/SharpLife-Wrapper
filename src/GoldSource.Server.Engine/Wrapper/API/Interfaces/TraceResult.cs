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
using GoldSource.Server.Engine.Entities;
using GoldSource.Shared.Entities;

namespace GoldSource.Server.Engine.Wrapper.API.Interfaces
{
    //Returned by TraceLine
    internal unsafe struct TraceResult
    {
        //Field <name> is never assigned to, and will always have its default value
        //This type is an output of engine functions
#pragma warning disable CS0649
        internal int fAllSolid;
        internal int fStartSolid;
        internal int fInOpen;
        internal int fInWater;

        internal float flFraction;

        internal Vector vecEndPos;

        internal float flPlaneDist;

        internal Vector vecPlaneNormal;

        internal Edict.Native* pHit;

        internal int iHitgroup;

#pragma warning restore CS0649

        internal Engine.TraceResult ToManaged(EntityDictionary dictionary)
        {
            var tr = new Engine.TraceResult
            {
                AllSolid = 0 != fAllSolid,
                StartSolid = 0 != fStartSolid,
                InOpen = 0 != fInOpen,
                InWater = 0 != fInWater,

                Fraction = flFraction,

                EndPos = vecEndPos,
                PlaneDist = flPlaneDist,
                PlaneNormal = vecPlaneNormal,

                Hit = dictionary.EdictFromNative(pHit),

                Hitgroup = iHitgroup
            };

            return tr;
        }
    }
}
