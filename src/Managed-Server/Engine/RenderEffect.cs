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

namespace Server.Engine
{
    public enum RenderEffect
    {
        None = 0,

        PulseSlow = 1,

        PulseFast = 2,

        PulseSlowWide = 3,

        PulseFastWide = 4,

        FadeSlow = 5,

        FadeFast = 6,

        SolidSlow = 7,

        SolidFast = 8,

        StrobeSlow = 9,

        StrobeFast = 10,

        StrobeFaster = 11,

        FlickerSlow = 12,

        FlickerFast = 13,

        NoDissipation = 14,

        Distort = 15,

        Hologram = 16,

        DeadPlayer = 17,

        Explode = 18,

        GlowShell = 19,

        ClampMinScale = 20,

        LightMultiplier = 21,
    }
}
