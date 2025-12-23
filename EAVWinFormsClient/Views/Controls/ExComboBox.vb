Public Class ExComboBox
    Inherits ComboBox
    Implements IView(Of List(Of IBaseDTO))

    Private _Items As List(Of IBaseDTO) ' список привязанных объектов
    Public Delegate Function GetDataDelegate(ByVal idEntity As Integer) As List(Of IBaseDTO)
    Private GetDataFunc As GetDataDelegate ' переменная для делегата метода получения данных
    Private IsBuilding As Boolean

    Public Property GetData As [Delegate] Implements IView(Of List(Of IBaseDTO)).GetData
        Get
            Return GetDataFunc
        End Get
        Set(value As [Delegate])
            GetDataFunc = value
        End Set
    End Property

    ''' <summary>
    ''' идентификатор свойства EAV-объекта
    ''' </summary>
    ''' <returns></returns>
    Public Property IdEntity As Integer

    Public Event Changed As EventHandler Implements IView(Of List(Of IBaseDTO)).Changed

    ''' <summary>
    ''' Обновляем список привязанных объектов.
    ''' </summary>
    ''' <param name="obj"></param>
    Public Sub Build(ByVal obj As List(Of IBaseDTO)) Implements IView(Of List(Of IBaseDTO)).Build
        IsBuilding = True
        _Items = obj
        Me.BeginUpdate()
        Me.DataSource = _Items
        Me.SelectedIndex = -1
        Me.EndUpdate()
        IsBuilding = False
    End Sub

    Private Sub SelectedItemChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Me.SelectedIndexChanged
        If Not IsBuilding Then ' если идёт обновление интерфейса, то не реагируем
            RaiseEvent Changed(Me, e)
        End If
    End Sub

    Private Sub ComboBox_DropDown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.DropDown
        If GetDataFunc IsNot Nothing Then
            Dim data = GetDataFunc.Invoke(Me.IdEntity) ' получаем данные вызвав делегат
            Me.Build(data) ' привяжем вновь полученные данные
        End If
    End Sub
End Class
