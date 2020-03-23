Option Explicit On
Option Infer On
Option Strict On
Imports Microsoft.EntityFrameworkCore.Migrations

Namespace Data.Migrations
    Public Partial Class UpdatingDefaultDataTypes
        Inherits Migration
        Protected Overrides Sub Up(migrationBuilder As MigrationBuilder)
            migrationBuilder.DropForeignKey(
name:="FK_BasketItem_Baskets_BasketId",
table:="BasketItem")

            migrationBuilder.DropPrimaryKey(
                name:="PK_BasketItem",
                table:="BasketItem")

            migrationBuilder.RenameTable(
                name:="BasketItem",
newName:="BasketItems")

            migrationBuilder.RenameIndex(
                name:="IX_BasketItem_BasketId",
                table:="BasketItems",
                newName:="IX_BasketItems_BasketId")

            migrationBuilder.AddPrimaryKey(
                name:="PK_BasketItems",
                table:="BasketItems",
column:="Id")

            migrationBuilder.AddForeignKey(
                name:="FK_BasketItems_Baskets_BasketId",
                table:="BasketItems",
                column:="BasketId",
principalTable:="Baskets",
principalColumn:="Id",
onDelete:=ReferentialAction.Restrict)
        End Sub

        Protected Overrides Sub Down(migrationBuilder As MigrationBuilder)
            migrationBuilder.DropForeignKey(
                name:="FK_BasketItems_Baskets_BasketId",
                table:="BasketItems")

            migrationBuilder.DropPrimaryKey(
                name:="PK_BasketItems",
                table:="BasketItems")

            migrationBuilder.RenameTable(
                name:="BasketItems",
                newName:="BasketItem")

            migrationBuilder.RenameIndex(
                name:="IX_BasketItems_BasketId",
                table:="BasketItem",
                newName:="IX_BasketItem_BasketId")

            migrationBuilder.AddPrimaryKey(
                name:="PK_BasketItem",
                table:="BasketItem",
                column:="Id")

            migrationBuilder.AddForeignKey(
                name:="FK_BasketItem_Baskets_BasketId",
                table:="BasketItem",
                column:="BasketId",
                principalTable:="Baskets",
                principalColumn:="Id",
                onDelete:=ReferentialAction.Restrict)
        End Sub
    End Class
End Namespace
