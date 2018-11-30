Public Class Form_Sys_Set

    Private timer1_ct As Byte

    Private Sub Form_Sys_Set_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(0, 0)
        Me.Width = Scr_W
        Me.Height = Scr_H
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Timer1.Interval = 4000
        Timer1.Enabled = True

        Label1.Top = Me.Height / 4
        Label1.Left = Me.Width / 2 - Label1.Width / 2
        timer1_ct = 0
        Me.BackColor = Color.Black

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        timer1_ct = timer1_ct + 1

        If timer1_ct = 1 Then
            Label1.Top = Me.Height / 3
            Me.BackColor = Color.RoyalBlue
        ElseIf timer1_ct = 2 Then
            Label1.Top = Me.Height / 2
            Me.BackColor = Color.Red
        Else
            Me.Close()
        End If

    End Sub


    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Timer1.Enabled Then
            Timer1.Enabled = False
        Else
            Timer1.Enabled = True
        End If
    End Sub
End Class