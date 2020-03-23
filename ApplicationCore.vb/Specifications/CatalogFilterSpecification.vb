Imports Microsoft.eShopWeb.ApplicationCore.Entities

Namespace Specifications
    Public Class CatalogFilterSpecification
        Inherits BaseSpecification(Of CatalogItem)

        Public Sub New(ByVal brandId As Integer?, ByVal typeId As Integer?)
            MyBase.New(
                Function(i) (Not brandId.HasValue OrElse i.CatalogBrandId = brandId.Value) AndAlso
                                   (Not typeId.HasValue OrElse i.CatalogTypeId = typeId.Value))
        End Sub
    End Class
End Namespace
