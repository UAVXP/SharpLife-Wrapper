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
using System;

namespace GoldSource.Client.Engine.Wrapper.API.Implementations
{
    internal sealed unsafe class ClientDLLFunctions
    {
        internal void Initialize()
        {
            //Never called
        }

        internal void HUD_Init()
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal int HUD_VidInit()
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }

            return 1;
        }

        internal int HUD_Redraw(float time, int intermission)
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
            return 1;
        }

        internal int HUD_UpdateClientData(HUDClientData.Native* pcldata, float flTime)
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }

            return 1;
        }

        internal void HUD_Reset()
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal void HUD_ClientMove(PlayerMove.Native* ppmove, QBoolean server)
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal void HUD_ClientMoveInit(PlayerMove.Native* ppmove)
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal byte HUD_TextureType(string name)
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }

            return 0;
        }

        internal void HUD_In_ActivateMouse()
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal void HUD_In_DeactivateMouse()
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal void HUD_In_MouseEvent(int mstate)
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal void HUD_IN_ClearStates()
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal void HUD_In_Accumulate()
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal void HUD_CL_CreateMove(float frametime, UserCmd.Native* cmd, int active)
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal int HUD_CL_IsThirdPerson()
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }

            return 0;
        }

        internal void HUD_CL_GetCameraOffsets(out Vector ofs)
        {
            ofs = new Vector();

            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal KeyButton.Native* HUD_KB_Find(string name)
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }

            return null;
        }

        internal void HUD_CamThink()
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal void HUD_CalcRef(RefParams.Native* pparams)
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal int HUD_AddEntity(int type, ClientEntity.Native* ent, string modelname)
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }

            return 0;
        }

        internal void HUD_CreateEntities()
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal void HUD_DrawNormalTriangles()
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal void HUD_DrawTransparentTriangles()
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal void HUD_StudioEvent(StudioEvent.Native* studioEvent, ClientEntity.Native* entity)
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal void HUD_PostRunCmd(LocalState.Native* from, LocalState.Native* to, UserCmd.Native* cmd, int runfuncs, double time, uint random_seed)
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal void Shutdown()
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal void HUD_TXFerLocalOverrides(EntityState.Native* state, ClientData.Native* client)
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal void HUD_ProcessPlayerState(EntityState.Native* dst, EntityState.Native* src)
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal void HUD_TXFerPredictionData(EntityState.Native* ps, EntityState.Native* pps, ClientData.Native* pcd, ClientData.Native* ppcd, WeaponData.Native* wd, WeaponData.Native* pwd)
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal void HUD_DemoRead(int size, byte* buffer)
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal int HUD_ConnectionlessPacket(NetAddress.Native* net_from, string args, byte* response_buffer, ref int response_buffer_size)
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }

            return 0;
        }

        internal int HUD_GetHullBounds(PMHull hullnumber, out Vector mins, out Vector maxs)
        {
            mins = new Vector();
            maxs = new Vector();
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }

            return 0;
        }

        internal void HUD_Frame(double time)
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal int HUD_KeyEvent(int eventcode, int keynum, string pszCurrentBinding)
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }

            return 0;
        }

        internal void HUD_TempEntUpdate(double frametime, double client_time, double cl_gravity,
            ref TempEntity.Native* ppTempEntFree, ref TempEntity.Native* ppTempEntActive,
            Interfaces.ClientDLLFunctions.Callback_AddVisibleEntity pfnCallback_AddVisibleEntity,
            Interfaces.ClientDLLFunctions.Callback_TempEntPlaySound pfnCallback_TempEntPlaySound)
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal ClientEntity.Native* HUD_GetUserEntity(int index)
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }

            return null;
        }

        internal void HUD_VoiceStatus(int entindex, QBoolean bTalking)
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal void HUD_DirectorMessage(int iSize, void* pbuf)
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal int HUD_StudioInterface(int version, out Interfaces.StudioInterface ppinterface, StudioAPI pstudio)
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }

            ppinterface = null;

            return 0;
        }

        internal void HUD_ChatInputPosition(out int x, out int y)
        {
            x = 0;
            y = 0;

            try
            {

            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        internal int HUD_GetPlayerTeam(int iplayer)
        {
            //Never called by engine
            return 0;
        }
    }
}
