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

using GoldSource.Shared.Entities;

namespace GoldSource.Server.Engine.Game.API
{
    /// <summary>
    /// The main interface from the server to the managed wrapper
    /// </summary>
    public interface IServerInterface
    {
        string GameDescription { get; }

        bool IsActive { get; }

        void Initialize();

        void Shutdown();

        void Activate();

        void Deactivate();

        void StartFrame();

        void SysError(string errorString);

        void CvarValue(Edict pEnt, string value);

        void CvarValue2(Edict pEnt, int requestID, string cvarName, string value);
    }
}
