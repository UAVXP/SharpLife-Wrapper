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

namespace Server.Game.API
{
    public interface IEntities
    {
        void LoadEntities(string data);

        void OnFreeEntPrivateData(Edict pEnt);

        int Spawn(Edict pent);

        void Think(Edict pent);

        void Use(Edict pentUsed, Edict pentOther);

        void Touch(Edict pentTouched, Edict pentOther);

        void Blocked(Edict pentBlocked, Edict pentOther);

        void KeyValue(Edict pentKeyvalue, KeyValueData pkvd);

        void SetAbsBox(Edict pent);

        bool ShouldCollide(Edict pentTouched, Edict pentOther);
    }
}
