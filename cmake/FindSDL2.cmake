#
# Finds SDL2 (GoldSource specific)
#

# Prefer game install directory (for shared libs)
find_library( SDL2_LIB NAMES SDL2.lib libSDL2-2.0.so.0 libSDL2-2.0.0.dylib PATHS ${SDL2_DIR} ${CMAKE_CURRENT_LIST_DIR}/../external/SDL2/lib NO_DEFAULT_PATH )

include( FindPackageHandleStandardArgs )
find_package_handle_standard_args( SDL2 DEFAULT_MSG SDL2_LIB )

if( SDL2_LIB )
	add_library( SDL2 SHARED IMPORTED )
	
	set_property( TARGET SDL2 PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CMAKE_CURRENT_LIST_DIR}/../external/SDL2/include )
	
	if( MSVC )
		set_property( TARGET SDL2 PROPERTY IMPORTED_IMPLIB ${SDL2_LIB} )
	else()
		set_property( TARGET SDL2 PROPERTY IMPORTED_LOCATION ${SDL2_LIB} )
	endif()
endif()

unset( SDL2_LIB CACHE )
