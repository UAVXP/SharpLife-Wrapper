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
    /// Thrown when code tries to use a resource that wasn't precached
    /// </summary>
    public sealed class NotPrecachedException : Exception
    {
        public NotPrecachedException()
        {
        }

        public NotPrecachedException(string message)
            : base(message)
        {
        }

        public NotPrecachedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        private NotPrecachedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
