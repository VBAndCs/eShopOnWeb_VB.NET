Option Explicit On
Option Infer On
Option Strict On

Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata.Builders
Imports Microsoft.eShopWeb.ApplicationCore.Entities

Namespace Data.Config
    Public Class CatalogBrandConfiguration
        Implements IEntityTypeConfiguration(Of CatalogBrand)
        Public Sub Configure(builder As EntityTypeBuilder(Of CatalogBrand)) Implements IEntityTypeConfiguration(Of CatalogBrand).Configure
            builder.HasKey(Function(ci) ci.Id)

            builder.Property(
                Function(ci) ci.Id
                ).UseHiLo("catalog_brand_hilo").
                  IsRequired()

            builder.Property(
                Function(cb) cb.Brand
                ).IsRequired().
                  HasMaxLength(100)
        End Sub
    End Class
End Namespace
