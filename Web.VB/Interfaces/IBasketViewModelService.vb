Imports Microsoft.eShopWeb.Web.Pages.Basket
Imports System.Threading.Tasks

Namespace Interfaces
    Public Interface IBasketViewModelService
        Function GetOrCreateBasketForUser(ByVal userName As String) As Task(Of BasketViewModel)
    End Interface
End Namespace
