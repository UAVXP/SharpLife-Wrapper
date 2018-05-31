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
using GoldSource.Shared.Engine.Sound;
using GoldSource.Shared.Entities;

namespace GoldSource.Server.Engine.Sound
{
    public interface IEngineSound
    {
        void EmitSound(Edict edict, SoundChannel channel, string sample, float volume, float attenuation, SoundFlags flags, int pitch);

        void EmitAmbientSound(Edict edict, in Vector position, string sample, float volume, float attenuation, SoundFlags flags, int pitch);
    }
}
