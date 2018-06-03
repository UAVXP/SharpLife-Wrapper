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

using Serilog;
using System;
using System.Collections.Generic;
using System.IO;

namespace GoldSource.Registry
{
    internal sealed class UnixRegistry : IRegistry
    {
        internal sealed class KV
        {
            public string Key { get; set; }

            public string Value { get; set; }
        }

        private string Name { get; }

        private ILogger Logger { get; }

        private List<KV> KeyValues { get; } = new List<KV>();

        internal UnixRegistry(string name, ILogger logger)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;

            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private void LoadKeyValuesFromDisk()
        {
            if (KeyValues.Count == 0)
            {
                try
                {
                    using (var reader = File.OpenText(Name))
                    {
                        foreach (var kv in KeyValues)
                        {
                            var line = reader.ReadLine();

                            var separator = line.IndexOf('=');

                            if (separator != -1)
                            {
                                var key = line.Substring(0, separator);
                                var value = line.Substring(separator + 1);

                                KeyValues.Add(new KV { Key = key, Value = value });
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e, "Exception reading registry from disk");
                }
            }
        }

        private void WriteKeyValuesToDisk()
        {
            if (KeyValues.Count > 0)
            {
                try
                {
                    using (var writer = new StreamWriter(Name))
                    {
                        foreach (var kv in KeyValues)
                        {
                            writer.WriteLine($"{kv.Key}={kv.Value}");
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e, "Exception writing registry to disk");
                }
            }
        }

        public int ReadInt(string key, int defaultValue = default)
        {
            LoadKeyValuesFromDisk();

            var result = KeyValues.Find(kv => kv.Key == key);

            if (result != null)
            {
                int.TryParse(result.Value, out var value);

                return value;
            }
            else
            {
                return defaultValue;
            }
        }

        public void WriteInt(string key, int value)
        {
            LoadKeyValuesFromDisk();

            var result = KeyValues.Find(kv => kv.Key == key);

            if (result != null)
            {
                result.Value = value.ToString();
            }
            else
            {
                KeyValues.Add(new KV { Key = key, Value = value.ToString() });
            }

            WriteKeyValuesToDisk();
        }

        public string ReadString(string key, string defaultValue = default)
        {
            LoadKeyValuesFromDisk();

            var result = KeyValues.Find(kv => kv.Key == key);

            if (result != null)
            {
                return result.Value;
            }
            else
            {
                return defaultValue;
            }
        }

        public void WriteString(string key, string value)
        {
            LoadKeyValuesFromDisk();

            var result = KeyValues.Find(kv => kv.Key == key);

            if (result != null)
            {
                result.Value = value;
            }
            else
            {
                KeyValues.Add(new KV { Key = key, Value = value });
            }

            WriteKeyValuesToDisk();
        }
    }
}
