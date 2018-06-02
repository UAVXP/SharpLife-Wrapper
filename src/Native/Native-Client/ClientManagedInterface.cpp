#include "CConfiguration.h"
#include "CLR/CCLRHostException.h"
#include "ICLRHostManager.h"
#include "Log.h"
#include "ClientManagedInterface.h"
#include "Utility/StringUtils.h"

namespace Wrapper
{
using ManagedEntryPoint = bool ( STDMETHODCALLTYPE* )( ClientManagedAPI** managedAPI );

std::optional<ClientManagedAPI*> LoadClientManagedLibrary( const CConfiguration::CManagedEntryPoint& entryPointConfig, ICLRHostManager& clrHost )
{
	try
	{
		auto entryPoint = reinterpret_cast<ManagedEntryPoint>( clrHost.LoadAssemblyAndGetEntryPoint(
			Utility::ToWideString( entryPointConfig.AssemblyName ),
			Utility::ToWideString( entryPointConfig.Class ),
			Utility::ToWideString( entryPointConfig.Method )
		) );

		ClientManagedAPI* pManagedAPI = nullptr;

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
