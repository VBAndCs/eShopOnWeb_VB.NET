Imports Vazor

Namespace Areas.Identity.Pages
    Partial Public Class ViewImports
        Inherits VazorSharedView

        Public Sub New()
            MyBase.New("_ViewImports", "Areas\Identity\Pages", "ViewImports")
        End Sub

        Public Overrides Function GetVbXml() As XElement
            Return _
 _
<zml xmlns:z="zml">
    <z:using ns="Microsoft.AspNetCore.Identity"/>
    <z:using ns="Microsoft.eShopWeb.Web.Areas.Identity"/>
    <z:using ns="Microsoft.eShopWeb.Infrastructure.Identity"/>
    <z:namespace ns="Microsoft.eShopWeb.Web.Areas.Identity.Pages"/>
    <z:helpers Microsoft.AspNetCore.Mvc.TagHelpers="*"/>
</zml>

        End Function


    End Class
End Namespace
