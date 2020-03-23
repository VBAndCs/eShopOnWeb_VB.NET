Imports Microsoft.eShopWeb.ApplicationCore.Entities
Imports Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate
Imports Microsoft.eShopWeb.ApplicationCore.Interfaces
Imports Microsoft.eShopWeb.ApplicationCore.Specifications
Imports Microsoft.eShopWeb.Web.Interfaces
Imports Microsoft.eShopWeb.Web.Pages.Basket

Namespace Services
    Public Class BasketViewModelService
        Implements IBasketViewModelService

        Private ReadOnly _basketRepository As IAsyncRepository(Of Basket)
        Private ReadOnly _uriComposer As IUriComposer
        Private ReadOnly _itemRepository As IAsyncRepository(Of CatalogItem)

        Public Sub New(ByVal basketRepository As IAsyncRepository(Of Basket), ByVal itemRepository As IAsyncRepository(Of CatalogItem), ByVal uriComposer As IUriComposer)
            _basketRepository = basketRepository
            _uriComposer = uriComposer
            _itemRepository = itemRepository
        End Sub

        Public Async Function GetOrCreateBasketForUser(ByVal userName As String) As Task(Of BasketViewModel) Implements IBasketViewModelService.GetOrCreateBasketForUser
            Dim basketSpec = New BasketWithItemsSpecification(userName)
            Dim basket = (Await _basketRepository.ListAsync(basketSpec)).FirstOrDefault()

            If basket Is Nothing Then
                Return Await CreateBasketForUser(userName)
            End If

            Return Await CreateViewModelFromBasket(basket)
        End Function

        Private Async Function CreateViewModelFromBasket(ByVal basket As Basket) As Task(Of BasketViewModel)
            Dim viewModel = New BasketViewModel()
            viewModel.Id = basket.Id
            viewModel.BuyerId = basket.BuyerId
            viewModel.Items = Await GetBasketItems(basket.Items)
            Return viewModel
        End Function

        Private Async Function CreateBasketForUser(ByVal userId As String) As Task(Of BasketViewModel)
            Dim basket = New Basket(userId)
            Await _basketRepository.AddAsync(basket)
            Return New BasketViewModel() With {
                .BuyerId = basket.BuyerId,
                .Id = basket.Id,
                .Items = New List(Of BasketItemViewModel)()
            }
        End Function

        Private Async Function GetBasketItems(ByVal basketItems As IReadOnlyCollection(Of BasketItem)) As Task(Of List(Of BasketItemViewModel))
            Dim items = New List(Of BasketItemViewModel)()

            For Each item In basketItems
                Dim itemModel = New BasketItemViewModel With {
                    .Id = item.Id,
                    .UnitPrice = item.UnitPrice,
                    .Quantity = item.Quantity,
                    .CatalogItemId = item.CatalogItemId
                }
                Dim catalogItem = Await _itemRepository.GetByIdAsync(item.CatalogItemId)
                itemModel.PictureUrl = _uriComposer.ComposePicUri(catalogItem.PictureUri)
                itemModel.ProductName = catalogItem.Name
                items.Add(itemModel)
            Next

            Return items
        End Function
    End Class
End Namespace
