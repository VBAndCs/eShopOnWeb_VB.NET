Option Explicit On
Option Infer On
Option Strict On

Imports Microsoft.EntityFrameworkCore
Imports Microsoft.eShopWeb.ApplicationCore.Entities
Imports Microsoft.eShopWeb.ApplicationCore.Interfaces

Namespace Data
    Public Class SpecificationEvaluator(Of T As BaseEntity)
        Public Shared Function GetQuery(inputQuery As IQueryable(Of T), specification As ISpecification(Of T)) As IQueryable(Of T)
            Dim query = inputQuery

            ' modify the IQueryable using the specification's criteria expression
            If specification.Criteria IsNot Nothing Then
                query = query.Where(specification.Criteria)
            End If

            ' Includes all expression-based includes
            query = specification.Includes.Aggregate(
                    query,
                    Function(current, include) current.Include(include)
                )

            ' Include any string-based include statements
            query = specification.IncludeStrings.Aggregate(
                    query,
                    Function(current, include) current.Include(include)
                )

            ' Apply ordering if expressions are set
            If specification.OrderBy IsNot Nothing Then
                query = query.OrderBy(specification.OrderBy)
            ElseIf specification.OrderByDescending IsNot Nothing Then
                query = query.OrderByDescending(specification.OrderByDescending)
            End If

            If specification.GroupBy IsNot Nothing Then
                query = query.GroupBy(specification.GroupBy).
                      SelectMany(Function(x) x)
            End If

            ' Apply paging if enabled
            If specification.IsPagingEnabled Then
                query = query.Skip(specification.Skip).
                                       Take(specification.Take)
            End If

            Return query
        End Function
    End Class
End Namespace
