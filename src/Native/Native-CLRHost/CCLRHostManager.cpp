#include "CLR/CCLRHost.h"
#include "CCLRHostManager.h"
#include "interface.h"

namespace Wrapper
{
namespace
{
CCLRHostManager g_Host;

IBaseInterface* GetCLRHost()
{
	g_Host.AddReference();
	return &g_Host;
}
}

EXPOSE_INTERFACE_FN( &GetCLRHost, IHost, CLRHOSTMANAGER_INTERFACE_VERSION );

CCLRHostManager::CCLRHostManager() = default;
CCLRHostManager::~CCLRHostManager() = default;

void CCLRHostManager::Start( const std::wstring& targetAppPath, const std::vector<std::string>& supportedDotNetCoreVersions )
{
	if( !IsRunning() )
	{
		m_Host = std::make_unique<CLR::CCLRHost>( targetAppPath, supportedDotNetCoreVersions );
	}
}

void* CCLRHostManager::LoadAssemblyAndGetEntryPoint( const std::wstring& assemblyName, const std::wstring& entryPointClass, const std::wstring& entryPointMethod )
{
	if( m_Host )
	{
		return m_Host->LoadAssemblyAndGetEntryPoint( assemblyName, entryPointClass, entryPointMethod );
	}

	return nullptr;
}

void CCLRHostManager::Release()
{
	if( m_iReferenceCount > 0 )
	{
		--m_iReferenceCount;

		if( 0 == m_iReferenceCount )
		{
			m_Host.reset();
		}
	}
}

void CCLRHostManager::AddReference()
{
	++m_iReferenceCount;
}
}
