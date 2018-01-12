

''' <summary>
''' Base class for a game object
''' </summary>
''' <remarks></remarks>
Public MustInherit Class Entity

    'every entity must have a unique identifying number
    Private _UniqueID As Integer

    'this is the next valid ID. Each time a Entity is instantiated
    'this value is updated
    Private Shared _NextID As Integer = 0

    Public Shared ReadOnly Property NextID As Integer
        Get
            Return _NextID
        End Get
    End Property

    Private Shared _Random As Random = New Random

    Friend Shared ReadOnly Property Random As Random
        Get
            If _Random Is Nothing Then _Random = New Random(Date.Now.Millisecond)
            Return _Random
        End Get
    End Property

    ''' <summary>
    '''  This must be called within each constructor to make sure the ID is set
    '''  correctly. It verifies that the value passed to the method is greater
    '''   or equal to the next valid ID, before setting the ID and incrementing
    '''  the next valid ID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UniqueID As Integer
        Get
            Return _UniqueID
        End Get
        Set(value As Integer)
            'make sure the val is equal to or greater than the next available ID
            _UniqueID = value

            _NextID = _UniqueID + 1
        End Set
    End Property

    Protected Sub New(ByVal id As Integer)
        _UniqueID = id
    End Sub

    ''' <summary>
    ''' All entities must implement an update function
    ''' </summary>
    ''' <remarks></remarks>
    Public MustOverride Sub Update()

    Public MustOverride Function HandleMessage(ByVal msg As Telegram) As Boolean
     

End Class
