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
using System.Linq;
using System.Runtime.InteropServices;

namespace Server.Engine.StudioModel
{
    public sealed unsafe class StudioHeader
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct Native
        {
            internal int id;
            internal int version;

            internal fixed byte name[64];
            internal int length;

            internal Vector eyeposition; // ideal eye position
            internal Vector min;         // ideal movement hull size
            internal Vector max;

            internal Vector bbmin;           // clipping bounding box
            internal Vector bbmax;

            internal int flags;

            internal int numbones;           // bones
            internal int boneindex;

            internal int numbonecontrollers;     // bone controllers
            internal int bonecontrollerindex;

            internal int numhitboxes;            // complex bounding boxes
            internal int hitboxindex;

            internal int numseq;             // animation sequences
            internal int seqindex;

            internal int numseqgroups;       // demand loaded sequences
            internal int seqgroupindex;

            internal int numtextures;        // raw textures
            internal int textureindex;
            internal int texturedataindex;

            internal int numskinref;         // replaceable textures
            internal int numskinfamilies;
            internal int skinindex;

            internal int numbodyparts;
            internal int bodypartindex;

            internal int numattachments;     // queryable attachable points
            internal int attachmentindex;

            internal int soundtable;
            internal int soundindex;
            internal int soundgroups;
            internal int soundgroupindex;

            internal int numtransitions;     // animation node to animation node transition graph
            internal int transitionindex;
        }

        internal Native* Data { get; }

        internal StudioHeader(Native* nativeMemory)
        {
            Data = nativeMemory;

            Name = Marshal.PtrToStringUTF8(new IntPtr(Data->name));

            var buffer = (byte*)Data;

            Bones = APIUtils.AllocateListWrapper(buffer, Data->numbones, Data->boneindex, n => new StudioBone((StudioBone.Native*)n.ToPointer()));
            BoneControllers = APIUtils.AllocateListWrapper(buffer, Data->numbones, Data->boneindex, n => new StudioBoneController((StudioBoneController.Native*)n.ToPointer()));
            Sequences = APIUtils.AllocateListWrapper(buffer, Data->numseq, Data->seqindex, n => new StudioSequence(buffer, (StudioSequence.Native*)n.ToPointer()));
            BodyParts = APIUtils.AllocateListWrapper(buffer, Data->numbodyparts, Data->bodypartindex, n => new StudioBodyPart((StudioBodyPart.Native*)n.ToPointer()));

            var transitions = new byte[Data->numtransitions];

            Marshal.Copy(new IntPtr(buffer + Data->transitionindex), transitions, 0, Data->numtransitions);

            Transitions = transitions.ToList();
        }

        public int Id => Data->id;

        public int Version => Data->version;

        public string Name { get; }

        public Vector EyePosition => Data->eyeposition;

        public Vector Min => Data->min;

        public Vector Max => Data->max;

        public Vector BBMin => Data->bbmin;

        public Vector BBMax => Data->bbmin;

        public int Flags => Data->flags;

        public IReadOnlyList<StudioBone> Bones { get; }

        public IReadOnlyList<StudioBoneController> BoneControllers { get; }

        public IReadOnlyList<StudioSequence> Sequences { get; }

        public IReadOnlyList<StudioBodyPart> BodyParts { get; }

        public IReadOnlyList<byte> Transitions { get; }

        //TODO: finish rest of data
    }
}
