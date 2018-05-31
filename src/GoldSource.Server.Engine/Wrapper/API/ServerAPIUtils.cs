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

using GoldSource.Server.Engine.API;
using GoldSource.Server.Engine.Wrapper.API.Interfaces;
using GoldSource.Shared.Engine;
using GoldSource.Shared.Entities;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GoldSource.Server.Engine.Wrapper.API
{
    internal static class ServerAPIUtils
    {
        /// <summary>
        /// Converts the current command arguments to a list
        /// </summary>
        /// <param name="engineFuncs"></param>
        /// <returns></returns>
        internal static List<string> ArgsAsList(EngineFuncs engineFuncs)
        {
            var count = engineFuncs.pfnCmd_Argc();

            var list = new List<string>(count);

            for (var i = 0; i < count; ++i)
            {
                var address = engineFuncs.pfnCmd_Argv(i);

                var arg = Marshal.PtrToStringUTF8(address);

                list.Add(arg);
            }

            return list;
        }

        internal static bool IsClient(Edict edict, IEntityDictionary entityDictionary, IGlobalVars globalVars)
        {
            if (edict == null)
            {
                return false;
            }

            var index = entityDictionary.EntityIndex(edict);

            return 0 < index && index <= globalVars.MaxClients;
        }
    }
}
