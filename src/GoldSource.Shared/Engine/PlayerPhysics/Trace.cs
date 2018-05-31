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
using System;
using System.Runtime.InteropServices;

namespace GoldSource.Shared.Engine.PlayerPhysics
{
    /// <summary>
    /// Actually the engine's internal trace type, exposed by one physics method
    /// </summary>
    public sealed unsafe class Trace
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Native
        {
            internal QBoolean allsolid;  // if true, plane is not valid
            internal QBoolean startsolid;    // if true, the initial point was in a solid area
            internal QBoolean inopen, inwater;
            internal float fraction;     // time completed, 1.0 = didn't hit anything
            internal Vector endpos;          // final position
            internal Plane plane;          // surface normal at impact
            internal IntPtr ent;           // entity the surface is on. Always null
            internal int hitgroup;		// 0 == generic, non zero is specific body part
        }

        internal Native* Data { get; }

        public Trace()
        {
            Data = (Native*)Marshal.AllocHGlobal(Marshal.SizeOf<Native>()).ToPointer();
        }

        ~Trace()
        {
            Marshal.FreeHGlobal(new IntPtr(Data));
        }

        /// <summary>
        /// if true, plane is not valid
        /// </summary>
        public bool AllSolid => Data->allsolid != QBoolean.False;

        /// <summary>
        /// if true, the initial point was in a solid area
        /// </summary>
        public bool StartSolid => Data->startsolid != QBoolean.False;

        /// <summary>
        /// End point is in empty space
        /// </summary>
        public bool InOpen => Data->inopen != QBoolean.False;

        /// <summary>
        /// End point is in water
        /// </summary>
        public bool InWater => Data->inwater != QBoolean.False;

        /// <summary>
        /// time completed, 1.0 = didn't hit anything
        /// </summary>
        public float Fraction => Data->fraction;

        /// <summary>
        /// final position
        /// </summary>
        public Vector EndPos => Data->endpos;

        /// <summary>
        /// surface normal at impact
        /// </summary>
        public Plane Plane => Data->plane;

        //ent is not exposed since it's an Edict, which is only valid on the server to begin with
        //It's also set to null every time

        public int HitGroup => Data->hitgroup;
    }
}
