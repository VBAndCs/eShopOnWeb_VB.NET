Imports Microsoft.AspNetCore.Http
Imports Microsoft.AspNetCore.Routing
Imports Microsoft.Extensions.Diagnostics.HealthChecks
Imports System.Net.Http
Imports System.Threading
Imports System.Threading.Tasks

Namespace HealthChecks
    Public Class ApiHealthCheck
        Implements IHealthCheck

        Private ReadOnly _httpContextAccessor As IHttpContextAccessor
        Private ReadOnly _linkGenerator As LinkGenerator

        Public Sub New(ByVal httpContextAccessor As IHttpContextAccessor, ByVal linkGenerator As LinkGenerator)
            _httpContextAccessor = httpContextAccessor
            _linkGenerator = linkGenerator
        End Sub

        Public Async Function CheckHealthAsync(ByVal context As HealthCheckContext, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of HealthCheckResult) Implements IHealthCheck.CheckHealthAsync
            Dim request = _httpContextAccessor.HttpContext.Request
            Dim apiLink As String = _linkGenerator.GetPathByAction("List", "Catalog")
            Dim myUrl As String = request.Scheme & "://" + request.Host.ToString() & apiLink
            Dim client = New HttpClient()
            Dim response = Await client.GetAsync(myUrl)
            Dim pageContents = Await response.Content.ReadAsStringAsync()

            If pageContents.Contains(".NET Bot Black Sweatshirt") Then
                Return HealthCheckResult.Healthy("The check indicates a healthy result.")
            End If

            Return HealthCheckResult.Unhealthy("The check indicates an unhealthy result.")
        End Function
    End Class
End Namespace
