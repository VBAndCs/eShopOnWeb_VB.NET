Option Explicit On
Option Infer On
Option Strict On

Imports Microsoft.eShopWeb.ApplicationCore.Interfaces

Namespace Entities
    Public Class CatalogBrand
        Inherits BaseEntity
        Implements IAggregateRoot

        Private _Brand As String

        Public Property Brand As String
            Get
                Return _Brand
            End Get
            Private Set
                _Brand = Value
            End Set
        End Property

        Public Sub New(brand As String)
            Me.Brand = brand
        End Sub
    End Class
End Namespace
