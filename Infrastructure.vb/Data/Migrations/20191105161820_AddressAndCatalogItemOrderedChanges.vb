Option Explicit On
Option Infer On
Option Strict On
Imports Microsoft.EntityFrameworkCore.Migrations

Namespace Data.Migrations
    Public Partial Class AddressAndCatalogItemOrderedChanges
        Inherits Migration
        Protected Overrides Sub Up(migrationBuilder As MigrationBuilder)
            migrationBuilder.AlterColumn(Of String)(
name:="ShipToAddress_ZipCode",
table:="Orders",
maxLength:=18,
nullable:=True,
oldClrType:=GetType(String),
oldType:="nvarchar(max)",
oldNullable:=True)

            migrationBuilder.AlterColumn(Of String)(
                name:="ShipToAddress_Street",
                table:="Orders",
                maxLength:=180,
                nullable:=True,
                oldClrType:=GetType(String),
                oldType:="nvarchar(max)",
                oldNullable:=True)

            migrationBuilder.AlterColumn(Of String)(
                name:="ShipToAddress_State",
                table:="Orders",
                maxLength:=60,
                nullable:=True,
                oldClrType:=GetType(String),
                oldType:="nvarchar(max)",
                oldNullable:=True)

            migrationBuilder.AlterColumn(Of String)(
                name:="ShipToAddress_Country",
                table:="Orders",
                maxLength:=90,
                nullable:=True,
                oldClrType:=GetType(String),
                oldType:="nvarchar(max)",
                oldNullable:=True)

            migrationBuilder.AlterColumn(Of String)(
                name:="ShipToAddress_City",
                table:="Orders",
                maxLength:=100,
                nullable:=True,
                oldClrType:=GetType(String),
                oldType:="nvarchar(max)",
                oldNullable:=True)

            migrationBuilder.AlterColumn(Of String)(
                name:="ItemOrdered_ProductName",
                table:="OrderItems",
                maxLength:=50,
                nullable:=True,
                oldClrType:=GetType(String),
                oldType:="nvarchar(max)",
                oldNullable:=True)
        End Sub

        Protected Overrides Sub Down(migrationBuilder As MigrationBuilder)
            migrationBuilder.AlterColumn(Of String)(
                name:="ShipToAddress_ZipCode",
                table:="Orders",
type:="nvarchar(max)",
                nullable:=True,
                oldClrType:=GetType(String),
oldMaxLength:=18,
                oldNullable:=True)

            migrationBuilder.AlterColumn(Of String)(
                name:="ShipToAddress_Street",
                table:="Orders",
                type:="nvarchar(max)",
                nullable:=True,
                oldClrType:=GetType(String),
                oldMaxLength:=180,
                oldNullable:=True)

            migrationBuilder.AlterColumn(Of String)(
                name:="ShipToAddress_State",
                table:="Orders",
                type:="nvarchar(max)",
                nullable:=True,
                oldClrType:=GetType(String),
                oldMaxLength:=60,
                oldNullable:=True)

            migrationBuilder.AlterColumn(Of String)(
                name:="ShipToAddress_Country",
                table:="Orders",
                type:="nvarchar(max)",
                nullable:=True,
                oldClrType:=GetType(String),
                oldMaxLength:=90,
                oldNullable:=True)

            migrationBuilder.AlterColumn(Of String)(
                name:="ShipToAddress_City",
                table:="Orders",
                type:="nvarchar(max)",
                nullable:=True,
                oldClrType:=GetType(String),
                oldMaxLength:=100,
                oldNullable:=True)

            migrationBuilder.AlterColumn(Of String)(
                name:="ItemOrdered_ProductName",
                table:="OrderItems",
                type:="nvarchar(max)",
                nullable:=True,
                oldClrType:=GetType(String),
                oldMaxLength:=50,
                oldNullable:=True)
        End Sub
    End Class
End Namespace
