#ifndef WRAPPER_MANAGEDINTERFACE_H
#define WRAPPER_MANAGEDINTERFACE_H

#include <optional>
#include <string>

#include "Dlls/extdll.h"
#include "EngineOverrideInterface.h"

/**
*	@file
*
*	Provides types and functions to interface with managed code
*/

namespace Wrapper
{
class CConfiguration;
class ICLRHostManager;

/**
*	@brief Structure retrieved from managed libraries that lets us access its main native<->managed interface
*/
struct ManagedAPI final
{
	enum class MemoryType
	{
		ManagedAPI = 0,
		EntityAPI,
		NewEntityAPI,
		EngineOverridesAPI,
		String
	};

	using FreeMemory = void ( * )( MemoryType memoryType, void* pMemory );
	using GiveFnptrsToDll = bool ( * )( enginefuncs_t* pengfuncsFromEngine, globalvars_t* pGlobals );
	using GetEntityAPI = bool ( * )( DLL_FUNCTIONS** pFunctionTable );
	using GetNewDLLFunctions = bool ( * )( NEW_DLL_FUNCTIONS** pFunctionTable );
	using GetEngineOverrides = bool ( * )( EngineOverrides** pFunctionTable );

	FreeMemory pfnFreeMemory = nullptr;

	//Used to pass engine interfaces and managed interfaces around
	GiveFnptrsToDll pfnGiveFnptrsToDll = nullptr;
	GetEntityAPI pfnGetEntityAPI = nullptr;
	GetNewDLLFunctions pfnGetNewDLLFunctions = nullptr;
	GetEngineOverrides pfnGetEngineOverrides = nullptr;
};

std::optional<ManagedAPI*> LoadManagedLibrary( const CConfiguration& configuration, ICLRHostManager& clrHost );
}

#endif //WRAPPER_MANAGEDINTERFACE_H
