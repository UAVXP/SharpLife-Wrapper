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
using System.Runtime.InteropServices;

namespace GoldSource.Client.Engine.Networking
{
    public sealed unsafe class HUDClientData
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct Native
        {
            // fields that cannot be modified  (ie. have no effect if changed)
            internal Vector origin;

            // fields that can be changed by the cldll
            internal Vector viewangles;
            internal int iWeaponBits;
            //	int		iAccessoryBits;
            internal float fov;	// field of view
        }

        internal Native* Data { get; }

        internal HUDClientData(Native* nativeMemory)
        {
            Data = nativeMemory;
        }

        public Vector Origin => Data->origin;

        public Vector ViewAngles
        {
            get => Data->viewangles;
            set => Data->viewangles = value;
        }

        public int WeaponBits
        {
            get => Data->iWeaponBits;
            set => Data->iWeaponBits = value;
        }

        public float FieldOfView
        {
            get => Data->fov;
            set => Data->fov = value;
        }
    }
}
