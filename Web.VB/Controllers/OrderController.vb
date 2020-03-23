Imports Microsoft.AspNetCore.Authorization
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.eShopWeb.ApplicationCore.Interfaces
Imports Microsoft.eShopWeb.ApplicationCore.Specifications
Imports Microsoft.eShopWeb.Web.ViewModels
Imports System.Linq
Imports System.Threading.Tasks

Namespace Controllers
    <ApiExplorerSettings(IgnoreApi:=True)>
    <Authorize>
    <Route("[controller]/[action]")>
    Public Class OrderController
        Inherits Controller

        Private ReadOnly _orderRepository As IOrderRepository

        Public Sub New(ByVal orderRepository As IOrderRepository)
            _orderRepository = orderRepository
        End Sub

        <HttpGet()>
        Public Async Function MyOrders() As Task(Of IActionResult)
            Dim orders = Await _orderRepository.ListAsync(New CustomerOrdersWithItemsSpecification(User.Identity.Name))
            Dim viewModel = orders.[Select](Function(o) New OrderViewModel() With {
                .OrderDate = o.OrderDate,
                .OrderItems = o.OrderItems?.[Select](Function(oi) New OrderItemViewModel() With {
                    .Discount = 0,
                    .PictureUrl = oi.ItemOrdered.PictureUri,
                    .ProductId = oi.ItemOrdered.CatalogItemId,
                    .ProductName = oi.ItemOrdered.ProductName,
                    .UnitPrice = oi.UnitPrice,
                    .Units = oi.Units
                }).ToList(),
                .OrderNumber = o.Id,
                .ShippingAddress = o.ShipToAddress,
                .Status = "Pending",
                .Total = o.Total()
            })
            Return View(viewModel)
        End Function

        <HttpGet("{orderId}")>
        Public Async Function Detail(ByVal orderId As Integer) As Task(Of IActionResult)
            Dim customerOrders = Await _orderRepository.ListAsync(New CustomerOrdersWithItemsSpecification(User.Identity.Name))
            Dim order = customerOrders.FirstOrDefault(Function(o) o.Id = orderId)

            If order Is Nothing Then
                Return BadRequest("No such order found for this user.")
            End If

            Dim viewModel = New OrderViewModel() With {
                .OrderDate = order.OrderDate,
                .OrderItems = order.OrderItems.[Select](Function(oi) New OrderItemViewModel() With {
                    .Discount = 0,
                    .PictureUrl = oi.ItemOrdered.PictureUri,
                    .ProductId = oi.ItemOrdered.CatalogItemId,
                    .ProductName = oi.ItemOrdered.ProductName,
                    .UnitPrice = oi.UnitPrice,
                    .Units = oi.Units
                }).ToList(),
                .OrderNumber = order.Id,
                .ShippingAddress = order.ShipToAddress,
                .Status = "Pending",
                .Total = order.Total()
            }
            Return View(viewModel)
        End Function
    End Class
End Namespace
