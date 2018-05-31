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

using Serilog;

namespace GoldSource.Shared
{
    /// <summary>
    /// Contains the logger used by shared code
    /// </summary>
    public static class Logger
    {
        //TODO: initialize this in the wrapper startup
        public static ILogger Instance { get; set; }
    }
}
