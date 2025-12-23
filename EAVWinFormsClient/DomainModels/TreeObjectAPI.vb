Imports System.Net.Http
Imports System.Text.Json
Imports EAVWinFormsClient

Public Class TreeObjectAPI
    Implements IObjectTemplates

    Private client As HttpClient

    Sub New(client As HttpClient)
        Me.client = client
    End Sub


    Private Function GetObjectTemplates() As List(Of TemplateNameDTO) Implements IObjectTemplates.GetObjectTemplates
        Return GetObjectTemplatesAsync(Threading.CancellationToken.None).Result
    End Function

    Public Function GetObjectTemplatesAsync(ByVal cancelToken As Threading.CancellationToken) As Task(Of List(Of TemplateNameDTO))
        Return Task.Run(Of List(Of TemplateNameDTO))(Async Function()
                                                         Dim templates As New List(Of TemplateNameDTO)()

                                                         Dim url = $"{client.BaseAddress.AbsoluteUri}api/template-tree/trees"

                                                         Dim response As HttpResponseMessage = Await client.GetAsync(url, cancelToken)

                                                         If response.IsSuccessStatusCode Then
                                                             Dim content = Await response.Content.ReadAsStreamAsync()
                                                             Dim options = New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                                                             ' Десериализация JSON в список объектов
                                                             templates = Await JsonSerializer.DeserializeAsync(Of List(Of TemplateNameDTO))(content, options, cancelToken)
                                                         Else
                                                             Throw New Exception($"Ошибка: {response.StatusCode}. {vbNewLine} {response.Content.ReadAsStringAsync().Result}")
                                                         End If

                                                         Return templates
                                                     End Function, cancelToken)
    End Function

    Public Function GetObjectTemplateProperties(templateId As Integer) As List(Of TemplatePropertyDTO) Implements IObjectTemplates.GetObjectTemplateProperties
        Return GetObjectTemplatePropertiesAsync(templateId, Threading.CancellationToken.None).Result
    End Function

    Public Function GetObjectTemplatePropertiesAsync(templateId As Integer, ByVal cancelToken As Threading.CancellationToken) As Task(Of List(Of TemplatePropertyDTO))
        Return Task.Run(Of List(Of TemplatePropertyDTO))(Async Function()
                                                             Dim nodes As New List(Of TemplatePropertyDTO)()
                                                             Dim url As String = $"{client.BaseAddress.AbsoluteUri}api/template-tree/trees/{templateId}/nodes"

                                                             Dim response As HttpResponseMessage = Await client.GetAsync(url, cancelToken)

                                                             If response.IsSuccessStatusCode Then
                                                                 Dim content = Await response.Content.ReadAsStreamAsync()
                                                                 Dim options = New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                                                                 nodes = Await JsonSerializer.DeserializeAsync(Of List(Of TemplatePropertyDTO))(content, options, cancelToken)
                                                             Else
                                                                 Throw New Exception($"Ошибка API: {response.StatusCode}. {vbNewLine} {response.Content.ReadAsStringAsync().Result}")
                                                             End If

                                                             Return nodes
                                                         End Function)
    End Function

End Class
