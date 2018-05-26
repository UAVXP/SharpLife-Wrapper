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

using Server.Wrapper.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Server.Engine.CVar
{
    internal sealed class CVar : ICVar
    {
        private EngineFuncs EngineFuncs { get; }

        private Dictionary<string, EngineCVar> CVarCache { get; } = new Dictionary<string, EngineCVar>();

        public CVar(EngineFuncs engineFuncs)
        {
            EngineFuncs = engineFuncs ?? throw new ArgumentNullException(nameof(engineFuncs));
        }

        public string GetString(string name)
        {
            var value = EngineFuncs.pfnCVarGetString(name);

            return Marshal.PtrToStringUTF8(value);
        }

        public float GetFloat(string name)
        {
            return EngineFuncs.pfnCVarGetFloat(name);
        }

        public EngineCVar GetCVar(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (CVarCache.TryGetValue(name, out var cvar))
            {
                return cvar;
            }

            var engineCVar = EngineFuncs.pfnCVarGetPointer(name);

            if (engineCVar == IntPtr.Zero)
            {
                return null;
            }

            cvar = new EngineCVar(engineCVar);

            CVarCache.Add(name, cvar);

            return cvar;
        }

        public unsafe void Register(ConVar variable)
        {
            if (variable == null)
            {
                throw new ArgumentNullException(nameof(variable));
            }

            if (EngineFuncs.pfnCVarGetPointer(variable.Name) != IntPtr.Zero)
            {
                throw new ArgumentException($"Cannot register variable \"{variable.Name}\", already registered", nameof(variable));
            }

            var stringValueToFree = variable.EngineCVar.GetNativeString();

            EngineFuncs.pfnCVarRegister(new IntPtr(variable.EngineCVar.Data));

            Marshal.FreeHGlobal(stringValueToFree);

            CVarCache.Add(variable.Name, variable.EngineCVar);

            variable.EngineFuncs = EngineFuncs;
        }

        public void ServerCommand(string command)
        {
            //TODO: could verify presence of correct end characters
            EngineFuncs.pfnServerCommand(command);
        }

        public unsafe void ClientCommand(Edict edict, string command)
        {
            //TODO: could verify presence of correct end characters
            EngineFuncs.pfnClientCommand(edict.Data, command);
        }
    }
}
