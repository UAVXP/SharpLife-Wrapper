#include "CConfiguration.h"
#include "CLR/CCLRHostException.h"
#include "ICLRHostManager.h"
#include "Log.h"
#include "ManagedInterface.h"
#include "Utility/StringUtils.h"

namespace Wrapper
{
using ManagedEntryPoint = bool ( STDMETHODCALLTYPE* )( ManagedAPI** managedAPI );

std::optional<ManagedAPI*> LoadManagedLibrary( const CConfiguration& configuration, ICLRHostManager& clrHost )
{
	try
	{
		auto entryPoint = reinterpret_cast<ManagedEntryPoint>( clrHost.LoadAssemblyAndGetEntryPoint(
			Utility::ToWideString( configuration.ManagedAssemblyName ),
			Utility::ToWideString( configuration.ManagedEntryPointClass ),
			Utility::ToWideString( configuration.ManagedEntryPointMethod )
		) );

		ManagedAPI* pManagedAPI = nullptr;

		const auto result = entryPoint( &pManagedAPI );

		if( !result )
		{
			return {};
		}

		return pManagedAPI;
	}
	catch( const CLR::CCLRHostException& e )
	{
		if( e.HasResultCode() )
		{
			Log::Message( "ERROR - %s\nError code:%x", e.what(), e.GetResultCode() );
		}
		else
		{
			Log::Message( "ERROR - %s", e.what() );
		}

		return {};
	}
}
}
