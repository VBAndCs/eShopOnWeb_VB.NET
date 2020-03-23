Option Explicit On
Option Infer On
Option Strict On
Imports System
Imports Microsoft.EntityFrameworkCore.Migrations
Imports Microsoft.EntityFrameworkCore.Metadata

Namespace Identity.Migrations
    Public Partial Class InitialIdentityModel
        Inherits Migration
        Protected Overrides Sub Up(migrationBuilder As MigrationBuilder)
            migrationBuilder.CreateTable(
name:="AspNetUsers",
columns:=Function(table) New With {Key .Id = table.Column(Of String)(nullable:=False), Key .AccessFailedCount = table.Column(Of Integer)(nullable:=False), Key .ConcurrencyStamp = table.Column(Of String)(nullable:=True), Key .Email = table.Column(Of String)(maxLength:=256, nullable:=True), Key .EmailConfirmed = table.Column(Of Boolean)(nullable:=False), Key .LockoutEnabled = table.Column(Of Boolean)(nullable:=False), Key .LockoutEnd = table.Column(Of DateTimeOffset)(nullable:=True), Key .NormalizedEmail = table.Column(Of String)(maxLength:=256, nullable:=True), Key .NormalizedUserName = table.Column(Of String)(maxLength:=256, nullable:=True), Key .PasswordHash = table.Column(Of String)(nullable:=True), Key .PhoneNumber = table.Column(Of String)(nullable:=True), Key .PhoneNumberConfirmed = table.Column(Of Boolean)(nullable:=False), Key .SecurityStamp = table.Column(Of String)(nullable:=True), Key .TwoFactorEnabled = table.Column(Of Boolean)(nullable:=False), Key .UserName = table.Column(Of String)(maxLength:=256, nullable:=True)
},
constraints:=Sub(table)
                 table.PrimaryKey("PK_AspNetUsers", Function(x) x.Id)
             End Sub)

            migrationBuilder.CreateTable(
                name:="AspNetRoles",
                columns:=Function(table) New With {Key .Id = table.Column(Of String)(nullable:=False), Key .ConcurrencyStamp = table.Column(Of String)(nullable:=True), Key .Name = table.Column(Of String)(maxLength:=256, nullable:=True), Key .NormalizedName = table.Column(Of String)(maxLength:=256, nullable:=True)
                },
                constraints:=Sub(table)
                                 table.PrimaryKey("PK_AspNetRoles", Function(x) x.Id)
                             End Sub)

            migrationBuilder.CreateTable(
                name:="AspNetUserTokens",
                columns:=Function(table) New With {Key .UserId = table.Column(Of String)(nullable:=False), Key .LoginProvider = table.Column(Of String)(nullable:=False), Key .Name = table.Column(Of String)(nullable:=False), Key .Value = table.Column(Of String)(nullable:=True)
                },
                constraints:=Sub(table)
                                 table.PrimaryKey("PK_AspNetUserTokens", Function(x) New With {x.UserId, x.LoginProvider, x.Name
                                 })
                             End Sub)

            migrationBuilder.CreateTable(
                name:="AspNetUserClaims",
                columns:=Function(table) New With {Key .Id = table.Column(Of Integer)(nullable:=False).
    Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn), Key .ClaimType = table.Column(Of String)(nullable:=True), Key .ClaimValue = table.Column(Of String)(nullable:=True), Key .UserId = table.Column(Of String)(nullable:=False)
                },
                constraints:=Sub(table)
                                 table.PrimaryKey("PK_AspNetUserClaims", Function(x) x.Id)
                                 table.ForeignKey(
                                     name:="FK_AspNetUserClaims_AspNetUsers_UserId",
                                     column:=Function(x) x.UserId,
             principalTable:="AspNetUsers",
             principalColumn:="Id",
             onDelete:=ReferentialAction.Cascade)
                             End Sub)

            migrationBuilder.CreateTable(
                name:="AspNetUserLogins",
                columns:=Function(table) New With {Key .LoginProvider = table.Column(Of String)(nullable:=False), Key .ProviderKey = table.Column(Of String)(nullable:=False), Key .ProviderDisplayName = table.Column(Of String)(nullable:=True), Key .UserId = table.Column(Of String)(nullable:=False)
                },
                constraints:=Sub(table)
                                 table.PrimaryKey("PK_AspNetUserLogins", Function(x) New With {x.LoginProvider, x.ProviderKey
                                 })
                                 table.ForeignKey(
                                     name:="FK_AspNetUserLogins_AspNetUsers_UserId",
                                     column:=Function(x) x.UserId,
                                     principalTable:="AspNetUsers",
                                     principalColumn:="Id",
                                     onDelete:=ReferentialAction.Cascade)
                             End Sub)

            migrationBuilder.CreateTable(
                name:="AspNetRoleClaims",
                columns:=Function(table) New With {Key .Id = table.Column(Of Integer)(nullable:=False).
    Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn), Key .ClaimType = table.Column(Of String)(nullable:=True), Key .ClaimValue = table.Column(Of String)(nullable:=True), Key .RoleId = table.Column(Of String)(nullable:=False)
                },
                constraints:=Sub(table)
                                 table.PrimaryKey("PK_AspNetRoleClaims", Function(x) x.Id)
                                 table.ForeignKey(
                                     name:="FK_AspNetRoleClaims_AspNetRoles_RoleId",
                                     column:=Function(x) x.RoleId,
                                     principalTable:="AspNetRoles",
                                     principalColumn:="Id",
                                     onDelete:=ReferentialAction.Cascade)
                             End Sub)

            migrationBuilder.CreateTable(
                name:="AspNetUserRoles",
                columns:=Function(table) New With {Key .UserId = table.Column(Of String)(nullable:=False), Key .RoleId = table.Column(Of String)(nullable:=False)
                },
                constraints:=Sub(table)
                                 table.PrimaryKey("PK_AspNetUserRoles", Function(x) New With {x.UserId, x.RoleId
                                 })
                                 table.ForeignKey(
                                     name:="FK_AspNetUserRoles_AspNetRoles_RoleId",
                                     column:=Function(x) x.RoleId,
                                     principalTable:="AspNetRoles",
                                     principalColumn:="Id",
                                     onDelete:=ReferentialAction.Cascade)
                                 table.ForeignKey(
                                     name:="FK_AspNetUserRoles_AspNetUsers_UserId",
                                     column:=Function(x) x.UserId,
                                     principalTable:="AspNetUsers",
                                     principalColumn:="Id",
                                     onDelete:=ReferentialAction.Cascade)
                             End Sub)

            migrationBuilder.CreateIndex(
                name:="EmailIndex",
                table:="AspNetUsers",
                column:="NormalizedEmail")

            migrationBuilder.CreateIndex(
                name:="UserNameIndex",
                table:="AspNetUsers",
                column:="NormalizedUserName",
unique:=True)

            migrationBuilder.CreateIndex(
                name:="RoleNameIndex",
                table:="AspNetRoles",
                column:="NormalizedName",
                unique:=True)

            migrationBuilder.CreateIndex(
                name:="IX_AspNetRoleClaims_RoleId",
                table:="AspNetRoleClaims",
                column:="RoleId")

            migrationBuilder.CreateIndex(
                name:="IX_AspNetUserClaims_UserId",
                table:="AspNetUserClaims",
                column:="UserId")

            migrationBuilder.CreateIndex(
                name:="IX_AspNetUserLogins_UserId",
                table:="AspNetUserLogins",
                column:="UserId")

            migrationBuilder.CreateIndex(
                name:="IX_AspNetUserRoles_RoleId",
                table:="AspNetUserRoles",
                column:="RoleId")
        End Sub

        Protected Overrides Sub Down(migrationBuilder As MigrationBuilder)
            migrationBuilder.DropTable(
                name:="AspNetRoleClaims")

            migrationBuilder.DropTable(
                name:="AspNetUserClaims")

            migrationBuilder.DropTable(
                name:="AspNetUserLogins")

            migrationBuilder.DropTable(
                name:="AspNetUserRoles")

            migrationBuilder.DropTable(
                name:="AspNetUserTokens")

            migrationBuilder.DropTable(
                name:="AspNetRoles")

            migrationBuilder.DropTable(
                name:="AspNetUsers")
        End Sub
    End Class
End Namespace
