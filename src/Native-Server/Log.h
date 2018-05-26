#ifndef WRAPPER_LOG_H
#define WRAPPER_LOG_H

#include <string>

namespace Wrapper
{
namespace Log
{
void Message( const char* pszFormat, ... );

/**
*	@brief Log a debug interface message
*/
void DebugInterfaceLog( const char* pszFormat, ... );

void SetDebugInterfaceLoggingEnabled( bool bEnable );
}
}

#endif //WRAPPER_LOG_H
