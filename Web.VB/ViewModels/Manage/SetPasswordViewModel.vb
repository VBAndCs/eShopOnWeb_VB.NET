Imports System.ComponentModel.DataAnnotations

Namespace ViewModels.Manage
    Public Class SetPasswordViewModel
        <Required>
        <StringLength(100, ErrorMessage:="The {0} must be at least {2} and at max {1} characters long.", MinimumLength:=6)>
        <DataType(DataType.Password)>
        <Display(Name:="New password")>
        Public Property NewPassword As String
        <DataType(DataType.Password)>
        <Display(Name:="Confirm new password")>
        <Compare("NewPassword", ErrorMessage:="The new password and confirmation password do not match.")>
        Public Property ConfirmPassword As String
        Public Property StatusMessage As String
    End Class
End Namespace
