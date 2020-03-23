Imports Microsoft.AspNetCore.Identity
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.eShopWeb.Infrastructure.Identity
Imports Microsoft.eShopWeb.Web.Interfaces
Imports Microsoft.eShopWeb.Web.Pages.Basket
Imports Microsoft.eShopWeb.Web.ViewModels
Imports System.Linq
Imports System.Threading.Tasks
Imports ZML

Namespace Pages.Shared.Components.BasketComponent
    Public Class Basket
        Inherits ViewComponent

        Private ReadOnly _basketService As IBasketViewModelService
        Private ReadOnly _signInManager As SignInManager(Of ApplicationUser)

        Public Sub New(ByVal basketService As IBasketViewModelService, ByVal signInManager As SignInManager(Of ApplicationUser))
            _basketService = basketService
            _signInManager = signInManager
        End Sub

        Public Async Function InvokeAsync(ByVal userName As String) As Task(Of IViewComponentResult)
            Dim vm = New BasketComponentViewModel()
            vm.ItemsCount = (Await GetBasketViewModelAsync()).Items.Sum(Function(i) i.Quantity)
            Return View(vm)
        End Function

        Private Async Function GetBasketViewModelAsync() As Task(Of BasketViewModel)
            If _signInManager.IsSignedIn(HttpContext.User) Then
                Return Await _basketService.GetOrCreateBasketForUser(User.Identity.Name)
            End If

            Dim anonymousId As String = GetBasketIdFromCookie()
            If anonymousId Is Nothing Then Return New BasketViewModel()
            Return Await _basketService.GetOrCreateBasketForUser(anonymousId)
        End Function

        Private Function GetBasketIdFromCookie() As String
            If Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME) Then
                Return Request.Cookies(Constants.BASKET_COOKIENAME)
            End If

            Return Nothing
        End Function
    End Class
End Namespace
