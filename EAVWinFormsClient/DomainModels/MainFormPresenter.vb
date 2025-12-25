''' <summary>
''' Представляет слой посредника между источником данных и представлением (UI) в шаблоне разработки MVP.
''' Может содержать логику валидирования и конвертирования данных.
''' </summary>
Public Class MainFormPresenter

    Private ReadOnly _DataProvider As IDataProvider
    Private _CurrentObjectTemplateName As TemplateNameDTO
    Private _CurrentObjectTemplate As ObjectTemplate
    Private _CurrentObjectTemplateProperty As ObjectTemplateProperty

    ''' <summary>
    ''' Поставщик данных.
    ''' </summary>
    Public ReadOnly Property DataProvider As IDataProvider
        Get
            Return _DataProvider
        End Get
    End Property

    ''' <summary>
    ''' Текущее имя шаблона EAV-объекта.
    ''' </summary>
    Public Property CurrentObjectTemplateName As TemplateNameDTO
        Get
            Return _CurrentObjectTemplateName
        End Get
        Set
            _CurrentObjectTemplateName = Value
            SetObjectTemplateByDTO(_CurrentObjectTemplateName)
            OnCurrentObjectTemplateNameChanged(EventArgs.Empty)
        End Set
    End Property

    ''' <summary>
    ''' Текущий шаблон EAV-объекта.
    ''' </summary>
    Public Property CurrentObjectTemplate As ObjectTemplate
        Get
            Return _CurrentObjectTemplate
        End Get
        Set
            _CurrentObjectTemplate = Value
            OnCurrentObjectTemplateChanged(EventArgs.Empty)
        End Set
    End Property


    ''' <summary>
    ''' Текущее свойство шаблона EAV-объекта.
    ''' </summary>
    Public Property CurrentObjectTemplateProperty As ObjectTemplateProperty
        Get
            Return _CurrentObjectTemplateProperty
        End Get
        Set
            _CurrentObjectTemplateProperty = Value
            OnCurrentObjectTemplatePropertyChanged(EventArgs.Empty)
        End Set
    End Property

    ''' <summary>
    ''' Происходит при изменении CurrentObjectTemplateName.
    ''' </summary>
    Public Event CurrentObjectTemplateNameChanged As EventHandler

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
    End Sub

    ''' <summary>
    ''' Возвращает список DTO представляющих названия шаблонов EAV-объектов. 
    ''' </summary>
    Public Function GetObjectTemplateNames() As List(Of TemplateNameDTO)
        Return DataProvider.ObjectTamplates.GetObjectTemplates()
    End Function

    Private Sub SetObjectTemplateByDTO(ByVal objectTemplateName As TemplateNameDTO)
        If objectTemplateName IsNot Nothing Then
            Dim DTOproperties = DataProvider.ObjectTamplates.GetObjectTemplateProperties(objectTemplateName.Id)
            CurrentObjectTemplate = ObjectTemplateConverter.ToObjectTemplate(objectTemplateName, DTOproperties)
        Else
            CurrentObjectTemplate = Nothing
        End If
    End Sub

    Public Function GetValues(ByVal idEntity As Integer) As IList(Of ValueOfEntity)
        Dim DTOValues = DataProvider.Values.GetValues(idEntity)
        Dim resutlValues As New List(Of ValueOfEntity)

        For Each value As ValueDTO In DTOValues
            resutlValues.Add(ValueConverter.ToValueOfEntity(value))
        Next
        Return resutlValues
    End Function

    ''' <summary>
    ''' Вызывает событие CurrentObjectTemplateNameChanged.
    ''' </summary>
    ''' <param name="args"></param>
    Private Sub OnCurrentObjectTemplateNameChanged(ByVal args As EventArgs)
        RaiseEvent CurrentObjectTemplateNameChanged(Me, args)
    End Sub

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
