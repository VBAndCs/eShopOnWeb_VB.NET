Option Explicit On
Option Infer On
Option Strict On
Imports Microsoft.eShopWeb.ApplicationCore.Interfaces
Imports Microsoft.Extensions.Logging

Namespace Logging
    Public Class LoggerAdapter(Of T)
        Implements IAppLogger(Of T)
        Private ReadOnly _logger As ILogger(Of T)
        Public Sub New(loggerFactory As ILoggerFactory)
            _logger = loggerFactory.CreateLogger(Of T)()
        End Sub

        Public Sub LogWarning(message As String, ParamArray args As Object()) Implements IAppLogger(Of T).LogWarning
            _logger.LogWarning(message, args)
        End Sub

        Public Sub LogInformation(message As String, ParamArray args As Object()) Implements IAppLogger(Of T).LogInformation
            _logger.LogInformation(message, args)
        End Sub
    End Class
End Namespace
