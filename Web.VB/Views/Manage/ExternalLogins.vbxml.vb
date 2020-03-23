Partial Public Class ExternalLoginsView

    Public Overrides Function GetVbXml() As XElement
        Return _
 _
        <zml xmlns:z="zml">
            <z:model type="ExternalLoginsViewModel"/>
            <z:title>Manage your external logins</z:title>

            <partial name="_StatusMessage" for="StatusMessage"/>
            <%= (Function()
                     If Model.CurrentLogins?.Count > 0 Then
                         Return <zml xmlns:z="zml">
                                    <h4>Registered Logins</h4>
                                    <table class="table">
                                        <tbody>
                                            <%= (Iterator Function()
                                                     For Each login In Model.CurrentLogins
                                                         Yield <tr>
                                                                   <td>@login.LoginProvider</td>
                                                                   <td>
                                                                       <%= (Function()
                                                                                If Model.ShowRemoveButton Then
                                                                                    Return <form asp-action="RemoveLogin" method="post">
                                                                                               <div>
                                                                                                   <input asp-for="@login.LoginProvider" name="LoginProvider" type="hidden"/>
                                                                                                   <input asp-for="@login.ProviderKey" name="ProviderKey" type="hidden"/>
                                                                                                   <button type="submit" class="btn btn-default" title="Remove this @login.LoginProvider login from your account">Remove</button>
                                                                                               </div>
                                                                                           </form>
                                                                                Else
                                                                                    Return "&nbsp;"
                                                                                End If
                                                                            End Function)() %>
                                                                   </td>
                                                               </tr>
                                                     Next
                                                 End Function)() %>
                                        </tbody>
                                    </table>
                                </zml>
                     End If

                     If Model.OtherLogins?.Count > 0 Then
                         Return <zml>
                                    <h4>Add another service to log in.</h4>
                                    <hr/>
                                    <form asp-action="LinkLogin" method="post" class="form-horizontal">
                                        <div id="socialLoginList">
                                            <p>
                                                <%= (Iterator Function()
                                                         For Each provider In Model.OtherLogins
                                                             Yield <button type="submit" class="btn btn-default" name="provider" value="@provider.Name" title="Log in using your @provider.DisplayName account">@provider.DisplayName</button>
                                                         Next
                                                     End Function)() %>
                                            </p>
                                        </div>
                                    </form>
                                </zml>
                     End If
                     Return Nothing
                 End Function)() %>
        </zml>

    End Function


End Class
