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
using GoldSource.Shared.Engine;
using System;

namespace GoldSource.Server.Engine.API
{
    /// <summary>
    /// Global variables shared between the engine and the server
    /// </summary>
    public interface IGlobalVars
    {
        float Time { get; }

        float FrameTime { get; }

        uint ForceRetouch { get; set; }

        string MapName { get; }

        string StartSpot { get; }

        bool IsDeathMatch { get; }

        bool CoOp { get; }

        bool TeamPlay { get; }

        int ServerFlags { get; set; }

        uint FoundSecrets { get; set; }

        Vector ForwardVector { get; set; }

        Vector UpVector { get; set; }

        Vector RightVector { get; set; }

        TraceResult GlobalTrace { get; }

        int MessageEntity { get; }

        int CDAudioTrack { get; set; }

        int MaxClients { get; }

        int MaxEntities { get; }

        IntPtr StringBase { get; }

        IntPtr SaveData { get; }

        Vector LandmarkOffset { get; set; }
    }
}
