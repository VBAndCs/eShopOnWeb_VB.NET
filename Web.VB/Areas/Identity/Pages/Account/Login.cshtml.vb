Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports System.Threading.Tasks
Imports Microsoft.AspNetCore.Authorization
Imports Microsoft.AspNetCore.Authentication
Imports Microsoft.AspNetCore.Identity
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.AspNetCore.Mvc.RazorPages
Imports Microsoft.eShopWeb.Infrastructure.Identity
Imports Microsoft.Extensions.Logging
Imports Vazor

Namespace Areas.Identity.Pages.Account
    <AllowAnonymous>
    Public Class LoginModel
        Inherits PageModel

        Private ReadOnly _signInManager As SignInManager(Of ApplicationUser)
        Private ReadOnly _logger As ILogger(Of LoginModel)

        Public Sub New(ByVal signInManager As SignInManager(Of ApplicationUser), ByVal logger As ILogger(Of LoginModel))
            _signInManager = signInManager
            _logger = logger
        End Sub

        Const Title = "Log in"

        Public ReadOnly Property ViewName As String
            Get
                ViewData("Title") = Title
                Dim html = Me.GetVbXml().ParseZML()
                Return VazorPage.CreateNew("Login", "Areas\Identity\Pages\Account", Title, html)
            End Get
        End Property

        <BindProperty>
        Public Property Input As InputModel
        Public Property ExternalLogins As IList(Of AuthenticationScheme)
        Public Property ReturnUrl As String
        <TempData>
        Public Property ErrorMessage As String

        Public Class InputModel
            <Required>
            <EmailAddress>
            Public Property Email As String
            <Required>
            <DataType(DataType.Password)>
            Public Property Password As String
            <Display(Name:="Remember me?")>
            Public Property RememberMe As Boolean
        End Class

        Public Async Function OnGetAsync(ByVal Optional returnUrl As String = Nothing) As Task
            If Not String.IsNullOrEmpty(ErrorMessage) Then
                ModelState.AddModelError(String.Empty, ErrorMessage)
            End If

            returnUrl = If(returnUrl, Url.Content("~/"))
            Await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme)
            ExternalLogins = (Await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            returnUrl = returnUrl
        End Function

        Public Async Function OnPostAsync(ByVal Optional returnUrl As String = Nothing) As Task(Of IActionResult)
            returnUrl = If(returnUrl, Url.Content("~/"))

            If ModelState.IsValid Then
                Dim result = Await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure:=True)

                If result.Succeeded Then
                    _logger.LogInformation("User logged in.")
                    Return LocalRedirect(returnUrl)
                End If

                If result.RequiresTwoFactor Then
                    Return RedirectToPage("./LoginWith2fa", New With {
                            Key .ReturnUrl = returnUrl,
                            Key .RememberMe = Input.RememberMe
                    })
                End If

                If result.IsLockedOut Then
                    _logger.LogWarning("User account locked out.")
                    Return RedirectToPage("./Lockout")
                Else
                    ModelState.AddModelError(String.Empty, "Invalid login attempt.")
                    Return Page()
                End If
            End If

            Return Page()
        End Function
    End Class
End Namespace
