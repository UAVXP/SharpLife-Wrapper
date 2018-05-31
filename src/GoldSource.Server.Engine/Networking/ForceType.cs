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
    /// For integrity checking of content on clients
    /// </summary>
    public enum ForceType
    {
        ExactFile,                      // File on client must exactly match server's file
        ModelSameBounds,                // For model files only, the geometry must fit in the same bbox
        ModelSpecifyBounds,             // For model files only, the geometry must fit in the specified bbox
        ModelSpecifyBoundsIfAvail,	    // For Steam model files only, the geometry must fit in the specified bbox (if the file is available)
    }
}
