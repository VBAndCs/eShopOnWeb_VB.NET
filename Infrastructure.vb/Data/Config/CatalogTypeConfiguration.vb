Option Explicit On
Option Infer On
Option Strict On

Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata.Builders
Imports Microsoft.eShopWeb.ApplicationCore.Entities

Namespace Data.Config
    Public Class CatalogTypeConfiguration
        Implements IEntityTypeConfiguration(Of CatalogType)

        Public Sub Configure(builder As EntityTypeBuilder(Of CatalogType)) Implements IEntityTypeConfiguration(Of CatalogType).Configure
            builder.HasKey(NameOf(CatalogType.Id))

            builder.Property(NameOf(CatalogType.Id)
                ).UseHiLo("catalog_type_hilo").
                  IsRequired()

            builder.Property(NameOf(CatalogType.Type)
                ).IsRequired().
                  HasMaxLength(100)
        End Sub
    End Class
End Namespace
