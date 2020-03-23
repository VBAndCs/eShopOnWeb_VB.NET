Imports Microsoft.AspNetCore.Mvc.Rendering
Imports Microsoft.eShopWeb.ApplicationCore.Entities
Imports Microsoft.eShopWeb.ApplicationCore.Interfaces
Imports Microsoft.eShopWeb.ApplicationCore.Specifications
Imports Microsoft.eShopWeb.Web.ViewModels
Imports Microsoft.Extensions.Logging

Namespace Services
    Public Class CatalogViewModelService
        Implements ICatalogViewModelService

        Private ReadOnly _logger As ILogger(Of CatalogViewModelService)
        Private ReadOnly _itemRepository As IAsyncRepository(Of CatalogItem)
        Private ReadOnly _brandRepository As IAsyncRepository(Of CatalogBrand)
        Private ReadOnly _typeRepository As IAsyncRepository(Of CatalogType)
        Private ReadOnly _uriComposer As IUriComposer

        Public Sub New(ByVal loggerFactory As ILoggerFactory, ByVal itemRepository As IAsyncRepository(Of CatalogItem), ByVal brandRepository As IAsyncRepository(Of CatalogBrand), ByVal typeRepository As IAsyncRepository(Of CatalogType), ByVal uriComposer As IUriComposer)
            _logger = loggerFactory.CreateLogger(Of CatalogViewModelService)()
            _itemRepository = itemRepository
            _brandRepository = brandRepository
            _typeRepository = typeRepository
            _uriComposer = uriComposer
        End Sub

        Public Async Function GetCatalogItems(ByVal pageIndex As Integer, ByVal itemsPage As Integer, ByVal brandId As Integer?, ByVal typeId As Integer?) As Task(Of CatalogIndexViewModel) _
            Implements ICatalogViewModelService.GetCatalogItems

            _logger.LogInformation("GetCatalogItems called.")
            Dim filterSpecification = New CatalogFilterSpecification(brandId, typeId)
            Dim filterPaginatedSpecification = New CatalogFilterPaginatedSpecification(itemsPage * pageIndex, itemsPage, brandId, typeId)
            Dim itemsOnPage = Await _itemRepository.ListAsync(filterPaginatedSpecification)
            Dim totalItems = Await _itemRepository.CountAsync(filterSpecification)

            Dim vm = New CatalogIndexViewModel() With {
                .CatalogItems = itemsOnPage.[Select](
                   Function(i) New CatalogItemViewModel() With {
                    .Id = i.Id,
                    .Name = i.Name,
                    .PictureUri = _uriComposer.ComposePicUri(i.PictureUri),
                    .Price = i.Price
                }),
                .Brands = Await GetBrands(),
                .Types = Await GetTypes(),
                .BrandFilterApplied = If(brandId, 0),
                .TypesFilterApplied = If(typeId, 0),
                .PaginationInfo = New PaginationInfoViewModel() With {
                    .ActualPage = pageIndex,
                    .ItemsPerPage = itemsOnPage.Count,
                    .TotalItems = totalItems,
                    .TotalPages = Integer.Parse(Math.Ceiling((CDec(totalItems) / itemsPage)).ToString())
                }
            }
            vm.PaginationInfo.[Next] = If((vm.PaginationInfo.ActualPage = vm.PaginationInfo.TotalPages - 1), "is-disabled", "")
            vm.PaginationInfo.Previous = If((vm.PaginationInfo.ActualPage = 0), "is-disabled", "")
            Return vm
        End Function

        Public Async Function GetBrands() As Task(Of IEnumerable(Of SelectListItem)) _
            Implements ICatalogViewModelService.GetBrands

            _logger.LogInformation("GetBrands called.")
            Dim brands = Await _brandRepository.ListAllAsync()
            Dim items = New List(Of SelectListItem) From {
                New SelectListItem() With {
                    .Value = Nothing,
                    .Text = "All",
                    .Selected = True
                }
            }

            For Each brand As CatalogBrand In brands
                items.Add(New SelectListItem() With {
                    .Value = brand.Id.ToString(),
                    .Text = brand.Brand
                })
            Next

            Return items
        End Function

        Public Async Function GetTypes() As Task(Of IEnumerable(Of SelectListItem)) _
            Implements ICatalogViewModelService.GetTypes

            _logger.LogInformation("GetTypes called.")
            Dim types = Await _typeRepository.ListAllAsync()
            Dim items = New List(Of SelectListItem) From {
                New SelectListItem() With {
                    .Value = Nothing,
                    .Text = "All",
                    .Selected = True
                }
            }

            For Each type As CatalogType In types
                items.Add(New SelectListItem() With {
                    .Value = type.Id.ToString(),
                    .Text = type.Type
                })
            Next

            Return items
        End Function
    End Class
End Namespace
