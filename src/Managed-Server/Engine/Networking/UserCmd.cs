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

using System.Runtime.InteropServices;

namespace Server.Engine.Networking
{
    public sealed unsafe class UserCmd
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct Native
        {
            internal short lerp_msec;      // Interpolation time on client
            internal byte msec;           // Duration in ms of command
            internal Vector viewangles;     // Command view angles.

            // intended velocities
            internal float forwardmove;    // Forward velocity.
            internal float sidemove;       // Sideways velocity.
            internal float upmove;         // Upward velocity.
            internal byte lightlevel;     // Light level at spot where we are standing.
            internal ushort buttons;  // Attack buttons
            internal byte impulse;          // Impulse command issued.
            internal byte weaponselect;  // Current weapon id

            // Experimental player impact stuff.
            internal int impact_index;
            internal Vector impact_position;
        }

        internal Native* Data { get; }

        internal UserCmd(Native* nativeMemory)
        {
            Data = nativeMemory;
        }

        /// <summary>
        /// Interpolation time on client
        /// </summary>
        public short LerpMSec
        {
            get => Data->lerp_msec;
            set => Data->lerp_msec = value;
        }

        /// <summary>
        /// Duration in ms of command
        /// </summary>
        public byte MSec
        {
            get => Data->msec;
            set => Data->msec = value;
        }

        /// <summary>
        /// Command view angles
        /// </summary>
        public Vector ViewAngles
        {
            get => Data->viewangles;
            set => Data->viewangles = value;
        }

        // intended velocities
        /// <summary>
        /// Forward velocity
        /// </summary>
        public float ForwardMove
        {
            get => Data->forwardmove;
            set => Data->forwardmove = value;
        }

        /// <summary>
        /// Sideways velocity
        /// </summary>
        public float SideMove
        {
            get => Data->sidemove;
            set => Data->sidemove = value;
        }

        /// <summary>
        /// Upward velocity
        /// </summary>
        public float UpMove
        {
            get => Data->upmove;
            set => Data->upmove = value;
        }

        /// <summary>
        /// Light level at spot where we are standing
        /// </summary>
        public byte LightLevel
        {
            get => Data->lightlevel;
            set => Data->lightlevel = value;
        }

        /// <summary>
        /// Attack buttons
        /// </summary>
        public InputKeys Buttons
        {
            get => (InputKeys)Data->buttons;
            set => Data->buttons = (ushort)value;
        }

        /// <summary>
        /// Impulse command issued
        /// </summary>
        public byte Impulse
        {
            get => Data->impulse;
            set => Data->impulse = value;
        }

        /// <summary>
        /// Current weapon id
        /// </summary>
        public byte WeaponSelect
        {
            get => Data->weaponselect;
            set => Data->weaponselect = value;
        }

        // Experimental player impact stuff.
        public int ImpactIndex
        {
            get => Data->impact_index;
            set => Data->impact_index = value;
        }

        public Vector ImpactPosition
        {
            get => Data->impact_position;
            set => Data->impact_position = value;
        }
    }
}
