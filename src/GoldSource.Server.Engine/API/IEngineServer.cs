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
using GoldSource.Shared.Entities;
using System;

namespace GoldSource.Server.Engine.API
{
    /// <summary>
    /// Main interface to the engine for the server
    /// </summary>
    public interface IEngineServer
    {
        /// <summary>
        /// The game directory that the current mod is loaded in
        /// </summary>
        string GameDirectory { get; }

        /// <summary>
        /// Precaches a model or sprite
        /// </summary>
        /// <param name="name">Name of the model, starting in the game/mod directory, including the extension</param>
        /// <returns>Model index</returns>
        int PrecacheModel(string name);

        /// <summary>
        /// Precaches a sound
        /// </summary>
        /// <param name="name">Name of the sound, starting in the sound directory, including the extension</param>
        /// <returns>Sound index</returns>
        int PrecacheSound(string name);

        /// <summary>
        /// Precaches a generic resource to be downloaded to clients
        /// </summary>
        /// <param name="name">Name of the resource, starting in the game/mod directory</param>
        /// <returns>Resource index</returns>
        int PrecacheGeneric(string name);

        /// <summary>
        /// Gets the model of the given edict
        /// </summary>
        /// <param name="edict"></param>
        /// <returns></returns>
        object GetModel(Edict edict);

        /// <summary>
        /// Gets the position of a bone from the given entity
        /// </summary>
        /// <param name="edict"></param>
        /// <param name="bone"></param>
        /// <param name="origin"></param>
        /// <param name="angles"></param>
        void GetBonePosition(Edict edict, int bone, out Vector origin, out Vector angles);

        /// <summary>
        /// Gets the position of an attachment from the given entity
        /// </summary>
        /// <param name="edict"></param>
        /// <param name="attachment"></param>
        /// <param name="origin"></param>
        /// <param name="angles"></param>
        void GetAttachment(Edict edict, int attachment, out Vector origin, out Vector angles);

        /// <summary>
        /// Returns the server assigned userid for this player
        /// Useful for logging frags, etc
        /// Returns -1 if the edict couldn't be found in the list of clients
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        int GetPlayerUserId(Edict player);

        /// <summary>
        /// Gets the Steam2 Id for this player
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        string GetPlayerAuthId(Edict player);

        /// <summary>
        /// For voice communications, get which clients hear eachother.
        /// Note: these functions take player entity indices (starting at 1).
        /// </summary>
        /// <param name="receiver"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        bool GetClientListening(int receiver, int sender);

        /// <summary>
        /// For voice communications, set which clients hear eachother.
        /// Note: these functions take player entity indices (starting at 1).
        /// </summary>
        /// <param name="receiver"></param>
        /// <param name="sender"></param>
        /// <param name="canHear"></param>
        void SetClientListening(int receiver, int sender, bool canHear);

        /// <summary>
        /// Gets the local info buffer
        /// Contains values set with the "localinfo" console command
        /// </summary>
        /// <returns></returns>
        IInfoBuffer GetLocalInfoBuffer();

        /// <summary>
        /// Gets the server info buffer
        /// Contains values set with the "serverinfo" console command
        /// CVars marked as Server will be added to this buffer
        /// </summary>
        /// <returns></returns>
        IInfoBuffer GetServerInfoBuffer();

        /// <summary>
        /// Gets the info buffer for the given client
        /// </summary>
        /// <param name="pClient"></param>
        /// <returns></returns>
        IInfoBuffer GetClientInfoBuffer(Edict pClient);

        /// <summary>
        /// Gets the physics info buffer for the given client
        /// </summary>
        /// <param name="pClient"></param>
        /// <returns></returns>
        IInfoBuffer GetClientPhysicsInfoBuffer(Edict pClient);

        /// <summary>
        /// Gets the unmanaged client physics info buffer
        /// Should only be used for cases where it is more efficient to use this
        /// </summary>
        /// <param name="pClient"></param>
        /// <returns></returns>
        IntPtr GetUnmanagedClientPhysicsInfoBuffer(Edict pClient);

        /// <summary>
        /// Check if the given entity can see the given visible set
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pvs"></param>
        /// <returns></returns>
        bool CheckVisibility(Edict entity, IntPtr pvs);

        IntPtr SetFatPVS(in Vector org);

        IntPtr SetFatPAS(in Vector org);

        void ClientPrint(Edict edict, PrintType type, string message);

        void ServerPrint(string message);

        void Alert(AlertType atype, string message);

        Contents PointContents(in Vector org);

        bool IsMapValid(string mapName);

        Edict FindClientInPVS(Edict pvsEntity);

        /// <summary>
        /// Builds a temporary list of all entities visible from the given entity's PVS
        /// The result is stored in Edict.Vars.Chain
        /// </summary>
        /// <param name="pvsEntity"></param>
        /// <returns></returns>
        Edict EntitiesInPVS(Edict pvsEntity);

        void ParticleEffect(in Vector origin, in Vector direction, float color, float count);

        void MoveToOrigin(Edict entity, in Vector goal, float dist, MoveToOrigin moveType);

        Vector GetAimVector(Edict edict, float speed);

        void ChangeLevel(string mapName, string landmarkName = null);
    }
}
