Imports Microsoft.AspNetCore.Mvc.Rendering

Namespace ViewModels
    Public Class CatalogIndexViewModel
        Public Property CatalogItems As IEnumerable(Of CatalogItemViewModel)
        Public Property Brands As IEnumerable(Of SelectListItem)
        Public Property Types As IEnumerable(Of SelectListItem)
        Public Property BrandFilterApplied As Integer?
        Public Property TypesFilterApplied As Integer?
        Public Property PaginationInfo As PaginationInfoViewModel
    End Class
End Namespace
