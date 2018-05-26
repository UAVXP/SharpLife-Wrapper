#ifndef WRAPPER_CCONFIGURATION_H
#define WRAPPER_CCONFIGURATION_H

#include <string>
#include <unordered_map>
#include <vector>

#include "Utility/InterfaceUtils.h"

namespace Wrapper
{
/**
*	@brief Stores the configuration for the wrapper
*/
class CConfiguration final
{
public:
	CConfiguration() = default;
	~CConfiguration() = default;
	CConfiguration( CConfiguration&& ) = default;
	CConfiguration& operator=( CConfiguration&& ) = default;

	/**
	*	@brief When enabled, all native->managed interface calls will be routed through SharpLife's wrapper library for debugging
	*	@detail This enables you to place breakpoints and logging code to debug issues with the native<->managed code transition
	*/
	bool EnableDebugInterface = false;

	/**
	*	@brief List of supported dot net core versions
	*	Ordered from most to least important (usually newest to oldest), used to find the runtime install directory
	*/
	std::vector<std::string> SupportedDotNetCoreVersions;

	std::string ManagedAssemblyName;
	std::string ManagedEntryPointClass;
	std::string ManagedEntryPointMethod;

	std::unordered_map<std::string, Utility::FUNCPTR> EngineOverrides;

private:
	CConfiguration( const CConfiguration& ) = delete;
	CConfiguration& operator=( const CConfiguration& ) = delete;
};
}

#endif //WRAPPER_CCONFIGURATION_H
