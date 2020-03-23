Imports Microsoft.eShopWeb.ApplicationCore.Entities

Namespace Specifications
    Public Class CatalogFilterPaginatedSpecification
        Inherits BaseSpecification(Of CatalogItem)

        Public Sub New(ByVal skip As Integer, ByVal take As Integer, ByVal brandId As Integer?, ByVal typeId As Integer?)
            MyBase.New(
                Function(i) (Not brandId.HasValue OrElse i.CatalogBrandId = brandId.Value) AndAlso
                                   (Not typeId.HasValue OrElse i.CatalogTypeId = typeId.Value))
            ApplyPaging(skip, take)
        End Sub
    End Class
End Namespace
