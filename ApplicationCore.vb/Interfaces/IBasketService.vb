Option Explicit On
Option Infer On
Option Strict On

Namespace Interfaces
    Public Interface IBasketService
        Function GetBasketItemCountAsync(userName As String) As Task(Of Integer)
        Function TransferBasketAsync(anonymousId As String, userName As String) As Task
        Function AddItemToBasket(basketId As Integer, catalogItemId As Integer, price As Decimal, Optional quantity As Integer = 1) As Task
        Function SetQuantities(basketId As Integer, quantities As Dictionary(Of String, Integer)) As Task
        Function DeleteBasketAsync(basketId As Integer) As Task
    End Interface
End Namespace
