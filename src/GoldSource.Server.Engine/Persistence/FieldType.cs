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

namespace GoldSource.Server.Engine.Persistence
{
    internal enum FieldType
    {
        Float = 0,          // Any floating point value
        String,             // A string ID (return from ALLOC_STRING)
        Entity,             // An entity offset (EOFFSET)
        ClassPtr,           // CBaseEntity *
        EHandle,            // Entity handle
        EVars,              // EVARS *
        EDict,              // edict_t *, or edict_t *  (same thing)
        Vector,             // Any vector
        PositionVector,     // A world coordinate (these are fixed up across level transitions automagically)
        Pointer,            // Arbitrary data pointer... to be removed, use an array of FIELD_CHARACTER
        Integer,            // Any integer or enum
        Function,           // A class function pointer (Think, Use, etc)
        Boolean,            // boolean, implemented as an int, I may use this as a hint for compression
        Short,              // 2 byte integer
        Character,          // a byte
        Time,               // a floating point time (these are fixed up automatically too!)
        ModelName,          // Engine string that is a model name (needs precache)
        SoundName,          // Engine string that is a sound name (needs precache)

        TypeCount,		    // MUST BE LAST
    }
}
