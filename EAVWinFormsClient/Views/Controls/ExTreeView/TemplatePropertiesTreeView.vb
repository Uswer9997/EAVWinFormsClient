Imports EAVWinFormsClient

Public Class TemplatePropertiesTreeView
    Inherits TreeView
    Implements IView(Of List(Of ObjectTemplateProperty))

    Private _Items As List(Of TemplatePropertyDTO)
    Public Delegate Function GetTemplatePropertiesDelegate() As List(Of TemplatePropertyDTO)
    Private GetTemplatePropertiesFunc As GetTemplatePropertiesDelegate

    ''' <summary>
    ''' Делегат метода получения данных.
    ''' </summary>
    ''' <returns></returns>
    Public Property GetData As [Delegate] Implements IView(Of List(Of ObjectTemplateProperty)).GetData
        Get
            Return GetTemplatePropertiesFunc
        End Get
        Set(value As [Delegate])
            GetTemplatePropertiesFunc = value
        End Set
    End Property

    Public Event Changed As EventHandler Implements IView(Of List(Of ObjectTemplateProperty)).Changed

    ''' <summary>
    ''' Строит дерево узлов.
    ''' </summary>
    ''' <param name="nodes">Список DTO узлов.</param>
    Public Sub Build(nodes As List(Of ObjectTemplateProperty)) Implements IView(Of List(Of ObjectTemplateProperty)).Build
        Dim lookup As Dictionary(Of Integer, List(Of ObjectTemplateProperty)) = nodes.Where(Function(n) n.IdParentEntity > 0) _
                     .GroupBy(Function(n) n.IdParentEntity) _
                     .ToDictionary(Function(g) g.Key, Function(g) g.ToList())

        Dim queue As New Queue(Of ExTreeNode)
        Dim tnc = Me.Nodes
        tnc.Clear()

        ' Корневые узлы
        For Each root In nodes.Where(Function(n) n.IdParentEntity = 0)
            queue.Enqueue(CreateAndAppendTreeNode(tnc, root))
        Next

        ' Обработка дочерних узлов
        While queue.Count > 0
            Dim current = queue.Dequeue()
            If lookup.ContainsKey(current.BindingObject.Id) Then
                current.NodeFont = New Font(Me.Font.FontFamily, Me.Font.Size, FontStyle.Bold Or FontStyle.Underline)
                current.Text = current.Text ' обход бага при изменении шрифта узла
                For Each child In lookup(current.BindingObject.Id)
                    queue.Enqueue(CreateAndAppendTreeNode(current.Nodes, child))
                Next
            End If
        End While
    End Sub

    Private Function CreateAndAppendTreeNode(nodes As TreeNodeCollection, propertyObj As ObjectTemplateProperty) As ExTreeNode
        Dim node As New ExTreeNode(propertyObj)
        node.Text = propertyObj.NameObj.Name
        nodes.Add(node)
        node.ToolTipText = node.FullPath

        If propertyObj.Mandatory Then
            node.Text += " *"
        End If

        If propertyObj.IsKey Then
            node.Text += " 🗝"
        End If

        Return node
    End Function

    Private Sub TemplatePropertiesTreeView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles Me.AfterSelect
        OnChanged(EventArgs.Empty)
    End Sub

    Public Sub OnChanged(ByVal e As EventArgs)
        RaiseEvent Changed(Me, e)
    End Sub
End Class
