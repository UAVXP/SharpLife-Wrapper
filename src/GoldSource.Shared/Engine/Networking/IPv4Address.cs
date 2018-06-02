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

namespace GoldSource.Shared.Engine.Networking
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct IPv4Address
    {
        private fixed byte _ip[4];

        public byte this[int index]
        {
            get
            {
                fixed (byte* p = _ip)
                {
                    return p[index];
                }
            }

            set
            {
                fixed (byte* p = _ip)
                {
                    p[index] = value;
                }
            }
        }

        public IPv4Address(byte c0, byte c1, byte c2, byte c3)
        {
            fixed (byte* p = _ip)
            {
                p[0] = c0;
                p[1] = c1;
                p[2] = c2;
                p[3] = c3;
            }
        }
    }
}
