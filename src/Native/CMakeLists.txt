cmake_minimum_required( VERSION 3.11 )

project( Native-Wrapper LANGUAGES C CXX )

set( CMAKE_CXX_STANDARD 17 )
set( CMAKE_CXX_STANDARD_REQUIRED ON )

add_subdirectory( Native-Server )
