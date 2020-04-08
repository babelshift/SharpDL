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
After you've imported the NuGet package, you will still need to download the native SDL2 libraries for your platform and place the libraries alongside your executable. Note that SharpDL only supports Windows 64-bit at this time. There are plans to support Linux 64-bit in the future.

**SDL2**: https://www.libsdl.org/download-2.0.php

**SDL2_image**: https://www.libsdl.org/projects/SDL_image/

**SDL2_ttf**: https://www.libsdl.org/projects/SDL_ttf/

## Code Organization
SharpDL consists of four main projects: SharpDL, SharpDL.Events, SharpDL.Graphics, and SharpDL.Input. A project for audio is not yet available.

<dl>
    <dt>SharpDL</dt>
    <dd>Responsible for the base <code>Game</code> class, <code>GameTime</code>, and some experimental classes such as <code>Logger</code> and <code>Timer</code>.</dd>
    <dt>SharpDL.Events</dt>
    <dd>responsible for various <code>EventArgs</code> implementations that are based on the <code>SDL_Event</code> union. For example, the SDL_Event union is processed in the SharpDL's <code>Run</code> loop, <code>EventArgs</code> of the proper type are created, and an <code>event</code> of that handles that type is fired. This allows for a more .NET-like event system.</dd>
    <dt>SharpDL.Graphics</dt>
    <dd>Contains any class that is responsible for graphical operations such as creating a SDL_Window, rendering with a SDL_Renderer, creating a SDL_Surface, or loading a SDL_Texture. Examples of some of these classes are <code>Color</code>, <code>Font</code>, <code>Renderer</code>, <code>Window</code>, <code>Texture</code>, <code>TrueTypeText</code>, and more. Some classes do not have direct relations to SDL structures such as <code>Vector</code>, which represents an X,Y coordinate in a 2D space.</dd>
    <dt>SharpDL.Input</dt>
    <dd>Contains classes to handle Keyboard and Mouse input by capturing mapped SDL structures into .NET-style enumerators. Joystick and controller input is not yet available.</dd>
</dl>

## Examples
The base `Game` class offers options to initialize SDL, create windows, create renderers, and process events. Two important steps to the initialization of the library is to create a SDL window and create a SDL renderer as shown below.

`SDL_CreateWindow` is wrapped by the `Window` class. Creating a window:
  
        string title = "My Window"
        int x = 0;
        int y = 0;
        int width = 640;
        int height = 480;
        WindowFlags flags = WindowFlags.Shown | WindowFlags.GrabbedInputFocus;
    
        Window window = new Window(title, x, y, width, height, flags);

`SDL_CreateRenderer` is wrapped by the `Renderer` class. Creating a renderer:
    
        int index = -1;
        RendererFlags flags = RendererFlags.RendererAccelerated;
    
        Renderer renderer = new Renderer(window, index, flags);
        renderer.SetRenderLogicalSize(640, 480); // or however large we want to render to

To simply make a game without worrying about the way the library works, follow these steps (or look at the examples folder in the project).

1. Inherit from the SharpDL.Game class.

2. Override the `Initialize` method. Call `base.Initialize()` to initialize SDL.

You are now free to create surfaces and textures to render to the screen with your renderer object. However, you will want to override the `LoadContent`, `Update`, and `Draw` methods of the `Game` class in order to get the timing of your texture creation, updating, and rendering to work with SharpDL's game loop.

    Texture myTexture;
    int myTexturePositionX = 100;
    int myTexturePositionY = 100;
    
    // load all game assets here such as images and audio
    protected override void LoadContent()
    {
        // creates an in memory SDL Surface from the PNG at the passed path
        Surface surface = new Surface("Content/Images/MyImage.png", SurfaceType.PNG);
        
        // creates a GPU-driven SDL texture using the initialized renderer and created surface
        myTexture = new Texture(renderer, surface);
    }
    
    // update the game state here such as entity positions
    protected override void Update(GameTime gameTime)
    {
    }
    
    // draw loaded assets here
    protected override void Draw(GameTime gameTime)
    {
        renderer.RenderTexture(myTexture, positionX, positionY);
        renderer.RenderPresent();
    }

    
You can see that this is very similar to XNA's game looping features. While the SharpDL library is extremely simple at this time, you can still create some pretty fun games from it.
