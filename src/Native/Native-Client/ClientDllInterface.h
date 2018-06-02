#ifndef WRAPPER_CLIENTDLLINTERFACE_H
#define WRAPPER_CLIENTDLLINTERFACE_H

#include "Dlls/extdll.h"
#include "Engine/APIProxy.h"

namespace Wrapper
{
class CConfiguration;

cldll_func_t CreateClientDllInterface( const CConfiguration& configuration );

extern "C"
{
void WRAPPER_DLLEXPORT F( cldll_func_t* pcldll_func );

int Initialize( cl_enginefunc_t* pEnginefuncs, int iVersion );

void HUD_Init();
int HUD_VidInit();
int HUD_Redraw( float time, int intermission );
int HUD_UpdateClientData( struct client_data_s* pcldata, float flTime );
void HUD_Reset();
void HUD_ClientMove( struct playermove_s* ppmove, qboolean server );
void HUD_ClientMoveInit( struct playermove_s* ppmove );
char HUD_TextureType( char* name );
void HUD_In_ActivateMouse();
void HUD_In_DeactivateMouse();
void HUD_In_MouseEvent( int mstate );
void HUD_IN_ClearStates();
void HUD_In_Accumulate();
void HUD_CL_CreateMove( float frametime, struct usercmd_s* cmd, int active );
int HUD_CL_IsThirdPerson();
void HUD_CL_GetCameraOffsets( Vector& ofs );
struct kbutton_s* HUD_KB_Find( const char* name );
void HUD_CamThink();
void HUD_CalcRef( struct ref_params_s* pparams );
int HUD_AddEntity( int type, struct cl_entity_s* ent, const char* modelname );
void HUD_CreateEntities();
void HUD_DrawNormalTriangles();
void HUD_DrawTransparentTriangles();
void HUD_StudioEvent( const struct mstudioevent_s* event, const struct cl_entity_s* entity );
void HUD_PostRunCmd( struct local_state_s* from, struct local_state_s* to, struct usercmd_s* cmd, int runfuncs, double time, unsigned int random_seed );
void Shutdown();
void HUD_TXFerLocalOverrides( struct entity_state_s* state, const struct clientdata_s* client );
void HUD_ProcessPlayerState( struct entity_state_s* dst, const struct entity_state_s* src );
void HUD_TXFerPredictionData( struct entity_state_s* ps, const struct entity_state_s* pps, struct clientdata_s* pcd, const struct clientdata_s* ppcd, struct weapon_data_s* wd, const struct weapon_data_s* pwd );
void HUD_DemoRead( int size, unsigned char* buffer );
int HUD_ConnectionlessPacket( const struct netadr_s* net_from, const char* args, char* response_buffer, int* response_buffer_size );
int HUD_GetHullBounds( int hullnumber, Vector& mins, Vector& maxs );
void HUD_Frame( double time );
int HUD_KeyEvent( int eventcode, int keynum, const char* pszCurrentBinding );
void HUD_TempEntUpdate( double frametime, double client_time, double cl_gravity, struct tempent_s** ppTempEntFree, struct tempent_s** ppTempEntActive, int( *Callback_AddVisibleEntity )( struct cl_entity_s* pEntity ), void( *Callback_TempEntPlaySound )( struct tempent_s* pTemp, float damp ) );
struct cl_entity_s* HUD_GetUserEntity( int index );
void HUD_VoiceStatus( int entindex, qboolean bTalking );
void HUD_DirectorMessage( int iSize, void* pbuf );
int HUD_StudioInterface( int version, struct r_studio_interface_s** ppinterface, struct engine_studio_api_s* pstudio );
void HUD_ChatInputPosition( int* x, int* y );
int HUD_GetPlayerTeam( int iplayer );
}
}

#endif //WRAPPER_CLIENTDLLINTERFACE_H
