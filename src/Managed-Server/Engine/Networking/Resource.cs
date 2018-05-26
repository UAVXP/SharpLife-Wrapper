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
    public sealed unsafe class Resource
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct Native
        {
            //Field <name> is never assigned to, and will always have its default value 0
#pragma warning disable CS0649
            internal fixed byte szFileName[InternalConstants.MaxQPath];
            internal ResourceType type;
            internal int nIndex;
            internal int nDownloadSize;
            internal ResourceFlags ucFlags;

            internal fixed byte rgucMD5_hash[InternalConstants.MD5HashSize];
            internal byte playernum;

            internal fixed byte rguc_reserved[32];
            internal Native* pNext;
            internal Native* pPrev;
#pragma warning restore CS0649
        }

        internal Native* Data { get; }

        internal Resource(Native* nativeMemory)
        {
            Data = nativeMemory;
        }

        /// <summary>
        /// File name to download/precache
        /// </summary>
        public string FileName => Marshal.PtrToStringUTF8(new IntPtr(Data->szFileName));

        /// <summary>
        /// Sound, Skin, Model, Decal
        /// </summary>
        public ResourceType Type => Data->type;

        /// <summary>
        /// For Decal
        /// </summary>
        public int Index => Data->nIndex;

        /// <summary>
        /// Size in Bytes if this must be downloaded
        /// </summary>
        public int DownloadSize => Data->nDownloadSize;

        public ResourceFlags Flags => Data->ucFlags;

        // For handling client to client resource propagation
        /// <summary>
        /// To determine if we already have it
        /// </summary>
        public byte[] MD5Hash
        {
            get
            {
                var hash = new byte[InternalConstants.MD5HashSize];

                Marshal.Copy(new IntPtr(Data->rgucMD5_hash), hash, 0, hash.Length);

                return hash;
            }
        }

        /// <summary>
        /// Which player index this resource is associated with, if it's a custom resource
        /// </summary>
        public byte playernum;

        /// <summary>
        /// Next in chain
        /// </summary>
        public Resource Next => new Resource(Data->pNext);

        public Resource Prev => new Resource(Data->pPrev);
    }
}
