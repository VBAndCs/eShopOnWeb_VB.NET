Imports Vazor

Namespace Areas.Identity.Pages
    Partial Public Class ValidationScriptsPartialView
        Inherits VazorSharedView

        Public Sub New()
            MyBase.New("_ValidationScriptsPartial", "Areas\Identity\Pages", "ValidationScriptsPartial")
        End Sub

        Public Overrides Function GetVbXml() As XElement
            Return _
 _
        <zml xmlns:z="zml">
            <environment include="Development">
                <script src="~/Identity/lib/jquery-validation/dist/jquery.validate.js"></script>
                <script src="~/Identity/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js"></script>
            </environment>
            <environment exclude="Development">
                <script src="https://ajax.aspnetcdn.com/ajax/jquery.validate/1.17.0/jquery.validate.min.js"
                    asp-fallback-src="~/Identity/lib/jquery-validation/dist/jquery.validate.min.js"
                    asp-fallback-test="window.jQuery __amp__;__amp__; window.jQuery.validator"
                    crossorigin="anonymous"
                    integrity="sha384-rZfj/ogBloos6wzLGpPkkOr/gpkBNLZ6b6yLy4o+ok+t/SAKlL5mvXLr0OXNi1Hp">
                </script>
                <script src="https://ajax.aspnetcdn.com/ajax/jquery.validation.unobtrusive/3.2.9/jquery.validate.unobtrusive.min.js"
                    asp-fallback-src="~/Identity/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js"
                    asp-fallback-test="window.jQuery __amp__;__amp__; window.jQuery.validator __amp__;__amp__; window.jQuery.validator.unobtrusive"
                    crossorigin="anonymous"
                    integrity="sha384-ifv0TYDWxBHzvAk2Z0n8R434FL1Rlv/Av18DXE43N/1rvHyOG4izKst0f2iSLdds">
                </script>
            </environment>
        </zml>

        End Function


    End Class
End Namespace
