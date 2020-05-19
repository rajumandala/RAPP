Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net.Http
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Web
Imports System.Web.Http
Imports System.Web.Http.Controllers
Imports System.Web.Http.Metadata

Namespace XmlExternalEntitiesUnsafeApi.Util
    <AttributeUsage(AttributeTargets.[Class] Or AttributeTargets.Parameter, AllowMultiple:=False, Inherited:=True)>
    Public NotInheritable Class RawRequestDataAttribute
        Inherits ParameterBindingAttribute

        Public Overrides Function GetBinding(ByVal parameter As HttpParameterDescriptor) As HttpParameterBinding
            If parameter Is Nothing Then Throw New ArgumentException("Invalid parameter")
            Return New RawRequestDataParameterBinding(parameter)
        End Function
    End Class

    Public Class RawRequestDataParameterBinding
        Inherits HttpParameterBinding

        Public Sub New(ByVal descriptor As HttpParameterDescriptor)
            MyBase.New(descriptor)
        End Sub

        Public Overrides Function ExecuteBindingAsync(ByVal metadataProvider As ModelMetadataProvider, ByVal actionContext As HttpActionContext, ByVal cancellationToken As CancellationToken) As Task
            Dim binding = actionContext.ActionDescriptor.ActionBinding
            If binding.ParameterBindings.Length > 1 OrElse actionContext.Request.Method = HttpMethod.[Get] Then Return EmptyTask.Start()
            Dim type = binding.ParameterBindings(0).Descriptor.ParameterType

            If type = GetType(String) Then
                Return actionContext.Request.Content.ReadAsStringAsync().ContinueWith(Function(task)
                                                                                          Dim stringResult = task.Result
                                                                                          SetValue(actionContext, stringResult)
                                                                                      End Function)
            ElseIf type = GetType(Byte()) Then
                Return actionContext.Request.Content.ReadAsByteArrayAsync().ContinueWith(Function(task)
                                                                                             Dim result As Byte() = task.Result
                                                                                             SetValue(actionContext, result)
                                                                                         End Function)
            End If

            Throw New InvalidOperationException("Only string and byte[] are supported for [RawRequestData] parameters")
        End Function

        Public Overrides ReadOnly Property WillReadBody As Boolean
            Get
                Return True
            End Get
        End Property
    End Class

    Public Class EmptyTask
        Public Shared Function Start() As Task
            Dim taskSource = New TaskCompletionSource(Of AsyncVoid)()
            taskSource.SetResult(Nothing)
            Return TryCast(taskSource.Task, Task)
        End Function

        Private Structure AsyncVoid
        End Structure
    End Class
End Namespace

