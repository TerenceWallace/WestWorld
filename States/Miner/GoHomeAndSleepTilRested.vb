Imports Microsoft.VisualBasic
Imports System

Public Class GoHomeAndSleepTilRested
    Inherits State(Of Miner)

    Private Shared _Instance As New GoHomeAndSleepTilRested

    Public Shared ReadOnly Property Instance As GoHomeAndSleepTilRested
        Get
            Return _Instance
        End Get
    End Property

    Private Sub New()
    End Sub

    Public Overrides Sub Enter(ByVal p As Miner)
        If p.Location() <> LocationType.shack Then

            Console.Write(vbLf)
            Console.Write(GetNameOfEntity(p.UniqueID))
            Console.Write(": ")
            Console.Write("Walkin' home")

            p.Location = LocationType.Shack

            'let the wife know I'm home
            MessageDispatcher.Instance().DispatchMessage(MessageDispatcher.SEND_MSG_IMMEDIATELY, p.UniqueID, EntityType.Wife, MessageType.HiHoneyImHome, MessageDispatcher.NO_ADDITIONAL_INFO) 'the message - ID of recipient - ID of sender - time delay

        End If
    End Sub

    Public Overrides Sub Execute(ByVal p As Miner)
        'if miner is not fatigued start to dig for nuggets again.
        If Not p.Fatigued() Then

            Console.Write(vbLf)
            Console.Write(GetNameOfEntity(p.UniqueID))
            Console.Write(": ")
            Console.Write("What a God darn fantastic nap! Time to find more gold")

            p.StateMachine.ChangeState(EnterMineAndDigForNugget.Instance())

        Else
            'sleep
            p.DecreaseFatigue()

            Console.Write(vbLf)
            Console.Write(GetNameOfEntity(p.UniqueID))
            Console.Write(": ")
            Console.Write("ZZZZ... ")
        End If
    End Sub

    Public Overrides Sub [Exit](ByVal p As Miner)
    End Sub

    Public Overrides Function OnMessage(ByVal pMiner As Miner, ByVal msg As Telegram) As Boolean
        Console.ForegroundColor = ConsoleColor.Gray

        Select Case msg.Message
            Case MessageType.StewReady

                Console.Write(vbLf & "Message handled by ")
                Console.Write(GetNameOfEntity(pMiner.UniqueID))
                Console.Write(" at time: ")
                Console.Write(CrudeTimer.Instance().CurrentTime())


                Console.ForegroundColor = ConsoleColor.Red

                Console.Write(vbLf)
                Console.Write(GetNameOfEntity(pMiner.UniqueID))
                Console.Write(": Okay Hun, ahm a comin'!")


                pMiner.StateMachine.ChangeState(EatStew.Instance())

                Return True

        End Select 'end switch

        Return False 'send message to global message handler
    End Function
End Class

