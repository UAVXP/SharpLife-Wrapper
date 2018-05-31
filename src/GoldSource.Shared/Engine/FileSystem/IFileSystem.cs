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

namespace GoldSource.Shared.Engine.FileSystem
{
    /// <summary>
    /// Represents the engine's filesystem
    /// Supports SteamPipe directories
    /// </summary>
    public interface IFileSystem
    {
        /// <summary>
        /// Adds a search path
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="pathID"></param>
        /// <param name="writeAccess"></param>
        void AddSearchPath(string basePath, string pathID, bool writeAccess = true);

        /// <summary>
        /// Removes all search paths with the given base path
        /// </summary>
        /// <param name="basePath"></param>
        /// <returns></returns>
        bool RemoveSearchPath(string basePath);

        /// <summary>
        /// Removes all search paths
        /// </summary>
        void RemoveAllSearchPaths();

        /// <summary>
        /// Given a relative path, returns the absolute path to it if it exists, otherwise returns <see cref="defaultValue"/>
        /// </summary>
        /// <param name="relativePath"></param>
        /// <param name="pathID">If not null, specifies that only paths with this Id should be checked for the file's existence</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        string GetAbsolutePath(string relativePath, string pathID = null, string defaultValue = null);

        /// <summary>
        /// Given an absolute path, attempts to resolve the path to a relative path
        /// </summary>
        /// <param name="absolutePath"></param>
        /// <param name="pathID">If not null, specifies that only paths with this Id should be used to resolve the path</param>
        /// <param name="defaultValue"></param>
        /// <returns>The relative path if it could be resolved, otherwise returns <see cref="defaultValue"/></returns>
        string GetRelativePath(string absolutePath, string pathID = null, string defaultValue = null);

        /// <summary>
        /// Given a relative path, attempts to construct an absolute path that can be written to
        /// </summary>
        /// <param name="relativePath"></param>
        /// <param name="pathID">If not null, specifies that only paths with this Id should be used to construct the path</param>
        /// <param name="defaultValue"></param>
        /// <returns>The absolute path if it could be constructed, otherwise returns <see cref="defaultValue"/></returns>
        string GetWritePath(string relativePath, string pathID = null, string defaultValue = null);

        /// <summary>
        /// Creates all directories for the given path
        /// </summary>
        /// <param name="relativePath"></param>
        /// <param name="pathID"></param>
        /// <returns></returns>
        bool CreateDirectoryHierarchy(string relativePath, string pathID = null);

        /// <summary>
        /// Checks whether the file exists
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="pathID">If not null, the ID of the paths to check for the file's existence, otherwise all paths are searched</param>
        /// <returns></returns>
        bool Exists(string fileName, string pathID = null);
    }
}
