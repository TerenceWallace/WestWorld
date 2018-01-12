

''' <summary>
''' This defines a telegram. A telegram is a data structure that
'''  records information required to dispatch messages. Messages 
'''  are used by game agents to communicate with each other.
''' </summary>
''' <remarks></remarks>
Public Class Telegram

    Public Property DispatchTime As Double

    Public Property Sender As Integer

    Public Property Receiver As Integer

    Public Property Message As Integer

    Public Property ExtraInfo As Integer

    Public Sub New(ByVal inTime As Double, ByVal inSender As Integer, ByVal inReceiver As Integer, ByVal inMessage As Integer, ByVal inExtraInfo As Integer)
        _DispatchTime = inTime
        _Sender = inSender
        _Receiver = inReceiver
        _Message = inMessage
        _ExtraInfo = inExtraInfo
    End Sub

End Class
