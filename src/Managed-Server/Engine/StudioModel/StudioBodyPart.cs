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

namespace Server.Engine.StudioModel
{
    public sealed unsafe class StudioBodyPart
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct Native
        {
            internal fixed byte name[64];
            internal int nummodels;
            internal int baseIndex;
	        internal int modelindex; // index into models array
        }

        internal Native* Data { get; }

        internal StudioBodyPart(Native* nativeMemory)
        {
            Data = nativeMemory;
        }

        public string Name => Marshal.PtrToStringUTF8(new IntPtr(Data->name));

        public int NumModels => Data->nummodels;

        public int BaseIndex => Data->baseIndex;

        public int ModelIndex => Data->modelindex;
    }
}
