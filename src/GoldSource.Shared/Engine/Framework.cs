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
    /// Framework constants
    /// </summary>
    public static class Framework
    {
        /// <summary>
        /// The registry path used to access framework keys
        /// </summary>
        public const string WindowsRegistryPath = @"HKEY_CURRENT_USER\Software\Valve\Half-Life\Settings";

        /// <summary>
        /// The Unix registry file name, excluding the extension
        /// </summary>
        public const string UnixRegistryFileName = "hl";

        /// <summary>
        /// The Unix registry file extension
        /// </summary>
        public const string UnixRegistryFileExtension = ".conf";

        /// <summary>
        /// This is the game that is loaded by default
        /// Assets from this game will always be available
        /// </summary>
        public const string DefaultGame = "valve";

        /// <summary>
        /// This is the default language that game data is localized in
        /// </summary>
        public const string DefaultLanguage = "english";

        /// <summary>
        /// The maximum number of clients that can be connected to a server at the same time
        /// </summary>
        public const int MaxClients = 32;

        /// <summary>
        /// Maximum number of bytes in a map name
        /// </summary>
        public const int MaxMapNameLength = 32;
    }
}
