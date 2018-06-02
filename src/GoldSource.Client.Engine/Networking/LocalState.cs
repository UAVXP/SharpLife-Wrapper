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
using GoldSource.Shared.Engine.Networking;
using System.Runtime.InteropServices;

namespace GoldSource.Client.Engine.Networking
{
    public sealed unsafe class LocalState
    {
        public const int MaxWeapons = 64;

        [StructLayout(LayoutKind.Sequential)]
        internal struct Native
        {
            internal EntityState.Native* playerstate;
            internal ClientData.Native* client;
            internal WeaponData.Native weapondata;//[64];
        }

        internal Native* Data { get; }

        internal LocalState(Native* nativeMemory)
        {
            Data = nativeMemory;
        }

        //TODO
    }
}
