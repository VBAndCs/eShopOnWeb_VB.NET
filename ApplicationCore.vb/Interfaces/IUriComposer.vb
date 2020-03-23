Option Explicit On
Option Infer On
Option Strict On

Namespace Interfaces
    Public Interface IUriComposer
        Function ComposePicUri(uriTemplate As String) As String
    End Interface
End Namespace
