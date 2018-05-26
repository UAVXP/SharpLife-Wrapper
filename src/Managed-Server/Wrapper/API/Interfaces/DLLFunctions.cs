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
using Server.Engine.Networking;
using Server.Engine.Persistence;
using Server.Engine.PlayerPhysics;
using System;
using System.Runtime.InteropServices;

namespace Server.Wrapper.API.Interfaces
{
    [StructLayout(LayoutKind.Sequential)]
    internal sealed unsafe class DLLFunctions
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void GameInit();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal GameInit pfnGameInit;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int Spawn(Edict.Native* pent);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal Spawn pfnSpawn;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void Think(Edict.Native* pent);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal Think pfnThink;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void Use(Edict.Native* pentUsed, Edict.Native* pentOther);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal Use pfnUse;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void Touch(Edict.Native* pentTouched, Edict.Native* pentOther);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal Touch pfnTouch;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void Blocked(Edict.Native* pentBlocked, Edict.Native* pentOther);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal Blocked pfnBlocked;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void KeyValue(Edict.Native* pentKeyvalue, KeyValueData.Native* pkvd);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal KeyValue pfnKeyValue;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void Save(Edict.Native* pent, SaveRestoreData.Native* pSaveData);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal Save pfnSave;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int Restore(Edict.Native* pent, SaveRestoreData.Native* pSaveData, int globalEntity);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal Restore pfnRestore;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void SetAbsBox(Edict.Native* pent);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal SetAbsBox pfnSetAbsBox;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void SaveWriteFields(SaveRestoreData.Native* pSaveData, [MarshalAs(UnmanagedType.LPStr)]string pname, void* pBaseData, TypeDescription.Native* pFields, int fieldCount);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal SaveWriteFields pfnSaveWriteFields;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void SaveReadFields(SaveRestoreData.Native* pSaveData, [MarshalAs(UnmanagedType.LPStr)]string pname, void* pBaseData, TypeDescription.Native* pFields, int fieldCount);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal SaveReadFields pfnSaveReadFields;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void SaveGlobalState(SaveRestoreData.Native* pSaveData);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal SaveGlobalState pfnSaveGlobalState;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void RestoreGlobalState(SaveRestoreData.Native* pSaveData);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal RestoreGlobalState pfnRestoreGlobalState;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ResetGlobalState();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal ResetGlobalState pfnResetGlobalState;

        internal const int ClientConnectRejectReasonLength = 128;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate QBoolean ClientConnect(Edict.Native* pEntity, [MarshalAs(UnmanagedType.LPStr)] string name, [MarshalAs(UnmanagedType.LPStr)]string address, byte* szRejectReason);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal ClientConnect pfnClientConnect;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ClientDisconnect(Edict.Native* pEntity);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal ClientDisconnect pfnClientDisconnect;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ClientKill(Edict.Native* pEntity);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal ClientKill pfnClientKill;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ClientPutInServer(Edict.Native* pEntity);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal ClientPutInServer pfnClientPutInServer;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ClientCommand(Edict.Native* pEntity);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal ClientCommand pfnClientCommand;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ClientUserInfoChanged(Edict.Native* pEntity, IntPtr infoBuffer);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal ClientUserInfoChanged pfnClientUserInfoChanged;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ServerActivate(Edict.Native* pEdictList, int edictCount, int clientMax);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal ServerActivate pfnServerActivate;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ServerDeactivate();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal ServerDeactivate pfnServerDeactivate;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void PlayerPreThink(Edict.Native* pEntity);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal PlayerPreThink pfnPlayerPreThink;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void PlayerPostThink(Edict.Native* pEntity);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal PlayerPostThink pfnPlayerPostThink;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void StartFrame();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal StartFrame pfnStartFrame;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ParmsNewLevel();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal ParmsNewLevel pfnParmsNewLevel;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ParmsChangeLevel();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal ParmsChangeLevel pfnParmsChangeLevel;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr GetGameDescription();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal GetGameDescription pfnGetGameDescription;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void PlayerCustomization(Edict.Native* pEntity, Customization.Native* pCustom);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal PlayerCustomization pfnPlayerCustomization;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void SpectatorConnect(Edict.Native* pEntity);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal SpectatorConnect pfnSpectatorConnect;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void SpectatorDisconnect(Edict.Native* pEntity);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal SpectatorDisconnect pfnSpectatorDisconnect;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void SpectatorThink(Edict.Native* pEntity);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal SpectatorThink pfnSpectatorThink;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void Sys_Error([MarshalAs(UnmanagedType.LPStr)]string error_string);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal Sys_Error pfnSys_Error;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void PM_Move(PlayerMove.Native* ppmove, QBoolean server);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal PM_Move pfnPM_Move;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void PM_Init(PlayerMove.Native* ppmove);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal PM_Init pfnPM_Init;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate byte PM_FindTextureType([MarshalAs(UnmanagedType.LPStr)]string name);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal PM_FindTextureType pfnPM_FindTextureType;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void SetupVisibility(Edict.Native* pViewEntity, Edict.Native* pClient, out IntPtr pvs, out IntPtr pas);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal SetupVisibility pfnSetupVisibility;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void UpdateClientData(Edict.Native* ent, QBoolean sendweapons, ClientData.Native* cd);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal UpdateClientData pfnUpdateClientData;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate QBoolean AddToFullPack(EntityState.Native* state, int e, Edict.Native* ent, Edict.Native* host, HostFlags hostflags, QBoolean player, IntPtr pSet);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal AddToFullPack pfnAddToFullPack;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void CreateBaseline(int player, int eindex, EntityState.Native* baseline, Edict.Native* entity, int playermodelindex, in Vector player_mins, in Vector player_maxs);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal CreateBaseline pfnCreateBaseline;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void RegisterEncoders();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal RegisterEncoders pfnRegisterEncoders;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate QBoolean GetWeaponData(Edict.Native* player, WeaponData.Native* info);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal GetWeaponData pfnGetWeaponData;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void CmdStart(Edict.Native* player, UserCmd.Native* cmd, uint random_seed);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal CmdStart pfnCmdStart;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void CmdEnd(Edict.Native* player);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal CmdEnd pfnCmdEnd;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate QBoolean ConnectionlessPacket(NetAddress.Native* net_from, [MarshalAs(UnmanagedType.LPStr)]string args, byte* response_buffer, ref int response_buffer_size);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal ConnectionlessPacket pfnConnectionlessPacket;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate QBoolean GetHullBounds(PMHull hullnumber, out Vector mins, out Vector maxs);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal GetHullBounds pfnGetHullBounds;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void CreateInstancedBaselines();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal CreateInstancedBaselines pfnCreateInstancedBaselines;

        internal const int InconsistentFileDisconnectMessageLength = 256;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate QBoolean InconsistentFile(Edict.Native* player, [MarshalAs(UnmanagedType.LPStr)]string filename, byte* disconnect_message);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal InconsistentFile pfnInconsistentFile;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate QBoolean AllowLagCompensation();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal AllowLagCompensation pfnAllowLagCompensation;
    }
}
