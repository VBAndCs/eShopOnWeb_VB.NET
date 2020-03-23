Imports Vazor

Namespace Views.Account
    Partial Public Class LoginWith2faView
        Inherits VazorSharedView

        Public Sub New()
            MyBase.New("LoginWith2fa", "Views\Account", "Two-factor authentication")
        End Sub

        Public Overrides Function GetVbXml() As XElement
            Return _
 _
        <zml xmlns:z="zml">
            <z:model type="LoginWith2faViewModel"/>
            <z:title>Two-factor authentication</z:title>

            <h2><z:title/></h2>
            <hr/>
            <p>Your login is protected with an authenticator app. Enter your authenticator code below.</p>
            <div class="row">
                <div class="col-md-4">
                    <form method="post" asp-route-returnUrl="@ViewData[''ReturnUrl'']">
                        <input asp-for="RememberMe" type="hidden"/>
                        <div asp-validation-summary="All" class="text-danger"></div>
                        <div class="form-group">
                            <label asp-for="TwoFactorCode"></label>
                            <input asp-for="TwoFactorCode" class="form-control" autocomplete="off"/>
                            <span asp-validation-for="TwoFactorCode" class="text-danger"></span>
                        </div>
                        <div class="form-group">
                            <div class="checkbox">
                                <label asp-for="RememberMachine">
                                    <input asp-for="RememberMachine"/>
                        @Html.DisplayNameFor( Fn(m) => m.RememberMachine )
                    </label>
                            </div>
                        </div>
                        <div class="form-group">
                            <button type="submit" class="btn btn-default">Log in</button>
                        </div>
                    </form>
                </div>
            </div>
            <p>Don't have access to your authenticator device? You can <a asp-action="LoginWithRecoveryCode" asp-route-returnUrl="@ViewData[''ReturnUrl'']">log in with a recovery code</a>.</p>

            <z:section name="Scripts">
                <partial name="_ValidationScriptsPartial"/>
            </z:section>

        </zml>

        End Function

    End Class
End Namespace