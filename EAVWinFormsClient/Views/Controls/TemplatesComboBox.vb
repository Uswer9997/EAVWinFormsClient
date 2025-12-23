Imports EAVWinFormsClient

Public Class TemplatesComboBox
    Inherits ComboBox
    Implements IView(Of List(Of TemplateNameDTO))

    Private _Items As List(Of TemplateNameDTO)
    Public Delegate Function GetTemplatesDelegate() As List(Of TemplateNameDTO)
    Private GetTemplatesFunc As GetTemplatesDelegate
    Private IsBuilding As Boolean

    Public Property GetData As [Delegate] Implements IView(Of List(Of TemplateNameDTO)).GetData
        Get
            Return GetTemplatesFunc
        End Get
        Set(value As [Delegate])
            GetTemplatesFunc = value
        End Set
    End Property

    Public Event Changed As EventHandler Implements IView(Of List(Of TemplateNameDTO)).Changed


    Public Sub Build(obj As List(Of TemplateNameDTO)) Implements IView(Of List(Of TemplateNameDTO)).Build
        IsBuilding = True
        _Items = obj
        Me.BeginUpdate()
        Me.DataSource = _Items
        Me.SelectedIndex = -1
        Me.EndUpdate()
        IsBuilding = False
    End Sub

    Private Sub SelectedItemChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Me.SelectedIndexChanged
        If Not IsBuilding Then
            RaiseEvent Changed(Me, e)
        End If
    End Sub

    Private Sub TemplatesComboBox_DropDown(sender As Object, e As EventArgs) Handles Me.DropDown
        If GetTemplatesFunc IsNot Nothing Then
            Dim templates = GetTemplatesFunc.Invoke()
            Me.Build(templates)
        End If
    End Sub
End Class
