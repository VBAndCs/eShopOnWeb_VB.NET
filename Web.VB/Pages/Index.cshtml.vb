Imports Microsoft.AspNetCore.Mvc.RazorPages
Imports Microsoft.eShopWeb.Web.Services
Imports Microsoft.eShopWeb.Web.ViewModels
Imports Vazor

Namespace Pages

    Public Class IndexModel : Inherits PageModel

        Const Title = "Catalog"

        Public ReadOnly Property ViewName As String
            Get
                ViewData("Title") = Title
                _CatalogItems = CatalogModel.CatalogItems.ToArray()
                Dim html = Me.GetVbXml().ParseZML()
                Return VazorPage.CreateNew("Index", "Pages", Title, html)
            End Get
        End Property

        Public ReadOnly Property CatalogItems As CatalogItemViewModel()

        Private ReadOnly _catalogViewModelService As ICatalogViewModelService

        Public Sub New(ByVal catalogViewModelService As ICatalogViewModelService)
            _catalogViewModelService = catalogViewModelService
        End Sub

        Public Property CatalogModel As CatalogIndexViewModel = New CatalogIndexViewModel()

        Public Async Function OnGet(ByVal catalogModel As CatalogIndexViewModel, ByVal pageId As Integer?) As Task
            Me.CatalogModel = Await _catalogViewModelService.
                GetCatalogItems(
                    If(pageId, 0),
                    Constants.ITEMS_PER_PAGE,
                    catalogModel.BrandFilterApplied,
                    catalogModel.TypesFilterApplied)
        End Function

    End Class
End Namespace