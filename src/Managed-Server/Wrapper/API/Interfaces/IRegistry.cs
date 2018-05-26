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

namespace Server.Wrapper.API.Interfaces
{
    /// <summary>
    /// Interface to registry values
    /// </summary>
    internal interface IRegistry
    {
        int ReadInt(string key, int defaultValue = default);

        void WriteInt(string key, int value);

        string ReadString(string key, string defaultValue = default);

        void WriteString(string key, string value);
    }
}
