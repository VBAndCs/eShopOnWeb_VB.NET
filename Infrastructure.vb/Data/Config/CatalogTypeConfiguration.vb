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
            builder.HasKey(Function(ci) ci.Id)

            builder.Property(
                Function(ci) ci.Id
                ).UseHiLo("catalog_type_hilo").
                  IsRequired()

            builder.Property(
                Function(cb) cb.Type
                ).IsRequired().
                  HasMaxLength(100)
        End Sub
    End Class
End Namespace
