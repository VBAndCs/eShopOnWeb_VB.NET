Imports System.ComponentModel.DataAnnotations

Namespace ViewModels.Account
    Public Class LoginWith2faViewModel
        <Required>
        <StringLength(7, ErrorMessage:="The {0} must be at least {2} and at max {1} characters long.", MinimumLength:=6)>
        <DataType(DataType.Text)>
        <Display(Name:="Authenticator code")>
        Public Property TwoFactorCode As String
        <Display(Name:="Remember this machine")>
        Public Property RememberMachine As Boolean
        Public Property RememberMe As Boolean
    End Class
End Namespace
