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

namespace GoldSource.Shared.Engine
{
    /// <summary>
    /// Interface from the engine to the game that's shared between the client and server
    /// </summary>
    public interface IEngineShared
    {
        /// <summary>
        /// The game directory that the current mod is loaded in
        /// </summary>
        string GameDirectory { get; }
    }
}
