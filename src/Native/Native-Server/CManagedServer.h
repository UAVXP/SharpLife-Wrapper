#ifndef WRAPPER_CMANAGEDSERVER_H
#define WRAPPER_CMANAGEDSERVER_H

#include <memory>
#include <string>
#include <vector>

#include "CBaseManagedHost.h"
#include "Dlls/extdll.h"
#include "EngineOverrideInterface.h"

namespace Wrapper
{
struct ServerManagedAPI;

/**
*	@brief Manages the server managed code system
*/
class CManagedServer final : public CBaseManagedHost
{
private:
	

public:
	CManagedServer();
	~CManagedServer();

	static CManagedServer& Instance();

	std::string GetGameDirectory() const override;

	enginefuncs_t* GetEngineFunctions() { return &m_EngineFuncs; }

	DLL_FUNCTIONS* GetManagedDLLFunctions() { return m_DLLFunctions.Managed; }

	DLL_FUNCTIONS* GetExportedDLLFunctions() { return &m_DLLFunctions.ExportedToEngine; }

	NEW_DLL_FUNCTIONS* GetManagedNewDLLFunctions() { return m_NewDLLFunctions.Managed; }

	NEW_DLL_FUNCTIONS* GetExportedNewDLLFunctions() { return &m_NewDLLFunctions.ExportedToEngine; }

	EngineOverrides* GetManagedEngineOverrideFunctions() { return m_EngineOverrides.Managed; }

	EngineOverrides* GetExportedEngineOverrideFunctions() { return &m_EngineOverrides.ExportedToEngine; }

	bool Initialize(const enginefuncs_t& engfuncsFromEngine, globalvars_t* pGlobalvars );

	void Shutdown();

	void FreeMarshalledString( const char* pszString );

private:
	bool LoadManagedWrapper();

	void ShutdownManagedWrapper();

private:
	enginefuncs_t m_EngineFuncs = {};
	globalvars_t* m_pGlobals = nullptr;

	//Exported by the managed library, used to communicate with managed code
	ServerManagedAPI* m_pManagedAPI = nullptr;

	FunctionTable<DLL_FUNCTIONS> m_DLLFunctions;
	FunctionTable<NEW_DLL_FUNCTIONS> m_NewDLLFunctions;
	FunctionTable<EngineOverrides> m_EngineOverrides;

private:
	CManagedServer( const CManagedServer& ) = delete;
	CManagedServer& operator=( const CManagedServer& ) = delete;
	CManagedServer( CManagedServer&& ) = delete;
	CManagedServer& operator=( CManagedServer&& ) = delete;
};
}

#endif //WRAPPER_CMANAGEDSERVER_H