Imports System.ComponentModel.DataAnnotations

Namespace ViewModels.Account
    Public Class LoginViewModel
        <Required>
        <EmailAddress>
        Public Property Email As String
        <Required>
        <DataType(DataType.Password)>
        Public Property Password As String
        <Display(Name:="Remember me?")>
        Public Property RememberMe As Boolean
    End Class
End Namespace
