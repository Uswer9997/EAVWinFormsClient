Imports EAVWinFormsClient
''' <summary>
''' Класс заглушка
''' </summary>
Public Class ObjectTemplatesMemorySource
    Implements IObjectTemplates

    Dim templateNames As New List(Of TemplateNameDTO)()
    Dim templateProperties As New List(Of TemplatePropertyDTO)()

    Sub New()
        templateNames.Add(New TemplateNameDTO() With {.Id = 1, .Name = "Фрукт"})
        templateNames.Add(New TemplateNameDTO() With {.Id = 2, .Name = "Автомобиль"})
    End Sub

    Public Function GetObjectTemplates() As List(Of TemplateNameDTO) Implements IObjectTemplates.GetObjectTemplates
        Return templateNames
    End Function

    Public Function GetObjectTemplateProperties(templateId As Integer) As List(Of TemplatePropertyDTO) Implements IObjectTemplates.GetObjectTemplateProperties
        templateProperties.Clear()

        If templateId = 1 Then
            templateProperties.Add(New TemplatePropertyDTO() With {.IdEntity = 1, .Name = "Фрукт"})
            templateProperties.Add(New TemplatePropertyDTO() With {.IdEntity = 102, .Name = "Вкус", .IdParentEntity = 1})
            templateProperties.Add(New TemplatePropertyDTO() With {.IdEntity = 101, .Name = "Цвет", .IdParentEntity = 1})
        Else
            templateProperties.Add(New TemplatePropertyDTO() With {.IdEntity = 2, .Name = "Автомобиль"})
            templateProperties.Add(New TemplatePropertyDTO() With {.IdEntity = 103, .Name = "VIN", .IdParentEntity = 2})
            templateProperties.Add(New TemplatePropertyDTO() With {.IdEntity = 101, .Name = "Цвет", .IdParentEntity = 2})
        End If

        Return templateProperties
    End Function
End Class
