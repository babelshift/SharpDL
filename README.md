SharpDL
=======

Sharp DirectMedia Layer (XNA-like Framework for SDL)

Overview
--------

SharpDL aims to provide a managed, cross-platform, XNA-like framework with graphics, input, audio, and event systems based on the SDL2 library. Games created with this library should strive to be lightweight and portable (in both .NET and Mono). Currently, SharpDL only supports utilizing the built-in SDL 2D rendering system and does not support any OpenGL bindings.

Important!
----------

Because SharpDL is written in C# intended for compilation in .NET or Mono, utilizing the native SDL2 libraries requires marshalling of data types through P/Invoke. This also means that any memory allocated in "native land" (ie: in the SDL libraries) will require strict adherence to good disposal mechanisms in the managed code. Failing to free any allocated resources will result in memory leaks.

For more information about the wrapper around SDL2 in order to make our P/Invoke possible, see the [SDL2-CS] [1] GitHub page.

  [1]: https://github.com/flibitijibibo/SDL2-CS        "SDL2-CS"

Code Organization
-----------------

SharpDL consists of four main projects: SharpDL, SharpDL.Events, SharpDL.Graphics, and SharpDL.Input. A project for audio is not yet available.

The main SharpDL project is responsible for the base `Game` class, `GameTime`, and some experimental classes such as `Logger` and `Timer`.

SharpDL.Events is responsible for various `EventArgs` implementations that are based on the `SDL_Event` union. For example, the [SDL_Event] [2] union is processed in the SharpDL's `Run` loop, `EventArgs` of the proper type are created, and an `event` of that handles that type is fired. This allows for a more .NET-like event system.

  [2]: http://wiki.libsdl.org/SDL_Event        "SDL_Event"

SharpDL.Graphics contains any class that is responsible for graphical operations such as creating a SDL_Window, rendering with a SDL_Renderer, creating a SDL_Surface, or loading a SDL_Texture. Examples of some of these classes are `Color`, `Font`, `Renderer`, `Window`, `Texture`, `TrueTypeText`, and more. Some classes do not have direct relations to SDL structures such as `Vector`, which represents an X,Y coordinate in a 2D space.

SharpDL.Input contains classes to handle Keyboard and Mouse input by capturing mapped SDL structures into .NET-style enumerators. Joystick and controller input is not yet available.

Examples
--------

The base `Game` class offers options to initialize SDL, create windows, create renderers, and process events. 

1. Inherit from the SharpDL.Game class.

2. Override the `Initialize` method. Call `base.Initialize()` to initialize SDL.

2. `SDL_CreateWindow` is wrapped by the `Window` class. Creating a window:
  
        string title = "My Window"
        int x = 0;
        int y = 0;
        int width = 640;
        int height = 480;
        WindowFlags flags = WindowFlags.Shown | WindowFlags.GrabbedInputFocus;
    
        Window window = new Window(title, x, y, width, height, flags);

3. `SDL_CreateRenderer` is wrapped by the `Renderer` class. Creating a renderer:
    
        int index = -1;
        RendererFlags flags = RendererFlags.RendererAccelerated;
    
        Renderer renderer = new Renderer(window, index, flags);
        renderer.SetRenderLogicalSize(640, 480); // or however large we want to render to

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
