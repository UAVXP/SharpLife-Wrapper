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

using GoldSource.Shared.Engine;
using System;
using System.Runtime.InteropServices;

namespace GoldSource.Shared.Entities
{
    public unsafe sealed class Edict
    {
        public const int MAX_ENT_LEAFS = 48;

        [StructLayout(LayoutKind.Sequential)]
        public struct Native
        {
            internal int free;
            internal int serialnumber;
            internal AreaLink area;

            internal int headnode;
            internal int num_leafs;
            internal fixed short leafnums[MAX_ENT_LEAFS];

            internal float freetime;

            internal void* pvPrivateData;

            public EntVars.Native v;
        }

        public Native* Data { get; }

        public Edict(Native* nativeMemory)
        {
            Data = nativeMemory;

            Vars = new EntVars(&Data->v);
        }

        public bool Free
        {
            get => 0 != Data->free;
            set => Data->free = value ? 1 : 0;
        }

        public int SerialNumber
        {
            get => Data->serialnumber;
            set => Data->serialnumber = value;
        }

        /// <summary>
        /// Linked to a division node or leaf
        /// This works based on the address of the area member, so return the address
        /// </summary>
        public IntPtr Area => new IntPtr(&Data->area);

        /// <summary>
        /// -1 to use normal leaf check
        /// </summary>
        public int HeadNode
        {
            get => Data->headnode;
            set => Data->headnode = value;
        }

        public int NumLeafs
        {
            get => Data->num_leafs;
            set => Data->num_leafs = value;
        }

        //TODO: range check
        public short GetLeafNumber(int index) => Data->leafnums[index];
        public void SetLeafNumber(int index, short number) => Data->leafnums[index] = number;

        /// <summary>
        /// sv.time when the object was freed
        /// </summary>
        public float FreeTime
        {
            get => Data->freetime;
            set => Data->freetime = value;
        }

        private IEntity _entity;

        public IEntity PrivateData
        {
            get => _entity;

            set
            {
                _entity = value;
                //We can't store the managed type address in the native object since the CLR can move it around
                //So we just mark the private data as present or not present, since the engine doesn't care about its contents or particular address
                Data->pvPrivateData = (void*)(_entity != null ? 1 : 0);
            }
        }

        /// <summary>
        /// C exported fields from progs
        /// other fields from progs come immediately after
        /// </summary>
        public EntVars Vars { get; }
    }
}
