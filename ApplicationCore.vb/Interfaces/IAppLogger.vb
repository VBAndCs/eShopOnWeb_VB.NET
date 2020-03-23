Option Explicit On
Option Infer On
Option Strict On

Namespace Interfaces
    ''' <summary>
    ''' This type eliminates the need to depend directly on the ASP.NET Core logging types.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    Public Interface IAppLogger(Of T)
        Sub LogInformation(message As String, ParamArray args As Object())
        Sub LogWarning(message As String, ParamArray args As Object())
    End Interface
End Namespace
