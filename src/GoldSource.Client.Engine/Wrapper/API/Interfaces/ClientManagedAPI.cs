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

namespace GoldSource.Client.Engine.Wrapper.API.Interfaces
{
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class ClientManagedAPI
    {
        internal enum MemoryType
        {
            ManagedAPI = 0,
            ClientAPI,
            String
        }

        internal const string DelegateInstanceNamePrefix = "pfn";

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void FreeMemory(MemoryType memoryType, IntPtr memory);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool Initialize(EngineFuncs pengfuncsFromEngine);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool GetClientDllInterface(out ClientDLLFunctions pFunctionTable);

        //Used to free managed memory
        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal FreeMemory pfnFreeMemory;

        //Used to pass engine and managed interfaces around
        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal Initialize pfnInitialize;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal GetClientDllInterface pfnGetClientDllInterface;
    }
}
