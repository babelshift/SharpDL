Module mTimer

    Public Class myTimer
        Private Const FRAME_COUNT_MAX As Integer = 255 'Max frame counter storage
        Private Const FRAME_AVERAGE_COUNT As Integer = 100
        Private m_Frames(FRAME_COUNT_MAX) As Double
        Private m_FrameCount As UInteger = 0

        Public Sub New()
            StartTicks = 0
            PausedTicks = 0
            SetTicks = 0
            FrameStart = System.Environment.TickCount
            LastTick = 0
            Paused = 0
            Started = 0
        End Sub

        Private Property StartTicks() As UInt32
        Private Property PausedTicks() As UInt32

        Private Property SetTicks() As UInt32
        Private Property FrameStart() As UInt32
        Private Property LastTick() As UInt32

        Private m_paused As Boolean = False
        Private Property Paused() As Boolean
            Get
                Return (m_paused AndAlso Started)
            End Get
            Set(value As Boolean)
                m_paused = value
            End Set
        End Property
        Private Property Started() As Boolean

        Private Sub ResetMe()
            Paused = False
            Started = False
            StartTicks = 0
            PausedTicks = 0
            SetTicks = 0
        End Sub

        Public Sub StartMe()
            'if were already running reset
            StopMe()
            Started = True
            StartTicks = System.Environment.TickCount
        End Sub

        Public Sub StopMe()
            ResetMe()
        End Sub

        Public Sub PauseMe()
            If Started AndAlso Not Paused Then
                Paused = True
                PausedTicks = System.Environment.TickCount - StartTicks
                StartTicks = 0
            End If
        End Sub

        Public Sub ContinueMe()
            If Started AndAlso Paused Then
                Paused = False
                StartTicks = System.Environment.TickCount - PausedTicks
                PausedTicks = 0
            End If
        End Sub

        Public Function GetFPSDelta() As UInt32
            Return (System.Environment.TickCount - FrameStart)
        End Function

        Public Sub NextFrame()
            If m_FrameCount = FRAME_COUNT_MAX Then
                m_FrameCount = 0
            Else
                m_FrameCount += 1
            End If
            FrameStart = System.Environment.TickCount
        End Sub

        Public Function CalculateFPS() As Double
            Dim _fps As Double = 0.0F
            Dim _frametime As Double = GetFPSDelta()
            m_Frames(m_FrameCount Mod FRAME_AVERAGE_COUNT) = _frametime
            Dim count As Integer = 0
            If m_FrameCount < FRAME_AVERAGE_COUNT Then
                count = m_FrameCount - 1 'for the vb loop
            Else
                count = FRAME_AVERAGE_COUNT - 1 'for the vb loop
            End If
            Dim average_value As Double = 0.0F
            For i As Integer = 0 To count
                average_value += m_Frames(i)
            Next
            average_value = average_value / count
            If average_value > 0 Then
                _fps = 1000.0F / average_value
            Else
                _fps = 9000.0F
            End If
            NextFrame()
            Return _fps
        End Function

        Public Function GetDelta() As UInt32
            Return System.Environment.TickCount - StartTicks
        End Function

    End Class

End Module