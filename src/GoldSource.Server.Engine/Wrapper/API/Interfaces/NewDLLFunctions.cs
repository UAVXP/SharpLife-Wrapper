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

using GoldSource.Shared.Entities;
using System.Runtime.InteropServices;

namespace GoldSource.Server.Engine.Wrapper.API.Interfaces
{
    [StructLayout(LayoutKind.Sequential)]
    internal sealed unsafe class NewDLLFunctions
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void OnFreeEntPrivateData(Edict.Native* pEnt);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal OnFreeEntPrivateData pfnOnFreeEntPrivateData;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void GameShutdown();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal GameShutdown pfnGameShutdown;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int ShouldCollide(Edict.Native* pentTouched, Edict.Native* pentOther);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal ShouldCollide pfnShouldCollide;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void CvarValue(Edict.Native* pEnt, [MarshalAs(UnmanagedType.LPStr)]string value);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal CvarValue pfnCvarValue;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void CvarValue2(Edict.Native* pEnt, int requestID, [MarshalAs(UnmanagedType.LPStr)]string cvarName, [MarshalAs(UnmanagedType.LPStr)]string value);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal CvarValue2 pfnCvarValue2;
    }
}
