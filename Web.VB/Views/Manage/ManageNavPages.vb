Imports System
Imports System.Runtime.CompilerServices
Imports Microsoft.AspNetCore.Mvc.Rendering
Imports Microsoft.AspNetCore.Mvc.ViewFeatures

Namespace Views.Manage
    Public Module ManageNavPages
        Public ReadOnly Property ActivePageKey As String
            Get
                Return "ActivePage"
            End Get
        End Property

        Public ReadOnly Property Index As String
            Get
                Return "Index"
            End Get
        End Property

        Public ReadOnly Property ChangePassword As String
            Get
                Return "ChangePassword"
            End Get
        End Property

        Public ReadOnly Property ExternalLogins As String
            Get
                Return "ExternalLogins"
            End Get
        End Property

        Public ReadOnly Property TwoFactorAuthentication As String
            Get
                Return "TwoFactorAuthentication"
            End Get
        End Property

        Function IndexNavClass(ByVal viewContext As ViewContext) As String
            Return PageNavClass(viewContext, Index)
        End Function

        Function ChangePasswordNavClass(ByVal viewContext As ViewContext) As String
            Return PageNavClass(viewContext, ChangePassword)
        End Function

        Function ExternalLoginsNavClass(ByVal viewContext As ViewContext) As String
            Return PageNavClass(viewContext, ExternalLogins)
        End Function

        Function TwoFactorAuthenticationNavClass(ByVal viewContext As ViewContext) As String
            Return PageNavClass(viewContext, TwoFactorAuthentication)
        End Function

        Function PageNavClass(ByVal viewContext As ViewContext, ByVal page As String) As String
            Dim activePage = TryCast(viewContext.ViewData("ActivePage"), String)
            Return If(String.Equals(activePage, page, StringComparison.OrdinalIgnoreCase), "active", Nothing)
        End Function

        <Extension()>
        Sub AddActivePage(ByVal viewData As ViewDataDictionary, ByVal activePage As String)
            viewData(ActivePageKey) = activePage
        End Sub

    End Module
End Namespace
