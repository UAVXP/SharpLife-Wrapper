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

using System.Runtime.InteropServices;

namespace Server.Engine.Networking
{
    public unsafe struct NetAddress
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct Native
        {
            internal NetAddressType type;
            internal IPv4Address ip;
            internal fixed byte ipx[10]; //IPX is not used
            internal ushort port;
        }

        internal Native* Data { get; }

        internal NetAddress(Native* nativeMemory)
        {
            Data = nativeMemory;
        }

        public NetAddressType Type
        {
            get => Data->type;
            set => Data->type = value;
        }

        public IPv4Address IP
        {
            get => Data->ip;
            set => Data->ip = value;
        }

        public ushort Port
        {
            get => Data->port;
            set => Data->port = value;
        }
    }
}
