
Imports Vazor

Namespace Views.Manage

    Public Class ViewImports
        Inherits VazorSharedView

        Public Sub New()
            MyBase.New("_ViewImports", "Views\Manage", "ViewImports")
        End Sub

        Public Overrides Function GetVbXml() As XElement
            Return <zml xmlns:z="zml">
                       <z:imports ns="Microsoft.eShopWeb.Web.Views.Manage"/>
                   </zml>
        End Function
    End Class
End Namespace