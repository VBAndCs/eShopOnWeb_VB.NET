Imports Microsoft.eShopWeb.Web.Services
Imports Microsoft.AspNetCore.Mvc

Namespace Controllers.Api
    Public Class CatalogController
        Inherits BaseApiController

        Private ReadOnly _catalogViewModelService As ICatalogViewModelService

        Public Sub New(ByVal catalogViewModelService As ICatalogViewModelService)
            _catalogViewModelService = catalogViewModelService
        End Sub

        <HttpGet>
        Public Async Function List(ByVal brandFilterApplied As Integer?, ByVal typesFilterApplied As Integer?, ByVal page As Integer?) As Task(Of IActionResult)
            Dim itemsPage = 10
            Dim catalogModel = Await _catalogViewModelService.GetCatalogItems(If(page, 0), itemsPage, brandFilterApplied, typesFilterApplied)
            Return Ok(catalogModel)
        End Function

        Private Class CSharpImpl
            <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Class
End Namespace
