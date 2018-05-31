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

namespace GoldSource.Server.Engine.Persistence
{
    public sealed unsafe class TypeDescription
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct Native
        {
            internal FieldType fieldType;
            internal byte* fieldName;
            internal int fieldOffset;
            internal short fieldSize;
            internal FieldFlags flags;
        }

        internal Native* Data { get; }

        internal TypeDescription(Native* nativeMemory)
        {
            Data = nativeMemory;
        }

        internal FieldType FieldType => Data->fieldType;

        internal string FieldName => Marshal.PtrToStringUTF8(new IntPtr(Data->fieldName));

        internal int FieldOffset => Data->fieldOffset;

        internal short FieldSize => Data->fieldSize;

        internal FieldFlags Flags => Data->flags;
    }
}
