#ifndef WRAPPER_CLIENTMANAGEDINTERFACE_H
#define WRAPPER_CLIENTMANAGEDINTERFACE_H

#include <optional>
#include <string>

#include "Engine./APIProxy.h"

/**
*	@file
*
*	Provides types and functions to interface with managed code
*/

namespace Wrapper
{
class CConfiguration;
struct CConfiguration::CManagedEntryPoint;
class ICLRHostManager;

/**
*	@brief Structure retrieved from managed libraries that lets us access its main native<->managed interface
*/
struct ClientManagedAPI final
{
	enum class MemoryType
	{
		ManagedAPI = 0,
		ClientAPI,
		String
	};

	using FreeMemory = void ( * )( MemoryType memoryType, void* pMemory );
	using Initialize = bool ( * )( cl_enginefunc_t* pengfuncsFromEngine );
	using GetClientDllInterface = bool ( * )( cldll_func_t** pFunctionTable );

	FreeMemory pfnFreeMemory = nullptr;

	//Used to pass engine interfaces and managed interfaces around
	Initialize pfnInitialize = nullptr;
	GetClientDllInterface pfnGetClientDllInterface = nullptr;
};

std::optional<ClientManagedAPI*> LoadClientManagedLibrary( const CConfiguration::CManagedEntryPoint& entryPointConfig, ICLRHostManager& clrHost );
}

#endif //WRAPPER_CLIENTMANAGEDINTERFACE_H
