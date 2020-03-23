Option Explicit On
Option Infer On
Option Strict On

Namespace Entities.OrderAggregate
    Public Class Address
        Private _Street As String
        Private _City As String
        Private _State As String
        Private _Country As String
        Private _ZipCode As String

        Public Property Street As String
            Get
                Return _Street
            End Get
            Private Set
                _Street = Value
            End Set
        End Property

        Public Property City As String
            Get
                Return _City
            End Get

            Private Set
                _City = Value
            End Set
        End Property

        Public Property State As String
            Get
                Return _State
            End Get

            Private Set
                _State = Value
            End Set
        End Property

        Public Property Country As String
            Get
                Return _Country
            End Get
            Set
                _Country = Value
            End Set
        End Property

        Public Property ZipCode As String
            Get
                Return _ZipCode
            End Get
            Set
                _ZipCode = Value
            End Set
        End Property

        Private Sub New()
        End Sub

        Public Sub New(street As String, city As String, state As String, country As String, zipcode As String)
            Me.Street = street
            Me.City = city
            Me.State = state
            Me.Country = country
            Me.ZipCode = zipcode
        End Sub
    End Class
End Namespace
