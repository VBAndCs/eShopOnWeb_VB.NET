Option Explicit On
Option Infer On
Option Strict On

Imports Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate

Namespace Interfaces

    Public Interface IOrderRepository
        Inherits IAsyncRepository(Of Order)
        Function GetByIdWithItemsAsync(id As Integer) As Task(Of Order)
    End Interface
End Namespace
