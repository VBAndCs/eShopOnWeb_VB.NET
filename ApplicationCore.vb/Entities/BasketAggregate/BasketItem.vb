Namespace Entities.BasketAggregate
    Public Class BasketItem
        Inherits BaseEntity

        Private _UnitPrice As Decimal
        Private _Quantity As Integer
        Private _CatalogItemId As Integer
        Private _BasketId As Integer

        Public Property UnitPrice As Decimal
            Get
                Return _UnitPrice
            End Get
            Private Set
                _UnitPrice = Value
            End Set
        End Property

        Public Property Quantity As Integer
            Get
                Return _Quantity
            End Get
            Private Set
                _Quantity = Value
            End Set
        End Property

        Public Property CatalogItemId As Integer
            Get
                Return _CatalogItemId
            End Get
            Private Set
                _CatalogItemId = Value
            End Set
        End Property

        Public Property BasketId As Integer
            Get
                Return _BasketId
            End Get
            Private Set
                _BasketId = Value
            End Set
        End Property

        Public Sub New(ByVal catalogItemId As Integer, ByVal quantity As Integer, ByVal unitPrice As Decimal)
            Me.CatalogItemId = catalogItemId
            Me.Quantity = quantity
            Me.UnitPrice = unitPrice
        End Sub

        Public Sub AddQuantity(ByVal quantity As Integer)
            Me.Quantity += quantity
        End Sub

        Public Sub SetNewQuantity(ByVal quantity As Integer)
            Me.Quantity = quantity
        End Sub
    End Class
End Namespace
