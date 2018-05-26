#ifndef WRAPPER_NEWDLLINTERFACE_H
#define WRAPPER_NEWDLLINTERFACE_H

#include "Dlls/extdll.h"

namespace Wrapper
{
class CConfiguration;
}

/**
*	@brief Creates an instance of the NEW_DLL_FUNCTIONS interface
*/
NEW_DLL_FUNCTIONS CreateNewDLLFunctionsInterface( const Wrapper::CConfiguration& configuration );

void OnFreeEntPrivateData( edict_t* pEdict );

void GameDLLShutdown();

int ShouldCollide( edict_t* pentTouched, edict_t* pentOther );

void CvarValue( const edict_t* pEnt, const char* value );
void CvarValue2( const edict_t* pEnt, int requestID, const char* cvarName, const char* value );

#endif //WRAPPER_NEWDLLINTERFACE_H
