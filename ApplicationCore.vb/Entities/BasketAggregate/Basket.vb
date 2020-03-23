Imports Microsoft.eShopWeb.ApplicationCore.Interfaces

Namespace Entities.BasketAggregate
    Public Class Basket
        Inherits BaseEntity
        Implements IAggregateRoot

        Public Property BuyerId As String
            Get
                Return _BuyerId
            End Get
            Private Set
                _BuyerId = Value
            End Set
        End Property

        Private ReadOnly _items As List(Of BasketItem) = New List(Of BasketItem)()
        Private _BuyerId As String

        Public ReadOnly Property Items As IReadOnlyCollection(Of BasketItem)
            Get
                Return _items.AsReadOnly()
            End Get
        End Property

        Public Sub New(ByVal buyerId As String)
            Me.BuyerId = buyerId
        End Sub

        Public Sub AddItem(ByVal catalogItemId As Integer, ByVal unitPrice As Decimal, ByVal Optional quantity As Integer = 1)
            If Not Items.Any(Function(i) i.CatalogItemId = catalogItemId) Then
                _items.Add(New BasketItem(catalogItemId, quantity, unitPrice))
                Return
            End If

            Dim existingItem = Items.FirstOrDefault(Function(i) i.CatalogItemId = catalogItemId)
            existingItem.AddQuantity(quantity)
        End Sub

        Public Sub RemoveEmptyItems()
            _items.RemoveAll(Function(i) i.Quantity = 0)
        End Sub

        Public Sub SetNewBuyerId(ByVal buyerId As String)
            Me.BuyerId = buyerId
        End Sub
    End Class
End Namespace
