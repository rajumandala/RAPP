Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports System.Xml.Serialization
Imports XmlExternalEntitiesUnsafeApi.ApiModels
Imports XmlExternalEntitiesUnsafeApi.Data
Imports System.Xml
Imports XmlExternalEntitiesUnsafeApi.Util

Namespace XmlExternalEntitiesUnsafeApi.ApiControllers
    Public Class OrdersPoxController
        Inherits ApiController

        Public Sub New(ByVal repository As OrdersRepository)
            ordersRepository = repository
        End Sub

        Private ordersRepository As OrdersRepository

        Public Function [Get]() As List(Of OrderModel)
            Dim orders = ordersRepository.GetOrders()
            Return orders
        End Function

        Public Sub Post(
<RawRequestDataAttribute> ByVal xml As String)
            Using textReader As StringReader = New StringReader(xml)
                Dim settings As XmlReaderSettings = New XmlReaderSettings()
                settings.DtdProcessing = DtdProcessing.Parse
                Dim reader As XmlReader = XmlReader.Create(textReader, settings)
                Dim serializer As XmlSerializer = New XmlSerializer(GetType(OrderModel))
                Dim orderModel = TryCast(serializer.Deserialize(reader), OrderModel)
                ordersRepository.AddOrder(orderModel)
            End Using
        End Sub
    End Class
End Namespace

