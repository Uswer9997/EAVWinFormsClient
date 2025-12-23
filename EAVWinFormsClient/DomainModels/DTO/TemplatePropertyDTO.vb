Imports EAVWinFormsClient

Public Class TemplatePropertyDTO
    Implements IBaseDTO

    Public Property IdEntity As Integer Implements IBaseDTO.Id
    Public Property IdParentEntity As Integer
    Public Property IdName As Integer
    Public Property Name As String Implements IBaseDTO.Name
    Public Property IdList As Integer
    Public Property NameList As String
    Public Property Position As Integer
    Public Property IdMeasureUnit As Integer
    Public Property MeasureUnit As String
    Public Property IsKey As Boolean
    Public Property Mandatory As Boolean
    Public Property IdRelation As Integer
    Public Property DotnetType As String
    Public Property FormatPattern As String

End Class
