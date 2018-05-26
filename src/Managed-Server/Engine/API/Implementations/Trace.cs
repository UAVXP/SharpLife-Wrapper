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

namespace Server.Engine.API.Implementations
{
    internal sealed unsafe class Trace : ITrace
    {
        private struct GroupTrace
        {
            public int Mask;

            public GroupOp Op;
        }

        private EngineFuncs EngineFuncs { get; }

        private EntityDictionary EntityDictionary { get; }

        private Stack<GroupTrace> GroupTraces { get; } = new Stack<GroupTrace>();

        public Trace(EngineFuncs engineFuncs, EntityDictionary entityDictionary)
        {
            EngineFuncs = engineFuncs ?? throw new ArgumentNullException(nameof(engineFuncs));
            EntityDictionary = entityDictionary ?? throw new ArgumentNullException(nameof(entityDictionary));

            //Set default trace group
            GroupTraces.Push(new GroupTrace { Mask = 0, Op = GroupOp.And });
        }

        public void Line(in Vector start, in Vector end, TraceFlags flags, Edict entToSkip, out TraceResult tr)
        {
            var nativeEntToSkip = entToSkip != null ? entToSkip.Data : null;

            EngineFuncs.pfnTraceLine(start, end, flags, nativeEntToSkip, out var resultTr);

            tr = resultTr.ToManaged(EntityDictionary);
        }

        public void TraceToss(Edict ent, Edict entToSkip, out TraceResult tr)
        {
            if (ent == null)
            {
                throw new ArgumentNullException(nameof(ent));
            }

            var nativeEntToSkip = entToSkip != null ? entToSkip.Data : null;

            EngineFuncs.pfnTraceToss(EntityDictionary.EdictToNative(ent), nativeEntToSkip, out var resultTr);

            tr = resultTr.ToManaged(EntityDictionary);
        }

        public bool TraceMonsterHull(Edict edict, in Vector start, in Vector end, TraceFlags flags, Edict entToSkip, out TraceResult tr)
        {
            if (edict == null)
            {
                throw new ArgumentNullException(nameof(edict));
            }

            var nativeEntToSkip = entToSkip != null ? entToSkip.Data : null;

            var result = EngineFuncs.pfnTraceMonsterHull(EntityDictionary.EdictToNative(edict), start, end, flags, nativeEntToSkip, out var resultTr);

            tr = resultTr.ToManaged(EntityDictionary);

            return result != 0;
        }

        public void TraceHull(in Vector start, in Vector end, TraceFlags flags, Hull hullNumber, Edict entToSkip, out TraceResult tr)
        {
            var nativeEntToSkip = entToSkip != null ? entToSkip.Data : null;

            EngineFuncs.pfnTraceHull(start, end, flags, hullNumber, nativeEntToSkip, out var resultTr);

            tr = resultTr.ToManaged(EntityDictionary);
        }

        public void TraceModel(in Vector start, in Vector end, Hull hullNumber, Edict ent, out TraceResult tr)
        {
            if (ent == null)
            {
                throw new ArgumentNullException(nameof(ent));
            }

            EngineFuncs.pfnTraceModel(start, end, hullNumber, EntityDictionary.EdictToNative(ent), out var resultTr);

            tr = resultTr.ToManaged(EntityDictionary);
        }

        public string TraceTexture(Edict textureEntity, in Vector start, in Vector end)
        {
            var name = EngineFuncs.pfnTraceTexture(textureEntity != null ?EntityDictionary.EdictToNative(textureEntity) : null, start, end);

            if (name == IntPtr.Zero)
            {
                return string.Empty;
            }

            return Marshal.PtrToStringUTF8(name);
        }

        public void GetGroupTrace(out int mask, out GroupOp op)
        {
            var current = GroupTraces.Peek();

            mask = current.Mask;
            op = current.Op;
        }

        public void PushGroupTrace(int mask, GroupOp op)
        {
            GroupTraces.Push(new GroupTrace { Mask = mask, Op = op });
            EngineFuncs.pfnSetGroupMask(mask, op);
        }

        public void PopGroupTrace()
        {
            if (GroupTraces.Count == 1)
            {
                //Never pop the last one since it's our initial state
                throw new InvalidOperationException("Tried to pop group trace too many times");
            }
            GroupTraces.Pop();
            var current = GroupTraces.Peek();

            EngineFuncs.pfnSetGroupMask(current.Mask, current.Op);
        }
    }
}
