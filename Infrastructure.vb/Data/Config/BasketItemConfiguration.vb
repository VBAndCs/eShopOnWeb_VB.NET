Option Explicit On
Option Infer On
Option Strict On

Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata.Builders
Imports Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate

Namespace Data.Config
    Public Class BasketItemConfiguration
        Implements IEntityTypeConfiguration(Of BasketItem)
        Public Sub Configure(builder As EntityTypeBuilder(Of BasketItem)) Implements IEntityTypeConfiguration(Of BasketItem).Configure
            builder.Property(NameOf(BasketItem.UnitPrice)
                ).IsRequired(True).
                  HasColumnType("decimal(18,2)")
        End Sub
    End Class
End Namespace
