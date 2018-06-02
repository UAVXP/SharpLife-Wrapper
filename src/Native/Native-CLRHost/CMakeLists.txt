set( TARGET_NAME Native-CLRHost )

add_library( ${TARGET_NAME} SHARED )

target_sources( ${TARGET_NAME} PRIVATE
	CCLRHostManager.cpp
	CCLRHostManager.h
	ICLRHostManager.h
)

get_target_property( sources ${TARGET_NAME} SOURCES )

source_group( TREE ${CMAKE_CURRENT_LIST_DIR} PREFIX ${TARGET_NAME} FILES ${sources} )

target_include_directories( ${TARGET_NAME} PRIVATE
	${CMAKE_CURRENT_LIST_DIR}
	${CMAKE_CURRENT_LIST_DIR}/../Native-Utility
	${CMAKE_CURRENT_LIST_DIR}/../../../external/dotnetcore/include
)

target_compile_definitions( ${TARGET_NAME} PRIVATE
	$<$<CXX_COMPILER_ID:MSVC>:_CRT_SECURE_NO_WARNINGS>
)

target_link_libraries( ${TARGET_NAME} PRIVATE
	Native-Utility
)

set_target_properties( ${TARGET_NAME} PROPERTIES
	LIBRARY_OUTPUT_DIRECTORY "${GAME_BASE_PATH}/${NATIVE_DLLS_DIRECTORY}"
	RUNTIME_OUTPUT_DIRECTORY_DEBUG "${GAME_BASE_PATH}/${NATIVE_DLLS_DIRECTORY}"
	RUNTIME_OUTPUT_DIRECTORY_RELEASE "${GAME_BASE_PATH}/${NATIVE_DLLS_DIRECTORY}"
)
