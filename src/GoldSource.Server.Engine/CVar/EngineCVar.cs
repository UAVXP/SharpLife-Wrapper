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

using GoldSource.Shared.Wrapper.API;
using System;
using System.Runtime.InteropServices;

namespace GoldSource.Server.Engine.CVar
{
    public sealed unsafe class EngineCVar
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct Native
        {
            internal IntPtr name;
            internal IntPtr stringValue;
	        internal CVarFlags flags;
            internal float value;
            internal Native* next;
        }

        internal Native* Data { get; }

        private bool IsOwned { get; }

        internal EngineCVar(IntPtr nativeMemory)
        {
            Data = (Native*)nativeMemory.ToPointer();

            //Cache the name since it's readonly
            Name = Marshal.PtrToStringUTF8(Data->name);
        }

        /// <summary>
        /// Create an engine cvar to register
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        internal EngineCVar(string name, string value, CVarFlags flags)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Data = (Native*)Marshal.AllocHGlobal(Marshal.SizeOf<Native>()).ToPointer();

            Data->name = InterfaceUtils.AllocateUnmanagedString(name);
            Data->stringValue = InterfaceUtils.AllocateUnmanagedString(value);
            Data->flags = flags;

            IsOwned = true;
        }

        ~EngineCVar()
        {
            if (IsOwned)
            {
                Marshal.FreeHGlobal(Data->name);
                Marshal.FreeHGlobal(new IntPtr(Data));
            }
        }

        public string Name { get; }

        public string String => Marshal.PtrToStringUTF8(Data->stringValue);

        public CVarFlags Flags => Data->flags;

        public float Float => Data->value;

        /// <summary>
        /// Gets the native memory address for the string value
        /// Used during registration to set a valid value without leaking memory
        /// </summary>
        /// <param name="nativeMemory"></param>
        internal IntPtr GetNativeString()
        {
            return Data->stringValue;
        }
    }
}
