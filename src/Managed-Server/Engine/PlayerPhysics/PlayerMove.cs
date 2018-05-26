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

using Server.Engine.Networking;
using Server.Wrapper.API;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Server.Engine.PlayerPhysics
{
    public sealed unsafe class PlayerMove
    {
        private const int MaxTextureName = 256;
        private const int MaxPhysEnts = 600;
        private const int MaxMoveEnts = 64;

        [StructLayout(LayoutKind.Explicit)]
        internal struct Native
        {
            [FieldOffset(0)]
            internal int player_index;  // So we don't try to run the PM_CheckStuck nudging too quickly.

            [FieldOffset(4)]
            internal QBoolean server;        // For debugging, are we running physics code on server side?

            [FieldOffset(8)]
            internal QBoolean multiplayer;   // 1 == multiplayer server

            [FieldOffset(12)]
            internal float time;          // realtime on host, for reckoning duck timing

            [FieldOffset(16)]
            internal float frametime;       // Duration of this frame

            // Vectors for angles
            [FieldOffset(20)]
            internal Vector forward;

            [FieldOffset(32)]
            internal Vector right;

            [FieldOffset(44)]
            internal Vector up;

            // player state
            [FieldOffset(56)]
            internal Vector origin;        // Movement origin.

            [FieldOffset(68)]
            internal Vector angles;        // Movement view angles.

            [FieldOffset(80)]
            internal Vector oldangles;     // Angles before movement view angles were looked at.

            [FieldOffset(92)]
            internal Vector velocity;      // Current movement direction.

            [FieldOffset(104)]
            internal Vector movedir;       // For waterjumping, a forced forward velocity so we can fly over lip of ledge.

            [FieldOffset(116)]
            internal Vector basevelocity;  // Velocity of the conveyor we are standing, e.g.

            // For ducking/dead
            [FieldOffset(128)]
            internal Vector view_ofs;      // Our eye position.

            [FieldOffset(140)]
            internal float flDuckTime;    // Time we started duck

            [FieldOffset(144)]
            internal QBoolean bInDuck;       // In process of ducking or ducked already?

            // For walking/falling
            [FieldOffset(148)]
            internal int flTimeStepSound;  // Next time we can play a step sound

            [FieldOffset(152)]
            internal int iStepLeft;

            [FieldOffset(156)]
            internal float flFallVelocity;

            [FieldOffset(160)]
            internal Vector punchangle;

            [FieldOffset(172)]
            internal float flSwimTime;

            [FieldOffset(176)]
            internal float flNextPrimaryAttack;

            [FieldOffset(180)]
            internal int effects;        // MUZZLE FLASH, e.g.

            [FieldOffset(184)]
            internal EntFlags flags;         // FL_ONGROUND, FL_DUCKING, etc.

            [FieldOffset(188)]
            internal PMHull usehull;       // 0 = regular player hull, 1 = ducked player hull, 2 = point hull

            [FieldOffset(192)]
            internal float gravity;       // Our current gravity and friction.

            [FieldOffset(196)]
            internal float friction;

            [FieldOffset(200)]
            internal InputKeys oldbuttons;    // Buttons last usercmd

            [FieldOffset(204)]
            internal float waterjumptime; // Amount of time left in jumping out of water cycle.

            [FieldOffset(208)]
            internal QBoolean dead;          // Are we a dead player?

            [FieldOffset(212)]
            internal DeadFlag deadflag;

            [FieldOffset(216)]
            internal int spectator;     // Should we use spectator physics model?

            [FieldOffset(220)]
            internal MoveType movetype;      // Our movement type, NOCLIP, WALK, FLY

            [FieldOffset(224)]
            internal int onground;

            [FieldOffset(228)]
            internal WaterLevel waterlevel;

            [FieldOffset(232)]
            internal Contents watertype;

            [FieldOffset(236)]
            internal WaterLevel oldwaterlevel;

            [FieldOffset(240)]
            internal fixed byte sztexturename[MaxTextureName];

            [FieldOffset(496)]
            internal byte chtexturetype;

            [FieldOffset(500)]
            internal float maxspeed;

            [FieldOffset(504)]
            internal float clientmaxspeed; // Player specific maxspeed

            // For mods
            [FieldOffset(508)]
            internal int iuser1;

            [FieldOffset(512)]
            internal int iuser2;

            [FieldOffset(516)]
            internal int iuser3;

            [FieldOffset(520)]
            internal int iuser4;

            [FieldOffset(524)]
            internal float fuser1;

            [FieldOffset(528)]
            internal float fuser2;

            [FieldOffset(532)]
            internal float fuser3;

            [FieldOffset(536)]
            internal float fuser4;

            [FieldOffset(540)]
            internal Vector vuser1;

            [FieldOffset(552)]
            internal Vector vuser2;

            [FieldOffset(564)]
            internal Vector vuser3;

            [FieldOffset(576)]
            internal Vector vuser4;

            // world state
            // Number of entities to clip against.
            [FieldOffset(588)]
            internal int numphysent;

            [FieldOffset(592)]
            internal PhysEnt.Native physents;//[MaxPhysEnts];

            // Number of momvement entities (ladders)
            [FieldOffset(134992)]
            internal int nummoveent;

            // just a list of ladders
            [FieldOffset(134996)]
            internal PhysEnt.Native moveents;//[MaxMoveEnts];

            // All things being rendered, for tracing against things you don't actually collide with
            [FieldOffset(149332)]
            internal int numvisent;

            [FieldOffset(149336)]
            internal PhysEnt.Native visents;//[MaxPhysEnts];

            // input to run through physics.
            [FieldOffset(283736)]
            internal UserCmd.Native cmd;

            // Trace results for objects we collided with.
            [FieldOffset(283788)]
            internal int numtouch;

            [FieldOffset(283792)]
            internal PMTrace.Native touchindex;//[MaxPhysEnts];

            [FieldOffset(324592)]
            internal fixed byte physinfo[InternalConstants.MaxPhysInfoString]; // Physics info string

            [FieldOffset(324848)]
            internal MoveVars.Native* movevars;

            [FieldOffset(324852)]
            internal Vector player_mins;//[4];

            [FieldOffset(324900)]
            internal Vector player_maxs;//[4];

            //Not a member, used to locate the start of the functions
            [FieldOffset(324948)]
            internal int firstFunctionOffset;

            [FieldOffset(325040)]
            internal QBoolean runfuncs;
        }

        internal Native* Data { get; }

        private IList<PhysEnt> WrapPhysEnts(PhysEnt.Native* ent, int max)
        {
            var ents = new List<PhysEnt>(max);

            for (var i = 0; i < max; ++i)
            {
                ents.Add(new PhysEnt(ent + i));
            }

            return ents;
        }

        internal PlayerMove(Native* nativeMemory, bool isServer)
        {
            Data = nativeMemory;

            Data->server = isServer ? QBoolean.True : QBoolean.False;

            PhysEnts = WrapPhysEnts(&Data->physents, MaxPhysEnts);
            MoveEnts = WrapPhysEnts(&Data->moveents, MaxMoveEnts);
            VisEnts = WrapPhysEnts(&Data->visents, MaxPhysEnts);

            Cmd = new UserCmd(&nativeMemory->cmd);

            var touchIndex = new List<PMTrace>(MaxPhysEnts);

            for (var i = 0; i < MaxPhysEnts; ++i)
            {
                touchIndex.Add(new PMTrace(&Data->touchindex + i));
            }

            TouchIndex = touchIndex;

            MoveVars = new MoveVars(Data->movevars);
        }

        /// <summary>
        /// So we don't try to run the PM_CheckStuck nudging too quickly
        /// </summary>
        public int PlayerIndex => Data->player_index;

        /// <summary>
        /// For debugging, are we running physics code on server side?
        /// </summary>
        public bool IsServer => Data->server != QBoolean.False;

        /// <summary>
        /// true == multiplayer server
        /// </summary>
        public bool IsMultiplayer => Data->multiplayer != QBoolean.False;

        /// <summary>
        /// realtime on host, for reckoning duck timing
        /// </summary>
        public float Time => Data->time;

        /// <summary>
        /// Duration of this frame
        /// </summary>
        public float FrameTime
        {
            get => Data->frametime;
            set => Data->frametime = value;
        }

        /// <summary>
        /// Vectors for angles
        /// </summary>
        public Vector Forward
        {
            get => Data->forward;
            set => Data->forward = value;
        }

        public Vector Right
        {
            get => Data->right;
            set => Data->right = value;
        }

        public Vector Up
        {
            get => Data->up;
            set => Data->up = value;
        }

        // player state
        /// <summary>
        /// Movement origin
        /// </summary>
        public Vector Origin
        {
            get => Data->origin;
            set => Data->origin = value;
        }

        /// <summary>
        /// Movement view angles
        /// </summary>
        public Vector Angles
        {
            get => Data->angles;
            set => Data->angles = value;
        }

        /// <summary>
        /// Angles before movement view angles were looked at
        /// </summary>
        public Vector OldAngles
        {
            get => Data->oldangles;
            set => Data->oldangles = value;
        }

        /// <summary>
        /// Current movement direction
        /// </summary>
        public Vector Velocity
        {
            get => Data->velocity;
            set => Data->velocity = value;
        }

        /// <summary>
        /// For waterjumping, a forced forward velocity so we can fly over lip of ledge
        /// </summary>
        public Vector MoveDirection
        {
            get => Data->movedir;
            set => Data->movedir = value;
        }

        /// <summary>
        /// Velocity of the conveyor we are standing, e.g
        /// </summary>
        public Vector BaseVelocity
        {
            get => Data->basevelocity;
            set => Data->basevelocity = value;
        }

        // For ducking/dead

        /// <summary>
        /// Our eye position
        /// </summary>
        public Vector ViewOffset
        {
            get => Data->view_ofs;
            set => Data->view_ofs = value;
        }

        /// <summary>
        /// Time we started duck
        /// </summary>
        public float DuckTime
        {
            get => Data->flDuckTime;
            set => Data->flDuckTime = value;
        }

        /// <summary>
        /// In process of ducking or ducked already?
        /// </summary>
        public bool InDuck
        {
            get => Data->bInDuck != QBoolean.False;
            set => Data->bInDuck = value ? QBoolean.True : QBoolean.False;
        }

        // For walking/falling
        /// <summary>
        /// Next time we can play a step sound
        /// </summary>
        public int TimeStepSound
        {
            get => Data->flTimeStepSound;
            set => Data->flTimeStepSound = value;
        }

        public bool StepLeft
        {
            get => Data->iStepLeft != 0;
            set => Data->iStepLeft = value ? 1 : 0;
        }

        public float FallVelocity
        {
            get => Data->flFallVelocity;
            set => Data->flFallVelocity = value;
        }

        public Vector PunchAngle
        {
            get => Data->punchangle;
            set => Data->punchangle = value;
        }

        public float SwimTime
        {
            get => Data->flSwimTime;
            set => Data->flSwimTime = value;
        }

        public float NextPrimaryAttack => Data->flNextPrimaryAttack;

        /// <summary>
        /// MUZZLE FLASH, e.g
        /// </summary>
        public int Effects => Data->effects;

        /// <summary>
        /// FL_ONGROUND, FL_DUCKING, etc
        /// </summary>
        public EntFlags Flags
        {
            get => Data->flags;
            set => Data->flags = value;
        }

        public PMHull UseHull
        {
            get => Data->usehull;
            set => Data->usehull = value;
        }

        /// <summary>
        /// Our current gravity and friction
        /// </summary>
        public float Gravity
        {
            get => Data->gravity;
            set => Data->gravity = value;
        }

        public float Friction
        {
            get => Data->friction;
            set => Data->friction = value;
        }

        /// <summary>
        /// Buttons last usercmd
        /// </summary>
        public InputKeys OldButtons
        {
            get => Data->oldbuttons;
            set => Data->oldbuttons = value;
        }

        /// <summary>
        /// Amount of time left in jumping out of water cycle
        /// </summary>
        public float WaterJumpTime
        {
            get => Data->waterjumptime;
            set => Data->waterjumptime = value;
        }

        /// <summary>
        /// Are we a dead player?
        /// </summary>
        public bool Dead => Data->dead != QBoolean.False;

        public DeadFlag DeadFlag => Data->deadflag;

        /// <summary>
        /// Should we use spectator physics model?
        /// </summary>
        public bool IsSpectator => Data->spectator != 0;

        /// <summary>
        /// Our movement type, NOCLIP, WALK, FLY
        /// </summary>
        public MoveType MoveType
        {
            get => Data->movetype;
            set => Data->movetype = value;
        }

        public int OnGround
        {
            get => Data->onground;
            set => Data->onground = value;
        }

        public WaterLevel WaterLevel
        {
            get => Data->waterlevel;
            set => Data->waterlevel = value;
        }

        public Contents WaterType
        {
            get => Data->watertype;
            set => Data->watertype = value;
        }

        public WaterLevel OldWaterLevel
        {
            get => Data->oldwaterlevel;
            set => Data->oldwaterlevel = value;
        }

        public string TextureName
        {
            get => Marshal.PtrToStringUTF8(new IntPtr(Data->sztexturename));
            set => InterfaceUtils.CopyStringToUnmanagedBuffer(value, Data->sztexturename, MaxTextureName);
        }

        public byte TextureType
        {
            get => Data->chtexturetype;
            set => Data->chtexturetype = value;
        }

        public float MaxSpeed
        {
            get => Data->maxspeed;
            set => Data->maxspeed = value;
        }

        /// <summary>
        /// Player specific maxspeed
        /// </summary>
        public float ClientMaxSpeed => Data->clientmaxspeed;

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

        // world state
        // Number of entities to clip against.
        public int NumPhysEnt => Data->numphysent;

        public IList<PhysEnt> PhysEnts { get; }

        // Number of momvement entities (ladders)
        public int NumMoveEnt => Data->nummoveent;

        // just a list of ladders
        public IList<PhysEnt> MoveEnts { get; }

        // All things being rendered, for tracing against things you don't actually collide with
        public int NumVisEnt => Data->numvisent;

        public IList<PhysEnt> VisEnts { get; }

        /// <summary>
        /// input to run through physics
        /// </summary>
        public UserCmd Cmd { get; }

        // Trace results for objects we collided with.
        public int NumTouch
        {
            get => Data->numtouch;
            set => Data->numtouch = value;
        }

        public IList<PMTrace> TouchIndex { get; }

        /// <summary>
        /// Physics info string
        /// </summary>
        public IntPtr PhysInfo => new IntPtr(Data->physinfo);

        public MoveVars MoveVars { get; }

        public Vector GetPlayerMins(int index)
        {
            return (&Data->player_mins)[index];
        }

        public void SetPlayerMins(int index, in Vector value)
        {
            (&Data->player_mins)[index] = value;
        }

        public Vector GetPlayerMaxs(int index)
        {
            return (&Data->player_maxs)[index];
        }

        public void SetPlayerMaxs(int index, in Vector value)
        {
            (&Data->player_maxs)[index] = value;
        }

        public bool RunFuncs => Data->runfuncs != QBoolean.False;
    }
}
