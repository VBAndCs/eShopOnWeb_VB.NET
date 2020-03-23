Option Explicit On
Option Infer On
Option Strict On

Imports Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate

Namespace Interfaces
    Public Interface IOrderService
        Function CreateOrderAsync(basketId As Integer, shippingAddress As Address) As Task
    End Interface
End Namespace
