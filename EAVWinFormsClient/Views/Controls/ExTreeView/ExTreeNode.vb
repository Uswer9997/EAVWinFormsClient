<DebuggerNonUserCode>
Public Class ExTreeNode
    Inherits TreeNode

    Public Property BindingObject As Object

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(Text As String)
        MyBase.New(Text)
    End Sub

    Public Sub New(bindingObj As Object)
        Text = bindingObj.ToString
        BindingObject = bindingObj
    End Sub

End Class