Imports Vazor

Namespace Pages.Shared.Components.BasketComponent
    Public Class DefaultView
        Inherits VazorSharedView

        Public Sub New()
            MyBase.New("Default", "Pages\Shared\Components\BasketComponent", "Default")
        End Sub

        Public Overrides Function GetVbXml() As XElement
            Return _
 _
    <zml xmlns:z="zml">
        <z:model type="BasketComponentViewModel"/>
        <z:title>My Basket</z:title>

        <a class="esh-basketstatus " asp-page="/Basket/Index">
            <div class="esh-basketstatus-image">
                <img src="~/images/cart.png"/>
            </div>
            <div class="esh-basketstatus-badge">
                @Model.ItemsCount
            </div>
        </a>

    </zml>

        End Function
    End Class

End Namespace