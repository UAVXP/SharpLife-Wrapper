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

using GoldSource.Server.Engine.Wrapper.API.Interfaces;
using System;

namespace GoldSource.Server.Engine.Networking.InfoBuffers
{
    /// <summary>
    /// Buffer for server info buffers
    /// </summary>
    internal sealed class ServerInfoBuffer : BaseInfoBuffer
    {
        internal ServerInfoBuffer(EngineFuncs engineFuncs, IntPtr buffer)
            : base(engineFuncs, buffer)
        {
        }

        public override void SetValue(string key, string value)
        {
            EngineFuncs.pfnSetKeyValue(Buffer, key, value);
        }
    }
}
