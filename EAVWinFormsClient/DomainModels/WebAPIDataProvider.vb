Imports EAVWinFormsClient

''' <summary>
''' Поставщик данных из источника (Web-API) в приложение.
''' </summary>
Public Class WebAPIDataProvider
    Implements IDataProvider, IDisposable

#Region "Properties"

    Private _AccessToken As String
    Private client As Net.Http.HttpClient
    Private _ObjectTemplates As IObjectTemplates

    Public Property AccessToken As String
        Get
            Return _AccessToken
        End Get
        Set(value As String)
            _AccessToken = value
            client.DefaultRequestHeaders.Authorization =
                New Net.Http.Headers.AuthenticationHeaderValue("Bearer", _AccessToken)
        End Set
    End Property

    Public Property APIBaseUrl As String
        Get
            Return client.BaseAddress.AbsoluteUri
        End Get
        Set(value As String)
            client.BaseAddress = New Uri(value)
        End Set
    End Property

    Public ReadOnly Property ObjectTemplates As IObjectTemplates Implements IDataProvider.ObjectTamplates
        Get
            Return _ObjectTemplates
        End Get
    End Property
#End Region

#Region "Constructors"

    Public Sub New()
        client = New Net.Http.HttpClient
        _ObjectTemplates = New TreeObjectAPI(client)
    End Sub

    Public Sub New(ByVal apiBaseUrl As String)
        MyClass.New()
        Me.APIBaseUrl = apiBaseUrl
    End Sub

    Public Sub New(ByVal apiBaseUrl As String, ByVal accessToken As String)
        MyClass.New()
        Me.APIBaseUrl = apiBaseUrl
        Me.AccessToken = accessToken
    End Sub
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Для определения избыточных вызовов

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: освободить управляемое состояние (управляемые объекты).
                client?.Dispose()
                client = Nothing
            End If

            ' TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить ниже Finalize().
            ' TODO: задать большим полям значение NULL.
        End If
        disposedValue = True
    End Sub

    ' TODO: переопределить Finalize(), только если Dispose(disposing As Boolean) выше имеет код для освобождения неуправляемых ресурсов.
    'Protected Overrides Sub Finalize()
    '    ' Не изменяйте этот код. Разместите код очистки выше в методе Dispose(disposing As Boolean).
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Этот код добавлен редактором Visual Basic для правильной реализации шаблона высвобождаемого класса.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Не изменяйте этот код. Разместите код очистки выше в методе Dispose(disposing As Boolean).
        Dispose(True)
        ' TODO: раскомментировать следующую строку, если Finalize() переопределен выше.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
