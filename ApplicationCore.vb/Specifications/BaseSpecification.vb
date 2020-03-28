Imports Microsoft.eShopWeb.ApplicationCore.Interfaces
Imports System.Linq.Expressions
Imports Microsoft.eShopWeb.ApplicationCore.Helpers.Query

Namespace Specifications
    Public MustInherit Class BaseSpecification(Of T)
        Implements ISpecification(Of T)

        Protected Sub New(ByVal criteria As Expression(Of Func(Of T, Boolean)))
            Me.Criteria = criteria
        End Sub

        Public ReadOnly Property Criteria As Expression(Of Func(Of T, Boolean)) Implements ISpecification(Of T).Criteria
        Public ReadOnly Property Includes _
              As New List(Of Expression(Of Func(Of T, Object))) _
             Implements ISpecification(Of T).Includes

        Public ReadOnly Property IncludeStrings As New List(Of String) _
            Implements ISpecification(Of T).IncludeStrings

        Public Property OrderBy As Expression(Of Func(Of T, Object)) Implements ISpecification(Of T).OrderBy
        Public Property OrderByDescending As Expression(Of Func(Of T, Object)) Implements ISpecification(Of T).OrderByDescending
        Public Property GroupBy As Expression(Of Func(Of T, Object)) Implements ISpecification(Of T).GroupBy
        Public Property Take As Integer Implements ISpecification(Of T).Take
        Public Property Skip As Integer Implements ISpecification(Of T).Skip
        Public Property IsPagingEnabled As Boolean = False Implements ISpecification(Of T).IsPagingEnabled

        Protected Overridable Sub AddInclude(ByVal includeExpression As Expression(Of Func(Of T, System.Object)))
            Includes.Add(includeExpression)
        End Sub

        Protected Overridable Sub AddIncludes(Of TProperty)(ByVal includeGenerator As Func(Of IncludeAggregator(Of T), IIncludeQuery(Of T, TProperty)))
            Dim includeQuery = includeGenerator(New IncludeAggregator(Of T)())
            IncludeStrings.AddRange(includeQuery.Paths)
        End Sub

        Protected Overridable Sub AddInclude(ByVal includeString As String)
            IncludeStrings.Add(includeString)
        End Sub

        Protected Overridable Sub ApplyPaging(ByVal skip As Integer, ByVal take As Integer)
            Me.Skip = skip
            Me.Take = take
            IsPagingEnabled = True
        End Sub

        Protected Overridable Sub ApplyOrderBy(ByVal orderByExpression As Expression(Of Func(Of T, Object)))
            OrderBy = orderByExpression
        End Sub

        Protected Overridable Sub ApplyOrderByDescending(ByVal orderByDescendingExpression As Expression(Of Func(Of T, Object)))
            OrderByDescending = orderByDescendingExpression
        End Sub

        Protected Overridable Sub ApplyGroupBy(ByVal groupByExpression As Expression(Of Func(Of T, Object)))
            GroupBy = groupByExpression
        End Sub

    End Class
End Namespace
