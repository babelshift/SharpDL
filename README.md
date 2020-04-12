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
## Getting Started (Version 3.0+)

Version 3.0 rearchitected the structure of the engine to favor [composition over inheritance](https://en.wikipedia.org/wiki/Composition_over_inheritance). As such, it will help to understand the fundamentals of [dependency injection](https://en.wikipedia.org/wiki/Dependency_injection). Don't worry, you don't need to be an expert to use the framework. 

While this approach introduces more boilerplate to the bootstrapping process, it also allows for more flexibility in how the engine is implemented. In fact, you can even inject your own implementations of dependencies to change the behavior of your game.

SharpDL has helper extensions which make wiring up all of its inner dependencies easy (examples below) but requires the use of the `Microsoft.Extensions.DependencyInjection` framework. You can use a different dependency injection container, but you'll need to wire up the game engine dependencies on your own.

### The Game Loop

Calling the `Start` method on the `IGame` interface will result in the following:

1. `Initialize` will initialize `SDL` with `SDL_INIT_EVERYTHING` flag, initialize `SDL_ttf`, and initialize `SDL_image`.
2. `LoadContent` will perform a no-op.
3. Until the `End` method is called on the `IGame` interface, the game loop will call `Update` to update the game state and `Draw` to draw the game state. Note that the game loop uses a [Fixed Time Step](https://gafferongames.com/post/fix_your_timestep/) approach
4. `UnloadContent` will dispose the `Window`, `Renderer`, `SDL_ttf`, `SDL_image`, and `SDL`.

### Creating a Blank Window

1. First create a console application.

   `dotnet new console -o MyFirstSharpDL`

2. Create a `MainGame` class which implements the `IGame` interface and takes an injected dependency on `IGameEngine`.

   ```c#
   public class MainGame : IGame
   {
       private readonly IGameEngine engine;
       
       public MainGame(IGameEngine engine)
       {
           this.engine = engine;
       }
       
       // Must be implemented as part of IGame interface. 
       // Usually just used to start the engine.
       public void Run()
       {
           engine.Start(GameEngineInitializeType.Everything);
       }
   }
   ```

3. Update `Program.cs` to create your dependency injection container, add the game engine to it, and run your `MainGame` class. See comments below.

   ```c#
   class Program
   {
       static void Main(string[] args)
       {
           // Create dependency injection container with configured services.
           ServiceProvider serviceProvider = GetServiceProvider();
           
           // Get instance of your implemented game and then run it.
           var game = serviceProvider.GetService<IGame>();
           game.Run();
       }
   
       // Creates the dependency injection container, registers services with the container,
       // and returns the built container.
       private static ServiceProvider GetServiceProvider()
       {
           var services = new ServiceCollection();
           ConfigureServices(services);
           var serviceProvider = services.BuildServiceProvider();
           return serviceProvider;
       }
   
       private static void ConfigureServices(ServiceCollection services)
       {
           // Add the game engine components and your game to the dependency injection container.
           services.AddSharpGame<MainGame>();
       }
   }
   ```

4. Add an `Initialize` method to your `MainGame` class. Update the `MainGame` constructor to set the engine's `Initialize` action to your method. This will inject your implementation into the game loop.

   ```c#
   private IWindow window;
   private IRenderer renderer;
   
   public MainGame(IGameEngine engine)
   {
   	this.engine = engine;
   	engine.Initialize = () => Initialize();
   }
   
   // Create a window from the engine's window factory.
   // Create a renderer using that window from the engine's renderer factory.
   private void Initialize()
   {
   	window = engine.WindowFactory.CreateWindow("Example 0 - Sandbox");
   	renderer = engine.RendererFactory.CreateRenderer(window);
   }
   ```

5. Build and run to see a blank window.

   `dotnet build`

   `dotnet run`

### Drawing Textures

1. Follow the steps from the above `Creating a Blank Window` example.

2. Add a `LoadContent` method to your `MainGame` class. Update the `MainGame` constructor to set the engine's `LoadContent` action to your method. This will inject your implementation into the game loop.

   ```c#
   private IWindow window;
   private IRenderer renderer;
   private Texture texture;
   
   public MainGame(IGameEngine engine)
   {
   	this.engine = engine;
   	engine.Initialize = () => Initialize();
   	engine.LoadContent = () => LoadContent();
   }
   
   // Load a surface into memory from the image at your path.
   // Create a GPU-driven texture using a renderer and a surface.
   private void LoadContent()
   {
   	Surface surface = new Surface("image.png", SurfaceType.PNG);
   	texture = new Texture(renderer, surface);
   }
   ```

3. Add a `Draw` method to your `MainGame` class. Update the `MainGame` constructor to set the engine's `Draw` action to your method. This will inject your implementation into the game loop.

   ```c#
   private IRenderer renderer;
   private Texture texture;
   
   public MainGame(IGameEngine engine)
   {
   	this.engine = engine;
   	engine.Initialize = () => Initialize();
   	engine.LoadContent = () => LoadContent();
   	engine.Draw = (gameTime) => Draw(gameTime);
   }
   
   // Clear the screen to prevent stale renders.
   // Draw the texture at (100, 100).
   // Commit renders to the window.
   private void Draw(GameTime gameTime)
   {
   	renderer.ClearScreen();
   	texture.Draw(100, 100);
   	renderer.RenderPresent();
   }
   ```

4. Add an `UnloadContent` method to your `MainGame` class. Update the `MainGame` constructor to set the engine's `UnloadContent` action to your method. This will inject your implementation into the game loop.

   ```c#
   private Texture texture;
   
   public MainGame(IGameEngine engine)
   {
   	this.engine = engine;
   	engine.Initialize = () => Initialize();
   	engine.LoadContent = () => LoadContent();
   	engine.Draw = (gameTime) => Draw(gameTime);
   	engine.UnloadContent = () => UnloadContent(gameTime);
   }
   
   // Always dispose your assets!
   private void UnloadContent()
   {
   	texture.Dispose();
   }
   ```

5. Build and run to see drawn textures

   `dotnet build`

   `dotnet run`

### Add Console Logging

The SharpDL game engine allows you to inject whatever logging framework you want to use (provided that the framework implements a provider for the `using Microsoft.Extensions.Logging` framework).

1. Add the `Microsoft.Extensions.Logging.Console` NuGet package to your game project.

   `dotnet add package Microsoft.Extensions.Logging.Console`

2. Update `Program.cs` to add console logging to the dependency injection container with filtering to whatever log level you prefer. Example below allows `Trace` and above.

   ```c#
   private static void ConfigureServices(ServiceCollection services)
   {
       services.AddSharpGame<MainGame>()
       .AddLogging(config => {
           config.AddConsole();
       })
       .Configure<LoggerFilterOptions>(options => {
           options.AddFilter<ConsoleLoggerProvider>(null, LogLevel.Trace);
       });
   }
   ```

3. Add an injected `ILogger<MainGame>` dependency to your `MainGame` constructor.

   ```c#
   private readonly IGameEngine engine;
   private readonly ILogger<MainGame> logger;
   
   public MainGame(IGameEngine engine, ILogger<MainGame> logger)
   {
   	this.engine = engine;
       this.logger = logger;
   	engine.Initialize = () => Initialize();
   	engine.LoadContent = () => LoadContent();
   	engine.Draw = (gameTime) => Draw(gameTime);
   	engine.UnloadContent = () => UnloadContent(gameTime);
   }
   ```

4. Use the logger in your `MainGame` class where you see fit. For example, this example logs when your injected implementation of the `LoadContent` method finishes.

   ```c#
   private readonly ILogger<MainGame> logger;
   
   private void LoadContent()
   {
   	Surface surface = new Surface("image.png", SurfaceType.PNG);
   	texture = new Texture(renderer, surface);
       logger.LogInformation("LoadContent finished.");
   }
   ```

5. Build and run to see logs that look like below. These are trace events occurring in the engine.

   ```bash
   dotnet build
   dotnet run
   
   trce: SharpDL.Graphics.WindowFactory[0]
         Window created. Title = Example 0 - Sandbox, X = 100, Y = 100, Width = 1280, Height = 720, Handle = 2031943582128.
   trce: SharpDL.Graphics.RendererFactory[0]
         Renderer created. Handle = 2031943582928, Window Title = Example 0 - Sandbox, Window Handle = 2031943582128.
   trce: SharpDL.GameEngine[0]
         SDL_Event: SDL_AUDIODEVICEADDED
   ```

## Getting Started (Version 2.0)

### The Game Loop

Calling the `Run` method on the `Game` class will result in the following:

1. `Initialize` will initialize `SDL` with `SDL_INIT_EVERYTHING` flag, initialize `SDL_ttf`, and initialize `SDL_image`.
2. `LoadContent` will perform a no-op.
3. Until the `Quit` method is called, the game loop will call `Update` to update the game state and `Draw` to draw the game state. Note that the game loop uses a [Fixed Time Step](https://gafferongames.com/post/fix_your_timestep/) approach
4. `UnloadContent` will dispose the `Window`, `Renderer`, `SDL_ttf`, `SDL_image`, and `SDL`.

### Creating a Blank Window

1. First create a console application.

   `dotnet new console -o MyFirstSharpDL`

2. Create a `MainGame` class which inherits from `Game` to handle your game logic.

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
