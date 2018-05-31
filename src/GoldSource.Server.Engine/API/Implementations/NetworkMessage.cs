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

namespace GoldSource.Server.Engine.API.Implementations
{
    internal sealed class NetworkMessage : INetworkMessage
    {
        private EngineFuncs EngineFuncs { get; }

        private bool Ended { get; }

        private EngineNetworking EngineNetworking { get; }

        public NetworkMessage(EngineFuncs engineFuncs, EngineNetworking engineNetworking)
        {
            EngineFuncs = engineFuncs ?? throw new ArgumentNullException(nameof(engineFuncs));
            EngineNetworking = engineNetworking ?? throw new ArgumentNullException(nameof(engineNetworking));
        }

        public void End()
        {
            if (Ended)
            {
                throw new InvalidOperationException("Cannot end an already ended network message");
            }

            EngineFuncs.pfnMessageEnd();

            EngineNetworking.MessageEnded(this);
        }

        public void WriteByte(int value)
        {
            if (Ended)
            {
                throw new InvalidOperationException("Cannot write to a network message that has been ended");
            }

            EngineFuncs.pfnWriteByte(value);
        }

        public void WriteChar(int value)
        {
            if (Ended)
            {
                throw new InvalidOperationException("Cannot write to a network message that has been ended");
            }

            EngineFuncs.pfnWriteChar(value);
        }

        public void WriteShort(int value)
        {
            if (Ended)
            {
                throw new InvalidOperationException("Cannot write to a network message that has been ended");
            }

            EngineFuncs.pfnWriteShort(value);
        }

        public void WriteLong(int value)
        {
            if (Ended)
            {
                throw new InvalidOperationException("Cannot write to a network message that has been ended");
            }

            EngineFuncs.pfnWriteLong(value);
        }

        public void WriteAngle(float value)
        {
            if (Ended)
            {
                throw new InvalidOperationException("Cannot write to a network message that has been ended");
            }

            EngineFuncs.pfnWriteAngle(value);
        }

        public void WriteCoord(float value)
        {
            if (Ended)
            {
                throw new InvalidOperationException("Cannot write to a network message that has been ended");
            }

            EngineFuncs.pfnWriteCoord(value);
        }

        public void WriteString(string str)
        {
            if (Ended)
            {
                throw new InvalidOperationException("Cannot write to a network message that has been ended");
            }

            EngineFuncs.pfnWriteString(str);
        }

        public void WriteEntity(int value)
        {
            if (Ended)
            {
                throw new InvalidOperationException("Cannot write to a network message that has been ended");
            }

            EngineFuncs.pfnWriteEntity(value);
        }
    }
}
