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
    /// <summary>
    /// pitch values
    /// </summary>
    public static class Pitch
    {
        /// <summary>
        /// non-pitch shifted
        /// </summary>
        public const int Normal = 100;

        /// <summary>
        /// other values are possible - 0-255, where 255 is very high
        /// </summary>
        public const int Low = 95;

        public const int High = 120;
    }
}
