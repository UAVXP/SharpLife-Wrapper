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

namespace Server.Engine
{
    public enum MsgDest
    {
        Broadcast		= 0,	// unreliable to all
        One				= 1,	// reliable to one (msg_entity)
        All				= 2,	// reliable to all
        Init			= 3,	// write to the init string
        PVS				= 4,	// Ents in PVS of org
        PAS				= 5,	// Ents in PAS of org
        PVS_R			= 6,	// Reliable to PVS
        PAS_R			= 7,	// Reliable to PAS
        One_Unreliable	= 8,	// Send to one client, but don't put in reliable stream, put in unreliable datagram ( could be dropped )
        Spec			= 9,	// Sends to all spectator proxies
    }
}
