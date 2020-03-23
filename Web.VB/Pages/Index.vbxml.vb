Namespace Pages

    Partial Public Class IndexModel
        Private Function GetVbXml() As XElement

            Return _
<zml xmlns:z="zml">
    <z:model type="IndexModel"/>

    <section class="esh-catalog-hero">
        <div class="container">
            <img class="esh-catalog-title" src="~/images/main_banner_text.png"/>
        </div>
    </section>

    <section class="esh-catalog-filters">
        <div class="container">
            <form method="get">
                <label class="esh-catalog-label" data-title="brand">
                    <select asp-for="CatalogModel.BrandFilterApplied" asp-items="CatalogModel.Brands" class=" esh-catalog-filter"></select>
                </label>
                <label class="esh-catalog-label" data-title="type">
                    <select asp-for="CatalogModel.TypesFilterApplied" asp-items="CatalogModel.Types" class=" esh-catalog-filter"></select>
                </label>
                <input class="esh-catalog-send" type="image" src="images/arrow-right.svg"/>
            </form>
        </div>
    </section>

    <div class="container">
        <%= (Function()
                 If Me.CatalogModel.CatalogItems.Any() Then
                     Return <zml>
                                <partial name="_pagination" for="CatalogModel.PaginationInfo"/>
                                <div class="esh-catalog-items row">
                                    <%= (Iterator Function()
                                             For i = 0 To CatalogModel.CatalogItems.Count - 1
                                                 Yield _
                                                     <div class="esh-catalog-item col-md-4">
                                                         <partial name="_product" for=<%= $"CatalogItems[{i}]" %>/>
                                                     </div>
                                             Next
                                         End Function)() %>
                                </div>
                                <partial name="_pagination" for="CatalogModel.PaginationInfo"/>
                            </zml>
                 Else
                     Return _
        <div Class="esh-catalog-items row">
                THERE ARE NO RESULTS THAT MATCH YOUR SEARCH
            </div>
                 End If
             End Function)() %>
    </div>
</zml>
        End Function
    End Class
End Namespace