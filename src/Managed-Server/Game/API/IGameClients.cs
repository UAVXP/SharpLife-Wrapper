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
using Server.Engine.Networking;

namespace Server.Game.API
{
    public interface IGameClients
    {
        bool Connect(Edict entity, string name, string address, out string rejectReason);
        void Disconnect(Edict entity);

        void Kill(Edict pEntity);

        void PutInServer(Edict pEntity);

        void Command(Edict pEntity, ICommand command);

        void UserInfoChanged(Edict pEntity, IInfoBuffer infoBuffer);

        void PreThink(Edict pEntity);

        void PostThink(Edict pEntity);

        void Customization(Edict entity, Customization custom);
    }
}
