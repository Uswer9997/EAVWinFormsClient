Public Class ValueConverter
    Public Shared Function ToValueOfEntity(ByVal valueDTO As ValueDTO) As ValueOfEntity
        Dim newValueOfEntity As New ValueOfEntity
        newValueOfEntity.Id = valueDTO.Id
        newValueOfEntity.Name = valueDTO.Name
        newValueOfEntity.Value = valueDTO.Value

        Return newValueOfEntity
    End Function
End Class
