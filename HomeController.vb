Imports BlogUploader.Models
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.IO
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports System.Xml

Namespace BlogUploader.Controllers
    Public Class HomeController
        Inherits Controller

        Public Function Index() As ActionResult
            Return View()
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Function Index(ByVal postedFile As HttpPostedFileBase) As ActionResult
            Dim blogs As List(Of Blog) = New List(Of Blog)()

            If postedFile IsNot Nothing Then
                Dim reader As XmlTextReader = New XmlTextReader(postedFile.InputStream)
                Dim blog As Blog = New Blog()

                While reader.Read()

                    If reader.IsStartElement() Then

                        Select Case reader.Name
                            Case "Blog"
                            Case "Title"
                                blog.Title = reader.ReadString()
                            Case "Author"
                                blog.Author = reader.ReadString()
                            Case "Description"
                                blog.Description = reader.ReadString()
                            Case "Status"
                                blog.Status = reader.ReadString()
                        End Select
                    End If
                End While

                blogs.Add(blog)
                Return View(blogs)
            End If

            Return Nothing
        End Function
        
        
        <HttpPost>
        Public Function Index(ByVal postedFile As HttpPostedFileBase) As ActionResult
            Dim emp As Employee = Nothing

            Try

                If postedFile IsNot Nothing Then
                    Dim formatter As BinaryFormatter = New BinaryFormatter()
                    Dim obj As Object = formatter.Deserialize(postedFile.InputStream)
                    emp = CType(obj, Employee)
                Else
                    ViewBag.Result = "No file uploaded"
                End If

            Catch e As Exception
                ViewBag.Result = "Error in deserializing data"
            End Try

            Return View(emp)
        End Function
    End Class
End Namespace

