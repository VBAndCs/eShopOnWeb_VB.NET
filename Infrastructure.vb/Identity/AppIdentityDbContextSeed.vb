Option Explicit On
Option Infer On
Option Strict On
Imports Microsoft.AspNetCore.Identity
Imports Microsoft.eShopWeb.ApplicationCore.Constants

Namespace Identity
    Public Class AppIdentityDbContextSeed
        Public Shared Async Function SeedAsync(_userManager As UserManager(Of ApplicationUser), _roleManager As RoleManager(Of IdentityRole)) As Task
            Await _roleManager.CreateAsync(New IdentityRole(AuthorizationConstants.Roles.ADMINISTRATORS))

            Dim defaultUser = New ApplicationUser With {
                .UserName = "demouser@microsoft.com",
                .Email = "demouser@microsoft.com"
            }
            Await _userManager.CreateAsync(defaultUser, AuthorizationConstants.DEFAULT_PASSWORD)

            Dim adminUserName As String = "admin@microsoft.com"
            Dim adminUser = New ApplicationUser With {
                .UserName = adminUserName,
                .Email = adminUserName
            }
            Await _userManager.CreateAsync(adminUser, AuthorizationConstants.DEFAULT_PASSWORD)
            adminUser = Await _userManager.FindByNameAsync(adminUserName)
            Await _userManager.AddToRoleAsync(adminUser, AuthorizationConstants.Roles.ADMINISTRATORS)
        End Function
    End Class
End Namespace
