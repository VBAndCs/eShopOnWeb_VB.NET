
Namespace Views.Manage
    Public Class ManageNavView
        Inherits Vazor.VazorSharedView

        Public Sub New()
            MyBase.New("_ManageNav", "Views\Manage", "ManageNav")
        End Sub

        Public Overrides Function GetVbXml() As XElement
            Return _
 _
        <zml xmlns:z="zml">
            <z:inject SignInManager.type="SignInManager(Of ApplicationUser)"/>

            <z:declare var="hasExternalLogins">
                <z:dot>
                    <z:await method="SignInManager.GetExternalAuthenticationSchemesAsync"/>
                    <z:invoke method="Any"/>
                </z:dot>
            </z:declare>

            <ul class="nav nav-pills nav-stacked">
                <li class="@ManageNavPages.IndexNavClass(ViewContext)"><a asp-action="MyAccount">Profile</a></li>
                <li class="@ManageNavPages.ChangePasswordNavClass(ViewContext)"><a asp-action="ChangePassword">Password</a></li>
                <z:if condition="hasExternalLogins">
                    <li class="@ManageNavPages.ExternalLoginsNavClass(ViewContext)">
                        <a asp-action="ExternalLogins">External logins</a>
                    </li>
                </z:if>
                <li class="@ManageNavPages.TwoFactorAuthenticationNavClass(ViewContext)">
                    <a asp-action="TwoFactorAuthentication">Two-factor authentication</a>
                </li>
            </ul>


        </zml>

        End Function


    End Class
End Namespace
