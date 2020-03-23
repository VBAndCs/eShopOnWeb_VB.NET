Imports System.Collections.Generic
Imports System.Threading.Tasks
Imports Microsoft.AspNetCore.Mvc.Rendering
Imports Microsoft.eShopWeb.Web.ViewModels
Imports Microsoft.Extensions.Caching.Memory
Imports System

Namespace Services
    Public Class CachedCatalogViewModelService
        Implements ICatalogViewModelService

        Private ReadOnly _cache As IMemoryCache
        Private ReadOnly _catalogViewModelService As CatalogViewModelService
        Private Shared ReadOnly _brandsKey As String = "brands"
        Private Shared ReadOnly _typesKey As String = "types"
        Private Shared ReadOnly _itemsKeyTemplate As String = "items-{0}-{1}-{2}-{3}"
        Private Shared ReadOnly _defaultCacheDuration As TimeSpan = TimeSpan.FromSeconds(30)

        Public Sub New(ByVal cache As IMemoryCache, ByVal catalogViewModelService As CatalogViewModelService)
            _cache = cache
            _catalogViewModelService = catalogViewModelService
        End Sub

        Public Async Function GetBrands() As Task(Of IEnumerable(Of SelectListItem)) _
            Implements ICatalogViewModelService.GetBrands

            Return Await _cache.GetOrCreateAsync(_brandsKey, Async Function(entry)
                                                                 entry.SlidingExpiration = _defaultCacheDuration
                                                                 Return Await _catalogViewModelService.GetBrands()
                                                             End Function)
        End Function

        Public Async Function GetCatalogItems(ByVal pageIndex As Integer, ByVal itemsPage As Integer, ByVal brandId As Integer?, ByVal typeId As Integer?) As Task(Of CatalogIndexViewModel) _
            Implements ICatalogViewModelService.GetCatalogItems

            Dim cacheKey As String = String.Format(_itemsKeyTemplate, pageIndex, itemsPage, brandId, typeId)
            Return Await _cache.GetOrCreateAsync(cacheKey, Async Function(entry)
                                                               entry.SlidingExpiration = _defaultCacheDuration
                                                               Return Await _catalogViewModelService.GetCatalogItems(pageIndex, itemsPage, brandId, typeId)
                                                           End Function)
        End Function

        Public Async Function GetTypes() As Task(Of IEnumerable(Of SelectListItem)) _
            Implements ICatalogViewModelService.GetTypes

            Return Await _cache.GetOrCreateAsync(_typesKey, Async Function(entry)
                                                                entry.SlidingExpiration = _defaultCacheDuration
                                                                Return Await _catalogViewModelService.GetTypes()
                                                            End Function)
        End Function
    End Class
End Namespace
