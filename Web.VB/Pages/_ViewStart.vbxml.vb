Imports Vazor

Namespace Pages
    Partial Public Class ViewStart
        Inherits VazorSharedView

        Public Sub New()
            MyBase.New("_ViewStart", "Pages", "ViewStart")
        End Sub

        Public Overrides Function GetVbXml() As XElement
            Return _
 _
        <zml xmlns:z="zml">
            <z:layout page="_Layout"/>
        </zml>

        End Function


    End Class
End Namespace