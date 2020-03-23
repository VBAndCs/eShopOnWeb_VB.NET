
Imports System.Text
Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.AspNetCore.Identity
Imports Microsoft.eShopWeb.Infrastructure.Data
Imports Microsoft.eShopWeb.Infrastructure.Identity
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Hosting
Imports Microsoft.Extensions.Logging

Public Class Program
    Public Shared Sub Main(ByVal args As String())
        Dim x As Integer?
        Dim y = If(x.HasValue, x.Value = 0, False)

        Dim Host = CreateHostBuilder(args).Build()

        Using scope = Host.Services.CreateScope()
            Dim Services = scope.ServiceProvider
            Dim LoggerFactory = Services.GetRequiredService(Of ILoggerFactory)()
            Try
                Dim CatalogContext = Services.GetRequiredService(Of CatalogContext)()
                Task.Run(Sub() CatalogContextSeed.SeedAsync(CatalogContext, LoggerFactory)).Wait()

                Dim UserManager = Services.GetRequiredService(Of UserManager(Of ApplicationUser))()
                Dim RoleManager = Services.GetRequiredService(Of RoleManager(Of IdentityRole))()
                Task.Run(Sub() AppIdentityDbContextSeed.SeedAsync(UserManager, RoleManager)).Wait()
            Catch ex As Exception
                Dim Logger = LoggerFactory.CreateLogger(Of Program)()
                Logger.LogError(ex, "An error occurred seeding the DB.")
            End Try
        End Using

        Host.Run()
    End Sub

    Public Shared Function CreateHostBuilder(ByVal args As String()) As IHostBuilder
        Return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(
            Sub(webBuilder) webBuilder.UseStartup(Of Startup)()
        )
    End Function
End Class

