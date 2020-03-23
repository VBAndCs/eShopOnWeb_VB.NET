Option Explicit On
Option Infer On
Option Strict On

Namespace Entities.BuyerAggregate
    Public Class PaymentMethod
        Inherits BaseEntity

        Private _Alias As String
        Private _CardId As String
        Private _Last4 As String

        Public Property [Alias] As String
            Get
                Return _Alias
            End Get

            Private Set
                _Alias = Value
            End Set
        End Property

        Public Property CardId As String ' actual card data must be stored in a PCI compliant system, like Stripe
            Get
                Return _CardId
            End Get
            Private Set
                _CardId = Value
            End Set
        End Property

        Public Property Last4 As String
            Get
                Return _Last4
            End Get

            Private Set
                _Last4 = Value
            End Set
        End Property
    End Class
End Namespace
