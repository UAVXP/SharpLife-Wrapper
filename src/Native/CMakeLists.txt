cmake_minimum_required( VERSION 3.11 )

set( STEAMCOMMON "$ENV{STEAMCOMMON}" CACHE PATH "Steam common directory path" )

set( GAME_DIRECTORY "Half-Life" CACHE STRING "Game directory name" )
set( MOD_DIRECTORY "sharplife" CACHE STRING "Mod directory name" )

set( GAME_BASE_PATH ${STEAMCOMMON}/${GAME_DIRECTORY}/${MOD_DIRECTORY} )

set( NATIVE_DLLS_DIRECTORY "dlls" )

project( Native-Wrapper LANGUAGES C CXX )

set( CMAKE_CXX_STANDARD 17 )
set( CMAKE_CXX_STANDARD_REQUIRED ON )

add_subdirectory( Native-Utility )
add_subdirectory( Native-CLRHost )
add_subdirectory( Native-Server )
