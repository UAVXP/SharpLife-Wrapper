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

namespace GoldSource.Server.Engine.API
{
    /// <summary>
    /// Allows writing to a network message
    /// </summary>
    public interface INetworkMessage
    {
        /// <summary>
        /// Ends writing and prepares the message for sending
        /// It is no longer possible to write to the message after this
        /// </summary>
        void End();

        void WriteByte(int value);

        void WriteChar(int value);

        void WriteShort(int value);

        void WriteLong(int value);

        void WriteAngle(float value);

        void WriteCoord(float value);

        void WriteString(string str);

        void WriteEntity(int value);
    }
}
