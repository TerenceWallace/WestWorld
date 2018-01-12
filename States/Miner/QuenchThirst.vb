

Public Class QuenchThirst
    Inherits State(Of Miner)

    Private Shared _Instance As New QuenchThirst

    Public Shared ReadOnly Property Instance As QuenchThirst
        Get
            Return _Instance
        End Get
    End Property

    Private Sub New()
    End Sub


    Public Overrides Sub Enter(ByVal p As Miner)
        If p.Location <> LocationType.Saloon Then
            p.Location = LocationType.Saloon

            Console.Write(vbLf)
            Console.Write(GetNameOfEntity(p.UniqueID))
            Console.Write(": ")
            Console.Write("Boy, ah sure is thusty! Walking to the saloon")

        End If
    End Sub

    Public Overrides Sub Execute(ByVal p As Miner)
        If p.Thirsty() Then
            p.BuyAndDrinkAWhiskey()


            Console.Write(vbLf)
            Console.Write(GetNameOfEntity(p.UniqueID))
            Console.Write(": ")
            Console.Write("That's mighty fine sippin' liquer")

            p.StateMachine.ChangeState(EnterMineAndDigForNugget.Instance())

        Else

            Console.Write(vbLf & "ERROR!" & vbLf & "ERROR!" & vbLf & "ERROR!")

        End If
    End Sub

    Public Overrides Sub [Exit](ByVal p As Miner)

        Console.Write(vbLf)
        Console.Write(GetNameOfEntity(p.UniqueID))
        Console.Write(": ")
        Console.Write("Leaving the saloon, feelin' good")

    End Sub
End Class

