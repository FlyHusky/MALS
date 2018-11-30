Public Class Form_Comm_TS

    Private Sub Form_Comm_TS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.Top = Form1.Top
        Me.Left = Form1.Left
        Me.Width = Form1.Width
        Me.Height = Form1.Height
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

 


End Class