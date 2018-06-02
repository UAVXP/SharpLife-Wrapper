#ifndef WRAPPER_CCLRHOSTMANAGER_H
#define WRAPPER_CCLRHOSTMANAGER_H

#include <memory>

#include "ICLRHostManager.h"

namespace CLR
{
class CCLRHost;
}

namespace Wrapper
{
class CCLRHostManager final : public ICLRHostManager
{
public:
	CCLRHostManager();
	~CCLRHostManager();

	bool IsRunning() const override { return !!m_Host; }

	int GetReferenceCount() const override { return m_iReferenceCount; }

	void Start( const std::wstring& targetAppPath, const std::vector<std::string>& supportedDotNetCoreVersions ) override;

	void* LoadAssemblyAndGetEntryPoint( const std::wstring& assemblyName, const std::wstring& entryPointClass, const std::wstring& entryPointMethod );

	void Release() override;

	void AddReference();

private:
	int m_iReferenceCount = 0;

	std::unique_ptr<CLR::CCLRHost> m_Host;
};
}

#endif //WRAPPER_CCLRHOSTMANAGER_H
