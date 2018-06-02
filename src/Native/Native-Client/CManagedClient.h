#ifndef WRAPPER_CMANAGEDCLIENT_H
#define WRAPPER_CMANAGEDCLIENT_H

#include "CBaseManagedHost.h"
#include "CConfiguration.h"
#include "ClientManagedInterface.h"
#include "Engine/APIProxy.h"

namespace Wrapper
{
/**
*	@brief Manages the client managed code system
*/
class CManagedClient final : public CBaseManagedHost
{
public:
	CManagedClient();
	~CManagedClient();

	static CManagedClient& Instance();

	std::string GetGameDirectory() const override;

	cl_enginefunc_t* GetEngineFunctions() { return &m_EngineFuncs; }

	cldll_func_t* GetManagedDLLFunctions() { return m_ClientDLLFunctions.Managed; }

	cldll_func_t* GetExportedDLLFunctions() { return &m_ClientDLLFunctions.ExportedToEngine; }

	void SetEngineClientDllInterface( cldll_func_t* pClientDllInterface );

	bool Initialize( const cl_enginefunc_t& engfuncsFromEngine );

	void Shutdown();

private:
	bool LoadManagedWrapper();

	void ShutdownManagedWrapper();

private:
	//The engine's client dll interface
	cldll_func_t* m_pClientDllInterface = nullptr;

	cl_enginefunc_t m_EngineFuncs = {};

	//Exported by the managed library, used to communicate with managed code
	ClientManagedAPI* m_pManagedAPI = nullptr;

	FunctionTable<cldll_func_t> m_ClientDLLFunctions;
};
}

#endif //WRAPPER_CMANAGEDCLIENT_H
