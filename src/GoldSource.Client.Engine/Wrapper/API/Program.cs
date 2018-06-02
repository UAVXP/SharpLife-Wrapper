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

using GoldSource.Client.Engine.Wrapper.API.Interfaces;
using GoldSource.Shared;
using GoldSource.Shared.Wrapper.API;
using Serilog;
using System;
using System.Runtime.InteropServices;

namespace GoldSource.Client.Engine.Wrapper.API
{
    internal static class Program
    {
        //Stored off so the unmanaged reference doesn't access freed memory
        private static ClientManagedAPI ManagedAPI { get; set; }

        internal static Wrapper Wrapper { get; set; }

        internal static void FreeMemory(ClientManagedAPI.MemoryType memoryType, IntPtr memory)
        {
            switch (memoryType)
            {
                case ClientManagedAPI.MemoryType.ManagedAPI:
                    {
                        Marshal.DestroyStructure<ClientManagedAPI>(memory);
                        break;
                    }

                case ClientManagedAPI.MemoryType.ClientAPI:
                    {
                        Marshal.DestroyStructure<ClientDLLFunctions>(memory);
                        break;
                    }

                case ClientManagedAPI.MemoryType.String:
                    {
                        //Free a string that was returned to native code
                        Marshal.FreeHGlobal(memory);
                        break;
                    }
            }
        }

        public static bool Start(out ClientManagedAPI managedAPI)
        {
            //Log nothing for now
            Logger.Instance = new LoggerConfiguration()
                .WriteTo.File(Log.LogFileName)
                .CreateLogger();

            Log.Message("Starting managed wrapper");

            managedAPI = new ClientManagedAPI();

            try
            {
                InterfaceUtils.InitializeFields(ClientManagedAPI.DelegateInstanceNamePrefix, managedAPI, typeof(Program));

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

        internal static bool Initialize(EngineFuncs engineFuncs)
        {
            Log.Message("Engine functions received");

            try
            {
                Wrapper = new Wrapper(engineFuncs);
                return Wrapper.Initialize();
            }
            catch (Exception e)
            {
                Log.Exception(e);
                Wrapper = null;
                return false;
            }
        }

        internal static bool GetClientDllInterface(out ClientDLLFunctions pFunctionTable)
        {
            Log.Message("Request ClientDLLInterface interface");

            if (InterfaceUtils.SetupInterface(ClientManagedAPI.DelegateInstanceNamePrefix, out pFunctionTable, Wrapper.ClientDLLFunctions))
            {
                Wrapper.ClientDLLFunctionsInterface = pFunctionTable;
                return true;
            }

            return false;
        }
    }
}
