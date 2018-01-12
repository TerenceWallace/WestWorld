
Imports System
Imports System.Diagnostics
Imports System.Collections.Generic
Imports System.Threading

Public Class GlobalMembersMain


    Shared Function Main() As Integer

        'create a miner
        Dim Bob As New Miner(EntityType.Miner)

        'create his wife
        Dim Elsa As New Wife(EntityType.Wife)

        'register them with the entity manager
        EntityManager.Instance().RegisterEntity(Bob)
        EntityManager.Instance().RegisterEntity(Elsa)

        'run Bob and Elsa through a few Update calls
        For i As Integer = 0 To 19
            Bob.Update()
            Elsa.Update()

            MessageDispatcher.Instance().DispatchDelayedMessages()
            Thread.Sleep(800)
        Next i

        'wait for a keypress before exiting
        Console.Read()

        Return 0
    End Function
End Class

