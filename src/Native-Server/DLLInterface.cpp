#include <string>

#include "CConfiguration.h"
#include "CManagedServer.h"
#include "DLLInterface.h"
#include "Log.h"
#include "Utility/InterfaceUtils.h"

using namespace Wrapper::Utility;

DLL_FUNCTIONS CreateDLLFunctionsInterface( const Wrapper::CConfiguration& configuration )
{
	ProcessSharpLifeInterfaceFunctor functor{ configuration.EnableDebugInterface };

	DLL_FUNCTIONS dllFunctions =
	{
		functor( GameDLLInit ),
		functor( DispatchSpawn ),
		functor( DispatchThink ),
		functor( DispatchUse ),
		functor( DispatchTouch ),
		functor( DispatchBlocked ),
		functor( DispatchKeyValue ),
		functor( DispatchSave ),
		functor( DispatchRestore ),
		functor( DispatchObjectCollisionBox ),

		functor( SaveWriteFields ),
		functor( SaveReadFields ),

		functor( SaveGlobalState ),
		functor( RestoreGlobalState ),
		functor( ResetGlobalState ),

		functor( ClientConnect ),
		functor( ClientDisconnect ),
		functor( ClientKill ),
		functor( ClientPutInServer ),
		functor( ClientCommand ),
		functor( ClientUserInfoChanged ),
		functor( ServerActivate ),
		functor( ServerDeactivate ),

		functor( PlayerPreThink ),
		functor( PlayerPostThink ),

		functor( StartFrame ),
		functor( ParmsNewLevel ),
		functor( ParmsChangeLevel ),

		//Overridden to manage memory properly
		functor( GetGameDescription, true ),
		functor( PlayerCustomization ),

		functor( SpectatorConnect ),
		functor( SpectatorDisconnect ),
		functor( SpectatorThink ),

		functor( Sys_Error ),

		functor( PM_Move ),
		functor( PM_Init ),
		functor( PM_FindTextureType ),

		functor( SetupVisibility ),
		functor( UpdateClientData ),
		functor( AddToFullPack, true ),
		functor( CreateBaseline ),
		functor( RegisterEncoders ),
		functor( GetWeaponData ),
		functor( CmdStart ),
		functor( CmdEnd ),
		functor( ConnectionlessPacket ),
		functor( GetHullBounds ),
		functor( CreateInstancedBaselines ),
		functor( InconsistentFile ),
		functor( AllowLagCompensation ),
	};

	return dllFunctions;
}

void GameDLLInit()
{
	Wrapper::Log::DebugInterfaceLog( "GameDLLInit started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnGameInit();
	Wrapper::Log::DebugInterfaceLog( "GameDLLInit ended" );
}

int DispatchSpawn( edict_t* pent )
{
	Wrapper::Log::DebugInterfaceLog( "DispatchSpawn started" );
	return Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnSpawn( pent );
	Wrapper::Log::DebugInterfaceLog( "DispatchSpawn ended" );
}

void DispatchKeyValue( edict_t* pentKeyvalue, KeyValueData* pkvd )
{
	Wrapper::Log::DebugInterfaceLog( "DispatchKeyValue started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnKeyValue( pentKeyvalue, pkvd );
	Wrapper::Log::DebugInterfaceLog( "DispatchKeyValue ended" );
}

void DispatchTouch( edict_t* pentTouched, edict_t* pentOther )
{
	Wrapper::Log::DebugInterfaceLog( "DispatchTouch started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnTouch( pentTouched, pentOther );
	Wrapper::Log::DebugInterfaceLog( "DispatchTouch ended" );
}

void DispatchUse( edict_t* pentUsed, edict_t* pentOther )
{
	Wrapper::Log::DebugInterfaceLog( "DispatchUse started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnUse( pentUsed, pentOther );
	Wrapper::Log::DebugInterfaceLog( "DispatchUse ended" );
}

void DispatchThink( edict_t* pent )
{
	Wrapper::Log::DebugInterfaceLog( "DispatchThink started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnThink( pent );
	Wrapper::Log::DebugInterfaceLog( "DispatchThink ended" );
}

void DispatchBlocked( edict_t* pentBlocked, edict_t* pentOther )
{
	Wrapper::Log::DebugInterfaceLog( "DispatchBlocked started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnBlocked( pentBlocked, pentOther );
	Wrapper::Log::DebugInterfaceLog( "DispatchBlocked ended" );
}

void DispatchSave( edict_t* pent, SAVERESTOREDATA* pSaveData )
{
	Wrapper::Log::DebugInterfaceLog( "DispatchSave started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnSave( pent, pSaveData );
	Wrapper::Log::DebugInterfaceLog( "DispatchSave ended" );
}

int DispatchRestore( edict_t* pent, SAVERESTOREDATA* pSaveData, int globalEntity )
{
	Wrapper::Log::DebugInterfaceLog( "DispatchRestore started" );
	return Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnRestore( pent, pSaveData, globalEntity );
	Wrapper::Log::DebugInterfaceLog( "DispatchRestore ended" );
}

void DispatchObjectCollisionBox( edict_t* pent )
{
	Wrapper::Log::DebugInterfaceLog( "DispatchObjectCollisionBox started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnSetAbsBox( pent );
	Wrapper::Log::DebugInterfaceLog( "DispatchObjectCollisionBox ended" );
}

void SaveWriteFields( SAVERESTOREDATA* pSaveData, const char* pname, void* pBaseData, TYPEDESCRIPTION* pFields, int fieldCount )
{
	Wrapper::Log::DebugInterfaceLog( "GameDLLInit started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnSaveWriteFields( pSaveData, pname, pBaseData, pFields, fieldCount );
	Wrapper::Log::DebugInterfaceLog( "GameDLLInit ended" );
}

void SaveReadFields( SAVERESTOREDATA* pSaveData, const char* pname, void* pBaseData, TYPEDESCRIPTION* pFields, int fieldCount )
{
	Wrapper::Log::DebugInterfaceLog( "SaveReadFields started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnSaveReadFields( pSaveData, pname, pBaseData, pFields, fieldCount );
	Wrapper::Log::DebugInterfaceLog( "SaveReadFields ended" );
}

void SaveGlobalState( SAVERESTOREDATA* pSaveData )
{
	Wrapper::Log::DebugInterfaceLog( "SaveGlobalState started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnSaveGlobalState( pSaveData );
	Wrapper::Log::DebugInterfaceLog( "SaveGlobalState ended" );
}

void RestoreGlobalState( SAVERESTOREDATA* pSaveData )
{
	Wrapper::Log::DebugInterfaceLog( "RestoreGlobalState started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnRestoreGlobalState( pSaveData );
	Wrapper::Log::DebugInterfaceLog( "RestoreGlobalState ended" );
}

void ResetGlobalState()
{
	Wrapper::Log::DebugInterfaceLog( "ResetGlobalState started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnResetGlobalState();
	Wrapper::Log::DebugInterfaceLog( "ResetGlobalState ended" );
}

BOOL ClientConnect( edict_t* pEntity, const char* pszName, const char* pszAddress, char szRejectReason[ 128 ] )
{
	Wrapper::Log::DebugInterfaceLog( "ClientConnect started" );
	return Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnClientConnect( pEntity, pszName, pszAddress, szRejectReason );
	Wrapper::Log::DebugInterfaceLog( "ClientConnect ended" );
}

void ClientDisconnect( edict_t* pEntity )
{
	Wrapper::Log::DebugInterfaceLog( "ClientDisconnect started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnClientDisconnect( pEntity );
	Wrapper::Log::DebugInterfaceLog( "ClientDisconnect ended" );
}

void ClientKill( edict_t* pEntity )
{
	Wrapper::Log::DebugInterfaceLog( "ClientKill started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnClientKill( pEntity );
	Wrapper::Log::DebugInterfaceLog( "ClientKill ended" );
}

void ClientPutInServer( edict_t* pEntity )
{
	Wrapper::Log::DebugInterfaceLog( "ClientPutInServer started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnClientPutInServer( pEntity );
	Wrapper::Log::DebugInterfaceLog( "ClientPutInServer ended" );
}

void ClientCommand( edict_t* pEntity )
{
	Wrapper::Log::DebugInterfaceLog( "ClientCommand started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnClientCommand( pEntity );
	Wrapper::Log::DebugInterfaceLog( "ClientCommand ended" );
}

void ClientUserInfoChanged( edict_t* pEntity, char* infobuffer )
{
	Wrapper::Log::DebugInterfaceLog( "ClientUserInfoChanged started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnClientUserInfoChanged( pEntity, infobuffer );
	Wrapper::Log::DebugInterfaceLog( "ClientUserInfoChanged ended" );
}

void ServerActivate( edict_t* pEdictList, int edictCount, int clientMax )
{
	Wrapper::Log::DebugInterfaceLog( "ServerActivate started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnServerActivate( pEdictList, edictCount, clientMax );
	Wrapper::Log::DebugInterfaceLog( "ServerActivate ended" );
}

void ServerDeactivate()
{
	Wrapper::Log::DebugInterfaceLog( "ServerDeactivate started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnServerDeactivate();
	Wrapper::Log::DebugInterfaceLog( "ServerDeactivate ended" );
}

void StartFrame()
{
	Wrapper::Log::DebugInterfaceLog( "StartFrame started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnStartFrame();
	Wrapper::Log::DebugInterfaceLog( "StartFrame ended" );
}

void PlayerPreThink( edict_t* pEntity )
{
	Wrapper::Log::DebugInterfaceLog( "PlayerPreThink started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnPlayerPreThink( pEntity );
	Wrapper::Log::DebugInterfaceLog( "PlayerPreThink ended" );
}

void PlayerPostThink( edict_t* pEntity )
{
	Wrapper::Log::DebugInterfaceLog( "PlayerPostThink started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnPlayerPostThink( pEntity );
	Wrapper::Log::DebugInterfaceLog( "PlayerPostThink ended" );
}

void ParmsNewLevel()
{
	Wrapper::Log::DebugInterfaceLog( "ParmsNewLevel started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnParmsNewLevel();
	Wrapper::Log::DebugInterfaceLog( "ParmsNewLevel ended" );
}

void ParmsChangeLevel()
{
	Wrapper::Log::DebugInterfaceLog( "ParmsChangeLevel started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnParmsChangeLevel();
	Wrapper::Log::DebugInterfaceLog( "ParmsChangeLevel ended" );
}

const char* GetGameDescription()
{
	Wrapper::Log::DebugInterfaceLog( "GetGameDescription started" );

	//Store the description locally so we can free the marshalled memory and avoid leaks
	static std::string szDescription;

	auto pszDescription = Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnGetGameDescription();

	szDescription = pszDescription;

	Wrapper::CManagedServer::Instance().FreeMarshalledString( pszDescription );

	Wrapper::Log::DebugInterfaceLog( "GetGameDescription ended" );

	return szDescription.c_str();
}

void PlayerCustomization( edict_t* pEntity, customization_t* pCust )
{
	Wrapper::Log::DebugInterfaceLog( "PlayerCustomization started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnPlayerCustomization( pEntity, pCust );
	Wrapper::Log::DebugInterfaceLog( "PlayerCustomization ended" );
}

void SpectatorConnect( edict_t* pEntity )
{
	Wrapper::Log::DebugInterfaceLog( "SpectatorConnect started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnSpectatorConnect( pEntity );
	Wrapper::Log::DebugInterfaceLog( "SpectatorConnect ended" );
}

void SpectatorDisconnect( edict_t* pEntity )
{
	Wrapper::Log::DebugInterfaceLog( "SpectatorDisconnect started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnSpectatorDisconnect( pEntity );
	Wrapper::Log::DebugInterfaceLog( "SpectatorDisconnect ended" );
}

void SpectatorThink( edict_t* pEntity )
{
	Wrapper::Log::DebugInterfaceLog( "SpectatorThink started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnSpectatorThink( pEntity );
	Wrapper::Log::DebugInterfaceLog( "SpectatorThink ended" );
}

void Sys_Error( const char* error_string )
{
	Wrapper::Log::DebugInterfaceLog( "Sys_Error started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnSys_Error( error_string );
	Wrapper::Log::DebugInterfaceLog( "Sys_Error ended" );
}

void PM_Init( playermove_t* ppmove )
{
	Wrapper::Log::DebugInterfaceLog( "PM_Init started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnPM_Init( ppmove );
	Wrapper::Log::DebugInterfaceLog( "PM_Init ended" );
}

void PM_Move( playermove_t* ppmove, int server )
{
	Wrapper::Log::DebugInterfaceLog( "PM_Move started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnPM_Move( ppmove, server );
	Wrapper::Log::DebugInterfaceLog( "PM_Move ended" );
}

char PM_FindTextureType( char* name )
{
	Wrapper::Log::DebugInterfaceLog( "PM_FindTextureType started" );
	return Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnPM_FindTextureType( name );
	Wrapper::Log::DebugInterfaceLog( "PM_FindTextureType ended" );
}

void SetupVisibility( edict_t* pViewEntity, edict_t* pClient, unsigned char** pvs, unsigned char** pas )
{
	Wrapper::Log::DebugInterfaceLog( "SetupVisibility started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnSetupVisibility( pViewEntity, pClient, pvs, pas );
	Wrapper::Log::DebugInterfaceLog( "SetupVisibility ended" );
}

void UpdateClientData( const edict_t* ent, int sendweapons, clientdata_t* cd )
{
	Wrapper::Log::DebugInterfaceLog( "UpdateClientData started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnUpdateClientData( ent, sendweapons, cd );
	Wrapper::Log::DebugInterfaceLog( "UpdateClientData ended" );
}

int AddToFullPack( entity_state_t* state, int e, edict_t* ent, edict_t* host, int hostflags, int player, unsigned char* pSet )
{
	memset( state, 0, sizeof( *state ) );
	Wrapper::Log::DebugInterfaceLog( "AddToFullPack started" );
	return Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnAddToFullPack( state, e, ent, host, hostflags, player, pSet );
	Wrapper::Log::DebugInterfaceLog( "AddToFullPack ended" );
}

void CreateBaseline( int player, int eindex, entity_state_t* baseline, edict_t* entity, int playermodelindex, vec3_t player_mins, vec3_t player_maxs )
{
	Wrapper::Log::DebugInterfaceLog( "CreateBaseline started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnCreateBaseline( player, eindex, baseline, entity, playermodelindex, player_mins, player_maxs );
	Wrapper::Log::DebugInterfaceLog( "CreateBaseline ended" );
}

void RegisterEncoders()
{
	Wrapper::Log::DebugInterfaceLog( "RegisterEncoders started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnRegisterEncoders();
	Wrapper::Log::DebugInterfaceLog( "RegisterEncoders ended" );
}

int GetWeaponData( edict_t* player, weapon_data_t* info )
{
	Wrapper::Log::DebugInterfaceLog( "GetWeaponData started" );
	return Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnGetWeaponData( player, info );
	Wrapper::Log::DebugInterfaceLog( "GetWeaponData ended" );
}

void CmdStart( const edict_t* player, const struct usercmd_s* cmd, unsigned int random_seed )
{
	Wrapper::Log::DebugInterfaceLog( "CmdStart started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnCmdStart( player, cmd, random_seed );
	Wrapper::Log::DebugInterfaceLog( "CmdStart ended" );
}

void CmdEnd( const edict_t* player )
{
	Wrapper::Log::DebugInterfaceLog( "CmdEnd started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnCmdEnd( player );
	Wrapper::Log::DebugInterfaceLog( "CmdEnd ended" );
}

int	ConnectionlessPacket( const netadr_t* net_from, const char* args, char* response_buffer, int* response_buffer_size )
{
	Wrapper::Log::DebugInterfaceLog( "ConnectionlessPacket started" );
	return Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnConnectionlessPacket( net_from, args, response_buffer, response_buffer_size );
	Wrapper::Log::DebugInterfaceLog( "ConnectionlessPacket ended" );
}

int GetHullBounds( int hullnumber, float* mins, float* maxs )
{
	Wrapper::Log::DebugInterfaceLog( "GetHullBounds started" );
	return Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnGetHullBounds( hullnumber, mins, maxs );
	Wrapper::Log::DebugInterfaceLog( "GetHullBounds ended" );
}

void CreateInstancedBaselines()
{
	Wrapper::Log::DebugInterfaceLog( "CreateInstancedBaselines started" );
	Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnCreateInstancedBaselines();
	Wrapper::Log::DebugInterfaceLog( "CreateInstancedBaselines ended" );
}

int	InconsistentFile( const edict_t* player, const char* filename, char* disconnect_DebugInterfaceLog )
{
	Wrapper::Log::DebugInterfaceLog( "InconsistentFile started" );
	return Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnInconsistentFile( player, filename, disconnect_DebugInterfaceLog );
	Wrapper::Log::DebugInterfaceLog( "InconsistentFile ended" );
}

int AllowLagCompensation()
{
	Wrapper::Log::DebugInterfaceLog( "AllowLagCompensation started" );
	return Wrapper::CManagedServer::Instance().GetManagedDLLFunctions()->pfnAllowLagCompensation();
	Wrapper::Log::DebugInterfaceLog( "AllowLagCompensation ended" );
}
