#include <cstring>

#include "interface.h"

InterfaceReg* InterfaceReg::s_pInterfaceRegs = nullptr;

InterfaceReg::InterfaceReg( InstantiateInterfaceFn fn, const char* pName )
	: m_pName( pName )
{
	m_CreateFn = fn;
	m_pNext = s_pInterfaceRegs;
	s_pInterfaceRegs = this;
}

static IBaseInterface* CreateInterfaceLocal( const char* pName, IFaceResult* pReturnCode )
{
	for( auto pCur = InterfaceReg::s_pInterfaceRegs; pCur; pCur = pCur->m_pNext )
	{
		if( strcmp( pCur->m_pName, pName ) == 0 )
		{
			if( pReturnCode )
			{
				*pReturnCode = IFaceResult::OK;
			}
			return pCur->m_CreateFn();
		}
	}

	if( pReturnCode )
	{
		*pReturnCode = IFaceResult::FAILED;
	}
	return nullptr;
}

EXPORT_FUNCTION IBaseInterface* CreateInterface( const char* pName, IFaceResult* pReturnCode )
{
	return CreateInterfaceLocal( pName, pReturnCode );
}

CreateInterfaceFn Sys_GetFactoryThis()
{
	return CreateInterfaceLocal;
}

CreateInterfaceFn Sys_GetFactory( Wrapper::Utility::CLibrary& library )
{
	return library.GetAddress<CreateInterfaceFn>( CREATEINTERFACE_PROCNAME );
}
