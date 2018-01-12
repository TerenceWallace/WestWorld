
''' <summary>
''' A class defining a goldminer.
''' </summary>
''' <remarks></remarks>
Public Class Miner
    Inherits Entity

    'an instance of the state machine class
    Public Property StateMachine As StateMachine(Of Miner)

    Public Property Location As LocationType

    'how many nuggets the miner has in his pockets
    Public Property GoldCarried As Integer

    Public Property Wealth As Integer

    'the higher the value, the thirstier the miner
    Private _Thirst As Integer

    'the higher the value, the more tired the miner
    Private _Fatigue As Integer


    Public Sub New(ByVal id As Integer)
        MyBase.New(id)
        _Location = LocationType.Shack
        _GoldCarried = 0
        _Wealth = 0
        _Thirst = 0
        _Fatigue = 0
        _StateMachine = New StateMachine(Of Miner)(Me)
        _StateMachine.Current = GoHomeAndSleepTilRested.Instance()
    End Sub

    ''' <summary>
    ''' Update the miners status
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Update()
        Console.ForegroundColor = ConsoleColor.Red

        _Thirst += 1

        _StateMachine.Update()
    End Sub


    Public Function PocketsFull() As Boolean
        Return _GoldCarried >= Common.MaxNuggets
    End Function


    Public Function Fatigued() As Boolean
        If _Fatigue > Common.TirednessThreshold Then
            Return True
        End If

        Return False
    End Function

    Public Sub DecreaseFatigue()
        _Fatigue -= 1
    End Sub
    Public Sub IncreaseFatigue()
        _Fatigue += 1
    End Sub

    Public Function Thirsty() As Boolean
        If _Thirst >= Common.ThirstLevel Then
            Return True
        End If

        Return False
    End Function

    Public Sub BuyAndDrinkAWhiskey()
        _Thirst = 0
        _Wealth -= 2
    End Sub

    Public Overrides Function HandleMessage(ByVal msg As Telegram) As Boolean
        Return _StateMachine.HandleMessage(msg)
    End Function

End Class