Imports Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate
Imports System
Imports System.Collections.Generic

Namespace ViewModels
    Public Class OrderViewModel
        Public Property OrderNumber As Integer
        Public Property OrderDate As DateTimeOffset
        Public Property Total As Decimal
        Public Property Status As String
        Public Property ShippingAddress As Address
        Public Property OrderItems As List(Of OrderItemViewModel) = New List(Of OrderItemViewModel)()
    End Class
End Namespace
