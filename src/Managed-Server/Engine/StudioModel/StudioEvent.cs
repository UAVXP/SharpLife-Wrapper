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
    public sealed unsafe class StudioEvent
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct Native
        {
            internal int frame;
            internal int eventId;

            internal int type;
            internal fixed byte options[64];
        }

        internal Native* Data { get; }

        internal StudioEvent(Native* nativeMemory)
        {
            Data = nativeMemory;
        }

        public int Frame => Data->frame;

        public int EventId => Data->eventId;

        public int Type => Data->type;

        public string Options => Marshal.PtrToStringUTF8(new IntPtr(Data->options));
    }
}
