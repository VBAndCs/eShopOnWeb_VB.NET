Imports Vazor

Namespace Views.Account
    Partial Public Class LockoutView
        Inherits VazorSharedView

        Public Sub New()
            MyBase.New("Lockout", "Views\Account", "Lockout")
        End Sub

        Public Overrides Function GetVbXml() As XElement
            Return _
 _
        <zml xmlns:z="zml">
            <z:title>Locked out</z:title>

            <header>
                <h2 class="text-danger"><z:title/></h2>
                <p class="text-danger">This account has been locked out, please try again later.</p>
            </header>
        </zml>

        End Function

    End Class
End Namespace