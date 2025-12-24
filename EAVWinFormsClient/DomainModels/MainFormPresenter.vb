''' <summary>
''' Представляет слой посредника между источником данных и представлением (UI) в шаблоне разработки MVP.
''' Может содержать логику валидирования и конвертирования данных.
''' </summary>
Public Class MainFormPresenter

    Private ReadOnly _DataProvider As IDataProvider
    Private _CurrentObjectTemplate As TemplateNameDTO
    Private _CurrentObjectTemplateProperty As TemplatePropertyDTO

    ''' <summary>
    ''' Поставщик данных.
    ''' </summary>
    Public ReadOnly Property DataProvider As IDataProvider
        Get
            Return _DataProvider
        End Get
    End Property

    ''' <summary>
    ''' Текущий шаблон EAV-объекта.
    ''' </summary>
    Public Property CurrentObjectTemplate As TemplateNameDTO
        Get
            Return _CurrentObjectTemplate
        End Get
        Set
            _CurrentObjectTemplate = Value
            UpdateObjectTemplateProperties()
            OnCurrentObjectTemplateChanged(EventArgs.Empty)
        End Set
    End Property


    ''' <summary>
    ''' Список свойств текущего шаблона EAV-объекта.
    ''' </summary>
    Public Property ObjectTemplateProperties As List(Of TemplatePropertyDTO)

    ''' <summary>
    ''' Текущее свойство шаблона EAV-объекта.
    ''' </summary>
    Public Property CurrentObjectTemplateProperty As TemplatePropertyDTO
        Get
            Return _CurrentObjectTemplateProperty
        End Get
        Set
            _CurrentObjectTemplateProperty = Value
            OnCurrentObjectTemplatePropertyChanged(EventArgs.Empty)
        End Set
    End Property

    ''' <summary>
    ''' Происходит при изменении CurrentObjectTemplate.
    ''' </summary>
    Public Event CurrentObjectTemplateChanged As EventHandler

    ''' <summary>
    ''' Происходит при изменении CurrentObjectTemplateProperty.
    ''' </summary>
    Public Event CurrentObjectTemplatePropertyChanged As EventHandler

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New(ByVal dataProvider As IDataProvider)
        _DataProvider = dataProvider
        ObjectTemplateProperties = New List(Of TemplatePropertyDTO)
    End Sub

    ''' <summary>
    ''' Возвращает список DTO представляющих названия шаблонов EAV-объектов. 
    ''' </summary>
    Public Function GetObjectTemplates() As List(Of TemplateNameDTO)
        Return DataProvider.ObjectTamplates.GetObjectTemplates()
    End Function

    ''' <summary>
    ''' Обновляет список свойств текущего шаблона EAV-объекта.
    ''' </summary>
    Private Sub UpdateObjectTemplateProperties()
        If CurrentObjectTemplate IsNot Nothing Then
            ' Id шаблона EAV-объекта
            Dim IdEntity As Integer = CurrentObjectTemplate.Id
            ' получим все свойства текущего шаблона EAV-объекта
            ObjectTemplateProperties = DataProvider.ObjectTamplates.GetObjectTemplateProperties(IdEntity)
        Else
            ObjectTemplateProperties.Clear()

        End If
    End Sub

    Public Function GetValues(ByVal idEntity As Integer) As IList(Of ValueDTO)
        Return DataProvider.Values.GetValues(idEntity)
    End Function

    ''' <summary>
    ''' Вызывает событие CurrentObjectTemplateChanged.
    ''' </summary>
    ''' <param name="args"></param>
    Private Sub OnCurrentObjectTemplateChanged(ByVal args As EventArgs)
        RaiseEvent CurrentObjectTemplateChanged(Me, args)
    End Sub

    ''' <summary>
    ''' Вызывает событие CurrentObjectTemplatePropertyChanged.
    ''' </summary>
    ''' <param name="args"></param>
    Private Sub OnCurrentObjectTemplatePropertyChanged(ByVal args As EventArgs)
        RaiseEvent CurrentObjectTemplatePropertyChanged(Me, args)
    End Sub
End Class
