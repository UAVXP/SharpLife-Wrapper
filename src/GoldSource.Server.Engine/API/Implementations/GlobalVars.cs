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
using GoldSource.Server.Engine.Entities;
using GoldSource.Shared.Engine;
using GoldSource.Shared.Entities;
using System;
using System.Runtime.InteropServices;

namespace GoldSource.Server.Engine.API.Implementations
{
    /// <summary>
    /// Wrapper around the global variables instance
    /// Uses unsafe to directly access data without needing wrapper functions in native code
    /// </summary>
    internal unsafe class GlobalVars : IGlobalVars
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct Internal
        {
            public float time;
            public float frametime;
            public float force_retouch;
            public EngineString mapname;
            public EngineString startspot;
            public float deathmatch;
            public float coop;
            public float teamplay;
            public float serverflags;
            public float found_secrets;
            public Vector v_forward;
            public Vector v_up;
            public Vector v_right;
            public float trace_allsolid;
            public float trace_startsolid;
            public float trace_fraction;
            public Vector trace_endpos;
            public Vector trace_plane_normal;
            public float trace_plane_dist;
            public Edict.Native* trace_ent;
            public float trace_inopen;
            public float trace_inwater;
            public int trace_hitgroup;
            public int trace_flags;
            public int msg_entity;
            public int cdAudioTrack;
            public int maxClients;
            public int maxEntities;
            public IntPtr pStringBase;

            public IntPtr pSaveData;
            public Vector vecLandmarkOffset;
        }

        private Internal* Data { get; }

        private EntityDictionary EntityDictionary { get; }

        internal GlobalVars(Internal* pGlobals, EntityDictionary entityDictionary)
        {
            Data = pGlobals;

            EntityDictionary = entityDictionary ?? throw new ArgumentNullException(nameof(entityDictionary));
        }

        public float Time => Data->time;

        public float FrameTime => Data->frametime;

        public uint ForceRetouch
        {
            get => (uint)Data->force_retouch;
            set => Data->force_retouch = value;
        }

        public string MapName => Data->mapname.ToString();

        public string StartSpot => Data->startspot.ToString();

        public bool IsDeathMatch => 0 != Data->deathmatch;

        public bool CoOp => 0 != Data->coop;

        public bool TeamPlay => 0 != Data->teamplay;

        public int ServerFlags
        {
            get => (int)Data->serverflags;
            set => Data->serverflags = value;
        }

        public uint FoundSecrets
        {
            get => (uint)Data->found_secrets;
            set => Data->found_secrets = value;
        }

        public Vector ForwardVector
        {
            get => Data->v_forward;
            set => Data->v_forward = value;
        }

        public Vector UpVector
        {
            get => Data->v_up;
            set => Data->v_up = value;
        }

        public Vector RightVector
        {
            get => Data->v_right;
            set => Data->v_right = value;
        }

        public TraceResult GlobalTrace
        {
            get
            {
                return new TraceResult
                {
                    AllSolid = 0 != Data->trace_allsolid,
                    StartSolid = 0 != Data->trace_startsolid,
                    InOpen = 0 != Data->trace_inopen,
                    InWater = 0 != Data->trace_inwater,
                    Fraction = Data->trace_fraction,
                    EndPos = Data->trace_endpos,
                    PlaneDist = Data->trace_plane_dist,
                    PlaneNormal = Data->trace_plane_normal,
                    Hit = EntityDictionary.EdictFromNative(Data->trace_ent),
                    Hitgroup = Data->trace_hitgroup
                };
            }
        }

        public int MessageEntity => Data->msg_entity;

        public int CDAudioTrack
        {
            get => Data->cdAudioTrack;
            set => Data->cdAudioTrack = value;
        }

        public int MaxClients => Data->maxClients;

        public int MaxEntities => Data->maxEntities;

        public IntPtr StringBase => Data->pStringBase;

        public IntPtr SaveData => Data->pSaveData;

        public Vector LandmarkOffset
        {
            get => Data->vecLandmarkOffset;
            set => Data->vecLandmarkOffset = value;
        }
    }
}
