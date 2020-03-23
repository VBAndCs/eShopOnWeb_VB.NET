Option Explicit On
Option Infer On
Option Strict On

Imports Microsoft.EntityFrameworkCore
Imports Microsoft.eShopWeb.ApplicationCore.Entities
Imports Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate
Imports Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate
Imports System.Reflection

Namespace Data

    Public Class CatalogContext
        Inherits DbContext
        Public Sub New(options As DbContextOptions(Of CatalogContext))
            MyBase.New(options)
        End Sub

        Public Property Baskets As DbSet(Of Basket)
        Public Property CatalogItems As DbSet(Of CatalogItem)
        Public Property CatalogBrands As DbSet(Of CatalogBrand)
        Public Property CatalogTypes As DbSet(Of CatalogType)
        Public Property Orders As DbSet(Of Order)
        Public Property OrderItems As DbSet(Of OrderItem)
        Public Property BasketItems As DbSet(Of BasketItem)

        Protected Overrides Sub OnModelCreating(builder As ModelBuilder)
            MyBase.OnModelCreating(builder)
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly())
        End Sub
    End Class
End Namespace
