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
using GoldSource.Shared.Wrapper.API.Interfaces;
using System;

namespace GoldSource.Shared.Engine.PlayerPhysics
{
    public sealed unsafe class EnginePhysics : IEnginePhysics
    {
        private PlayerMoveFunctions PlayerMoveFunctions { get; }

        public EnginePhysics(PlayerMoveFunctions playerMoveFunctions)
        {
            PlayerMoveFunctions = playerMoveFunctions ?? throw new ArgumentNullException(nameof(playerMoveFunctions));
        }

        public string Info_ValueForKey(IntPtr s, string key)
        {
            return PlayerMoveFunctions.PM_Info_ValueForKeyHelper(s, key);
        }

        public void Particle(in Vector origin, int color, float life, int zpos, int zvel)
        {
            PlayerMoveFunctions.pfnPM_Particle(origin, color, life, zpos, zvel);
        }

        public int TestPlayerPosition(in Vector pos, out PMTrace ptrace)
        {
            ptrace = new PMTrace();

            return PlayerMoveFunctions.pfnPM_TestPlayerPosition(pos, ptrace.Data);
        }

        public void Con_NPrintf(int idx, string text)
        {
            PlayerMoveFunctions.pfnCon_NPrintf(idx, "%s", text);
        }

        public void Con_DPrintf(string text)
        {
            PlayerMoveFunctions.pfnCon_DPrintf("%s", text);
        }

        public void Con_Printf(string text)
        {
            PlayerMoveFunctions.pfnCon_Printf("%s", text);
        }

        public double Sys_FloatTime()
        {
            return PlayerMoveFunctions.pfnSys_FloatTime();
        }

        public void StuckTouch(int hitent, PMTrace ptraceresult)
        {
            PlayerMoveFunctions.pfnPM_StuckTouch(hitent, ptraceresult.Data);
        }

        public Contents PointContents(in Vector p, out Contents truecontents)
        {
            return PlayerMoveFunctions.pfnPM_PointContents(p, out truecontents);
        }

        public Contents TruePointContents(in Vector p)
        {
            return PlayerMoveFunctions.pfnPM_TruePointContents(p);
        }

        public Contents HullPointContents(HullData hull, int num, in Vector p)
        {
            return PlayerMoveFunctions.pfnPM_HullPointContents(hull.Data, num, p);
        }

        public PMTrace PlayerTrace(in Vector start, in Vector end, PMTraceFlags traceFlags, int ignore_pe)
        {
            var result = PlayerMoveFunctions.pfnPM_PlayerTrace(start, end, traceFlags, ignore_pe);

            var trace = new PMTrace();

            *trace.Data = result;

            return trace;
        }

        public PMTrace TraceLine(in Vector start, in Vector end, int flags, PMHull usehull, int ignore_pe)
        {
            var result = PlayerMoveFunctions.pfnPM_TraceLine(start, end, flags, usehull, ignore_pe);

            var trace = new PMTrace();

            *trace.Data = *result;

            return trace;
        }

        public int RandomLong(int low, int high)
        {
            return PlayerMoveFunctions.pfnRandomLong(low, high);
        }

        public float RandomFloat(float low, float high)
        {
            return PlayerMoveFunctions.pfnRandomFloat(low, high);
        }

        public ModelType GetModelType(IntPtr mod)
        {
            return PlayerMoveFunctions.pfnPM_GetModelType(mod);
        }

        public void GetModelBounds(IntPtr mod, out Vector mins, out Vector maxs)
        {
            PlayerMoveFunctions.pfnPM_GetModelBounds(mod, out mins, out maxs);
        }

        public HullData HullForBsp(PhysEnt pe, out Vector offset)
        {
            return new HullData(PlayerMoveFunctions.pfnPM_HullForBsp(pe.Data, out offset));
        }

        public float TraceModel(PhysEnt pe, in Vector start, in Vector end, out Trace trace)
        {
            trace = new Trace();
            return PlayerMoveFunctions.pfnPM_TraceModel(pe.Data, start, end, trace.Data);
        }

        public void PlaySound(SoundChannel channel, string sample, float volume, float attenuation, SoundFlags flags, int pitch)
        {
            PlayerMoveFunctions.pfnPM_PlaySound(channel, sample, volume, attenuation, flags, pitch);
        }

        public string TraceTexture(int ground, in Vector start, in Vector end)
        {
            return PlayerMoveFunctions.PM_TraceTextureHelper(ground, start, end);
        }

        public void PlaybackEventFull(int flags, int clientindex, ushort eventindex, float delay, in Vector origin, in Vector angles, float fparam1, float fparam2, int iparam1, int iparam2, int bparam1, int bparam2)
        {
            PlayerMoveFunctions.pfnPM_PlaybackEventFull(flags, clientindex, eventindex, delay, origin, angles, fparam1, fparam2, iparam1, iparam2, bparam1, bparam2);
        }

        private static PlayerMoveFunctions.TraceIgnore WrapTraceIgnore(TraceIgnore pfnIgnore)
        {
            PlayerMoveFunctions.TraceIgnore ignoreFn = null;

            if (pfnIgnore != null)
            {
                ignoreFn = pe => pfnIgnore(new PhysEnt(pe)) ? 1 : 0;
            }

            return ignoreFn;
        }

        public PMTrace PlayerTraceEx(in Vector start, in Vector end, int traceFlags, TraceIgnore pfnIgnore)
        {
            var result = PlayerMoveFunctions.pfnPM_PlayerTraceEx(start, end, traceFlags, WrapTraceIgnore(pfnIgnore));

            var trace = new PMTrace();

            *trace.Data = result;

            return trace;
        }

        public int TestPlayerPositionEx(in Vector pos, out PMTrace ptrace, TraceIgnore pfnIgnore)
        {
            ptrace = new PMTrace();

            return PlayerMoveFunctions.pfnPM_TestPlayerPositionEx(pos, ptrace.Data, WrapTraceIgnore(pfnIgnore));
        }

        public int TraceLineEx(in Vector start, in Vector end, PMTraceFlags flags, PMHull usehull, TraceIgnore pfnIgnore)
        {
            return PlayerMoveFunctions.pfnPM_TraceLineEx(start, end, flags, usehull, WrapTraceIgnore(pfnIgnore));
        }
    }
}
