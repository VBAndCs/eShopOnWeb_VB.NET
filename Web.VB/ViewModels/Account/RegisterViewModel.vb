Imports System.ComponentModel.DataAnnotations

Namespace ViewModels.Account
    Public Class RegisterViewModel
        <Required>
        <EmailAddress>
        <Display(Name:="Email")>
        Public Property Email As String
        <Required>
        <StringLength(100, ErrorMessage:="The {0} must be at least {2} characters long.", MinimumLength:=6)>
        <DataType(DataType.Password)>
        <Display(Name:="Password")>
        Public Property Password As String
        <DataType(DataType.Password)>
        <Display(Name:="Confirm password")>
        <Compare("Password", ErrorMessage:="The password and confirmation password do not match.")>
        Public Property ConfirmPassword As String
    End Class
End Namespace
