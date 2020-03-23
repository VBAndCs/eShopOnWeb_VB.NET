Imports Vazor

Namespace Areas.Identity.Pages.Account
    Partial Public Class ViewImports
        Inherits VazorSharedView

        Public Sub New()
            MyBase.New("_ViewImports", "Areas\Identity\Pages\Account", "ViewImports")
        End Sub

        Public Overrides Function GetVbXml() As XElement
            Return <zml xmlns:z="zml">
                       <z:using ns="Microsoft.eShopWeb.Web.Areas.Identity.Pages.Account"/>
                   </zml>
        End Function
    End Class
End Namespace
