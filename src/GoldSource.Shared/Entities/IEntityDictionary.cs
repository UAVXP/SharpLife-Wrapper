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

using GoldSource.Shared.Engine;
using System.Collections.Generic;

namespace GoldSource.Shared.Entities
{
    /// <summary>
    /// Manages the list of edicts
    /// </summary>
    public interface IEntityDictionary
    {
        /// <summary>
        /// Gets a read-only view of the entity list
        /// This list contains all edicts, even those that are not in use
        /// </summary>
        IReadOnlyList<Edict> Entities { get; }

        /// <summary>
        /// Returns the highest entity index that's in use
        /// -1 if no entities are in use
        /// </summary>
        int HighestInUse { get; }

        /// <summary>
        /// The maximum number of entities that can exist at any one time
        /// </summary>
        int Max { get; }

        unsafe Edict EdictFromNative(Edict.Native* address);

        unsafe Edict.Native* EdictToNative(Edict edict);

        /// <summary>
        /// Gets an edict from its variables
        /// </summary>
        /// <param name="vars"></param>
        /// <returns></returns>
        Edict EdictFromVars(EntVars vars);

        /// <summary>
        /// Gets the index of the given edict
        /// </summary>
        /// <param name="edict"></param>
        /// <returns></returns>
        int EntityIndex(Edict edict);

        /// <summary>
        /// Gets the index of the edict that the given variables are owned by
        /// </summary>
        /// <param name="vars"></param>
        /// <returns></returns>
        int EntityIndex(EntVars vars);

        /// <summary>
        /// Gets an edict by index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        Edict EdictByIndex(int index);

        /// <summary>
        /// Allocate an edict
        /// </summary>
        /// <returns></returns>
        Edict Allocate();

        /// <summary>
        /// Allocates the given edict index for use
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">If the edict is already in use</exception>
        Edict Allocate(int index);

        /// <summary>
        /// Free an edict
        /// </summary>
        /// <param name="edict"></param>
        void Free(Edict edict);
    }
}
