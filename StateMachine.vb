Imports System.Diagnostics

''' <summary>
''' State machine class. Inherit from this class and create some 
''' states to give your agents FSM functionality
''' </summary>
''' <typeparam name="T"></typeparam>
''' <remarks></remarks>
Public Class StateMachine(Of T)

    ''' <summary>
    ''' Pointer to the agent that owns this instance
    ''' </summary>
    Public Property Owner As T

    Public Property Current As State(Of T)

    ''' <summary>
    '''  Record of the last state the agent was in
    ''' </summary>
    Public Property Previous As State(Of T)

    ''' <summary>
    ''' This is called every time the FSM is updated
    ''' </summary>
    Public Property [Global] As State(Of T)

    Public Sub New(ByVal owner As T)
        _Owner = owner
        _Current = Nothing
        _Previous = Nothing
        _Global = Nothing
    End Sub


    ''' <summary>
    ''' call this to update the FSM
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Update()
        'if a global state exists, call its execute method, else do nothing
        If _Global IsNot Nothing Then
            _Global.Execute(_Owner)
        End If

        'same for the current state
        If _Current IsNot Nothing Then
            _Current.Execute(_Owner)
        End If
    End Sub

    ''' <summary>
    ''' Change to a new state
    ''' </summary>
    ''' <param name="pNewState"></param>
    Public Sub ChangeState(ByVal pNewState As State(Of T))

        'keep a record of the previous state
        _Previous = _Current

        'call the exit method of the existing state
        _Current.Exit(_Owner)

        'change state to the new state
        _Current = pNewState

        'call the entry method of the new state
        _Current.Enter(_Owner)
    End Sub

    ''' <summary>
    ''' Change state back to the previous state
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RevertToPreviousState()
        ChangeState(_Previous)
    End Sub

    Public Function Compare(ByVal st As State(Of T)) As Boolean
        Return _Current Is st
    End Function

    Public Function HandleMessage(ByVal msg As Telegram) As Boolean
        'first see if the current state is valid and that it can handle
        'the message
        If _Current IsNot Nothing AndAlso _Current.OnMessage(_Owner, msg) Then
            Return True
        End If

        'if not, and if a global state has been implemented, send 
        'the message to the global state
        If _Global IsNot Nothing AndAlso _Global.OnMessage(_Owner, msg) Then
            Return True
        End If

        Return False
    End Function

End Class