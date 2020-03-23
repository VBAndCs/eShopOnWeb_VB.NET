
Imports Vazor

Namespace Views.Manage

    Public Class LayoutView
        Inherits VazorSharedView

        Public Sub New()
            MyBase.New("_Layout", "Views\Manage", "Layout")
        End Sub

        Public Overrides Function GetVbXml() As XElement
            Return _
<zml xmlns:z="zml">
    <z:layout page="/Views/Shared/_Layout.cshtml"/>

    <h2>Manage your account</h2>

    <div>
        <h4>Change your account settings</h4>
        <hr/>
        <div class="row">
            <div class="col-md-3">
                <partial name="_ManageNav"/>
            </div>
            <div class="col-md-9">
                <z:invoke method="RenderBody"/>
            </div>
        </div>
    </div>

    <z:section name="Scripts">
        <z:invoke method="RenderSection">
            <z:arg>Scripts</z:arg>
            <z:arg name="required">false</z:arg>
        </z:invoke>
    </z:section>

</zml>
        End Function
    End Class
End Namespace