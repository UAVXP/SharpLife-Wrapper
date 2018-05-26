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
using Server.Engine.API.Implementations;
using Server.Game.API;
using System;

namespace Server.Wrapper.API.Implementations
{
    internal sealed unsafe class NewDLLFunctions
    {
        private EntityDictionary EntityDictionary { get; }

        private Wrapper Wrapper { get; }

        private IServerInterface ServerInterface { get; }

        private IEntities ServerEntities { get; }

        public NewDLLFunctions(EntityDictionary entityDictionary, Wrapper wrapper, IServerInterface serverInterface, IEntities serverEntities)
        {
            EntityDictionary = entityDictionary ?? throw new ArgumentNullException(nameof(entityDictionary));
            Wrapper = wrapper ?? throw new ArgumentNullException(nameof(wrapper));
            ServerInterface = serverInterface ?? throw new ArgumentNullException(nameof(serverInterface));
            ServerEntities = serverEntities ?? throw new ArgumentNullException(nameof(serverEntities));
        }

        internal void OnFreeEntPrivateData(Edict.Native* pEnt)
        {
            try
            {
                ServerEntities.OnFreeEntPrivateData(EntityDictionary.EdictFromNative(pEnt));
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void GameShutdown()
        {
            try
            {
                ServerInterface.Shutdown();
                Wrapper.Shutdown();
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal int ShouldCollide(Edict.Native* pentTouched, Edict.Native* pentOther)
        {
            try
            {
                return ServerEntities.ShouldCollide(EntityDictionary.EdictFromNative(pentTouched), EntityDictionary.EdictFromNative(pentOther)) ? 1 : 0;
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void CvarValue(Edict.Native* pEnt, string value)
        {
            try
            {
                ServerInterface.CvarValue(EntityDictionary.EdictFromNative(pEnt), value);
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }

        internal void CvarValue2(Edict.Native* pEnt, int requestID, string cvarName, string value)
        {
            try
            {
                ServerInterface.CvarValue2(EntityDictionary.EdictFromNative(pEnt), requestID, cvarName, value);
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw;
            }
        }
    }
}
