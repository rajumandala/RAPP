Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Xml.Serialization

Namespace XmlExternalEntitiesUnsafeApi.ApiModels
    <XmlType(TypeName:="order", [Namespace]:="")>
    Public Class OrderModel
        <XmlElement(ElementName:="name")>
        Public Property Name As String
        <XmlElement(ElementName:="phone")>
        Public Property Phone As String
        <XmlArray("items")>
        <XmlArrayItem("orderItem")>
        Public Property Items As List(Of OrderItemModel)
    End Class

    Public Class OrderItemModel
        <XmlElement(ElementName:="itemName")>
        Public Property ItemName As String
        <XmlElement(ElementName:="quantity")>
        Public Property Quantity As Integer
        <XmlElement(ElementName:="specialInstructions")>
        Public Property SpecialInstructions As String
    End Class
End Namespace

