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

using GoldSource.Server.Engine.API.Implementations;
using System;
using System.Runtime.InteropServices;

namespace GoldSource.Server.Engine.Wrapper.API.Interfaces
{
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class ServerManagedAPI
    {
        internal enum MemoryType
        {
            ManagedAPI = 0,
            EntityAPI,
            NewEntityAPI,
            EngineOverridesAPI,
            String
        }

        internal const string DelegateInstanceNamePrefix = "pfn";

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void FreeMemory(MemoryType memoryType, IntPtr memory);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal unsafe delegate bool GiveFnptrsToDll(EngineFuncs pengfuncsFromEngine, GlobalVars.Internal* pGlobals);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool GetEntityAPI(out DLLFunctions pFunctionTable);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool GetNewDLLFunctions(out NewDLLFunctions pFunctionTable);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool GetEngineOverrides(out EngineOverrides pFunctionTable);

        //Used to free managed memory
        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal FreeMemory pfnFreeMemory;

        //Used to pass engine and managed interfaces around
        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal GiveFnptrsToDll pfnGiveFnptrsToDll;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal GetEntityAPI pfnGetEntityAPI;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal GetNewDLLFunctions pfnGetNewDLLFunctions;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal GetEngineOverrides pfnGetEngineOverrides;
    }
}
