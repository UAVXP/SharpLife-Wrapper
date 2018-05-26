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

using Server.Wrapper.API.Interfaces;
using System;
using System.Runtime.InteropServices;

namespace Server.Engine.Networking.InfoBuffers
{
    /// <summary>
    /// Base class for info buffers
    /// </summary>
    internal abstract class BaseInfoBuffer : IInfoBuffer
    {
        protected EngineFuncs EngineFuncs { get; }

        protected IntPtr Buffer { get; }

        protected BaseInfoBuffer(EngineFuncs engineFuncs, IntPtr buffer)
        {
            EngineFuncs = engineFuncs ?? throw new ArgumentNullException(nameof(engineFuncs));

            if (buffer == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            Buffer = buffer;
        }

        public string GetValue(string key)
        {
            return Marshal.PtrToStringUTF8(EngineFuncs.pfnInfoKeyValue(Buffer, key));
        }

        public abstract void SetValue(string key, string value);

        public void RemoveKey(string key)
        {
            EngineFuncs.pfnInfo_RemoveKey(Buffer, key);
        }
    }
}
