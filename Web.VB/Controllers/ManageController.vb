Imports Microsoft.AspNetCore.Authentication
Imports Microsoft.AspNetCore.Authorization
Imports Microsoft.AspNetCore.Identity
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.eShopWeb.ApplicationCore.Interfaces
Imports Microsoft.eShopWeb.Infrastructure.Identity
Imports Microsoft.eShopWeb.Web.Services
Imports Microsoft.eShopWeb.Web.ViewModels.Manage
Imports System.Text
Imports System.Text.Encodings.Web

Namespace Controllers
    <ApiExplorerSettings(IgnoreApi:=True)>
    <Authorize>
    <Route("[controller]/[action]")>
    Public Class ManageController
        Inherits Controller

        Private ReadOnly _userManager As UserManager(Of ApplicationUser)
        Private ReadOnly _signInManager As SignInManager(Of ApplicationUser)
        Private ReadOnly _emailSender As IEmailSender
        Private ReadOnly _logger As IAppLogger(Of ManageController)
        Private ReadOnly _urlEncoder As UrlEncoder
        Private Const AuthenicatorUriFormat As String = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6"

        Public Sub New(ByVal userManager As UserManager(Of ApplicationUser), ByVal signInManager As SignInManager(Of ApplicationUser), ByVal emailSender As IEmailSender, ByVal logger As IAppLogger(Of ManageController), ByVal urlEncoder As UrlEncoder)
            _userManager = userManager
            _signInManager = signInManager
            _emailSender = emailSender
            _logger = logger
            _urlEncoder = urlEncoder
        End Sub

        <TempData>
        Public Property StatusMessage As String

        <HttpGet>
        Public Async Function MyAccount() As Task(Of IActionResult)
            Dim _user = Await _userManager.GetUserAsync(User)

            If _user Is Nothing Then
                Throw New ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.")
            End If

            Dim model = New IndexViewModel With {
                .Username = _user.UserName,
                .Email = _user.Email,
                .PhoneNumber = _user.PhoneNumber,
                .IsEmailConfirmed = _user.EmailConfirmed,
                .StatusMessage = StatusMessage
            }
            Return View(model)
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function Index(ByVal model As IndexViewModel) As Task(Of IActionResult)
            If Not ModelState.IsValid Then
                Return View(model)
            End If

            Dim _user = Await _userManager.GetUserAsync(User)

            If _user Is Nothing Then
                Throw New ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.")
            End If

            Dim email = _user.Email

            If model.Email <> email Then
                Dim setEmailResult = Await _userManager.SetEmailAsync(_user, model.Email)

                If Not setEmailResult.Succeeded Then
                    Throw New ApplicationException($"Unexpected error occurred setting email for user with ID '{_user.Id}'.")
                End If
            End If

            Dim phoneNumber = _user.PhoneNumber

            If model.PhoneNumber <> phoneNumber Then
                Dim setPhoneResult = Await _userManager.SetPhoneNumberAsync(_user, model.PhoneNumber)

                If Not setPhoneResult.Succeeded Then
                    Throw New ApplicationException($"Unexpected error occurred setting phone number for user with ID '{_user.Id}'.")
                End If
            End If

            StatusMessage = "Your profile has been updated"
            Return RedirectToAction("Index")
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function SendVerificationEmail(ByVal model As IndexViewModel) As Task(Of IActionResult)
            If Not ModelState.IsValid Then
                Return View(model)
            End If

            Dim _user = Await _userManager.GetUserAsync(User)

            If _user Is Nothing Then
                Throw New ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.")
            End If

            Dim code = Await _userManager.GenerateEmailConfirmationTokenAsync(_user)
            Dim callbackUrl = Url.EmailConfirmationLink(_user.Id, code, Request.Scheme)
            Dim email = _user.Email
            Await _emailSender.SendEmailConfirmationAsync(email, callbackUrl)
            StatusMessage = "Verification email sent. Please check your email."
            Return RedirectToAction(NameOf(Index))
        End Function

        <HttpGet>
        Public Async Function ChangePassword() As Task(Of IActionResult)
            Dim _user = Await _userManager.GetUserAsync(User)

            If _user Is Nothing Then
                Throw New ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.")
            End If

            Dim hasPassword = Await _userManager.HasPasswordAsync(_user)

            If Not hasPassword Then
                Return RedirectToAction(NameOf(SetPassword))
            End If

            Dim model = New ChangePasswordViewModel With {
                .StatusMessage = StatusMessage
            }
            Return View(model)
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function ChangePassword(ByVal model As ChangePasswordViewModel) As Task(Of IActionResult)
            If Not ModelState.IsValid Then
                Return View(model)
            End If

            Dim _user = Await _userManager.GetUserAsync(User)

            If _user Is Nothing Then
                Throw New ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.")
            End If

            Dim changePasswordResult = Await _userManager.ChangePasswordAsync(_user, model.OldPassword, model.NewPassword)

            If Not changePasswordResult.Succeeded Then
                AddErrors(changePasswordResult)
                Return View(model)
            End If

            Await _signInManager.SignInAsync(_user, isPersistent:=False)
            _logger.LogInformation("User changed their password successfully.")
            StatusMessage = "Your password has been changed."
            Return RedirectToAction("ChangePassword")
        End Function

        <HttpGet>
        Public Async Function SetPassword() As Task(Of IActionResult)
            Dim _user = Await _userManager.GetUserAsync(User)

            If _user Is Nothing Then
                Throw New ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.")
            End If

            Dim hasPassword = Await _userManager.HasPasswordAsync(_user)

            If hasPassword Then
                Return RedirectToAction(NameOf(ChangePassword))
            End If

            Dim model = New SetPasswordViewModel With {
                .StatusMessage = StatusMessage
            }
            Return View(model)
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function SetPassword(ByVal model As SetPasswordViewModel) As Task(Of IActionResult)
            If Not ModelState.IsValid Then
                Return View(model)
            End If

            Dim _user = Await _userManager.GetUserAsync(User)

            If _user Is Nothing Then
                Throw New ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.")
            End If

            Dim addPasswordResult = Await _userManager.AddPasswordAsync(_user, model.NewPassword)

            If Not addPasswordResult.Succeeded Then
                AddErrors(addPasswordResult)
                Return View(model)
            End If

            Await _signInManager.SignInAsync(_user, isPersistent:=False)
            StatusMessage = "Your password has been set."
            Return RedirectToAction("SetPassword")
        End Function

        <HttpGet>
        Public Async Function ExternalLogins() As Task(Of IActionResult)
            Dim _user = Await _userManager.GetUserAsync(User)

            If _user Is Nothing Then
                Throw New ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.")
            End If

            Dim model = New ExternalLoginsViewModel With {
                .CurrentLogins = Await _userManager.GetLoginsAsync(_user)
            }
            model.OtherLogins = (Await _signInManager.GetExternalAuthenticationSchemesAsync()).Where(Function(auth) model.CurrentLogins.All(Function(ul) auth.Name <> ul.LoginProvider)).ToList()
            model.ShowRemoveButton = Await _userManager.HasPasswordAsync(_user) OrElse model.CurrentLogins.Count > 1
            model.StatusMessage = StatusMessage
            Return View(ExternalLoginsView.CreateNew(model, ViewData), model)
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function LinkLogin(ByVal provider As String) As Task(Of IActionResult)
            Await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme)
            Dim redirectUrl = Url.Action(NameOf(LinkLoginCallback))
            Dim properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, _userManager.GetUserId(User))
            Return New ChallengeResult(provider, properties)
        End Function

        <HttpGet>
        Public Async Function LinkLoginCallback() As Task(Of IActionResult)
            Dim _user = Await _userManager.GetUserAsync(User)

            If _user Is Nothing Then
                Throw New ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.")
            End If

            Dim info = Await _signInManager.GetExternalLoginInfoAsync(_user.Id)

            If info Is Nothing Then
                Throw New ApplicationException($"Unexpected error occurred loading external login info for _user with ID '{_user.Id}'.")
            End If

            Dim result = Await _userManager.AddLoginAsync(_user, info)

            If Not result.Succeeded Then
                Throw New ApplicationException($"Unexpected error occurred adding external login for user with ID '{_user.Id}'.")
            End If

            Await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme)
            StatusMessage = "The external login was added."
            Return RedirectToAction(NameOf(ExternalLogins))
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function RemoveLogin(ByVal model As RemoveLoginViewModel) As Task(Of IActionResult)
            Dim _user = Await _userManager.GetUserAsync(User)

            If _user Is Nothing Then
                Throw New ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.")
            End If

            Dim result = Await _userManager.RemoveLoginAsync(_user, model.LoginProvider, model.ProviderKey)

            If Not result.Succeeded Then
                Throw New ApplicationException($"Unexpected error occurred removing external login for _user with ID '{_user.Id}'.")
            End If

            Await _signInManager.SignInAsync(_user, isPersistent:=False)
            StatusMessage = "The external login was removed."
            Return RedirectToAction(NameOf(ExternalLogins))
        End Function

        <HttpGet>
        Public Async Function TwoFactorAuthentication() As Task(Of IActionResult)
            Dim _user = Await _userManager.GetUserAsync(User)

            If _user Is Nothing Then
                Throw New ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.")
            End If

            Dim model = New TwoFactorAuthenticationViewModel With {
                .HasAuthenticator = Await _userManager.GetAuthenticatorKeyAsync(_user) IsNot Nothing,
                .Is2faEnabled = _user.TwoFactorEnabled,
                .RecoveryCodesLeft = Await _userManager.CountRecoveryCodesAsync(_user)
            }
            Return View(model)
        End Function

        <HttpGet>
        Public Async Function Disable2faWarning() As Task(Of IActionResult)
            Dim _user = Await _userManager.GetUserAsync(User)

            If _user Is Nothing Then
                Throw New ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.")
            End If

            If Not _user.TwoFactorEnabled Then
                Throw New ApplicationException($"Unexpected error occured disabling 2FA for user with ID '{_user.Id}'.")
            End If

            Return View(NameOf(Disable2fa))
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function Disable2fa() As Task(Of IActionResult)
            Dim _user = Await _userManager.GetUserAsync(User)

            If User Is Nothing Then
                Throw New ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.")
            End If

            Dim disable2faResult = Await _userManager.SetTwoFactorEnabledAsync(_user, False)

            If Not disable2faResult.Succeeded Then
                Throw New ApplicationException($"Unexpected error occured disabling 2FA for user with ID '{_user.Id}'.")
            End If

            _logger.LogInformation("User with ID {UserId} has disabled 2fa.", _user.Id)
            Return RedirectToAction(NameOf(TwoFactorAuthentication))
        End Function

        <HttpGet>
        Public Async Function EnableAuthenticator() As Task(Of IActionResult)
            Dim _user = Await _userManager.GetUserAsync(User)

            If User Is Nothing Then
                Throw New ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.")
            End If

            Dim unformattedKey = Await _userManager.GetAuthenticatorKeyAsync(_user)

            If String.IsNullOrEmpty(unformattedKey) Then
                Await _userManager.ResetAuthenticatorKeyAsync(_user)
                unformattedKey = Await _userManager.GetAuthenticatorKeyAsync(_user)
            End If

            Dim model = New EnableAuthenticatorViewModel With {
                .SharedKey = FormatKey(unformattedKey),
                .AuthenticatorUri = GenerateQrCodeUri(_user.Email, unformattedKey)
            }
            Return View(model)
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function EnableAuthenticator(ByVal model As EnableAuthenticatorViewModel) As Task(Of IActionResult)
            If Not ModelState.IsValid Then
                Return View(model)
            End If

            Dim _user = Await _userManager.GetUserAsync(User)

            If User Is Nothing Then
                Throw New ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.")
            End If

            Dim verificationCode = model.Code.Replace(" ", String.Empty).Replace("-", String.Empty)
            Dim is2faTokenValid = Await _userManager.VerifyTwoFactorTokenAsync(_user, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode)

            If Not is2faTokenValid Then
                ModelState.AddModelError("model.TwoFactorCode", "Verification code is invalid.")
                Return View(model)
            End If

            Await _userManager.SetTwoFactorEnabledAsync(_user, True)
            _logger.LogInformation("User with ID {UserId} has enabled 2FA with an authenticator app.", _user.Id)
            Return RedirectToAction(NameOf(GenerateRecoveryCodes))
        End Function

        <HttpGet>
        Public Function ResetAuthenticatorWarning() As IActionResult
            Return View(NameOf(ResetAuthenticator))
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function ResetAuthenticator() As Task(Of IActionResult)
            Dim _user = Await _userManager.GetUserAsync(User)

            If User Is Nothing Then
                Throw New ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.")
            End If

            Await _userManager.SetTwoFactorEnabledAsync(_user, False)
            Await _userManager.ResetAuthenticatorKeyAsync(_user)
            _logger.LogInformation("User with id '{UserId}' has reset their authentication app key.", _user.Id)
            Return RedirectToAction(NameOf(EnableAuthenticator))
        End Function

        <HttpGet>
        Public Async Function GenerateRecoveryCodes() As Task(Of IActionResult)
            Dim _user = Await _userManager.GetUserAsync(User)

            If User Is Nothing Then
                Throw New ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.")
            End If

            If Not _user.TwoFactorEnabled Then
                Throw New ApplicationException($"Cannot generate recovery codes for user with ID '{_user.Id}' as they do not have 2FA enabled.")
            End If

            Dim recoveryCodes = Await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(_user, 10)
            Dim model = New GenerateRecoveryCodesViewModel With {
                .RecoveryCodes = recoveryCodes.ToArray()
            }
            _logger.LogInformation("User with ID {UserId} has generated new 2FA recovery codes.", _user.Id)
            Return View(model)
        End Function

        Private Sub AddErrors(ByVal result As IdentityResult)
            For Each [error] In result.Errors
                ModelState.AddModelError(String.Empty, [error].Description)
            Next
        End Sub

        Private Function FormatKey(ByVal unformattedKey As String) As String
            Dim result = New StringBuilder()
            Dim currentPosition As Integer = 0

            While currentPosition + 4 < unformattedKey.Length
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ")
                currentPosition += 4
            End While

            If currentPosition < unformattedKey.Length Then
                result.Append(unformattedKey.Substring(currentPosition))
            End If

            Return result.ToString().ToLowerInvariant()
        End Function

        Private Function GenerateQrCodeUri(ByVal email As String, ByVal unformattedKey As String) As String
            Return String.Format(AuthenicatorUriFormat, _urlEncoder.Encode("eShopOnWeb"), _urlEncoder.Encode(email), unformattedKey)
        End Function
    End Class
End Namespace
