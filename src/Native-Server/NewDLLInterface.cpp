#include "CConfiguration.h"
#include "CManagedServer.h"
#include "Log.h"
#include "NewDLLInterface.h"
#include "Utility/InterfaceUtils.h"

using namespace Wrapper::Utility;

NEW_DLL_FUNCTIONS CreateNewDLLFunctionsInterface( const Wrapper::CConfiguration& configuration )
{
	ProcessSharpLifeInterfaceFunctor functor{ configuration.EnableDebugInterface };

	NEW_DLL_FUNCTIONS newDLLFunctions =
	{
		functor( OnFreeEntPrivateData, configuration.EnableDebugInterface ),
		//SharpLife needs to know about shutdown
		functor( GameDLLShutdown, true ),
		functor( ShouldCollide ),
		functor( CvarValue ),
		functor( CvarValue2 )
	};

	return newDLLFunctions;
}

void OnFreeEntPrivateData( edict_t* pEdict )
{
	Wrapper::Log::DebugInterfaceLog( "OnFreeEntPrivateData started" );
	Wrapper::CManagedServer::Instance().GetManagedNewDLLFunctions()->pfnOnFreeEntPrivateData( pEdict );
	Wrapper::Log::DebugInterfaceLog( "OnFreeEntPrivateData ended" );
}

void GameDLLShutdown()
{
	Wrapper::Log::DebugInterfaceLog( "GameDLLShutdown started" );
	Wrapper::CManagedServer::Instance().GetManagedNewDLLFunctions()->pfnGameShutdown();

	Wrapper::CManagedServer::Instance().Shutdown();
	Wrapper::Log::DebugInterfaceLog( "GameDLLShutdown ended" );
}

int ShouldCollide( edict_t* pentTouched, edict_t* pentOther )
{
	Wrapper::Log::DebugInterfaceLog( "ShouldCollide started" );
	return Wrapper::CManagedServer::Instance().GetManagedNewDLLFunctions()->pfnShouldCollide( pentTouched, pentOther );
	Wrapper::Log::DebugInterfaceLog( "ShouldCollide ended" );
}

void CvarValue( const edict_t* pEnt, const char* value )
{
	Wrapper::Log::DebugInterfaceLog( "CvarValue started" );
	Wrapper::CManagedServer::Instance().GetManagedNewDLLFunctions()->pfnCvarValue( pEnt, value );
	Wrapper::Log::DebugInterfaceLog( "CvarValue ended" );
}

void CvarValue2( const edict_t* pEnt, int requestID, const char* cvarName, const char* value )
{
	Wrapper::Log::DebugInterfaceLog( "CvarValue2 started" );
	Wrapper::CManagedServer::Instance().GetManagedNewDLLFunctions()->pfnCvarValue2( pEnt, requestID, cvarName, value );
	Wrapper::Log::DebugInterfaceLog( "CvarValue2 ended" );
}
