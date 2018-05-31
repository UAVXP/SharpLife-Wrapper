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

namespace GoldSource.Server.Engine
{
    /// <summary>
    /// Entity keyvalue data
    /// </summary>
    public unsafe sealed class KeyValueData
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct Native
        {
            //Field <name> is never assigned to, and will always have its default value
#pragma warning disable CS0649
            internal byte* szClassName;
            internal byte* szKeyName;
            internal byte* szValue;
            internal int fHandled;
#pragma warning restore CS0649
        }

        internal Native* Data { get; }

        internal KeyValueData(Native* nativeMemory)
        {
            Data = nativeMemory;

            //Cache off the strings to avoid expensive conversions executing multiple times
            KeyName = Marshal.PtrToStringUTF8(new IntPtr(Data->szKeyName));
            Value = Marshal.PtrToStringUTF8(new IntPtr(Data->szValue));

            //If this keyvalue is "classname", then this field is null
            //To avoid having to handle this logic in game code, just use the same string
            ClassName = Data->szClassName != null ? Marshal.PtrToStringUTF8(new IntPtr(Data->szClassName)) : Value;
        }

        /// <summary>
        /// in: entity classname
        /// </summary>
        public string ClassName { get; }

        /// <summary>
        /// in: name of key
        /// </summary>
        public string KeyName { get; }

        /// <summary>
        /// in: value of key
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// out: DLL sets to true if key-value pair was understood
        /// </summary>
        public bool Handled
        {
            get => 0 != Data->fHandled;
            set => Data->fHandled = value ? 1 : 0;
        }
    }
}
