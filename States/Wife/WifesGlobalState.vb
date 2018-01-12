Imports Microsoft.VisualBasic
Imports System

Public Class WifesGlobalState
    Inherits State(Of Wife)

    Private Shared _Instance As New WifesGlobalState

    Private Sub New()
    End Sub

    Public Shared ReadOnly Property Instance As WifesGlobalState
        Get
            Return _Instance
        End Get
    End Property

    Public Overrides Sub Enter(ByVal p As Wife)
    End Sub

    Public Overrides Sub Execute(ByVal p As Wife)
        '1 in 10 chance of needing the bathroom
        If Entity.Random.Next(0, 100) <= 10 Then
            p.StateMachine.ChangeState(VisitBathroom.Instance())
        End If
    End Sub

    Public Overrides Sub [Exit](ByVal p As Wife)
    End Sub

    Public Overrides Function OnMessage(ByVal p As Wife, ByVal msg As Telegram) As Boolean
        Console.ForegroundColor = ConsoleColor.Gray

        Select Case msg.Message
            Case MessageType.HiHoneyImHome

                Console.Write(vbLf & "Message handled by ")
                Console.Write(GetNameOfEntity(p.UniqueID))
                Console.Write(" at time: ")
                Console.Write(CrudeTimer.Instance().CurrentTime)

                Console.ForegroundColor = ConsoleColor.Green

                Console.Write(vbLf)
                Console.Write(GetNameOfEntity(p.UniqueID))
                Console.Write(": Hi honey. Let me make you some of mah fine country stew")

                p.StateMachine.ChangeState(CookStew.Instance())

                Return True

        End Select 'end switch

        Return False
    End Function
End Class

