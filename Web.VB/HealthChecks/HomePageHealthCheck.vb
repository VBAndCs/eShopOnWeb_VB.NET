Imports Microsoft.AspNetCore.Http
Imports Microsoft.Extensions.Diagnostics.HealthChecks
Imports System.Net.Http
Imports System.Threading
Imports System.Threading.Tasks

Namespace HealthChecks
    Public Class HomePageHealthCheck
        Implements IHealthCheck

        Private ReadOnly _httpContextAccessor As IHttpContextAccessor

        Public Sub New(ByVal httpContextAccessor As IHttpContextAccessor)
            _httpContextAccessor = httpContextAccessor
        End Sub

        Public Async Function CheckHealthAsync(ByVal context As HealthCheckContext, ByVal Optional cancellationToken As CancellationToken = Nothing) As Task(Of HealthCheckResult) _
                             Implements IHealthCheck.CheckHealthAsync

            Dim request = _httpContextAccessor.HttpContext.Request
            Dim myUrl As String = request.Scheme & "://" + request.Host.ToString()
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
