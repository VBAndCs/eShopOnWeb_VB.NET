Namespace Pages.Basket

    Partial Public Class IndexModel
        Private Function GetVbXml() As XElement

            Return _
<zml xmlns:z="zml">
    <z:model type="Pages.Basket.IndexModel"/>

    <section class="esh-catalog-hero">
        <div class="container">
            <img class="esh-catalog-title" src="~/images/main_banner_text.png"/>
        </div>
    </section>

    <div class="container">

        <z:if condition="Model.BasketModel.Items.Any()">
            <z:then>
                <form method="post">
                    <article class="esh-basket-titles row">
                        <br/>
                        <section class="esh-basket-title col-xs-3">Product</section>
                        <section class="esh-basket-title col-xs-3 hidden-lg-down"></section>
                        <section class="esh-basket-title col-xs-2">Price</section>
                        <section class="esh-basket-title col-xs-2">Quantity</section>
                        <section class="esh-basket-title col-xs-2">Cost</section>
                    </article>
                    <div class="esh-catalog-items row">
                        <z:for i="0" to="Model.BasketModel.Items.Count - 1">
                            <z:declare var="item" value="Model.BasketModel.Items" key="@i"/>
                            <article class="esh-basket-items row">
                                <div>
                                    <section class="esh-basket-item esh-basket-item--middle col-lg-3 hidden-lg-down">
                                        <img class="esh-basket-image" src="@item.PictureUrl"/>
                                    </section>
                                    <section class="esh-basket-item esh-basket-item--middle col-xs-3">@item.ProductName</section>
                                    <section class="esh-basket-item esh-basket-item--middle col-xs-2">$ @item.UnitPrice.ToString("N2")</section>
                                    <section class="esh-basket-item esh-basket-item--middle col-xs-2">
                                        <input type="hidden" name="Items[@i].Key" value="@item.Id"/>
                                        <input type="number" class="esh-basket-input" min="1" name="Items[@i].Value" value="@item.Quantity"/>
                                    </section>
                                    <section class="esh-basket-item esh-basket-item--middle esh-basket-item--mark col-xs-2">$ @Math.Round(item.Quantity * item.UnitPrice, 2).ToString("N2")</section>
                                </div>
                                <div class="row">

                                </div>
                            </article>
                            <z:comment>
                                <div class="esh-catalog-item col-md-4">
                                @item.ProductId
                            </div>
                            </z:comment>

                        </z:for>

                        <div class="container">
                            <article class="esh-basket-titles esh-basket-titles--clean row">
                                <section class="esh-basket-title col-xs-10"></section>
                                <section class="esh-basket-title col-xs-2">Total</section>
                            </article>

                            <article class="esh-basket-items row">
                                <section class="esh-basket-item col-xs-10"></section>
                                <section class="esh-basket-item esh-basket-item--mark col-xs-2">$ @Model.BasketModel.Total().ToString("N2")</section>
                            </article>

                            <article class="esh-basket-items row">
                                <section class="esh-basket-item col-xs-7"></section>
                                <section class="esh-basket-item col-xs-2">
                                    <z:comment><button class="btn esh-basket-checkout" name="name" value="" type="submit">[ Update ]</button></z:comment>
                                </section>
                            </article>
                        </div>

                        <section class="esh-basket-item col-xs-push-8 col-xs-4">
                            <button class="btn esh-basket-checkout" name="updatebutton" value="" type="submit"
                                asp-page-handler="Update">
                            [ Update ]
                        </button>
                            <input type="submit" asp-page="Checkout"
                                class="btn esh-basket-checkout"
                                value="[ Checkout ]" name="action"/>
                        </section>

                    </div>
                </form>
            </z:then>
            <z:else>
                <div class="esh-catalog-items row">
                Basket is empty.
            </div>
            </z:else>
        </z:if>
    </div>
</zml>

        End Function
    End Class
End Namespace