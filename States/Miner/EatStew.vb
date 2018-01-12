Public Class EatStew
    Inherits State(Of Miner)

    Private Shared _Instance As New EatStew

    Public Shared ReadOnly Property Instance As EatStew
        Get
            Return _Instance
        End Get
    End Property

    Private Sub New()
    End Sub

    Public Overrides Sub Enter(ByVal p As Miner)

        Console.Write(vbLf)
        Console.Write(GetNameOfEntity(p.UniqueID))
        Console.Write(": ")
        Console.Write("Smells Reaaal goood Elsa!")

    End Sub

    Public Overrides Sub Execute(ByVal p As Miner)

        Console.Write(vbLf)
        Console.Write(GetNameOfEntity(p.UniqueID))
        Console.Write(": ")
        Console.Write("Tastes real good too!")

        p.StateMachine.RevertToPreviousState()
    End Sub

    Public Overrides Sub [Exit](ByVal p As Miner)

        Console.Write(vbLf)
        Console.Write(GetNameOfEntity(p.UniqueID))
        Console.Write(": ")
        Console.Write("Thankya li'lle lady. Ah better get back to whatever ah wuz doin'")

    End Sub

    Public Overrides Function OnMessage(ByVal p As Miner, ByVal msg As Telegram) As Boolean
        'send msg to global message handler
        Return False
    End Function
End Class
