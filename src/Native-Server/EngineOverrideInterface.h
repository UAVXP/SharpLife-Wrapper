#ifndef WRAPPER_ENGINEOVERRIDEINTERFACE_H
#define WRAPPER_ENGINEOVERRIDEINTERFACE_H

#include <memory>
#include <vector>

#include "CConfiguration.h"
#include "CDetour/detours.h"

namespace Wrapper
{
struct DetourDestructor final
{
	void operator()( CDetour* pDetour )
	{
		pDetour->Destroy();
	}
};

struct EngineOverrides
{
	void ( *pfnED_LoadFromFile )( char* data );
};

bool CreateDetours( const CConfiguration& configuration, std::vector<std::unique_ptr<CDetour, DetourDestructor>>& detours );

EngineOverrides CreateEngineOverridesInterface( const CConfiguration& configuration );

namespace Overrides
{
void ED_LoadFromFile( char* data );
}
}

#endif //WRAPPER_ENGINEOVERRIDEINTERFACE_H
