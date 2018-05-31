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
using System.Collections.Generic;

namespace GoldSource.Shared.Wrapper.API
{
    /// <summary>
    /// Utilities for API conversion
    /// </summary>
    public static class APIUtils
    {
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
