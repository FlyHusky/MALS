Public Class Form_Fault

    
    Private Sub Form_Fault_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.Top = Form1.Top
        Me.Left = Form1.Left
        Me.Width = Form1.Width
        Me.Height = Form1.Height
    End Sub
End Class