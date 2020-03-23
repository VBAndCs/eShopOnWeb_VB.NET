Imports Microsoft.eShopWeb.ApplicationCore.Interfaces
Imports System.Linq.Expressions

Namespace Helpers.Query
    Public Class IncludeAggregator(Of TEntity)
        Public Function Include(Of TProperty)(ByVal selector As Expression(Of Func(Of TEntity, TProperty))) As IncludeQuery(Of TEntity, TProperty)
            Dim visitor = New IncludeVisitor()
            visitor.Visit(selector)
            Dim pathMap = New Dictionary(Of IIncludeQuery, String)()
            Dim query = New IncludeQuery(Of TEntity, TProperty)(pathMap)

            If Not String.IsNullOrEmpty(visitor.Path) Then
                pathMap(query) = visitor.Path
            End If

            Return query
        End Function
    End Class
End Namespace
