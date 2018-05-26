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

using Server.Engine.Utility;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Server.Engine
{
    /// <summary>
    /// 3D Vector
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [TypeConverter(typeof(VectorTypeConverter))]
    public struct Vector : IEquatable<Vector>
    {
        public static readonly Vector Zero = new Vector(0, 0, 0);

        //Members
        public float x;
        public float y;
        public float z;

        //As long as we need compatibility with the engine this struct needs to match the layout of an array of floats
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;

                    default: throw new ArgumentOutOfRangeException(nameof(index));
                }
            }

            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;

                    default: throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
        }

        //Construction/destruction
        public Vector(float X, float Y, float Z = 0)
        {
            x = X; y = Y; z = Z;
        }

        public Vector(float XYZ = 0)
        {
            x = XYZ;
            y = XYZ;
            z = XYZ;
        }

        /*
        *	inline Vector(double X, double Y, double Z)		{ x = (float)X; y = (float)Y; z = (float)Z;	}
        *	inline Vector(int X, int Y, int Z)				{ x = (float)X; y = (float)Y; z = (float)Z;	}
        */
        public Vector(Vector v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }

        public static Vector operator -(Vector self)
        {
            return new Vector(-self.x, -self.y, -self.z);
        }

        public static bool operator ==(Vector lhs, Vector rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
        }

        public static bool operator !=(Vector lhs, Vector rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object obj)
        {
            return obj is Vector vec && this == vec;
        }

        public bool Equals(Vector other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            //This is a mutable object, so use identity hash
            //If this were to be made immutable, the calculation below should be good
            return base.GetHashCode();

            //return x.GetHashCode() * 23 + y.GetHashCode() * 17 + z.GetHashCode();
        }

        public static Vector operator +(Vector lhs, Vector rhs)
        {
            return new Vector(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
        }

        public static Vector operator -(Vector lhs, Vector rhs)
        {
            return new Vector(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
        }

        public static Vector operator *(Vector lhs, float fl)
        {
            return new Vector(lhs.x * fl, lhs.y * fl, lhs.z * fl);
        }

        public static Vector operator *(float fl, Vector rhs)
        {
            return rhs * fl;
        }

        public static Vector operator *(Vector lhs, double fl)
        {
            return new Vector((float)(lhs.x * fl), (float)(lhs.y * fl), (float)(lhs.z * fl));
        }

        public static Vector operator *(double fl, Vector rhs)
        {
            return rhs * fl;
        }

        public static Vector operator /(Vector lhs, float fl)
        {
            return new Vector(lhs.x / fl, lhs.y / fl, lhs.z / fl);
        }

        public float Length()
        {
            return (float)Math.Sqrt((x * x) + (y * y) + (z * z));
        }

        public Vector Normalize()
        {
            float flLen = Length();
            if (flLen == 0) return new Vector(0, 0, 1); // ????
            flLen = 1 / flLen;
            return new Vector(x * flLen, y * flLen, z * flLen);
        }

        public Vector2D Make2D()
        {
            Vector2D Vec2;

            Vec2.x = x;
            Vec2.y = y;

            return Vec2;
        }

        public void Clear()
        {
            x = y = z = 0;
        }

        public float Length2D()
        {
            return (float)Math.Sqrt((x * x) + (y * y));
        }

        public float DotProduct(Vector other)
        {
            return (x * other.x) + (y * other.y) + (z * other.z);
        }

        public Vector CrossProduct(Vector other)
        {
            return new Vector(
                (y * other.z) - (z * other.y),
                (z * other.x) - (x * other.z),
                (x * other.y) - (y * other.x)
                );
        }

        public override string ToString()
        {
            return $"Vector{{{x}, {y}, {z}}}";
        }
    }
}
