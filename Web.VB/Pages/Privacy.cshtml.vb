Imports Microsoft.AspNetCore.Mvc.RazorPages
Imports Vazor
Imports ZML

Namespace Pages
    Public Class PrivacyModel : Inherits PageModel

        Const Title = "Privacy Policy"

        Public ReadOnly Property ViewName As String
            Get
                ViewData("Title") = Title
                Dim html = Me.GetVbXml().ParseZML()
                Return VazorPage.CreateNew("Privacy", "Pages", Title, html)
            End Get
        End Property

        Public Sub OnGet()

        End Sub

    End Class
End Namespace