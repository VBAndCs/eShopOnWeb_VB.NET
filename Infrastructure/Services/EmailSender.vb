Option Explicit On
Option Infer On
Option Strict On
Imports Microsoft.eShopWeb.ApplicationCore.Interfaces
Imports System.Threading.Tasks

Namespace Services
    ' This class is used by the application to send email for account confirmation and password reset.
    ' For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    Public Class EmailSender
        Implements IEmailSender
        Public Function SendEmailAsync(email As String, subject As String, message As String) As Task Implements IEmailSender.SendEmailAsync
            ' TODO: Wire this up to actual email sending logic via SendGrid, local SMTP, etc.
            Return Task.CompletedTask
        End Function
    End Class
End Namespace
