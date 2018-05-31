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

using System.Runtime.InteropServices;

namespace GoldSource.Shared.Engine
{
    /// <summary>
    /// An offset to glGlobals.pStringBase
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct EngineString
    {
        /// <summary>
        /// The string pool used to back EngineString instances
        /// Must be set prior to usage
        /// </summary>
        public static IStringPool StringPool { get; set; }

        public static readonly EngineString NullString = new EngineString(0);

        public int Offset { get; }

        public EngineString(int offset)
        {
            Offset = offset;
        }

        public bool IsValid => this != NullString;

        public static bool operator ==(EngineString lhs, EngineString rhs)
        {
            return lhs.Offset == rhs.Offset;
        }

        public static bool operator !=(EngineString lhs, EngineString rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is EngineString))
            {
                return false;
            }

            var t = (EngineString)obj;
            return Offset == t.Offset;
        }

        public override int GetHashCode()
        {
            var hashCode = -1986672662;
            hashCode = (hashCode * -1521134295) + base.GetHashCode();
            return (hashCode * -1521134295) + Offset.GetHashCode();
        }

        public static EngineString FromString(string str)
        {
            return StringPool.GetEngineString(str);
        }

        public override string ToString()
        {
            return StringPool.GetString(this);
        }
    }
}
