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
using System;
using System.Runtime.InteropServices;

namespace GoldSource.Shared.Engine
{
    public sealed unsafe class HullData
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Native
        {
            internal IntPtr clipnodes;
            internal IntPtr planes;
            internal int firstclipnode;
            internal int lastclipnode;
            internal Vector clip_mins;
            internal Vector clip_maxs;
        }

        internal Native* Data { get; }

        public HullData(Native* nativeMemory)
        {
            Data = nativeMemory;
        }

        public int FirstClipNode => Data->firstclipnode;

        public int LastClipNode => Data->lastclipnode;

        public Vector ClipMins => Data->clip_mins;

        public Vector ClipMaxs => Data->clip_maxs;
    }
}
