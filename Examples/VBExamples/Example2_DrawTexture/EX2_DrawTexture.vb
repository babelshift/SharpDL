Imports Microsoft.Extensions.Logging
Imports SharpDL
Imports SharpDL.Graphics


Namespace EX2_DrawTexture

    Public Class MainGame : Implements IGame

        Private ReadOnly mLogger As ILogger(Of MainGame)
        Private mEngine As IGameEngine
        Private mWindow As IWindow
        Private mRenderer As IRenderer

        Private mTextureGitLogo As Texture
        Private mTextureVisualStudioLogo As Texture
        Private mTextureYboc As Texture

        Public Sub New(engine As IGameEngine, Optional logger As ILogger(Of MainGame) = Nothing)
            Me.mEngine = engine
            Me.mLogger = logger
            With mEngine
                .Initialize = Sub() Initialize()
                .LoadContent = Sub() LoadContent()
                .Update = Sub(gameTime) Update(gameTime)
                .Draw = Sub(gameTime) Draw(gameTime)
                .UnloadContent = Sub() UnloadContent()
            End With
        End Sub

        Public Sub Run() Implements IGame.Run
            mEngine.Start(GameEngineInitializeType.Everything)
        End Sub

        Public Sub Initialize()
            mWindow = mEngine.WindowFactory.CreateWindow("Example 2 - Draw Texture")
            mRenderer = mEngine.RendererFactory.CreateRenderer(mWindow)
            mRenderer.SetRenderLogicalSize(1280, 720)
        End Sub

        Private Sub LoadContent()
            ' Creates an in memory SDL Surface from the PNG at the passed path
            Dim surGitLogo As Surface = New Surface("Content/logo_git.png", SurfaceType.PNG)
            Dim SurVisualStudioLogo As Surface = New Surface("Content/logo_vs_2019.png", SurfaceType.PNG)
            Dim SurYboc As Surface = New Surface("Content/logo_yboc.png", SurfaceType.PNG)

            ' Creates a GPU-driven SDL texture using the initialized renderer and created surface
            mTextureGitLogo = New Texture(mRenderer, surGitLogo)
            mTextureVisualStudioLogo = New Texture(mRenderer, SurVisualStudioLogo)
            mTextureYboc = New Texture(mRenderer, SurYboc)
        End Sub

        Private Sub Update(vGameTime As GameTime)

        End Sub
        Private Sub Draw(vGameTime As GameTime)
            ' Clear the screen on each iteration so that we don't get stale renders
            mRenderer.ClearScreen()

            ' Draw the Git logo at (0,0) And -45 degree angle rotated around the center (calculated Vector)
            mTextureGitLogo.Draw(0, 0, -45, New Vector(mTextureGitLogo.Width / 2, mTextureGitLogo.Height / 2))

            ' Draw the Git logo at (300,300) And 45 degree angle rotated around the center (calculated Vector)
            mTextureGitLogo.Draw(300, 300, 45, New Vector(mTextureGitLogo.Width / 2, mTextureGitLogo.Height / 2))

            ' Draw the Visual Studio logo at (700, 400) with no rotation
            mTextureVisualStudioLogo.Draw(700, 400)

            ' Draw the YBOC logo at (900, 900) cropped to a 50x50 rectangle with (0,0) being the starting point
            mTextureYboc.Draw(800, 600, New Rectangle(0, 0, 50, 50))

            ' Update the rendered state of the screen
            mRenderer.RenderPresent()
        End Sub

        Private Sub UnloadContent()
            mTextureGitLogo.Dispose()
            mTextureVisualStudioLogo.Dispose()
            mTextureYboc.Dispose()              'you forgot 1 in your cs ex
        End Sub
    End Class


End Namespace
