Option Explicit On
Option Infer On
Option Strict On

Imports Microsoft.eShopWeb.ApplicationCore.Entities
Imports Microsoft.Extensions.Logging

Namespace Data
    Public Class CatalogContextSeed
        Public Shared Async Function SeedAsync(
                              catalogContext As CatalogContext,
                              loggerFactory As ILoggerFactory,
                              Optional retry As Integer? = 0
                            ) As Task

            Dim retryForAvailability As Integer = retry.Value
            Try
                ' TODO: Only run this if using a real database
                ' context.Database.Migrate();

                If Not catalogContext.CatalogBrands.Any() Then
                    catalogContext.CatalogBrands.AddRange(
                        GetPreconfiguredCatalogBrands()
                     )

                    Await catalogContext.SaveChangesAsync()
                End If

                If Not catalogContext.CatalogTypes.Any() Then
                    catalogContext.CatalogTypes.AddRange(
                            GetPreconfiguredCatalogTypes()
                    )

                    Await catalogContext.SaveChangesAsync()
                End If

                If Not catalogContext.CatalogItems.Any() Then
                    catalogContext.CatalogItems.AddRange(
                            GetPreconfiguredItems()
                     )

                    Await catalogContext.SaveChangesAsync()
                End If

            Catch ex As Exception
                If retryForAvailability < 10 Then
                    retryForAvailability += 1
                    Dim log = loggerFactory.CreateLogger(Of CatalogContextSeed)()
                    log.LogError(ex.Message)
                    Task.Run(Sub() SeedAsync(catalogContext, loggerFactory, retryForAvailability)).Wait()
                End If
                Throw
            End Try
        End Function

        Private Shared Function GetPreconfiguredCatalogBrands() As IEnumerable(Of CatalogBrand)
            Return New List(Of CatalogBrand)() From {
                New CatalogBrand("Azure"),
                New CatalogBrand(".NET"),
                New CatalogBrand("Visual Studio"),
                New CatalogBrand("SQL Server"),
                New CatalogBrand("Other")}
        End Function

        Private Shared Function GetPreconfiguredCatalogTypes() As IEnumerable(Of CatalogType)
            Return New List(Of CatalogType)() From {
                New CatalogType("Mug"),
                New CatalogType("T-Shirt"),
                New CatalogType("Sheet"),
                New CatalogType("USB Memory Stick")}
        End Function

        Private Shared Function GetPreconfiguredItems() As IEnumerable(Of CatalogItem)
            Return New List(Of CatalogItem)() From {
                New CatalogItem(2, 2, ".NET Bot Black Sweatshirt", ".NET Bot Black Sweatshirt", 19.5D, "http://catalogbaseurltobereplaced/images/products/1.png"),
                New CatalogItem(1, 2, ".NET Black & White Mug", ".NET Black & White Mug", 8.5D, "http://catalogbaseurltobereplaced/images/products/2.png"),
                New CatalogItem(2, 5, "Prism White T-Shirt", "Prism White T-Shirt", 12, "http://catalogbaseurltobereplaced/images/products/3.png"),
                New CatalogItem(2, 2, ".NET Foundation Sweatshirt", ".NET Foundation Sweatshirt", 12, "http://catalogbaseurltobereplaced/images/products/4.png"),
                New CatalogItem(3, 5, "Roslyn Red Sheet", "Roslyn Red Sheet", 8.5D, "http://catalogbaseurltobereplaced/images/products/5.png"),
                New CatalogItem(2, 2, ".NET Blue Sweatshirt", ".NET Blue Sweatshirt", 12, "http://catalogbaseurltobereplaced/images/products/6.png"),
                New CatalogItem(2, 5, "Roslyn Red T-Shirt", "Roslyn Red T-Shirt", 12, "http://catalogbaseurltobereplaced/images/products/7.png"),
                New CatalogItem(2, 5, "Kudu Purple Sweatshirt", "Kudu Purple Sweatshirt", 8.5D, "http://catalogbaseurltobereplaced/images/products/8.png"),
                New CatalogItem(1, 5, "Cup<T> White Mug", "Cup<T> White Mug", 12, "http://catalogbaseurltobereplaced/images/products/9.png"),
                New CatalogItem(3, 2, ".NET Foundation Sheet", ".NET Foundation Sheet", 12, "http://catalogbaseurltobereplaced/images/products/10.png"),
                New CatalogItem(3, 2, "Cup<T> Sheet", "Cup<T> Sheet", 8.5D, "http://catalogbaseurltobereplaced/images/products/11.png"),
                New CatalogItem(2, 5, "Prism White TShirt", "Prism White TShirt", 12, "http://catalogbaseurltobereplaced/images/products/12.png")}
        End Function
    End Class
End Namespace
