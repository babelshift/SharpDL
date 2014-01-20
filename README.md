SharpDL
=======

Sharp DirectMedia Layer (XNA-like Framework for SDL)

Overview
======

SharpDL aims to provide a managed, cross-platform, XNA-like framework with graphics, input, audio, and event systems based on the SDL2 library. Games created with this library should strive to be lightweight and portable (in both .NET and Mono). Currently, SharpDL only supports utilizing the built-in SDL 2D rendering system and does not support any OpenGL bindings.

Organization
======

Because SharpDL is written in C# intended for compilation in .NET or Mono, utilizing the native SDL2 libraries requires marshalling of data types through P/Invoke.

Structures and functions used in the SDL2 library are wrapped and modified in classes that adhere to a more .NET-like style.
