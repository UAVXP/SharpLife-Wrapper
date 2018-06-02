#include "CBaseManagedHost.h"
#include "CLR/CCLRHostException.h"
#include "ConfigurationInput.h"
#include "Log.h"
#include "Utility/StringUtils.h"

namespace Wrapper
{
const std::string_view CBaseManagedHost::CONFIG_FILENAME{ "cfg/SharpLife-Wrapper-Native.ini" };
const std::wstring CBaseManagedHost::WRAPPER_DIRECTORY{ L"dlls" };
const std::wstring CBaseManagedHost::CLRHOST_LIBRARY_NAME{ L"Native-CLRHost" };
const std::wstring CBaseManagedHost::LIBRARY_EXTENSION_NAME{ L".dll" };

CBaseManagedHost::CBaseManagedHost() = default;

CBaseManagedHost::~CBaseManagedHost()
{
	//If we've reached this point and there are still detours in the list, then we're crashing
	//Detours can't patch the engine at this point, so just leak them instead so they don't trigger the patch
	for( auto& detour : m_Detours )
	{
		detour.release();
	}
}

std::wstring CBaseManagedHost::GetWideGameDirectory() const
{
	return Utility::ToWideString( GetGameDirectory() );
}

bool CBaseManagedHost::LoadConfiguration()
{
	auto config = Wrapper::LoadConfiguration( GetGameDirectory() + '/' + std::string{ CONFIG_FILENAME } );

	if( config )
	{
		m_Configuration = std::move( config.value() );
		return true;
	}

	return false;
}

bool CBaseManagedHost::StartManagedHost()
{
	auto dllsPath = GetWideGameDirectory() + L'/' + WRAPPER_DIRECTORY;

	dllsPath = Utility::GetAbsolutePath( dllsPath );

	try
	{
		m_CLRHostLibrary = Utility::CLibrary( dllsPath + L'/' + CLRHOST_LIBRARY_NAME + LIBRARY_EXTENSION_NAME );

		if( !m_CLRHostLibrary )
		{
			Log::Message( "ERROR - Couldn't load CLR host library" );
			return false;
		}

		auto factory = Sys_GetFactory( m_CLRHostLibrary );

		if( !factory )
		{
			Log::Message( "ERROR - Couldn't get CLR host factory" );
			return false;
		}

		m_CLRHost.reset( static_cast<ICLRHostManager*>( factory( CLRHOSTMANAGER_INTERFACE_VERSION, nullptr ) ) );

		if( !m_CLRHost->IsRunning() )
		{
			m_CLRHost->Start( dllsPath, m_Configuration.SupportedDotNetCoreVersions );
		}
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

void CBaseManagedHost::ShutdownManagedHost()
{
	m_CLRHost.release();
	m_CLRHostLibrary = Utility::CLibrary{};
}
}
