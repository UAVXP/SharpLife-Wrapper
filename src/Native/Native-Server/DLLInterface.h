#ifndef WRAPPER_DLLINTERFACE_H
#define WRAPPER_DLLINTERFACE_H

#include "Dlls/extdll.h"
#include "Common/entity_state.h"
#include "Common/netadr.h"
#include "Common/weaponinfo.h"
#include "PmShared/pm_defs.h"

/**
*	@brief Creates an instance of the DLL_FUNCTIONS interface
*/
DLL_FUNCTIONS CreateDLLFunctionsInterface( const Wrapper::CConfiguration& configuration );

void GameDLLInit();

int DispatchSpawn( edict_t* pent );
void DispatchKeyValue( edict_t* pentKeyvalue, KeyValueData* pkvd );
void DispatchTouch( edict_t* pentTouched, edict_t* pentOther );
void DispatchUse( edict_t* pentUsed, edict_t* pentOther );
void DispatchThink( edict_t* pent );
void DispatchBlocked( edict_t* pentBlocked, edict_t* pentOther );
void DispatchSave( edict_t* pent, SAVERESTOREDATA* pSaveData );
int  DispatchRestore( edict_t* pent, SAVERESTOREDATA* pSaveData, int globalEntity );
void DispatchObjectCollisionBox( edict_t* pent );
void SaveWriteFields( SAVERESTOREDATA* pSaveData, const char* pname, void* pBaseData, TYPEDESCRIPTION* pFields, int fieldCount );
void SaveReadFields( SAVERESTOREDATA* pSaveData, const char* pname, void* pBaseData, TYPEDESCRIPTION* pFields, int fieldCount );
void SaveGlobalState( SAVERESTOREDATA* pSaveData );
void RestoreGlobalState( SAVERESTOREDATA* pSaveData );
void ResetGlobalState();

BOOL ClientConnect( edict_t* pEntity, const char* pszName, const char* pszAddress, char szRejectReason[ 128 ] );
void ClientDisconnect( edict_t* pEntity );
void ClientKill( edict_t* pEntity );
void ClientPutInServer( edict_t* pEntity );
void ClientCommand( edict_t* pEntity );
void ClientUserInfoChanged( edict_t* pEntity, char* infobuffer );
void ServerActivate( edict_t* pEdictList, int edictCount, int clientMax );
void ServerDeactivate();
void StartFrame();
void PlayerPostThink( edict_t* pEntity );
void PlayerPreThink( edict_t* pEntity );
void ParmsNewLevel();
void ParmsChangeLevel();

const char* GetGameDescription();

void PlayerCustomization( edict_t* pEntity, customization_t* pCust );

void SpectatorConnect( edict_t* pEntity );
void SpectatorDisconnect( edict_t* pEntity );
void SpectatorThink( edict_t* pEntity );

void Sys_Error( const char* error_string );

void PM_Init( playermove_t* ppmove );
void PM_Move( playermove_t* ppmove, int server );
char PM_FindTextureType( char* name );

void SetupVisibility( edict_t* pViewEntity, edict_t* pClient, unsigned char** pvs, unsigned char** pas );
void UpdateClientData( const edict_t* ent, int sendweapons, clientdata_t* cd );
int AddToFullPack( entity_state_t* state, int e, edict_t* ent, edict_t* host, int hostflags, int player, unsigned char* pSet );
void CreateBaseline( int player, int eindex, entity_state_t* baseline, edict_t* entity, int playermodelindex, vec3_t player_mins, vec3_t player_maxs );
void RegisterEncoders();

int GetWeaponData( edict_t* player, weapon_data_t* info );

void CmdStart( const edict_t* player, const struct usercmd_s* cmd, unsigned int random_seed );
void CmdEnd( const edict_t* player );

int	ConnectionlessPacket( const netadr_t* net_from, const char* args, char* response_buffer, int* response_buffer_size );

int GetHullBounds( int hullnumber, float* mins, float* maxs );

void CreateInstancedBaselines();

int	InconsistentFile( const edict_t* player, const char* filename, char* disconnect_message );

int AllowLagCompensation();

#endif //WRAPPER_DLLINTERFACE_H
