Imports Microsoft.AspNetCore.Http
Imports Microsoft.AspNetCore.Identity
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.AspNetCore.Mvc.RazorPages
Imports Microsoft.eShopWeb.ApplicationCore.Interfaces
Imports Microsoft.eShopWeb.Infrastructure.Identity
Imports Microsoft.eShopWeb.Web.Interfaces
Imports Microsoft.eShopWeb.Web.ViewModels
Imports Vazor

Namespace Pages.Basket
    Public Class IndexModel
        Inherits PageModel

        Private ReadOnly _basketService As IBasketService
        Private Const _basketSessionKey As String = "basketId"
        Private ReadOnly _uriComposer As IUriComposer
        Private ReadOnly _signInManager As SignInManager(Of ApplicationUser)
        Private _username As String = Nothing
        Private ReadOnly _basketViewModelService As IBasketViewModelService

        Public Sub New(ByVal basketService As IBasketService, ByVal basketViewModelService As IBasketViewModelService, ByVal uriComposer As IUriComposer, ByVal signInManager As SignInManager(Of ApplicationUser))
            _basketService = basketService
            _uriComposer = uriComposer
            _signInManager = signInManager
            _basketViewModelService = basketViewModelService
        End Sub


        Const Title = "Basket"

        Public ReadOnly Property ViewName As String
            Get
                ViewData("Title") = Title
                Dim html = Me.GetVbXml().ParseZML()
                Return VazorPage.CreateNew("Index", "Pages\Basket", Title, html)
            End Get
        End Property

        Public Property BasketModel As BasketViewModel = New BasketViewModel()

        Public Async Function OnGet() As Task
            Await SetBasketModelAsync()
        End Function

        Public Async Function OnPost(ByVal productDetails As CatalogItemViewModel) As Task(Of IActionResult)
            If productDetails?.Id Is Nothing Then
                Return RedirectToPage("/Index")
            End If

            Await SetBasketModelAsync()
            Await _basketService.AddItemToBasket(BasketModel.Id, productDetails.Id, productDetails.Price, 1)
            Await SetBasketModelAsync()
            Return RedirectToPage()
        End Function

        Public Async Function OnPostUpdate(ByVal items As Dictionary(Of String, Integer)) As Task
            Await SetBasketModelAsync()
            Await _basketService.SetQuantities(BasketModel.Id, items)
            Await SetBasketModelAsync()
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
            Dim cookieOptions = New CookieOptions With {
                .IsEssential = True
            }
            cookieOptions.Expires = DateTime.Today.AddYears(10)
            Response.Cookies.Append(Constants.BASKET_COOKIENAME, _username, cookieOptions)
        End Sub
    End Class
End Namespace
