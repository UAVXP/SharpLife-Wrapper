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
using GoldSource.Server.Engine.API;
using GoldSource.Server.Engine.API.Implementations;
using GoldSource.Server.Engine.Wrapper.API.Interfaces;
using GoldSource.Shared.Engine;
using GoldSource.Shared.Entities;
using System;
using System.Text.RegularExpressions;

namespace GoldSource.Server.Engine.Entities
{
    internal sealed unsafe class EngineEntities : IEngineEntities
    {
        private EngineFuncs EngineFuncs { get; }

        private EngineServer EngineServer { get; }

        private IStringPool StringPool { get; }

        private static readonly Regex BrushModelNameRegex = new Regex(@"^\*\d+$");

        public EngineEntities(EngineFuncs engineFuncs, EngineServer engineServer, IStringPool stringPool)
        {
            EngineFuncs = engineFuncs ?? throw new ArgumentNullException(nameof(engineFuncs));
            EngineServer = engineServer ?? throw new ArgumentNullException(nameof(engineServer));
            StringPool = stringPool ?? throw new ArgumentNullException(nameof(stringPool));
        }

        public int GetIllumination(Edict edict)
        {
            return EngineFuncs.pfnGetEntityIllum(edict.Data);
        }

        public void SetModel(Edict edict, string modelName)
        {
            if (modelName == null)
            {
                throw new ArgumentNullException(nameof(modelName));
            }

            //TODO: brush models should be precached manaully to match the engine
            //TODO: use a default model so it will always succeed
            if (!BrushModelNameRegex.IsMatch(modelName) && EngineServer.ModelCache.Find(modelName) == null)
            {
                throw new NotPrecachedException($"Model \"{modelName}\" not precached");
            }

            //The engine sets the model name using what we've passed in
            var pooled = StringPool.GetPooledString(modelName);

            EngineFuncs.pfnSetModel(edict.Data, pooled);
        }

        public void SetSize(Edict edict, in Vector mins, in Vector maxs)
        {
            EngineFuncs.pfnSetSize(edict.Data, mins, maxs);
        }

        public void SetOrigin(Edict edict, in Vector origin)
        {
            EngineFuncs.pfnSetOrigin(edict.Data, origin);
        }
    }
}
