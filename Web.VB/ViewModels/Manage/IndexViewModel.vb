Imports System.ComponentModel.DataAnnotations

Namespace ViewModels.Manage
    Public Class IndexViewModel
        Public Property Username As String
        Public Property IsEmailConfirmed As Boolean
        <Required>
        <EmailAddress>
        Public Property Email As String
        <Phone>
        <Display(Name:="Phone number")>
        Public Property PhoneNumber As String
        Public Property StatusMessage As String
    End Class
End Namespace
