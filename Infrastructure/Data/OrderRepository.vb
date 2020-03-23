Option Explicit On
Option Infer On
Option Strict On

Imports Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate
Imports Microsoft.eShopWeb.ApplicationCore.Interfaces
Imports Microsoft.EntityFrameworkCore

Namespace Data
    Public Class OrderRepository
        Inherits EfRepository(Of Order)
        Implements IOrderRepository
        Public Sub New(dbContext As CatalogContext)
            MyBase.New(dbContext)
        End Sub

        Public Function GetByIdWithItemsAsync(id As Integer) As Task(Of Order) Implements IOrderRepository.GetByIdWithItemsAsync
            Return _dbContext.Orders.
                        Include(Function(o) o.OrderItems).
                        Include($"{NameOf(Order.OrderItems)}.{NameOf(OrderItem.ItemOrdered)}").
                        FirstOrDefaultAsync(Function(x) x.Id = id)
        End Function
    End Class
End Namespace
