Imports System.Data.Odbc
Public Class Form3

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.Top = Form1.Top
        Me.Left = Form1.Left
        Me.Width = Form1.Width
        Me.Height = Form1.Height

        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.MultiSelect = False
        Panel1.Visible = False
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        TextBox1.Enabled = False
        Label21.Visible = False
    End Sub



    ''' <summary>
    ''' 显示所有探测器记录，并标记系统正在使用的记录行
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Show_All_Tcq()


        Try
            Dim odbc_con As Odbc.OdbcConnection
            Dim odbc_ada As New Odbc.OdbcDataAdapter()
            Dim com_str As String
            Dim mydataset As New DataSet()

            com_str = "select * from tcq_info order by id "
            Dim odbc_cmd As New Odbc.OdbcCommand(com_str)

            odbc_con = New Odbc.OdbcConnection(db_con_str)
            odbc_ada.SelectCommand = odbc_cmd
            odbc_ada.SelectCommand.Connection = odbc_con
            odbc_ada.Fill(mydataset, "tcq_info")



            Dim row_count As UShort
            Dim i_temp As UShort
            Dim n_temp As Byte

            Dim tcq_table As DataTable
            tcq_table = mydataset.Tables("tcq_info")


            row_count = mydataset.Tables("tcq_info").Rows.Count    '取到记录的行数。
            DataGridView1.Rows.Clear()

            If row_count <= 0 Then
                MsgBox("查询到0条记录")
                odbc_con.Close()
                Exit Sub
            End If

            DataGridView1.Rows.Add(row_count)



            For i_temp = 0 To row_count - 1

                For n_temp = 0 To 8
                    DataGridView1.Rows(i_temp).Cells(n_temp).Value = tcq_table.Rows(i_temp).Item(n_temp)
                Next

                If i_temp < Sys_node_count Then
                    DataGridView1.Rows(i_temp).Cells(0).Style.BackColor = Color.DarkGreen
                End If

            Next

            odbc_con.Close()



        Catch ex As Exception
            MsgBox("查询数据失败" & ex.ToString)
        End Try
    



    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Show_All_Tcq()
    End Sub

 

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Panel1.Visible = False
    End Sub


    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Dim row_num As UShort
        row_num = DataGridView1.SelectedRows(0).Index
        If row_num = 0 Then
            Exit Sub
        End If


        DataGridView1.Rows(row_num - 1).Selected = True
        With DataGridView1.SelectedRows(0)
            TextBox1.Text = .Cells(0).Value

            TextBox2.Text = .Cells(1).Value
            ComboBox1.SelectedIndex = .Cells(2).Value - 1
            ComboBox2.Text = .Cells(3).Value

            TextBox4.Text = .Cells(5).Value
            TextBox5.Text = .Cells(6).Value
            TextBox6.Text = .Cells(7).Value

            If .Cells(8).Value > 0 Then
                RadioButton1.Checked = True
            Else
                RadioButton2.Checked = True
            End If

        End With

    End Sub


    

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Show_All_Tcq()
    End Sub



    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        xg_xz()
    End Sub

    Public Sub xg_xz()

        If DataGridView1.SelectedRows.Count = 0 Then
            MsgBox("没有选中的记录！！！")
            Exit Sub
        End If


        Button14.Visible = False
        Panel1.Visible = True
        Panel1.Left = Panel2.Left
        Panel1.Top = Panel2.Top
        Panel1.BringToFront()
        Button11.Visible = True

        Label21.Visible = False

        With DataGridView1.SelectedRows(0)
            TextBox1.Text = .Cells(0).Value
            TextBox2.Text = .Cells(1).Value
            ComboBox1.SelectedIndex = .Cells(2).Value - 1
            ComboBox2.Text = .Cells(3).Value

            TextBox4.Text = .Cells(5).Value
            TextBox5.Text = .Cells(6).Value
            TextBox6.Text = .Cells(7).Value

            If .Cells(8).Value > 0 Then
                RadioButton1.Checked = True
            Else
                RadioButton2.Checked = True
            End If

        End With
    End Sub



    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Dim row_num As UShort
        row_num = DataGridView1.SelectedRows(0).Index
        If row_num = DataGridView1.Rows.Count - 1 Then
            Exit Sub
        End If


        DataGridView1.Rows(row_num + 1).Selected = True
        With DataGridView1.SelectedRows(0)
            TextBox1.Text = .Cells(0).Value

            TextBox2.Text = .Cells(1).Value
            ComboBox1.SelectedIndex = .Cells(2).Value - 1
            ComboBox2.Text = .Cells(3).Value

            TextBox4.Text = .Cells(5).Value
            TextBox5.Text = .Cells(6).Value
            TextBox6.Text = .Cells(7).Value

            If .Cells(8).Value > 0 Then
                RadioButton1.Checked = True
            Else
                RadioButton2.Checked = True
            End If

        End With
    End Sub


    '修改信息按钮
    '首先判断信息有没有改变
    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click


        If DataGridView1.SelectedRows.Count = 0 Then
            MsgBox("没有选中的记录！！！")
            Exit Sub
        End If

        Dim i_t As UShort
        i_t = DataGridView1.SelectedRows(0).Cells(0).Value

        If i_t <> Val(TextBox1.Text) Then
            MsgBox("请重新选择要修改的行！(鼠标点击对应行即可)")
            Exit Sub
        End If


        If Check_data() = False Then
            Exit Sub
        End If



        Dim info_change As Boolean
        info_change = False

        Dim i_text As String
        Dim xg_sql As String
        xg_sql = ""

        i_text = Trim(TextBox2.Text)


        '箱号
        If i_text <> DataGridView1.SelectedRows(0).Cells(1).Value Then
            info_change = True
            xg_sql = "box_name='" & i_text & "'"
        End If

        Dim i As Byte
        i = ComboBox1.SelectedIndex
        i = i + 1

        '表类型
        If i <> DataGridView1.SelectedRows(0).Cells(2).Value Then
            If info_change Then
                xg_sql = xg_sql & " , "
            End If
            xg_sql = xg_sql & "type=" & i.ToString
            info_change = True
        End If

        '报警电流值-----此处需要验证500.0 是否等于500
        Dim i_single As Single
        Dim k_single As Single
        k_single = DataGridView1.SelectedRows(0).Cells(5).Value

        i_single = Val(TextBox4.Text)

        If i_single <> k_single Then
            If info_change Then
                xg_sql = xg_sql & " , "
            End If
            xg_sql = xg_sql & "il_bj=" & i_single
            info_change = True
        End If

        '温度1报警值-----此处需要验证500.0 是否等于500
        i_single = Val(TextBox5.Text)
        k_single = DataGridView1.SelectedRows(0).Cells(6).Value
        If i_single <> k_single Then
            If info_change Then
                xg_sql = xg_sql & " , "
            End If
            xg_sql = xg_sql & "t1_bj=" & i_single
            info_change = True
        End If

        '温度1报警值-----此处需要验证500.0 是否等于500
        i_single = Val(TextBox6.Text)
        k_single = DataGridView1.SelectedRows(0).Cells(7).Value
        If i_single <> k_single Then
            If info_change Then
                xg_sql = xg_sql & " , "
            End If
            xg_sql = xg_sql & "t2_bj=" & i_single
            info_change = True
        End If

        '归属支路
        Dim i_byte As Byte
        i_byte = Val(ComboBox2.Text)

        If i_byte <> DataGridView1.SelectedRows(0).Cells(3).Value Then
            If info_change Then
                xg_sql = xg_sql & " , "
            End If
            xg_sql = xg_sql & "net=" & i_byte
            info_change = True
        End If


        '归属支路中位置

        i_byte = 1
        If i_byte <> DataGridView1.SelectedRows(0).Cells(4).Value Then
            If info_change Then
                xg_sql = xg_sql & " , "
            End If
            xg_sql = xg_sql & "net_id=" & i_byte
            info_change = True
        End If


        '是否启用
        If RadioButton1.Checked = True Then
            i_byte = 1
        Else
            i_byte = 0
        End If

        If i_byte <> DataGridView1.SelectedRows(0).Cells(8).Value Then
            If info_change Then
                xg_sql = xg_sql & " , "
            End If
            xg_sql = xg_sql & "enable=" & i_byte
            info_change = True
        End If

        If info_change Then
            Dim sql_str As String
            Dim sql_mes As String
            sql_mes = "修改失败"
            sql_str = " update tcq_info set " & xg_sql & " where id=" & Val(TextBox1.Text)


            If Sql_Exe1(sql_str, sql_mes) Then
                Label21.Text = "修改完成"
                Label21.Visible = True
                Label21.ForeColor = Color.Blue

                '然后同步更新记录表
                Copy_to_dv()
            Else
                Label21.Visible = True
                Label21.ForeColor = Color.Red
                Label21.Text = "修改失败"
                MsgBox(sql_mes)
            End If
        Else
            MsgBox("数据未改变！")
        End If


    End Sub

    '把面板里的数据更新到datagridview 
    Private Sub Copy_to_dv()
        With DataGridView1.SelectedRows(0)
            .Cells(1).Value = TextBox2.Text

            .Cells(2).Value = ComboBox1.SelectedIndex + 1

            .Cells(3).Value = ComboBox2.Text

            .Cells(4).Value = 1

            .Cells(5).Value = TextBox4.Text
            .Cells(6).Value = TextBox5.Text
            .Cells(7).Value = TextBox6.Text
            If RadioButton1.Checked Then
                .Cells(8).Value = 1
            Else
                .Cells(8).Value = 0
            End If
        End With
    End Sub


    '检查用户输入数据的正确性
    '
    Private Function Check_data() As Boolean

        TextBox2.Text = Trim(TextBox2.Text)
        If TextBox2.Text.Length < 3 Then
            MsgBox("箱号太短，至少3个字符！")
            Return False
        End If

        If TextBox2.Text.Length > 15 Then
            MsgBox("箱号太长，自多15个字符！")
            Return False
        End If

        TextBox4.Text = Trim(TextBox4.Text)
        If IsNumeric(TextBox4.Text) <> True Then
            MsgBox("报警电流值：请输入正确的数字！")
            TextBox4.Focus()
            TextBox4.SelectAll()
            Return False
        End If
        Dim i_data As Single
        i_data = Val(TextBox4.Text)
        '电流的最小值，不能小于10  
        If i_data < 10 Then
            MsgBox("报警电流值：不能小于10！")
            TextBox4.Focus()
            TextBox4.SelectAll()
            Return False
        End If

        '电流的最大值，不能大于999 
        If i_data > 999 Then
            MsgBox("报警电流值：不能大于999！")
            TextBox4.Focus()
            TextBox4.SelectAll()
            Return False
        End If


        TextBox5.Text = Trim(TextBox5.Text)
        If IsNumeric(TextBox5.Text) <> True Then
            MsgBox("温度1报警值：请输入正确的数字！")
            TextBox5.Focus()
            TextBox5.SelectAll()
            Return False
        End If
        i_data = Val(TextBox5.Text)
        If i_data < 10 Then
            MsgBox("温度1报警值：不能小于10！")
            TextBox5.Focus()
            TextBox5.SelectAll()
            Return False
        End If

        If i_data > 150 Then
            MsgBox("温度1报警值：不能大于150！")
            TextBox5.Focus()
            TextBox5.SelectAll()
            Return False
        End If


        TextBox6.Text = Trim(TextBox6.Text)
        If IsNumeric(TextBox6.Text) <> True Then
            MsgBox("温度2报警值：请输入正确的数字！")
            TextBox6.Focus()
            TextBox6.SelectAll()
            Return False
        End If
        i_data = Val(TextBox6.Text)
        If i_data < 10 Then
            MsgBox("温度2报警值：不能小于10！")
            TextBox5.Focus()
            TextBox5.SelectAll()
            Return False
        End If

        If i_data > 150 Then
            MsgBox("温度2报警值：不能大于150！")
            TextBox6.Focus()
            TextBox6.SelectAll()
            Return False
        End If

        If IsNumeric(ComboBox2.Text) <> True Then
            MsgBox("归属支路：请输入正确的数字！")
            ComboBox2.Focus()
            ComboBox2.SelectAll()
            Return False
        End If

      
        Return True
    End Function

    Private Sub DataGridView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.Click
        If Panel1.Visible = True And Button11.Visible = True Then
            xg_xz()
        End If
    End Sub



   

    '添加记录面板-按钮
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

        Panel1.Visible = True
        Panel1.Left = Panel2.Left
        Panel1.Top = Panel2.Top
        Panel1.BringToFront()
        TextBox1.Enabled = True
        TextBox1.Text = ""

        Button12.Visible = False
        Button13.Visible = False

        Button11.Visible = False
        Button14.Top = Button11.Top
        Button14.Left = Button11.Left
        Button14.Visible = True
        Label21.Visible = False
        TextBox2.Text = ""
        ComboBox1.SelectedIndex = 0
        TextBox4.Text = 500.0
        TextBox5.Text = 100.0
        TextBox6.Text = 100.0
        ComboBox2.Text = 1

        RadioButton2.Checked = False
        RadioButton1.Checked = True

    End Sub


    '确定添加记录按钮
    '首先判断数据的有效性
    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        TextBox1.Text = Trim(TextBox1.Text)
        If IsNumeric(TextBox1.Text) <> True Then
            MsgBox("地址输入不正确!!!")
            Exit Sub
        End If
        '判断id 是否重复。。。。
        Dim i_id As UShort
        Dim i_temp As UShort
        i_id = Val(TextBox1.Text)

        If i_id <= 0 Or i_id >= 550 Then
            MsgBox("地址数值超限!!!")
            Exit Sub
        End If

        If DataGridView1.Rows.Count >= 1 Then
            For i_temp = 0 To DataGridView1.Rows.Count - 1
                If i_id = DataGridView1.Rows(i_temp).Cells(0).Value Then
                    MsgBox("地址:" & i_id & "和现有数据地址重复!!!")
                    Exit Sub
                End If
            Next
        End If


        '其它数据有效性校验
        If Check_data() = False Then
            Exit Sub
        End If


        '生产sql ....

        Try
            Dim t_box_name As String
            Dim t_type As Byte
            Dim t_net As Byte
            Dim t_net_id As Byte
            Dim t_il_bj As Single
            Dim t_t1_bj As Single
            Dim t_t2_bj As Single
            Dim t_en As Byte
            Dim t_mes As String
            t_mes = ""
            t_box_name = Trim(TextBox2.Text)
            t_type = ComboBox1.SelectedIndex + 1
            t_net = Val(ComboBox2.Text)
            t_net_id = 1
            t_il_bj = Val(TextBox4.Text)
            t_t1_bj = Val(TextBox5.Text)
            t_t2_bj = Val(TextBox6.Text)

            If RadioButton1.Checked Then
                t_en = 1
            Else
                t_en = 0
            End If

            Dim sql_str As String
            sql_str = " insert into  tcq_info values(" & i_id & ", '" & t_box_name & "'," _
            & t_type & "," & t_net & "," & t_net_id & "," & t_il_bj & "," & t_t1_bj & "," & t_t2_bj & "," & t_en & ")"

            If Sql_Exe1(sql_str, t_mes) Then
                Label21.ForeColor = Color.Blue
                Label21.Visible = True
                Label21.Text = "添加成功"

                DataGridView1.Rows.Add()
                DataGridView1.Rows(DataGridView1.Rows.Count - 1).Selected = True
                Copy_to_dv()
                DataGridView1.SelectedRows(0).Cells(0).Value = TextBox1.Text
            Else
                Label21.ForeColor = Color.Red
                Label21.Visible = True
                Label21.Text = "添加失败"
                MsgBox(t_mes)
            End If


        Catch ex As Exception
            MsgBox(ex)
        End Try
    End Sub

    '删除一条记录。。。
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MsgBox("没有选中的记录！！！")
            Exit Sub
        End If

        Dim tar_num As UShort

        tar_num = DataGridView1.SelectedRows(0).Cells(0).Value

        If MsgBox("确定删除地址为：" & tar_num & "的探测器记录？", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Exit Sub
        End If

        Dim del_sql As String
        Dim mes As String
        mes = ""

        del_sql = "delete from tcq_info where id=" & tar_num

        If Sql_Exe1(del_sql, mes) Then
            MsgBox("删除成功！")
            DataGridView1.Rows.RemoveAt(DataGridView1.SelectedRows(0).Index)
        Else
            MsgBox("删除失败" & vbCrLf & mes)
        End If

    End Sub

    '删除所有的的数据表
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        If MsgBox("确定删除所有的探测器记录？", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Exit Sub
        End If

        Dim del_sql As String
        
        del_sql = " truncate tcq_info"

        Sql_Exe(del_sql)

        MsgBox("删除成功！")


    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Add_tcq_into_table()
        Show_All_Tcq()
    End Sub


    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.Close()
    End Sub
End Class