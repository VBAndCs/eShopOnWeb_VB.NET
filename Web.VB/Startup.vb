Imports Ardalis.ListStartupServices
Imports MediatR
Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Diagnostics.HealthChecks
Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.AspNetCore.Http
Imports Microsoft.AspNetCore.Identity
Imports Microsoft.AspNetCore.Mvc.ApplicationModels
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.eShopWeb.ApplicationCore.Interfaces
Imports Microsoft.eShopWeb.ApplicationCore.Services
Imports Microsoft.eShopWeb.Infrastructure.Data
Imports Microsoft.eShopWeb.Infrastructure.Identity
Imports Microsoft.eShopWeb.Infrastructure.Logging
Imports Microsoft.eShopWeb.Infrastructure.Services
Imports Microsoft.eShopWeb.Web.Interfaces
Imports Microsoft.eShopWeb.Web.Services
Imports Microsoft.Extensions.Configuration
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Diagnostics.HealthChecks
Imports Microsoft.Extensions.Hosting
Imports Newtonsoft.Json
Imports Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
Imports System.Net.Mime
Imports Microsoft.eShopWeb.Web.ViewModels
Imports Microsoft.eShopWeb.ApplicationCore

Public Class Startup
    Private _services As IServiceCollection

    Public Sub New(ByVal configuration As IConfiguration)
        Me.Configuration = configuration
    End Sub

    Public ReadOnly Property Configuration As IConfiguration

    Public Sub ConfigureDevelopmentServices(ByVal services As IServiceCollection)
        ConfigureInMemoryDatabases(services)
    End Sub

    Private Sub ConfigureInMemoryDatabases(ByVal services As IServiceCollection)
        services.AddDbContext(Of CatalogContext)(Function(c) c.UseInMemoryDatabase("Catalog"))
        services.AddDbContext(Of AppIdentityDbContext)(Function(options) options.UseInMemoryDatabase("Identity"))
        ConfigureServices(services)
    End Sub

    Public Sub ConfigureProductionServices(ByVal services As IServiceCollection)
        services.AddDbContext(Of CatalogContext)(Function(c) c.UseSqlServer(Configuration.GetConnectionString("CatalogConnection")))
        services.AddDbContext(Of AppIdentityDbContext)(Function(options) options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")))
        ConfigureServices(services)
    End Sub

    Public Sub ConfigureTestingServices(ByVal services As IServiceCollection)
        ConfigureInMemoryDatabases(services)
    End Sub

    Public Sub ConfigureServices(ByVal services As IServiceCollection)
        ConfigureCookieSettings(services)
        CreateIdentityIfNotCreated(services)
        services.AddMediatR(GetType(BasketViewModelService).Assembly)
        services.AddScoped(GetType(IAsyncRepository(Of)), GetType(EfRepository(Of)))
        services.AddScoped(Of ICatalogViewModelService, CachedCatalogViewModelService)()
        services.AddScoped(Of IBasketService, BasketService)()
        services.AddScoped(Of IBasketViewModelService, BasketViewModelService)()
        services.AddScoped(Of IOrderService, OrderService)()
        services.AddScoped(Of IOrderRepository, OrderRepository)()
        services.AddScoped(Of CatalogViewModelService)()
        services.AddScoped(Of ICatalogItemViewModelService, CatalogItemViewModelService)()
        services.Configure(Of CatalogSettings)(Configuration)
        services.AddSingleton(Of IUriComposer)(New UriComposer(Configuration.[Get](Of CatalogSettings)()))
        services.AddScoped(GetType(IAppLogger(Of)), GetType(LoggerAdapter(Of)))
        services.AddTransient(Of IEmailSender, EmailSender)()
        services.AddMemoryCache()
        services.AddRouting(
                 Sub(options) options.ConstraintMap("slugify") = GetType(SlugifyParameterTransformer)
            )
        services.AddMvc(
                Sub(options) options.Conventions.Add(New RouteTokenTransformerConvention(New SlugifyParameterTransformer()))
          )
        services.AddRazorPages(
               Sub(options) options.Conventions.AuthorizePage("/Basket/Checkout")
            )
        services.AddControllersWithViews(). ' Enable Vazor
        AddRazorRuntimeCompilation(
             Sub(options) options.FileProviders.Add(New Vazor.VazorViewProvider())
        )
        services.AddHttpContextAccessor()
        services.AddSwaggerGen(Sub(c) c.SwaggerDoc("v1", New OpenApi.Models.OpenApiInfo With {
        .Title = "My API",
        .Version = "v1"
    }))
        services.AddHealthChecks()
        services.Configure(Of ServiceConfig)(Function(config)
                                                 config.Services = New List(Of ServiceDescriptor)(services)
                                                 config.Path = "/allservices"
                                             End Function)
        _services = services
    End Sub

    Private Shared Sub CreateIdentityIfNotCreated(ByVal services As IServiceCollection)
        Dim sp = services.BuildServiceProvider()

        Using scope = sp.CreateScope()
            Dim existingUserManager = scope.ServiceProvider.GetService(Of UserManager(Of ApplicationUser))()

            If existingUserManager Is Nothing Then
                services.AddIdentity(Of ApplicationUser, IdentityRole)().AddDefaultUI().AddEntityFrameworkStores(Of AppIdentityDbContext)().AddDefaultTokenProviders()
            End If
        End Using
    End Sub

    Private Shared Sub ConfigureCookieSettings(ByVal services As IServiceCollection)
        services.Configure(Of CookiePolicyOptions)(Function(options)
                                                       options.CheckConsentNeeded = Function(context) True
                                                       options.MinimumSameSitePolicy = SameSiteMode.None
                                                   End Function)
        services.ConfigureApplicationCookie(Function(options)
                                                options.Cookie.HttpOnly = True
                                                options.ExpireTimeSpan = TimeSpan.FromHours(1)
                                                options.LoginPath = "/Account/Login"
                                                options.LogoutPath = "/Account/Logout"
                                                options.Cookie = New CookieBuilder With {
                                                    .IsEssential = True
                                                }
                                            End Function)
    End Sub

    Public Sub Configure(ByVal app As IApplicationBuilder, ByVal env As IWebHostEnvironment)

        ZML.ZmlPages.Compile() ' Erase this statement when you fisnish converting all .zml files to vazor files
        Vazor.VazorSharedView.CreateAll()

        app.UseHealthChecks("/health", New HealthCheckOptions With {
            .ResponseWriter = Async Function(context, report)
                                  Dim result = JsonConvert.SerializeObject(New With {
                                               Key .status = report.Status.ToString(),
                                               Key .errors = report.Entries.[Select](Function(e) New With {
                                               Key .key = e.Key,
                                               Key .value = [Enum].GetName(GetType(HealthStatus), e.Value.Status)
                                          })
                                      })
                                  context.Response.ContentType = MediaTypeNames.Application.Json
                                  Await context.Response.WriteAsync(result)
                              End Function
        })

        If env.IsDevelopment() Then
            app.UseDeveloperExceptionPage()
            app.UseShowAllServicesMiddleware()
            app.UseDatabaseErrorPage()
        Else
            app.UseExceptionHandler("/Error")
            app.UseHsts()
        End If

        app.UseStaticFiles()
        app.UseRouting()
        app.UseHttpsRedirection()
        app.UseCookiePolicy()
        app.UseAuthentication()
        app.UseAuthorization()
        app.UseSwagger()
        app.UseSwaggerUI(Function(c)
                             c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1")
                         End Function)
        app.UseEndpoints(Function(endpoints)
                             endpoints.MapControllerRoute("default", "{controller:slugify=Home}/{action:slugify=Index}/{id?}")
                             endpoints.MapRazorPages()
                             endpoints.MapHealthChecks("home_page_health_check")
                             endpoints.MapHealthChecks("api_health_check")
                         End Function)
    End Sub
End Class
