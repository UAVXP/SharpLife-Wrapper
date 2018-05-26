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
using Server.Engine.API.Implementations;
using Server.Engine.Networking;
using Server.Engine.Networking.InfoBuffers;
using Server.Engine.Persistence;
using Server.Engine.PlayerPhysics;
using Server.Game.API;
using Server.Wrapper.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Server.Wrapper.API.Implementations
{
    internal sealed unsafe class DLLFunctions
    {
        private EngineFuncs EngineFuncs { get; }

        private IGlobalVars Globals { get; }

        private EntityDictionary EntityDictionary { get; }

        private IServerInterface ServerInterface { get; }

        private IEntities Entities { get; }

        private IGameClients GameClients { get; }

        private INetworking Networking { get; }

        private IPersistence Persistence { get; }

        private IPlayerPhysics PlayerPhysics { get; }

        private PlayerMove PlayerMove { get; set; }

        private PlayerMoveFunctions PlayerMoveFunctions { get; set; }

        private IEnginePhysics EnginePhysics { get; set; }

        /// <summary>
        /// Tracks whether the next call to KeyValue is the first
        /// If true, we'll need to do some setup code to make interop work
        /// </summary>
        private bool FirstKeyValueCall { get; set; } = true;

        public DLLFunctions(
            EngineFuncs engineFuncs,
            IGlobalVars globals,
            EntityDictionary entityDictionary,
            IServerInterface serverInterface,
            IEntities entities,
            IGameClients gameClients,
            INetworking networking,
            IPersistence persistence,
            IPlayerPhysics playerPhysics)
        {
            EngineFuncs = engineFuncs ?? throw new ArgumentNullException(nameof(engineFuncs));
            Globals = globals ?? throw new ArgumentNullException(nameof(globals));
            EntityDictionary = entityDictionary ?? throw new ArgumentNullException(nameof(entityDictionary));
            ServerInterface = serverInterface ?? throw new ArgumentNullException(nameof(serverInterface));
            Entities = entities ?? throw new ArgumentNullException(nameof(entities));
            GameClients = gameClients ?? throw new ArgumentNullException(nameof(gameClients));
            Networking = networking ?? throw new ArgumentNullException(nameof(networking));
            Persistence = persistence ?? throw new ArgumentNullException(nameof(persistence));
            PlayerPhysics = playerPhysics ?? throw new ArgumentNullException(nameof(playerPhysics));
        }

        internal void GameInit()
        {
            Log.Message("Game initialized");

            try
            {
                ServerInterface.Initialize();
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal int Spawn(Edict.Native* pent)
        {
            try
            {
                return Entities.Spawn(EntityDictionary.EdictFromNative(pent));
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void Think(Edict.Native* pent)
        {
            try
            {
                Entities.Think(EntityDictionary.EdictFromNative(pent));
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void Use(Edict.Native* pentUsed, Edict.Native* pentOther)
        {
            try
            {
                Entities.Use(EntityDictionary.EdictFromNative(pentUsed), EntityDictionary.EdictFromNative(pentOther));
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void Touch(Edict.Native* pentTouched, Edict.Native* pentOther)
        {
            try
            {
                Entities.Touch(EntityDictionary.EdictFromNative(pentTouched), EntityDictionary.EdictFromNative(pentOther));
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void Blocked(Edict.Native* pentBlocked, Edict.Native* pentOther)
        {
            try
            {
                Entities.Blocked(EntityDictionary.EdictFromNative(pentBlocked), EntityDictionary.EdictFromNative(pentOther));
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void KeyValue(Edict.Native* pentKeyvalue, KeyValueData.Native* pkvd)
        {
            try
            {
                if (FirstKeyValueCall)
                {
                    FirstKeyValueCall = false;

                    var pEdictList = EngineFuncs.pfnPEntityOfEntOffset(0);

                    Log.Message($"Initializing entity dictionary with 0x{(uint)pEdictList:X} as edict list address");

                    EntityDictionary.Initialize(pEdictList, Globals.MaxEntities);

                    Log.Message("Finished initializing entity dictionary");
                }

                Entities.KeyValue(EntityDictionary.EdictFromNative(pentKeyvalue), new KeyValueData(pkvd));
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void Save(Edict.Native* pent, SaveRestoreData.Native* pSaveData)
        {
            try
            {
                Persistence.Save(EntityDictionary.EdictFromNative(pent), new SaveRestoreData(pSaveData));
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal int Restore(Edict.Native* pent, SaveRestoreData.Native* pSaveData, int globalEntity)
        {
            try
            {
                return Persistence.Restore(EntityDictionary.EdictFromNative(pent), new SaveRestoreData(pSaveData), globalEntity != 0) ? 0 : -1;
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void SetAbsBox(Edict.Native* pent)
        {
            try
            {
                Entities.SetAbsBox(EntityDictionary.EdictFromNative(pent));
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void SaveWriteFields(SaveRestoreData.Native* pSaveData, string pname, void* pBaseData, TypeDescription.Native* pFields, int fieldCount)
        {
            //TODO: handle internally?
            //We need the bare minimum data to allow VGUI2's saved game list to work
            try
            {
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void SaveReadFields(SaveRestoreData.Native* pSaveData, string pname, void* pBaseData, TypeDescription.Native* pFields, int fieldCount)
        {
            //TODO: handle internally?
            //We need the bare minimum data to allow VGUI2's saved game list to work
            try
            {
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void SaveGlobalState(SaveRestoreData.Native* pSaveData)
        {
            try
            {
                Persistence.SaveGlobalState(new SaveRestoreData(pSaveData));
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void RestoreGlobalState(SaveRestoreData.Native* pSaveData)
        {
            try
            {
                Persistence.RestoreGlobalState(new SaveRestoreData(pSaveData));
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void ResetGlobalState()
        {
            try
            {
                Persistence.ResetGlobalState();
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal QBoolean ClientConnect(Edict.Native* pEntity, string name, string address, byte* szRejectReason)
        {
            try
            {
                var result = GameClients.Connect(EntityDictionary.EdictFromNative(pEntity), name, address, out var rejectReason);

                InterfaceUtils.CopyStringToUnmanagedBuffer(rejectReason ?? string.Empty, szRejectReason, Interfaces.DLLFunctions.ClientConnectRejectReasonLength);

                return result ? QBoolean.True : QBoolean.False;
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void ClientDisconnect(Edict.Native* pEntity)
        {
            try
            {
                GameClients.Disconnect(EntityDictionary.EdictFromNative(pEntity));
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void ClientKill(Edict.Native* pEntity)
        {
            try
            {
                GameClients.Kill(EntityDictionary.EdictFromNative(pEntity));
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void ClientPutInServer(Edict.Native* pEntity)
        {
            try
            {
                GameClients.PutInServer(EntityDictionary.EdictFromNative(pEntity));
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void ClientCommand(Edict.Native* pEntity)
        {
            try
            {
                GameClients.Command(EntityDictionary.EdictFromNative(pEntity), new Command(APIUtils.ArgsAsList(EngineFuncs)));
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void ClientUserInfoChanged(Edict.Native* pEntity, IntPtr infoBuffer)
        {
            try
            {
                var edict = EntityDictionary.EdictFromNative(pEntity);
                GameClients.UserInfoChanged(edict, new ClientInfoBuffer(EngineFuncs, infoBuffer, EntityDictionary.EntityIndex(edict)));
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

#pragma warning disable RCS1163 // Unused parameter.
        internal void ServerActivate(Edict.Native* pEdictList, int edictCount, int clientMax)
#pragma warning restore RCS1163 // Unused parameter.
        {
            try
            {
                //This is done in KeyValue because we need the list earlier than this gets called
                //EntityDictionary.Initialize(pEdictList, Program.Wrapper.Globals.MaxEntities);

                ServerInterface.Activate();
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void ServerDeactivate()
        {
            try
            {
                ServerInterface.Deactivate();

                //The next call will be to create entities after map change/saved game load
                FirstKeyValueCall = true;
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void PlayerPreThink(Edict.Native* pEntity)
        {
            try
            {
                GameClients.PreThink(EntityDictionary.EdictFromNative(pEntity));
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void PlayerPostThink(Edict.Native* pEntity)
        {
            try
            {
                GameClients.PostThink(EntityDictionary.EdictFromNative(pEntity));
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void StartFrame()
        {
            try
            {
                ServerInterface.StartFrame();
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        //Never called by the engine
        internal void ParmsNewLevel()
        {
            try
            {
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void ParmsChangeLevel()
        {
            try
            {
                //TODO: tell game about this
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal IntPtr GetGameDescription()
        {
            try
            {
                var str = ServerInterface.GameDescription ?? string.Empty;

                var utf8String = Encoding.UTF8.GetBytes(str + '\0');

                var unmanagedMemory = Marshal.AllocHGlobal(utf8String.Length);

                Marshal.Copy(utf8String, 0, unmanagedMemory, utf8String.Length);

                return unmanagedMemory;
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void PlayerCustomization(Edict.Native* pEntity, Customization.Native* pCustom)
        {
            try
            {
                GameClients.Customization(EntityDictionary.EdictFromNative(pEntity), new Customization(pCustom));
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        //These 3 methods are never called by the engine
        //Spectators are not handled specially anymore
#pragma warning disable RCS1163 // Unused parameter.
        internal void SpectatorConnect(Edict.Native* pEntity)
#pragma warning restore RCS1163 // Unused parameter.
        {
            try
            {
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

#pragma warning disable RCS1163 // Unused parameter.
        internal void SpectatorDisconnect(Edict.Native* pEntity)
#pragma warning restore RCS1163 // Unused parameter.
        {
            try
            {
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

#pragma warning disable RCS1163 // Unused parameter.
        internal void SpectatorThink(Edict.Native* pEntity)
#pragma warning restore RCS1163 // Unused parameter.
        {
            try
            {
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void Sys_Error(string error_string)
        {
            try
            {
                Log.Message($"Sys_Error: {error_string}");

                ServerInterface.SysError(error_string);
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

#pragma warning disable RCS1163 // Unused parameter.
        internal void PM_Move(PlayerMove.Native* ppmove, QBoolean server)
#pragma warning restore RCS1163 // Unused parameter.
        {
            try
            {
                if (PlayerMoveFunctions == null)
                {
                    //Get the functions separately so we can wrap them
                    PlayerMoveFunctions = Marshal.PtrToStructure<PlayerMoveFunctions>(new IntPtr(&ppmove->firstFunctionOffset));
                    EnginePhysics = new EnginePhysics(PlayerMoveFunctions);
                }

                PlayerPhysics.Move(PlayerMove, EnginePhysics);
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void PM_Init(PlayerMove.Native* ppmove)
        {
            try
            {
                //Create our wrapper and set the server field (not set by engine)
                PlayerMove = new PlayerMove(ppmove, true);

                PlayerPhysics.Init(PlayerMove);
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal byte PM_FindTextureType(string name)
        {
            try
            {
                return PlayerPhysics.FindTextureType(name);
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void SetupVisibility(Edict.Native* pViewEntity, Edict.Native* pClient, out IntPtr pvs, out IntPtr pas)
        {
            try
            {
                Networking.SetupVisibility(pViewEntity != null ? EntityDictionary.EdictFromNative(pViewEntity) : null, EntityDictionary.EdictFromNative(pClient), out pvs, out pas);
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void UpdateClientData(Edict.Native* ent, QBoolean sendweapons, ClientData.Native* cd)
        {
            try
            {
                Networking.UpdateClientData(EntityDictionary.EdictFromNative(ent), sendweapons != QBoolean.False, new ClientData(cd));
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal QBoolean AddToFullPack(EntityState.Native* state, int e, Edict.Native* ent, Edict.Native* host, HostFlags hostflags, QBoolean player, IntPtr pSet)
        {
            try
            {
                return Networking.AddToFullPack(
                    new EntityState(state),
                    e,
                    EntityDictionary.EdictFromNative(ent),
                    EntityDictionary.EdictFromNative(host),
                    hostflags,
                    player != QBoolean.False, pSet
                    )
                    ? QBoolean.True : QBoolean.False;
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                throw;
            }
        }

        internal void CreateBaseline(int player, int eindex, EntityState.Native* baseline, Edict.Native* entity, int playermodelindex, in Vector player_mins, in Vector player_maxs)
        {
            try
            {
                Networking.CreateBaseline(player != 0, eindex, new EntityState(baseline), EntityDictionary.EdictFromNative(entity), playermodelindex, player_mins, player_maxs);
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void RegisterEncoders()
        {
            try
            {
                Networking.RegisterEncoders();
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal QBoolean GetWeaponData(Edict.Native* player, WeaponData.Native* info)
        {
            try
            {
                //Convert the weapon data array to a list
                var list = new List<WeaponData>(InternalConstants.MaxWeapons);

                for (var i = 0; i < InternalConstants.MaxWeapons; ++i)
                {
                    list.Add(new WeaponData(&info[i]));
                }

                return Networking.GetWeaponData(EntityDictionary.EdictFromNative(player), list) ? QBoolean.True : QBoolean.False;
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void CmdStart(Edict.Native* player, UserCmd.Native* cmd, uint random_seed)
        {
            try
            {
                Networking.CmdStart(EntityDictionary.EdictFromNative(player), new UserCmd(cmd), random_seed);
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void CmdEnd(Edict.Native* player)
        {
            try
            {
                Networking.CmdEnd(EntityDictionary.EdictFromNative(player));
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal QBoolean ConnectionlessPacket(NetAddress.Native* net_from, string args, byte* response_buffer, ref int response_buffer_size)
        {
            //TODO:
            try
            {
                return QBoolean.False;
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal QBoolean GetHullBounds(PMHull hullnumber, out Vector mins, out Vector maxs)
        {
            try
            {
                return PlayerPhysics.GetHullBounds(hullnumber, out mins, out maxs) ? QBoolean.True : QBoolean.False;
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void CreateInstancedBaselines()
        {
            try
            {
                Networking.CreateInstancedBaselines();
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal QBoolean InconsistentFile(Edict.Native* player, string filename, byte* disconnect_message)
        {
            try
            {
                var result = Networking.InconsistentFile(EntityDictionary.EdictFromNative(player), filename, out var disconnectMessage);

                InterfaceUtils.CopyStringToUnmanagedBuffer(disconnectMessage ?? string.Empty, disconnect_message, Interfaces.DLLFunctions.InconsistentFileDisconnectMessageLength);

                return result ? QBoolean.True : QBoolean.False;
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal QBoolean AllowLagCompensation()
        {
            try
            {
                return Networking.AllowLagCompensation() ? QBoolean.True : QBoolean.False;
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }
    }
}
