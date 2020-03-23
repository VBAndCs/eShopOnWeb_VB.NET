Imports Vazor

Namespace Pages.Shared
    Partial Public Class ProductView
        Inherits VazorSharedView

        Public Sub New()
            MyBase.New("_product", "Pages\Shared", "Product")
        End Sub

        Public Overrides Function GetVbXml() As XElement
            Return _
 _
        <zml xmlns:z="zml">
            <z:model type="CatalogItemViewModel"/>

            <form asp-page="\Basket\Index" method="post">
                <img class="esh-catalog-thumbnail" src="@Model.PictureUri"/>
                <input class="esh-catalog-button" type="submit" value="[ ADD TO BASKET ]"/>
                <div class="esh-catalog-name">
                    <span>@Model.Name</span>
                </div>
                <div class="esh-catalog-price">
                    <span>@Model.Price.ToString("N2")</span>
                </div>
                <input type="hidden" asp-for="@Model.Id" name="id"/>
                <input type="hidden" asp-for="@Model.Name" name="name"/>
                <input type="hidden" asp-for="@Model.PictureUri" name="pictureUri"/>
                <input type="hidden" asp-for="@Model.Price" name="price"/>
            </form>

        </zml>

        End Function


    End Class
End Namespace