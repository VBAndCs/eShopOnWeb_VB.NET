Option Explicit On
Option Infer On
Option Strict On

Imports System.Linq.Expressions

Namespace Interfaces
    Public Interface ISpecification(Of T)
        ReadOnly Property Criteria As Expression(Of Func(Of T, Boolean))
        ReadOnly Property Includes As List(Of Expression(Of Func(Of T, Object)))
        ReadOnly Property IncludeStrings As List(Of String)
        ReadOnly Property OrderBy As Expression(Of Func(Of T, Object))
        ReadOnly Property OrderByDescending As Expression(Of Func(Of T, Object))
        ReadOnly Property GroupBy As Expression(Of Func(Of T, Object))

        ReadOnly Property Take As Integer
        ReadOnly Property Skip As Integer
        ReadOnly Property IsPagingEnabled As Boolean
    End Interface
End Namespace
