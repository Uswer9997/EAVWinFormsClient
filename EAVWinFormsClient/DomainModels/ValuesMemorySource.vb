''' <summary>
''' Класс заглушка
''' </summary>
Public Class ValuesMemorySource
    Inherits ValuesSource

    Private _values As List(Of ValueDTO)

    Public Overrides Function GetValues(ByVal idEntity As Integer) As IList(Of ValueDTO)
        Select Case idEntity
            Case 101
                Return _values.Where(Function(x) x.Name = "Цвета").ToList
            Case 102
                Return _values.Where(Function(x) x.Name = "Вкусы").ToList
            Case 103
                Return _values.Where(Function(x) x.Name = "VINs").ToList
        End Select

        Return _values
    End Function

    Public Sub New()
        Initialize()
    End Sub

    Private Sub Initialize()
        _values = New List(Of ValueDTO)
        _values.Add(New ValueDTO() With {.Id = 201, .Name = "Цвета", .Value = "Синий"})
        _values.Add(New ValueDTO() With {.Id = 202, .Name = "Цвета", .Value = "Красный"})
        _values.Add(New ValueDTO() With {.Id = 203, .Name = "Цвета", .Value = "Оранжевый"})
        _values.Add(New ValueDTO() With {.Id = 204, .Name = "Вкусы", .Value = "Кислый"})
        _values.Add(New ValueDTO() With {.Id = 205, .Name = "Вкусы", .Value = "Сладкий"})
        _values.Add(New ValueDTO() With {.Id = 206, .Name = "Вкусы", .Value = "Горький"})
        _values.Add(New ValueDTO() With {.Id = 207, .Name = "VINs", .Value = "XTE120982183764"})
        _values.Add(New ValueDTO() With {.Id = 208, .Name = "VINs", .Value = "HTV872376912378"})
    End Sub
End Class
