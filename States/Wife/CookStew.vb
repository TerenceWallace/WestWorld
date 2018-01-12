Public Class CookStew
    Inherits State(Of Wife)

    Private Shared _Instance As New CookStew

    Private Sub New()
    End Sub

    Public Shared ReadOnly Property Instance As CookStew
        Get
            Return _Instance
        End Get
    End Property

    Public Overrides Sub Enter(ByVal p As Wife)
        'if not already cooking put the stew in the oven
        If Not p.Cooking() Then

            Console.Write(vbLf)
            Console.Write(GetNameOfEntity(p.UniqueID))
            Console.Write(": Putting the stew in the oven")

            'send a delayed message myself so that I know when to take the stew out of the oven
            MessageDispatcher.Instance().DispatchMessage(1.5, p.UniqueID, p.UniqueID, MessageType.StewReady, MessageDispatcher.NO_ADDITIONAL_INFO) 'msg - receiver ID - sender ID - time delay

            p.Cooking = True
        End If
    End Sub

    Public Overrides Sub Execute(ByVal p As Wife)

        Console.Write(vbLf)
        Console.Write(GetNameOfEntity(p.UniqueID))
        Console.Write(": Fussin' over food")

    End Sub

    Public Overrides Sub [Exit](ByVal p As Wife)
        Console.ForegroundColor = ConsoleColor.Green


        Console.Write(vbLf)
        Console.Write(GetNameOfEntity(p.UniqueID))
        Console.Write(": Puttin' the stew on the table")

    End Sub

    Public Overrides Function OnMessage(ByVal p As Wife, ByVal inTelegram As Telegram) As Boolean
        Console.ForegroundColor = ConsoleColor.Blue

        Select Case inTelegram.Message
            Case MessageType.StewReady

                Console.Write(vbLf & "Message received by ")
                Console.Write(GetNameOfEntity(p.UniqueID))
                Console.Write(" at time: ")
                Console.Write(CrudeTimer.Instance().CurrentTime)

                Console.ForegroundColor = ConsoleColor.Green

                Console.Write(vbLf)
                Console.Write(GetNameOfEntity(p.UniqueID))
                Console.Write(": StewReady! Lets eat")

                'let hubby know the stew is ready
                MessageDispatcher.Instance().DispatchMessage(MessageDispatcher.SEND_MSG_IMMEDIATELY, p.UniqueID, EntityType.Miner, MessageType.StewReady, MessageDispatcher.NO_ADDITIONAL_INFO)

                p.Cooking = False

                p.StateMachine.ChangeState(DoHouseWork.Instance())

                Return True

        End Select 'end switch

        Return False
    End Function
End Class
