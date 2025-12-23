<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TemplatePropertiesTreeView1 = New EAVWinFormsClient.TemplatePropertiesTreeView()
        Me.TemplatePropertyTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.TemplatesComboBox1 = New EAVWinFormsClient.TemplatesComboBox()
        Me.SuspendLayout()
        '
        'TemplatePropertiesTreeView1
        '
        Me.TemplatePropertiesTreeView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TemplatePropertiesTreeView1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.TemplatePropertiesTreeView1.GetData = Nothing
        Me.TemplatePropertiesTreeView1.Location = New System.Drawing.Point(13, 65)
        Me.TemplatePropertiesTreeView1.Name = "TemplatePropertiesTreeView1"
        Me.TemplatePropertiesTreeView1.Size = New System.Drawing.Size(205, 302)
        Me.TemplatePropertiesTreeView1.TabIndex = 1
        '
        'TemplatePropertyTableLayoutPanel
        '
        Me.TemplatePropertyTableLayoutPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TemplatePropertyTableLayoutPanel.ColumnCount = 1
        Me.TemplatePropertyTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TemplatePropertyTableLayoutPanel.Location = New System.Drawing.Point(237, 65)
        Me.TemplatePropertyTableLayoutPanel.Name = "TemplatePropertyTableLayoutPanel"
        Me.TemplatePropertyTableLayoutPanel.RowCount = 1
        Me.TemplatePropertyTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TemplatePropertyTableLayoutPanel.Size = New System.Drawing.Size(297, 302)
        Me.TemplatePropertyTableLayoutPanel.TabIndex = 3
        '
        'TemplatesComboBox1
        '
        Me.TemplatesComboBox1.FormattingEnabled = True
        Me.TemplatesComboBox1.GetData = Nothing
        Me.TemplatesComboBox1.Location = New System.Drawing.Point(13, 13)
        Me.TemplatesComboBox1.Name = "TemplatesComboBox1"
        Me.TemplatesComboBox1.Size = New System.Drawing.Size(205, 21)
        Me.TemplatesComboBox1.TabIndex = 4
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(612, 379)
        Me.Controls.Add(Me.TemplatesComboBox1)
        Me.Controls.Add(Me.TemplatePropertyTableLayoutPanel)
        Me.Controls.Add(Me.TemplatePropertiesTreeView1)
        Me.Name = "MainForm"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TemplatePropertiesTreeView1 As TemplatePropertiesTreeView
    Friend WithEvents TemplatePropertyTableLayoutPanel As TableLayoutPanel
    Friend WithEvents TemplatesComboBox1 As TemplatesComboBox
End Class
