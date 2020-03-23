Imports Microsoft.AspNetCore.Mvc.Rendering
Imports Microsoft.eShopWeb.Web.ViewModels

Namespace Services
    Public Interface ICatalogViewModelService
        Function GetCatalogItems(ByVal pageIndex As Integer, ByVal itemsPage As Integer, ByVal brandId As Integer?, ByVal typeId As Integer?) As Task(Of CatalogIndexViewModel)
        Function GetBrands() As Task(Of IEnumerable(Of SelectListItem))
        Function GetTypes() As Task(Of IEnumerable(Of SelectListItem))
    End Interface
End Namespace
