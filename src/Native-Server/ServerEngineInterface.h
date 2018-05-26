#ifndef WRAPPER_SERVERENGINEINTERFACE_H
#define WRAPPER_SERVERENGINEINTERFACE_H

#include "Dlls/extdll.h"

/**
*	@file
*
*	Provides the exported functions used by the engine to interface with the server
*/

#ifdef WIN32
#define GIVEFNPTRSTODLL_DLLEXPORT __stdcall
#else
#define GIVEFNPTRSTODLL_DLLEXPORT WRAPPER_DLLEXPORT
#endif

extern "C" void GIVEFNPTRSTODLL_DLLEXPORT GiveFnptrsToDll( enginefuncs_t* pengfuncsFromEngine, globalvars_t* pGlobals );

extern "C" WRAPPER_DLLEXPORT int GetEntityAPI( DLL_FUNCTIONS* pFunctionTable, int interfaceVersion );
extern "C" WRAPPER_DLLEXPORT int GetEntityAPI2( DLL_FUNCTIONS* pFunctionTable, int* interfaceVersion );
extern "C" WRAPPER_DLLEXPORT int GetNewDLLFunctions( NEW_DLL_FUNCTIONS* pFunctionTable, int* interfaceVersion );
extern "C" WRAPPER_DLLEXPORT void custom( entvars_t* pev );

#endif //WRAPPER_SERVERENGINEINTERFACE_H
