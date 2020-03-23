Imports Microsoft.AspNetCore.Authentication
Imports Microsoft.AspNetCore.Identity
Imports System.Collections.Generic

Namespace ViewModels.Manage
    Public Class ExternalLoginsViewModel
        Public Property CurrentLogins As IList(Of UserLoginInfo)
        Public Property OtherLogins As IList(Of AuthenticationScheme)
        Public Property ShowRemoveButton As Boolean
        Public Property StatusMessage As String
    End Class
End Namespace
