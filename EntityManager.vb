Imports System.Collections.Generic
Imports System.Diagnostics

Public Class EntityManager

    'to facilitate quick lookup the entities are stored in a std::map, in which
    'pointers to entities are cross referenced by their identifying number
    Private _EntityMap As New Dictionary(Of Integer, Entity)

    Private Shared _Instance As New EntityManager

    Private Sub New()
    End Sub

    Public Shared ReadOnly Property Instance As EntityManager
        Get
            Return _Instance
        End Get
    End Property

    ''' <summary>
    ''' This method stores a pointer to the entity in the std::vector
    '''  m_Entities at the index position indicated by the entity's ID
    '''  (makes for faster access)
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub RegisterEntity(ByVal e As Entity)
        _EntityMap.Add(e.UniqueID, e)
    End Sub

    ''' <summary>
    ''' Returns a pointer to the entity with the ID given as a parameter
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEntityFromID(ByVal id As Integer) As Entity
        'find the entity
        Return _EntityMap(id)
    End Function

    ''' <summary>
    '''  Removes the entity from the list
    ''' </summary>
    ''' <param name="p"></param>
    ''' <remarks></remarks>
    Public Sub RemoveEntity(ByVal p As Entity)
        _EntityMap.Remove(p.UniqueID)
    End Sub
End Class
