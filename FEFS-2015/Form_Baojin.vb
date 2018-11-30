Public Class Form_Baojin

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.Top = Main.Panel4.Top
        Me.Left = Main.Panel4.Left
        Me.Width = Main.Panel4.Width
        Me.Height = Main.Panel4.Height

        GroupBox1.Left = 2
        GroupBox1.Top = 2
        GroupBox1.Width = Me.Width - GroupBox1.Left * 2
        GroupBox1.Height = 50


        DataGridView1.Left = GroupBox1.Left
        DataGridView1.Height = Me.Height - GroupBox1.Height - GroupBox1.Top * 2
        DataGridView1.Width = GroupBox1.Width
        DataGridView1.Top = GroupBox1.Top * 2 + GroupBox1.Height

        With DataGridView1
            .Columns(0).Width = .Width * 0.05
            .Columns(1).Width = .Width * 0.13
            .Columns(2).Width = .Width * 0.13
            .Columns(3).Width = .Width * 0.13
            .Columns(4).Width = .Width * 0.1

            .Columns(5).Width = .Width * 0.13
            .Columns(6).Width = .Width * 0.1
            .Columns(7).Width = .Width * 0.1
            .Columns(8).Width = .Width * 0.1
        End With

        ComboBox1.SelectedIndex = 0
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        Dim f2 As UShort

        ComboBox2.Items.Add("所有")

        For f2 = 0 To sys_node_count - 1
            ComboBox2.Items.Add(Fesn(f2).id_str)
        Next
        ComboBox2.MaxDropDownItems = 15
        ComboBox2.SelectedIndex = 0
    End Sub

    ''' <summary>
    ''' 查询按钮。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Try
            Dim c_box_name As String
            Dim c_alarm_kind As Byte
            Dim find_by_name As Boolean
            Dim find_by_kind As Boolean
            Dim find_first As Boolean


            find_by_name = False
            find_by_kind = False
            find_first = True

            c_box_name = ""
            '1先判断用户有没有输入箱号,如果有输入，则判断输入是否正确。

            If ComboBox2.Text <> "所有" Then
                c_box_name = ComboBox2.Text
                find_by_name = True
            End If

            If ComboBox1.Text <> "所有" Then
                c_alarm_kind = ComboBox1.SelectedIndex   '报警类型0=IL ，1=T1  ,2=T2
                find_by_kind = True
            End If



            'If ComboBox2.Text <> "" Then
            '    If ComboBox2.SelectedIndex = -1 Then
            '        For Each fs As Tcq In Fesn
            '            If fs.name = ComboBox2.Text Then
            '                c_box_name = fs.name
            '                GoTo L_Alarm_Kind
            '            End If
            '        Next
            '        MsgBox("没有找到你输入的探测器箱号！")
            '        Exit Sub
            '    Else
            '        c_box_name = Fesn(ComboBox2.SelectedIndex).name
            '    End If
            'End If




            '3：判断用户输入的时间值，要求，开始天数小于等于结束天数
            Dim start_date As String
            Dim end_date As String


            start_date = DateTimePicker1.Value.GetDateTimeFormats()(0)
            end_date = DateTimePicker2.Value.GetDateTimeFormats()(0)

            If DateTimePicker2.Value < DateTimePicker1.Value And start_date <> end_date Then
                MsgBox("开始日期大于结束日期！")
                Exit Sub
            End If
            Dim odbc_con As Odbc.OdbcConnection
            Dim com_str As String

            com_str = "select * from his_alarm where "

            'If c_box_name <> "" Then
            '    com_str = com_str & "box_name='" & c_box_name & "' and "
            'End If

            If find_by_name Then
                com_str = com_str & "tcq_id='" & c_box_name & "' and "
            End If

            If find_by_kind Then
                com_str = com_str & "alarm_kind=" & c_alarm_kind & " and "
            End If

            com_str = com_str & "  his_date between  '" & start_date.ToString & "' and '" & end_date.ToString & "'"

            Dim odbc_cmd As New Odbc.OdbcCommand(com_str)
            Dim odbc_ada As New Odbc.OdbcDataAdapter()
            odbc_con = New Odbc.OdbcConnection(db_con_str)
            odbc_ada.SelectCommand = odbc_cmd
            odbc_ada.SelectCommand.Connection = odbc_con
            Dim dataset As New DataSet()
            Dim row_count As UShort
            odbc_ada.Fill(dataset, "his_alarm")

            DataGridView1.Rows.Clear()

            If dataset.Tables("his_alarm").Rows.Count = 0 Then
                Label4.Text = "查询到0条记录！"
                odbc_con.Close()
                Exit Sub
            Else
                row_count = dataset.Tables("his_alarm").Rows.Count
                Label4.Text = "查询到" & row_count & "条记录！"
            End If

            DataGridView1.Rows.Clear()
            DataGridView1.Rows.Add(row_count)

            Dim date1 As Date

            Dim i_temp As UShort
            With dataset.Tables("his_alarm")
                For i_temp = 0 To row_count - 1
                    DataGridView1.Rows(i_temp).Cells(0).Value = i_temp + 1

                    date1 = .Rows(i_temp).Item("his_date")
                    DataGridView1.Rows(i_temp).Cells(1).Value = date1.GetDateTimeFormats()(0)
                    DataGridView1.Rows(i_temp).Cells(2).Value = .Rows(i_temp).Item("his_time")
                    If .Rows(i_temp).Item("alarm_kind") = 0 Then
                        DataGridView1.Rows(i_temp).Cells(3).Value = "电流报警"
                    ElseIf .Rows(i_temp).Item("alarm_kind") = 1 Then
                        DataGridView1.Rows(i_temp).Cells(3).Value = "温度1报警"
                    Else
                        DataGridView1.Rows(i_temp).Cells(3).Value = "温度2报警"
                    End If
                    DataGridView1.Rows(i_temp).Cells(4).Value = .Rows(i_temp).Item("tcq_id")

                    DataGridView1.Rows(i_temp).Cells(5).Value = .Rows(i_temp).Item("box_name")
                    DataGridView1.Rows(i_temp).Cells(6).Value = .Rows(i_temp).Item("il")

                    If .Rows(i_temp).Item("t1") = "170" Then
                        DataGridView1.Rows(i_temp).Cells(7).Value = " F "
                    Else
                        DataGridView1.Rows(i_temp).Cells(7).Value = .Rows(i_temp).Item("t1")
                    End If

                    If .Rows(i_temp).Item("t2") = "170" Then
                        DataGridView1.Rows(i_temp).Cells(8).Value = " F "
                    Else
                        DataGridView1.Rows(i_temp).Cells(8).Value = .Rows(i_temp).Item("t2")
                    End If
                Next

            End With

        Catch ex As Exception

        End Try
   
    End Sub
End Class