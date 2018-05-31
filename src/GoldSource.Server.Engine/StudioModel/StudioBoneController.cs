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

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace GoldSource.Server.Engine.StudioModel
{
    public sealed unsafe class StudioBoneController
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct Native
        {
            internal int bone;   // -1 == 0
            internal StudioControllerTypes type;   // X, Y, Z, XR, YR, ZR, M
            internal float start;
            internal float end;
            internal int rest;   // byte index value at rest
            internal int index;	// 0-3 user set controller, 4 mouth
        }

        internal Native* Data { get; }

        internal StudioBoneController(Native* nativeMemory)
        {
            Data = nativeMemory;
        }

        public int Bone => Data->bone;

        public StudioControllerTypes Type => Data->type;

        public float Start => Data->start;

        public float End => Data->end;

        public int Rest => Data->rest;

        public int Index => Data->index;
    }
}
