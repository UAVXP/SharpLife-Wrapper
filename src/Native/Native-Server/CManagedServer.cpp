#include "CManagedServer.h"
#include "ConfigurationInput.h"
#include "DLLInterface.h"
#include "interface.h"
#include "Log.h"
#include "NewDLLInterface.h"
#include "ServerManagedInterface.h"
#include "Utility/InterfaceUtils.h"
#include "Utility/StringUtils.h"

namespace Wrapper
{
CManagedServer::CManagedServer() = default;
CManagedServer::~CManagedServer() = default;

CManagedServer& CManagedServer::Instance()
{
	static CManagedServer instance;

	return instance;
}

std::string CManagedServer::GetGameDirectory() const
{
	char szGameDir[ MAX_PATH ];

	m_EngineFuncs.pfnGetGameDir( szGameDir );

	return szGameDir;
}

bool CManagedServer::Initialize( const enginefuncs_t& engfuncsFromEngine, globalvars_t* pGlobalvars )
{
	memcpy( &m_EngineFuncs, &engfuncsFromEngine, sizeof( enginefuncs_t ) );
	m_pGlobals = pGlobalvars;

	if( !LoadConfiguration() )
	{
		Log::Message( "Couldn't load configuration file" );
		return false;
	}

	Log::SetDebugInterfaceLoggingEnabled( GetConfiguration().EnableDebugInterface );

	Log::Message( "StartManagedHost" );

	if( !StartManagedHost() )
	{
		Log::Message( "Couldn't initialize managed environment" );
		return false;
	}

	m_DLLFunctions.SharpLife = CreateDLLFunctionsInterface( GetConfiguration() );
	m_NewDLLFunctions.SharpLife = CreateNewDLLFunctionsInterface( GetConfiguration() );
	m_EngineOverrides.SharpLife = CreateEngineOverridesInterface( GetConfiguration() );

	Log::Message( "LoadManagedWrapper" );

	if( !LoadManagedWrapper() )
	{
		Log::Message( "Couldn't load managed library" );
		return false;
	}

	Log::Message( "pfnGiveFnptrsToDll" );

	if( !m_pManagedAPI->pfnGiveFnptrsToDll( &m_EngineFuncs, m_pGlobals ) )
	{
		Log::Message( "Failed to initialize managed API" );
		return false;
	}

	m_DLLFunctions.Managed = nullptr;

	Log::Message( "pfnGetEntityAPI" );

	if( !m_pManagedAPI->pfnGetEntityAPI( &m_DLLFunctions.Managed ) )
	{
		Log::Message( "Couldn't get managed Entity API" );
		return false;
	}

	if( !Utility::ValidateFunctionTable( *m_DLLFunctions.Managed ) )
	{
		Log::Message( "Managed Entity API is missing one or more functions" );
		return false;
	}

	m_NewDLLFunctions.Managed = nullptr;

	Log::Message( "pfnGetNewDLLFunctions" );

	if( !m_pManagedAPI->pfnGetNewDLLFunctions( &m_NewDLLFunctions.Managed ) )
	{
		Log::Message( "Couldn't get managed New Entity API" );
		return false;
	}

	if( !Utility::ValidateFunctionTable( *m_NewDLLFunctions.Managed ) )
	{
		Log::Message( "Managed New Entity API is missing one or more functions" );
		return false;
	}

	if( !m_pManagedAPI->pfnGetEngineOverrides( &m_EngineOverrides.Managed ) )
	{
		Log::Message( "Couldn't get managed Engine Overrides API" );
		return false;
	}

	if( !Utility::ValidateFunctionTable( *m_EngineOverrides.Managed ) )
	{
		Log::Message( "Managed Engine Overrides API is missing one or more functions" );
		return false;
	}

	Utility::SpliceFunctionTables( *m_DLLFunctions.Managed, m_DLLFunctions.SharpLife, m_DLLFunctions.ExportedToEngine );
	Utility::SpliceFunctionTables( *m_NewDLLFunctions.Managed, m_NewDLLFunctions.SharpLife, m_NewDLLFunctions.ExportedToEngine );
	Utility::SpliceFunctionTables( *m_EngineOverrides.Managed, m_EngineOverrides.SharpLife, m_EngineOverrides.ExportedToEngine );

	//Enable our detours
	AdjustEngineAddresses( GetConfiguration(), m_pGlobals );

	if( !CreateDetours( GetConfiguration(), GetDetours() ) )
	{
		Log::Message( "Couldn't create all detours" );
		return false;
	}

	//Enable all detours
	for( const auto& detour : GetDetours() )
	{
		detour->EnableDetour();
	}

	return true;
}

void CManagedServer::Shutdown()
{
	GetDetours().clear();

	ShutdownManagedWrapper();
	ShutdownManagedHost();
}

void CManagedServer::FreeMarshalledString( const char* pszString )
{
	if( m_pManagedAPI )
	{
		m_pManagedAPI->pfnFreeMemory( ServerManagedAPI::MemoryType::String, const_cast<char*>( pszString ) );
	}
}

bool CManagedServer::LoadManagedWrapper()
{
	Log::Message( "Loading library %s", GetConfiguration().Server.EntryPoint.AssemblyName.c_str() );
	Log::Message( "Entry point is %s.%s", GetConfiguration().Server.EntryPoint.Class.c_str(), GetConfiguration().Server.EntryPoint.Method.c_str() );

	auto managedAPI = LoadServerManagedLibrary( GetConfiguration().Server.EntryPoint, GetCLRHost() );

	if( managedAPI )
	{
		m_pManagedAPI = managedAPI.value();
	}

	return managedAPI.has_value();
}

void CManagedServer::ShutdownManagedWrapper()
{
	if( nullptr != m_pManagedAPI )
	{
		//Free the memory that was allocated for the api structures
		if( nullptr != m_NewDLLFunctions.Managed )
		{
			m_pManagedAPI->pfnFreeMemory( ServerManagedAPI::MemoryType::NewEntityAPI, m_NewDLLFunctions.Managed );
			m_NewDLLFunctions.Managed = nullptr;
		}

		if( nullptr != m_DLLFunctions.Managed )
		{
			m_pManagedAPI->pfnFreeMemory( ServerManagedAPI::MemoryType::EntityAPI, m_DLLFunctions.Managed );
			m_DLLFunctions.Managed = nullptr;
		}

		m_pManagedAPI->pfnFreeMemory( ServerManagedAPI::MemoryType::ManagedAPI, m_pManagedAPI );
		m_pManagedAPI = nullptr;
	}
}
}
