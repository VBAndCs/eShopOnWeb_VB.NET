Imports Microsoft.eShopWeb.ApplicationCore.Interfaces

Namespace Services
    Public Class UriComposer
        Implements IUriComposer

        Private ReadOnly _catalogSettings As CatalogSettings

        Public Sub New(ByVal catalogSettings As CatalogSettings)
            _catalogSettings = catalogSettings
        End Sub

        Public Function ComposePicUri(ByVal uriTemplate As String
                         ) As String Implements IUriComposer.ComposePicUri

            Return uriTemplate.Replace("http://catalogbaseurltobereplaced", _catalogSettings.CatalogBaseUrl)
        End Function

    End Class
End Namespace
