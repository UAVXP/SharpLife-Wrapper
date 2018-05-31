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
using GoldSource.Shared.Entities;
using System.Runtime.InteropServices;

namespace GoldSource.Server.Engine.Persistence
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct EntityTable
    {
        /// <summary>
        /// Ordinal ID of this entity (used for entity <--> pointer conversions)
        /// </summary>
        internal int id;

        /// <summary>
        /// Pointer to the in-game entity
        /// </summary>
        internal Edict.Native* pent;

        /// <summary>
        /// Offset from the base data of this entity
        /// </summary>
        internal int location;

        /// <summary>
        /// Byte size of this entity's data
        /// </summary>
        internal int size;

        /// <summary>
        /// This could be a short -- bit mask of transitions that this entity is in the PVS of
        /// </summary>
        internal EntTableFlags flags;

        /// <summary>
        /// entity class name
        /// </summary>
        internal EngineString classname;
    }
}
