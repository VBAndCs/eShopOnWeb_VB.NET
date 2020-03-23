Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace Pages.Basket
    Public Class BasketViewModel
        Public Property Id As Integer
        Public Property Items As List(Of BasketItemViewModel) = New List(Of BasketItemViewModel)()
        Public Property BuyerId As String

        Public Function Total() As Decimal
            Return Math.Round(Items.Sum(Function(x) x.UnitPrice * x.Quantity), 2)
        End Function
    End Class
End Namespace
