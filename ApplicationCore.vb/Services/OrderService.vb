Imports Microsoft.eShopWeb.ApplicationCore.Interfaces
Imports Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate
Imports Microsoft.eShopWeb.ApplicationCore.Entities
Imports Ardalis.GuardClauses
Imports Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate
Imports Microsoft.eShopWeb.ApplicationCore.Ardalis.GuardClauses

Namespace Services
    Public Class OrderService
        Implements IOrderService

        Private ReadOnly _orderRepository As IAsyncRepository(Of Order)
        Private ReadOnly _uriComposer As IUriComposer
        Private ReadOnly _basketRepository As IAsyncRepository(Of Basket)
        Private ReadOnly _itemRepository As IAsyncRepository(Of CatalogItem)

        Public Sub New(ByVal basketRepository As IAsyncRepository(Of Basket), ByVal itemRepository As IAsyncRepository(Of CatalogItem), ByVal orderRepository As IAsyncRepository(Of Order), ByVal uriComposer As IUriComposer)
            _orderRepository = orderRepository
            _uriComposer = uriComposer
            _basketRepository = basketRepository
            _itemRepository = itemRepository
        End Sub

        Public Async Function CreateOrderAsync(ByVal basketId As Integer,
                                       ByVal shippingAddress As Address
                               ) As Task Implements IOrderService.CreateOrderAsync

            Dim basket = Await _basketRepository.GetByIdAsync(basketId)
            Guard.Against.NullBasket(basketId, basket)
            Dim items = New List(Of OrderItem)()

            For Each item In basket.Items
                Dim catalogItem = Await _itemRepository.GetByIdAsync(item.CatalogItemId)
                Dim itemOrdered = New CatalogItemOrdered(catalogItem.Id, catalogItem.Name, _uriComposer.ComposePicUri(catalogItem.PictureUri))
                Dim orderItem = New OrderItem(itemOrdered, item.UnitPrice, item.Quantity)
                items.Add(orderItem)
            Next

            Dim order = New Order(basket.BuyerId, shippingAddress, items)
            Await _orderRepository.AddAsync(order)
        End Function
    End Class
End Namespace
