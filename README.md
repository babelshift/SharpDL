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
## Getting Started

### The Game Loop

Calling the `Run` method on the `Game` class will result in the following:

1. `Initialize` will initialize `SDL` with `SDL_INIT_EVERYTHING` flag, initialize `SDL_ttf`, and initialize `SDL_image` with only PNG support.
2. `LoadContent` will perform a no-op.
3. Until the `Quit` method is called, the game loop will call `Update` to update the game state and `Draw` to draw the game state. Note that the game loop uses a [Fixed Time Step](https://gafferongames.com/post/fix_your_timestep/) approach
4. `UnloadContent` will dispose the `Window`, `Renderer`, `SDL_ttf`, `SDL_image`, and `SDL`.

### Creating a Blank Window

1. First create a console application.

   `dotnet new console -o MyFirstSharpDL`

2. Create a `MainGame` class which inherits from `Game` to handle your game logic

   ```c#
   using SharpDL;
   
   namespace MyFirstSharpDL
   {
       public class MainGame : Game
       {
           
       }
   }
   ```

3. Update `Program.cs` to create a new instance of your `MainGame` and start it with the `Run` method.

   ```c#
   namespace MyFirstSharpDL
   {
       class Program
       {
           static void Main(string[] args)
           {
               MainGame game = new MainGame();
               game.Run();
           }
       }
   }
   ```

4. Override the `Initialize` method in your `MainGame` class to create a `Window` and `Renderer`. In this case, we are rendering at position (100, 100) in a window of size (1152, 720) with acceleration and v-sync.

   ```c#
   protected override void Initialize()
   {
       // Important. This initializes the engine.
       base.Initialize();
       
   	// Create your window and renderer
       CreateWindow("MyFirstSharpDL", 100, 100, 1152, 720, WindowFlags.Shown);
       CreateRenderer(RendererFlags.RendererAccelerated | RendererFlags.RendererPresentVSync);
   }
   ```

5. Build and run to see a blank window.

   `dotnet build`

   `dotnet run`

### Rendering Textures

1. Follow the steps from the above `Creating a Blank Window` example.

2. Override the `LoadContent` method in your `MainGame` class to load a PNG image.

   ```c#
   private Texture texture;
   
   protected override void LoadContent()
   {
       base.LoadContent();
       
       // Load a PNG into a surface object and create a texture from it
       Surface surface = new Surface("logo.png", SurfaceType.PNG);
       texture = new Texture(Renderer, texture);
   }
   ```

3. Override the `Draw` method in your `MainGame` class to draw the texture.

   ```c#
   protected override void Draw(GameTime gameTime)
   {
       base.Draw(gameTime);
       
       // Clear the screen of any previous draws
       Renderer.ClearScreen();
       
       // Draw the texture at position (100, 100) and commit the renderer
       texture.Draw(100, 100);
       Renderer.RenderPresent();
   }
   ```

4. Override the `UnloadContent` method in your `MainGame` class to dispose of textures. Forgetting this step can lead to memory leaks. Any object created through SDL is not tracked by the garbage collector automatically and needs to be manually cleaned up.

   ```c#
   protected override void UnloadContent()
   {
       base.UnloadContent();
       texture.Dispose();
   }
   ```

5. Build and run the game.

   `dotnet build`

   `dotnet run`