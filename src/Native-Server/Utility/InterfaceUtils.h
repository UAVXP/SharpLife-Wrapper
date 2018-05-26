#ifndef WRAPPER_UTILITY_INTERFACEUTILS_H
#define WRAPPER_UTILITY_INTERFACEUTILS_H

#include <cstdlib>

namespace Wrapper
{
namespace Utility
{
/**
*	@brief A basic function pointer type used for operations involving types
*/
using FUNCPTR = void( * )( );

/**
*	@brief Helper type to provide metadata and conversions
*/
template<typename FUNCTABLE>
struct FunctionTableData final
{
	using FuncTable_t = FUNCTABLE;

	//Treat function tables as arrays of function pointers
	static const size_t NumFunctions = sizeof( FUNCTABLE ) / sizeof( FUNCPTR );

	static const FUNCPTR* GetAsArray( const FUNCTABLE& table )
	{
		return reinterpret_cast<const FUNCPTR*>( &table );
	}

	static FUNCPTR* GetAsArray( FUNCTABLE& table )
	{
		return reinterpret_cast<FUNCPTR*>( &table );
	}
};

/**
*	@brief Validates that a function table has defined all of the functions
*/
template<typename FUNCTABLE>
bool ValidateFunctionTable( const FUNCTABLE& table )
{
	auto pTable = FunctionTableData<FUNCTABLE>::GetAsArray( table );

	for( size_t i = 0; i < FunctionTableData<FUNCTABLE>::NumFunctions; ++i )
	{
		if( nullptr == pTable[ i ] )
		{
			return false;
		}
	}

	return true;
}

/**
*	@brief Helper function to splice the function tables from the managed wrapper and SharpLife
*	@detail This will take the managed table and override any we've implemented with our own
*	It is expected that SharpLife's implementation will call the managed version internally
*/
template<typename FUNCTABLE>
void SpliceFunctionTables( const FUNCTABLE& managedAPI, const FUNCTABLE& sharpLifeAPI, FUNCTABLE& result )
{
	auto pManagedAPI = FunctionTableData<FUNCTABLE>::GetAsArray( managedAPI );
	auto pSharpLifeAPI = FunctionTableData<FUNCTABLE>::GetAsArray( sharpLifeAPI );

	auto pResult = FunctionTableData<FUNCTABLE>::GetAsArray( result );

	for( size_t i = 0; i < FunctionTableData<FUNCTABLE>::NumFunctions; ++i )
	{
		auto pFunction = pSharpLifeAPI[ i ];

		if( nullptr == pFunction )
		{
			pFunction = pManagedAPI[ i ];
		}

		pResult[ i ] = pFunction;
	}
}

/**
*	@brief Helper function used to conditionally include SharpLife interface implementations for debugging
*/
template<typename FUNCPTR>
FUNCPTR ProcessSharpLifeInterface( FUNCPTR ptr, bool enableDebugInterface, bool required = false )
{
	return required || enableDebugInterface ? ptr : nullptr;
}

struct ProcessSharpLifeInterfaceFunctor final
{
	const bool EnableDebugInterface;

	ProcessSharpLifeInterfaceFunctor( const bool enableDebugInterface )
		: EnableDebugInterface( enableDebugInterface )
	{
	}

	template<typename FUNCPTR>
	FUNCPTR operator()( FUNCPTR ptr, bool required = false )
	{
		return ProcessSharpLifeInterface( ptr, EnableDebugInterface, required );
	}
};
}
}

#endif //WRAPPER_UTILITY_INTERFACEUTILS_H
