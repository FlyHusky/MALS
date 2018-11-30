Public Class Main_Form

    Private Sub Main_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None

        '下面处理DataGridView1和DataGridView2，每个view显示50个数据，一个界面显示100个数据。。。
        '暂时只显示一个界面，不做分页处理。。。。
        '数据库中恰好有100条数据，一页数据显示OK
        Dim temp_b As Byte

        With DataGridView1
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToOrderColumns = False
            .AllowUserToResizeColumns = False
            .AllowUserToResizeRows = False
        End With

        With DataGridView2
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToOrderColumns = False
            .AllowUserToResizeColumns = False
            .AllowUserToResizeRows = False
        End With

       
        For temp_b = 0 To 49
            DataGridView1.Rows.Add()
            DataGridView1.Rows(temp_b).Cells(0).Value = Fesn(temp_b).id
            DataGridView1.Rows(temp_b).Cells(1).Value = Fesn(temp_b).name
            DataGridView1.Rows(temp_b).Cells(2).Value = Fesn(temp_b).IL
            DataGridView1.Rows(temp_b).Cells(3).Value = Fesn(temp_b).T1
            DataGridView1.Rows(temp_b).Cells(4).Value = Fesn(temp_b).T2

            DataGridView2.Rows.Add()
            DataGridView2.Rows(temp_b).Cells(0).Value = Fesn(temp_b + 50).id
            DataGridView2.Rows(temp_b).Cells(1).Value = Fesn(temp_b + 50).name
            DataGridView2.Rows(temp_b).Cells(2).Value = Fesn(temp_b + 50).IL
            DataGridView2.Rows(temp_b).Cells(3).Value = Fesn(temp_b + 50).T1
            DataGridView2.Rows(temp_b).Cells(4).Value = Fesn(temp_b + 50).T2

        Next

        




    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Add_tcq_into_table()
        End
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        DataGridView1.Height = DataGridView1.Height + 30
        Label1.Text = DataGridView1.Height.ToString
    End Sub
End Class