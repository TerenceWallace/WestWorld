Public Module EntityFunctions

    Public Function GetNameOfEntity(ByVal n As Integer) As String
        Select Case n
            Case EntityType.Miner

                Return "Miner Bob"

            Case EntityType.Wife

                Return "Elsa"

            Case Else

                Return "UNKNOWN!"
        End Select
    End Function

    Public Function GetMessage(ByVal n As Integer) As String
        Select Case n
            Case MessageType.HiHoneyImHome

                Return "HiHoneyImHome"

            Case MessageType.StewReady

                Return "StewReady"

            Case Else

                Return "Not recognized!"
        End Select
    End Function

End Module


