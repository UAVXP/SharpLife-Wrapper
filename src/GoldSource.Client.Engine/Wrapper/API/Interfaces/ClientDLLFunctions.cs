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

using GoldSource.Client.Engine.Entities;
using GoldSource.Client.Engine.Input;
using GoldSource.Client.Engine.Networking;
using GoldSource.Client.Engine.StudioRenderer;
using GoldSource.Client.Engine.View;
using GoldSource.Mathlib;
using GoldSource.Shared.Engine;
using GoldSource.Shared.Engine.Networking;
using GoldSource.Shared.Engine.PlayerPhysics;
using GoldSource.Shared.Engine.StudioModel;
using System.Runtime.InteropServices;

namespace GoldSource.Client.Engine.Wrapper.API.Interfaces
{
    [StructLayout(LayoutKind.Sequential)]
    internal sealed unsafe class ClientDLLFunctions
    {
        //Not used, kept for binary compatibility
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void Initialize();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal Initialize pfnInitialize;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_Init();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_Init pfnHUD_Init;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HUD_VidInit();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_VidInit pfnHUD_VidInit;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HUD_Redraw(float time, int intermission);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_Redraw pfnHUD_Redraw;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HUD_UpdateClientData(HUDClientData.Native* pcldata, float flTime);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_UpdateClientData pfnHUD_UpdateClientData;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_Reset();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_Reset pfnHUD_Reset;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_ClientMove(PlayerMove.Native* ppmove, QBoolean server);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_ClientMove pfnHUD_ClientMove;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_ClientMoveInit(PlayerMove.Native* ppmove);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_ClientMoveInit pfnHUD_ClientMoveInit;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate byte HUD_TextureType(string name);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_TextureType pfnHUD_TextureType;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_In_ActivateMouse();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_In_ActivateMouse pfnHUD_In_ActivateMouse;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_In_DeactivateMouse();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_In_DeactivateMouse pfnHUD_In_DeactivateMouse;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_In_MouseEvent(int mstate);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_In_MouseEvent pfnHUD_In_MouseEvent;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_IN_ClearStates();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_IN_ClearStates pfnHUD_IN_ClearStates;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_In_Accumulate();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_In_Accumulate pfnHUD_In_Accumulate;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_CL_CreateMove(float frametime, UserCmd.Native* cmd, int active);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_CL_CreateMove pfnHUD_CL_CreateMove;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HUD_CL_IsThirdPerson();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_CL_IsThirdPerson pfnHUD_CL_IsThirdPerson;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_CL_GetCameraOffsets(out Vector ofs);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_CL_GetCameraOffsets pfnHUD_CL_GetCameraOffsets;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate KeyButton.Native* HUD_KB_Find(string name);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_KB_Find pfnHUD_KB_Find;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_CamThink();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_CamThink pfnHUD_CamThink;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_CalcRef(RefParams.Native* pparams);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_CalcRef pfnHUD_CalcRef;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HUD_AddEntity(int type, ClientEntity.Native* ent, string modelname);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_AddEntity pfnHUD_AddEntity;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_CreateEntities();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_CreateEntities pfnHUD_CreateEntities;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_DrawNormalTriangles();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_DrawNormalTriangles pfnHUD_DrawNormalTriangles;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_DrawTransparentTriangles();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_DrawTransparentTriangles pfnHUD_DrawTransparentTriangles;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_StudioEvent(StudioEvent.Native* studioEvent, ClientEntity.Native* entity);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_StudioEvent pfnHUD_StudioEvent;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_PostRunCmd(LocalState.Native* from, LocalState.Native* to, UserCmd.Native* cmd, int runfuncs, double time, uint random_seed);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_PostRunCmd pfnHUD_PostRunCmd;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void Shutdown();

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal Shutdown pfnShutdown;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_TXFerLocalOverrides(EntityState.Native* state, ClientData.Native* client);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_TXFerLocalOverrides pfnHUD_TXFerLocalOverrides;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_ProcessPlayerState(EntityState.Native* dst, EntityState.Native* src);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_ProcessPlayerState pfnHUD_ProcessPlayerState;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_TXFerPredictionData(EntityState.Native* ps, EntityState.Native* pps, ClientData.Native* pcd, ClientData.Native* ppcd, WeaponData.Native* wd, WeaponData.Native* pwd);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_TXFerPredictionData pfnHUD_TXFerPredictionData;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_DemoRead(int size, byte* buffer);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_DemoRead pfnHUD_DemoRead;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HUD_ConnectionlessPacket(NetAddress.Native* net_from, string args, byte* response_buffer, ref int response_buffer_size);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_ConnectionlessPacket pfnHUD_ConnectionlessPacket;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HUD_GetHullBounds(PMHull hullnumber, out Vector mins, out Vector maxs);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_GetHullBounds pfnHUD_GetHullBounds;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_Frame(double time);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_Frame pfnHUD_Frame;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HUD_KeyEvent(int eventcode, int keynum, string pszCurrentBinding);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_KeyEvent pfnHUD_KeyEvent;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int Callback_AddVisibleEntity(ClientEntity.Native* pEntity);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void Callback_TempEntPlaySound(TempEntity.Native* pTemp, float damp);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_TempEntUpdate(double frametime, double client_time, double cl_gravity, ref TempEntity.Native* ppTempEntFree, ref TempEntity.Native* ppTempEntActive, Callback_AddVisibleEntity pfnCallback_AddVisibleEntity, Callback_TempEntPlaySound pfnCallback_TempEntPlaySound);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_TempEntUpdate pfnHUD_TempEntUpdate;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate ClientEntity.Native* HUD_GetUserEntity(int index);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_GetUserEntity pfnHUD_GetUserEntity;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_VoiceStatus(int entindex, QBoolean bTalking);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_VoiceStatus pfnHUD_VoiceStatus;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_DirectorMessage(int iSize, void* pbuf);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_DirectorMessage pfnHUD_DirectorMessage;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HUD_StudioInterface(int version, out StudioInterface ppinterface, StudioAPI pstudio);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_StudioInterface pfnHUD_StudioInterface;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HUD_ChatInputPosition(out int x, out int y);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_ChatInputPosition pfnHUD_ChatInputPosition;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HUD_GetPlayerTeam(int iplayer);

        [MarshalAs(UnmanagedType.FunctionPtr)]
        internal HUD_GetPlayerTeam pfnHUD_GetPlayerTeam;
    }
}
