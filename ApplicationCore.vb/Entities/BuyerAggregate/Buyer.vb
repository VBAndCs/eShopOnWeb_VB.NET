Option Explicit On
Option Infer On
Option Strict On

Imports Microsoft.eShopWeb.ApplicationCore.Interfaces
Imports Ardalis.GuardClauses


Namespace Entities.BuyerAggregate
    Public Class Buyer
        Inherits BaseEntity
        Implements IAggregateRoot
        Public Property IdentityGuid As String
            Get
                Return _IdentityGuid
            End Get

            Private Set
                _IdentityGuid = Value
            End Set
        End Property

        Private _paymentMethods As List(Of PaymentMethod) = New List(Of PaymentMethod)()
        Private _IdentityGuid As String

        Public ReadOnly Property PaymentMethods As IEnumerable(Of PaymentMethod)
            Get
                Return _paymentMethods.AsReadOnly()
            End Get
        End Property

        Private Sub New()
            ' required by EF
        End Sub

        Public Sub New(identity As String)
            Me.New()
            Guard.Against.NullOrEmpty(identity, NameOf(identity))
            IdentityGuid = identity
        End Sub
    End Class
End Namespace
