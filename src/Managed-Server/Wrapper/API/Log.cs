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
using System.IO;
using System.Reflection;

namespace Server.Wrapper.API
{
    /// <summary>
    /// Logging functions for the wrapper interface
    /// </summary>
    internal static class Log
    {
        private const string LOG_FILENAME = "SharpLifeWrapper-Managed.log";

        internal static void Message(string message)
        {
            File.AppendAllText(LOG_FILENAME, $"[{DateTimeOffset.Now}]: {message}{Environment.NewLine}");
        }

        internal static void Exception(Exception e)
        {
            Message($"Exception {e.GetType().Name}: {e.Message}\nStack trace:\n{e.StackTrace}");

            if (e is ReflectionTypeLoadException reflEx)
            {
                Message("Loader exceptions:");
                foreach (var ex in reflEx.LoaderExceptions)
                {
                    Exception(ex);
                }
            }
        }
    }
}
