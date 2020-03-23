Option Explicit On
Option Infer On
Option Strict On

Namespace Entities
    ' This can easily be modified to be BaseEntity<T> and public T Id to support different key types.
    ' Using non-generic integer types for simplicity and to ease caching logic
    Public MustInherit Class BaseEntity

        Private _Id As Integer
        Public Overridable Property Id As Integer
            Get
                Return _Id
            End Get
            Protected Set(value As Integer)
                _Id = value
            End Set
        End Property
    End Class
End Namespace
