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
using System.Runtime.Serialization;

namespace GoldSource.Server.Engine.API
{
    /// <summary>
    /// Thrown when a precache call for a resource that is not yet precached is made when precaching is no longer possible
    /// </summary>
    public sealed class LatePrecacheException : Exception
    {
        public LatePrecacheException()
        {
        }

        public LatePrecacheException(string message)
            : base(message)
        {
        }

        public LatePrecacheException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        private LatePrecacheException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
