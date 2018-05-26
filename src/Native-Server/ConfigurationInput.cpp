#include <cstdint>
#include <cstdio>
#include <inih/INIReader.h>

#ifdef WIN32
#include <Windows.h>
#endif

#include "Dlls/extdll.h"
#include "ConfigurationInput.h"
#include "Log.h"

namespace Wrapper
{
Utility::FUNCPTR GetFunctionAddress( INIReader& reader, const std::string& szSection, const std::string& szKeyName )
{
	const auto address = reader.Get( szSection, szKeyName, "" );

#ifdef WIN32
	return reinterpret_cast<Utility::FUNCPTR>( std::stol( address, nullptr, 16 ) );
#else
#error "Implement me"
#endif
}

std::optional<CConfiguration> LoadConfiguration( const std::string& szFileName )
{
	INIReader reader( szFileName );

	if( reader.ParseError() < 0 )
	{
		Log::Message( "Error parsing INI file: %d", reader.ParseError() );
		return {};
	}

	CConfiguration config;

	config.EnableDebugInterface = reader.GetBoolean( "SharpLife", "EnableDebugInterface", false );

	const auto numDotNetCoreVersions = reader.GetInteger( "DotNetCoreVersions", "Count", 0 );

	config.SupportedDotNetCoreVersions.reserve( numDotNetCoreVersions );

	for( long i = 0; i < numDotNetCoreVersions; ++i )
	{
		auto version = reader.Get( "DotNetCoreVersions", std::to_string( i ) + "/Version", "" );

		if( !version.empty() )
		{
			config.SupportedDotNetCoreVersions.emplace_back( std::move( version ) );
		}
	}

	config.ManagedAssemblyName = reader.Get( "Managed", "AssemblyName", "" );
	config.ManagedEntryPointClass = reader.Get( "Managed", "EntryPointClass", "" );
	config.ManagedEntryPointMethod = reader.Get( "Managed", "EntryPointMethod", "" );

	const std::string engineOverrideNames[] = 
	{
		"ED_LoadFromFile"
	};

#ifdef WIN32
	const std::string szSection{ "DetourOverrides-Windows" };
#else
	const std::string szSection{ "DetourOverrides-Linux" };
#endif

	for( const auto& name : engineOverrideNames )
	{
		config.EngineOverrides.emplace( name, GetFunctionAddress( reader, szSection, name ) );
	}

	return config;
}

bool AdjustEngineAddresses( CConfiguration& configuration, void* pEngineAddress )
{
#ifdef WIN32
	MEMORY_BASIC_INFORMATION mem;

	if( !VirtualQuery( pEngineAddress, &mem, sizeof( mem ) ) )
	{
		return false;
	}

	for( auto& engineOverride : configuration.EngineOverrides )
	{
		engineOverride.second = reinterpret_cast<Utility::FUNCPTR>( reinterpret_cast<intptr_t>( engineOverride.second ) + reinterpret_cast<intptr_t>( mem.AllocationBase ) );
	}

	return true;
#endif
}
}
