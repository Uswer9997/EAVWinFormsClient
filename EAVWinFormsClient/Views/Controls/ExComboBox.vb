Public Class ExComboBox
    Inherits ComboBox
    Implements IView(Of Object), IDataControl

    Public Delegate Function GetDataDelegate(ByVal idEntity As Integer) As Object
    Private GetDataFunc As GetDataDelegate ' переменная для делегата метода получения данных
    Private IsBuilding As Boolean

    ''' <summary>
    ''' Получает или задаёт ссылку на делегат метода получения данных.
    ''' </summary>
    Public Property GetData As [Delegate] Implements IView(Of Object).GetData
        Get
            Return GetDataFunc
        End Get
        Set(value As [Delegate])
            GetDataFunc = value
        End Set
    End Property

    ''' <summary>
    ''' Получает или задаёт идентификатор EAV-объекта.
    ''' </summary>
    Public Property IdEntity As Integer Implements IDataControl.IdEntity

    ''' <summary>
    ''' Происходит при изменении свойства SelectedIndex.
    ''' </summary>
    Public Event Changed As EventHandler Implements IView(Of Object).Changed

    ''' <summary>
    ''' Привязывает данные.
    ''' </summary>
    ''' <param name="obj">Привязываемый объект.</param>
    Public Sub Build(ByVal obj As Object) Implements IView(Of Object).Build
        IsBuilding = True
        Me.BeginUpdate()
        Me.DataSource = obj
        Me.SelectedIndex = -1
        Me.EndUpdate()
        IsBuilding = False
    End Sub

    Private Sub SelectedItemChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Me.SelectedIndexChanged
        If Not IsBuilding Then ' если идёт обновление интерфейса, то не реагируем
            OnChanged(EventArgs.Empty)
        End If
    End Sub

    ''' <summary>
    ''' Обновляет привязываемые данные при раскрытии списка.
    ''' </summary>
    ''' <param name="sender">Отправитель события.</param>
    ''' <param name="e">Аргумент события.</param>
    Private Sub ComboBox_DropDown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.DropDown
        If GetDataFunc IsNot Nothing Then
            Dim data = GetDataFunc.Invoke(Me.IdEntity) ' получаем данные вызвав делегат
            Me.Build(data) ' привяжем вновь полученные данные
        End If
    End Sub

    ''' <summary>
    ''' Вызывает событие Changed.
    ''' </summary>
    ''' <param name="args">Аргумент события.</param>
    Public Overridable Sub OnChanged(ByVal args As EventArgs)
        RaiseEvent Changed(Me, args)
    End Sub
End Class
