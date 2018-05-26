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

using Server.Engine.API;
using Server.Engine.API.Implementations;
using Server.Game.API;
using Server.Wrapper.API.Interfaces;
using System;

namespace Server.Wrapper.API.Implementations
{
    internal sealed unsafe class EngineOverrides
    {
        private EngineFuncs EngineFuncs { get; }

        private IGlobalVars Globals { get; }

        private EntityDictionary EntityDictionary { get; }

        private EngineServer EngineServer { get; }

        private IEntities Entities { get; }

        public EngineOverrides(
            EngineFuncs engineFuncs,
            IGlobalVars globals,
            EntityDictionary entityDictionary,
            EngineServer engineServer,
            IEntities entities
            )
        {
            EngineFuncs = engineFuncs ?? throw new ArgumentNullException(nameof(engineFuncs));
            Globals = globals ?? throw new ArgumentNullException(nameof(globals));
            EntityDictionary = entityDictionary ?? throw new ArgumentNullException(nameof(entityDictionary));
            EngineServer = engineServer ?? throw new ArgumentNullException(nameof(engineServer));
            Entities = entities ?? throw new ArgumentNullException(nameof(entities));
        }

        internal void LoadEntities(string data)
        {
            try
            {
                //Refresh the cache
                EngineServer.MapStartedLoading();

                var pEdictList = EngineFuncs.pfnPEntityOfEntOffset(0);

                Log.Message($"Initializing entity dictionary with 0x{(uint)pEdictList:X} as edict list address");

                EntityDictionary.Initialize(pEdictList, Globals.MaxEntities);

                Log.Message("Finished initializing entity dictionary");

                Entities.LoadEntities(data);
            }
            catch(Exception e)
            {
                Log.Exception(e);
                throw;
            }
            finally
            {
                EngineServer.MapFinishedLoading();
            }
        }
    }
}
