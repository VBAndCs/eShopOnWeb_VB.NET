Imports System.Linq.Expressions

Namespace Helpers.Query
    Public Class IncludeVisitor
        Inherits ExpressionVisitor

        Public Property Path As String = String.Empty

        Protected Overrides Function VisitMember(ByVal node As MemberExpression) As Expression
            Path = If(String.IsNullOrEmpty(Path), node.Member.Name, $"{node.Member.Name}.{Path}")
            Return MyBase.VisitMember(node)
        End Function
    End Class
End Namespace
