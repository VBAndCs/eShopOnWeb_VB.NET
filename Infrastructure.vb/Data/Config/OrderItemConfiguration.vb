Option Explicit On
Option Infer On
Option Strict On

Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata.Builders
Imports Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate

Namespace Data.Config
    Public Class OrderItemConfiguration
        Implements IEntityTypeConfiguration(Of OrderItem)
        Public Sub Configure(builder As EntityTypeBuilder(Of OrderItem)) Implements IEntityTypeConfiguration(Of OrderItem).Configure

            builder.OwnsOne(
                Function(i) i.ItemOrdered,
                 Sub(io)
                     io.WithOwner()
                     io.Property(Function(cio) cio.ProductName).
                         HasMaxLength(50).
                         IsRequired()
                 End Sub
            )

            builder.Property(
                Function(oi) oi.UnitPrice
                ).IsRequired(True).
                  HasColumnType("decimal(18,2)")
        End Sub
    End Class
End Namespace
