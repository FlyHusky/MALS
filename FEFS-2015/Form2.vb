Public Class Form2

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label1.Text = "Form2"
        Label1.Font = New Font("宋体", 20)
        Me.Top = 50
        Me.Left = 250
        Me.TopMost = True
        MsgBox("form2_load")

    End Sub
End Class