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
using System.Runtime.InteropServices;

namespace Server.Engine
{
    /// <summary>
    /// Used for many pathfinding and many other 
    /// operations that are treated as planar rather than 3d.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2D
    {
        public float x;

        public float y;

        public Vector2D(float X = 0, float Y = 0)
        {
            x = X; y = Y;
        }

        public static Vector2D operator +(Vector2D lhs, Vector2D rhs)
        {
            return new Vector2D(lhs.x + rhs.x, lhs.y + rhs.y);
        }

        public static Vector2D operator -(Vector2D lhs, Vector2D rhs)
        {
            return new Vector2D(lhs.x - rhs.x, lhs.y - rhs.y);
        }

        public static Vector2D operator *(Vector2D lhs, float fl)
        {
            return new Vector2D(lhs.x * fl, lhs.y * fl);
        }

        public static Vector2D operator *(float fl, Vector2D rhs)
        {
            return rhs * fl;
        }

        public static Vector2D operator /(Vector2D lhs, float fl)
        {
            return new Vector2D(lhs.x / fl, lhs.y / fl);
        }

        public float Length()
        {
            return (float)Math.Sqrt((x * x) + (y * y));
        }

        public Vector2D Normalize()
        {
            float flLen = Length();
            if (flLen == 0)
            {
                return new Vector2D(0, 0);
            }
            else
            {
                flLen = 1 / flLen;
                return new Vector2D(x * flLen, y * flLen);
            }
        }

        public float DotProduct(Vector2D other)
        {
            return (x * other.x) + (y * other.y);
        }
    }
}
