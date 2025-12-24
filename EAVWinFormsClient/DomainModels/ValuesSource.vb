Public MustInherit Class ValuesSource
    Public MustOverride Function GetValues(ByVal idEntity As Integer) As IList(Of ValueDTO)
End Class
