Imports System

Namespace Extensions
    Module CacheHelpers
        Public Shared ReadOnly DefaultCacheDuration As TimeSpan = TimeSpan.FromSeconds(30)
        Private Shared ReadOnly _itemsKeyTemplate As String = "items-{0}-{1}-{2}-{3}"

        Function GenerateCatalogItemCacheKey(ByVal pageIndex As Integer, ByVal itemsPage As Integer, ByVal brandId As Integer?, ByVal typeId As Integer?) As String
            Return String.Format(_itemsKeyTemplate, pageIndex, itemsPage, brandId, typeId)
        End Function

        Function GenerateBrandsCacheKey() As String
            Return "brands"
        End Function

        Function GenerateTypesCacheKey() As String
            Return "types"
        End Function
    End Module
End Namespace
