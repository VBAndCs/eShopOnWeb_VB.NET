Option Explicit On
Option Infer On
Option Strict On

Imports Microsoft.eShopWeb.ApplicationCore.Exceptions
Imports Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate
Imports System.Runtime.CompilerServices
Imports Ardalis.GuardClauses

Namespace Ardalis.GuardClauses
    Public Module BasketGuards
        <Extension()>
        Public Sub NullBasket(guardClause As IGuardClause, basketId As Integer, basket As Basket)
            If basket Is Nothing Then
                Throw New BasketNotFoundException(basketId)
            End If
        End Sub
    End Module
End Namespace
