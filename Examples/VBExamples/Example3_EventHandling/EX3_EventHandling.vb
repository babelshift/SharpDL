Imports Microsoft.Extensions.Logging
Imports SharpDL
Imports SharpDL.Graphics

Namespace EX3_EventHandling

    Public Class MainGame : Implements IGame

        Private ReadOnly mLogger As ILogger(Of MainGame)
        Private mEngine As IGameEngine
        Private mWindow As IWindow
        Private mRenderer As IRenderer

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

        Private Sub Initialize()
            mWindow = mEngine.WindowFactory.CreateWindow("Example 3 - Event Handling")
            mRenderer = mEngine.RendererFactory.CreateRenderer(mWindow)
            mRenderer.SetRenderLogicalSize(1280, 720)

            ' Add the event handler for key released
            AddHandler mEngine.EventManager.KeyReleased, AddressOf KeyReleased

        End Sub

        ' Event handler for key released.
        Public Sub KeyReleased(ByVal sender As Object, ByVal EventArgs As Object)
            mLogger.LogTrace($"Key released event: State = {EventArgs.State}, VirtualKey = {EventArgs.KeyInformation.VirtualKey}, PhysicalKey = {EventArgs.KeyInformation.PhysicalKey}.")
        End Sub

        Private Sub LoadContent()
            '
        End Sub
        Private Sub Update(vGameTime As GameTime)
            '
        End Sub
        Private Sub Draw(vGameTime As GameTime)
            mRenderer.RenderPresent()
        End Sub

        Private Sub UnloadContent()
            '
        End Sub
    End Class

End Namespace
