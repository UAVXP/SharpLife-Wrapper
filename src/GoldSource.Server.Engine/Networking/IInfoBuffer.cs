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

namespace GoldSource.Server.Engine.Networking
{
    /// <summary>
    /// A buffer containing key-value pairs that is networked between clients and the server
    /// </summary>
    public interface IInfoBuffer
    {
        /// <summary>
        /// Get a value for a key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>If the key is in this buffer, the value for that key. Otherwise, an empty string</returns>
        string GetValue(string key);

        /// <summary>
        /// Sets the value for a key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetValue(string key, string value);

        /// <summary>
        /// Removes the given key from this buffer
        /// </summary>
        /// <param name="key"></param>
        void RemoveKey(string key);
    }
}
