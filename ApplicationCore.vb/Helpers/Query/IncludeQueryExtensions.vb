Imports Microsoft.eShopWeb.ApplicationCore.Interfaces
Imports System.Linq.Expressions
Imports System.Runtime.CompilerServices

Namespace Helpers.Query
    Module IncludeQueryExtensions

        <Extension()>
        Function Include(Of TEntity, TPreviousProperty, TNewProperty)(ByVal query As IIncludeQuery(Of TEntity, TPreviousProperty), ByVal selector As Expression(Of Func(Of TEntity, TNewProperty))) As IIncludeQuery(Of TEntity, TNewProperty)
            query.Visitor.Visit(selector)
            Dim includeQuery = New IncludeQuery(Of TEntity, TNewProperty)(query.PathMap)
            query.PathMap(includeQuery) = query.Visitor.Path
            Return includeQuery
        End Function

        <Extension()>
        Function ThenInclude(Of TEntity, TPreviousProperty, TNewProperty)(ByVal query As IIncludeQuery(Of TEntity, TPreviousProperty), ByVal selector As Expression(Of Func(Of TPreviousProperty, TNewProperty))) As IIncludeQuery(Of TEntity, TNewProperty)
            query.Visitor.Visit(selector)

            If String.IsNullOrEmpty(query.Visitor.Path) Then
                Return New IncludeQuery(Of TEntity, TNewProperty)(query.PathMap)
            End If

            Dim pathMap = query.PathMap
            Dim existingPath = pathMap(query)
            pathMap.Remove(query)
            Dim includeQuery = New IncludeQuery(Of TEntity, TNewProperty)(query.PathMap)
            pathMap(includeQuery) = $"{existingPath}.{query.Visitor.Path}"
            Return includeQuery
        End Function

        <Extension()>
        Function ThenInclude(Of TEntity, TPreviousProperty, TNewProperty)(ByVal query As IIncludeQuery(Of TEntity, IEnumerable(Of TPreviousProperty)), ByVal selector As Expression(Of Func(Of TPreviousProperty, TNewProperty))) As IIncludeQuery(Of TEntity, TNewProperty)
            query.Visitor.Visit(selector)

            If String.IsNullOrEmpty(query.Visitor.Path) Then
                Return New IncludeQuery(Of TEntity, TNewProperty)(query.PathMap)
            End If

            Dim pathMap = query.PathMap
            Dim existingPath = pathMap(query)
            pathMap.Remove(query)
            Dim includeQuery = New IncludeQuery(Of TEntity, TNewProperty)(query.PathMap)
            pathMap(includeQuery) = $"{existingPath}.{query.Visitor.Path}"
            Return includeQuery
        End Function
    End Module
End Namespace
