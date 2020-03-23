Option Explicit On
Option Infer On
Option Strict On

Imports Microsoft.eShopWeb.ApplicationCore.Entities

Namespace Interfaces
    Public Interface IAsyncRepository(Of T As {BaseEntity, IAggregateRoot})
        Function GetByIdAsync(id As Integer) As Task(Of T)
        Function ListAllAsync() As Task(Of IReadOnlyList(Of T))
        Function ListAsync(spec As ISpecification(Of T)) As Task(Of IReadOnlyList(Of T))
        Function AddAsync(entity As T) As Task(Of T)
        Function UpdateAsync(entity As T) As Task
        Function DeleteAsync(entity As T) As Task
        Function CountAsync(spec As ISpecification(Of T)) As Task(Of Integer)
    End Interface
End Namespace
