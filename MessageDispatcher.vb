
''' <summary>
''' A message dispatcher. Manages messages of the type Telegram.
''' </summary>
''' <remarks></remarks>
Public Class MessageDispatcher

    'to make code easier to read
    Public Const SEND_MSG_IMMEDIATELY As Double = 0.0F
    Public Const NO_ADDITIONAL_INFO As Integer = 0

    'a std::set is used as the container for the delayed messages
    'because of the benefit of automatic sorting and avoidance
    'of duplicates. Messages are sorted by their dispatch time.
    Private PriorityQ As New HashSet(Of Telegram)()

    Private Shared _Instance As New MessageDispatcher

    Private Sub New()
    End Sub

    Public Shared ReadOnly Property Instance As MessageDispatcher
        Get
            Return _Instance
        End Get
    End Property


    ''' <summary>
    '''  This method is utilized by DispatchMessage or DispatchDelayedMessages.
    '''  This method calls the message handling member function of the receiving
    '''  entity, pReceiver, with the newly created telegram
    ''' </summary>
    ''' <param name="pReceiver"></param>
    ''' <param name="telegram"></param>
    Private Sub Discharge(ByVal pReceiver As Entity, ByVal telegram As Telegram)
        If Not pReceiver.HandleMessage(telegram) Then
            'telegram could not be handled
            Console.Write("Message not handled")
        End If
    End Sub

    ''' <summary>
    '''  Send a message to another agent. Receiving agent is referenced by ID.
    '''  Given a message, a receiver, a sender and any time delay , this function
    '''  routes the message to the correct agent (if no delay) or stores
    '''  in the message queue to be dispatched at the correct time
    ''' </summary>
    ''' <param name="delay"></param>
    ''' <param name="sender"></param>
    ''' <param name="receiver"></param>
    ''' <param name="msg"></param>
    ''' <param name="ExtraInfo"></param>
    Public Sub DispatchMessage(ByVal delay As Double, ByVal sender As Integer, ByVal receiver As Integer, ByVal msg As Integer, ByVal ExtraInfo As Integer)
        Console.ForegroundColor = ConsoleColor.Gray

        'get pointers to the sender and receiver
        Dim pSender As Entity = EntityManager.Instance().GetEntityFromID(sender)
        Dim pReceiver As Entity = EntityManager.Instance().GetEntityFromID(receiver)

        'make sure the receiver is valid
        If pReceiver Is Nothing Then

            Console.Write(vbLf & "Warning! No Receiver with ID of ")
            Console.Write(receiver)
            Console.Write(" found")

            Return
        End If

        'create the telegram
        Dim telegram As New Telegram(0, sender, receiver, msg, ExtraInfo)

        'if there is no delay, route telegram immediately
        If delay <= 0.0F Then

            Console.Write(vbLf & "Instant telegram dispatched at time: ")
            Console.Write(CrudeTimer.Instance().CurrentTime)
            Console.Write(" by ")
            Console.Write(GetNameOfEntity(pSender.UniqueID))
            Console.Write(" for ")
            Console.Write(GetNameOfEntity(pReceiver.UniqueID))
            Console.Write(". Msg is ")
            Console.Write(GetMessage(msg))

            'send the telegram to the recipient
            Discharge(pReceiver, telegram)

            'else calculate the time when the telegram should be dispatched
        Else
            Dim CurrentTime As Double = CrudeTimer.Instance().CurrentTime

            telegram.DispatchTime = CurrentTime + delay

            'and put it in the queue
            PriorityQ.Add(telegram)


            Console.Write(vbLf & "Delayed telegram from ")
            Console.Write(GetNameOfEntity(pSender.UniqueID))
            Console.Write(" recorded at time ")
            Console.Write(CrudeTimer.Instance().CurrentTime)
            Console.Write(" for ")
            Console.Write(GetNameOfEntity(pReceiver.UniqueID))
            Console.Write(". Msg is ")
            Console.Write(GetMessage(msg))

        End If
    End Sub

    ''' <summary>
    '''   Send out any delayed messages. This method is called each time through   
    '''   the main game loop.
    '''   This function dispatches any telegrams with a timestamp that has
    '''    expired. Any dispatched telegrams are removed from the queue
    ''' </summary>
    Public Sub DispatchDelayedMessages()
        Console.ForegroundColor = ConsoleColor.Gray

        'get current time
        Dim CurrentTime As Double = CrudeTimer.Instance.CurrentTime

        'now peek at the queue to see if any telegrams need dispatching.
        'remove all telegrams from the front of the queue that have gone
        'past their sell by date
        Dim mQueue As HashSet(Of Telegram) = PriorityQ

        For n As Integer = 0 To mQueue.Count - 1
            If mQueue(n).DispatchTime < CurrentTime AndAlso mQueue(n).DispatchTime >= 0 Then
                'read the telegram from the front of the queue
                Dim mTelegram As Telegram = mQueue(n)

                'find the recipient
                Dim pReceiver As Entity = EntityManager.Instance().GetEntityFromID(mTelegram.Receiver)

                Console.Write(vbLf & "Queued telegram ready for dispatch: Sent to ")
                Console.Write(GetNameOfEntity(pReceiver.UniqueID))
                Console.Write(". Msg is ")
                Console.Write(GetMessage(mTelegram.Message))


                'send the telegram to the recipient
                Discharge(pReceiver, mTelegram)

                'remove it from the queue
                PriorityQ.Remove(mTelegram)
            End If
        Next n

    End Sub
End Class