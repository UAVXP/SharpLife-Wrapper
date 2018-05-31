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

using GoldSource.Mathlib;
using GoldSource.Server.Engine.Networking;
using GoldSource.Shared.Engine;
using GoldSource.Shared.Engine.Networking;
using GoldSource.Shared.Entities;
using System;
using System.Collections.Generic;

namespace GoldSource.Server.Engine.Game.API
{
    /// <summary>
    /// Takes care of networking operations in the game
    /// </summary>
    public interface INetworking
    {
        /// <summary>
        /// Sets up visibility for a given client with a view entity to look from
        /// </summary>
        /// <param name="pViewEntity"></param>
        /// <param name="pClient"></param>
        /// <param name="pvs"></param>
        /// <param name="pas"></param>
        void SetupVisibility(Edict pViewEntity, Edict pClient, out IntPtr pvs, out IntPtr pas);

        void UpdateClientData(Edict ent, bool sendWeapons, ClientData cd);

        bool AddToFullPack(EntityState state, int e, Edict ent, Edict host, HostFlags hostFlags, bool isPlayer, IntPtr pSet);

        void CreateBaseline(bool isPlayer, int eindex, EntityState baseline, Edict entity, int playermodelindex, in Vector playerMins, in Vector playerMaxs);

        //TODO: needs to pass an object to register to
        void RegisterEncoders();

        bool GetWeaponData(Edict player, IReadOnlyList<WeaponData> info);

        void CmdStart(Edict player, UserCmd cmd, uint randomSeed);

        void CmdEnd(Edict player);

        void CreateInstancedBaselines();

        bool InconsistentFile(Edict player, string fileName, out string disconnectMessage);

        bool AllowLagCompensation();
    }
}
