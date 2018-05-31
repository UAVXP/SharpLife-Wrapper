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

namespace GoldSource.Shared.Engine.PlayerPhysics
{
    public sealed unsafe class MoveVars
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Native
        {
            internal float gravity;           // Gravity for map
            internal float stopspeed;         // Deceleration when not moving
            internal float maxspeed;          // Max allowed speed
            internal float spectatormaxspeed;
            internal float accelerate;        // Acceleration factor
            internal float airaccelerate;     // Same for when in open air
            internal float wateraccelerate;   // Same for when in water
            internal float friction;
            internal float edgefriction;    // Extra friction near dropofs 
            internal float waterfriction;     // Less in water
            internal float entgravity;        // 1.0
            internal float bounce;            // Wall bounce value. 1.0
            internal float stepsize;          // sv_stepsize;
            internal float maxvelocity;       // maximum server velocity.
            internal float zmax;            // Max z-buffer range (for GL)
            internal float waveHeight;          // Water wave height (for GL)
            internal QBoolean footsteps;        // Play footstep sounds
            internal fixed byte skyName[32];      // Name of the sky map
            internal float rollangle;
            internal float rollspeed;
            internal float skycolor_r;           // Sky color
            internal float skycolor_g;           // 
            internal float skycolor_b;           //
            internal float skyvec_x;         // Sky vector
            internal float skyvec_y;         // 
            internal float skyvec_z;			// 
        }

        internal Native* Data { get; }

        public MoveVars(Native* nativeMemory)
        {
            Data = nativeMemory;
        }

        /// <summary>
        /// Gravity for map
        /// </summary>
        public float Gravity => Data->gravity;

        /// <summary>
        /// Deceleration when not moving
        /// </summary>
        public float StopSpeed => Data->stopspeed;

        /// <summary>
        /// Max allowed speed
        /// </summary>
        public float MaxSpeed => Data->maxspeed;

        public float SpectatorMaxSpeed => Data->spectatormaxspeed;

        /// <summary>
        /// Acceleration factor
        /// </summary>
        public float Accelerate => Data->accelerate;

        /// <summary>
        /// Same for when in open air
        /// </summary>
        public float AirAccelerate => Data->airaccelerate;

        /// <summary>
        /// Same for when in water
        /// </summary>
        public float WaterAccelerate => Data->wateraccelerate;

        public float Friction => Data->friction;

        /// <summary>
        /// Extra friction near dropofs 
        /// </summary>
        public float EdgeFriction => Data->edgefriction;

        /// <summary>
        /// Less in water
        /// </summary>
        public float WaterFriction => Data->waterfriction;

        /// <summary>
        /// 1.0
        /// </summary>
        public float EntGravity => Data->entgravity;

        /// <summary>
        /// Wall bounce value. 1.0
        /// </summary>
        public float Bounce => Data->bounce;

        /// <summary>
        /// sv_stepsize
        /// </summary>
        public float StepSize => Data->stepsize;

        /// <summary>
        /// maximum server velocity
        /// </summary>
        public float MaxVelocity => Data->maxvelocity;

        /// <summary>
        /// Max z-buffer range (for GL)
        /// </summary>
        public float ZMax => Data->zmax;

        /// <summary>
        /// Water wave height (for GL)
        /// </summary>
        public float WaveHeight => Data->waveHeight;

        /// <summary>
        /// Play footstep sounds
        /// </summary>
        public bool Footsteps => Data->footsteps != QBoolean.False;

        /// <summary>
        /// Name of the sky map
        /// </summary>
        public string SkyName => Marshal.PtrToStringUTF8(new IntPtr(Data->skyName));
        public float RollAngle => Data->rollangle;
        public float RollSpeed => Data->rollspeed;

        /// <summary>
        /// Sky color
        /// </summary>
        public float SkyColorR => Data->skycolor_r;
        public float SkyColorG => Data->skycolor_g;
        public float SkyColorB => Data->skycolor_b;

        /// <summary>
        /// Sky vector
        /// </summary>
        public float SkyVecX => Data->skyvec_x;
        public float SkyVecY => Data->skyvec_y;
        public float SkyVecZ => Data->skyvec_z;
    }
}
