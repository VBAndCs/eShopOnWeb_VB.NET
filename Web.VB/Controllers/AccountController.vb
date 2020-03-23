Imports Microsoft.AspNetCore.Authentication
Imports Microsoft.AspNetCore.Authorization
Imports Microsoft.AspNetCore.Identity
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.eShopWeb.ApplicationCore.Interfaces
Imports Microsoft.eShopWeb.Infrastructure.Identity
Imports Microsoft.eShopWeb.Web.ViewModels.Account

Namespace Controllers
    <ApiExplorerSettings(IgnoreApi:=True)>
    <Route("[controller]/[action]")>
    <Authorize>
    Public Class AccountController
        Inherits Controller

        Private ReadOnly _userManager As UserManager(Of ApplicationUser)
        Private ReadOnly _signInManager As SignInManager(Of ApplicationUser)
        Private ReadOnly _basketService As IBasketService
        Private ReadOnly _logger As IAppLogger(Of AccountController)

        Public Sub New(ByVal userManager As UserManager(Of ApplicationUser), ByVal signInManager As SignInManager(Of ApplicationUser), ByVal basketService As IBasketService, ByVal logger As IAppLogger(Of AccountController))
            _userManager = userManager
            _signInManager = signInManager
            _basketService = basketService
            _logger = logger
        End Sub

        <HttpGet>
        <AllowAnonymous>
        Public Async Function SignIn(ByVal Optional returnUrl As String = Nothing) As Task(Of IActionResult)
            Await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme)
            ViewData("ReturnUrl") = returnUrl

            If Not String.IsNullOrEmpty(returnUrl) AndAlso returnUrl.IndexOf("checkout", StringComparison.OrdinalIgnoreCase) >= 0 Then
                ViewData("ReturnUrl") = "/Basket/Index"
            End If

            Return View()
        End Function

        <HttpPost>
        <AllowAnonymous>
        <ValidateAntiForgeryToken>
        Public Async Function SignIn(ByVal model As LoginViewModel, ByVal Optional returnUrl As String = Nothing) As Task(Of IActionResult)
            If Not ModelState.IsValid Then
                Return View(model)
            End If

            ViewData("ReturnUrl") = returnUrl
            Dim result = Await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure:=False)

            If result.RequiresTwoFactor Then
                Return RedirectToAction(NameOf(LoginWith2fa), New With {returnUrl, model.RememberMe
                })
            End If

            If result.Succeeded Then
                Dim anonymousBasketId As String = Request.Cookies(Constants.BASKET_COOKIENAME)

                If Not String.IsNullOrEmpty(anonymousBasketId) Then
                    Await _basketService.TransferBasketAsync(anonymousBasketId, model.Email)
                    Response.Cookies.Delete(Constants.BASKET_COOKIENAME)
                End If

                Return RedirectToLocal(returnUrl)
            End If

            ModelState.AddModelError(String.Empty, "Invalid login attempt.")
            Return View(model)
        End Function

        <HttpGet>
        <AllowAnonymous>
        Public Async Function LoginWith2fa(ByVal rememberMe As Boolean, ByVal Optional returnUrl As String = Nothing) As Task(Of IActionResult)
            Dim user = Await _signInManager.GetTwoFactorAuthenticationUserAsync()

            If user Is Nothing Then
                Throw New ApplicationException($"Unable to load two-factor authentication user.")
            End If

            Dim model = New LoginWith2faViewModel With {
                .RememberMe = rememberMe
            }
            ViewData("ReturnUrl") = returnUrl
            Return View(model)
        End Function

        <HttpPost>
        <AllowAnonymous>
        <ValidateAntiForgeryToken>
        Public Async Function LoginWith2fa(ByVal model As LoginWith2faViewModel, ByVal rememberMe As Boolean, ByVal Optional returnUrl As String = Nothing) As Task(Of IActionResult)
            If Not ModelState.IsValid Then
                Return View(model)
            End If

            Dim _user = Await _signInManager.GetTwoFactorAuthenticationUserAsync()

            If User Is Nothing Then
                Throw New ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.")
            End If

            Dim authenticatorCode = model.TwoFactorCode.Replace(" ", String.Empty).Replace("-", String.Empty)
            Dim result = Await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, model.RememberMachine)

            If result.Succeeded Then
                _logger.LogInformation("User with ID {UserId} logged in with 2fa.", _user.Id)
                Return RedirectToLocal(returnUrl)
            ElseIf result.IsLockedOut Then
                _logger.LogWarning("User with ID {UserId} account locked out.", _user.Id)
                Return RedirectToAction(NameOf(Lockout))
            Else
                _logger.LogWarning("Invalid authenticator code entered for user with ID {UserId}.", _user.Id)
                ModelState.AddModelError(String.Empty, "Invalid authenticator code.")
                Return View()
            End If
        End Function

        <HttpGet>
        <AllowAnonymous>
        Public Function Lockout() As IActionResult
            Return View()
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function SignOut() As Task(Of ActionResult)
            Await _signInManager.SignOutAsync()
            Return RedirectToPage("/Index")
        End Function

        <AllowAnonymous>
        <HttpGet>
        Public Function Register() As IActionResult
            Return View()
        End Function

        <HttpPost>
        <AllowAnonymous>
        <ValidateAntiForgeryToken>
        Public Async Function Register(ByVal model As RegisterViewModel, ByVal Optional returnUrl As String = Nothing) As Task(Of IActionResult)
            If ModelState.IsValid Then
                Dim user = New ApplicationUser With {
                    .UserName = model.Email,
                    .Email = model.Email
                }
                Dim result = Await _userManager.CreateAsync(user, model.Password)

                If result.Succeeded Then
                    Await _signInManager.SignInAsync(user, isPersistent:=False)
                    Return RedirectToLocal(returnUrl)
                End If

                AddErrors(result)
            End If

            Return View(model)
        End Function

        <HttpGet>
        <AllowAnonymous>
        Public Async Function ConfirmEmail(ByVal userId As String, ByVal code As String) As Task(Of IActionResult)
            If userId Is Nothing OrElse code Is Nothing Then
                Return RedirectToPage("/Index")
            End If

            Dim user = Await _userManager.FindByIdAsync(userId)

            If user Is Nothing Then
                Throw New ApplicationException($"Unable to load user with ID '{userId}'.")
            End If

            Dim result = Await _userManager.ConfirmEmailAsync(user, code)
            Return View(If(result.Succeeded, "ConfirmEmail", "Error"))
        End Function

        <HttpGet>
        <AllowAnonymous>
        Public Function ResetPassword(ByVal Optional code As String = Nothing) As IActionResult
            If code Is Nothing Then
                Throw New ApplicationException("A code must be supplied for password reset.")
            End If

            Dim model = New ResetPasswordViewModel With {
                .Code = code
            }
            Return View(model)
        End Function

        Private Function RedirectToLocal(ByVal returnUrl As String) As IActionResult
            If Url.IsLocalUrl(returnUrl) Then
                Return Redirect(returnUrl)
            Else
                Return RedirectToPage("/Index")
            End If
        End Function

        Private Sub AddErrors(ByVal result As IdentityResult)
            For Each [error] In result.Errors
                ModelState.AddModelError("", [error].Description)
            Next
        End Sub
    End Class
End Namespace
