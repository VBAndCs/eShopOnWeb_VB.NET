Option Explicit On
Option Infer On
Option Strict On

Imports Microsoft.EntityFrameworkCore
Imports Microsoft.eShopWeb.ApplicationCore.Entities
Imports Microsoft.eShopWeb.ApplicationCore.Interfaces


Namespace Data
    ''' <summary>
    ''' "There's some repetition here - couldn't we have some the sync methods call the async?"
    ''' https://blogs.msdn.microsoft.com/pfxteam/2012/04/13/should-i-expose-synchronous-wrappers-for-asynchronous-methods/
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    Public Class EfRepository(Of T As {BaseEntity, IAggregateRoot})
        Implements IAsyncRepository(Of T)

        Protected ReadOnly _dbContext As CatalogContext

        Public Sub New(dbContext As CatalogContext)
            _dbContext = dbContext
        End Sub

        Public Overridable Async Function GetByIdAsync(id As Integer) As Task(Of T) Implements IAsyncRepository(Of T).GetByIdAsync
            Return Await _dbContext.Set(Of T)().FindAsync(id)
        End Function

        Public Async Function ListAllAsync() As Task(Of IReadOnlyList(Of T)) Implements IAsyncRepository(Of T).ListAllAsync
            Return Await _dbContext.Set(Of T)().ToListAsync()
        End Function

        Public Async Function ListAsync(spec As ISpecification(Of T)) As Task(Of IReadOnlyList(Of T)) Implements IAsyncRepository(Of T).ListAsync
            Return Await ApplySpecification(spec).ToListAsync()
        End Function

        Public Async Function CountAsync(spec As ISpecification(Of T)) As Task(Of Integer) Implements IAsyncRepository(Of T).CountAsync
            Return Await ApplySpecification(spec).CountAsync()
        End Function

        Public Async Function AddAsync(entity As T) As Task(Of T) Implements IAsyncRepository(Of T).AddAsync
            Await _dbContext.Set(Of T)().AddAsync(entity)
            Await _dbContext.SaveChangesAsync()

            Return entity
        End Function

        Public Async Function UpdateAsync(entity As T) As Task Implements IAsyncRepository(Of T).UpdateAsync
            _dbContext.Entry(entity).State = EntityState.Modified
            Await _dbContext.SaveChangesAsync()
        End Function

        Public Async Function DeleteAsync(entity As T) As Task Implements IAsyncRepository(Of T).DeleteAsync
            _dbContext.Set(Of T)().Remove(entity)
            Await _dbContext.SaveChangesAsync()
        End Function

        Private Function ApplySpecification(spec As ISpecification(Of T)) As IQueryable(Of T)
            Return SpecificationEvaluator(Of T).GetQuery(
                          _dbContext.Set(Of T)().AsQueryable(),
                          spec
                    )
        End Function
    End Class
End Namespace
