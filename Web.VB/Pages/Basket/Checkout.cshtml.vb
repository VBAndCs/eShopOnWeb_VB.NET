Imports Microsoft.AspNetCore.Http
Imports Microsoft.AspNetCore.Identity
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.AspNetCore.Mvc.RazorPages
Imports Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate
Imports Microsoft.eShopWeb.ApplicationCore.Interfaces
Imports Microsoft.eShopWeb.Infrastructure.Identity
Imports Microsoft.eShopWeb.Web.Interfaces
Imports Vazor

Namespace Pages.Basket
    Public Class CheckoutModel
        Inherits PageModel

        Private ReadOnly _basketService As IBasketService
        Private ReadOnly _uriComposer As IUriComposer
        Private ReadOnly _signInManager As SignInManager(Of ApplicationUser)
        Private ReadOnly _orderService As IOrderService
        Private _username As String = Nothing
        Private ReadOnly _basketViewModelService As IBasketViewModelService

        Public Sub New(ByVal basketService As IBasketService, ByVal basketViewModelService As IBasketViewModelService, ByVal uriComposer As IUriComposer, ByVal signInManager As SignInManager(Of ApplicationUser), ByVal orderService As IOrderService)
            _basketService = basketService
            _uriComposer = uriComposer
            _signInManager = signInManager
            _orderService = orderService
            _basketViewModelService = basketViewModelService
        End Sub

        Const Title = "Checkout Complete"
        Public ReadOnly Property ViewName As String
            Get
                ViewData("Title") = Title
                Dim html = Me.GetVbXml().ParseZML()
                Return VazorPage.CreateNew("Checkout", "Pages\Basket", Title, html)
            End Get
        End Property

        Public Property BasketModel As BasketViewModel = New BasketViewModel()

        Public Sub OnGet()
        End Sub

        Public Async Function OnPost(ByVal items As Dictionary(Of String, Integer)) As Task(Of IActionResult)
            Await SetBasketModelAsync()
            Await _basketService.SetQuantities(BasketModel.Id, items)
            Await _orderService.CreateOrderAsync(BasketModel.Id, New Address("123 Main St.", "Kent", "OH", "United States", "44240"))
            Await _basketService.DeleteBasketAsync(BasketModel.Id)
            Return RedirectToPage()
        End Function

        Private Async Function SetBasketModelAsync() As Task
            If _signInManager.IsSignedIn(HttpContext.User) Then
                BasketModel = Await _basketViewModelService.GetOrCreateBasketForUser(User.Identity.Name)
            Else
                GetOrSetBasketCookieAndUserName()
                BasketModel = Await _basketViewModelService.GetOrCreateBasketForUser(_username)
            End If
        End Function

        Private Sub GetOrSetBasketCookieAndUserName()
            If Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME) Then
                _username = Request.Cookies(Constants.BASKET_COOKIENAME)
            End If

            If _username IsNot Nothing Then Return
            _username = Guid.NewGuid().ToString()
            Dim cookieOptions = New CookieOptions()
            cookieOptions.Expires = DateTime.Today.AddYears(10)
            Response.Cookies.Append(Constants.BASKET_COOKIENAME, _username, cookieOptions)
        End Sub
    End Class
End Namespace
