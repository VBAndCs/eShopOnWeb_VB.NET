Imports Microsoft.eShopWeb.ApplicationCore.Interfaces
Imports System.Text.Encodings.Web
Imports System.Threading.Tasks
Imports System.Runtime.CompilerServices

Namespace Services
    Module EmailSenderExtensions
        <Extension()>
        Function SendEmailConfirmationAsync(ByVal emailSender As IEmailSender, ByVal email As String, ByVal link As String) As Task
            Return emailSender.SendEmailAsync(email, "Confirm your email", $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.[Default].Encode(link)}'>link</a>")
        End Function
    End Module
End Namespace
