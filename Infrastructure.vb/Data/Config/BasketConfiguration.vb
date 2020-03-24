Option Explicit On
Option Infer On
Option Strict On

Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata.Builders
Imports Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate

Namespace Data.Config
    Public Class BasketConfiguration
        Implements IEntityTypeConfiguration(Of Basket)
        Public Sub Configure(builder As EntityTypeBuilder(Of Basket)) Implements IEntityTypeConfiguration(Of Basket).Configure
            Dim navigation = builder.Metadata.FindNavigation(NameOf(Basket.Items))
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field)

            builder.Property(NameOf(Basket.BuyerId)
                         ).IsRequired().
                           HasMaxLength(40)
        End Sub
    End Class
End Namespace
