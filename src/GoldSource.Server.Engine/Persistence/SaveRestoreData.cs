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
using GoldSource.Shared.Engine;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GoldSource.Server.Engine.Persistence
{
    public sealed unsafe class SaveRestoreData
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct Native
        {
            internal const int LevelListSize = 80;

            internal byte* pBaseData;    // Start of all entity save data
            internal byte* pCurrentData; // Current buffer pointer for sequential access
            internal int size;           // Current data size
            internal int bufferSize;     // Total space for data
            internal int tokenSize;      // Size of the linear list of tokens
            internal int tokenCount;     // Number of elements in the pTokens table
            internal byte** pTokens;     // Hash table of entity strings (sparse)
            internal int currentIndex;   // Holds a global entity table ID
            internal int tableCount;     // Number of elements in the entity table
            internal int connectionCount;// Number of elements in the levelList[]
            internal EntityTable* pTable;        // Array of ENTITYTABLE elements (1 for each entity)

            //Can't use fixed size array of struct, so manually marshal as needed
            internal fixed byte levelList[LevelListSize * Constants.MaxLevelConnections];     // List of connections from this level

            // smooth transition
            internal int fUseLandmark;
            internal fixed byte szLandmarkName[20];// landmark we'll spawn near in next level
            internal Vector vecLandmarkOffset;// for landmark transitions
            internal float time;
            internal fixed byte szCurrentMapName[32];	// To check global entities
        }

        internal Native* Data { get; }

        internal SaveRestoreData(Native* nativeMemory)
        {
            Data = nativeMemory;
        }

        internal LevelList GetLevelList(int index)
        {
            byte* pAddress = &Data->levelList[Native.LevelListSize * index];

            return Marshal.PtrToStructure<LevelList>(new IntPtr(pAddress));
        }

        internal void SetLevelList(int index, LevelList value)
        {
            byte* pAddress = &Data->levelList[Native.LevelListSize * index];

            Marshal.StructureToPtr(value, new IntPtr(pAddress), true);
        }

        internal List<LevelList> LevelList
        {
            get
            {
                var list = new List<LevelList>(Constants.MaxLevelConnections);

                for (var i = 0; i < Constants.MaxLevelConnections; ++i)
                {
                    list.Add(GetLevelList(i));
                }

                return list;
            }

            set
            {
                //The list given here should be the same list acquired using get so as to match the data
                if (value.Count != Constants.MaxLevelConnections)
                {
                    throw new ArgumentOutOfRangeException($"Level list must contain exactly {Constants.MaxLevelConnections} entries");
                }

                var index = 0;

                foreach (var list in value)
                {
                    SetLevelList(index++, list);
                }
            }
        }
    }
}
