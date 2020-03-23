Imports Vazor

Namespace Views.Account
    Partial Public Class RegisterView
        Inherits VazorSharedView

        Public Sub New()
            MyBase.New("Register", "Views\Account", "Register")
        End Sub

        Public Overrides Function GetVbXml() As XElement
            Return _
 _
        <zml xmlns:z="zml">
            <z:model>RegisterViewModel</z:model>
            <z:title>Register</z:title>

            <div class="brand-header-block">
                <ul class="container">
                    <li class="active" style="margin-right: 65px;">Already have an account? <a asp-action="Signin">LOGIN</a></li>
                </ul>
            </div>
            <div class="container account-login-container">
                <div class="row">
                    <div class="col-md-12">
                        <section>
                            <form asp-controller="Account" asp-route-returnurl="@ViewData[''ReturnUrl'']" method="post" class="form-horizontal">
                                <div asp-validation-summary="All" class="text-danger"></div>
                                <div class="form-group">
                                    <label asp-for="Email" class="col-md-2 control-label"></label>
                                    <div class="col-md-10">
                                        <input asp-for="Email" class="form-control"/>
                                        <span asp-validation-for="Email" class="text-danger"></span>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label asp-for="Password" class="col-md-2 control-label"></label>
                                    <div class="col-md-10">
                                        <input asp-for="Password" class="form-control"/>
                                        <span asp-validation-for="Password" class="text-danger"></span>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label asp-for="ConfirmPassword" class="col-md-2 control-label"></label>
                                    <div class="col-md-10">
                                        <input asp-for="ConfirmPassword" class="form-control"/>
                                        <span asp-validation-for="ConfirmPassword" class="text-danger"></span>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <button type="submit" class="btn btn-default btn-brand btn-brand-big">REGISTER</button>
                                </div>
                                <p>Note that for demo purposes you don't need to register! Use the credentials shown below the <a asp-action="signin">login screen</a>.</p>
                            </form>
                        </section>
                    </div>
                </div>
            </div>

            <z:section name="Scripts">
                <z:await method="Html.RenderPartialAsync">
                    <z:arg>_ValidationScriptsPartial</z:arg>
                </z:await>
            </z:section>

        </zml>

        End Function

    End Class
End Namespace