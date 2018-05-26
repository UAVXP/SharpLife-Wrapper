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
using System.Runtime.InteropServices;

namespace Server.Engine.PlayerPhysics
{
    public sealed unsafe class PhysEnt
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct Native
        {
            internal fixed byte name[32];
            internal int player;
            internal Vector origin;
            //Never directly accessed, so we can get away with using IntPtr
            internal IntPtr model;
            internal IntPtr studiomodel;
            internal Vector mins, maxs;
            internal int info;
            internal Vector angles;

            internal Solid solid;
            internal int skin;
            internal RenderMode rendermode;

            // Complex collision detection.
            internal float frame;
            internal int sequence;
            internal fixed byte controller[4];
            internal fixed byte blending[2];

            internal MoveType movetype;
            internal TakeDamageState takedamage;
            internal int blooddecal; //Obsolete
            internal int team;
            internal int classnumber;

            // For mods
            internal int iuser1;
            internal int iuser2;
            internal int iuser3;
            internal int iuser4;
            internal float fuser1;
            internal float fuser2;
            internal float fuser3;
            internal float fuser4;
            internal Vector vuser1;
            internal Vector vuser2;
            internal Vector vuser3;
            internal Vector vuser4;
        }

        internal Native* Data { get; }

        internal PhysEnt(Native* nativeMemory)
        {
            Data = nativeMemory;
        }

        /// <summary>
        /// Name of model, or "player" or "world"
        /// </summary>
        public string Name => Marshal.PtrToStringUTF8(new IntPtr(Data->name));

        /// <summary>
        /// If this is a player this is their index, otherwise 0
        /// </summary>
        public int PlayerIndex
        {
            get => Data->player;
            set => Data->player = value;
        }

        /// <summary>
        /// Model's origin in world coordinates
        /// </summary>
        public Vector Origin
        {
            get => Data->origin;
            set => Data->origin = value;
        }

        /// <summary>
        /// only for bsp models
        /// </summary>
        public IntPtr Model
        {
            get => Data->model;
            set => Data->model = value;
        }

        /// <summary>
        /// Solid.BBox, but studio clip intersections
        /// </summary>
        public IntPtr StudioModel
        {
            get => Data->studiomodel;
            set => Data->studiomodel = value;
        }

        /// <summary>
        /// only for non-bsp models
        /// </summary>
        public Vector Mins
        {
            get => Data->mins;
            set => Data->mins = value;
        }

        public Vector Maxs
        {
            get => Data->maxs;
            set => Data->maxs = value;
        }

        /// <summary>
        /// For client or server to use to identify (index into edicts or cl_entities)
        /// </summary>
        public int Info
        {
            get => Data->info;
            set => Data->info = value;
        }

        /// <summary>
        /// rotated entities need this info for hull testing to work
        /// </summary>
        public Vector Angles
        {
            get => Data->angles;
            set => Data->angles = value;
        }

        /// <summary>
        /// Triggers and func_door type WATER brushes are Solid.Not
        /// </summary>
        public Solid Solid
        {
            get => Data->solid;
            set => Data->solid = value;
        }

        /// <summary>
        /// BSP Contents for such things like fun_door water brushes
        /// </summary>
        public int Skin
        {
            get => Data->skin;
            set => Data->skin = value;
        }

        /// <summary>
        /// So we can ignore glass
        /// </summary>
        public RenderMode RenderMode
        {
            get => Data->rendermode;
            set => Data->rendermode = value;
        }

        // Complex collision detection.
        public float Frame
        {
            get => Data->frame;
            set => Data->frame = value;
        }

        public int Sequence
        {
            get => Data->sequence;
            set => Data->sequence = value;
        }

        //TODO: range checks
        public byte GetController(int index) => Data->controller[index];
        public void SetController(int index, byte value) => Data->controller[index] = value;

        public byte GetBlending(int index) => Data->blending[index];
        public void SetBlending(int index, byte value) => Data->blending[index] = value;

        public MoveType MoveType
        {
            get => Data->movetype;
            set => Data->movetype = value;
        }

        public TakeDamageState TakeDamage
        {
            get => Data->takedamage;
            set => Data->takedamage = value;
        }

        public int Team
        {
            get => Data->team;
            set => Data->team = value;
        }

        public int ClassNumber
        {
            get => Data->classnumber;
            set => Data->classnumber = value;
        }

        // For mods
        public int UserInt1
        {
            get => Data->iuser1;
            set => Data->iuser1 = value;
        }

        public int UserInt2
        {
            get => Data->iuser2;
            set => Data->iuser2 = value;
        }

        public int UserInt3
        {
            get => Data->iuser3;
            set => Data->iuser3 = value;
        }

        public int UserInt4
        {
            get => Data->iuser4;
            set => Data->iuser4 = value;
        }

        public float UserFloat1
        {
            get => Data->fuser1;
            set => Data->fuser1 = value;
        }

        public float UserFloat2
        {
            get => Data->fuser2;
            set => Data->fuser2 = value;
        }

        public float UserFloat3
        {
            get => Data->fuser3;
            set => Data->fuser3 = value;
        }

        public float UserFloat4
        {
            get => Data->fuser4;
            set => Data->fuser4 = value;
        }

        public Vector UserVector1
        {
            get => Data->vuser1;
            set => Data->vuser1 = value;
        }

        public Vector UserVector2
        {
            get => Data->vuser2;
            set => Data->vuser2 = value;
        }

        public Vector UserVector3
        {
            get => Data->vuser3;
            set => Data->vuser3 = value;
        }

        public Vector UserVector4
        {
            get => Data->vuser4;
            set => Data->vuser4 = value;
        }
    }
}
