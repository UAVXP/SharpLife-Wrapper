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

namespace GoldSource.Server.Engine.API.Implementations
{
    /// <summary>
    /// A cache of precached resources
    /// </summary>
    internal sealed class ResourceCache
    {
        private Dictionary<string, Resource> Resources { get; } = new Dictionary<string, Resource>();

        public Resource Find(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            Resources.TryGetValue(fileName, out var value);

            return value;
        }

        /// <summary>
        /// Tries to add the file resource to the cache
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="allowPrecache">Whether to allow precaching of new resources</param>
        /// <param name="precacheFunc"></param>
        /// <returns>
        /// A tuple of a bool telling whether the file was newly added or if it was already present,
        /// and the resource that represents the file, or null if precaching isn't allowed and the file wasn't precached yet
        /// </returns>
        public (bool, Resource) TryAdd(string fileName, bool allowPrecache, Func<int> precacheFunc)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException(nameof(fileName));
            }

            var resource = Find(fileName);

            if (resource != null)
            {
                return (false, resource);
            }

            if (!allowPrecache)
            {
                return (false, null);
            }

            resource = new Resource(fileName, precacheFunc());

            Resources.Add(fileName, resource);

            return (true, resource);
        }
    }
}
