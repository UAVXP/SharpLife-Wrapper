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

using System;

namespace Server.Engine.CVar
{
    [Flags]
    public enum CVarFlags
    {
        None = 0,

        /// <summary>
        /// set to cause it to be saved to vars.rc
        /// </summary>
        Archive = 1 << 0,

        /// <summary>
        /// changes the client's info string
        /// </summary>
        UserInfo = 1 << 1,

        /// <summary>
        /// notifies players when changed
        /// </summary>
        Server = 1 << 2,

        /// <summary>
        /// defined by external DLL
        /// </summary>
        ExtDLL = 1 << 3,

        /// <summary>
        /// defined by the client dll
        /// </summary>
        ClientDLL = 1 << 4,

        /// <summary>
        /// It's a server cvar, but we don't send the data since it's a password, etc.  Sends 1 if it's not bland/zero, 0 otherwise as value
        /// </summary>
        Protected = 1 << 5,

        /// <summary>
        /// This cvar cannot be changed by clients connected to a multiplayer server
        /// </summary>
        SPOnly = 1 << 6,

        /// <summary>
        /// This cvar's string cannot contain unprintable characters ( e.g., used for player name etc )
        /// </summary>
        PrintableOnly = 1 << 7,

        /// <summary>
        /// If this is a Server, don't log changes to the log file / console if we are creating a log
        /// </summary>
        Unlogged = 1 << 8,

        /// <summary>
        /// strip trailing/leading white space from this cvar
        /// </summary>
        NoExtraWhitespace = 1 << 9,
    }
}
