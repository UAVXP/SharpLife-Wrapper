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
using GoldSource.Shared.Engine;
using GoldSource.Shared.Entities;

namespace GoldSource.Server.Engine.API
{
    /// <summary>
    /// Trace functions
    /// </summary>
    public interface ITrace
    {
        void Line(in Vector start, in Vector end, TraceFlags flags, Edict entToSkip, out TraceResult tr);

        void TraceToss(Edict ent, Edict entToSkip, out TraceResult tr);

        bool TraceMonsterHull(Edict edict, in Vector start, in Vector end, TraceFlags flags, Edict entToSkip, out TraceResult tr);

        void TraceHull(in Vector start, in Vector end, TraceFlags flags, Hull hullNumber, Edict entToSkip, out TraceResult tr);

        void TraceModel(in Vector start, in Vector end, Hull hullNumber, Edict ent, out TraceResult tr);

        string TraceTexture(Edict textureEntity, in Vector start, in Vector end);

        void GetGroupTrace(out int mask, out GroupOp op);

        void PushGroupTrace(int mask, GroupOp op);

        void PopGroupTrace();
    }
}
