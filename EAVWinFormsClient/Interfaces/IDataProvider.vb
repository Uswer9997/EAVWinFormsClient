''' <summary>
''' Определяет тип основного поставщика данных.
''' Через объект данного типа должны запрашиваться любые данные из источника для всего приложения.
''' Исключением могут являться данные получаемые из другого источника, например настройки приложения.
''' </summary>
Public Interface IDataProvider
    ''' <summary>
    ''' Является поставщиком данных шаблонов EAV-объектов.
    ''' </summary>
    ReadOnly Property ObjectTamplates As IObjectTemplates

    ''' <summary>
    ''' Является поставщиком значений для EAV-объектов
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property Values As ValuesSource

End Interface
