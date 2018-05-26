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

using Server.Wrapper.API;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Server.Engine.StudioModel
{
    public sealed unsafe class StudioSequence
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct Native
        {
            internal fixed byte label[32]; // sequence label

            internal float fps;      // frames per second	
            internal SequenceFlags flags;      // looping/non-looping flags

            internal int activity;
            internal int actweight;

            internal int numevents;
            internal int eventindex;

            internal int numframes;  // number of frames per sequence

            internal int numpivots;  // number of foot pivots
            internal int pivotindex;

            internal int motiontype;
            internal int motionbone;
            internal Vector linearmovement;
            internal int automoveposindex;
            internal int automoveangleindex;

            internal Vector bbmin;       // per sequence bounding box
            internal Vector bbmax;

            internal int numblends;
            internal int animindex;      // mstudioanim_t pointer relative to start of sequence group data
                                         // [blend][bone][X, Y, Z, XR, YR, ZR]

            internal fixed int blendtype[2];   // X, Y, Z, XR, YR, ZR
            internal fixed float blendstart[2];    // starting value
            internal fixed float blendend[2];  // ending value
            internal int blendparent;

            internal int seqgroup;       // sequence group for demand loading

            internal int entrynode;      // transition node at entry
            internal int exitnode;       // transition node at exit
            internal int nodeflags;      // transition rules

            internal int nextseq;		// auto advancing sequences
        }

        internal Native* Data { get; }

        internal StudioSequence(byte* buffer, Native* nativeMemory)
        {
            Data = nativeMemory;

            Label = Marshal.PtrToStringUTF8(new IntPtr(Data->label));

            Events = APIUtils.AllocateListWrapper(buffer, Data->numevents, Data->eventindex, n => new StudioEvent((StudioEvent.Native*)n.ToPointer()));
        }

        public string Label { get; }

        public float FPS => Data->fps;

        public SequenceFlags Flags => Data->flags;

        public int Activity => Data->activity;

        public int ActWeight => Data->actweight;

        public IReadOnlyList<StudioEvent> Events { get; }

        public int NumFrames => Data->numframes;

        public Vector LinearMovement => Data->linearmovement;

        public Vector BBMin => Data->bbmin;

        public Vector BBMax => Data->bbmax;

        public StudioControllerTypes GetBlendType(int index) => (StudioControllerTypes)Data->blendtype[index];

        public float GetBlendStart(int index) => Data->blendstart[index];

        public float GetBlendEnd(int index) => Data->blendend[index];

        public int EntryNode => Data->entrynode;

        public int ExitNode => Data->exitnode;

        public int NodeFlags => Data->nodeflags;
    }
}
