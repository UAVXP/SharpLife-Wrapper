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
using GoldSource.Shared.Engine.PlayerPhysics;
using System;
using System.Runtime.InteropServices;

namespace GoldSource.Client.Engine.Entities
{
    public sealed unsafe class TempEntity
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct Native
        {
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            internal delegate void HitCallback(Native* ent, PMTrace.Native* ptr);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            internal delegate void Callback(Native* ent, float frametime, float currenttime);

            internal int flags;
            internal float die;
            internal float frameMax;
            internal float x;
            internal float y;
            internal float z;
            internal float fadeSpeed;
            internal float bounceFactor;
            internal int hitSound;
            internal IntPtr hitcallback;
            internal IntPtr callback;
            internal Native* next;
            internal int priority;
            internal short clientIndex;  // if attached, this is the index of the client to stick to
                                         // if COLLIDEALL, this is the index of the client to ignore
                                         // TENTS with FTENT_PLYRATTACHMENT MUST set the clientindex! 

            internal Vector tentOffset;      // if attached, client origin + tentOffset = tent origin.
            internal ClientEntity.Native* entity;

            // baseline.origin		- velocity
            // baseline.renderamt	- starting fadeout intensity
            // baseline.angles		- angle velocity
        }

        internal Native* Data { get; }

        //Stored to ensure it stays alive
        internal Native.HitCallback HitCallback;

        internal Native.Callback Callback;

        internal TempEntity(Native* nativeMemory)
        {
            Data = nativeMemory;
        }

        //TODO
    }
}
