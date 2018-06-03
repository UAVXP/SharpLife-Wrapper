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

using GoldSource.Server.Engine.API.Implementations;
using GoldSource.Server.Engine.Wrapper.API.Interfaces;
using GoldSource.Shared;
using GoldSource.Shared.Wrapper.API;
using Serilog;
using System;
using System.Runtime.InteropServices;

namespace GoldSource.Server.Engine.Wrapper.API
{
    internal static class Program
    {
        //Stored off so the unmanaged reference doesn't access freed memory
        private static ServerManagedAPI ManagedAPI { get; set; }

        internal static Wrapper Wrapper { get; private set; }

        internal static void FreeMemory(ServerManagedAPI.MemoryType memoryType, IntPtr memory)
        {
            switch (memoryType)
            {
                case ServerManagedAPI.MemoryType.ManagedAPI:
                    {
                        Marshal.DestroyStructure<ServerManagedAPI>(memory);
                        break;
                    }

                case ServerManagedAPI.MemoryType.EntityAPI:
                    {
                        Marshal.DestroyStructure<DLLFunctions>(memory);
                        break;
                    }

                case ServerManagedAPI.MemoryType.NewEntityAPI:
                    {
                        Marshal.DestroyStructure<NewDLLFunctions>(memory);
                        break;
                    }

                case ServerManagedAPI.MemoryType.String:
                    {
                        //Free a string that was returned to native code
                        Marshal.FreeHGlobal(memory);
                        break;
                    }
            }
        }

        internal static bool Start(out ServerManagedAPI managedAPI)
        {
            //Log nothing for now
            //Client may have already initialized the logger
            if (Logger.Instance == null)
            {
                Logger.Instance = new LoggerConfiguration()
                    .WriteTo.File(Log.LogFileName)
                    .CreateLogger();
            }

            Log.Message("Starting managed wrapper");

            managedAPI = new ServerManagedAPI();

            try
            {
                InterfaceUtils.InitializeFields(ServerManagedAPI.DelegateInstanceNamePrefix, managedAPI, typeof(Program));

                Log.Message("Managed wrapper started");

                ManagedAPI = managedAPI;

                return true;
            }
            catch (Exception e)
            {
                Log.Message(e.Message);
                managedAPI = null;
                return false;
            }
        }

        internal static unsafe bool GiveFnptrsToDll(EngineFuncs engineFuncs, GlobalVars.Internal* pGlobals)
        {
            Log.Message("Engine functions received");

            try
            {
                Wrapper = new Wrapper(engineFuncs, pGlobals);
                return Wrapper.Initialize();
            }
            catch (Exception e)
            {
                Log.Exception(e);
                Wrapper = null;
                return false;
            }
        }

        internal static bool GetEntityAPI(out DLLFunctions pFunctionTable)
        {
            Log.Message("Request DLLFunctions interface");

            if (InterfaceUtils.SetupInterface(ServerManagedAPI.DelegateInstanceNamePrefix, out pFunctionTable, Wrapper.DLLFunctions))
            {
                Wrapper.DLLFunctionsInterface = pFunctionTable;
                return true;
            }

            return false;
        }

        internal static bool GetNewDLLFunctions(out NewDLLFunctions pFunctionTable)
        {
            Log.Message("Request NewDLLFunctions interface");

            if (InterfaceUtils.SetupInterface(ServerManagedAPI.DelegateInstanceNamePrefix, out pFunctionTable, Wrapper.NewDLLFunctions))
            {
                Wrapper.NewDLLFunctionsInterface = pFunctionTable;
                return true;
            }

            return false;
        }

        internal static bool GetEngineOverrides(out EngineOverrides pFunctionTable)
        {
            Log.Message("Request Engine Overrides interface");

            if (InterfaceUtils.SetupInterface(ServerManagedAPI.DelegateInstanceNamePrefix, out pFunctionTable, Wrapper.EngineOverrides))
            {
                Wrapper.EngineOverridesInterface = pFunctionTable;
                return true;
            }

            return false;
        }
    }
}
