Imports System.ComponentModel.DataAnnotations

Namespace ViewModels.Account
    Public Class ResetPasswordViewModel
        <Required>
        <EmailAddress>
        Public Property Email As String
        <Required>
        <StringLength(100, ErrorMessage:="The {0} must be at least {2} and at max {1} characters long.", MinimumLength:=6)>
        <DataType(DataType.Password)>
        Public Property Password As String
        <DataType(DataType.Password)>
        <Display(Name:="Confirm password")>
        <Compare("Password", ErrorMessage:="The password and confirmation password do not match.")>
        Public Property ConfirmPassword As String
        Public Property Code As String
    End Class
End Namespace
