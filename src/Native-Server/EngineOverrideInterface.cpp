#include "CDetour/detours.h"
#include "CManagedServer.h"
#include "EngineOverrideInterface.h"
#include "Log.h"
#include "Utility/InterfaceUtils.h"

using namespace Wrapper::Utility;

namespace Wrapper
{
DETOUR_DECL_STATIC1( ED_LoadFromFile, void, char*, data )
{
	CManagedServer::Instance().GetExportedEngineOverrideFunctions()->pfnED_LoadFromFile( data );
}

#define CREATE_DETOUR_OVERRIDE( name )											\
{																				\
	auto it = configuration.EngineOverrides.find( #name );						\
																				\
	if( it != configuration.EngineOverrides.end() )								\
	{																			\
		detours.emplace_back(													\
			std::unique_ptr<CDetour, DetourDestructor>{							\
			DETOUR_CREATE_STATIC_FIXED( name, it->second )						\
		}																		\
		);																		\
	}																			\
	else																		\
	{																			\
		Log::Message( "Couldn't get engine override address \"%s\"", #name );	\
		return false;															\
	}																			\
}

bool CreateDetours( const CConfiguration& configuration, std::vector<std::unique_ptr<CDetour, DetourDestructor>>& detours )
{
	CREATE_DETOUR_OVERRIDE( ED_LoadFromFile )

	return true;
}

EngineOverrides CreateEngineOverridesInterface( const CConfiguration& configuration )
{
	ProcessSharpLifeInterfaceFunctor functor{ configuration.EnableDebugInterface };

	//This is useful to test if our interface works properly
	EngineOverrides pFunctionTable = 
	{
		functor( Overrides::ED_LoadFromFile )
	};

	return pFunctionTable;
}

namespace Overrides
{
void ED_LoadFromFile( char* data )
{
	Log::DebugInterfaceLog( "ED_LoadFromFile started" );
	CManagedServer::Instance().GetManagedEngineOverrideFunctions()->pfnED_LoadFromFile( data );
	Log::DebugInterfaceLog( "ED_LoadFromFile ended" );
}
}
}
