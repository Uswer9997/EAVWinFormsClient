Public Class ObjectTemplateConverter
    Public Shared Function ToObjectTemplate(ByVal objectTemplateDTO As TemplateNameDTO,
                                            ByVal objectTemplatePropertyDTO As List(Of TemplatePropertyDTO)) As ObjectTemplate
        Dim newTemplate As New ObjectTemplate
        newTemplate.Id = objectTemplateDTO.Id
        newTemplate.Name = objectTemplateDTO.Name
        newTemplate.Properties = New List(Of ObjectTemplateProperty)

        For Each prop As TemplatePropertyDTO In objectTemplatePropertyDTO
            newTemplate.Properties.Add(ToObjectTemplateProperty(prop))
        Next

        Return newTemplate
    End Function

    Public Shared Function ToObjectTemplateProperty(ByVal objectTemplatePropertyDTO As TemplatePropertyDTO) As ObjectTemplateProperty
        Dim newProperty As New ObjectTemplateProperty
        Dim newNameObj As New PropertyName
        newNameObj.Id = objectTemplatePropertyDTO.IdName
        newNameObj.Name = objectTemplatePropertyDTO.Name
        Dim newListObj As New ListName
        newListObj.Id = objectTemplatePropertyDTO.IdList
        newListObj.Name = objectTemplatePropertyDTO.NameList
        Dim newMeasureObj As New MeasureName
        newMeasureObj.Id = objectTemplatePropertyDTO.IdMeasureUnit
        newMeasureObj.Name = objectTemplatePropertyDTO.MeasureUnit

        With newProperty
            .Id = objectTemplatePropertyDTO.IdEntity
            .IdParentEntity = objectTemplatePropertyDTO.IdParentEntity
            .NameObj = newNameObj
            .ListObj = newListObj
            .Position = objectTemplatePropertyDTO.Position
            .MeasureUnit = newMeasureObj
            .IsKey = objectTemplatePropertyDTO.IsKey
            .Mandatory = objectTemplatePropertyDTO.Mandatory
            .IdRelation = objectTemplatePropertyDTO.IdRelation
            .DotnetType = objectTemplatePropertyDTO.DotnetType
            .FormatPattern = objectTemplatePropertyDTO.FormatPattern
        End With

    Return newProperty
    End Function
End Class
