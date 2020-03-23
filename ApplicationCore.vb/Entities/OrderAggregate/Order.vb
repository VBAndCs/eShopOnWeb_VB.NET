Option Explicit On
Option Infer On
Option Strict On

Imports Microsoft.eShopWeb.ApplicationCore.Interfaces
Imports Ardalis.GuardClauses


Namespace Entities.OrderAggregate
    Public Class Order
        Inherits BaseEntity
        Implements IAggregateRoot

        Private Sub New()
            ' required by EF
        End Sub

        Public Sub New(buyerId As String, shipToAddress As Address, items As List(Of OrderItem))
            Guard.Against.NullOrEmpty(buyerId, NameOf(buyerId))
            Guard.Against.Null(shipToAddress, NameOf(shipToAddress))
            Guard.Against.Null(items, NameOf(items))

            Me.BuyerId = buyerId
            Me.ShipToAddress = shipToAddress
            _orderItems = items
        End Sub
        Public Property BuyerId As String
            Get
                Return _BuyerId
            End Get
            Private Set
                _BuyerId = Value
            End Set
        End Property

        Public Property OrderDate As DateTimeOffset
            Get
                Return _OrderDate
            End Get
            Private Set
                _OrderDate = Value
            End Set
        End Property

        Public Property ShipToAddress As Address
            Get
                Return _ShipToAddress
            End Get
            Private Set
                _ShipToAddress = Value
            End Set
        End Property

        ' DDD Patterns comment
        ' Using a private collection field, better for DDD Aggregate's encapsulation
        ' so OrderItems cannot be added from "outside the AggregateRoot" directly to the collection,
        ' but only through the method Order.AddOrderItem() which includes behavior.
        Private ReadOnly _orderItems As List(Of OrderItem) = New List(Of OrderItem)()
        Private _BuyerId As String
        Private _OrderDate As DateTimeOffset = DateTimeOffset.Now
        Private _ShipToAddress As Address

        ' Using List<>.AsReadOnly() 
        ' This will create a read only wrapper around the private list so is protected against "external updates".
        ' It's much cheaper than .ToList() because it will not have to copy all items in a new collection. (Just one heap alloc for the wrapper instance)
        'https://msdn.microsoft.com/en-us/library/e78dcd75(v=vs.110).aspx 
        Public ReadOnly Property OrderItems As IReadOnlyCollection(Of OrderItem)
            Get
                Return _orderItems.AsReadOnly()
            End Get
        End Property

        Public Function Total() As Decimal
            Dim _total As Decimal = 0D
            For Each item As OrderItem In _orderItems
                _total += item.UnitPrice * item.Units
            Next

            Return _total
        End Function
    End Class
End Namespace
