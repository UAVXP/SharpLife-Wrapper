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
    public sealed unsafe class PMTrace
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct Native
        {
            internal QBoolean allsolid;        // if true, plane is not valid
            internal QBoolean startsolid;          // if true, the initial point was in a solid area
            internal QBoolean inopen, inwater;  // End point is in empty space or in water
            internal float fraction;       // time completed, 1.0 = didn't hit anything
            internal Vector endpos;            // final position
            internal Plane plane;              // surface normal at impact
            internal int ent;              // entity at impact
            internal Vector deltavelocity;    // Change in player's velocity caused by impact.  
                                              // Only run on server.
            internal int hitgroup;
        }

        internal Native* Data { get; }

        private bool Owned { get; }

        internal PMTrace(Native* nativeMemory)
        {
            Data = nativeMemory;
        }

        public PMTrace()
        {
            Data = (Native*)Marshal.AllocHGlobal(Marshal.SizeOf<Native>()).ToPointer();
        }

        ~PMTrace()
        {
            if (Owned)
            {
                Marshal.FreeHGlobal(new IntPtr(Data));
            }
        }

        /// <summary>
        /// Assigns the contents of the given trace to this one
        /// </summary>
        /// <param name="other"></param>
        public void Assign(PMTrace other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            *Data = *other.Data;
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

        /// <summary>
        /// entity at impact
        /// </summary>
        public int Ent => Data->ent;

        /// <summary>
        /// Change in player's velocity caused by impact
        /// Only run on server
        /// </summary>
        public Vector DeltaVelocity
        {
            get => Data->deltavelocity;
            set => Data->deltavelocity = value;
        }

        public int HitGroup => Data->hitgroup;
    }
}
