#ifndef WRAPPER_ICLRHOSTMANAGER_H
#define WRAPPER_ICLRHOSTMANAGER_H

#include <string>
#include <vector>

#include "interface.h"

namespace Wrapper
{
/**
*	@brief Provides access to the single SharpLife CLR host instance
*/
class ICLRHostManager : public IBaseInterface
{
public:
	/**
	*	@brief Whether the host is running
	*/
	virtual bool IsRunning() const = 0;

	/**
	*	@brief Gets the number of references to the host
	*	@detail This is usually the number of native libraries that are using the host
	*/
	virtual int GetReferenceCount() const = 0;

	/**
	*	@brief Starts the CLR host with the given parameters, if it has not already been started
	*	@throws CLR::CCLRException if an error occurs during startup
	*/
	virtual void Start( const std::wstring& targetAppPath, const std::vector<std::string>& supportedDotNetCoreVersions ) = 0;

	/**
	*	@brief Loads an assembly and gets an entry point from the given class and static method
	*	@throws CLR::CCLRHostException If an error occurs while loading the assembly or getting the entry point
	*/
	virtual void* LoadAssemblyAndGetEntryPoint( const std::wstring& assemblyName, const std::wstring& entryPointClass, const std::wstring& entryPointMethod ) = 0;

	/**
	*	@brief Releases this object
	*	@detail Once released, you can no longer access the CLR or use this interface
	*	Once all users have released the host, the host will shut down
	*/
	virtual void Release() = 0;
};

struct ICLRHostMananagerDeleter
{
	void operator()( ICLRHostManager* ptr )
	{
		ptr->Release();
	}
};
}

#define CLRHOSTMANAGER_INTERFACE_VERSION "CLRHostManagerV001"

#endif //WRAPPER_ICLRHOSTMANAGER_H
