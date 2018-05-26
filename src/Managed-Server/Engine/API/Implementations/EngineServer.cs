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

using Server.Engine.Networking;
using Server.Engine.Networking.InfoBuffers;
using Server.Engine.StudioModel;
using Server.Wrapper.API;
using Server.Wrapper.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Server.Engine.API.Implementations
{
    internal class EngineServer : IEngineServer
    {
        public string GameDirectory { get; }

        private EngineFuncs EngineFuncs { get; }

        private IStringPool StringPool { get; }

        private EntityDictionary EntityDictionary { get; }

        private IGlobalVars Globals { get; }

        private IFileSystem FileSystem { get; }

        //These caches are currently used to avoid triggering host and sys errors in the engine
        public ResourceCache ModelCache { get; private set; }

        public ResourceCache SoundCache { get; private set; }

        public ResourceCache GenericCache { get; private set; }

        private Dictionary<string, object> Models { get; set; }

        private bool PrecachingAllowed { get; set; }

        public EngineServer(EngineFuncs engineFuncs, IStringPool stringPool, EntityDictionary entityDictionary, IGlobalVars globals, IFileSystem fileSystem)
        {
            EngineFuncs = engineFuncs ?? throw new ArgumentNullException(nameof(engineFuncs));
            StringPool = stringPool ?? throw new ArgumentNullException(nameof(stringPool));
            EntityDictionary = entityDictionary ?? throw new ArgumentNullException(nameof(entityDictionary));
            Globals = globals ?? throw new ArgumentNullException(nameof(globals));
            FileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));

            Log.Message("Getting game directory");

            GameDirectory = EngineFuncs.GetGameDirHelper();

            Log.Message($"Game directory is \"{GameDirectory}\"");
        }

        internal void MapStartedLoading()
        {
            ModelCache = new ResourceCache();
            SoundCache = new ResourceCache();
            GenericCache = new ResourceCache();

            //Clear cache
            Models = new Dictionary<string, object>();

            PrecachingAllowed = true;
        }

        internal void MapFinishedLoading()
        {
            PrecachingAllowed = false;
        }

        //TODO: need a suitable default value (error.mdl)
        public int PrecacheModel(string name)
        {
            var (added, resource) = ModelCache.TryAdd(name, PrecachingAllowed, () => EngineFuncs.pfnPrecacheModel(StringPool.GetPooledString(name)));

            return resource?.Index ?? 0;
        }

        public int PrecacheSound(string name)
        {
            var (added, resource) = SoundCache.TryAdd(name, PrecachingAllowed, () => EngineFuncs.pfnPrecacheSound(StringPool.GetPooledString(name)));

            return resource?.Index ?? 0;
        }

        public int PrecacheGeneric(string name)
        {
            var (added, resource) = GenericCache.TryAdd(name, PrecachingAllowed, () => EngineFuncs.pfnPrecacheGeneric(StringPool.GetPooledString(name)));

            return resource?.Index ?? 0;
        }

        public unsafe object GetModel(Edict edict)
        {
            if (edict == null)
            {
                return null;
            }

            var modelName = edict.Vars.ModelName;

            if (!string.IsNullOrEmpty(modelName))
            {
                return null;
            }

            if (Models.TryGetValue(modelName, out var model))
            {
                return model;
            }

            var nativeData = EngineFuncs.pfnGetModelPtr(edict.Data);

            if (modelName.StartsWith('*'))
            {
                //BSP model
            }
            else if (modelName.EndsWith(".mdl"))
            {
                //Studio model
                model = new StudioHeader((StudioHeader.Native*)nativeData.ToPointer());
            }
            else if(modelName.EndsWith(".spr"))
            {
                //Sprite
            }

            if (model != null)
            {
                Models.Add(modelName, model);
            }

            return model;
        }

        public unsafe void GetBonePosition(Edict edict, int bone, out Vector origin, out Vector angles)
        {
            EngineFuncs.pfnGetBonePosition(edict.Data, bone, out origin, out angles);

            //TODO: engine never sets angles
            angles = new Vector();
        }

        public unsafe void GetAttachment(Edict edict, int attachment, out Vector origin, out Vector angles)
        {
            EngineFuncs.pfnGetAttachment(edict.Data, attachment, out origin, out angles);

            //TODO: engine never sets angles
            angles = new Vector();
        }

        public unsafe int GetPlayerUserId(Edict player)
        {
            return EngineFuncs.pfnGetPlayerUserId(player.Data);
        }

        public unsafe string GetPlayerAuthId(Edict player)
        {
            return Marshal.PtrToStringUTF8(EngineFuncs.pfnGetPlayerAuthId(player.Data));
        }

        public bool GetClientListening(int receiver, int sender)
        {
            return EngineFuncs.pfnVoice_GetClientListening(receiver, sender) != QBoolean.False;
        }

        public void SetClientListening(int receiver, int sender, bool canHear)
        {
            EngineFuncs.pfnVoice_SetClientListening(receiver, sender, canHear ? QBoolean.True : QBoolean.False);
        }

        public unsafe IInfoBuffer GetLocalInfoBuffer()
        {
            return new ServerInfoBuffer(EngineFuncs, EngineFuncs.pfnGetInfoKeyBuffer(null));
        }

        public unsafe IInfoBuffer GetServerInfoBuffer()
        {
            return new ServerInfoBuffer(EngineFuncs, EngineFuncs.pfnGetInfoKeyBuffer(EngineFuncs.pfnPEntityOfEntOffset(0)));
        }

        public unsafe IInfoBuffer GetClientInfoBuffer(Edict pClient)
        {
            if (!APIUtils.IsClient(pClient, EntityDictionary, Globals))
            {
                throw new ArgumentException("Edict must be a client", nameof(pClient));
            }

            return new ClientInfoBuffer(EngineFuncs, EngineFuncs.pfnGetInfoKeyBuffer(pClient.Data), EntityDictionary.EntityIndex(pClient));
        }

        public unsafe IInfoBuffer GetClientPhysicsInfoBuffer(Edict pClient)
        {
            if (!APIUtils.IsClient(pClient, EntityDictionary, Globals))
            {
                throw new ArgumentException("Edict must be a client", nameof(pClient));
            }

            return new ClientPhysicsInfoBuffer(EngineFuncs, pClient.Data, EntityDictionary.EntityIndex(pClient));
        }

        public unsafe IntPtr GetUnmanagedClientPhysicsInfoBuffer(Edict pClient)
        {
            if (!APIUtils.IsClient(pClient, EntityDictionary, Globals))
            {
                throw new ArgumentException("Edict must be a client", nameof(pClient));
            }

            return EngineFuncs.pfnGetPhysicsInfoString(pClient.Data);
        }

        public unsafe bool CheckVisibility(Edict entity, IntPtr pvs)
        {
            return EngineFuncs.pfnCheckVisibility(entity.Data, pvs) != 0;
        }

        public IntPtr SetFatPVS(in Vector org)
        {
            return EngineFuncs.pfnSetFatPVS(org);
        }

        public IntPtr SetFatPAS(in Vector org)
        {
            return EngineFuncs.pfnSetFatPAS(org);
        }

        public unsafe void ClientPrint(Edict edict, PrintType type, string message)
        {
            EngineFuncs.pfnClientPrintf(edict.Data, type, message);
        }

        public void ServerPrint(string message)
        {
            EngineFuncs.pfnServerPrint(message);
        }

        public void Alert(AlertType atype, string message)
        {
            EngineFuncs.pfnAlertMessage(atype, "%s", message);
        }

        public Contents PointContents(in Vector org)
        {
            return EngineFuncs.pfnPointContents(org);
        }

        public bool IsMapValid(string mapName)
        {
            //return EngineFuncs.pfnIsMapValid(mapName) != 0;

            //This is all that function does, and this is much more efficient
            return !string.IsNullOrEmpty(mapName) && FileSystem.Exists($"maps/{mapName}.bsp");
        }

        public unsafe Edict FindClientInPVS(Edict pvsEntity)
        {
            if (pvsEntity == null)
            {
                throw new ArgumentNullException(nameof(pvsEntity));
            }

            return EntityDictionary.EdictFromNative(EngineFuncs.pfnFindClientInPVS(pvsEntity.Data));
        }

        public unsafe Edict EntitiesInPVS(Edict pvsEntity)
        {
            if (pvsEntity == null)
            {
                throw new ArgumentNullException(nameof(pvsEntity));
            }

            return EntityDictionary.EdictFromNative(EngineFuncs.pfnEntitiesInPVS(pvsEntity.Data));
        }

        public void ParticleEffect(in Vector origin, in Vector direction, float color, float count)
        {
            EngineFuncs.pfnParticleEffect(origin, direction, color, count);
        }

        public unsafe void MoveToOrigin(Edict entity, in Vector goal, float dist, MoveToOrigin moveType)
        {
            EngineFuncs.pfnMoveToOrigin(entity.Data, goal, dist, moveType);
        }

        public unsafe Vector GetAimVector(Edict edict, float speed)
        {
            //TODO: implement in game code
            EngineFuncs.pfnGetAimVector(edict.Data, speed, out var result);
            return result;
        }

        public void ChangeLevel(string mapName, string landmarkName)
        {
            if (mapName == null)
            {
                throw new ArgumentNullException(nameof(mapName));
            }

            //The landmark should always be a valid string, even if empty
            //Otherwise we don't match original behavior and it might break in unexpected ways
            EngineFuncs.pfnChangeLevel(mapName, landmarkName ?? string.Empty);
        }
    }
}
