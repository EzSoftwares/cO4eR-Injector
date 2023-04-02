Public NotInheritable Class About

    Private Sub About_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Set the title of the form.
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Text = String.Format("About {0}", ApplicationTitle)
    End Sub
    'Handling the positioning, exit...
    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub Exit_MouseHover(sender As Object, e As EventArgs) Handles _Exit.MouseHover
        _Exit.ForeColor = Color.Red
    End Sub

    Private Sub Exit_MouseLeave(sender As Object, e As EventArgs) Handles _Exit.MouseLeave
        _Exit.ForeColor = Color.White
    End Sub

    Private Sub Exit_Click(sender As Object, e As EventArgs) Handles _Exit.Click
        Me.Close()
    End Sub
    Dim mousePos As Point
    Private Sub Title_MouseDown(sender As Object, e As MouseEventArgs) Handles Title.MouseDown
        mousePos = e.Location
    End Sub

    Private Sub Title_MouseMove(sender As Object, e As MouseEventArgs) Handles Title.MouseMove
        If e.Button = MouseButtons.Left Then
            Dim dx As Integer = e.Location.X - mousePos.X
            Dim dy As Integer = e.Location.Y - mousePos.Y
            Location = New Point(Location.X + dx, Location.Y + dy)
        End If
    End Sub
End Class
