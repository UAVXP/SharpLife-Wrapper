#include <cstdlib>
#include <cstring>
#include <string>

#include "CManagedServer.h"
#include "Dlls/extdll.h"
#include "Log.h"
#include "ServerEngineInterface.h"

#ifdef _WIN32
// Required DLL entry point
BOOL WINAPI DllMain(
	HINSTANCE hinstDLL,
	DWORD fdwReason,
	LPVOID lpvReserved )
{
	if( fdwReason == DLL_PROCESS_ATTACH )
	{
	}
	else if( fdwReason == DLL_PROCESS_DETACH )
	{
	}
	return TRUE;
}
#endif

//Export the function as an ordinal so the engine can get at it
//Required because the function is a stdcall function, and can't be dllexported directly
#if defined( _WIN32 ) && !defined( __GNUC__ ) && defined ( _MSC_VER )
#pragma comment( linker, "/EXPORT:GiveFnptrsToDll=_GiveFnptrsToDll@8,@1" )
#pragma comment( linker, "/SECTION:.data,RW" )
#endif

extern "C"
{
void GIVEFNPTRSTODLL_DLLEXPORT GiveFnptrsToDll( enginefuncs_t* pengfuncsFromEngine, globalvars_t* pGlobals )
{
	if( !Wrapper::CManagedServer::Instance().Initialize( *pengfuncsFromEngine, pGlobals ) )
	{
		Wrapper::Log::Message( "Error initializing managed system, exiting" );
		std::exit( 1 );
	}
}

int GetEntityAPI( DLL_FUNCTIONS* pFunctionTable, int interfaceVersion )
{
	if( !pFunctionTable || interfaceVersion != INTERFACE_VERSION )
	{
		return false;
	}

	memcpy( pFunctionTable, Wrapper::CManagedServer::Instance().GetExportedDLLFunctions(), sizeof( DLL_FUNCTIONS ) );
	return true;
}

int GetEntityAPI2( DLL_FUNCTIONS* pFunctionTable, int* interfaceVersion )
{
	if( !pFunctionTable || *interfaceVersion != INTERFACE_VERSION )
	{
		// Tell engine what version we had, so it can figure out who is out of date.
		*interfaceVersion = INTERFACE_VERSION;
		return false;
	}

	memcpy( pFunctionTable, Wrapper::CManagedServer::Instance().GetExportedDLLFunctions(), sizeof( DLL_FUNCTIONS ) );
	return true;
}

int GetNewDLLFunctions( NEW_DLL_FUNCTIONS* pFunctionTable, int* interfaceVersion )
{
	if( !pFunctionTable || *interfaceVersion != NEW_DLL_FUNCTIONS_VERSION )
	{
		*interfaceVersion = NEW_DLL_FUNCTIONS_VERSION;
		return false;
	}

	memcpy( pFunctionTable, Wrapper::CManagedServer::Instance().GetExportedNewDLLFunctions(), sizeof( NEW_DLL_FUNCTIONS ) );
	return true;
}

void custom( entvars_t* pev )
{
	//This function is used to spawn all entities on map load
	//The game can do this without needing to forward this explicitly,
	//it knows it has to construct entities in IServerEntities.KeyValue
	//This function must remain so the engine sees it and assumes the game created the entity
	//See IServerEntities.KeyValue implementation for more information
}
}
