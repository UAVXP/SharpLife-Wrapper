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

namespace GoldSource.Shared.Engine.Sound
{
    public enum SoundChannel
    {
        Auto = 0,
        Weapon = 1,
        Voice = 2,
        Item = 3,
        Body = 4,

        /// <summary>
        /// allocate stream channel from the static or dynamic area
        /// </summary>
        Stream = 5,

        /// <summary>
        /// allocate channel from the static area 
        /// </summary>
        Static = 6,

        /// <summary>
        /// voice data coming across the network
        /// network voice data reserves slots (NetworkVoiceBase through NetworkVoiceEnd).
        /// </summary>
        NetworkVoiceBase = 7,
        NetworkVoiceEnd = 500,

        /// <summary>
        /// channel used for bot chatter
        /// </summary>
        Bot = 501,
    }
}
