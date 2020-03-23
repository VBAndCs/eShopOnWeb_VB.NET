Imports System
Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.AspNetCore.Identity
Imports Microsoft.AspNetCore.Identity.UI
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.eShopWeb.Infrastructure.Identity
Imports Microsoft.Extensions.Configuration
Imports Microsoft.Extensions.DependencyInjection

<Assembly: HostingStartup(GetType(Areas.Identity.IdentityHostingStartup))>
Namespace Areas.Identity
    Public Class IdentityHostingStartup
        Implements IHostingStartup

        Public Sub Configure(ByVal builder As IWebHostBuilder) Implements IHostingStartup.Configure
            builder.ConfigureServices(Sub(context, services)
                                      End Sub)
        End Sub
    End Class
End Namespace
