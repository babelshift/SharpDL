Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Logging                'NuGet Package
Imports Microsoft.Extensions.Logging.Console        'NuGet Package

Imports SharpDL                                     'NuGet Package
Imports SharpDL.Shared                              'NuGet Package

Module Main

    Function Main(ByVal cmdArgs() As String) As Integer
        'MsgBox("The Main procedure is starting the application.")
        Dim returnValue As Integer = 0
        ' See if there are any arguments.
        If cmdArgs.Length > 0 Then
            For argNum As Integer = 0 To UBound(cmdArgs, 1)
                ' Insert code to examine cmdArgs(argNum) and take
                ' appropriate action based on its value.
            Next
        End If
        ' Insert call to appropriate starting place in your code.

        Dim servProvider As ServiceProvider = GetServiceProvider()
        Dim mGame As IGame = servProvider.GetService(Of IGame)()
        mGame.Run()

        ' On return, assign appropriate value to returnValue.
        ' 0 usually means successful completion.
        MsgBox("The application is terminating with error level " &
             CStr(returnValue) & ".")
        Return returnValue
    End Function

    Private Function GetServiceProvider() As ServiceProvider
        Dim varServices As ServiceCollection = New ServiceCollection()
        ConfigureServices(varServices)
        Dim varServProvider As ServiceProvider = varServices.BuildServiceProvider()
        Return varServProvider
    End Function

    Private Sub ConfigureServices(ByVal vServices As ServiceCollection)
        vServices.AddSharpGame(Of EX_BlankWindow.MainGame)().AddLogging(Sub(config)
                                                                            config.AddConsole()
                                                                        End Sub).Configure(Of LoggerFilterOptions)(Sub(options)
                                                                                                                       options.AddFilter(Of ConsoleLoggerProvider)(Nothing, LogLevel.Trace)
                                                                                                                   End Sub)
    End Sub

End Module

