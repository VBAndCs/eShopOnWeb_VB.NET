Option Explicit On
Option Infer On
Option Strict On
Imports System
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Infrastructure
Imports Microsoft.EntityFrameworkCore.Metadata
Imports Microsoft.EntityFrameworkCore.Migrations
Imports Microsoft.eShopWeb.Infrastructure.Identity

Namespace Identity.Migrations
    <DbContext(GetType(AppIdentityDbContext))> _
    <Migration("20170822214310_InitialIdentityModel")> _
    Partial Class InitialIdentityModel
        Protected Overrides Sub BuildTargetModel(_modelBuilder As ModelBuilder)
            _modelBuilder.
 HasAnnotation("ProductVersion", "1.1.2").
 HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)

            _modelBuilder.Entity("Infrastructure.Identity.ApplicationUser", Sub(b)
                                                                                b.Property(Of String)("Id").
                                                             ValueGeneratedOnAdd()

                                                                                b.Property(Of Integer)("AccessFailedCount")

                                                                                b.Property(Of String)("ConcurrencyStamp").
                                                             IsConcurrencyToken()

                                                                                b.Property(Of String)("Email").
                                                             HasMaxLength(256)

                                                                                b.Property(Of Boolean)("EmailConfirmed")

                                                                                b.Property(Of Boolean)("LockoutEnabled")

                                                                                b.Property(Of DateTimeOffset?)("LockoutEnd")

                                                                                b.Property(Of String)("NormalizedEmail").
                                                             HasMaxLength(256)

                                                                                b.Property(Of String)("NormalizedUserName").
                                                             HasMaxLength(256)

                                                                                b.Property(Of String)("PasswordHash")

                                                                                b.Property(Of String)("PhoneNumber")

                                                                                b.Property(Of Boolean)("PhoneNumberConfirmed")

                                                                                b.Property(Of String)("SecurityStamp")

                                                                                b.Property(Of Boolean)("TwoFactorEnabled")

                                                                                b.Property(Of String)("UserName").
                                                             HasMaxLength(256)

                                                                                b.HasKey("Id")

                                                                                b.HasIndex("NormalizedEmail").
                                                             HasName("EmailIndex")

                                                                                b.HasIndex("NormalizedUserName").
                                                             IsUnique().
                                                             HasName("UserNameIndex")

                                                                                b.ToTable("AspNetUsers")
                                                                            End Sub)

            _modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", Sub(b)
                                                                                                       b.Property(Of String)("Id").
                                                                                    ValueGeneratedOnAdd()

                                                                                                       b.Property(Of String)("ConcurrencyStamp").
                                                                                    IsConcurrencyToken()

                                                                                                       b.Property(Of String)("Name").
                                                                                    HasMaxLength(256)

                                                                                                       b.Property(Of String)("NormalizedName").
                                                                                    HasMaxLength(256)

                                                                                                       b.HasKey("Id")

                                                                                                       b.HasIndex("NormalizedName").
                                                                                    IsUnique().
                                                                                    HasName("RoleNameIndex")

                                                                                                       b.ToTable("AspNetRoles")
                                                                                                   End Sub)

            _modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", Sub(b)
                                                                                                                    b.Property(Of Integer)("Id").
                                                                                                 ValueGeneratedOnAdd()

                                                                                                                    b.Property(Of String)("ClaimType")

                                                                                                                    b.Property(Of String)("ClaimValue")

                                                                                                                    b.Property(Of String)("RoleId").
                                                                                                 IsRequired()

                                                                                                                    b.HasKey("Id")

                                                                                                                    b.HasIndex("RoleId")

                                                                                                                    b.ToTable("AspNetRoleClaims")
                                                                                                                End Sub)

            _modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", Sub(b)
                                                                                                                    b.Property(Of Integer)("Id").
                                                                                                 ValueGeneratedOnAdd()

                                                                                                                    b.Property(Of String)("ClaimType")

                                                                                                                    b.Property(Of String)("ClaimValue")

                                                                                                                    b.Property(Of String)("UserId").
                                                                                                 IsRequired()

                                                                                                                    b.HasKey("Id")

                                                                                                                    b.HasIndex("UserId")

                                                                                                                    b.ToTable("AspNetUserClaims")
                                                                                                                End Sub)

            _modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", Sub(b)
                                                                                                                    b.Property(Of String)("LoginProvider")

                                                                                                                    b.Property(Of String)("ProviderKey")

                                                                                                                    b.Property(Of String)("ProviderDisplayName")

                                                                                                                    b.Property(Of String)("UserId").
                                                                                                 IsRequired()

                                                                                                                    b.HasKey("LoginProvider", "ProviderKey")

                                                                                                                    b.HasIndex("UserId")

                                                                                                                    b.ToTable("AspNetUserLogins")
                                                                                                                End Sub)

            _modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", Sub(b)
                                                                                                                   b.Property(Of String)("UserId")

                                                                                                                   b.Property(Of String)("RoleId")

                                                                                                                   b.HasKey("UserId", "RoleId")

                                                                                                                   b.HasIndex("RoleId")

                                                                                                                   b.ToTable("AspNetUserRoles")
                                                                                                               End Sub)

            _modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", Sub(b)
                                                                                                                    b.Property(Of String)("UserId")

                                                                                                                    b.Property(Of String)("LoginProvider")

                                                                                                                    b.Property(Of String)("Name")

                                                                                                                    b.Property(Of String)("Value")

                                                                                                                    b.HasKey("UserId", "LoginProvider", "Name")

                                                                                                                    b.ToTable("AspNetUserTokens")
                                                                                                                End Sub)

            _modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", Sub(b)
                                                                                                                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole").
                                                                                                 WithMany("Claims").
                                                                                                 HasForeignKey("RoleId").
                                                                                                 OnDelete(DeleteBehavior.Cascade)
                                                                                                                End Sub)

            _modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", Sub(b)
                                                                                                                    b.HasOne("Infrastructure.Identity.ApplicationUser").
                                                                                                 WithMany("Claims").
                                                                                                 HasForeignKey("UserId").
                                                                                                 OnDelete(DeleteBehavior.Cascade)
                                                                                                                End Sub)

            _modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", Sub(b)
                                                                                                                    b.HasOne("Infrastructure.Identity.ApplicationUser").
                                                                                                 WithMany("Logins").
                                                                                                 HasForeignKey("UserId").
                                                                                                 OnDelete(DeleteBehavior.Cascade)
                                                                                                                End Sub)

            _modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", Sub(b)
                                                                                                                   b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole").
                                                                                                WithMany("Users").
                                                                                                HasForeignKey("RoleId").
                                                                                                OnDelete(DeleteBehavior.Cascade)

                                                                                                                   b.HasOne("Infrastructure.Identity.ApplicationUser").
                                                                                                WithMany("Roles").
                                                                                                HasForeignKey("UserId").
                                                                                                OnDelete(DeleteBehavior.Cascade)
                                                                                                               End Sub)
        End Sub
    End Class
End Namespace
