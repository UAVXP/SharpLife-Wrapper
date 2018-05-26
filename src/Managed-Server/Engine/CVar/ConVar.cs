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

namespace Server.Engine.CVar
{
    public unsafe class ConVar
    {
        internal EngineCVar EngineCVar { get; }

        //If you get a null pointer exception here, you haven't registered the variable
        internal EngineFuncs EngineFuncs { get; set; }

        public string Name => EngineCVar.Name;

        public string String
        {
            get => EngineCVar.String;

            set
            {
                EngineFuncs.pfnCvar_DirectSet(new IntPtr(EngineCVar.Data), value);
            }
        }

        public float Float
        {
            get => EngineCVar.Float;

            set
            {
                EngineFuncs.pfnCvar_DirectSet(new IntPtr(EngineCVar.Data), value.ToString());
            }
        }

        public CVarFlags Flags => EngineCVar.Flags;

        public ConVar(string name, string value, CVarFlags flags = CVarFlags.None)
        {
            EngineCVar = new EngineCVar(name, value, flags);
        }
    }
}
