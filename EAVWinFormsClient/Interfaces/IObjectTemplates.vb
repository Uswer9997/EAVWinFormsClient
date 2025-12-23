
Public Interface IObjectTemplates
    Function GetObjectTemplates() As List(Of TemplateNameDTO)
    Function GetObjectTemplateProperties(templateId As Integer) As List(Of TemplatePropertyDTO)
End Interface
