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
	Common/const.h
	Common/crc.h
	Common/cvardef.h
	Common/entity_state.h
	Common/enums.h
	Common/in_buttons.h
	Common/netadr.h
	Common/pmtrace.h
	Common/Sequence.h
	Common/usercmd.h
	Common/weaponinfo.h
	Common/winsani_in.h
	Common/winsani_out.h
	Common/wrect.h
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
	Utility/CLibrary.cpp
	Utility/CLibrary.h
	Utility/InterfaceUtils.h
	Utility/StringUtils.cpp
	Utility/StringUtils.h
	CBaseManagedHost.cpp
	CBaseManagedHost.h
	CConfiguration.h
	ConfigurationInput.cpp
	ConfigurationInput.h
	interface.cpp
	interface.h
	Log.cpp
	Log.h
)

get_target_property( sources ${TARGET_NAME} SOURCES )

source_group( TREE ${CMAKE_CURRENT_LIST_DIR} PREFIX ${TARGET_NAME} FILES ${sources} )

target_include_directories( ${TARGET_NAME} PRIVATE
	${CMAKE_CURRENT_LIST_DIR}
	${CMAKE_CURRENT_LIST_DIR}/../Native-CLRHost
	${CMAKE_CURRENT_LIST_DIR}/../../../external/inih/include
	${CMAKE_CURRENT_LIST_DIR}/../../../external/dotnetcore/include
)

target_compile_definitions( ${TARGET_NAME} PRIVATE
	$<$<CXX_COMPILER_ID:MSVC>:_CRT_SECURE_NO_WARNINGS>
)
