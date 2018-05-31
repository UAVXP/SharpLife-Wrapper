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

namespace GoldSource.Server.Engine.API.Implementations
{
    /// <summary>
    /// Represents a precached resource
    /// </summary>
    internal sealed class Resource
    {
        public string FileName { get; }

        public int Index { get; }

        public Resource(string fileName, int index)
        {
            FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            Index = index;
        }
    }
}
