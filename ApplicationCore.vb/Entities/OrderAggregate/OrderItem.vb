Option Explicit On
Option Infer On
Option Strict On

Namespace Entities.OrderAggregate

    Public Class OrderItem
        Inherits BaseEntity

        Private _ItemOrdered As CatalogItemOrdered
        Private _UnitPrice As Decimal
        Private _Units As Integer

        Public Property ItemOrdered As CatalogItemOrdered
            Get
                Return _ItemOrdered
            End Get
            Private Set
                _ItemOrdered = Value
            End Set
        End Property

        Public Property UnitPrice As Decimal
            Get
                Return _UnitPrice
            End Get
            Private Set
                _UnitPrice = Value
            End Set
        End Property

        Public Property Units As Integer
            Get
                Return _Units
            End Get
            Private Set
                _Units = Value
            End Set
        End Property

        Private Sub New()
            ' required by EF
        End Sub

        Public Sub New(itemOrdered As CatalogItemOrdered, unitPrice As Decimal, units As Integer)
            Me.ItemOrdered = itemOrdered
            Me.UnitPrice = unitPrice
            Me.Units = units
        End Sub
    End Class
End Namespace
