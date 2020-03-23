Namespace Pages.Basket

    Partial Public Class CheckoutModel
        Private Function GetVbXml() As XElement

            Return _
<zml xmlns:z="zml">

    <section class="esh-catalog-hero">
        <div class="container">
            <img class="esh-catalog-title" src="~/images/main_banner_text.png"/>
        </div>
    </section>

    <div class="container">
        <h1>Thanks for your Order!</h1>

        <a asp-page="/Index">Continue Shopping...</a>
    </div>

</zml>

        End Function
    End Class
End Namespace