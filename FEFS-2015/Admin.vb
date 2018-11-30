Imports System.IO
Public Class Admin
    '权限分配说明：
    '系统操作员：1： 翻页时间 + 修改自己密码
    '系统管理员：2： 承上+系统用户管理
    '超级管理员：3： all



    Private Sub Admin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: 这行代码将数据加载到表“DataSet3.DataTable”中。您可以根据需要移动或移除它。

        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.Top = Main.Panel4.Top
        Me.Left = Main.Panel4.Left
        Me.Width = Main.Panel4.Width
        Me.Height = Main.Panel4.Height

        
        'Panel5.Size = Panel4.Size
        'Panel5.Left = Panel4.Left

        'Panel6.Size = Panel4.Size
        'Panel6.Left = Panel4.Left

        'Panel7.Size = Panel4.Size
        'Panel7.Left = Panel4.Left


        If Me.Width > TabC.Width * 2 Then

            TabC.Width = CInt(TabC.Width * 1.5)

        End If

        ' TabC.Top = (Me.Height - TabC.Height) \ 2
        TabC.Left = (Me.Width - TabC.Width) \ 3

        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox12.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox13.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox14.DropDownStyle = ComboBoxStyle.DropDownList

        Dim ii As Byte
        ComboBox1.Items.Clear()
        For ii = 1 To 30
            ComboBox1.Items.Add("com" & ii.ToString)
        Next


        ComboBox12.Items.Clear()
        ComboBox12.Items.Add("全屏显示")
        ComboBox12.Items.Add("窗体显示")


        TextBox1.Text = Sys_name
        TextBox2.Text = Sys_node_count.ToString
        ComboBox1.SelectedIndex = Sys_tcq_com_id - 1
        ComboBox2.SelectedIndex = Sys_tcq_com_btl - 1 '存的是波特率了
        TextBox8.Text = Sys_Comm_Fail_Ct.ToString
        ComboBox12.SelectedIndex = Sys_Form_Style

        If Sys_Need_Pass > 0 Then
            ComboBox13.SelectedIndex = 1
        Else
            ComboBox13.SelectedIndex = 0
        End If

        If Sys_Company_Id = 1 Then
            ComboBox14.SelectedIndex = 1
        ElseIf Sys_Company_Id = 2 Then
            ComboBox14.SelectedIndex = 2
        Else
            ComboBox14.SelectedIndex = 0
        End If

        ButXG.Text = "修改"
        Button6.Text = "保存"
        Button6.Enabled = False

        Panel4.Enabled = False
        Panel5.Enabled = False
        Panel6.Enabled = False
        Panel7.Enabled = False
        Panel8.Enabled = False
        Panel9.Enabled = False
        Panel10.Enabled = False


        If Sys_user_level = User_Level_Enum.Enger Then
            Panel10.Visible = True
        End If





    End Sub



    Private Sub Button6_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs)

        TextBox1.Text = Trim(TextBox1.Text)

        If TextBox1.Text.Length > 20 Then
            MsgBox("系统名称太长！！！")
            Exit Sub
        End If

        Dim t_count As UShort
        Dim comm_ct As UShort


        t_count = Val(TextBox2.Text)
        comm_ct = Val(TextBox8.Text)

        If comm_ct > 100 Or comm_ct < 1 Then
            MsgBox("通讯失联次数应在1-100之间！！！")
            Exit Sub
        End If

        Dim t_i As Byte
        Dim p_i As Byte

        t_i = Val(ComboBox1.SelectedIndex)  '通讯口  1,2,3,4,5,6，
        p_i = Val(ComboBox2.SelectedIndex)  '波特率   
        t_i = t_i + 1
        p_i = p_i + 1



        If TextBox1.Text <> Sys_name Or t_count <> Sys_node_count Or t_i <> Sys_tcq_com_id Or _
           p_i <> Sys_tcq_com_btl Or Sys_comm_fail_ct <> comm_ct Then
            Try
                Dim data_file_path As String
                '系统数据备份文件sys_main_info，存放位置在 安装路径+ \backup_data 文件夹内。
                data_file_path = My.Application.Info.DirectoryPath & "\backup_data\sys_main_info"
                Dim df_sw As StreamWriter = File.CreateText(data_file_path)
                df_sw.WriteLine("-----------系统名称#" & TextBox1.Text)
                df_sw.WriteLine("---------------报警灯数量#" & t_count)
                df_sw.WriteLine("-----------主程序通讯串口#" & t_i)
                df_sw.WriteLine("-------------------波特率#" & p_i)
                df_sw.WriteLine("---报警器连续失联超限次数#5")
                df_sw.WriteLine("--------------预留控制位1#0")
                df_sw.WriteLine("--------------预留控制位2#0")
                df_sw.Flush()
                df_sw.Close()

                Sys_name = TextBox1.Text
                Sys_node_count = t_count
                Sys_tcq_com_id = t_i
                Sys_tcq_com_btl = p_i
                Sys_comm_fail_ct = comm_ct

                MsgBox("参数已经保存成功，请重启软件！")
            Catch ex As Exception
                MsgBox("参数保存失败" & ex.Message())
            End Try

        End If

    End Sub



    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Refresh_user()
    End Sub


    '刷新用户表，根据用户权限来。
    '系统管理员只能管理1级和2级的用户
    '超级管理员(富自电气)可以管理所有用户
    Private Sub Refresh_user()
        Try
            Dim odbc_con As Odbc.OdbcConnection
            Dim com_str As String

            If Sys_user_level = 4 Then
                com_str = "select * from user_info"

            Else   '管理员只允许修改1级(操作员)
                com_str = "select * from user_info where user_level<=1"

            End If

            Dim odbc_cmd As New Odbc.OdbcCommand(com_str)
            Dim odbc_ada As New Odbc.OdbcDataAdapter()
            odbc_con = New Odbc.OdbcConnection(db_con_str)
            odbc_ada.SelectCommand = odbc_cmd
            odbc_ada.SelectCommand.Connection = odbc_con
            Dim dataset As New DataSet()
            Dim row_count As UShort
            odbc_ada.Fill(dataset, "user_info")
            DataGridView1.Rows.Clear()
            With dataset.Tables("user_info")

                For row_count = 0 To .Rows.Count - 1
                    DataGridView1.Rows.Add()
                    DataGridView1.Rows(row_count).Cells(0).Value = .Rows(row_count).Item("user_name")
                    DataGridView1.Rows(row_count).Cells(1).Value = .Rows(row_count).Item("user_password")
                    ''普通操作员 ，高级操作员，系统管理员
                    'Dim t_level As Byte
                    't_level = .Rows(row_count).Item("user_level")
                    'If t_level = 2 Then
                    '    DataGridView1.Rows(row_count).Cells(2).Value = "系统管理员"
                    'ElseIf t_level = 1 Then
                    '    DataGridView1.Rows(row_count).Cells(2).Value = "系统操作员"
                    'Else
                    '    DataGridView1.Rows(row_count).Cells(2).Value = "操作员"
                    'End If

                    DataGridView1.Rows(row_count).Cells(2).Value = .Rows(row_count).Item("user_level")
                Next
            End With
            odbc_con.Close()
        Catch ex As Exception
            MsgBox("数据刷新失败")
        End Try
    End Sub



    '添加新用户按钮
    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Panel1.Visible = True
        Panel1.BringToFront()
        Button11.Text = "添加"
        TextBox3.Text = ""
        TextBox4.Text = ""

        '选择判断是管理员添加，还是admin


        GroupBox5.Enabled = False


        If Sys_user_level < 4 Then
            ComboBox3.SelectedIndex = 0
            ComboBox3.Enabled = False
        Else
            ComboBox3.SelectedIndex = 0
            ComboBox3.Enabled = True
        End If




    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Panel1.Visible = False
        GroupBox5.Enabled = True
        TextBox3.Text = ""
        TextBox4.Text = ""
        ComboBox3.SelectedIndex = 0
        DataGridView1.Enabled = True
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click

        '首先判断用户名是否正确
        If TextBox3.Text.Length < 2 Or TextBox3.Text.Length > 10 Then
            MsgBox("用户名长度,至少三位，最多10位!!!")
            Exit Sub
        End If

        If TextBox4.Text.Length < 3 Or TextBox4.Text.Length > 6 Then
            MsgBox("密码长度,至少三位，最多10位!!!")
            Exit Sub
        End If

        '判断用户名是否重复
        If DataGridView1.RowCount > 1 Then
            Dim ii As Byte
            For ii = 0 To DataGridView1.RowCount - 1

                If DataGridView1.Rows(ii).Selected <> True Then
                    If TextBox3.Text = DataGridView1.Rows(ii).Cells(0).Value Then
                        MsgBox("用户名重复!!!")
                        Exit Sub
                    End If
                End If
            Next
        End If

        If Button11.Text = "修改" Then
            '修改用户信息

            '判断用户信息是否改变。
            Dim yhxg As Boolean

            yhxg = False

            If TextBox3.Text <> DataGridView1.SelectedRows(0).Cells(0).Value Then
                yhxg = True
            End If

            If TextBox4.Text <> DataGridView1.SelectedRows(0).Cells(1).Value Then
                yhxg = True
            End If

            If ComboBox3.Text <> DataGridView1.SelectedRows(0).Cells(2).Value Then
                yhxg = True
            End If

            If yhxg Then

                Dim sql_str As String
                Dim sql_level As String


                If ComboBox3.SelectedIndex = 0 Then
                    sql_level = "操作员"
                Else
                    sql_level = "管理员"
                End If




                sql_str = "update   user_info set user_password='" & TextBox4.Text & "', user_name='" & TextBox3.Text & "', user_level='" & sql_level & "' where user_name='" & DataGridView1.SelectedRows(0).Cells(0).Value & "'"

                If Sql_Exe(sql_str) Then
                    MsgBox("修改成功")
                Else
                    MsgBox("修改失败")
                End If
                Refresh_user()

            End If



        ElseIf Button11.Text = "添加" Then
            '添加一个新的的用户


            Dim sql_str As String
            Dim sql_level As Byte
            sql_level = ComboBox3.SelectedIndex + 1

            sql_str = "insert into user_info values('" & TextBox3.Text & "', '" & TextBox4.Text & "', " & sql_level & ")"

            If Sql_Exe(sql_str) Then
                MsgBox("添加成功")
            Else
                MsgBox("添加失败")
            End If
            Refresh_user()
        End If

        Panel1.Visible = False
        GroupBox5.Enabled = True

        TextBox3.Text = ""
        TextBox4.Text = ""

        DataGridView1.Enabled = True
    End Sub


    '管理员修改用户按钮
    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click


        DataGridView1.Enabled = False

        Try
            If DataGridView1.SelectedRows.Count > 0 Then

                TextBox3.Text = DataGridView1.SelectedRows(0).Cells(0).Value.ToString

                TextBox4.Text = DataGridView1.SelectedRows(0).Cells(1).Value.ToString

                ComboBox3.Text = DataGridView1.SelectedRows(0).Cells(2).Value.ToString

                Panel1.Visible = True
                Button11.Text = "修改"
                GroupBox5.Enabled = False

            End If
        Catch ex As Exception
            MsgBox("操作出错!!!")
        End Try



    End Sub

    '管理员删除用户按钮
    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click

        If MsgBox("确定删除此用户？", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Exit Sub
        End If


        Dim usname As String
        usname = ""
        Try
            If DataGridView1.SelectedRows.Count > 0 Then
                usname = DataGridView1.SelectedRows(0).Cells(0).Value.ToString

                If usname = Sys_user_name Then
                    MsgBox("不能删除此账号！")
                    Exit Sub
                End If

                Dim sql_str As String


                sql_str = "delete from user_info where user_name='" & usname & "'"

                If Sql_Exe(sql_str) Then
                    MsgBox("删除完成")
                Else
                    MsgBox("操作失败")
                End If
                Refresh_user()

            End If
        Catch ex As Exception

        End Try
    End Sub

    '系统管理和超级进入 用户管理
    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click

        '如果是权限为1的操作员用户，则需要再次高级登录。。
        If Sys_user_level < 2 Then

            Login_event = 2
            Login_Level = 2
            ' LoginForm1.ComboBox1.SelectedIndex = 0
            ' LoginForm1.Show(Me)
            'LoginForm1.Label2.Visible = True

            Login_Need_Level = "管理员"
            Login_Mes = "请以更高一级得'管理员'权限身份登录！！！"
            LoginForm1.Show(Me)
        Else
            Button8_Click1()
        End If


    End Sub

    '承接上
    Public Sub Button8_Click1()
        GroupBox1.Top = GroupBox7.Top
        GroupBox1.Width = GroupBox7.Width
        GroupBox1.Height = GroupBox7.Height
        GroupBox1.Left = GroupBox7.Left

        GroupBox1.Visible = True
        GroupBox1.BringToFront()
        '刷新用户记录表
        Refresh_user()

        If Sys_user_level < 4 Then
            ComboBox3.SelectedIndex = 0
            ComboBox3.Enabled = False
        Else
            ComboBox3.SelectedIndex = 0
            ComboBox3.Enabled = True
        End If


    End Sub



    '根据user_lvevel,设置用户管理面板
    Private Sub button8_Click_my()

        Button8.Enabled = False
        Refresh_user()

    End Sub

    '修改登录密码-菜单按钮
    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        If Sys_user_name = "富自电气" Then
            GroupBox6.Enabled = False
        End If

        GroupBox6.Top = GroupBox7.Top
        GroupBox6.Width = GroupBox7.Width
        GroupBox6.Height = GroupBox7.Height
        GroupBox6.Left = GroupBox7.Left

        GroupBox6.Visible = True
        GroupBox6.BringToFront()


    End Sub


    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If Sys_user_level < 4 Then
            If ComboBox3.SelectedIndex = 2 Then
                MsgBox("无法给予管理员权限！")
                ComboBox3.SelectedIndex = 1
            End If
        End If
    End Sub

    '系统信息修改
    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click

        '填充系统关键信息
        '  Close_Chaxun()

    End Sub


    Private Sub Groupbox2Show()
        TextBox1.Text = Sys_name
        TextBox2.Text = Sys_node_count.ToString
        ComboBox1.SelectedIndex = Sys_tcq_com_id - 1

        '存的是波特率了
        ComboBox2.SelectedIndex = Sys_tcq_com_btl - 1

        TextBox8.Text = Str(Sys_comm_fail_ct)


        ButXG.Text = "修改"
        Button6.Text = "保存"
        Button6.Enabled = False

    End Sub


    '点击进入报警屏蔽设置。
    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click

        GroupBox11.Top = GroupBox7.Top
        GroupBox11.Width = GroupBox7.Width
        GroupBox11.Height = GroupBox7.Height
        GroupBox11.Left = GroupBox7.Left

        GroupBox11.Visible = True
        GroupBox11.BringToFront()

        If ILT12_fault_on Then
            ComboBox7.SelectedIndex = 1   'index=1=不屏蔽=true   0=不屏蔽=false
        Else
            ComboBox7.SelectedIndex = 0
        End If

        If IL_alarm_on Then
            ComboBox8.SelectedIndex = 1
        Else
            ComboBox8.SelectedIndex = 0
        End If


        If T1_alarm_on Then
            ComboBox9.SelectedIndex = 1
        Else
            ComboBox9.SelectedIndex = 0
        End If

        If T2_alarm_on Then
            ComboBox10.SelectedIndex = 1
        Else
            ComboBox10.SelectedIndex = 0
        End If

        If Speaker_work_on Then
            ComboBox11.SelectedIndex = 1
        Else
            ComboBox11.SelectedIndex = 0
        End If

        ComboBox7.Enabled = False
        ComboBox8.Enabled = False
        ComboBox9.Enabled = False
        ComboBox10.Enabled = False
        ComboBox11.Enabled = False

        'Close_Chaxun()
    End Sub


    ''弹出对话框，说明程序通讯模块已停止。信息修改完成后，请复位软件。
    Private Sub Close_Chaxun()
        If Main.Timer1.Enabled = True Then
            Main.Timer1.Enabled = False
            Main.Timer2.Enabled = False
            Form1.Timer1.Enabled = False
            MsgBox("主程序通讯已停止。信息修改操作完成后，请重启软件！")
            Sys_Restart = True

            Main.Label1.Text = " 请重启软件！"
        Else
            Exit Sub
        End If
    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        Close_Chaxun()
        Form3.MdiParent = Main
        Form3.Show()
        Form3.BringToFront()
    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        GroupBox7.Show()
        GroupBox7.BringToFront()
    End Sub

    '首先对其进行数值有效性校验，然后在存入数据库，同时更新系统。
    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        Dim sql_str As String
        Dim fanye As UShort
        fanye = Val(ComboBox4.SelectedItem)

        If fanye < 5 Then
            fanye = 5
        End If
        sql_str = "update sys_main_info set sys_fanye=" & fanye & ";"

        If Sql_Exe(sql_str) Then
            MsgBox("成功")
            Sys_Fanye = fanye
        Else
            MsgBox("失败")
        End If
    End Sub



    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        '用户修改密码-提交
        '先判断3个输入框是否正常，2个新密码是否正常。

        '超级管理员不用输当前密码
        If Sys_user_level < 3 Then
            If TextBox5.Text.Length < 3 Then
                MsgBox("请输入当前密码！")
                Exit Sub
            End If
        End If

        If TextBox6.Text.Length < 3 Then
            MsgBox("请输入新的密码，最少3位，最多10位！")
            Exit Sub
        End If

        If Trim(TextBox6.Text) <> Trim(TextBox7.Text) Then
            MsgBox("两次输入的新密码不一样，请重新输入！")
            TextBox6.Text = ""
            TextBox7.Text = ""
            Exit Sub
        End If


        If Sys_user_level < 3 Then
            '首先判断用户密码是否正确。
            If Fefs_Users(0).user_name = "管理员" Then
                If Fefs_Users(0).user_password <> Trim(TextBox5.Text) Then
                    MsgBox("当前密码错误，请重新输入！")
                    Exit Sub
                End If
            Else
                If Fefs_Users(1).user_password <> Trim(TextBox5.Text) Then
                    MsgBox("当前密码错误，请重新输入！")
                    Exit Sub
                End If
            End If
        End If


        '首先判断用户密码是否正确。
        'Dim rs As Byte
        'rs = User_Login(Sys_user_name, Trim(TextBox5.Text))

        'If rs <> 0 Then
        Dim sql_str As String
        sql_str = "update   user_info set user_password='" & Trim(TextBox6.Text) & "' where user_name='" & Sys_user_name & "'"

        If Sql_Exe(sql_str) Then
            '同时修改系统密码。
            Save_GLY_Pass_Into_File(Trim(TextBox6.Text))
            MsgBox("密码修改成功")

            '0 是 管理员  1是操作员

            If Fefs_Users(0).user_name = "管理员" Then
                Fefs_Users(0).user_password = Trim(TextBox6.Text)
            Else
                Fefs_Users(1).user_password = Trim(TextBox6.Text)
            End If


        Else
            '密码修改失败，这里将新密码保存备份文件中去。。
            If Save_GLY_Pass_Into_File(Trim(TextBox6.Text)) Then
                MsgBox("密码修改完成，以保存到备份文件中！")

                If Fefs_Users(0).user_name = "管理员" Then
                    Fefs_Users(0).user_password = Trim(TextBox6.Text)
                Else
                    Fefs_Users(1).user_password = Trim(TextBox6.Text)
                End If

            Else
                MsgBox("密码修改失败")
            End If

        End If

    End Sub



    '修改操作员秘密，直接修改。。
    Private Sub Button23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button23.Click


        If TextBox10.Text.Length < 3 Then
            MsgBox("请输入新的密码，最少3位，最多10位！")
            Exit Sub
        End If

        If Trim(TextBox10.Text) <> Trim(TextBox9.Text) Then
            TextBox10.Text = ""
            TextBox9.Text = ""
            MsgBox("两次输入的新密码不一样，请重新输入！")
            Exit Sub
        End If


        Dim sql_str As String
        sql_str = "update   user_info set user_password='" & Trim(TextBox10.Text) & "' where user_name='操作员'"

        If Sql_Exe(sql_str) Then
            '同时修改系统密码。
            Save_GLY_Pass_Into_File(Trim(TextBox10.Text))
            MsgBox("密码修改成功")

            '0 是 管理员  1是操作员

            If Fefs_Users(0).user_name = "操作员" Then
                Fefs_Users(0).user_password = Trim(TextBox9.Text)
            Else
                Fefs_Users(1).user_password = Trim(TextBox9.Text)
            End If


        Else
            '密码修改失败，这里将新密码保存备份文件中去。。
            If Save_CZY_Pass_Into_File(Trim(TextBox10.Text)) Then
                MsgBox("密码修改完成，以保存到备份文件中！")
                If Fefs_Users(0).user_name = "操作员" Then
                    Fefs_Users(0).user_password = Trim(TextBox9.Text)
                Else
                    Fefs_Users(1).user_password = Trim(TextBox9.Text)
                End If
            Else
                MsgBox("密码修改失败")
            End If

        End If


    End Sub


    '点击重启软件。
    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        Sys_Close()
        Application.Restart()
    End Sub

    '报警屏蔽  -  修改按钮
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ComboBox7.Enabled = True
        ComboBox8.Enabled = True
        ComboBox9.Enabled = True
        ComboBox10.Enabled = True
        ComboBox11.Enabled = True
    End Sub


    '报警屏蔽  -  录入数据库
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim ilt_falut As Boolean
        Dim ilt_falut_i As UShort

        Dim il_alarm As Boolean
        Dim il_alarm_i As UShort


        Dim t1_alarm As Boolean
        Dim t1_alarm_i As UShort


        Dim t2_alarm As Boolean
        Dim t2_alarm_i As UShort


        Dim laba_on As Boolean
        Dim laba_on_i As UShort

        'index=1=不屏蔽=true   0=不屏蔽=false

        If ComboBox7.SelectedIndex > 0 Then
            ilt_falut = True
            ilt_falut_i = 1
        Else
            ilt_falut = False
            ilt_falut_i = 0
        End If

        If ComboBox8.SelectedIndex > 0 Then
            il_alarm = True
            il_alarm_i = 1
        Else
            il_alarm = False
            il_alarm_i = 0
        End If

        If ComboBox9.SelectedIndex > 0 Then
            t1_alarm = True
            t1_alarm_i = 1
        Else
            t1_alarm = False
            t1_alarm_i = 0
        End If

        If ComboBox10.SelectedIndex > 0 Then
            t2_alarm = True
            t2_alarm_i = 1
        Else
            t2_alarm = False
            t2_alarm_i = 0
        End If

        If ComboBox11.SelectedIndex > 0 Then
            laba_on = True
            laba_on_i = 1
        Else
            laba_on = False
            laba_on_i = 0
        End If


        If (ilt_falut <> ILT12_fault_on) Or (il_alarm <> IL_alarm_on) _
           Or (t1_alarm <> T1_alarm_on) Or (t2_alarm <> T2_alarm_on) _
           Or (laba_on <> Speaker_work_on) Then

            Dim sql_str As String

            sql_str = "update sys_main_info  set " _
                 & "ilt_falut_on =" & ilt_falut_i _
                 & ", il_alarm_on=" & il_alarm_i _
                 & ",t1_alarm_on=" & t1_alarm_i _
                 & ",t2_alarm_on=" & t2_alarm_i _
                 & ", speaker_work_on=" & laba_on_i

            If Sql_Exe(sql_str) Then
                MessageBox.Show("数据库中数据修改完成!!!")
                ILT12_fault_on = ilt_falut
                IL_alarm_on = il_alarm
                T1_alarm_on = t1_alarm
                T2_alarm_on = t2_alarm
                Speaker_work_on = laba_on
            Else
                MessageBox.Show("数据库中数据修改失败!!!")
            End If
        Else

            MessageBox.Show("数据未发生改变，无需更新!!!")
        End If


    End Sub

    'C:/1.txt 文件批量导入
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If File.Exists("C:\1.txt") <> True Then
            MessageBox.Show("在C盘根目录下没有找1.txt文件!!!")
            Exit Sub
        End If

        MessageBox.Show("数据导入时间可能需要几十秒，期间请不要有任何操作!!!,点击确定-按钮，开始导入！")

        Dim sr As StreamReader
        Dim i_id As Integer
        Dim ct As Integer
        Dim str As String
        Dim boxname As String

        sr = New StreamReader("C:\1.txt", System.Text.Encoding.Default)


        str = " "

        ct = 0

        Do While sr.Peek() >= 0

            Dim ss() = (Split(sr.ReadLine, "@"))

            Try
                str = Trim(ss(0))

                If IsNumeric(str) Then

                    i_id = Val(str)

                    boxname = Trim(ss(1))

                    Dim sql_str As String
                    sql_str = "update tcq_info set box_name='" & boxname & "' where id=" & i_id

                    If Sql_Exe(sql_str) Then
                        ct = ct + 1
                    End If

                End If



            Catch ex As Exception

            End Try

        Loop

        sr.Close()

        MessageBox.Show("共计更新" & ct & "条记录！！！")



    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Main.GroupBox8.Top = GroupBox7.Top + Me.Top
        Main.GroupBox8.Width = GroupBox7.Width
        Main.GroupBox8.Height = GroupBox7.Height
        Main.GroupBox8.Left = GroupBox7.Left
        Main.GroupBox8.Visible = True
        Main.GroupBox8.Show()
        Main.GroupBox8.BringToFront()




    End Sub

    '一键清除报警历史表中所有的数据
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim sql_str As String
        sql_str = "truncate his_alarm"

        If Sql_Exe(sql_str) Then
            MessageBox.Show("清空操作完成!!!")
        Else
            MessageBox.Show("清空操作失败!!!")
        End If
    End Sub

    '一键屏蔽所有的通讯故障

    Private Sub Button24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button24.Click
        Dim sql_str As String
        sql_str = "update tcq_info set enable=0"

        If Sql_Exe(sql_str) Then
            MessageBox.Show("操作完成!!!")
        Else
            MessageBox.Show("操作失败 或 已经开启此功能!!!")
        End If
    End Sub


    Private Sub Button25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button25.Click
        Dim sql_str As String
        sql_str = "update tcq_info set enable=1"

        If Sql_Exe(sql_str) Then
            MessageBox.Show("操作完成!!!")
        Else
            MessageBox.Show("操作失败 或 已经开启此功能!!!")
        End If
    End Sub


 


    Private Sub ButXG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButXG.Click
        If ButXG.Text = "修改" Then
            If MsgBox("修改基础信息，将会停止主程序通讯查询工作。是否继续呢？", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Exit Sub
            End If
            ' Close_Chaxun()

            Main.But_Data_View.Text = "请重启软件"
            Main.But_Data_View.ForeColor = Color.Red
            Main.But_Table_View.Text = "功能已停止"
            Main.But_Map_View.Text = "请重启软件"
            Main.But_Table_View.ForeColor = Color.Red
            Main.But_Map_View.ForeColor = Color.Red
            '标记主程序查询暂停
            Main.Main_Chaxun_Loop_Wait = True




            Panel4.Enabled = True
            Panel5.Enabled = True
            Panel6.Enabled = True
            Panel7.Enabled = True
            Panel8.Enabled = True

 
            ButXG.Text = "刷新"
            Button6.Enabled = True
            TextBox8.Enabled = True

        Else

            ' Panel4.Enabled = False
            ' Panel5.Enabled = False
            '’ Panel6.Enabled = False
            'Panel7.Enabled = False
            ' Panel8.Enabled = False

            TextBox1.Text = Sys_name
            TextBox2.Text = Sys_node_count.ToString
            ComboBox1.SelectedIndex = Sys_tcq_com_id - 1
            ComboBox2.SelectedIndex = Sys_tcq_com_btl - 1 '存的是波特率了
            TextBox8.Text = Sys_Comm_Fail_Ct.ToString
            ComboBox12.SelectedIndex = Sys_Form_Style



            ' ButXG.Text = "修改"
            'Button6.Text = "保存"
            ' Button6.Enabled = False
        End If
    End Sub


    ''' <summary>
    ''' 保存按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click

 
        Dim name As String
        Dim node_cout As Integer
        Dim tcq_com_id As Byte
        Dim tcq_com_btl As Byte
        Dim comm_fail_ct As Byte
        Dim form_style As Byte

        name = Trim(TextBox1.Text)
        node_cout = CInt(TextBox2.Text)
        comm_fail_ct = CByte(TextBox8.Text)
        tcq_com_id = CByte(ComboBox1.SelectedIndex + 1) '通讯口  1,2,3,4,5,6，
        tcq_com_btl = CByte(ComboBox2.SelectedIndex + 1)  '波特率   

        form_style = CByte(ComboBox12.SelectedIndex)  '0-全屏 1-窗体

        If name.Length > 20 Then
            MsgBox("系统名称太长！不能超过20个字！")
            Exit Sub
        End If

        If comm_fail_ct > 20 Or comm_fail_ct < 1 Then
            MsgBox("报警器连续失联超限次数！")
            Exit Sub
        End If

        If node_cout > BJQMAXADDR Or comm_fail_ct < 1 Then
            MsgBox("报警器数量超限！")
            Exit Sub
        End If



        If name <> Sys_name Or node_cout <> Sys_node_count Or tcq_com_id <> Sys_tcq_com_id Or _
           tcq_com_btl <> Sys_tcq_com_btl Or Sys_Comm_Fail_Ct <> comm_fail_ct Or form_style <> Sys_Form_Style Then

            Try
                Dim data_file_path As String

                '系统数据备份文件sys_main_info，存放位置在 安装路径+ \backup_data 文件夹内。
                data_file_path = My.Application.Info.DirectoryPath & "\backup_data\sys_main_info"

                Dim df_sw As StreamWriter = File.CreateText(data_file_path)
                df_sw.WriteLine("-----------------系统名称#" & name)
                df_sw.WriteLine("---------------报警器数量#" & node_cout)
                df_sw.WriteLine("-----------主程序通讯串口#" & tcq_com_id)
                df_sw.WriteLine("-------------------波特率#" & tcq_com_btl)
                df_sw.WriteLine("---报警器连续失联超限次数#" & comm_fail_ct)
                df_sw.WriteLine("-------全屏显示or窗口显示#" & form_style)
                df_sw.WriteLine("--------------预留控制位1#0")
                df_sw.WriteLine("--------------预留控制位2#0")
                df_sw.Flush()
                df_sw.Close()

                Sys_name = name
                Sys_node_count = node_cout
                Sys_tcq_com_id = tcq_com_id
                Sys_tcq_com_btl = tcq_com_btl
                Sys_Comm_Fail_Ct = comm_fail_ct
                Sys_Form_Style = form_style

                If MessageBox.Show("数据已经保存到文件中，是否请重启软件？", "请选择", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    Main.SysRestart()
                End If

            Catch ex As Exception

            End Try
        Else
            MessageBox.Show("没有数据改变,无需保存！")
        End If


    End Sub



End Class