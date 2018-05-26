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

using Server.Engine.API.Implementations;
using System.Runtime.InteropServices;

namespace Server.Engine
{
    public unsafe class EntVars
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct Native
        {
            internal EngineString classname;

            internal EngineString globalname;

            internal Vector origin;

            internal Vector oldorigin;

            internal Vector velocity;

            internal Vector basevelocity;

            internal Vector clbasevelocity;

            /*
            *	Base velocity that was passed in to server physics so 
            *	client can predict conveyors correctly.  Server zeroes it, so we need to store here, too.
            */
            internal Vector movedir;

            internal Vector angles;
            //Model angles
            internal Vector avelocity;
            //angle velocity (degrees per second)
            internal Vector punchangle;
            //auto-decaying view angle adjustment
            internal Vector v_angle;

            /*
            *	Viewing angle (player only)
            *	For parametric entities
            */
            internal Vector endpos;

            internal Vector startpos;

            internal float impacttime;

            internal float starttime;

            internal FixAngleMode fixangle;
            //0:nothing, 1:force view angles, 2:add avelocity
            internal float idealpitch;

            internal float pitch_speed;

            internal float ideal_yaw;

            internal float yaw_speed;

            internal int modelindex;

            internal EngineString model;

            //player's viewmodel
            internal EngineString viewmodel;

            internal EngineString weaponmodel;
            //what other players see
            internal Vector absmin;
            //BB max translated to world coord
            internal Vector absmax;
            //BB max translated to world coord
            internal Vector mins;
            //local BB min
            internal Vector maxs;
            //local BB max
            internal Vector size;
            //maxs - mins
            internal float ltime;

            internal float nextthink;

            internal MoveType movetype;

            internal Solid solid;

            internal int skin;

            internal int body;
            //sub-model selection for studiomodels
            internal EntityEffects effects;

            internal float gravity;
            //% of "normal" gravity
            internal float friction;
            //inverse elasticity of MOVETYPE_BOUNCE
            internal int light_level;

            internal int sequence;
            //animation sequence
            internal int gaitsequence;
            //movement animation sequence for player (0 for none)
            internal float frame;
            //% playback position in animation sequences (0..255)
            internal float animtime;
            //world time when frame was set
            internal float framerate;
            //animation playback rate (-8x to 8x)
            internal fixed byte controller[4];
            //bone controller setting (0..255)
            internal fixed byte blending[2];
            //blending amount between sub-sequences (0..255)
            internal float scale;
            //sprite rendering scale (0..255)
            internal RenderMode rendermode;

            internal float renderamt;

            internal Vector rendercolor;

            internal RenderEffect renderfx;

            internal float health;

            internal float frags;

            internal int weapons; //bit mask for available weapons

            internal float takedamage;

            internal DeadFlag deadflag;

            internal Vector view_ofs; //eye position

            internal int button;

            internal int impulse;

            internal Edict.Native* chain;
            internal Edict.Native* dmg_inflictor;
            internal Edict.Native* enemy;
            internal Edict.Native* aiment;
            internal Edict.Native* owner;
            internal Edict.Native* groundentity;

            //Was int, changed to uint to eliminate warnings when setting highest bit
            internal uint spawnflags;

            internal EntFlags flags;

            internal int colormap; //lowbyte topcolor, highbyte bottomcolor

            internal int team;

            internal float max_health;

            internal float teleport_time;

            internal float armortype;

            internal float armorvalue;

            internal WaterLevel waterlevel;

            internal Contents watertype;

            internal EngineString target;
            internal EngineString targetname;
            internal EngineString netname;
            internal EngineString message;

            internal float dmg_take;
            internal float dmg_save;
            internal float dmg;
            internal float dmgtime;

            internal EngineString noise;
            internal EngineString noise1;
            internal EngineString noise2;
            internal EngineString noise3;

            internal float speed;

            internal float air_finished;
            internal float pain_finished;
            internal float radsuit_finished;

            internal Edict.Native* pContainingEntity;

            internal int playerclass;

            internal float maxspeed;

            internal float fov;

            internal int weaponanim;

            internal int pushmsec;

            internal int bInDuck;

            internal int flTimeStepSound;

            internal int flSwimTime;

            internal int flDuckTime;

            internal int iStepLeft;

            internal float flFallVelocity;

            internal int gamestate;

            internal int oldbuttons;

            internal int groupinfo;

            //For mods
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

            internal Edict.Native* euser1;
            internal Edict.Native* euser2;
            internal Edict.Native* euser3;
            internal Edict.Native* euser4;
        }

        internal static EntityDictionary EntityDictionary { get; set; }

        internal Native* Data { get; }

        internal EntVars(Native* nativeMemory)
        {
            Data = nativeMemory;
        }

        public string ClassName
        {
            get => Data->classname.ToString();
            set => Data->classname = EngineString.FromString(value);
        }

        public string GlobalName
        {
            get => Data->globalname.ToString();
            set => Data->globalname = EngineString.FromString(value);
        }

        public Vector Origin
        {
            get => Data->origin;
            set => Data->origin = value;
        }

        public Vector OldOrigin
        {
            get => Data->oldorigin;
            set => Data->oldorigin = value;
        }

        public Vector Velocity
        {
            get => Data->velocity;
            set => Data->velocity = value;
        }

        public Vector BaseVelocity
        {
            get => Data->basevelocity;
            set => Data->basevelocity = value;
        }

        public Vector ClientBaseVelocity
        {
            get => Data->clbasevelocity;
            set => Data->clbasevelocity = value;
        }

        /*
        *	Base velocity that was passed in to server physics so 
        *	client can predict conveyors correctly.  Server zeroes it, so we need to store here, too.
        */
        public Vector MoveDirection
        {
            get => Data->movedir;
            set => Data->movedir = value;
        }

        public Vector Angles //Model angles
        {
            get => Data->angles;
            set => Data->angles = value;
        }

        public Vector AngularVelocity //angle velocity (degrees per second)
        {
            get => Data->avelocity;
            set => Data->avelocity = value;
        }

        public Vector PunchAngle //auto-decaying view angle adjustment
        {
            get => Data->punchangle;
            set => Data->punchangle = value;
        }

        //Viewing angle (player only)
        public Vector ViewAngle
        {
            get => Data->v_angle;
            set => Data->v_angle = value;
        }

        //For parametric entities
        public Vector EndPosition
        {
            get => Data->endpos;
            set => Data->endpos = value;
        }

        public Vector StartPosition
        {
            get => Data->startpos;
            set => Data->startpos = value;
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

        public FixAngleMode FixAngle //0:nothing, 1:force view angles, 2:add avelocity
        {
            get => Data->fixangle;
            set => Data->fixangle = value;
        }

        public float IdealPitch
        {
            get => Data->idealpitch;
            set => Data->idealpitch = value;
        }

        public float PitchSpeed
        {
            get => Data->pitch_speed;
            set => Data->pitch_speed = value;
        }

        public float IdealYaw
        {
            get => Data->ideal_yaw;
            set => Data->ideal_yaw = value;
        }

        public float YawSpeed
        {
            get => Data->yaw_speed;
            set => Data->yaw_speed = value;
        }

        public int ModelIndex
        {
            get => Data->modelindex;
            set => Data->modelindex = value;
        }

        public string ModelName
        {
            get => Data->model.ToString();
            set => Data->model = EngineString.FromString(value);
        }

        public string ViewModel //player's viewmodel
        {
            get => Data->viewmodel.ToString();
            set => Data->viewmodel = EngineString.FromString(value);
        }

        public string WeaponModel //what other players see
        {
            get => Data->weaponmodel.ToString();
            set => Data->weaponmodel = EngineString.FromString(value);
        }

        public Vector AbsMin //BB max translated to world coord
        {
            get => Data->absmin;
            set => Data->absmin = value;
        }

        public Vector AbsMax //BB max translated to world coord
        {
            get => Data->absmax;
            set => Data->absmax = value;
        }

        public Vector Mins //local BB min
        {
            get => Data->mins;
            set => Data->mins = value;
        }

        public Vector Maxs //local BB max
        {
            get => Data->maxs;
            set => Data->maxs = value;
        }

        public Vector Size //maxs - mins
        {
            get => Data->size;
            set => Data->size = value;
        }

        public float LastTime
        {
            get => Data->ltime;
            set => Data->ltime = value;
        }

        public float NextThink
        {
            get => Data->nextthink;
            set => Data->nextthink = value;
        }

        public MoveType MoveType
        {
            get => Data->movetype;
            set => Data->movetype = value;
        }

        public Solid Solid
        {
            get => Data->solid;
            set => Data->solid = value;
        }

        public int Skin
        {
            get => Data->skin;
            set => Data->skin = value;
        }

        public int Body //sub-model selection for studiomodels
        {
            get => Data->body;
            set => Data->body = value;
        }

        public EntityEffects Effects
        {
            get => Data->effects;
            set => Data->effects = value;
        }

        public float Gravity //% of "normal" gravity
        {
            get => Data->gravity;
            set => Data->gravity = value;
        }

        public float Friction //inverse elasticity of MOVETYPE_BOUNCE
        {
            get => Data->friction;
            set => Data->friction = value;
        }

        public int LightLevel
        {
            get => Data->light_level;
            set => Data->light_level = value;
        }

        public int Sequence //animation sequence
        {
            get => Data->sequence;
            set => Data->sequence = value;
        }

        public int GaitSequence //movement animation sequence for player (0 for none)
        {
            get => Data->gaitsequence;
            set => Data->gaitsequence = value;
        }

        public float Frame //% playback position in animation sequences (0..255)
        {
            get => Data->frame;
            set => Data->frame = value;
        }

        public float AnimationTime //world time when frame was set
        {
            get => Data->animtime;
            set => Data->animtime = value;
        }

        public float FrameRate //animation playback rate (-8x to 8x)
        {
            get => Data->framerate;
            set => Data->framerate = value;
        }

        //TODO: range checks
        //bone controller setting (0..255)
        public byte GetController(int index) => Data->controller[index];
        public void SetController(int index, byte value) => Data->controller[index] = value;

        //blending amount between sub-sequences (0..255)
        public byte GetBlending(int index) => Data->blending[index];
        public void SetBlending(int index, byte value) => Data->blending[index] = value;

        public float Scale //sprite rendering scale (0..255)
        {
            get => Data->scale;
            set => Data->scale = value;
        }

        public RenderMode RenderMode
        {
            get => Data->rendermode;
            set => Data->rendermode = value;
        }

        public float RenderAmount
        {
            get => Data->renderamt;
            set => Data->renderamt = value;
        }

        public Vector RenderColor
        {
            get => Data->rendercolor;
            set => Data->rendercolor = value;
        }

        public RenderEffect RenderEffect
        {
            get => Data->renderfx;
            set => Data->renderfx = value;
        }

        public float Health
        {
            get => Data->health;
            set => Data->health = value;
        }

        public float Frags
        {
            get => Data->frags;
            set => Data->frags = value;
        }

        public int Weapons //bit mask for available weapons
        {
            get => Data->weapons;
            set => Data->weapons = value;
        }

        public TakeDamageState TakeDamage
        {
            get => (TakeDamageState)Data->takedamage;
            set => Data->takedamage = (float)value;
        }

        public DeadFlag DeadFlag
        {
            get => Data->deadflag;
            set => Data->deadflag = value;
        }

        public Vector ViewOffset //eye position
        {
            get => Data->view_ofs;
            set => Data->view_ofs = value;
        }

        public int Button
        {
            get => Data->button;
            set => Data->button = value;
        }

        public int Impulse
        {
            get => Data->impulse;
            set => Data->impulse = value;
        }

        public Edict Chain
        {
            get => EntityDictionary.EdictFromNative(Data->chain);
            set => Data->chain = EntityDictionary.EdictToNative(value);
        }

        public Edict DamageInflictor
        {
            get => EntityDictionary.EdictFromNative(Data->dmg_inflictor);
            set => Data->dmg_inflictor = EntityDictionary.EdictToNative(value);
        }

        public Edict Enemy
        {
            get => EntityDictionary.EdictFromNative(Data->enemy);
            set => Data->enemy = EntityDictionary.EdictToNative(value);
        }

        public Edict AimEnt
        {
            get => EntityDictionary.EdictFromNative(Data->aiment);
            set => Data->aiment = EntityDictionary.EdictToNative(value);
        }

        public Edict Owner
        {
            get => EntityDictionary.EdictFromNative(Data->owner);
            set => Data->owner = EntityDictionary.EdictToNative(value);
        }

        public Edict GroundEntity
        {
            get => EntityDictionary.EdictFromNative(Data->groundentity);
            set => Data->groundentity = EntityDictionary.EdictToNative(value);
        }

        public uint SpawnFlags
        {
            get => Data->spawnflags;
            set => Data->spawnflags = value;
        }

        public EntFlags Flags
        {
            get => Data->flags;
            set => Data->flags = value;
        }

        //TODO: decompose into parts
        public int ColorMap //lowbyte topcolor, highbyte bottomcolor
        {
            get => Data->colormap;
            set => Data->colormap = value;
        }

        public int Team
        {
            get => Data->team;
            set => Data->team = value;
        }

        public float MaxHealth
        {
            get => Data->max_health;
            set => Data->max_health = value;
        }

        public float TeleportTime
        {
            get => Data->teleport_time;
            set => Data->teleport_time = value;
        }

        public float ArmorType
        {
            get => Data->armortype;
            set => Data->armortype = value;
        }

        public float ArmorValue
        {
            get => Data->armorvalue;
            set => Data->armorvalue = value;
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

        public string Target
        {
            get => Data->target.ToString();
            set => Data->target = EngineString.FromString(value);
        }

        public string TargetName
        {
            get => Data->targetname.ToString();
            set => Data->targetname = EngineString.FromString(value);
        }

        public string NetName
        {
            get => Data->netname.ToString();
            set => Data->netname = EngineString.FromString(value);
        }

        public string Message
        {
            get => Data->message.ToString();
            set => Data->message = EngineString.FromString(value);
        }

        public float DamageTake
        {
            get => Data->dmg_take;
            set => Data->dmg_take = value;
        }

        public float DamageSave
        {
            get => Data->dmg_save;
            set => Data->dmg_save = value;
        }

        public float Damage
        {
            get => Data->dmg;
            set => Data->dmg = value;
        }

        public float DamageTime
        {
            get => Data->dmgtime;
            set => Data->dmgtime = value;
        }

        public string Noise
        {
            get => Data->noise.ToString();
            set => Data->noise = EngineString.FromString(value);
        }

        public string Noise1
        {
            get => Data->noise1.ToString();
            set => Data->noise1 = EngineString.FromString(value);
        }

        public string Noise2
        {
            get => Data->noise2.ToString();
            set => Data->noise2 = EngineString.FromString(value);
        }

        public string Noise3
        {
            get => Data->noise3.ToString();
            set => Data->noise3 = EngineString.FromString(value);
        }

        public float Speed
        {
            get => Data->speed;
            set => Data->speed = value;
        }

        public float AirFinished
        {
            get => Data->air_finished;
            set => Data->air_finished = value;
        }

        public float PainFinished
        {
            get => Data->pain_finished;
            set => Data->pain_finished = value;
        }

        public float RadSuitFinished
        {
            get => Data->radsuit_finished;
            set => Data->radsuit_finished = value;
        }

        public Edict ContainingEntity
        {
            get => EntityDictionary.EdictFromNative(Data->pContainingEntity);
            set => Data->pContainingEntity = EntityDictionary.EdictToNative(value);
        }

        public int PlayerClass
        {
            get => Data->playerclass;
            set => Data->playerclass = value;
        }

        public float MaxSpeed
        {
            get => Data->maxspeed;
            set => Data->maxspeed = value;
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

        public int PushMSec
        {
            get => Data->pushmsec;
            set => Data->pushmsec = value;
        }

        public bool InDuck
        {
            get => 0 != Data->bInDuck;
            set => Data->bInDuck = value ? 1 : 0;
        }

        public int TimeStepSound
        {
            get => Data->flTimeStepSound;
            set => Data->flTimeStepSound = value;
        }

        public int SwimTime
        {
            get => Data->flSwimTime;
            set => Data->flSwimTime = value;
        }

        public int DuckTime
        {
            get => Data->flDuckTime;
            set => Data->flDuckTime = value;
        }

        public bool StepLeft
        {
            get => 0 != Data->iStepLeft;
            set => Data->iStepLeft = value ? 1 : 0;
        }

        public float FallVelocity
        {
            get => Data->flFallVelocity;
            set => Data->flFallVelocity = value;
        }

        public int GameState
        {
            get => Data->gamestate;
            set => Data->gamestate = value;
        }

        public int OldButtons
        {
            get => Data->oldbuttons;
            set => Data->oldbuttons = value;
        }

        public int GroupInfo
        {
            get => Data->groupinfo;
            set => Data->groupinfo = value;
        }

        //For mods
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

        public Edict UserEdict1
        {
            get => EntityDictionary.EdictFromNative(Data->euser1);
            set => Data->euser1 = EntityDictionary.EdictToNative(value);
        }

        public Edict UserEdict2
        {
            get => EntityDictionary.EdictFromNative(Data->euser2);
            set => Data->euser2 = EntityDictionary.EdictToNative(value);
        }

        public Edict UserEdict3
        {
            get => EntityDictionary.EdictFromNative(Data->euser3);
            set => Data->euser3 = EntityDictionary.EdictToNative(value);
        }

        public Edict UserEdict4
        {
            get => EntityDictionary.EdictFromNative(Data->euser4);
            set => Data->euser4 = EntityDictionary.EdictToNative(value);
        }
    }
}
