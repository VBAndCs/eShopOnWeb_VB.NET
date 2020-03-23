Option Explicit On
Option Infer On
Option Strict On
Imports Microsoft.EntityFrameworkCore.Migrations

Namespace Data.Migrations
    Public Partial Class AddExtraConstraints
        Inherits Migration
        Protected Overrides Sub Up(migrationBuilder As MigrationBuilder)
            migrationBuilder.AlterColumn(Of String)(
name:="ShipToAddress_ZipCode",
table:="Orders",
maxLength:=18,
nullable:=False,
oldClrType:=GetType(String),
oldNullable:=True)

            migrationBuilder.AlterColumn(Of String)(
                name:="ShipToAddress_Street",
                table:="Orders",
                maxLength:=180,
                nullable:=False,
                oldClrType:=GetType(String),
                oldNullable:=True)

            migrationBuilder.AlterColumn(Of String)(
                name:="ShipToAddress_State",
                table:="Orders",
                maxLength:=60,
                nullable:=True,
                oldClrType:=GetType(String),
                oldNullable:=True)

            migrationBuilder.AlterColumn(Of String)(
                name:="ShipToAddress_Country",
                table:="Orders",
                maxLength:=90,
                nullable:=False,
                oldClrType:=GetType(String),
                oldNullable:=True)

            migrationBuilder.AlterColumn(Of String)(
                name:="ShipToAddress_City",
                table:="Orders",
                maxLength:=100,
                nullable:=False,
                oldClrType:=GetType(String),
                oldNullable:=True)

            migrationBuilder.AlterColumn(Of String)(
                name:="ItemOrdered_ProductName",
                table:="OrderItems",
                maxLength:=50,
                nullable:=False,
                oldClrType:=GetType(String),
                oldNullable:=True)
        End Sub

        Protected Overrides Sub Down(migrationBuilder As MigrationBuilder)
            migrationBuilder.AlterColumn(Of String)(
                name:="ShipToAddress_ZipCode",
                table:="Orders",
                nullable:=True,
                oldClrType:=GetType(String),
oldMaxLength:=18)

            migrationBuilder.AlterColumn(Of String)(
                name:="ShipToAddress_Street",
                table:="Orders",
                nullable:=True,
                oldClrType:=GetType(String),
                oldMaxLength:=180)

            migrationBuilder.AlterColumn(Of String)(
                name:="ShipToAddress_State",
                table:="Orders",
                nullable:=True,
                oldClrType:=GetType(String),
                oldMaxLength:=60,
                oldNullable:=True)

            migrationBuilder.AlterColumn(Of String)(
                name:="ShipToAddress_Country",
                table:="Orders",
                nullable:=True,
                oldClrType:=GetType(String),
                oldMaxLength:=90)

            migrationBuilder.AlterColumn(Of String)(
                name:="ShipToAddress_City",
                table:="Orders",
                nullable:=True,
                oldClrType:=GetType(String),
                oldMaxLength:=100)

            migrationBuilder.AlterColumn(Of String)(
                name:="ItemOrdered_ProductName",
                table:="OrderItems",
                nullable:=True,
                oldClrType:=GetType(String),
                oldMaxLength:=50)
        End Sub
    End Class
End Namespace
