Imports Microsoft.eShopWeb.ApplicationCore.Interfaces

Namespace Helpers.Query
    Public Class IncludeQuery(Of TEntity, TPreviousProperty)
        Implements IIncludeQuery(Of TEntity, TPreviousProperty)

        Public ReadOnly Property PathMap As Dictionary(Of IIncludeQuery, String) =
            New Dictionary(Of IIncludeQuery, String)() Implements IIncludeQuery(Of TEntity, TPreviousProperty).PathMap

        Public ReadOnly Property Visitor As IncludeVisitor =
            New IncludeVisitor() Implements IIncludeQuery(Of TEntity, TPreviousProperty).Visitor

        Public Sub New(ByVal pathMap As Dictionary(Of IIncludeQuery, String))
            Me.PathMap = pathMap
        End Sub

        Public ReadOnly Property Paths As HashSet(Of String) Implements IIncludeQuery(Of TEntity, TPreviousProperty).Paths
            Get
                Return PathMap.[Select](Function(x) x.Value).ToHashSet()
            End Get
        End Property
    End Class
End Namespace
