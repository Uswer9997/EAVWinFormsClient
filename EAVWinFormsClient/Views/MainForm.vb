Imports EAVWinFormsClient

Public Class MainForm
    ' поставщик данных для всего приложения
    Private AppDataProvider As IDataProvider

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        AppDataProvider = New MemorySourceDataProvider()

        ' сконфигурируем элементы управления
        TemplatesComboBox1.DisplayMember = "Name"
        TemplatesComboBox1.ValueMember = "Id"
        Dim GetTemplatesDelegate As New TemplatesComboBox.GetTemplatesDelegate(AddressOf AppDataProvider.ObjectTamplates.GetObjectTemplates)
        TemplatesComboBox1.GetData = GetTemplatesDelegate ' сам контрол будет извлекать данные
        AddHandler TemplatesComboBox1.Changed, AddressOf SelectedTemplateChanged

        TemplatePropertiesTreeView1.GetData = Nothing ' потому как сам TreeView запрашивать данные не должен
        AddHandler TemplatePropertiesTreeView1.Changed, AddressOf CurrentTemplatePropertyChanged
    End Sub

    Private Sub SelectedTemplateChanged(sender As Object, e As EventArgs)
        If TemplatesComboBox1.SelectedIndex > -1 Then
            Dim IdEntity As Integer = TemplatesComboBox1.SelectedValue
            Dim TemplateProperties = AppDataProvider.ObjectTamplates.GetObjectTemplateProperties(IdEntity)
            TemplatePropertiesTreeView1.Build(TemplateProperties)
            TemplatePropertiesTreeView1.TopNode.Expand()
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
                    Dim usCtTe As New ExComboBox()
                    usCtTe.IdEntity = id
                    Dim getValuesDelegate As New ExComboBox.GetDataDelegate(AddressOf GetValues)

                    AddHandler usCtTe.Changed, AddressOf UsCtTe_SelectedDataChanged

                    TemplatePropertyTableLayoutPanel.Controls.Add(usCtTe)
                End If
            Next

            TemplatePropertyTableLayoutPanel.ResumeLayout()
        Else
            TemplatePropertyTableLayoutPanel.Controls.Clear()
        End If

    End Sub


    ''' <summary>
    ''' Извлекает коллекцию значений свойств EAV-объектов посредством поставщика данных.
    ''' </summary>
    ''' <returns>Возвращает список объектов приведённых к IBaseDTO.</returns>
    Private Function GetValues(ByVal id As Integer) As List(Of IBaseDTO)
        ' К сожалению мне не удалось сделать ExComboBox на дженериках,
        ' поэтому приходится создавать вот такие методы-посредники,
        ' которые занимаются приведением типов как здесь.
        ' В идеале можно было делегату указать непосредственно метод поставщика данных. 
        Return AppDataProvider.Values.GetValues().Cast(Of IBaseDTO).ToList()
    End Function

    ''' <summary>
    ''' Обработчик изменения выбранного значения для свойства EAV-объекта.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub UsCtTe_SelectedDataChanged(sender As Object, e As EventArgs)

        ' тут что-то делаем

    End Sub
End Class
