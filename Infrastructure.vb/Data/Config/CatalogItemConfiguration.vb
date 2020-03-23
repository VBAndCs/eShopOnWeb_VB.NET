Option Explicit On
Option Infer On
Option Strict On

Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata.Builders
Imports Microsoft.eShopWeb.ApplicationCore.Entities

Namespace Data.Config
    Public Class CatalogItemConfiguration
        Implements IEntityTypeConfiguration(Of CatalogItem)
        Public Sub Configure(builder As EntityTypeBuilder(Of CatalogItem)) Implements IEntityTypeConfiguration(Of CatalogItem).Configure
            builder.ToTable("Catalog")

            builder.Property(
                Function(ci) ci.Id
                ).UseHiLo("catalog_hilo").
                  IsRequired()

            builder.Property(
                Function(ci) ci.Name
                ).IsRequired(True).
                  HasMaxLength(50)

            builder.Property(
                Function(ci) ci.Price
                ).IsRequired(True).
                  HasColumnType("decimal(18,2)")

            builder.Property(
                Function(ci) ci.PictureUri
                ).IsRequired(False)

            builder.HasOne(
                Function(ci) ci.CatalogBrand
                ).WithMany().
                  HasForeignKey(Function(ci) ci.CatalogBrandId)

            builder.HasOne(
                Function(ci) ci.CatalogType
                ).WithMany().
                  HasForeignKey(Function(ci) ci.CatalogTypeId)

        End Sub
    End Class
End Namespace
