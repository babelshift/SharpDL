# SharpDL
[![Build Status](https://dev.azure.com/justinskiles/justinskiles/_apis/build/status/babelshift.SharpDL?branchName=master)](https://dev.azure.com/justinskiles/justinskiles/_build/latest?definitionId=1&branchName=master)
[![NuGet](https://img.shields.io/nuget/v/SharpDL.svg)](https://www.nuget.org/packages/SharpDL)

Sharp DirectMedia Layer ([XNA](https://en.wikipedia.org/wiki/Microsoft_XNA)-like Framework for [SDL2](https://www.libsdl.org/index.php)). XNA was a free set of tools and a framework for building graphical applications (primarily for indie game development on Windows and Xbox 360). It has since been discontinued by Microsoft.

## Overview
SharpDL aims to provide a managed, cross-platform, XNA-like framework with graphics, input, audio, and event systems based on the SDL2 library. Games created with this library should strive to be lightweight and portable (Windows, Mac, Linux). Currently, SharpDL only supports utilizing the built-in SDL 2D rendering system and does not support any OpenGL bindings.

## Important!
Because SharpDL is written in C# intended for compilation in .NET Framework and .NET Core, utilizing the native SDL2 libraries requires marshalling of data types through P/Invoke. This also means that any memory allocated in "native land" (ie: in the SDL libraries) will require strict adherence to good disposal mechanisms in the managed code. Failing to free any allocated resources will result in memory leaks.

For more information about the wrapper around SDL2 in order to make our P/Invoke possible, see the [SDL2-CS](https://github.com/flibitijibibo/SDL2-CS) and the [forked SharpDL-SDL2-CS](https://github.com/babelshift/SDL2-CS) referenced by SharpDL.

## Installation & Setup
### Import NuGet
See the library in the NuGet gallery [here](https://www.nuget.org/packages/SharpDL).

Package Manager:
```
Install-Package SharpDL
```

.NET Core CLI:
```
dotnet add package SharpDL
```

### Download Native SDL2 Libraries
After you've imported the NuGet package, you will still need to download the native SDL2 libraries for your platform and place the libraries alongside your executable.

**Windows & Mac**

- SDL2: https://www.libsdl.org/download-2.0.php
- SDL2_image: https://www.libsdl.org/projects/SDL_image/
- SDL2_ttf: https://www.libsdl.org/projects/SDL_ttf/

**Ubuntu**

While the below will install SDL2 dependencies, SharpDL may still fail to function because of a limitation in the way .NET Core handles native library resolution when libraries are named differently across operating systems. See [#7](https://github.com/babelshift/SharpDL/issues/7).
```bash
apt install libsdl2-2.0-0
apt install libsdl2-image-2.0-0
apt install libsdl2-ttf-2.0-0
```

**Arch / Manjaro**

This doesn't have the same limitation as Ubuntu because the libraries are distributed as simply `libSDL2.so` and thus are resolved by .NET Core properly. See [#7](https://github.com/babelshift/SharpDL/issues/7).
```bash
pacman -S sdl2
```

## Getting Started

Start with some basic guides on the Wiki:

- [Getting Started](https://github.com/babelshift/SharpDL/wiki/1.-Getting-Started)

Move on to the Examples in the repository:

- [Examples](https://github.com/babelshift/SharpDL/tree/master/Examples)
  - [Sandbox](https://github.com/babelshift/SharpDL/tree/master/Examples/Example0_Sandbox) (anything goes, I change this regularly)
  - [Blank Window](https://github.com/babelshift/SharpDL/tree/master/Examples/Example1_BlankWindow)
  - [Draw Texture](https://github.com/babelshift/SharpDL/tree/master/Examples/Example2_DrawTexture)
  - [Event Handling](https://github.com/babelshift/SharpDL/tree/master/Examples/Example3_EventHandling)
