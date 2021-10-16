Imports Microsoft.Extensions.Logging
Imports SharpDL
Imports SharpDL.Graphics

Namespace EX3B_MouseEvents

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

            ' Keyboard Events
            AddHandler mEngine.EventManager.KeyReleased, AddressOf KeyReleased
            AddHandler mEngine.EventManager.KeyPressed, AddressOf KeyPressed

            ' Mouse Events
            AddHandler mEngine.EventManager.MouseButtonPressed, AddressOf MouseButtonPressed
            AddHandler mEngine.EventManager.MouseButtonReleased, AddressOf MouseButtonReleased
            AddHandler mEngine.EventManager.MouseWheelScrolling, AddressOf MouseWheelScrolling
            AddHandler mEngine.EventManager.MouseMoving, AddressOf MouseMoving

        End Sub

        ' Mouse Events
        Public Sub MouseButtonPressed(ByVal sender As Object, ByVal EventArgs As Object)
            mLogger.LogTrace($"MouseButtonPressed event: State = {EventArgs.State}, MouseButton = {EventArgs.MouseButton}.")
        End Sub
        Public Sub MouseButtonReleased(ByVal sender As Object, ByVal EventArgs As Object)
            mLogger.LogTrace($"MouseButtonReleased event: State = {EventArgs.State}, MouseButton = {EventArgs.MouseButton}.")
        End Sub
        Public Sub MouseWheelScrolling(ByVal sender As Object, ByVal EventArgs As Object)
            ' Positive Vertical Scroll is Scroll Up higher the value faster you scrolled, Negative is Scroll Down Lesser the value faster you scrolled
            ' note: I have never seen a Horizontal Scroll device on a mouse, is there a ball scroll?
            mLogger.LogTrace($"MouseWheelScrolling event: Type = {EventArgs.EventType}, HorizontalScrollAmount = {EventArgs.HorizontalScrollAmount}, VerticalScrollAmount = {EventArgs.VerticalScrollAmount}.")
        End Sub
        Public Sub MouseMoving(ByVal sender As Object, ByVal EventArgs As Object)
            ' Apparently SDL dosent capture right mouse button being held as a mousepressed while moving
            ' it does however count middle mouse and left mouse buttons
            mLogger.LogTrace($"MouseMoving event: Type = {EventArgs.EventType}, MouseButtonsPressed = {EventArgs.MouseButtonsPressed.Count}, MouseX = {EventArgs.RelativeToWindowX}, MouseY = {EventArgs.RelativeToWindowY}.")
        End Sub


        ' Keyboard Events.
        Public Sub KeyReleased(ByVal sender As Object, ByVal EventArgs As Object)
            mLogger.LogTrace($"Key released event: State = {EventArgs.State}, VirtualKey = {EventArgs.KeyInformation.VirtualKey}, PhysicalKey = {EventArgs.KeyInformation.PhysicalKey}.")
        End Sub
        Public Sub KeyPressed(ByVal sender As Object, ByVal EventArgs As Object)
            mLogger.LogTrace($"Key pressed event: State = {EventArgs.State}, VirtualKey = {EventArgs.KeyInformation.VirtualKey}, PhysicalKey = {EventArgs.KeyInformation.PhysicalKey}.")
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
