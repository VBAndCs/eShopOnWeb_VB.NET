Imports Microsoft.AspNetCore.Mvc.ViewFeatures
Imports Microsoft.eShopWeb.Web.ViewModels.Manage
Imports Vazor

Public Class ExternalLoginsView
    Inherits VazorView

    Public ReadOnly Property Model As Object

    Public ReadOnly Property ViewData() As ViewDataDictionary

    Public Sub New(model As ExternalLoginsViewModel, viewData As ViewDataDictionary)
        MyBase.New("ExternalLogins", "Views\Home", "Hello")
        Me.Model = model
        Me.ViewData = viewData
        viewData(Views.Manage.ActivePageKey) = Views.Manage.ExternalLogins

    End Sub

    Public Shared Function CreateNew(model As ExternalLoginsViewModel, viewData As ViewDataDictionary) As String
        Return VazorViewMapper.Add(New ExternalLoginsView(model, viewData))
    End Function

End Class
