Imports Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate
Imports Microsoft.eShopWeb.ApplicationCore.Helpers.Query
Namespace Specifications
    Public Class CustomerOrdersWithItemsSpecification
        Inherits BaseSpecification(Of Order)

        Public Sub New(ByVal buyerId As String)
            MyBase.New(Function(o) o.BuyerId = buyerId)
            AddIncludes(
                Function(query) query.
                        Include(Function(o) o.OrderItems).
                        ThenInclude(Function(i) i.ItemOrdered))
        End Sub
    End Class
End Namespace
