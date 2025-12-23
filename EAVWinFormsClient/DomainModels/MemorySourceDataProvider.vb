Imports EAVWinFormsClient

Public Class MemorySourceDataProvider
    Implements IDataProvider

    Private _ObjectTamplates As IObjectTemplates
    Private _Values As ValuesMemorySource

    Public ReadOnly Property ObjectTamplates As IObjectTemplates Implements IDataProvider.ObjectTamplates
        Get
            Return _ObjectTamplates
        End Get
    End Property

    Public ReadOnly Property Values As ValuesMemorySource Implements IDataProvider.Values
        Get
            Return _Values
        End Get
    End Property

    Public Sub New()
        _ObjectTamplates = New ObjectTemplatesMemorySource()
        _Values = New ValuesMemorySource()
    End Sub
End Class
