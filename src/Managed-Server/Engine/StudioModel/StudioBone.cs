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

namespace Server.Engine.StudioModel
{
    public sealed unsafe class StudioBone
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct Native
        {
            internal fixed byte name[32];  // bone name for symbolic links
            internal int parent;     // parent bone
            internal int flags;      // ??
            internal fixed int bonecontroller[6];  // bone controller index, -1 == none
            internal fixed float value[6]; // default DoF values
            internal fixed float scale[6];   // scale for delta DoF values
        }

        internal Native* Data { get; }

        internal StudioBone(Native* nativeMemory)
        {
            Data = nativeMemory;
        }
    }
}
