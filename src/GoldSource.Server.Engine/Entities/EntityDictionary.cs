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
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GoldSource.Server.Engine.Entities
{
    internal sealed class EntityDictionary : IEntityDictionary
    {
        private EngineFuncs EngineFuncs { get; }

        private IGlobalVars Globals { get; set; }

        private List<Edict> List { get; set; } = new List<Edict>();

        public IReadOnlyList<Edict> Entities => List;

        public int HighestInUse { get; set; }

        public int Max => List.Count;

        /// <summary>
        /// Used to quickly set other edicts to zeroed out memory
        /// Do not use for anything else
        /// </summary>
        private readonly Edict EmptyEdict;

        internal unsafe EntityDictionary(EngineFuncs engineFuncs)
        {
            EngineFuncs = engineFuncs ?? throw new ArgumentNullException(nameof(engineFuncs));

            var size = Marshal.SizeOf<Edict.Native>();

            var dummyNative = Marshal.AllocHGlobal(size);

            //Zero memory using a zero initialized array
            var bytes = new byte[size];

            Marshal.Copy(bytes, 0, dummyNative, size);

            EmptyEdict = new Edict((Edict.Native*)dummyNative.ToPointer());
        }

        unsafe ~EntityDictionary()
        {
            Marshal.FreeHGlobal(new IntPtr(EmptyEdict.Data));
        }

        internal void SetGlobals(IGlobalVars globals)
        {
            Globals = globals ?? throw new ArgumentNullException(nameof(globals));
        }

        /// <summary>
        /// Called by ServerActivate to initialize the managed list of edicts
        /// </summary>
        /// <param name="pEdictList"></param>
        /// <param name="maxEntities"></param>
        public unsafe void Initialize(Edict.Native* pEdictList, int maxEntities)
        {
            HighestInUse = -1;

            List = new List<Edict>(maxEntities);

            //Create all managed edicts
            for (int i = 0; i < maxEntities; ++i)
            {
                List.Add(new Edict(&pEdictList[i]));
            }
        }

        private unsafe Edict.Native* NativeEdict(int index)
        {
            if (index < 0 || index >= List.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var instance = List[index];

            return instance.Data;
        }

        public unsafe Edict EdictFromNative(Edict.Native* address)
        {
            if (address == null)
            {
                return null;
            }

            var baseAddress = NativeEdict(0);

            var index = address - baseAddress;

            return List[(int)index];
        }

        public unsafe Edict.Native* EdictToNative(Edict edict)
        {
            return edict != null ? edict.Data : null;
        }

        public Edict EdictFromVars(EntVars vars)
        {
            if (vars == null)
            {
                throw new ArgumentNullException(nameof(vars));
            }

            return vars.ContainingEntity;
        }

        private unsafe int EntityIndex(Edict.Native* pEdict)
        {
            var baseAddress = NativeEdict(0);

            var index = pEdict - baseAddress;

            return (int)index;
        }

        public unsafe int EntityIndex(Edict edict)
        {
            if (edict == null)
            {
                throw new ArgumentNullException(nameof(edict));
            }

            return EntityIndex(edict.Data);
        }

        public int EntityIndex(EntVars vars)
        {
            return EntityIndex(EdictFromVars(vars));
        }

        public Edict EdictByIndex(int index)
        {
            return List[index];
        }

        public unsafe Edict Allocate()
        {
            //TODO: the engine sys_errors if we run out of edicts, so try to implement this in managed code
            var edict = EdictFromNative(EngineFuncs.pfnCreateEntity());

            var index = EntityIndex(edict);

            if (index > HighestInUse)
            {
                HighestInUse = index;
            }

            return edict;
        }

        public unsafe Edict Allocate(int index)
        {
            var edict = EdictByIndex(index);

            if (edict.PrivateData != null)
            {
                throw new InvalidOperationException("Cannot allocate an edict that is already in use");
            }

            //Never zero out the world, since the engine sets some data that we need
            if (index != 0)
            {
                //Zero out memory
                *edict.Data = *EmptyEdict.Data;
            }

            edict.Free = false;
            edict.Data->v.pContainingEntity = edict.Data;

            return edict;
        }

        public unsafe void Free(Edict edict)
        {
            if (edict?.Free != false)
            {
                return;
            }

            var index = EntityIndex(edict);

            if (HighestInUse == index)
            {
                //Recalculate
                //if this is the last entity then we won't find a new highest
                HighestInUse = -1;

                for (var i = index - 1; i >= 0; --i)
                {
                    if (!EdictByIndex(i).Free)
                    {
                        HighestInUse = i;
                        break;
                    }
                }
            }

            EngineFuncs.pfnRemoveEntity(edict.Data);
        }
    }
}
