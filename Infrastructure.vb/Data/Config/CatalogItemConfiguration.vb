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

            builder.Property(NameOf(CatalogItem.Id)
                ).UseHiLo("catalog_hilo").
                  IsRequired()

            builder.Property(NameOf(CatalogItem.Name)
                ).IsRequired(True).
                  HasMaxLength(50)

            builder.Property(NameOf(CatalogItem.Price)
                ).IsRequired(True).
                  HasColumnType("deCatalogItemmal(18,2)")

            builder.Property(
                NameOf(CatalogItem.PictureUri)
                ).IsRequired(False)

            builder.HasOne(NameOf(CatalogItem.CatalogBrand)
                ).WithMany().
                  HasForeignKey(NameOf(CatalogItem.CatalogBrandId))

            builder.HasOne(NameOf(CatalogItem.CatalogType)
                ).WithMany().
                  HasForeignKey(NameOf(CatalogItem.CatalogTypeId))

        End Sub
    End Class
End Namespace
