Public Interface IView(Of T)
    Event Changed As EventHandler
    Sub Build(ByVal obj As T)
    Property GetData As [Delegate]
End Interface
