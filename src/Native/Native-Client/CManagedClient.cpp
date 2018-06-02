#include "ClientDllInterface.h"
#include "CManagedClient.h"
#include "ConfigurationInput.h"
#include "Log.h"

namespace Wrapper
{
CManagedClient::CManagedClient()
{
}

CManagedClient::~CManagedClient()
{
}

CManagedClient& CManagedClient::Instance()
{
	static CManagedClient instance;

	return instance;
}

std::string CManagedClient::GetGameDirectory() const
{
	return m_EngineFuncs.pfnGetGameDirectory();
}

void CManagedClient::SetEngineClientDllInterface( cldll_func_t* pClientDllInterface )
{
	m_pClientDllInterface = pClientDllInterface;

	//At this point in time we can't load the managed wrapper yet since we can't query for the game directory name,
	//so we need to cheat and set the functions to null
	//Right after this the Initialize function is called, where we'll update the engine's interface pointer
	cldll_func_t cldll_func =
	{
		&Wrapper::Initialize
	};

	*pClientDllInterface = cldll_func;
}

bool CManagedClient::Initialize( const cl_enginefunc_t& engfuncsFromEngine )
{
	memcpy( &m_EngineFuncs, &engfuncsFromEngine, sizeof( enginefuncs_t ) );

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

	m_ClientDLLFunctions.SharpLife = Wrapper::CreateClientDllInterface( GetConfiguration() );

	Log::Message( "LoadManagedWrapper" );

	if( !LoadManagedWrapper() )
	{
		Log::Message( "Couldn't load managed library" );
		return false;
	}

	Log::Message( "pfnInitialize" );

	if( !m_pManagedAPI->pfnInitialize( &m_EngineFuncs ) )
	{
		Log::Message( "Failed to initialize managed API" );
		return false;
	}

	m_ClientDLLFunctions.Managed = nullptr;

	Log::Message( "pfnGetClientDllInterface" );

	if( !m_pManagedAPI->pfnGetClientDllInterface( &m_ClientDLLFunctions.Managed ) )
	{
		Log::Message( "Couldn't get managed client DLL API" );
		return false;
	}

	if( !Utility::ValidateFunctionTable( *m_ClientDLLFunctions.Managed ) )
	{
		Log::Message( "Managed Entity API is missing one or more functions" );
		return false;
	}

	Utility::SpliceFunctionTables( *m_ClientDLLFunctions.Managed, m_ClientDLLFunctions.SharpLife, m_ClientDLLFunctions.ExportedToEngine );

	//Initialize the engine's interface now so it can access everything
	*m_pClientDllInterface = m_ClientDLLFunctions.ExportedToEngine;

	//Enable our detours
	AdjustEngineAddresses( GetConfiguration(), const_cast<cl_enginefunc_t*>( &engfuncsFromEngine ) );

	//No detours yet
	//if( !CreateDetours( GetConfiguration(), GetDetours() ) )
	//{
	//	Log::Message( "Couldn't create all detours" );
	//	return false;
	//}

	//Enable all detours
	for( const auto& detour : GetDetours() )
	{
		detour->EnableDetour();
	}

	return true;
}

void CManagedClient::Shutdown()
{
	GetDetours().clear();

	ShutdownManagedWrapper();
	ShutdownManagedHost();
}

bool CManagedClient::LoadManagedWrapper()
{
	Log::Message( "Loading library %s", GetConfiguration().Client.EntryPoint.AssemblyName.c_str() );
	Log::Message( "Entry point is %s.%s", GetConfiguration().Client.EntryPoint.Class.c_str(), GetConfiguration().Client.EntryPoint.Method.c_str() );

	auto managedAPI = LoadClientManagedLibrary( GetConfiguration().Client.EntryPoint, GetCLRHost() );

	if( managedAPI )
	{
		m_pManagedAPI = managedAPI.value();
	}

	return managedAPI.has_value();
}

void CManagedClient::ShutdownManagedWrapper()
{
	if( nullptr != m_pManagedAPI )
	{
		//Free the memory that was allocated for the api structures
		if( nullptr != m_ClientDLLFunctions.Managed )
		{
			m_pManagedAPI->pfnFreeMemory( ClientManagedAPI::MemoryType::ClientAPI, m_ClientDLLFunctions.Managed );
			m_ClientDLLFunctions.Managed = nullptr;
		}

		m_pManagedAPI->pfnFreeMemory( ClientManagedAPI::MemoryType::ManagedAPI, m_pManagedAPI );
		m_pManagedAPI = nullptr;
	}
}
}
