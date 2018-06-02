# Must be called client in order for the engine to load it
set( TARGET_NAME client )

find_package( SDL2 MODULE REQUIRED )

add_library( ${TARGET_NAME} SHARED )

target_sources( ${TARGET_NAME} PRIVATE
	ClientDllInterface.cpp
	ClientDllInterface.h
	ClientManagedInterface.cpp
	ClientManagedInterface.h
	CManagedClient.cpp
	CManagedClient.h
)

get_target_property( sources ${TARGET_NAME} SOURCES )

source_group( TREE ${CMAKE_CURRENT_LIST_DIR} PREFIX ${TARGET_NAME} FILES ${sources} )

target_include_directories( ${TARGET_NAME} PRIVATE
	${CMAKE_CURRENT_LIST_DIR}
	${CMAKE_CURRENT_LIST_DIR}/../Native-Utility
	${CMAKE_CURRENT_LIST_DIR}/../Native-CLRHost
	${CMAKE_CURRENT_LIST_DIR}/../../../external/inih/include
	${CMAKE_CURRENT_LIST_DIR}/../../../external/dotnetcore/include
)

target_compile_definitions( ${TARGET_NAME} PRIVATE
	$<$<CXX_COMPILER_ID:MSVC>:_CRT_SECURE_NO_WARNINGS>
)

target_link_libraries( ${TARGET_NAME} PRIVATE
	Native-Utility
	SDL2
)

set_target_properties( ${TARGET_NAME} PROPERTIES
	LIBRARY_OUTPUT_DIRECTORY "${GAME_BASE_PATH}/${NATIVE_CL_DLLS_DIRECTORY}"
	RUNTIME_OUTPUT_DIRECTORY_DEBUG "${GAME_BASE_PATH}/${NATIVE_CL_DLLS_DIRECTORY}"
	RUNTIME_OUTPUT_DIRECTORY_RELEASE "${GAME_BASE_PATH}/${NATIVE_CL_DLLS_DIRECTORY}"
)

install( TARGETS ${TARGET_NAME}
	RUNTIME DESTINATION ${NATIVE_CL_DLLS_DIRECTORY}
	LIBRARY DESTINATION ${NATIVE_CL_DLLS_DIRECTORY}
)

set_property( DIRECTORY PROPERTY VS_STARTUP_PROJECT ${TARGET_NAME} )
