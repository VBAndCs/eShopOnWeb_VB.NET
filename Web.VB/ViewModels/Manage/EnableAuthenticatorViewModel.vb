Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations

Namespace ViewModels.Manage
    Public Class EnableAuthenticatorViewModel
        <Required>
        <StringLength(7, ErrorMessage:="The {0} must be at least {2} and at max {1} characters long.", MinimumLength:=6)>
        <DataType(DataType.Text)>
        <Display(Name:="Verification Code")>
        Public Property Code As String
        <[ReadOnly](True)>
        Public Property SharedKey As String
        Public Property AuthenticatorUri As String
    End Class
End Namespace
