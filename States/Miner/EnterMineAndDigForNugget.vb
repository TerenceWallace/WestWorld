
''' <summary>
'''  In this state the miner will walk to a goldmine and pick up a nugget
'''   of gold. If the miner already has a nugget of gold he'll change state
'''   to VisitBankAndDepositGold. If he gets thirsty he'll change state
'''  to QuenchThirst
''' </summary>
Public Class EnterMineAndDigForNugget
    Inherits State(Of Miner)


    Private Shared _Instance As New EnterMineAndDigForNugget

    Public Shared ReadOnly Property Instance As EnterMineAndDigForNugget
        Get
            Return _Instance
        End Get
    End Property

    Private Sub New()
    End Sub

    Public Overrides Sub Enter(ByVal p As Miner)
        'if the miner is not already located at the goldmine, he must
        'change location to the gold mine
        If p.Location() <> LocationType.goldmine Then

            Console.Write(vbLf)
            Console.Write(GetNameOfEntity(p.UniqueID))
            Console.Write(": ")
            Console.Write("Walkin' to the goldmine")

            p.Location = LocationType.Goldmine
        End If
    End Sub

    Public Overrides Sub Execute(ByVal p As Miner)
        'if the miner is at the goldmine he digs for gold until he
        'is carrying in excess of MaxNuggets. If he gets thirsty during
        'his digging he packs up work for a while and changes state to
        'gp to the saloon for a whiskey.
        p.GoldCarried += 1

        p.IncreaseFatigue()


        Console.Write(vbLf)
        Console.Write(GetNameOfEntity(p.UniqueID))
        Console.Write(": ")
        Console.Write("Pickin' up a nugget")

        'if enough gold mined, go and put it in the bank
        If p.PocketsFull() Then
            p.StateMachine.ChangeState(VisitBankAndDepositGold.Instance())
        End If

        If p.Thirsty() Then
            p.StateMachine.ChangeState(QuenchThirst.Instance())
        End If
    End Sub

    Public Overrides Sub [Exit](ByVal p As Miner)

        Console.Write(vbLf)
        Console.Write(GetNameOfEntity(p.UniqueID))
        Console.Write(": ")
        Console.Write("Ah'm leavin' the goldmine with mah pockets full o' sweet gold")

    End Sub

End Class

