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
using GoldSource.Server.Engine.Networking;
using GoldSource.Server.Engine.Sequences;
using GoldSource.Shared.Engine;
using GoldSource.Shared.Engine.Sound;
using GoldSource.Shared.Entities;
using System;
using System.Runtime.InteropServices;

namespace GoldSource.Server.Engine.Wrapper.API.Interfaces
{
    [StructLayout(LayoutKind.Sequential)]
    internal sealed unsafe class EngineFuncs
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int PrecacheModel(IntPtr name);

        internal PrecacheModel pfnPrecacheModel;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int PrecacheSound(IntPtr name);

        internal PrecacheSound pfnPrecacheSound;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void SetModel(Edict.Native* edict, IntPtr name);

        internal SetModel pfnSetModel;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int ModelIndex([MarshalAs(UnmanagedType.LPStr)]string name);

        internal ModelIndex pfnModelIndex;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int ModelFrames(int modelindex);

        internal ModelFrames pfnModelFrames;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void SetSize(Edict.Native* edict, in Vector mins, in Vector maxs);

        internal SetSize pfnSetSize;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ChangeLevel([MarshalAs(UnmanagedType.LPStr)]string levelName, [MarshalAs(UnmanagedType.LPStr)]string spawnspot);

        internal ChangeLevel pfnChangeLevel;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void GetSpawnParms(Edict.Native* edict);

        internal GetSpawnParms pfnGetSpawnParms;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void SaveSpawnParms(Edict.Native* edict);

        internal SaveSpawnParms pfnSaveSpawnParms;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate float VecToYaw(in Vector vector);

        internal VecToYaw pfnVecToYaw;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void VecToAngles(in Vector vector, out Vector angles);

        internal VecToAngles pfnVecToAngles;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void MoveToOrigin(Edict.Native* edict, in Vector goal, float dist, GoldSource.Shared.Engine.MoveToOrigin moveType);

        internal MoveToOrigin pfnMoveToOrigin;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ChangeYaw(Edict.Native* edict);

        internal ChangeYaw pfnChangeYaw;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ChangePitch(Edict.Native* edict);

        internal ChangePitch pfnChangePitch;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate Edict.Native* FindEntitybyString(Edict.Native* startSearchAfter, [MarshalAs(UnmanagedType.LPStr)]string field, [MarshalAs(UnmanagedType.LPStr)]string value);

        internal FindEntitybyString pfnFindEntityByString;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int GetEntityIllum(Edict.Native* edict);

        internal GetEntityIllum pfnGetEntityIllum;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate Edict.Native* FindEntityInSphere(Edict.Native* edict, in Vector origin, float radius);

        internal FindEntityInSphere pfnFindEntityInSphere;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate Edict.Native* FindClientInPVS(Edict.Native* edict);

        internal FindClientInPVS pfnFindClientInPVS;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate Edict.Native* EntitiesInPVS(Edict.Native* edict);

        internal EntitiesInPVS pfnEntitiesInPVS;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void MakeVectors(in Vector vector);

        internal MakeVectors pfnMakeVectors;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void AngleVectors(in Vector vector, out Vector forward, out Vector right, out Vector up);

        internal AngleVectors pfnAngleVectors;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate Edict.Native* CreateEntity();

        internal CreateEntity pfnCreateEntity;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void RemoveEntity(Edict.Native* edict);

        internal RemoveEntity pfnRemoveEntity;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate Edict.Native* CreateNamedEntity(EngineString name);

        internal CreateNamedEntity pfnCreateNamedEntity;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void MakeStatic(Edict.Native* edict);

        internal MakeStatic pfnMakeStatic;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int EntIsOnFloor(Edict.Native* edict);

        internal EntIsOnFloor pfnEntIsOnFloor;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int DropToFloor(Edict.Native* edict);

        internal DropToFloor pfnDropToFloor;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int WalkMove(Edict.Native* edict, float yaw, float distance, int mode);

        internal WalkMove pfnWalkMove;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void SetOrigin(Edict.Native* edict, in Vector origin);

        internal SetOrigin pfnSetOrigin;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void EmitSound(Edict.Native* edict, SoundChannel channel, string sample, float volume, float attenuation, SoundFlags flags, int pitch);

        internal EmitSound pfnEmitSound;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void EmitAmbientSound(Edict.Native* edict, in Vector position, string sample, float volume, float attenuation, SoundFlags flags, int pitch);

        internal EmitAmbientSound pfnEmitAmbientSound;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void TraceLine(in Vector start, in Vector end, TraceFlags flags, Edict.Native* entToSkip, out TraceResult tr);

        internal TraceLine pfnTraceLine;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void TraceToss(Edict.Native* ent, Edict.Native* entToSkip, out TraceResult tr);

        internal TraceToss pfnTraceToss;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int TraceMonsterHull(Edict.Native* edict, in Vector start, in Vector end, TraceFlags flags, Edict.Native* entToSkip, out TraceResult tr);

        internal TraceMonsterHull pfnTraceMonsterHull;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void TraceHull(in Vector start, in Vector end, TraceFlags flags, Hull hullNumber, Edict.Native* entToSkip, out TraceResult tr);

        internal TraceHull pfnTraceHull;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void TraceModel(in Vector start, in Vector end, Hull hullNumber, Edict.Native* ent, out TraceResult tr);

        internal TraceModel pfnTraceModel;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr TraceTexture(Edict.Native* textureEntity, in Vector start, in Vector end);

        internal TraceTexture pfnTraceTexture;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void TraceSphere(in Vector start, in Vector end, int flags, float radius, Edict.Native* entToSkip, out TraceResult tr);

        internal TraceSphere pfnTraceSphere;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void GetAimVector(Edict.Native* ent, float speed, out Vector result);

        internal GetAimVector pfnGetAimVector;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ServerCommand([MarshalAs(UnmanagedType.LPStr)] string command);

        internal ServerCommand pfnServerCommand;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ServerExecute();

        internal ServerExecute pfnServerExecute;

        //Actually a varargs function, but can't expose that
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ClientCommand(Edict.Native* edict, [MarshalAs(UnmanagedType.LPStr)] string command);

        internal ClientCommand pfnClientCommand;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ParticleEffect(in Vector origin, in Vector direction, float color, float count);

        internal ParticleEffect pfnParticleEffect;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void LightStyle(int style, [MarshalAs(UnmanagedType.LPStr)] string value);

        internal LightStyle pfnLightStyle;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int DecalIndex(int style, [MarshalAs(UnmanagedType.LPStr)] string name);

        internal DecalIndex pfnDecalIndex;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate Contents PointContents(in Vector point);

        internal PointContents pfnPointContents;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void MessageBegin(MsgDest msg_dest, int msg_type, IntPtr origin, Edict.Native* ed = null);

        internal MessageBegin pfnMessageBegin;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void MessageEnd();

        internal MessageEnd pfnMessageEnd;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void WriteByte(int value);

        internal WriteByte pfnWriteByte;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void WriteChar(int value);

        internal WriteChar pfnWriteChar;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void WriteShort(int value);

        internal WriteShort pfnWriteShort;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void WriteLong(int value);

        internal WriteLong pfnWriteLong;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void WriteAngle(float value);

        internal WriteAngle pfnWriteAngle;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void WriteCoord(float value);

        internal WriteCoord pfnWriteCoord;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void WriteString([MarshalAs(UnmanagedType.LPStr)] string str);

        internal WriteString pfnWriteString;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void WriteEntity(int value);

        internal WriteEntity pfnWriteEntity;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void CVarRegister(IntPtr cvar);

        internal CVarRegister pfnCVarRegister;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate float CVarGetFloat([MarshalAs(UnmanagedType.LPStr)] string name);

        internal CVarGetFloat pfnCVarGetFloat;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr CVarGetString([MarshalAs(UnmanagedType.LPStr)] string name);

        internal CVarGetString pfnCVarGetString;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void CVarSetFloat([MarshalAs(UnmanagedType.LPStr)] string name, float value);

        internal CVarSetFloat pfnCVarSetFloat;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void CVarSetString([MarshalAs(UnmanagedType.LPStr)] string name, [MarshalAs(UnmanagedType.LPStr)] string value);

        internal CVarSetString pfnCVarSetString;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void AlertMessage(AlertType atype, [MarshalAs(UnmanagedType.LPStr)] string format, [MarshalAs(UnmanagedType.LPStr)] string args);

        internal AlertMessage pfnAlertMessage;

        //not used
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void EngineFPrintf();

        internal EngineFPrintf pfnEngineFprintf;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void PvAllocEntPrivateData(Edict.Native* edict, int size);

        internal PvAllocEntPrivateData pfnPvAllocEntPrivateData;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr PvEntPrivateData(Edict.Native* edict);

        internal PvEntPrivateData pfnPvEntPrivateData;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void FreeEntPrivateData(Edict.Native* edict);

        internal FreeEntPrivateData pfnFreeEntPrivateData;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr SzFromIndex(int index);

        internal SzFromIndex pfnSzFromIndex;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int AllocString([MarshalAs(UnmanagedType.LPStr)] string str);

        internal AllocString pfnAllocString;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate EntVars.Native* GetVarsOfEnt(Edict.Native* edict);

        internal GetVarsOfEnt pfnGetVarsOfEnt;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate Edict.Native* PEntityOfEntOffset(int eoffset);

        internal PEntityOfEntOffset pfnPEntityOfEntOffset;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int EntOffsetOfPEntity(Edict.Native* edict);

        internal EntOffsetOfPEntity pfnEntOffsetOfPEntity;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int IndexOfEdict(Edict.Native* edict);

        internal IndexOfEdict pfnIndexOfEdict;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate Edict.Native* PEntityOfEntIndex(int index);

        internal PEntityOfEntIndex pfnPEntityOfEntIndex;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate Edict.Native* FindEntityByVars(EntVars.Native* vars);

        internal FindEntityByVars pfnFindEntityByVars;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr GetModelPtr(Edict.Native* edict);

        internal GetModelPtr pfnGetModelPtr;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int RegUserMsg([MarshalAs(UnmanagedType.LPStr)] string name, int size);

        internal RegUserMsg pfnRegUserMsg;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void AnimationAutomove(Edict.Native* edict, float time);

        internal AnimationAutomove pfnAnimationAutomove;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void GetBonePosition(Edict.Native* edict, int bone, out Vector origin, out Vector angles);

        internal GetBonePosition pfnGetBonePosition;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate uint FunctionFromName([MarshalAs(UnmanagedType.LPStr)] string name);

        internal FunctionFromName pfnFunctionFromName;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr NameForFunction(uint function);

        internal NameForFunction pfnNameForFunction;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ClientPrintf(Edict.Native* client, PrintType type, [MarshalAs(UnmanagedType.LPStr)]string message);

        internal ClientPrintf pfnClientPrintf;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ServerPrint([MarshalAs(UnmanagedType.LPStr)]string message);

        internal ServerPrint pfnServerPrint;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr Cmd_Args();

        internal Cmd_Args pfnCmd_Args;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr Cmd_Argv(int arg);

        internal Cmd_Argv pfnCmd_Argv;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int Cmd_Argc();

        internal Cmd_Argc pfnCmd_Argc;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void GetAttachment(Edict.Native* edict, int attachment, out Vector origin, out Vector angles);

        internal GetAttachment pfnGetAttachment;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void CRC32_Init(out uint crc);

        internal CRC32_Init pfnCRC32_Init;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void CRC32_ProcessBuffer(ref uint crc, object obj, int size);

        internal CRC32_ProcessBuffer pfnCRC32_ProcessBuffer;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void CRC32_ProcessByte(ref uint crc, byte value);

        internal CRC32_ProcessByte pfnCRC32_ProcessByte;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate uint CRC32_Final(uint crc);

        internal CRC32_Final pfnCRC32_Final;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int RandomLong(int low, int high);

        internal RandomLong pfnRandomLong;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate float RandomFloat(float low, float high);

        internal RandomFloat pfnRandomFloat;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void SetView(Edict.Native* edict, Edict.Native* viewEdict);

        internal SetView pfnSetView;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate float Time();

        internal Time pfnTime;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void CrosshairAngle(Edict.Native* client, float pitch, float yaw);

        internal CrosshairAngle pfnCrosshairAngle;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate byte* LoadFileForMe([MarshalAs(UnmanagedType.LPStr)]string fileName, out int length);

        internal LoadFileForMe pfnLoadFileForMe;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void FreeFile(byte* data);

        internal FreeFile pfnFreeFile;

        internal byte[] LoadFileForMeHelper(string fileName)
        {
            var pBuffer = pfnLoadFileForMe(fileName, out var length);

            if (pBuffer != null)
            {
                //The engine allocates length + 1 bytes, and sets the extra byte to 0
                //This is meant for null terminated text, but it always does this
                var result = new byte[length + 1];

                Marshal.Copy(new IntPtr(pBuffer), result, 0, length + 1);

                pfnFreeFile(pBuffer);

                return result;
            }

            return new byte[0];
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void EndSection([MarshalAs(UnmanagedType.LPStr)]string sectionName);

        internal EndSection pfnEndSection;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int CompareFileTime([MarshalAs(UnmanagedType.LPStr)]string fileName1, [MarshalAs(UnmanagedType.LPStr)]string fileName2, out int compare);

        internal CompareFileTime pfnCompareFileTime;

        internal const int MAX_PATH = 260;

        /// <summary>
        /// Expects a buffer of MAX_PATH size
        /// </summary>
        /// <param name="szGetGameDir"></param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void GetGameDir(IntPtr szGetGameDir);

        internal GetGameDir pfnGetGameDir;

        internal string GetGameDirHelper()
        {
            var nativeMemory = Marshal.AllocHGlobal(sizeof(byte) * MAX_PATH);

            pfnGetGameDir(nativeMemory);

            //The engine was built for ASCII only, but now supports UTF8.
            //Being compatible with ASCII, this is the better choice to avoid weird bugs when international symbols are used in mod names.
            var result = Marshal.PtrToStringUTF8(nativeMemory);

            Marshal.FreeHGlobal(nativeMemory);
            return result;
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void Cvar_RegisterVariable(IntPtr cvar);

        internal Cvar_RegisterVariable pfnCvar_RegisterVariable;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void FadeClientVolume(Edict.Native* edict, int fadePercent, int fadeOutSeconds, int holdTime, int fadeInSeconds);

        internal FadeClientVolume pfnFadeClientVolume;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void SetClientMaxspeed(Edict.Native* edict, float fNewMaxspeed);

        internal SetClientMaxspeed pfnSetClientMaxspeed;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate Edict.Native* CreateFakeClient([MarshalAs(UnmanagedType.LPStr)]string netname);

        internal CreateFakeClient pfnCreateFakeClient;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void RunPlayerMove(Edict.Native* fakeclient, in Vector viewangles, float forwardmove, float sidemove, float upmove, ushort buttons, byte impulse, byte msec);

        internal RunPlayerMove pfnRunPlayerMove;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int NumberOfEntities();

        internal NumberOfEntities pfnNumberOfEntities;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr GetInfoKeyBuffer(Edict.Native* e);

        internal GetInfoKeyBuffer pfnGetInfoKeyBuffer;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr InfoKeyValue(IntPtr infobuffer, [MarshalAs(UnmanagedType.LPStr)]string key);

        internal InfoKeyValue pfnInfoKeyValue;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void SetKeyValue(IntPtr infobuffer, [MarshalAs(UnmanagedType.LPStr)]string key, [MarshalAs(UnmanagedType.LPStr)]string value);

        internal SetKeyValue pfnSetKeyValue;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void SetClientKeyValue(int clientIndex, IntPtr infobuffer, [MarshalAs(UnmanagedType.LPStr)]string key, [MarshalAs(UnmanagedType.LPStr)]string value);

        internal SetClientKeyValue pfnSetClientKeyValue;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int IsMapValid([MarshalAs(UnmanagedType.LPStr)]string filename);

        internal IsMapValid pfnIsMapValid;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void StaticDecal(in Vector origin, int decalIndex, int entityIndex, int modelIndex);

        internal StaticDecal pfnStaticDecal;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int PrecacheGeneric(IntPtr name);

        internal PrecacheGeneric pfnPrecacheGeneric;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int GetPlayerUserId(Edict.Native* e);

        internal GetPlayerUserId pfnGetPlayerUserId;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void BuildSoundMsg(Edict.Native* entity, SoundChannel channel, [MarshalAs(UnmanagedType.LPStr)]string sample, float volume, float attenuation, int fFlags, int pitch, int msg_dest, int msg_type, in Vector pOrigin, Edict.Native* ed);

        internal BuildSoundMsg pfnBuildSoundMsg;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int IsDedicatedServer();

        internal IsDedicatedServer pfnIsDedicatedServer;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr CVarGetPointer([MarshalAs(UnmanagedType.LPStr)]string szVarName);

        internal CVarGetPointer pfnCVarGetPointer;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int GetPlayerWONId(Edict.Native* e);

        internal GetPlayerWONId pfnGetPlayerWONId;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void Info_RemoveKey(IntPtr s, [MarshalAs(UnmanagedType.LPStr)]string key);

        internal Info_RemoveKey pfnInfo_RemoveKey;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr GetPhysicsKeyValue(Edict.Native* pClient, [MarshalAs(UnmanagedType.LPStr)]string key);

        internal GetPhysicsKeyValue pfnGetPhysicsKeyValue;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void SetPhysicsKeyValue(Edict.Native* pClient, [MarshalAs(UnmanagedType.LPStr)]string key, [MarshalAs(UnmanagedType.LPStr)]string value);

        internal SetPhysicsKeyValue pfnSetPhysicsKeyValue;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr GetPhysicsInfoString(Edict.Native* pClient);

        internal GetPhysicsInfoString pfnGetPhysicsInfoString;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate ushort PrecacheEvent(int type, [MarshalAs(UnmanagedType.LPStr)]string psz);

        internal PrecacheEvent pfnPrecacheEvent;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void PlaybackEvent(int flags, Edict.Native* pInvoker, ushort eventindex, float delay, in Vector origin, in Vector angles, float fparam1, float fparam2, int iparam1, int iparam2, int bparam1, int bparam2);

        internal PlaybackEvent pfnPlaybackEvent;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr SetFatPVS(in Vector org);

        internal SetFatPVS pfnSetFatPVS;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr SetFatPAS(in Vector org);

        internal SetFatPAS pfnSetFatPAS;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int CheckVisibility(Edict.Native* entity, IntPtr pset);

        internal CheckVisibility pfnCheckVisibility;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void DeltaSetField(Delta.Native* pFields, [MarshalAs(UnmanagedType.LPStr)]string fieldname);

        internal DeltaSetField pfnDeltaSetField;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void DeltaUnsetField(Delta.Native* pFields, [MarshalAs(UnmanagedType.LPStr)]string fieldname);

        internal DeltaUnsetField pfnDeltaUnsetField;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void DeltaEncoder(Delta.Native* pFields, byte* from, byte* to);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void DeltaAddEncoder([MarshalAs(UnmanagedType.LPStr)]string name, [MarshalAs(UnmanagedType.FunctionPtr)]DeltaEncoder conditionalencode);

        internal DeltaAddEncoder pfnDeltaAddEncoder;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int GetCurrentPlayer();

        internal GetCurrentPlayer pfnGetCurrentPlayer;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int CanSkipPlayer(Edict.Native* player);

        internal CanSkipPlayer pfnCanSkipPlayer;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int DeltaFindField(Delta.Native* pFields, [MarshalAs(UnmanagedType.LPStr)]string fieldname);

        internal DeltaFindField pfnDeltaFindField;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void DeltaSetFieldByIndex(Delta.Native* pFields, int fieldNumber);

        internal DeltaSetFieldByIndex pfnDeltaSetFieldByIndex;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void DeltaUnsetFieldByIndex(Delta.Native* pFields, int fieldNumber);

        internal DeltaUnsetFieldByIndex pfnDeltaUnsetFieldByIndex;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void SetGroupMask(int mask, GroupOp op);

        internal SetGroupMask pfnSetGroupMask;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int CreateInstancedBaseline(int classname, EntityState.Native* baseline);

        internal CreateInstancedBaseline pfnCreateInstancedBaseline;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void Cvar_DirectSet(IntPtr var, [MarshalAs(UnmanagedType.LPStr)]string value);

        internal Cvar_DirectSet pfnCvar_DirectSet;

        //Expects a string with a lifetime matching the map
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ForceUnmodified(ForceType type, IntPtr mins, IntPtr maxs, IntPtr filename);

        internal ForceUnmodified pfnForceUnmodified;

        internal void ForceUnmodifiedHelper(ForceType type, in Vector mins, in Vector maxs, string filename, IStringPool stringPool)
        {
            if (stringPool == null)
            {
                throw new ArgumentNullException(nameof(stringPool));
            }

            var pooled = stringPool.GetPooledString(filename);

            var minsAddress = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Vector)));
            var maxsAddress = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Vector)));

            try
            {
                Marshal.StructureToPtr(mins, minsAddress, false);
                Marshal.StructureToPtr(maxs, maxsAddress, false);
                pfnForceUnmodified(type, minsAddress, maxsAddress, pooled);
            }
            finally
            {
                Marshal.FreeHGlobal(maxsAddress);
                Marshal.FreeHGlobal(minsAddress);
            }
        }

        internal void ForceUnmodifiedHelper(ForceType type, string filename, IStringPool stringPool)
        {
            if (stringPool == null)
            {
                throw new ArgumentNullException(nameof(stringPool));
            }

            var pooled = stringPool.GetPooledString(filename);

            pfnForceUnmodified(type, IntPtr.Zero, IntPtr.Zero, pooled);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void GetPlayerStats(Edict.Native* pClient, out int ping, out int packet_loss);

        internal GetPlayerStats pfnGetPlayerStats;

        internal delegate void ServerCommandCallback();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void AddServerCommand([MarshalAs(UnmanagedType.LPStr)]string cmd_name, [MarshalAs(UnmanagedType.FunctionPtr)]ServerCommandCallback function);

        internal AddServerCommand pfnAddServerCommand;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate QBoolean Voice_GetClientListening(int iReceiver, int iSender);

        internal Voice_GetClientListening pfnVoice_GetClientListening;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate QBoolean Voice_SetClientListening(int iReceiver, int iSender, QBoolean bListen);

        internal Voice_SetClientListening pfnVoice_SetClientListening;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr GetPlayerAuthId(Edict.Native* e);

        internal GetPlayerAuthId pfnGetPlayerAuthId;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate SequenceEntry.Native* SequenceGet([MarshalAs(UnmanagedType.LPStr)]string fileName, [MarshalAs(UnmanagedType.LPStr)]string entryName);

        internal SequenceGet pfnSequenceGet;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate SentenceEntry.Native* SequencePickSentence([MarshalAs(UnmanagedType.LPStr)]string groupName, int pickMethod, out int picked);

        internal SequencePickSentence pfnSequencePickSentence;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int GetFileSize([MarshalAs(UnmanagedType.LPStr)]string filename);

        internal GetFileSize pfnGetFileSize;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate uint GetApproxWavePlayLen([MarshalAs(UnmanagedType.LPStr)]string filepath);

        internal GetApproxWavePlayLen pfnGetApproxWavePlayLen;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int IsCareerMatch();

        internal IsCareerMatch pfnIsCareerMatch;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int GetLocalizedStringLength([MarshalAs(UnmanagedType.LPStr)]string label);

        internal GetLocalizedStringLength pfnGetLocalizedStringLength;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void RegisterTutorMessageShown(int mid);

        internal RegisterTutorMessageShown pfnRegisterTutorMessageShown;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int GetTimesTutorMessageShown(int mid);

        internal GetTimesTutorMessageShown pfnGetTimesTutorMessageShown;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ProcessTutorMessageDecayBuffer(int* buffer, int bufferLength);

        internal ProcessTutorMessageDecayBuffer pfnProcessTutorMessageDecayBuffer;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ConstructTutorMessageDecayBuffer(int* buffer, int bufferLength);

        internal ConstructTutorMessageDecayBuffer pfnConstructTutorMessageDecayBuffer;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void ResetTutorMessageDecayData();

        internal ResetTutorMessageDecayData pfnResetTutorMessageDecayData;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void QueryClientCvarValue(Edict.Native* player, [MarshalAs(UnmanagedType.LPStr)]string cvarName);

        internal QueryClientCvarValue pfnQueryClientCvarValue;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void QueryClientCvarValue2(Edict.Native* player, [MarshalAs(UnmanagedType.LPStr)]string cvarName, int requestID);

        internal QueryClientCvarValue2 pfnQueryClientCvarValue2;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int CheckParm([MarshalAs(UnmanagedType.LPStr)]string pchCmdLineToken, byte** ppnext);

        internal CheckParm pfnCheckParm;

        internal int CheckParmHelper(string pchCmdLineToken, out string next)
        {
            byte* pnext = null;

            var result = pfnCheckParm(pchCmdLineToken, &pnext);

            //CheckParm returns the index of the parameter
            //If it's 0, the engine will not bother to set ppnext to a value, instead setting null
            if (result != 0 && pnext != null)
            {
                next = Marshal.PtrToStringUTF8(new IntPtr(pnext));
            }
            else
            {
                next = null;
            }

            return result;
        }
    }
}
