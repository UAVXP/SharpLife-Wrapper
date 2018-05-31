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
using GoldSource.Shared.Engine.PlayerPhysics;
using GoldSource.Shared.Engine.Sound;
using System;
using System.Runtime.InteropServices;

namespace GoldSource.Shared.Wrapper.API.Interfaces
{
    /// <summary>
    /// This is the functions part of the playermove_t interface
    /// All offsets are relative to the first function address
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public sealed unsafe class PlayerMoveFunctions
    {
        // Common functions
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr PM_Info_ValueForKey(IntPtr s, [MarshalAs(UnmanagedType.LPStr)]string key);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(0)]
        public PM_Info_ValueForKey pfnPM_Info_ValueForKey;

        public string PM_Info_ValueForKeyHelper(IntPtr s, string key)
        {
            var value = pfnPM_Info_ValueForKey(s, key);

            return Marshal.PtrToStringUTF8(value);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void PM_Particle(in Vector origin, int color, float life, int zpos, int zvel);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(4)]
        public PM_Particle pfnPM_Particle;

        //ptrace is optional, and an output
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int PM_TestPlayerPosition(in Vector pos, PMTrace.Native* ptrace);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(8)]
        public PM_TestPlayerPosition pfnPM_TestPlayerPosition;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Con_NPrintf(int idx, [MarshalAs(UnmanagedType.LPStr)]string fmt, [MarshalAs(UnmanagedType.LPStr)]string arg);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(12)]
        public Con_NPrintf pfnCon_NPrintf;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Con_DPrintf([MarshalAs(UnmanagedType.LPStr)]string fmt, [MarshalAs(UnmanagedType.LPStr)]string arg);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(16)]
        public Con_DPrintf pfnCon_DPrintf;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Con_Printf([MarshalAs(UnmanagedType.LPStr)]string fmt, [MarshalAs(UnmanagedType.LPStr)]string arg);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(20)]
        public Con_Printf pfnCon_Printf;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate double Sys_FloatTime();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(24)]
        public Sys_FloatTime pfnSys_FloatTime;

        //ptraceresult is an input
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void PM_StuckTouch(int hitent, PMTrace.Native* ptraceresult);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(28)]
        public PM_StuckTouch pfnPM_StuckTouch;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate Contents PM_PointContents(in Vector p, out Contents truecontents);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(32)]
        public PM_PointContents pfnPM_PointContents;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate Contents PM_TruePointContents(in Vector p);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(36)]
        public PM_TruePointContents pfnPM_TruePointContents;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate Contents PM_HullPointContents(HullData.Native* hull, int num, in Vector p);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(40)]
        public PM_HullPointContents pfnPM_HullPointContents;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate PMTrace.Native PM_PlayerTrace(in Vector start, in Vector end, PMTraceFlags traceFlags, int ignore_pe);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(44)]
        public PM_PlayerTrace pfnPM_PlayerTrace;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate PMTrace.Native* PM_TraceLine(in Vector start, in Vector end, int flags, PMHull usehull, int ignore_pe);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(48)]
        public PM_TraceLine pfnPM_TraceLine;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int RandomLong(int low, int high);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(52)]
        public RandomLong pfnRandomLong;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate float RandomFloat(float low, float high);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(56)]
        public RandomFloat pfnRandomFloat;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate ModelType PM_GetModelType(IntPtr mod);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(60)]
        public PM_GetModelType pfnPM_GetModelType;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void PM_GetModelBounds(IntPtr mod, out Vector mins, out Vector maxs);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(64)]
        public PM_GetModelBounds pfnPM_GetModelBounds;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate HullData.Native* PM_HullForBsp(PhysEnt.Native* pe, out Vector offset);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(68)]
        public PM_HullForBsp pfnPM_HullForBsp;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate float PM_TraceModel(PhysEnt.Native* pe, in Vector start, in Vector end, Trace.Native* trace);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(72)]
        public PM_TraceModel pfnPM_TraceModel;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int COM_FileSize([MarshalAs(UnmanagedType.LPStr)]string filename);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(76)]
        public COM_FileSize pfnCOM_FileSize;

        //Do not use
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr COM_LoadFile([MarshalAs(UnmanagedType.LPStr)]string path, int usehunk, out int pLength);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(80)]
        public COM_LoadFile pfnCOM_LoadFile;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void COM_FreeFile(IntPtr buffer);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(84)]
        public COM_FreeFile pfnCOM_FreeFile;

        //Do not use
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr memfgets(IntPtr pMemFile, int fileSize, ref int pFilePos, IntPtr pBuffer, int bufferSize);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(88)]
        public memfgets pfnmemfgets;

        // Functions
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void PM_PlaySound(SoundChannel channel, [MarshalAs(UnmanagedType.LPStr)]string sample, float volume, float attenuation, SoundFlags flags, int pitch);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(96)]
        public PM_PlaySound pfnPM_PlaySound;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr PM_TraceTexture(int ground, in Vector start, in Vector end);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(100)]
        public PM_TraceTexture pfnPM_TraceTexture;

        public string PM_TraceTextureHelper(int ground, in Vector start, in Vector end)
        {
            var name = pfnPM_TraceTexture(ground, start, end);

            //This method does not handle IntPtr.Zero properly, see
            //https://github.com/dotnet/coreclr/issues/14084
            //Should be fixed, but apparently is not
            return name != IntPtr.Zero ? Marshal.PtrToStringUTF8(name) : null;
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void PM_PlaybackEventFull(int flags, int clientindex, ushort eventindex, float delay, in Vector origin, in Vector angles, float fparam1, float fparam2, int iparam1, int iparam2, int bparam1, int bparam2);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(104)]
        public PM_PlaybackEventFull pfnPM_PlaybackEventFull;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int TraceIgnore(PhysEnt.Native* pe);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate PMTrace.Native PM_PlayerTraceEx(in Vector start, in Vector end, int traceFlags, TraceIgnore pfnIgnore);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(108)]
        public PM_PlayerTraceEx pfnPM_PlayerTraceEx;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int PM_TestPlayerPositionEx(in Vector pos, PMTrace.Native* ptrace, TraceIgnore pfnIgnore);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(112)]
        public PM_TestPlayerPositionEx pfnPM_TestPlayerPositionEx;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int PM_TraceLineEx(in Vector start, in Vector end, PMTraceFlags flags, PMHull usehull, TraceIgnore pfnIgnore);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(116)]
        public PM_TraceLineEx pfnPM_TraceLineEx;
    }
}
