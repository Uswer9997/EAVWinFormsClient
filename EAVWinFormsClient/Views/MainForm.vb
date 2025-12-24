Imports EAVWinFormsClient

''' <summary>
''' Форма демонстрирует подход разработки, где данные для отображения извлекаются самими контролами
''' через посредника в виде поставщика данных. При этом происходит динамическое изменение связанных 
''' элементов управления для чего используется общий интерфейс IView. 
''' В качестве дальнейшего улучшения структурированности кода следует разработать классы слоя данных,
''' которые инкапсулируют и поставщика данных, и методы обработки данных, и методы привязываемые к 
''' обратным вызовам интерфейса IView (в данном проекте не реализованы).
''' </summary>
Public Class MainForm
    ' поставщик данных для всего приложения
    Private AppDataProvider As IDataProvider

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        AppDataProvider = New MemorySourceDataProvider()

        ' сконфигурируем элементы управления
        TemplatesComboBox.DisplayMember = "Name"
        TemplatesComboBox.ValueMember = "Id"
        ' делегат метода получения списка шаблонов EAV-объектов
        Dim GetTemplatesDelegate As New ExComboBox.GetDataDelegate(AddressOf AppDataProvider.ObjectTamplates.GetObjectTemplates)
        TemplatesComboBox.GetData = GetTemplatesDelegate ' сам контрол будет извлекать данные
        ' подпишем метод обработки связанных данных при изменении текущего шаблона EAV-объекта,
        ' на сответствующее событие
        AddHandler TemplatesComboBox.Changed, AddressOf SelectedTemplateChanged

        ' зависимый контрол, отображающий свойства текущего EAV-объекта
        TemplatePropertiesTreeView1.GetData = Nothing ' потому как сам TreeView запрашивать данные не должен
        ' подпишем метод на событие изменения текущего выбранного свойства
        ' Следует отметить, что событие возникает не при изменении самого узла, а при смене выделенного узла
        AddHandler TemplatePropertiesTreeView1.Changed, AddressOf CurrentTemplatePropertyChanged
    End Sub

    Private Sub SelectedTemplateChanged(sender As Object, e As EventArgs)
        If TemplatesComboBox.SelectedIndex > -1 Then
            ' Id шаблона EAV-объекта
            Dim IdEntity As Integer = TemplatesComboBox.SelectedValue
            ' получим все свойства текущего шаблона EAV-объекта
            Dim TemplateProperties = AppDataProvider.ObjectTamplates.GetObjectTemplateProperties(IdEntity)
            ' обновим отображение вызвав метод интерфейса IView
            TemplatePropertiesTreeView1.Build(TemplateProperties)
            Dim selectedNode = TemplatePropertiesTreeView1.TopNode
            selectedNode.Expand()
            TemplatePropertiesTreeView1.SelectedNode = selectedNode
        End If
    End Sub

    ''' <summary>
    ''' Происходит при смене выделенного узла дерева свойств шаблона EAV-объекта. 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CurrentTemplatePropertyChanged(sender As Object, e As EventArgs)
        Dim TV As TemplatePropertiesTreeView = DirectCast(sender, TemplatePropertiesTreeView)
        Dim TN As ExTreeNode = TV.SelectedNode

        If TN.Nodes.Count <> 0 Then

            TemplatePropertyTableLayoutPanel.SuspendLayout()

            For Each uc As ExComboBox In TemplatePropertyTableLayoutPanel.Controls.OfType(Of ExComboBox)
                RemoveHandler uc.Changed, AddressOf UsCtTe_SelectedDataChanged
            Next

            TemplatePropertyTableLayoutPanel.Controls.Clear()

            'Далее обновим UI, но построим элементы управления только для конечных свойств.
            'То есть только для тех, у которых нет вложенных свойств.
            'Это можно сделать пройдясь по узлам дерева, а можно выбрать свойства из общей коллекции.
            For Each nod As ExTreeNode In TN.Nodes
                If nod.Nodes.Count = 0 Then
                    Dim id As Integer = nod.BindingObject.IdEntity
                    ' конфигурируем элементы управления для каждого свойства
                    Dim usCtTe As New ExComboBox() ' здесь может быть любой контрол реализующий IDataControl и IView
                    usCtTe.IdEntity = id
                    usCtTe.ValueMember = "Id"
                    usCtTe.DisplayMember = "Value"
                    ' делегат извлечения данных для отображения
                    Dim getValuesDelegate As New ExComboBox.GetDataDelegate(AddressOf AppDataProvider.Values.GetValues)
                    usCtTe.GetData = getValuesDelegate ' зададим делегат получения данных
                    ' подпишем метод на обработку события изменения выбранного значения
                    AddHandler usCtTe.Changed, AddressOf UsCtTe_SelectedDataChanged

                    TemplatePropertyTableLayoutPanel.Controls.Add(usCtTe)
                End If
            Next

            TemplatePropertyTableLayoutPanel.ResumeLayout()
        End If

    End Sub

    ''' <summary>
    ''' Обработчик изменения выбранного значения для свойства EAV-объекта.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub UsCtTe_SelectedDataChanged(sender As Object, e As EventArgs)

        ' тут что-то делаем

    End Sub
End Class
