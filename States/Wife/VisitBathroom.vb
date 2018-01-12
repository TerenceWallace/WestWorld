

Public Class VisitBathroom
    Inherits State(Of Wife)

    Private Shared _Instance As New VisitBathroom

    Private Sub New()
    End Sub

    Public Shared ReadOnly Property Instance As VisitBathroom
        Get
            Return _Instance
        End Get
    End Property

    Public Overrides Sub Enter(ByVal p As Wife)
        Console.Write(vbLf)
        Console.Write(GetNameOfEntity(p.UniqueID))
        Console.Write(": Walkin' to the can. Need to powda mah pretty li'lle nose")
    End Sub

    Public Overrides Sub Execute(ByVal p As Wife)
        Console.Write(vbLf)
        Console.Write(GetNameOfEntity(p.UniqueID))
        Console.Write(": Ahhhhhh! Sweet relief!")

        p.StateMachine.RevertToPreviousState()
    End Sub

    Public Overrides Sub [Exit](ByVal p As Wife)
        Console.Write(vbLf)
        Console.Write(GetNameOfEntity(p.UniqueID))
        Console.Write(": Leavin' the Jon")
    End Sub

    Public Overrides Function OnMessage(ByVal p As Wife, ByVal msg As Telegram) As Boolean
        Return False
    End Function

End Class

