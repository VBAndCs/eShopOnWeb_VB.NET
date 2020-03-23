Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata.Builders
Imports Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate

Namespace Data.Config
    Public Class OrderConfiguration
        Implements IEntityTypeConfiguration(Of Order)
        Public Sub Configure(builder As EntityTypeBuilder(Of Order)) Implements IEntityTypeConfiguration(Of Order).Configure
            Dim navigation = builder.Metadata.FindNavigation(NameOf(Order.OrderItems))

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field)

            builder.OwnsOne(
                Function(o) o.ShipToAddress,
                                Sub(x)
                                    x.WithOwner()
                                    x.Property(Function(a) a.ZipCode).HasMaxLength(18).IsRequired()
                                    x.Property(Function(a) a.Street).HasMaxLength(180).IsRequired()
                                    x.Property(Function(a) a.State).HasMaxLength(60)
                                    x.Property(Function(a) a.Country).HasMaxLength(90).IsRequired()
                                    x.Property(Function(a) a.City).HasMaxLength(100).IsRequired()
                                End Sub
            )
        End Sub
    End Class
End Namespace
