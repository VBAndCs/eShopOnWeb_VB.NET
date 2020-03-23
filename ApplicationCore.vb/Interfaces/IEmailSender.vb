Option Explicit On
Option Infer On
Option Strict On

Namespace Interfaces

    Public Interface IEmailSender
        Function SendEmailAsync(email As String, subject As String, message As String) As Task
    End Interface
End Namespace
