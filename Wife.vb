Public Class Wife
    Inherits Entity

    ''' <summary>
    ''' An instance of the state machine class
    ''' </summary>
    Public Property StateMachine As StateMachine(Of Wife)

    Public Property Location As LocationType

    Public Property Cooking As Boolean

    Public Sub New(ByVal id As Integer)
        MyBase.New(id)

        _Location = LocationType.Shack
        _StateMachine = New StateMachine(Of Wife)(Me)
        _StateMachine.Current = DoHouseWork.Instance()
        _StateMachine.Global = WifesGlobalState.Instance()
    End Sub

    Public Overrides Sub Update()
        'set text color to green
        Console.ForegroundColor = ConsoleColor.Green

        _StateMachine.Update()
    End Sub

    Public Overrides Function HandleMessage(ByVal msg As Telegram) As Boolean
        Return _StateMachine.HandleMessage(msg)
    End Function
End Class






