#ifndef WRAPPER_CMANAGEDSERVER_H
#define WRAPPER_CMANAGEDSERVER_H

#include <memory>
#include <string>
#include <vector>

#include "CConfiguration.h"
#include "CDetour/detours.h"
#include "Dlls/extdll.h"
#include "EngineOverrideInterface.h"
#include "ICLRHostManager.h"
#include "Utility/CLibrary.h"

namespace Wrapper
{
struct ManagedAPI;

/**
*	@brief Manages the server managed code system
*/
class CManagedServer final
{
private:
	//Helper type to store function tables provided by both SharpLife and managed code
	template<typename FUNCTABLE>
	struct FunctionTable final
	{
		FUNCTABLE SharpLife = {};
		FUNCTABLE* Managed = nullptr;

		//This is the spliced version containing a mix of managed code and SharpLife functions
		//When we override functions in SharpLife, for example to handle game shutdown it will call the managed code version internally
		FUNCTABLE ExportedToEngine = {};
	};

	static const std::string_view CONFIG_FILENAME;
	static const std::wstring WRAPPER_DIRECTORY;
	static const std::wstring CLRHOST_LIBRARY_NAME;
	static const std::wstring LIBRARY_EXTENSION_NAME;

public:
	CManagedServer();
	~CManagedServer();

	static CManagedServer& Instance();

	std::string GetGameDirectory() const;

	std::wstring GetWideGameDirectory() const;

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
	bool LoadConfiguration();

	bool StartManagedHost();

	void ShutdownManagedHost();

	bool LoadManagedWrapper();

	void ShutdownManagedWrapper();

private:
	enginefuncs_t m_EngineFuncs = {};
	globalvars_t* m_pGlobals = nullptr;

	CConfiguration m_Configuration;

	//The host for the managed code runtime
	Utility::CLibrary m_CLRHostLibrary;
	std::unique_ptr<ICLRHostManager, ICLRHostMananagerDeleter> m_CLRHost;

	//Exported by the managed library, used to communicate with managed code
	ManagedAPI* m_pManagedAPI = nullptr;

	FunctionTable<DLL_FUNCTIONS> m_DLLFunctions;
	FunctionTable<NEW_DLL_FUNCTIONS> m_NewDLLFunctions;
	FunctionTable<EngineOverrides> m_EngineOverrides;

	std::vector<std::unique_ptr<CDetour, DetourDestructor>> m_Detours;

private:
	CManagedServer( const CManagedServer& ) = delete;
	CManagedServer& operator=( const CManagedServer& ) = delete;
	CManagedServer( CManagedServer&& ) = delete;
	CManagedServer& operator=( CManagedServer&& ) = delete;
};
}

#endif //WRAPPER_CMANAGEDSERVER_H