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

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Server.Engine.API.Implementations
{
    /// <summary>
    /// A pool mapping managed strings to unmanaged UTF8 strings
    /// </summary>
    internal sealed class StringPool : IStringPool
    {
        internal sealed class Entry : IDisposable
        {
            internal string String { get; }

            internal IntPtr Unmanaged { get; }

            //Strings set by the engine will not be owned by us, so don't free them
            internal bool Owned { get; }

            internal Entry(string str, IntPtr address)
            {
                String = str ?? throw new ArgumentNullException(nameof(str));

                Unmanaged = address;

                Owned = false;
            }

            internal Entry(string str)
            {
                String = str ?? throw new ArgumentNullException(nameof(str));

                //Allocate the unmanaged memory
                //Ensure that there is a null terminator present, since GetBytes doesn't add it
                var utf8String = Encoding.UTF8.GetBytes(str + '\0');

                Unmanaged = Marshal.AllocHGlobal(utf8String.Length);

                Marshal.Copy(utf8String, 0, Unmanaged, utf8String.Length);

                Owned = true;
            }

            public void Dispose()
            {
                if (Owned)
                {
                    Marshal.FreeHGlobal(Unmanaged);
                }
            }
        }

        private IGlobalVars Globals { get; }

        //StringBase is not set until a map starts loading, so we can't just rely on it bein valid on startup
        private IntPtr BaseAddress => Globals.StringBase;

        private Dictionary<string, Entry> Pool { get; } = new Dictionary<string, Entry>();

        //For looking up EngineString strings
        private Dictionary<IntPtr, Entry> ReverseLookup { get; } = new Dictionary<IntPtr, Entry>();

        /// <summary>
        /// Creates the string pool
        /// </summary>
        /// <param name="globals">Used to get the base address for string offsets</param>
        internal StringPool(IGlobalVars globals)
        {
            Globals = globals ?? throw new ArgumentNullException(nameof(globals));
        }

        ~StringPool()
        {
            Clear();
        }

        private Entry Insert(string str, IntPtr? address = null)
        {
            var entry = address != null ? new Entry(str, address.Value) : new Entry(str);

            Pool.Add(str, entry);

            ReverseLookup.Add(entry.Unmanaged, entry);

            return entry;
        }

        public IntPtr GetPooledString(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (!Pool.TryGetValue(str, out var entry))
            {
                entry = Insert(str);
            }

            return entry.Unmanaged;
        }

        public void Clear()
        {
            ReverseLookup.Clear();

            foreach (var entry in Pool)
            {
                entry.Value.Dispose();
            }

            Pool.Clear();
        }

        public EngineString GetEngineString(string str)
        {
            var address = GetPooledString(str);

            return new EngineString(address.ToInt32() - BaseAddress.ToInt32());
        }

        public unsafe string GetString(EngineString str)
        {
            var address = new IntPtr(BaseAddress.ToInt32() + str.Offset);

            if (!ReverseLookup.TryGetValue(address, out var entry))
            {
                //This is where things get hairy:
                //The engine will set some EngineString fields itself, so we won't know about them
                //We'll have to assume that the input here is a valid string that was set by the engine
                //If this is an invalid string, it'll either allocate a garbage string or crash
                //EngineString's offset constructor is marked internal so unless a field is being set incorrectly in native code, this should never be a problem
                var result = Marshal.PtrToStringUTF8(address);

                //There might already be a string in the pool, so just add a reverse mapping
                if (Pool.TryGetValue(result, out entry))
                {
                    ReverseLookup.Add(address, entry);
                }
                else
                {
                    entry = Insert(result, address);
                }
            }

            return entry.String;
        }
    }
}
