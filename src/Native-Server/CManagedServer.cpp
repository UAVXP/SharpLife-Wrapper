#include "CLR/CCLRHostException.h"
#include "CManagedServer.h"
#include "ConfigurationInput.h"
#include "DLLInterface.h"
#include "Log.h"
#include "ManagedInterface.h"
#include "NewDLLInterface.h"
#include "Utility/InterfaceUtils.h"
#include "Utility/StringUtils.h"

namespace Wrapper
{
const std::string_view CManagedServer::CONFIG_FILENAME{ "cfg/SharpLife-Wrapper-Native.ini" };
const std::wstring_view CManagedServer::WRAPPER_DIRECTORY{ L"dlls" };

CManagedServer::CManagedServer() = default;

CManagedServer::~CManagedServer()
{
	//If we've reached this point and there are still detours in the list, then we're crashing
	//Detours can't patch the engine at this point, so just leak them instead so they don't trigger the patch
	for( auto& detour : m_Detours )
	{
		detour.release();
	}
}

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

std::wstring CManagedServer::GetWideGameDirectory() const
{
	char szGameDir[ MAX_PATH ];

	m_EngineFuncs.pfnGetGameDir( szGameDir );

	return Utility::ToWideString( szGameDir );
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

	Log::SetDebugInterfaceLoggingEnabled( m_Configuration.EnableDebugInterface );

	Log::Message( "StartManagedHost" );

	if( !StartManagedHost() )
	{
		Log::Message( "Couldn't initialize managed environment" );
		return false;
	}

	m_DLLFunctions.SharpLife = CreateDLLFunctionsInterface( m_Configuration );
	m_NewDLLFunctions.SharpLife = CreateNewDLLFunctionsInterface( m_Configuration );
	m_EngineOverrides.SharpLife = CreateEngineOverridesInterface( m_Configuration );

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
	AdjustEngineAddresses( m_Configuration, m_pGlobals );

	if( !CreateDetours( m_Configuration, m_Detours ) )
	{
		Log::Message( "Couldn't create all detours" );
		return false;
	}

	//Enable all detours
	for( const auto& detour : m_Detours )
	{
		detour->EnableDetour();
	}

	return true;
}

void CManagedServer::Shutdown()
{
	m_Detours.clear();

	ShutdownManagedWrapper();
	ShutdownManagedHost();
}

void CManagedServer::FreeMarshalledString( const char* pszString )
{
	if( m_pManagedAPI )
	{
		m_pManagedAPI->pfnFreeMemory( ManagedAPI::MemoryType::String, const_cast<char*>( pszString ) );
	}
}

bool CManagedServer::LoadConfiguration()
{
	auto config = Wrapper::LoadConfiguration( GetGameDirectory() + '/' + std::string{ CONFIG_FILENAME } );

	if( config )
	{
		m_Configuration = std::move( config.value() );
		return true;
	}

	return false;
}

bool CManagedServer::StartManagedHost()
{
	auto dllsPath = GetWideGameDirectory() + L'/' + std::wstring{ WRAPPER_DIRECTORY };

	dllsPath = Utility::GetAbsolutePath( dllsPath );

	try
	{
		m_CLRHost = std::make_unique<CLR::CCLRHost>( dllsPath, m_Configuration.SupportedDotNetCoreVersions );
	}
	catch( const CLR::CCLRHostException e )
	{
		if( e.HasResultCode() )
		{
			Log::Message( "ERROR - %s\nError code:%x", e.what(), e.GetResultCode() );
		}
		else
		{
			Log::Message( "ERROR - %s", e.what() );
		}

		return false;
	}

	return true;
}

void CManagedServer::ShutdownManagedHost()
{
	m_CLRHost.release();
}

bool CManagedServer::LoadManagedWrapper()
{
	Log::Message( "Loading library %s", m_Configuration.ManagedAssemblyName.c_str() );
	Log::Message( "Entry point is %s.%s", m_Configuration.ManagedEntryPointClass.c_str(), m_Configuration.ManagedEntryPointMethod.c_str() );

	auto managedAPI = LoadManagedLibrary( m_Configuration, *m_CLRHost );

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
			m_pManagedAPI->pfnFreeMemory( ManagedAPI::MemoryType::NewEntityAPI, m_NewDLLFunctions.Managed );
			m_NewDLLFunctions.Managed = nullptr;
		}

		if( nullptr != m_DLLFunctions.Managed )
		{
			m_pManagedAPI->pfnFreeMemory( ManagedAPI::MemoryType::EntityAPI, m_DLLFunctions.Managed );
			m_DLLFunctions.Managed = nullptr;
		}

		m_pManagedAPI->pfnFreeMemory( ManagedAPI::MemoryType::ManagedAPI, m_pManagedAPI );
		m_pManagedAPI = nullptr;
	}
}
}
