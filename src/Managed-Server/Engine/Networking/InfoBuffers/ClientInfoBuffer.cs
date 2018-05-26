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

namespace Server.Engine.Networking.InfoBuffers
{
    /// <summary>
    /// Buffer for client info buffers
    /// </summary>
    internal class ClientInfoBuffer : BaseInfoBuffer
    {
        private int ClientIndex { get; }

        internal ClientInfoBuffer(EngineFuncs engineFuncs, IntPtr buffer, int clientIndex)
            : base(engineFuncs, buffer)
        {
            ClientIndex = clientIndex;
        }

        public override void SetValue(string key, string value)
        {
            EngineFuncs.pfnSetClientKeyValue(ClientIndex, Buffer, key, value);
        }
    }
}
