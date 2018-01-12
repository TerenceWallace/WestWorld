Public Class CrudeTimer

    Private Shared _Instance As New CrudeTimer

    Private Timer As New System.Diagnostics.Stopwatch()

    Private Sub New()
        Timer.Start()
    End Sub

    Public Shared ReadOnly Property Instance As CrudeTimer
        Get
            Return _Instance
        End Get
    End Property

    Public ReadOnly Property CurrentTime As Double
        Get
            Return Timer.Elapsed.TotalSeconds
        End Get
    End Property


End Class
