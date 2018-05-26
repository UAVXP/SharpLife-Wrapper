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

using Server.Engine;
using Server.Engine.API;
using Server.Wrapper.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Server.Wrapper.API
{
    /// <summary>
    /// Utilities for API conversion
    /// </summary>
    internal static class APIUtils
    {
        /// <summary>
        /// Converts the current command arguments to a list
        /// </summary>
        /// <param name="engineFuncs"></param>
        /// <returns></returns>
        internal static List<string> ArgsAsList(EngineFuncs engineFuncs)
        {
            var count = engineFuncs.pfnCmd_Argc();

            var list = new List<string>(count);

            for (var i = 0; i < count; ++i)
            {
                var address = engineFuncs.pfnCmd_Argv(i);

                var arg = Marshal.PtrToStringUTF8(address);

                list.Add(arg);
            }

            return list;
        }

        internal static bool IsClient(Edict edict, IEntityDictionary entityDictionary, IGlobalVars globalVars)
        {
            if (edict == null)
            {
                return false;
            }

            var index = entityDictionary.EntityIndex(edict);

            return 0 < index && index <= globalVars.MaxClients;
        }

        /// <summary>
        /// Creates a list around an unmanaged array of elements
        /// </summary>
        /// <typeparam name="TManaged">The managed type to act as a wrapper around each element</typeparam>
        /// <param name="data">Base address of the array</param>
        /// <param name="num">Number of elements in the array</param>
        /// <param name="offset">Starting offset of the array from data</param>
        /// <param name="factory">Factory to create instances of the managed type</param>
        /// <returns></returns>
        public static unsafe IReadOnlyList<TManaged> AllocateListWrapper<TManaged>(byte* data, int num, int offset, Func<IntPtr, TManaged> factory)
        {
            var list = new List<TManaged>(num);

            for (var i = 0; i < num; ++i)
            {
                list.Add(factory(new IntPtr(data + offset + i)));
            }

            return list;
        }
    }
}
