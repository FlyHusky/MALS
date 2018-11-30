Public Class FormSetAddr

    Private Sub FormSetAddr_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.Top = Main.Panel4.Top
        Me.Left = Main.Panel4.Left
        Me.Width = Main.Panel4.Width
        Me.Height = Main.Panel4.Height

        Dim i As Integer

        For i = 1 To 54
            ComAddr.Items.Add(i)
        Next


    End Sub






    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ComAddr_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComAddr.SelectedIndexChanged

    End Sub

    ''' <summary>
    ''' 设置报警器的地址
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

    End Sub
End Class