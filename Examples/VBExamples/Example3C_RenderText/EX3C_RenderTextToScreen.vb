Imports Microsoft.Extensions.Logging
Imports SharpDL
Imports SharpDL.Graphics

Namespace EX3C_RenderTextToScreen

    Public Class MainGame : Implements IGame

        Private ReadOnly mLogger As ILogger(Of MainGame)
        Private mEngine As IGameEngine
        Private mWindow As IWindow
        Private mRenderer As IRenderer

        ' Font holder
        Private FPSText As TrueTypeText
        Private CapTimer As New mTimer.myTimer()
        Private MouseCoords As TrueTypeText

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
            mRenderer.SetDrawColor(37, 37, 117, 255)

            ' Set FPS Font file etc
            Dim clrWhite As Color = New Color(255, 255, 255)
            FPSText = TrueTypeTextFactory.CreateTrueTypeText(mRenderer, "Content/z3.ttf", 20, clrWhite, "FPS: 0", 0)
            MouseCoords = TrueTypeTextFactory.CreateTrueTypeText(mRenderer, "Content/z3.ttf", 20, clrWhite, "MouseCoords X: 0, Y: 0", 0)


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
            MouseCoords.UpdateText("MouseCoords X: " & EventArgs.RelativeToWindowX & ", Y: " & EventArgs.RelativeToWindowY)
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
            mRenderer.ClearScreen()
            CapTimer.StartMe() 'Testing FPS timer (always the first thing in the loop after clearing the screen)


            ' Draw our FPS counter in the top left of the screen
            FPSText.UpdateText("FPS: " & CapTimer.CalculateFPS().ToString())
            FPSText.Texture.Draw(3, 1)
            MouseCoords.Texture.Draw(3, FPSText.Texture.Height + 2)

            mRenderer.RenderPresent()
        End Sub

        Private Sub UnloadContent()
            FPSText.Dispose()
            MouseCoords.Dispose()
        End Sub
    End Class

End Namespace
