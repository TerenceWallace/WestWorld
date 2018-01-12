

Public Class DoHouseWork
    Inherits State(Of Wife)

    Private Shared _Instance As New DoHouseWork

    Private Sub New()
    End Sub

    Public Shared ReadOnly Property Instance As DoHouseWork
        Get
            Return _Instance
        End Get
    End Property

    Public Overrides Sub Enter(ByVal p As Wife)
    End Sub

    Public Overrides Sub Execute(ByVal p As Wife)

        Dim Chore As Integer = Entity.Random.Next(0, 2)
        Select Case Chore
            Case 0
                Console.Write(vbLf)
                Console.Write(GetNameOfEntity(p.UniqueID))
                Console.Write(": Moppin' the floor")

            Case 1
                Console.Write(vbLf)
                Console.Write(GetNameOfEntity(p.UniqueID))
                Console.Write(": Washin' the dishes")

            Case 2
                Console.Write(vbLf)
                Console.Write(GetNameOfEntity(p.UniqueID))
                Console.Write(": Makin' the bed")

        End Select
    End Sub

    Public Overrides Sub [Exit](ByVal p As Wife)
    End Sub

    Public Overrides Function OnMessage(ByVal p As Wife, ByVal msg As Telegram) As Boolean
        Return False
    End Function

End Class

