Option Explicit On
Option Infer On
Option Strict On
Imports Microsoft.AspNetCore.Identity.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore


Namespace Identity
    Public Class AppIdentityDbContext
        Inherits IdentityDbContext(Of ApplicationUser)
        Public Sub New(options As DbContextOptions(Of AppIdentityDbContext))
            MyBase.New(options)
        End Sub

        Protected Overrides Sub OnModelCreating(builder As ModelBuilder)
            MyBase.OnModelCreating(builder)
            ' Customize the ASP.NET Identity model and override the defaults if needed.
            ' For example, you can rename the ASP.NET Identity table names and more.
            ' Add your customizations after calling base.OnModelCreating(builder);
        End Sub
    End Class
End Namespace
