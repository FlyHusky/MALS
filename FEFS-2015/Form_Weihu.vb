Public Class Form_Weihu

    Dim yy As Integer
    Dim mon_b As Byte
    Dim sql_str As String
    Dim s_date As String
    Dim e_date As String
   
    Private Sub Form_Weihu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.Top = Form1.Top
        Me.Left = Form1.Left
        Me.Width = Form1.Width
        Me.Height = Form1.Height

        Panel1.Top = Me.Height * 0.1
        Panel1.Height = Me.Height * 0.8
        Panel1.Left = Me.Width * 0.02

        Label1.Top = 20
        Label1.Left = Me.Width / 2 - Label1.Width / 2

        '系统报警清楚
        Panel2.Top = Panel1.Top
        Panel2.Left = Panel1.Left + Panel1.Width + 30
        Panel2.Height = Panel1.Height

        '系统信息显示panel
        Panel3.Top = Panel1.Top
        Panel3.Left = Panel1.Left + Panel1.Width + 30
        Panel3.Height = Panel1.Height


        Panel2.Visible = True
        Panel3.Visible = True
        Panel3.BringToFront()


        Dim i As Integer

        i = Val(Now.Year)

        Dim k As Byte

        For k = 1 To 20
            ComboBox1.Items.Add(i)
            i = i - 1
        Next
        ComboBox1.SelectedIndex = 0



        '将今日的系统信息加载到textbox 中去。

        'Dim fileReader As String
        'fileReader = My.Computer.FileSystem.ReadAllText(Sys_Log_Path)

        'TextBox1.Text = fileReader



    End Sub

    '选择年份之后，按月份检索数据库中信息的条目数量。。
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        DataGridView1.Rows.Clear()
        yy = Val(ComboBox1.SelectedItem)

        lhh()
    End Sub

    Private Sub lhh()
        '工12个查询记录
        DataGridView1.Rows.Clear()
        For Me.mon_b = 1 To 12

            sql_str = "select count(*) from his_alarm where his_date between "

            s_date = yy.ToString & "-" & mon_b & "-01"

            sql_str = sql_str & "'" & s_date & "' and "

            e_date = yy.ToString & "-" & mon_b

            If mon_b = 1 Or mon_b = 3 Or mon_b = 3 Or mon_b = 5 Or mon_b = 7 Or mon_b = 8 Or mon_b = 10 Or mon_b = 12 Then
                e_date = e_date & "-31'"
            ElseIf mon_b = 2 Then
                If ((yy Mod 4 = 0) And (yy Mod 100 <> 0)) Or (yy Mod 400 = 0) Then
                    e_date = e_date & "-29'"
                Else
                    e_date = e_date & "-28'"
                End If
            End If

            sql_str = sql_str & "'" & e_date

            Dim num As Integer
            num = Find_Sql_Exe(sql_str)

            DataGridView1.Rows.Add()
            DataGridView1.Rows(mon_b - 1).Cells(1).Value = yy.ToString & "/" & mon_b
            DataGridView1.Rows(mon_b - 1).Cells(2).Value = num
        Next
    End Sub

     
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim i As Byte
        For i = 0 To DataGridView1.Rows.Count - 1
            DataGridView1.Rows(i).Cells(0).Value = True
        Next


    End Sub


    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim i As Byte
        For i = 0 To DataGridView1.Rows.Count - 1
            DataGridView1.Rows(i).Cells(0).Value = False
        Next
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click

        If MsgBox("是否确定删除选定的报警记录？", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Exit Sub
        End If

        '删除所选中的月份数据。。。。。。。
        Dim i As Byte
        For i = 0 To DataGridView1.Rows.Count - 1

            If (DataGridView1.Rows(i).Cells(0).Value = True) And (DataGridView1.Rows(i).Cells(2).Value > 0) Then

                mon_b = i + 1

                sql_str = "delete  from his_alarm where his_date between "

                s_date = yy.ToString & "-" & mon_b & "-01"

                sql_str = sql_str & "'" & s_date & "' and "

                e_date = yy.ToString & "-" & mon_b

                If mon_b = 1 Or mon_b = 3 Or mon_b = 3 Or mon_b = 5 Or mon_b = 7 Or mon_b = 8 Or mon_b = 10 Or mon_b = 12 Then
                    e_date = e_date & "-31'"
                ElseIf mon_b = 2 Then
                    If ((yy Mod 4 = 0) And (yy Mod 100 <> 0)) Or (yy Mod 400 = 0) Then
                        e_date = e_date & "-29'"
                    Else
                        e_date = e_date & "-28'"
                    End If
                End If

                sql_str = sql_str & "'" & e_date

                Dim num As Integer
                num = Find_Sql_Exe(sql_str)


            End If

        Next

        lhh()



    End Sub


    '
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Panel3.BringToFront()
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Panel2.BringToFront()
    End Sub
End Class