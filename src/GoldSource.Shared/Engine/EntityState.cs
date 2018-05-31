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
using GoldSource.Shared.Entities;
using System.Runtime.InteropServices;

namespace GoldSource.Shared.Engine
{
    public sealed unsafe class EntityState
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Native
        {
            // Fields which are filled in by routines outside of delta compression
            internal EntityType entityType;
            // Index into cl_entities array for this entity.
            internal int number;
            internal float msg_time;

            // Message number last time the player/entity state was updated.
            internal int messagenum;

            // Fields which can be transitted and reconstructed over the network stream
            internal Vector origin;
            internal Vector angles;

            internal int modelindex;
            internal int sequence;
            internal float frame;
            internal int colormap;
            internal short skin;
            internal short solid;
            internal EntityEffects effects;
            internal float scale;

            internal EntityStateFlags eflags;

            // Render information
            internal RenderMode rendermode;
            internal int renderamt;
            internal Color24 rendercolor;
            internal RenderEffect renderfx;

            internal MoveType movetype;
            internal float animtime;
            internal float framerate;
            internal int body;
            internal fixed byte controller[4];
            internal fixed byte blending[4];
            internal Vector velocity;

            // Send bbox down to client for use during prediction.
            internal Vector mins;
            internal Vector maxs;

            internal int aiment;
            // If owned by a player, the index of that player ( for projectiles ).
            internal int owner;

            // Friction, for prediction.
            internal float friction;
            // Gravity multiplier
            internal float gravity;

            // PLAYER SPECIFIC
            internal int team;
            internal int playerclass;
            internal int health;
            internal QBoolean spectator;
            internal int weaponmodel;
            internal int gaitsequence;
            // If standing on conveyor, e.g.
            internal Vector basevelocity;
            // Use the crouched hull, or the regular player hull.
            internal int usehull;
            // Latched buttons last time state updated.
            internal int oldbuttons;
            // -1 = in air, else pmove entity number
            internal int onground;
            internal int iStepLeft;
            // How fast we are falling
            internal float flFallVelocity;

            internal float fov;
            internal int weaponanim;

            // Parametric movement overrides
            internal Vector startpos;
            internal Vector endpos;
            internal float impacttime;
            internal float starttime;

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

        public EntityState(Native* nativeMemory)
        {
            Data = nativeMemory;
        }

        // Fields which are filled in by routines outside of delta compression
        public EntityType EntityType
        {
            get => Data->entityType;
            set => Data->entityType = value;
        }

        // Index into cl_entities array for this entity.
        public int Number
        {
            get => Data->number;
            set => Data->number = value;
        }

        public float MsgTime
        {
            get => Data->msg_time;
            set => Data->msg_time = value;
        }

        // Message number last time the player/entity state was updated.
        public int MessageNum
        {
            get => Data->messagenum;
            set => Data->messagenum = value;
        }

        // Fields which can be transitted and reconstructed over the network stream
        public Vector Origin
        {
            get => Data->origin;
            set => Data->origin = value;
        }

        public Vector Angles
        {
            get => Data->angles;
            set => Data->angles = value;
        }

        public int ModelIndex
        {
            get => Data->modelindex;
            set => Data->modelindex = value;
        }

        public int Sequence
        {
            get => Data->sequence;
            set => Data->sequence = value;
        }

        public float Frame
        {
            get => Data->frame;
            set => Data->frame = value;
        }

        public int ColorMap
        {
            get => Data->colormap;
            set => Data->colormap = value;
        }

        public short Skin
        {
            get => Data->skin;
            set => Data->skin = value;
        }

        public Solid Solid
        {
            get => (Solid)Data->solid;
            set => Data->solid = (short)value;
        }

        public EntityEffects Effects
        {
            get => Data->effects;
            set => Data->effects = value;
        }

        public float Scale
        {
            get => Data->scale;
            set => Data->scale = value;
        }

        public EntityStateFlags EFlags
        {
            get => Data->eflags;
            set => Data->eflags = value;
        }

        // Render information
        public RenderMode RenderMode
        {
            get => Data->rendermode;
            set => Data->rendermode = value;
        }

        public int RenderAmount
        {
            get => Data->renderamt;
            set => Data->renderamt = value;
        }

        public Color24 RenderColor
        {
            get => Data->rendercolor;
            set => Data->rendercolor = value;
        }

        public RenderEffect RenderEffect
        {
            get => Data->renderfx;
            set => Data->renderfx = value;
        }

        public MoveType MoveType
        {
            get => Data->movetype;
            set => Data->movetype = value;
        }

        public float AnimTime
        {
            get => Data->animtime;
            set => Data->animtime = value;
        }

        public float FrameRate
        {
            get => Data->framerate;
            set => Data->framerate = value;
        }

        public int Body
        {
            get => Data->body;
            set => Data->body = value;
        }

        //TODO: range checks
        public byte GetController(int index) => Data->controller[index];
        public void SetController(int index, byte value) => Data->controller[index] = value;

        public byte GetBlending(int index) => Data->blending[index];
        public void SetBlending(int index, byte value) => Data->blending[index] = value;

        public Vector Velocity
        {
            get => Data->velocity;
            set => Data->velocity = value;
        }

        // Send bbox down to client for use during prediction.
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

        public int AimEnt
        {
            get => Data->aiment;
            set => Data->aiment = value;
        }

        // If owned by a player, the index of that player ( for projectiles ).
        public int Owner
        {
            get => Data->owner;
            set => Data->owner = value;
        }

        // Friction, for prediction.
        public float Friction
        {
            get => Data->friction;
            set => Data->friction = value;
        }

        // Gravity multiplier
        public float Gravity
        {
            get => Data->gravity;
            set => Data->gravity = value;
        }

        // PLAYER SPECIFIC
        public int Team
        {
            get => Data->team;
            set => Data->team = value;
        }

        public int PlayerClass
        {
            get => Data->playerclass;
            set => Data->playerclass = value;
        }

        public int Health
        {
            get => Data->health;
            set => Data->health = value;
        }

        public bool Spectator
        {
            get => Data->spectator != QBoolean.False;
            set => Data->spectator = value ? QBoolean.True : QBoolean.False;
        }

        public int WeaponModel
        {
            get => Data->weaponmodel;
            set => Data->weaponmodel = value;
        }

        public int GaitSequence
        {
            get => Data->gaitsequence;
            set => Data->gaitsequence = value;
        }

        // If standing on conveyor, e.g.
        public Vector BaseVelocity
        {
            get => Data->basevelocity;
            set => Data->basevelocity = value;
        }

        // Use the crouched hull, or the regular player hull.
        public int UseHull
        {
            get => Data->usehull;
            set => Data->usehull = value;
        }

        // Latched buttons last time state updated.
        public int OldButtons
        {
            get => Data->oldbuttons;
            set => Data->oldbuttons = value;
        }

        // -1 = in air, else pmove entity number
        public int OnGround
        {
            get => Data->onground;
            set => Data->onground = value;
        }

        public bool StepLeft
        {
            get => Data->iStepLeft != 0;
            set => Data->iStepLeft = value ? 1 : 0;
        }

        // How fast we are falling
        public float FallVelocity
        {
            get => Data->flFallVelocity;
            set => Data->flFallVelocity = value;
        }

        public float FieldOfView
        {
            get => Data->fov;
            set => Data->fov = value;
        }

        public int WeaponAnim
        {
            get => Data->weaponanim;
            set => Data->weaponanim = value;
        }

        // Parametric movement overrides
        public Vector StartPos
        {
            get => Data->startpos;
            set => Data->startpos = value;
        }

        public Vector EndPos
        {
            get => Data->endpos;
            set => Data->endpos = value;
        }

        public float ImpactTime
        {
            get => Data->impacttime;
            set => Data->impacttime = value;
        }

        public float StartTime
        {
            get => Data->starttime;
            set => Data->starttime = value;
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
