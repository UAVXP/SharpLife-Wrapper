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
using GoldSource.Shared.Engine.Sound;
using System;

namespace GoldSource.Shared.Engine.PlayerPhysics
{
    public delegate bool TraceIgnore(PhysEnt pe);

    public interface IEnginePhysics
    {
        string Info_ValueForKey(IntPtr s, string key);

        void Particle(in Vector origin, int color, float life, int zpos, int zvel);

        int TestPlayerPosition(in Vector pos, out PMTrace ptrace);

        void Con_NPrintf(int idx, string text);

        void Con_DPrintf(string text);

        void Con_Printf(string text);

        double Sys_FloatTime();

        void StuckTouch(int hitent, PMTrace ptraceresult);

        Contents PointContents(in Vector p, out Contents truecontents);

        Contents TruePointContents(in Vector p);

        Contents HullPointContents(HullData hull, int num, in Vector p);

        PMTrace PlayerTrace(in Vector start, in Vector end, PMTraceFlags traceFlags, int ignore_pe);

        PMTrace TraceLine(in Vector start, in Vector end, int flags, PMHull usehull, int ignore_pe);

        int RandomLong(int low, int high);

        float RandomFloat(float low, float high);

        ModelType GetModelType(IntPtr mod);

        void GetModelBounds(IntPtr mod, out Vector mins, out Vector maxs);

        HullData HullForBsp(PhysEnt pe, out Vector offset);

        float TraceModel(PhysEnt pe, in Vector start, in Vector end, out Trace trace);

        void PlaySound(SoundChannel channel, string sample, float volume, float attenuation, SoundFlags flags, int pitch);

        string TraceTexture(int ground, in Vector start, in Vector end);

        void PlaybackEventFull(int flags, int clientindex, ushort eventindex, float delay, in Vector origin, in Vector angles, float fparam1, float fparam2, int iparam1, int iparam2, int bparam1, int bparam2);

        PMTrace PlayerTraceEx(in Vector start, in Vector end, int traceFlags, TraceIgnore pfnIgnore);

        int TestPlayerPositionEx(in Vector pos, out PMTrace ptrace, TraceIgnore pfnIgnore);

        int TraceLineEx(in Vector start, in Vector end, PMTraceFlags flags, PMHull usehull, TraceIgnore pfnIgnore);
    }
}
