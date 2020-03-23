
Imports Vazor

Namespace Pages

    Public Class ViewImports
        Inherits VazorSharedView

        Public Sub New()
            MyBase.New("_ViewImports", "Pages", "ViewImports")
        End Sub

        Public Overrides Function GetVbXml() As XElement
            Return _
 _
        <zml xmlns:z="zml">
            <z:imports>Microsoft.eShopWeb.Web</z:imports>
            <z:imports ns="Microsoft.eShopWeb.Web.ViewModels"/>
            <z:using>Microsoft.eShopWeb.Web.ViewModels.Account</z:using>
            <z:using ns="Microsoft.eShopWeb.Web.ViewModels.Manage"/>
            <z:using ns="Microsoft.eShopWeb.Web.Pages"/>
            <z:using ns="Microsoft.AspNetCore.Identity"/>
            <z:using ns="Microsoft.eShopWeb.Infrastructure.Identity"/>
            <z:namespace>Microsoft.eShopWeb.Web.Pages</z:namespace>
            <z:helpers Microsoft.AspNetCore.Mvc.TagHelpers="*"/>
        </zml>

        End Function
    End Class
End Namespace