Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.eShopWeb.Web.Controllers
Imports System.Runtime.CompilerServices

Module UrlHelperExtensions
    <Extension()>
    Function EmailConfirmationLink(ByVal urlHelper As IUrlHelper, ByVal userId As String, ByVal code As String, ByVal scheme As String) As String
        Return urlHelper.Action(action:=NameOf(AccountController.ConfirmEmail), controller:="Account", values:=New With {userId, code
        }, protocol:=scheme)
    End Function

    <Extension()>
    Function ResetPasswordCallbackLink(ByVal urlHelper As IUrlHelper, ByVal userId As String, ByVal code As String, ByVal scheme As String) As String
        Return urlHelper.Action(action:=NameOf(AccountController.ResetPassword), controller:="Account", values:=New With {userId, code
        }, protocol:=scheme)
    End Function
End Module
