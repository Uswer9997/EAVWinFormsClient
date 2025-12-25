''' <summary>
''' Описывает свойство шаблона EAV-объекта.
''' </summary>
Public Class ObjectTemplateProperty
    Public Property Id As Integer
    Public Property IdParentEntity As Integer
    Public Property NameObj As PropertyName
    Public Property ListObj As ListName
    Public Property Position As Integer
    Public Property MeasureUnit As MeasureName
    Public Property IsKey As Boolean
    Public Property Mandatory As Boolean
    Public Property IdRelation As Integer
    Public Property DotnetType As String
    Public Property FormatPattern As String
End Class
