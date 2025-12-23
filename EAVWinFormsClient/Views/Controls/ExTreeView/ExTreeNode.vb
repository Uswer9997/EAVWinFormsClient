<DebuggerNonUserCode>
Public Class ExTreeNode
    Inherits TreeNode

    Public Property BindingObject As TemplatePropertyDTO

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(Text As String)
        MyBase.New(Text)
    End Sub

    Public Sub New(bindingObj As TemplatePropertyDTO)
        'Text = bindingObj.Name
        Text = $"{bindingObj.Name} [{bindingObj.IdEntity}]"
        BindingObject = bindingObj
    End Sub

End Class