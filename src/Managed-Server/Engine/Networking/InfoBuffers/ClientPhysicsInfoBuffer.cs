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
    internal sealed unsafe class ClientPhysicsInfoBuffer : ClientInfoBuffer
    {
        //This is cheating a bit: normally you should use the engine functions for physics strings,
        //But they internally do exactly the same thing as the info buffers so we can just use that instead
        //It's also a bit more efficient
        internal ClientPhysicsInfoBuffer(EngineFuncs engineFuncs, Edict.Native* pClient, int clientIndex)
            : base(engineFuncs ?? throw new ArgumentNullException(nameof(engineFuncs)), engineFuncs.pfnGetPhysicsInfoString(pClient), clientIndex)
        {
        }
    }
}
