Option Explicit On
Option Infer On
Option Strict On

Imports Ardalis.GuardClauses
Imports Microsoft.eShopWeb.ApplicationCore.Interfaces

Namespace Entities
    Public Class CatalogItem
        Inherits BaseEntity
        Implements IAggregateRoot

        Private _Name As String
        Private _Description As String
        Private _Price As Decimal
        Private _PictureUri As String
        Private _CatalogTypeId As Integer
        Private _CatalogType As CatalogType
        Private _CatalogBrandId As Integer
        Private _CatalogBrand As CatalogBrand

        Public Property Name As String
            Get
                Return _Name
            End Get
            Private Set
                _Name = Value
            End Set
        End Property

        Public Property Description As String
            Get
                Return _Description
            End Get
            Private Set
                _Description = Value
            End Set
        End Property

        Public Property Price As Decimal
            Get
                Return _Price
            End Get
            Private Set
                _Price = Value
            End Set
        End Property

        Public Property PictureUri As String
            Get
                Return _PictureUri
            End Get
            Private Set
                _PictureUri = Value
            End Set
        End Property

        Public Property CatalogTypeId As Integer
            Get
                Return _CatalogTypeId
            End Get
            Private Set
                _CatalogTypeId = Value
            End Set
        End Property

        Public Property CatalogType As CatalogType
            Get
                Return _CatalogType
            End Get
            Private Set
                _CatalogType = Value
            End Set
        End Property

        Public Property CatalogBrandId As Integer
            Get
                Return _CatalogBrandId
            End Get
            Private Set
                _CatalogBrandId = Value
            End Set
        End Property

        Public Property CatalogBrand As CatalogBrand
            Get
                Return _CatalogBrand
            End Get
            Private Set
                _CatalogBrand = Value
            End Set
        End Property

        Public Sub New(catalogTypeId As Integer, catalogBrandId As Integer, description As String, name As String, price As Decimal, pictureUri As String)
            Me.CatalogTypeId = catalogTypeId
            Me.CatalogBrandId = catalogBrandId
            Me.Description = description
            Me.Name = name
            Me.Price = price
            Me.PictureUri = pictureUri
        End Sub

        Public Sub Update(name As String, price As Decimal)
            Guard.Against.NullOrEmpty(name, NameOf(name))
            Me._Name = name
            Me._Price = price
        End Sub
    End Class
End Namespace
