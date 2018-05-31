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

namespace GoldSource.Shared.Engine
{
    public interface IStringPool
    {
        /// <summary>
        /// Gets an unmanaged memory address of a given string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        IntPtr GetPooledString(string str);

        /// <summary>
        /// Clears all strings
        /// Should be used if the amount of memory used is getting too much, and there is a point where no pooled strings are in use (e.g. map change)
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets the engine string offset of a string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        EngineString GetEngineString(string str);

        /// <summary>
        /// Gets the string of an engine string
        /// </summary>
        /// <param name="str"></param>
        /// <returns>If the given offset is valid returns the string, otherwise returns null</returns>
        string GetString(EngineString str);
    }
}
