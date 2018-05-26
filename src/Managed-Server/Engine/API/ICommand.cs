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

using System.Collections.Generic;

namespace Server.Engine.API
{
    /// <summary>
    /// Contains command arguments for console and client commands
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// The name of the command being executed
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The number of arguments
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets command arguments by index
        /// </summary>
        /// <param name="index">The zero based index of the argument. The command name is not included in the argument list</param>
        /// <returns></returns>
        string this[int index] { get; }

        /// <summary>
        /// Gets the command arguments as a list
        /// </summary>
        IReadOnlyList<string> Arguments { get; }

        /// <summary>
        /// Gets the command arguments as a single string, with arguments that contain whitespace having quotes added if addQuotes is true
        /// </summary>
        /// <param name="addQuotes"></param>
        /// <returns></returns>
        string ArgumentsAsString(bool addQuotes = true);
    }
}
