Imports Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate

Namespace Specifications
    Public NotInheritable Class BasketWithItemsSpecification
        Inherits BaseSpecification(Of Basket)

        Public Sub New(ByVal basketId As Integer)
            MyBase.New(Function(b) b.Id = basketId)
            AddInclude(Function(b) b.Items)
        End Sub
        Public Sub New(ByVal buyerId As String)
            MyBase.New(Function(basket) basket.BuyerId = buyerId)
            AddInclude(NameOf(Basket.Items))
        End Sub
    End Class
End Namespace
