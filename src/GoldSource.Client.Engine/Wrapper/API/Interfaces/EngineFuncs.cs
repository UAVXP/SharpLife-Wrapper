﻿/***
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
    //TODO: use sequential layout
    [StructLayout(LayoutKind.Explicit)]
    internal sealed class EngineFuncs
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr GetGameDirectory();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        [FieldOffset(284)]
        internal GetGameDirectory pfnGetGameDirectory;

        internal string GetGameDirectoryHelper()
        {
            var dir = pfnGetGameDirectory();

            return Marshal.PtrToStringUTF8(dir);
        }
    }
}
