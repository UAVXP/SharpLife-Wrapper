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
using Server.Engine.Persistence;

namespace Server.Game.API
{
    public interface IPersistence
    {
        void Save(Edict pent, SaveRestoreData saveData);

        /// <summary>
        /// Restores the given entity
        /// Return true to keep the entity, false to have it removed
        /// </summary>
        /// <param name="pent"></param>
        /// <param name="saveData"></param>
        /// <param name="isGlobalEntity"></param>
        /// <returns></returns>
        bool Restore(Edict pent, SaveRestoreData saveData, bool isGlobalEntity);

        void SaveGlobalState(SaveRestoreData saveData);

        void RestoreGlobalState(SaveRestoreData saveData);

        void ResetGlobalState();
    }
}
