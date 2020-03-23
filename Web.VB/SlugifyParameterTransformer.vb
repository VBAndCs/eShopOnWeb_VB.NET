Imports Microsoft.AspNetCore.Routing
Imports System.Text.RegularExpressions


    Public Class SlugifyParameterTransformer
    Implements IOutboundParameterTransformer

    Public Function TransformOutbound(ByVal value As Object) As String _
        Implements IOutboundParameterTransformer.TransformOutbound

        If value Is Nothing Then
            Return Nothing
        End If

        Return Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower()
    End Function
End Class
