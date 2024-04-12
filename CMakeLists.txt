cmake_minimum_required(VERSION 3.28)
project(dynafx++)

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_AUTOMOC ON)
set(CMAKE_AUTORCC ON)
set(CMAKE_AUTOUIC ON)

find_package(Qt6 COMPONENTS
	Core
	Gui
	Widgets
	Quick
	REQUIRED)

include_directories(lib/include)
link_directories(lib)

qt_add_executable(dynafx++
	main.cpp
	SoundLooper.hpp
	resources.qrc)

target_link_libraries(dynafx++ PRIVATE
	Qt::Core
	Qt::Gui
	Qt::Widgets
	Qt::Quick
	PUBLIC bass)
