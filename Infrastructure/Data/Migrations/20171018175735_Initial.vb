Option Explicit On
Option Infer On
Option Strict On

Imports Microsoft.EntityFrameworkCore.Metadata
Imports Microsoft.EntityFrameworkCore.Migrations


Namespace Data.Migrations
    Partial Public Class Initial
        Inherits Migration
        Protected Overrides Sub Up(migrationBuilder As MigrationBuilder)
            migrationBuilder.CreateSequence(
name:="catalog_brand_hilo",
incrementBy:=10)

            migrationBuilder.CreateSequence(
                name:="catalog_hilo",
                incrementBy:=10)

            migrationBuilder.CreateSequence(
                name:="catalog_type_hilo",
                incrementBy:=10)

            migrationBuilder.CreateTable(
                name:="Baskets",
columns:=Function(table) New With {Key .Id = table.Column(Of Integer)(type:="int", nullable:=False).
    Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn), Key .BuyerId = table.Column(Of String)(type:="nvarchar(max)", nullable:=True)
},
constraints:=Sub(table)
                 table.PrimaryKey("PK_Baskets", Function(x) x.Id)
             End Sub)

            migrationBuilder.CreateTable(
                name:="CatalogBrand",
                columns:=Function(table) New With {Key .Id = table.Column(Of Integer)(type:="int", nullable:=False), Key .Brand = table.Column(Of String)(type:="nvarchar(100)", maxLength:=100, nullable:=False)
                },
                constraints:=Sub(table)
                                 table.PrimaryKey("PK_CatalogBrand", Function(x) x.Id)
                             End Sub)

            migrationBuilder.CreateTable(
                name:="CatalogType",
                columns:=Function(table) New With {Key .Id = table.Column(Of Integer)(type:="int", nullable:=False), Key .Type = table.Column(Of String)(type:="nvarchar(100)", maxLength:=100, nullable:=False)
                },
                constraints:=Sub(table)
                                 table.PrimaryKey("PK_CatalogType", Function(x) x.Id)
                             End Sub)

            migrationBuilder.CreateTable(
                name:="Orders",
                columns:=Function(table) New With {Key .Id = table.Column(Of Integer)(type:="int", nullable:=False).
    Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn), Key .BuyerId = table.Column(Of String)(type:="nvarchar(max)", nullable:=True), Key .OrderDate = table.Column(Of DateTimeOffset)(type:="datetimeoffset", nullable:=False), Key .ShipToAddress_City = table.Column(Of String)(type:="nvarchar(max)", nullable:=True), Key .ShipToAddress_Country = table.Column(Of String)(type:="nvarchar(max)", nullable:=True), Key .ShipToAddress_State = table.Column(Of String)(type:="nvarchar(max)", nullable:=True), Key .ShipToAddress_Street = table.Column(Of String)(type:="nvarchar(max)", nullable:=True), Key .ShipToAddress_ZipCode = table.Column(Of String)(type:="nvarchar(max)", nullable:=True)
                },
                constraints:=Sub(table)
                                 table.PrimaryKey("PK_Orders", Function(x) x.Id)
                             End Sub)

            migrationBuilder.CreateTable(
                name:="BasketItem",
                columns:=Function(table) New With {Key .Id = table.Column(Of Integer)(type:="int", nullable:=False).
    Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn), Key .BasketId = table.Column(Of Integer)(type:="int", nullable:=True), Key .CatalogItemId = table.Column(Of Integer)(type:="int", nullable:=False), Key .Quantity = table.Column(Of Integer)(type:="int", nullable:=False), Key .UnitPrice = table.Column(Of Decimal)(type:="decimal(18, 2)", nullable:=False)
                },
                constraints:=Sub(table)
                                 table.PrimaryKey("PK_BasketItem", Function(x) x.Id)
                                 table.ForeignKey(
                                     name:="FK_BasketItem_Baskets_BasketId",
                                     column:=Function(x) x.BasketId,
             principalTable:="Baskets",
             principalColumn:="Id",
             onDelete:=ReferentialAction.Restrict)
                             End Sub)

            migrationBuilder.CreateTable(
                name:="Catalog",
                columns:=Function(table) New With {Key .Id = table.Column(Of Integer)(type:="int", nullable:=False), Key .CatalogBrandId = table.Column(Of Integer)(type:="int", nullable:=False), Key .CatalogTypeId = table.Column(Of Integer)(type:="int", nullable:=False), Key .Description = table.Column(Of String)(type:="nvarchar(max)", nullable:=True), Key .Name = table.Column(Of String)(type:="nvarchar(50)", maxLength:=50, nullable:=False), Key .PictureUri = table.Column(Of String)(type:="nvarchar(max)", nullable:=True), Key .Price = table.Column(Of Decimal)(type:="decimal(18, 2)", nullable:=False)
                },
                constraints:=Sub(table)
                                 table.PrimaryKey("PK_Catalog", Function(x) x.Id)
                                 table.ForeignKey(
                                     name:="FK_Catalog_CatalogBrand_CatalogBrandId",
                                     column:=Function(x) x.CatalogBrandId,
                                     principalTable:="CatalogBrand",
                                     principalColumn:="Id",
                                     onDelete:=ReferentialAction.Cascade)
                                 table.ForeignKey(
                                     name:="FK_Catalog_CatalogType_CatalogTypeId",
                                     column:=Function(x) x.CatalogTypeId,
                                     principalTable:="CatalogType",
                                     principalColumn:="Id",
                                     onDelete:=ReferentialAction.Cascade)
                             End Sub)

            migrationBuilder.CreateTable(
                name:="OrderItems",
                columns:=Function(table) New With {Key .Id = table.Column(Of Integer)(type:="int", nullable:=False).
    Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn), Key .OrderId = table.Column(Of Integer)(type:="int", nullable:=True), Key .UnitPrice = table.Column(Of Decimal)(type:="decimal(18, 2)", nullable:=False), Key .Units = table.Column(Of Integer)(type:="int", nullable:=False), Key .ItemOrdered_CatalogItemId = table.Column(Of Integer)(type:="int", nullable:=False), Key .ItemOrdered_PictureUri = table.Column(Of String)(type:="nvarchar(max)", nullable:=True), Key .ItemOrdered_ProductName = table.Column(Of String)(type:="nvarchar(max)", nullable:=True)
                },
                constraints:=Sub(table)
                                 table.PrimaryKey("PK_OrderItems", Function(x) x.Id)
                                 table.ForeignKey(
                                     name:="FK_OrderItems_Orders_OrderId",
                                     column:=Function(x) x.OrderId,
                                     principalTable:="Orders",
                                     principalColumn:="Id",
                                     onDelete:=ReferentialAction.Restrict)
                             End Sub)

            migrationBuilder.CreateIndex(
                name:="IX_BasketItem_BasketId",
                table:="BasketItem",
                column:="BasketId")

            migrationBuilder.CreateIndex(
                name:="IX_Catalog_CatalogBrandId",
                table:="Catalog",
                column:="CatalogBrandId")

            migrationBuilder.CreateIndex(
                name:="IX_Catalog_CatalogTypeId",
                table:="Catalog",
                column:="CatalogTypeId")

            migrationBuilder.CreateIndex(
                name:="IX_OrderItems_OrderId",
                table:="OrderItems",
                column:="OrderId")
        End Sub

        Protected Overrides Sub Down(migrationBuilder As MigrationBuilder)
            migrationBuilder.DropTable(
                name:="BasketItem")

            migrationBuilder.DropTable(
                name:="Catalog")

            migrationBuilder.DropTable(
                name:="OrderItems")

            migrationBuilder.DropTable(
                name:="Baskets")

            migrationBuilder.DropTable(
                name:="CatalogBrand")

            migrationBuilder.DropTable(
                name:="CatalogType")

            migrationBuilder.DropTable(
                name:="Orders")

            migrationBuilder.DropSequence(
                name:="catalog_brand_hilo")

            migrationBuilder.DropSequence(
                name:="catalog_hilo")

            migrationBuilder.DropSequence(
                name:="catalog_type_hilo")
        End Sub
    End Class
End Namespace
