﻿/***
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

namespace GoldSource.Registry
{
    //This depends on a Windows only package
#if WINDOWS_BUILD
    internal sealed class WindowsRegistry : IRegistry
    {
        private string Key { get; }

        internal WindowsRegistry(string key)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
        }

        public int ReadInt(string key, int defaultValue = default)
        {
            return (int)Microsoft.Win32.Registry.GetValue(Key, key, defaultValue);
        }

        public void WriteInt(string key, int value)
        {
            Microsoft.Win32.Registry.SetValue(Key, key, value);
        }

        public string ReadString(string key, string defaultValue = default)
        {
            return (string)Microsoft.Win32.Registry.GetValue(Key, key, defaultValue);
        }

        public void WriteString(string key, string value)
        {
            Microsoft.Win32.Registry.SetValue(Key, key, value);
        }
    }
#endif
}
