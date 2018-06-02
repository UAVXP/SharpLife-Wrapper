set( TARGET_NAME Native-Utility )

add_library( ${TARGET_NAME} STATIC )

target_sources( ${TARGET_NAME} PRIVATE
	CDetour/asm/asm.c
	CDetour/asm/asm.h
	CDetour/detourhelpers.h
	CDetour/detours.cpp
	CDetour/detours.h
	CLR/CCLRHost.cpp
	CLR/CCLRHost.h
	CLR/CCLRHostException.h
	Utility/CLibrary.cpp
	Utility/CLibrary.h
	Utility/InterfaceUtils.h
	Utility/StringUtils.cpp
	Utility/StringUtils.h
	interface.cpp
	interface.h
	Log.cpp
	Log.h
)

get_target_property( sources ${TARGET_NAME} SOURCES )

source_group( TREE ${CMAKE_CURRENT_LIST_DIR} PREFIX ${TARGET_NAME} FILES ${sources} )

target_include_directories( ${TARGET_NAME} PRIVATE
	${CMAKE_CURRENT_LIST_DIR}
	${CMAKE_CURRENT_LIST_DIR}/../../../external/dotnetcore/include
)

target_compile_definitions( ${TARGET_NAME} PRIVATE
	$<$<CXX_COMPILER_ID:MSVC>:_CRT_SECURE_NO_WARNINGS>
)
