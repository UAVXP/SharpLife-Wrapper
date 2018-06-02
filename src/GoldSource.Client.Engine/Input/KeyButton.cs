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

namespace GoldSource.Client.Engine.Input
{
    public sealed unsafe class KeyButton
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct Native
        {
            internal fixed int down[2]; // key nums holding it down
            internal int state;			// low bit is down state
        }

        internal Native* Data { get; }

        internal KeyButton(Native* nativeMemory)
        {
            Data = nativeMemory;
        }

        public int Down(int index) => Data->down[index];

        public void SetDown(int index, int value) => Data->down[index] = value;

        public int State
        {
            get => Data->state;
            set => Data->state = value;
        }
    }
}
