Imports Microsoft.eShopWeb.ApplicationCore.Interfaces
Imports Microsoft.eShopWeb.ApplicationCore.Specifications
Imports Ardalis.GuardClauses
Imports Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate
Imports Microsoft.eShopWeb.ApplicationCore.Ardalis.GuardClauses

Namespace Services
    Public Class BasketService
        Implements IBasketService

        Private ReadOnly _basketRepository As IAsyncRepository(Of Basket)
        Private ReadOnly _logger As IAppLogger(Of BasketService)

        Public Sub New(ByVal basketRepository As IAsyncRepository(Of Basket), ByVal logger As IAppLogger(Of BasketService))
            _basketRepository = basketRepository
            _logger = logger
        End Sub

        Public Async Function AddItemToBasket(
                                    ByVal basketId As Integer, ByVal catalogItemId As Integer,
                                    ByVal price As Decimal, ByVal Optional quantity As Integer = 1
                               ) As Task Implements IBasketService.AddItemToBasket

            Dim basket = Await _basketRepository.GetByIdAsync(basketId)
            basket.AddItem(catalogItemId, price, quantity)
            Await _basketRepository.UpdateAsync(basket)
        End Function

        Public Async Function DeleteBasketAsync(
                                     ByVal basketId As Integer
                               ) As Task Implements IBasketService.DeleteBasketAsync

            Dim basket = Await _basketRepository.GetByIdAsync(basketId)
            Await _basketRepository.DeleteAsync(basket)
        End Function

        Public Async Function GetBasketItemCountAsync(
                                     ByVal userName As String
                               ) As Task(Of Integer) Implements IBasketService.GetBasketItemCountAsync

            Guard.Against.NullOrEmpty(userName, NameOf(userName))
            Dim basketSpec = New BasketWithItemsSpecification(userName)
            Dim basket = (Await _basketRepository.ListAsync(basketSpec)).FirstOrDefault()

            If basket Is Nothing Then
                _logger.LogInformation($"No basket found for {userName}")
                Return 0
            End If

            Dim count As Integer = basket.Items.Sum(Function(i) i.Quantity)
            _logger.LogInformation($"Basket for {userName} has {count} items.")
            Return count
        End Function

        Public Async Function SetQuantities(
                                     ByVal basketId As Integer, ByVal quantities As Dictionary(Of String, Integer)
                               ) As Task Implements IBasketService.SetQuantities

            Guard.Against.Null(quantities, NameOf(quantities))
            Dim basket = Await _basketRepository.GetByIdAsync(basketId)
            Guard.Against.NullBasket(basketId, basket)
            Dim quantity As Integer

            For Each item In basket.Items
                If quantities.TryGetValue(item.Id.ToString(), quantity) Then
                    If _logger IsNot Nothing Then _logger.LogInformation($"Updating quantity of item ID:{item.Id} to {quantity}.")
                    item.SetNewQuantity(quantity)
                End If
            Next

            basket.RemoveEmptyItems()
            Await _basketRepository.UpdateAsync(basket)
        End Function

        Public Async Function TransferBasketAsync(ByVal anonymousId As String,
                                                  ByVal userName As String
                              ) As Task Implements IBasketService.TransferBasketAsync

            Guard.Against.NullOrEmpty(anonymousId, NameOf(anonymousId))
            Guard.Against.NullOrEmpty(userName, NameOf(userName))
            Dim basketSpec = New BasketWithItemsSpecification(anonymousId)
            Dim basket = (Await _basketRepository.ListAsync(basketSpec)).FirstOrDefault()
            If basket Is Nothing Then Return
            basket.SetNewBuyerId(userName)
            Await _basketRepository.UpdateAsync(basket)
        End Function
    End Class
End Namespace
