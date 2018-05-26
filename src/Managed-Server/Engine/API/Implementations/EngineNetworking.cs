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

using Server.Wrapper.API;
using Server.Wrapper.API.Interfaces;
using System;
using System.Runtime.InteropServices;

namespace Server.Engine.API.Implementations
{
    internal sealed class EngineNetworking : IEngineNetworking
    {
        private const int MaxMessageNameLength = 11;

        private const int MaxMessageSize = 192;

        private EngineFuncs EngineFuncs { get; }

        private INetworkMessage CurrentMessage { get; set; }

        public EngineNetworking(EngineFuncs engineFuncs)
        {
            EngineFuncs = engineFuncs ?? throw new ArgumentNullException(nameof(engineFuncs));
        }

        public int RegisterMessage(string name, int size)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (name.Length > MaxMessageNameLength)
            {
                throw new ArgumentException($"Message name \"{name}\" is too long, maximum is {MaxMessageNameLength} characters");
            }

            if (size != -1 && (size < 0 || size > MaxMessageSize))
            {
                throw new ArgumentOutOfRangeException($"Message \"{name}\" size {size} is invalid, must be in range[0, {MaxMessageSize}] or -1 for variable length messages");
            }

            var index = EngineFuncs.pfnRegUserMsg(name, size);

            if (index <= 0)
            {
                throw new InvalidOperationException($"Couldn't register network message {name}, size {size}");
            }

            return index;
        }

        public unsafe INetworkMessage Begin(MsgDest msg_dest, int msg_type, Vector pOrigin, Edict ed = null)
        {
            if (CurrentMessage != null)
            {
                throw new InvalidOperationException("Cannot start a network message while another message is active");
            }

            var originAddress = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Vector)));

            try
            {
                Marshal.StructureToPtr(pOrigin, originAddress, false);
                EngineFuncs.pfnMessageBegin(msg_dest, msg_type, originAddress, ed.Data);
            }
            finally
            {
                Marshal.FreeHGlobal(originAddress);
            }

            CurrentMessage = new NetworkMessage(EngineFuncs, this);

            return CurrentMessage;
        }

        public unsafe INetworkMessage Begin(MsgDest msg_dest, int msg_type, Edict ed = null)
        {
            if (CurrentMessage != null)
            {
                throw new InvalidOperationException("Cannot start a network message while another message is active");
            }

            EngineFuncs.pfnMessageBegin(msg_dest, msg_type, IntPtr.Zero, ed.Data);

            CurrentMessage = new NetworkMessage(EngineFuncs, this);

            return CurrentMessage;
        }

        internal void MessageEnded(INetworkMessage message)
        {
            if (CurrentMessage != message)
            {
                Log.Message("Attempted to end a network message while another was active");
            }

            CurrentMessage = null;
        }
    }
}
