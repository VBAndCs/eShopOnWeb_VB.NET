Imports Microsoft.eShopWeb.ApplicationCore.Entities
Imports Microsoft.eShopWeb.ApplicationCore.Interfaces
Imports Microsoft.eShopWeb.Web.Interfaces
Imports Microsoft.eShopWeb.Web.ViewModels
Imports System.Threading.Tasks

Namespace Services
    Public Class CatalogItemViewModelService
        Implements ICatalogItemViewModelService

        Private ReadOnly _catalogItemRepository As IAsyncRepository(Of CatalogItem)

        Public Sub New(ByVal catalogItemRepository As IAsyncRepository(Of CatalogItem))
            _catalogItemRepository = catalogItemRepository
        End Sub

        Public Async Function UpdateCatalogItem(ByVal viewModel As CatalogItemViewModel) As Task Implements ICatalogItemViewModelService.UpdateCatalogItem
            Dim existingCatalogItem = Await _catalogItemRepository.GetByIdAsync(viewModel.Id)
            existingCatalogItem.Update(viewModel.Name, viewModel.Price)
            Await _catalogItemRepository.UpdateAsync(existingCatalogItem)
        End Function
    End Class
End Namespace
