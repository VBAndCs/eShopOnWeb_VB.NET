Option Explicit On
Option Infer On
Option Strict On

Imports Microsoft.eShopWeb.ApplicationCore.Helpers.Query

Namespace Interfaces
    Public Interface IIncludeQuery
        ReadOnly Property PathMap As Dictionary(Of IIncludeQuery, String)
        ReadOnly Property Visitor As IncludeVisitor
        ReadOnly Property Paths As HashSet(Of String)
    End Interface

    Public Interface IIncludeQuery(Of TEntity, Out TPreviousProperty)
        Inherits IIncludeQuery
    End Interface
End Namespace
