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

namespace GoldSource.Server.Engine.Networking
{
    [Flags]
    public enum ResourceFlags : byte
    {
        /// <summary>
        /// Disconnect if we can't get this file
        /// </summary>
        FatalIfMissing = 1 << 0,

        /// <summary>
        /// Do we have the file locally, did we get it ok?
        /// </summary>
        WasMissing = 1 << 1,

        /// <summary>
        /// Is this resource one that corresponds to another player's customization or is it a server startup resource
        /// </summary>
        Custom = 1 << 2,

        /// <summary>
        /// Already requested a download of this one
        /// </summary>
        Requested = 1 << 3,

        /// <summary>
        /// Already precached
        /// </summary>
        Precached = 1 << 4,

        /// <summary>
        /// download always even if available on client	
        /// </summary>
        Always = 1 << 5,

        /// <summary>
        /// check file on client
        /// </summary>
        CheckFile = 1 << 7,
    }
}
