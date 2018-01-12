
''' <summary>
''' Abstract base class to define an interface for a state
''' </summary>
Public MustInherit Class State(Of T)

    Public Property Color As ConsoleColor

    Public Overridable Sub Dispose()
    End Sub

    ''' <summary>
    ''' This will execute when the state is entered
    ''' </summary>
    ''' <param name="p"></param>
    Public MustOverride Sub Enter(ByVal p As T)

    ''' <summary>
    ''' This is the states normal update function
    ''' </summary>
    ''' <param name="p"></param>
    Public MustOverride Sub Execute(ByVal p As T)

    ''' <summary>
    ''' This will execute when the state is exited. 
    ''' </summary>
    ''' <param name="p"></param>
    Public MustOverride Sub [Exit](ByVal p As T)

    ''' <summary>
    ''' executes if the agent receives a message from the message dispatcher
    ''' </summary>
    ''' <param name="p1"></param>
    ''' <param name="p2"></param>
    Public Overridable Function OnMessage(ByVal p1 As T, ByVal p2 As Telegram) As Boolean
        Return False
    End Function
End Class
