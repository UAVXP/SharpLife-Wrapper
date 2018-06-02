#include <cstdlib>

#include "CConfiguration.h"
#include "ClientDllInterface.h"
#include "CManagedClient.h"
#include "Log.h"
#include "Utility/InterfaceUtils.h"

using namespace Wrapper::Utility;

namespace Wrapper
{
cldll_func_t CreateClientDllInterface( const CConfiguration& configuration )
{
	ProcessSharpLifeInterfaceFunctor functor{ configuration.EnableDebugInterface };

	cldll_func_t functionTable =
	{
		functor( &Initialize, true ),
		functor( &HUD_Init ),
		functor( &HUD_VidInit ),
		functor( &HUD_Redraw ),
		functor( &HUD_UpdateClientData ),
		functor( &HUD_Reset ),
		functor( &HUD_ClientMove ),
		functor( &HUD_ClientMoveInit ),
		functor( &HUD_TextureType ),
		functor( &HUD_In_ActivateMouse ),
		functor( &HUD_In_DeactivateMouse ),
		functor( &HUD_In_MouseEvent ),
		functor( &HUD_IN_ClearStates ),
		functor( &HUD_In_Accumulate ),
		functor( &HUD_CL_CreateMove ),
		functor( &HUD_CL_IsThirdPerson ),
		functor( &HUD_CL_GetCameraOffsets ),
		functor( &HUD_KB_Find ),
		functor( &HUD_CamThink ),
		functor( &HUD_CalcRef ),
		functor( &HUD_AddEntity ),
		functor( &HUD_CreateEntities ),
		functor( &HUD_DrawNormalTriangles ),
		functor( &HUD_DrawTransparentTriangles ),
		functor( &HUD_StudioEvent ),
		functor( &HUD_PostRunCmd ),
		functor( &Shutdown, true ),
		functor( &HUD_TXFerLocalOverrides ),
		functor( &HUD_ProcessPlayerState ),
		functor( &HUD_TXFerPredictionData ),
		functor( &HUD_DemoRead ),
		functor( &HUD_ConnectionlessPacket ),
		functor( &HUD_GetHullBounds ),
		functor( &HUD_Frame ),
		functor( &HUD_KeyEvent ),
		functor( &HUD_TempEntUpdate ),
		functor( &HUD_GetUserEntity ),
		functor( &HUD_VoiceStatus ),
		functor( &HUD_DirectorMessage ),
		functor( &HUD_StudioInterface ),
		functor( &HUD_ChatInputPosition ),
		functor( &HUD_GetPlayerTeam ),
		nullptr
	};

	return functionTable;
}

extern "C"
{
void F( cldll_func_t* pcldll_func )
{
	// Hack!
	//Don't need this
	//g_pcldstAddrs = ( ( cldll_func_dst_t * ) pcldll_func->pHudVidInitFunc );

	CManagedClient::Instance().SetEngineClientDllInterface( pcldll_func );
}

int Initialize( cl_enginefunc_t* pEnginefuncs, int iVersion )
{
	if( iVersion != CLDLL_INTERFACE_VERSION )
	{
		return false;
	}

	auto result = CManagedClient::Instance().Initialize( *pEnginefuncs );

	if( !result )
	{
		std::exit( EXIT_FAILURE );
	}

	return result;
}

void HUD_Init()
{
	Log::DebugInterfaceLog( "HUD_Init started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pHudInitFunc();
	Log::DebugInterfaceLog( "HUD_Init ended" );
}

int HUD_VidInit()
{
	Log::DebugInterfaceLog( "HUD_VidInit started" );
	auto result = CManagedClient::Instance().GetManagedDLLFunctions()->pHudVidInitFunc();
	Log::DebugInterfaceLog( "HUD_VidInit ended" );
	return result;
}

int HUD_Redraw( float time, int intermission )
{
	Log::DebugInterfaceLog( "HUD_Redraw started" );
	auto result = CManagedClient::Instance().GetManagedDLLFunctions()->pHudRedrawFunc( time, intermission );
	Log::DebugInterfaceLog( "HUD_Redraw ended" );
	return result;
}

int HUD_UpdateClientData( struct client_data_s* pcldata, float flTime )
{
	Log::DebugInterfaceLog( "HUD_UpdateClientData started" );
	auto result = CManagedClient::Instance().GetManagedDLLFunctions()->pHudUpdateClientDataFunc( pcldata, flTime );
	Log::DebugInterfaceLog( "HUD_UpdateClientData ended" );
	return result;
}

void HUD_Reset()
{
	Log::DebugInterfaceLog( "HUD_Reset started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pHudResetFunc();
	Log::DebugInterfaceLog( "HUD_Reset ended" );
}

void HUD_ClientMove( struct playermove_s* ppmove, qboolean server )
{
	Log::DebugInterfaceLog( "HUD_ClientMove started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pClientMove( ppmove, server );
	Log::DebugInterfaceLog( "HUD_ClientMove ended" );
}

void HUD_ClientMoveInit( struct playermove_s* ppmove )
{
	Log::DebugInterfaceLog( "HUD_ClientMoveInit started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pClientMoveInit( ppmove );
	Log::DebugInterfaceLog( "HUD_ClientMoveInit ended" );
}

char HUD_TextureType( char* name )
{
	Log::DebugInterfaceLog( "HUD_TextureType started" );
	auto result = CManagedClient::Instance().GetManagedDLLFunctions()->pClientTextureType( name );
	Log::DebugInterfaceLog( "HUD_TextureType ended" );
	return result;
}

void HUD_In_ActivateMouse()
{
	Log::DebugInterfaceLog( "HUD_In_ActivateMouse started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pIN_ActivateMouse();
	Log::DebugInterfaceLog( "HUD_In_ActivateMouse ended" );
}

void HUD_In_DeactivateMouse()
{
	Log::DebugInterfaceLog( "HUD_In_DeactivateMouse started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pIN_DeactivateMouse();
	Log::DebugInterfaceLog( "HUD_In_DeactivateMouse ended" );
}

void HUD_In_MouseEvent( int mstate )
{
	Log::DebugInterfaceLog( "HUD_In_MouseEvents started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pIN_MouseEvent( mstate );
	Log::DebugInterfaceLog( "HUD_In_MouseEvents ended" );
}

void HUD_IN_ClearStates()
{
	Log::DebugInterfaceLog( "HUD_IN_ClearStates started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pIN_ClearStates();
	Log::DebugInterfaceLog( "HUD_IN_ClearStates ended" );
}

void HUD_In_Accumulate()
{
	Log::DebugInterfaceLog( "HUD_In_Accumulate started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pIN_Accumulate();
	Log::DebugInterfaceLog( "HUD_In_Accumulate ended" );
}

void HUD_CL_CreateMove( float frametime, struct usercmd_s* cmd, int active )
{
	Log::DebugInterfaceLog( "HUD_CL_CreateMove started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pCL_CreateMove( frametime, cmd, active );
	Log::DebugInterfaceLog( "HUD_CL_CreateMove ended" );
}

int HUD_CL_IsThirdPerson()
{
	Log::DebugInterfaceLog( "HUD_CL_IsThirdPerson started" );
	auto result = CManagedClient::Instance().GetManagedDLLFunctions()->pCL_IsThirdPerson();
	Log::DebugInterfaceLog( "HUD_CL_IsThirdPerson ended" );
	return result;
}

void HUD_CL_GetCameraOffsets( Vector& ofs )
{
	Log::DebugInterfaceLog( "HUD_CL_GetCameraOffsets started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pCL_GetCameraOffsets( ofs );
	Log::DebugInterfaceLog( "HUD_CL_GetCameraOffsets ended" );
}

struct kbutton_s* HUD_KB_Find( const char* name )
{
	Log::DebugInterfaceLog( "HUD_KB_Find started" );
	auto result = CManagedClient::Instance().GetManagedDLLFunctions()->pFindKey( name );
	Log::DebugInterfaceLog( "HUD_KB_Find ended" );
	return result;
}

void HUD_CamThink()
{
	Log::DebugInterfaceLog( "HUD_CamThink started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pCamThink();
	Log::DebugInterfaceLog( "HUD_CamThink ended" );
}

void HUD_CalcRef( struct ref_params_s* pparams )
{
	Log::DebugInterfaceLog( "HUD_CalcRef started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pCalcRefdef( pparams );
	Log::DebugInterfaceLog( "HUD_CalcRef ended" );
}

int HUD_AddEntity( int type, struct cl_entity_s* ent, const char* modelname )
{
	Log::DebugInterfaceLog( "HUD_AddEntity started" );
	auto result = CManagedClient::Instance().GetManagedDLLFunctions()->pAddEntity( type, ent, modelname );
	Log::DebugInterfaceLog( "HUD_AddEntity ended" );
	return result;
}

void HUD_CreateEntities()
{
	Log::DebugInterfaceLog( "HUD_CreateEntities started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pCreateEntities();
	Log::DebugInterfaceLog( "HUD_CreateEntities ended" );
}

void HUD_DrawNormalTriangles()
{
	Log::DebugInterfaceLog( "HUD_DrawNormalTriangles started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pDrawNormalTriangles();
	Log::DebugInterfaceLog( "HUD_DrawNormalTriangles ended" );
}

void HUD_DrawTransparentTriangles()
{
	Log::DebugInterfaceLog( "HUD_DrawTransparentTriangles started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pDrawTransparentTriangles();
	Log::DebugInterfaceLog( "HUD_DrawTransparentTriangles ended" );
}

void HUD_StudioEvent( const struct mstudioevent_s* event, const struct cl_entity_s* entity )
{
	Log::DebugInterfaceLog( "HUD_StudioEvent started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pStudioEvent( event, entity );
	Log::DebugInterfaceLog( "HUD_StudioEvent ended" );
}

void HUD_PostRunCmd( struct local_state_s* from, struct local_state_s* to, struct usercmd_s* cmd, int runfuncs, double time, unsigned int random_seed )
{
	Log::DebugInterfaceLog( "HUD_PostRunCmd started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pPostRunCmd( from, to, cmd, runfuncs, time, random_seed );
	Log::DebugInterfaceLog( "HUD_PostRunCmd ended" );
}

void Shutdown()
{
	Log::DebugInterfaceLog( "Shutdown started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pShutdown();
	CManagedClient::Instance().Shutdown();
	Log::DebugInterfaceLog( "Shutdown ended" );
}

void HUD_TXFerLocalOverrides( struct entity_state_s* state, const struct clientdata_s* client )
{
	Log::DebugInterfaceLog( "HUD_TXFerLocalOverrides started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pTxferLocalOverrides( state, client );
	Log::DebugInterfaceLog( "HUD_TXFerLocalOverrides ended" );
}

void HUD_ProcessPlayerState( struct entity_state_s* dst, const struct entity_state_s* src )
{
	Log::DebugInterfaceLog( "HUD_ProcessPlayerState started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pProcessPlayerState( dst, src );
	Log::DebugInterfaceLog( "HUD_ProcessPlayerState ended" );
}

void HUD_TXFerPredictionData( struct entity_state_s* ps, const struct entity_state_s* pps, struct clientdata_s* pcd, const struct clientdata_s* ppcd, struct weapon_data_s* wd, const struct weapon_data_s* pwd )
{
	Log::DebugInterfaceLog( "HUD_TXFerPredictionData started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pTxferPredictionData( ps, pps, pcd, ppcd, wd, pwd );
	Log::DebugInterfaceLog( "HUD_TXFerPredictionData ended" );
}

void HUD_DemoRead( int size, unsigned char* buffer )
{
	Log::DebugInterfaceLog( "HUD_DemoRead started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pReadDemoBuffer( size, buffer );
	Log::DebugInterfaceLog( "HUD_DemoRead ended" );
}

int HUD_ConnectionlessPacket( const struct netadr_s* net_from, const char* args, char* response_buffer, int* response_buffer_size )
{
	Log::DebugInterfaceLog( "HUD_ConnectionlessPacket started" );
	auto result = CManagedClient::Instance().GetManagedDLLFunctions()->pConnectionlessPacket( net_from, args, response_buffer, response_buffer_size );
	Log::DebugInterfaceLog( "HUD_ConnectionlessPacket ended" );
	return result;
}

int HUD_GetHullBounds( int hullnumber, Vector& mins, Vector& maxs )
{
	Log::DebugInterfaceLog( "HUD_GetHullBounds started" );
	auto result = CManagedClient::Instance().GetManagedDLLFunctions()->pGetHullBounds( hullnumber, mins, maxs );
	Log::DebugInterfaceLog( "HUD_GetHullBounds ended" );
	return result;
}

void HUD_Frame( double time )
{
	Log::DebugInterfaceLog( "HUD_Frame started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pHudFrame( time );
	Log::DebugInterfaceLog( "HUD_Frame ended" );
}

int HUD_KeyEvent( int eventcode, int keynum, const char* pszCurrentBinding )
{
	Log::DebugInterfaceLog( "HUD_KeyEvent started" );
	auto result = CManagedClient::Instance().GetManagedDLLFunctions()->pKeyEvent( eventcode, keynum, pszCurrentBinding );
	Log::DebugInterfaceLog( "HUD_KeyEvent ended" );
	return result;
}

void HUD_TempEntUpdate( double frametime, double client_time, double cl_gravity, struct tempent_s** ppTempEntFree, struct tempent_s** ppTempEntActive, int( *Callback_AddVisibleEntity )( struct cl_entity_s* pEntity ), void( *Callback_TempEntPlaySound )( struct tempent_s* pTemp, float damp ) )
{
	Log::DebugInterfaceLog( "HUD_TempEntUpdate started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pTempEntUpdate( frametime, client_time, cl_gravity, ppTempEntFree, ppTempEntActive, Callback_AddVisibleEntity, Callback_TempEntPlaySound );
	Log::DebugInterfaceLog( "HUD_TempEntUpdate ended" );
}

struct cl_entity_s* HUD_GetUserEntity( int index )
{
	Log::DebugInterfaceLog( "HUD_GetUserEntity started" );
	auto result = CManagedClient::Instance().GetManagedDLLFunctions()->pGetUserEntity( index );
	Log::DebugInterfaceLog( "HUD_GetUserEntity ended" );
	return result;
}

void HUD_VoiceStatus( int entindex, qboolean bTalking )
{
	Log::DebugInterfaceLog( "HUD_VoiceStatus started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pVoiceStatus( entindex, bTalking );
	Log::DebugInterfaceLog( "HUD_VoiceStatus ended" );
}

void HUD_DirectorMessage( int iSize, void* pbuf )
{
	Log::DebugInterfaceLog( "HUD_DirectorMessage started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pDirectorMessage( iSize, pbuf );
	Log::DebugInterfaceLog( "HUD_DirectorMessage ended" );
}

int HUD_StudioInterface( int version, struct r_studio_interface_s** ppinterface, struct engine_studio_api_s* pstudio )
{
	Log::DebugInterfaceLog( "HUD_StudioInterface started" );
	auto result = CManagedClient::Instance().GetManagedDLLFunctions()->pStudioInterface( version, ppinterface, pstudio );
	Log::DebugInterfaceLog( "HUD_StudioInterface ended" );
	return result;
}

void HUD_ChatInputPosition( int* x, int* y )
{
	Log::DebugInterfaceLog( "HUD_ChatInputPosition started" );
	CManagedClient::Instance().GetManagedDLLFunctions()->pChatInputPosition( x, y );
	Log::DebugInterfaceLog( "HUD_ChatInputPosition ended" );
}

int HUD_GetPlayerTeam( int iplayer )
{
	Log::DebugInterfaceLog( "HUD_GetPlayerTeam started" );
	auto result = CManagedClient::Instance().GetManagedDLLFunctions()->pGetPlayerTeam( iplayer );
	Log::DebugInterfaceLog( "HUD_GetPlayerTeam ended" );
	return result;
}
}
}
