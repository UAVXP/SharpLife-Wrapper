set( TARGET_NAME Native-Server )

add_library( ${TARGET_NAME} SHARED )

target_sources( ${TARGET_NAME} PRIVATE
	Common/const.h
	Common/crc.h
	Common/cvardef.h
	Common/entity_state.h
	Common/in_buttons.h
	Common/netadr.h
	Common/pmtrace.h
	Common/Sequence.h
	Common/usercmd.h
	Common/weaponinfo.h
	Common/winsani_in.h
	Common/winsani_out.h
	Dlls/cdll_dll.h
	Dlls/enginecallback.h
	Dlls/extdll.h
	Dlls/vector.h
	Engine/custom.h
	Engine/edict.h
	Engine/eiface.h
	Engine/progdefs.h
	Engine/progs.h
	PmShared/pm_debug.h
	PmShared/pm_defs.h
	PmShared/pm_info.h
	PmShared/pm_materials.h
	PmShared/pm_movevars.h
	PmShared/pm_shared.h
	Public/Steam/steamtypes.h
	Public/archtypes.h
	CConfiguration.h
	CManagedServer.cpp
	CManagedServer.h
	ConfigurationInput.cpp
	ConfigurationInput.h
	DLLInterface.cpp
	DLLInterface.h
	EngineOverrideInterface.cpp
	EngineOverrideInterface.h
	ManagedInterface.cpp
	ManagedInterface.h
	NewDLLInterface.cpp
	NewDLLInterface.h
	ServerEngineInterface.cpp
	ServerEngineInterface.h
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
)

set_target_properties( ${TARGET_NAME} PROPERTIES
	LIBRARY_OUTPUT_DIRECTORY "${GAME_BASE_PATH}/${NATIVE_DLLS_DIRECTORY}"
	RUNTIME_OUTPUT_DIRECTORY_DEBUG "${GAME_BASE_PATH}/${NATIVE_DLLS_DIRECTORY}"
	RUNTIME_OUTPUT_DIRECTORY_RELEASE "${GAME_BASE_PATH}/${NATIVE_DLLS_DIRECTORY}"
)

install( TARGETS ${TARGET_NAME}
	RUNTIME DESTINATION ${NATIVE_DLLS_DIRECTORY}
	LIBRARY DESTINATION ${NATIVE_DLLS_DIRECTORY}
)

set_property( DIRECTORY PROPERTY VS_STARTUP_PROJECT ${TARGET_NAME} )
