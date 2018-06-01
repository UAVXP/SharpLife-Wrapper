#ifndef WRAPPER_CONFIGURATIONINPUT_H
#define WRAPPER_CONFIGURATIONINPUT_H

#include <optional>
#include <string>

#include "CConfiguration.h"

namespace Wrapper
{
std::optional<CConfiguration> LoadConfiguration( const std::string& szFileName );

bool AdjustEngineAddresses( CConfiguration& configuration, void* pEngineAddress );
}

#endif //WRAPPER_CONFIGURATIONINPUT_H
