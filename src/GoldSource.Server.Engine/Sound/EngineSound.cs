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

using GoldSource.Mathlib;
using GoldSource.Server.Engine.Wrapper.API.Interfaces;
using GoldSource.Shared.Engine.Sound;
using GoldSource.Shared.Entities;
using System;

namespace GoldSource.Server.Engine.Sound
{
    internal unsafe sealed class EngineSound : IEngineSound
    {
        private EngineFuncs EngineFuncs { get; }

        public EngineSound(EngineFuncs engineFuncs)
        {
            EngineFuncs = engineFuncs ?? throw new ArgumentNullException(nameof(engineFuncs));
        }

        public void EmitSound(Edict edict, SoundChannel channel, string sample, float volume, float attenuation, SoundFlags flags, int pitch)
        {
            EngineFuncs.pfnEmitSound(edict.Data, channel, sample, volume, attenuation, flags, pitch);
        }

        public void EmitAmbientSound(Edict edict, in Vector position, string sample, float volume, float attenuation, SoundFlags flags, int pitch)
        {
            EngineFuncs.pfnEmitAmbientSound(edict.Data, position, sample, volume, attenuation, flags, pitch);
        }
    }
}
