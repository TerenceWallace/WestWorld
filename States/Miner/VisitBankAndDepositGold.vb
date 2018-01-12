Imports Microsoft.VisualBasic
Imports System

Public Class VisitBankAndDepositGold
    Inherits State(Of Miner)

    Private Shared _Instance As New VisitBankAndDepositGold

    Public Shared ReadOnly Property Instance As VisitBankAndDepositGold
        Get
            Return _Instance
        End Get
    End Property

    Private Sub New()
    End Sub


    Public Overrides Sub Enter(ByVal p As Miner)
        'on entry the miner makes sure he is located at the bank
        If p.Location <> LocationType.Bank Then

            Console.Write(vbLf)
            Console.Write(GetNameOfEntity(p.UniqueID))
            Console.Write(": ")
            Console.Write("Goin' to the bank. Yes siree")

            p.Location = LocationType.Bank
        End If
    End Sub

    Public Overrides Sub Execute(ByVal p As Miner)
        'deposit the gold
        p.Wealth += p.GoldCarried
        p.GoldCarried = 0


        Console.Write(vbLf)
        Console.Write(GetNameOfEntity(p.UniqueID))
        Console.Write(": ")
        Console.Write("Depositing gold. Total savings now: ")
        Console.Write(p.Wealth())

        'wealthy enough to have a well earned rest?
        If p.Wealth() >= Common.ComfortLevel Then

            Console.Write(vbLf)
            Console.Write(GetNameOfEntity(p.UniqueID))
            Console.Write(": ")
            Console.Write("WooHoo! Rich enough for now. Back home to mah li'lle lady")

            p.StateMachine.ChangeState(GoHomeAndSleepTilRested.Instance())

        Else 'otherwise get more gold
            p.StateMachine.ChangeState(EnterMineAndDigForNugget.Instance())
        End If

    End Sub

    Public Overrides Sub [Exit](ByVal p As Miner)
        Console.Write(vbLf)
        Console.Write(GetNameOfEntity(p.UniqueID))
        Console.Write(": ")
        Console.Write("Leavin' the bank")
    End Sub

    Public Overrides Function OnMessage(ByVal p As Miner, ByVal msg As Telegram) As Boolean
        'send msg to global message handler
        Return False
    End Function

End Class

