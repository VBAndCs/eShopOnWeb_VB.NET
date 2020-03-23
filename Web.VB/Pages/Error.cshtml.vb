Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.AspNetCore.Mvc.RazorPages
Imports Vazor
Imports ZML

Namespace Pages
    <ResponseCache(Duration:=0, Location:=ResponseCacheLocation.None, NoStore:=True)>
    Public Class ErrorModel : Inherits PageModel

        Const Title = "Error"

        Public ReadOnly Property ViewName As String
            Get
                ViewData("Title") = Title
                Dim html = Me.GetVbXml().ParseZML()
                Return VazorPage.CreateNew("Error", "Pages", Title, html)
            End Get
        End Property


        Public Property RequestId As String

        Public ReadOnly Property ShowRequestId As Boolean
            Get
                Return Not String.IsNullOrEmpty(RequestId)
            End Get
        End Property

        Public Sub OnGet()
            RequestId = If(Activity.Current?.Id, HttpContext.TraceIdentifier)
        End Sub

    End Class
End Namespace