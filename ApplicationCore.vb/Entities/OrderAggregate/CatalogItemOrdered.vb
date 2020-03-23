Option Explicit On
Option Infer On
Option Strict On

Imports Ardalis.GuardClauses

Namespace Entities.OrderAggregate
    ''' <summary>
    ''' Represents a snapshot of the item that was ordered. If catalog item details change, details of
    ''' the item that was part of a completed order should not change.
    ''' </summary>
    Public Class CatalogItemOrdered
        Private _CatalogItemId As Integer
        Private _ProductName As String
        Private _PictureUri As String

        Public Sub New(catalogItemId As Integer, productName As String, pictureUri As String)
            Guard.Against.OutOfRange(catalogItemId, NameOf(catalogItemId), 1, Integer.MaxValue)
            Guard.Against.NullOrEmpty(productName, NameOf(productName))
            Guard.Against.NullOrEmpty(pictureUri, NameOf(pictureUri))

            Me.CatalogItemId = catalogItemId
            Me.ProductName = productName
            Me.PictureUri = pictureUri
        End Sub

        Private Sub New()
            ' required by EF
        End Sub

        Public Property CatalogItemId As Integer
            Get
                Return _CatalogItemId
            End Get
            Private Set
                _CatalogItemId = Value
            End Set
        End Property

        Public Property ProductName As String
            Get
                Return _ProductName
            End Get
            Private Set
                _ProductName = Value
            End Set
        End Property

        Public Property PictureUri As String
            Get
                Return _PictureUri
            End Get
            Private Set
                _PictureUri = Value
            End Set
        End Property
    End Class
End Namespace
