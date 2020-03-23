Namespace Areas.Identity.Pages
    Partial Public Class ViewStart
        Inherits Vazor.VazorSharedView

        Public Sub New()
            MyBase.New("_ViewStart", "Areas\Identity\Pages", "ViewStart")
        End Sub

        Public Overrides Function GetVbXml() As XElement
            Return <zml xmlns:z="zml">
                       <z:layout page="/Views/Shared/_Layout.cshtml"/>
                   </zml>
        End Function

    End Class
End Namespace
