Option Explicit On
Option Infer On
Option Strict On

Imports Microsoft.eShopWeb.ApplicationCore.Interfaces

Namespace Entities
    Public Class CatalogType
        Inherits BaseEntity
        Implements IAggregateRoot

        Private _Type As String

        Public Property Type As String
            Get
                Return _Type
            End Get
            Private Set
                _Type = Value
            End Set
        End Property

        Public Sub New(type As String)
            Me.Type = type
        End Sub
    End Class
End Namespace
