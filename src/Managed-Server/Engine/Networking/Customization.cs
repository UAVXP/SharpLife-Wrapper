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

namespace Server.Engine.Networking
{
    public sealed unsafe class Customization
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct Native
        {
            //Field <name> is never assigned to, and will always have its default value 0
#pragma warning disable CS0649
            internal QBoolean bInUse;
            internal Resource.Native resource;
            internal QBoolean bTranslated;
            internal int nUserData1;
            internal int nUserData2;
            internal void* pInfo;
            internal void* pBuffer;
            internal Native* pNext;
#pragma warning restore CS0649
        }

        internal Native* Data { get; }

        internal Customization(Native* nativeMemory)
        {
            Data = nativeMemory;
        }

        /// <summary>
        /// Is this customization in use?
        /// </summary>
        public bool InUse => QBoolean.False != Data->bInUse;

        /// <summary>
        /// The resource_t for this customization
        /// </summary>
        public Resource Resource => new Resource(&Data->resource);

        /// <summary>
        /// Has the raw data been translated into a useable format?
        /// (e.g., raw decal .wad make into texture_t*)
        /// </summary>
        public bool Translated => QBoolean.False != Data->bTranslated;

        /// <summary>
        /// Customization specific data
        /// </summary>
        public int UserData1 => Data->nUserData1;

        /// <summary>
        /// Customization specific data
        /// </summary>
        public int UserData2 => Data->nUserData2;

        /// <summary>
        /// Buffer that holds the data structure that references the data (e.g., the cachewad_t)
        /// TODO: provide a safe way to access data
        /// </summary>
        public IntPtr Info => new IntPtr(Data->pInfo);

        /// <summary>
        /// Buffer that holds the data for the customization (the raw .wad data)
        /// </summary>
        public IntPtr Buffer => new IntPtr(Data->pBuffer);

        /// <summary>
        /// Next in chain
        /// </summary>
        public Customization Next => new Customization(Data->pNext);
    }
}
