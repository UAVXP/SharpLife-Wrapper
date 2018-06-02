#ifndef WRAPPER_CBASEMANAGEDHOST_H
#define WRAPPER_CBASEMANAGEDHOST_H

#include <memory>
#include <vector>

#include "CConfiguration.h"
#include "CDetour/detours.h"
#include "ICLRHostManager.h"
#include "Utility/CLibrary.h"

namespace Wrapper
{
/**
*	@brief Base class for SharpLife managed hosts
*/
class CBaseManagedHost
{
protected:
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

protected:
	CBaseManagedHost();
	~CBaseManagedHost();

public:
	virtual std::string GetGameDirectory() const = 0;

	std::wstring GetWideGameDirectory() const;

protected:
	CConfiguration& GetConfiguration() { return m_Configuration; }

	ICLRHostManager& GetCLRHost() { return *m_CLRHost; }

	auto GetDetours() -> auto& { return m_Detours; }

	bool LoadConfiguration();

	bool StartManagedHost();

	void ShutdownManagedHost();

private:
	CConfiguration m_Configuration;

	//The host for the managed code runtime
	Utility::CLibrary m_CLRHostLibrary;
	std::unique_ptr<ICLRHostManager, ICLRHostMananagerDeleter> m_CLRHost;

	std::vector<std::unique_ptr<CDetour, DetourDestructor>> m_Detours;
};
}

#endif //WRAPPER_CBASEMANAGEDHOST_H
