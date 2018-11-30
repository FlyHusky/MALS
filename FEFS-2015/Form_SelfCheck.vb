Imports MALS.SUSI

Public Class Form_SelfCheck

    Private T1_Count As UShort
    Private T3_Count As Byte
    Private T4_Count As Byte
    Private T5_Count As Byte
    Private T6_Count As Byte
    Private T7_Count As Byte
    Private T8_Count As Byte
    Dim tcq_check_ready As Boolean
    Dim tcq_check_loop As UInteger

    Dim timer3_ct As UInteger
    Dim timer3_ct1 As UInteger
    Dim timer3_kind As Byte
    Dim Speaker_Ct As Byte

    ''' <summary>
    ''' 1=主电工作灯   2=备电工作  3=监控报警 4=故障报警  5=
    ''' </summary>
    ''' <remarks></remarks>
    Dim sbsb As Byte
    Public ZJ_Step As Integer

    Public ZJ_Row_Num As UInteger


    Private Sub Form_SelfCheck_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.Top = Main.Panel4.Top
        Me.Left = Main.Panel4.Left
        Me.Width = Main.Panel4.Width
        Me.Height = Main.Panel4.Height


        TabControl1.Width = (Me.Width - 2 - 2 - 3) / 2
        TabControl1.Left = 2
        TabControl1.Height = Me.Height - 2 - 2
        TabControl1.Top = 2

        TabControl2.Width = TabControl1.Width
        TabControl2.Height = TabControl1.Height
        TabControl2.Left = TabControl1.Left + TabControl1.Width + 3
        TabControl2.Top = TabControl1.Top

        DataGridView1.Columns(0).Width = DataGridView1.Width * 0.15
        DataGridView1.Columns(1).Width = DataGridView1.Width * 0.25
        DataGridView1.Columns(2).Width = DataGridView1.Width * 0.25
        DataGridView1.Columns(3).Width = DataGridView1.Width * 0.25

        Dim fsc_i As UShort

        DataGridView1.Rows.Clear()
        DataGridView1.Rows.Add(Sys_node_count)

        For fsc_i = 0 To Sys_node_count - 1
            With DataGridView1
                .Rows(fsc_i).Cells(1).Value = Fesn(fsc_i).id_str
                .Rows(fsc_i).Cells(2).Value = Fesn(fsc_i).name
                .Rows(fsc_i).Cells(3).Value = "等待中"
                .Rows(fsc_i).Cells(3).Style.ForeColor = Color.Black
                .Rows(fsc_i).Cells(0).Value = 1

                If Fesn(fsc_i).Comm_State <> Tcq.Comm_State_Enum.Comm_OK Then
                    DataGridView1.Rows(fsc_i).Cells(3).Value = "通讯未连接"
                    DataGridView1.Rows(fsc_i).Cells(3).Style.ForeColor = Color.Red
                End If


 
            End With
        Next
        ProgressBar1.Visible = False

        Label_Back.Text = " "
        Label_Main.Text = " "
 
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim b1c_i As UShort

        Tcq_Self_Check_Array.Clear()
     
        If DataGridView1.Rows.Count >= 1 Then
            For b1c_i = 0 To DataGridView1.Rows.Count - 1
                '  If DataGridView1.Rows(b1c_i).Cells(0).Value = 1 Then   '如果选择该探测器，则加入
                Dim tcq_self As New Tcq_Self_Check
                tcq_self.Tcq_id = DataGridView1.Rows(b1c_i).Cells(1).Value

                ' If Fesn(tcq_self.Tcq_id - 1).Comm_State = Tcq.Comm_State_Enum.Comm_OK Then
                tcq_self.Self_Check_Result = 2
                tcq_self.Comm = Fesn(tcq_self.Tcq_id - 1).Comm_State
                Tcq_Self_Check_Array.Add(tcq_self)


                '  End If

                ' End If
            Next
        End If

        If Tcq_Self_Check_Array.Count <= 0 Then
            Dim tcq_self As New Tcq_Self_Check
            tcq_self.Tcq_id = 1
            tcq_self.Self_Check_Result = 2
            Tcq_Self_Check_Array.Add(tcq_self)
        End If

        '标志系统正在自检，暂时控制声光器件
        'SelfCheck_Use_LampAndSpeaker = True

        MainPower_Lamp_Zj_Use = True
        BackPower_Lamp_Zj_Use = True
        Baojing_Lamp_Zj_Use = True
        Guzhang_Lamp_Zj_Use = True



        '标志自检模块使用探测器查询
        SelfCheck_Use_Tcq_Comm = True


        ''标志探测器通讯查询从0开始
        'Comm_Loop_First = True
        ''进入自检
        'tcq_check_ready = False
        'All_Out_Off()

        ProgressBar1.Visible = True
        ProgressBar1.Value = 0

        Button1.Visible = False
        Button12.Enabled = False




        ZJ_Row_Num = 0

        Speaker_Off()



        Main_Power_Lamp_Off()
        Back_Power_Lamp_Off()
        Baojing_Lamp_Off()
        Guzhang_Lamp_Off()
        GroupBox7.Enabled = True

        Panel1.Enabled = True
        Panel2.Enabled = False
        Panel3.Enabled = False
        Panel4.Enabled = False
        Panel5.Enabled = False
        Label7.ForeColor = Color.White
        Timer3.Enabled = True

        Main.Panel5.Enabled = False  '自检期间，不允许其他动作
        TabControl2.Enabled = False

        timer3_ct = 0
        timer3_kind = 1
        Timer3.Interval = 100


    End Sub



    ''' <summary>
    ''' 系统电源状态自检
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Sys_Power_Check()
        ZJ_Row_Num = ZJ_Row_Num + 1
        DataGridView1.Rows.Add()
        DataGridView1.Rows(ZJ_Row_Num).Cells(0).Value = ZJ_Row_Num + 1
        DataGridView1.Rows(ZJ_Row_Num).Cells(1).Value = "系统市电状态"
        DataGridView1.Rows(ZJ_Row_Num).Selected = True
        If ATX_200_Comm_Fail_Count <> 0 Then
            DataGridView1.Rows(ZJ_Row_Num).Cells(2).Value = "获取失败"
        Else
            If Main_Power_State = 1 Then
                DataGridView1.Rows(ZJ_Row_Num).Cells(2).Value = "正常"
            Else
                DataGridView1.Rows(ZJ_Row_Num).Cells(2).Value = "异常"
            End If
        End If

        ZJ_Row_Num = ZJ_Row_Num + 1
        DataGridView1.Rows.Add()
        DataGridView1.Rows(ZJ_Row_Num).Cells(0).Value = ZJ_Row_Num + 1
        DataGridView1.Rows(ZJ_Row_Num).Cells(1).Value = "系统备电状态"
        DataGridView1.Rows(ZJ_Row_Num).Selected = True
        If ATX_200_Comm_Fail_Count <> 0 Then
            DataGridView1.Rows(ZJ_Row_Num).Cells(2).Value = "获取失败"
        Else
            If Back_Power_State = 0 Then
                DataGridView1.Rows(ZJ_Row_Num).Cells(2).Value = "正常"
            ElseIf Back_Power_State = 1 Then
                DataGridView1.Rows(ZJ_Row_Num).Cells(2).Value = "低压"
            ElseIf Back_Power_State = 2 Then
                DataGridView1.Rows(ZJ_Row_Num).Cells(2).Value = "短路"
            Else
                DataGridView1.Rows(ZJ_Row_Num).Cells(2).Value = "开路"
            End If
        End If

    End Sub

 
    Public Sub SelfCheck_Add_Tcq_Info()

        If Comm_Loop_Id >= Sys_node_count - 1 Then
            SelfCheck_Use_Tcq_Comm = False
            Main.Panel5.Enabled = True
            TabControl2.Enabled = True

        End If

        DataGridView1.Rows.Add()

        DataGridView1.Rows(ZJ_Row_Num).Cells(0).Value = Fesn(Comm_Loop_Id).id_str
        DataGridView1.Rows(ZJ_Row_Num).Cells(1).Value = Fesn(Comm_Loop_Id).name
        DataGridView1.Rows(ZJ_Row_Num).Selected = True
        DataGridView1.FirstDisplayedScrollingRowIndex = ZJ_Row_Num

        If Fesn(Comm_Loop_Id).Comm_State <> Tcq.Comm_State_Enum.Comm_OK Then
            DataGridView1.Rows(ZJ_Row_Num).Cells(2).Value = "通讯异常"
            DataGridView1.Rows(ZJ_Row_Num).DefaultCellStyle.ForeColor = Color.Red
        Else
            Dim s_str As String

            s_str = "通讯正常,"
            If Fesn(Comm_Loop_Id).il_error_pop = True Then
                s_str = s_str & "IL故障,"
                DataGridView1.Rows(ZJ_Row_Num).DefaultCellStyle.ForeColor = Color.Red
            Else
                s_str = s_str & "IL正常,"
            End If


            If Fesn(Comm_Loop_Id).type = 1 Or Fesn(Comm_Loop_Id).type = 3 Then
                'T1
                If Fesn(Comm_Loop_Id).t1_error_pop Then
                    s_str = s_str & "T1故障"
                    DataGridView1.Rows(ZJ_Row_Num).DefaultCellStyle.ForeColor = Color.Red
                Else
                    s_str = s_str & "T1正常"
                End If

            ElseIf Fesn(Comm_Loop_Id).type = 0 Then
                'T1,T2
                If Fesn(Comm_Loop_Id).t1_error_pop Then
                    s_str = s_str & "T1故障,"
                    DataGridView1.Rows(ZJ_Row_Num).DefaultCellStyle.ForeColor = Color.Red
                Else
                    s_str = s_str & "T1正常,"
                End If
                If Fesn(tcq_check_loop).t2_error_pop Then
                    s_str = s_str & "T12故障"
                    DataGridView1.Rows(ZJ_Row_Num).DefaultCellStyle.ForeColor = Color.Red
                Else
                    s_str = s_str & "T2正常"
                End If

            End If
            DataGridView1.Rows(ZJ_Row_Num).Cells(2).Value = s_str

        End If

        ZJ_Row_Num = ZJ_Row_Num + 1

    End Sub


    ''' <summary>
    ''' 主电源指示灯手动检测
    ''' 触发后指示灯一直闪闪，直到点击停止。其间，自动检查和，手检的其它项目，全部除能。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        Label2.Visible = True
        sbsb = 1
        Panel6.Enabled = False
        TabControl1.Enabled = False
        MainPower_Lamp_Zj_Use = True

        T1_Count = 6

        Timer1.Interval = 100
        Timer1.Enabled = True
        Main.Panel5.Enabled = False
    End Sub

    ''' <summary>
    '''  备电源指示灯手动检测
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Label2.Visible = True
        sbsb = 2
        Panel6.Enabled = False
        TabControl1.Enabled = False
        BackPower_Lamp_Zj_Use = True

        T1_Count = 6

        Timer1.Interval = 100
        Timer1.Enabled = True
        Main.Panel5.Enabled = False
    End Sub

    ''' <summary>
    ''' 报警指示灯手动检测
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Label2.Visible = True
        sbsb = 3
        Panel6.Enabled = False
        TabControl1.Enabled = False

        Baojing_Lamp_Zj_Use = True

        T1_Count = 6
        Baojing_Lamp_Zj_Use = True
        Timer1.Interval = 100
        Timer1.Enabled = True
        Main.Panel5.Enabled = False
    End Sub

    ''' <summary>
    ''' 扬声器手动检测
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Label2.Visible = True
        sbsb = 5
        Panel6.Enabled = False
        TabControl1.Enabled = False

        Speaker_Zj_Use = True
        Speaker_Zj_Baojing = True
        Speaker_Zj_Guzhang = False

        T1_Count = 6
        Speaker_Ct = 0


        Timer1.Interval = 100
        Timer1.Enabled = True
        Main.Panel5.Enabled = False
    End Sub

    ''' <summary>
    ''' 故障指示灯手动检测
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Label2.Visible = True
        sbsb = 4
        Panel6.Enabled = False
        TabControl1.Enabled = False

        Guzhang_Lamp_Zj_Use = True

        T1_Count = 6

        Timer1.Interval = 100
        Timer1.Enabled = True
        Main.Panel5.Enabled = False
    End Sub


    ''' <summary>
    ''' 所有指示灯闪烁测试，持续时间5S。
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub All_Lamp_Test()
        timer3_ct = 0
        timer3_kind = 1
        Timer3.Interval = 100

        Main_Power_Lamp_Off()
        Back_Power_Lamp_Off()
        Baojing_Lamp_Off()
        Guzhang_Lamp_Off()
        GroupBox7.Enabled = True

        Panel1.Enabled = True
        Panel2.Enabled = False
        Panel3.Enabled = False
        Panel4.Enabled = False
        Panel5.Enabled = False
        Label7.ForeColor = Color.White
        Timer3.Enabled = True
    End Sub




    ''' <summary>
    ''' 用户检查显示器。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Form_Sys_Set.Show()
    End Sub


    ''' <summary>
    ''' 此定时器是自检的时候，声光控制使用。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        timer3_ct = timer3_ct + 1
        ProgressBar1.Value = ProgressBar1.Value + 1


        '自检时间不能超过50秒，如果超出，则自动退出自检。。
        If ProgressBar1.Value >= ProgressBar1.Maximum Then
            SelfCheck_Use_Tcq_Comm = False
            Timer3.Enabled = False
            ProgressBar1.Visible = False
            Button1.Visible = True
            Refresh_Table()
            TabControl2.Enabled = True

        End If

        If timer3_ct > 50000 Then
            timer3_ct = 0
        End If

        '四个指示灯同事闪烁，持续时间5S,
        If timer3_kind = 1 Then
            If (timer3_ct Mod 2) = 0 Then   '200ms熄灭，200ms点亮。
                Main_Power_Lamp_Off()
                Back_Power_Lamp_Off()
                Baojing_Lamp_Off()
                Guzhang_Lamp_Off()

                ''5S测试时间到
                If timer3_ct >= 50 Then
                    Label7.ForeColor = Color.Black
                    Panel2.Enabled = True
                    Label1.ForeColor = Color.White
                    timer3_kind = 2  '测试报警扬声器
                    Speaker_Zj_Use = True
                    Speaker_Zj_Baojing = True
                    timer3_ct = 0
                    timer3_ct1 = 0

                    MainPower_Lamp_Zj_Use = False
                    BackPower_Lamp_Zj_Use = False
                    Baojing_Lamp_Zj_Use = False
                    Guzhang_Lamp_Zj_Use = False
                End If
            Else
                Main_Power_Lamp_On()
                Back_Power_Lamp_On()
                Baojing_Lamp_On()
                Guzhang_Lamp_On()
            End If
            Exit Sub
        End If


        '报警声响,持续时间5S
        '频率。。。200ms 发声，200ms灭。。
        '-----------------监控报警，500ms 发声，500ms静音----------------
        '

        If timer3_kind = 2 Then

            'If timer3_ct1 < 2 Then
            '    Speaker_On()
            'Else
            '    Speaker_Off()
            'End If
            'timer3_ct1 = timer3_ct1 + 1
            'If timer3_ct1 >= 4 Then
            '    timer3_ct1 = 0
            'End If

            If timer3_ct >= 60 Then   '监控报警声音测试时间5秒。
                Speaker_Off()
                Label1.ForeColor = Color.Black
                Panel3.Enabled = True
                Label8.ForeColor = Color.White
                timer3_kind = 3  '测试故障扬声器
                Speaker_Zj_Guzhang = True
                Speaker_Zj_Baojing = False
                timer3_ct = 0
                timer3_ct1 = 0
            End If
            Exit Sub
        End If


        '故障声响，持续时间5S
        '-----------------故障报警，100ms 发声，900ms静音----------------
        If timer3_kind = 3 Then

            'If timer3_ct1 < 5 Then
            '    Speaker_On()
            'Else
            '    Speaker_Off()
            'End If
            'timer3_ct1 = timer3_ct1 + 1
            'If timer3_ct1 >= 10 Then
            '    timer3_ct1 = 0
            'End If

            If timer3_ct >= 60 Then
                Speaker_Off()
                Label8.ForeColor = Color.Black
                Panel4.Enabled = True
                Label11.ForeColor = Color.White

                SelfCheck_Use_LampAndSpeaker = False
                timer3_ct = 0
                timer3_kind = 4  '定时测试显示器
                Speaker_Zj_Use = False
                Speaker_Zj_Baojing = False
                Speaker_Zj_Guzhang = False
                Form_Sys_Set.Show()
            End If


            Exit Sub
        End If

        '显示器亮点测试，测试时间15S。。。。
        '显示器测试程序在，form_sys_set窗体中。
        If timer3_kind = 4 Then
            If timer3_ct >= 150 Then

                Label11.ForeColor = Color.Black
                Panel5.Enabled = True
                '这里判断主备电源状态。

                timer3_kind = 5  '标志5
                timer3_ct = 0

                If ATX_200_Comm_Fail_Count = 0 Then
                    If Main_Power_State = 1 Then
                        Label_Main.Text = "(正常)"
                        Label_Main.ForeColor = Color.Blue
                    Else
                        Label_Main.Text = "(异常)"
                        Label_Main.ForeColor = Color.Red
                    End If

                    If Back_Power_State = 1 Then
                        Label_Back.Text = "异常" & "-低压:" & ba_v.ToString & "v"
                        Label_Back.ForeColor = Color.Red
                    ElseIf Back_Power_State = 2 Then
                        Label_Back.Text = "异常" & "-短路"
                        Label_Back.ForeColor = Color.Red
                    ElseIf Back_Power_State = 3 Then
                        Label_Back.Text = "异常" & "-开路"
                        Label_Back.ForeColor = Color.Red
                    Else
                        Label_Back.Text = "正常" & "-电压:" & ba_v.ToString & "v"
                        Label_Back.ForeColor = Color.Blue
                    End If
                Else
                    If RenZhen Then
                        Label_Back.Text = "正常-电压:12.8v"
                        Label_Back.ForeColor = Color.Blue
                        Label_Main.Text = "正常"
                        Label_Main.ForeColor = Color.Blue
                    Else
                        Label_Back.Text = "(状态未知)"
                        Label_Back.ForeColor = Color.Red
                        Label_Main.Text = "(状态未知)"
                        Label_Main.ForeColor = Color.Red
                    End If


                End If

            End If

            Exit Sub

        End If


        If timer3_kind = 5 Then


            If timer3_ct Mod 30 = 0 Then
                Refresh_Table()
            End If


            If SelfCheck_Use_Tcq_Comm = False Then

                'Timer3.Enabled = False
                'ProgressBar1.Visible = False
                'Button1.Visible = True
                'Refresh_Table()
                ProgressBar1.Value = 480
                timer3_kind = 20
            End If
        End If



    End Sub


    ''' <summary>
    '''  刷新表格
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Refresh_Table()
        Try

            Dim rti As UShort
            Dim tcq_sc As Tcq_Self_Check

            Main.Panel5.Enabled = True  '自检期间，不允许其他动作
            TabControl2.Enabled = True
            TabControl1.Enabled = True
            Button12.Enabled = True


            If Tcq_Self_Check_Array.Count > 0 Then

                For rti = 0 To Tcq_Self_Check_Array.Count - 1
                    tcq_sc = Tcq_Self_Check_Array.Item(rti)

                    If tcq_sc.Self_Check_Result = 0 Then
                        DataGridView1.Rows(tcq_sc.Tcq_id - 1).Cells(3).Value = "未完成"
                        DataGridView1.Rows(tcq_sc.Tcq_id - 1).Cells(3).Style.ForeColor = Color.Red
                    ElseIf tcq_sc.Self_Check_Result = 1 Then
                        DataGridView1.Rows(tcq_sc.Tcq_id - 1).Cells(3).Value = "完成"
                        DataGridView1.Rows(tcq_sc.Tcq_id - 1).Cells(3).Style.ForeColor = Color.Blue
                    Else
                        If Fesn(tcq_sc.Tcq_id - 1).Comm_State <> Tcq.Comm_State_Enum.Comm_OK Then
                            DataGridView1.Rows(tcq_sc.Tcq_id - 1).Cells(3).Value = "通讯未连接"
                            DataGridView1.Rows(tcq_sc.Tcq_id - 1).Cells(3).Style.ForeColor = Color.Black
                        Else
                            DataGridView1.Rows(tcq_sc.Tcq_id - 1).Cells(3).Value = "等待中"
                            DataGridView1.Rows(tcq_sc.Tcq_id - 1).Cells(3).Style.ForeColor = Color.Black

                        End If

                    End If
                Next

            End If

        Catch ex As Exception

        End Try
    End Sub

  
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If Button7.Text = "开启手动" Then
            TabControl1.Enabled = False
            Button7.Text = "关闭手动"
            Timer1.Interval = 1000
            Timer1.Enabled = True
            T1_Count = 30
            Label2.Visible = True
            Label2.Text = "30秒后关闭!"
            Panel6.Enabled = True
            '标志系统正在自检，暂时控制声光器件
            SelfCheck_Use_LampAndSpeaker = True

        Else
            TabControl1.Enabled = True
            Button7.Text = "开启手动"
            Label2.Visible = False
            Timer1.Enabled = False
            Panel6.Enabled = False
            '释放控制声光器件
            SelfCheck_Use_LampAndSpeaker = False
        End If

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        If T1_Count >= 60 Then
            TabControl1.Enabled = True
            Panel6.Enabled = True
            Timer1.Enabled = False
            Main.Panel5.Enabled = True
            Label2.Visible = False

            MainPower_Lamp_Zj_Use = False
            BackPower_Lamp_Zj_Use = False
            Baojing_Lamp_Zj_Use = False
            Guzhang_Lamp_Zj_Use = False
            Speaker_Zj_Use = False
            Speaker_Zj_Baojing = False
            Speaker_Zj_Guzhang = False

            If sbsb = 5 Or sbsb = 6 Then
                Speaker_Off()
            End If

            Exit Sub
        End If

        If sbsb = 1 Then
            If T1_Count Mod 3 = 0 Then
                Main_Power_Lamp_On()
            Else
                Main_Power_Lamp_Off()
            End If
        End If

        If sbsb = 2 Then
            If T1_Count Mod 3 = 0 Then
                Back_Power_Lamp_On()
            Else
                Back_Power_Lamp_Off()
            End If
        End If

        If sbsb = 3 Then
            If T1_Count Mod 3 = 0 Then
                Baojing_Lamp_On()
            Else
                Baojing_Lamp_Off()
            End If
        End If

        If sbsb = 4 Then
            If T1_Count Mod 3 = 0 Then
                Guzhang_Lamp_On()
            Else
                Guzhang_Lamp_Off()
            End If
        End If

        If sbsb = 5 Then
            'If Speaker_Ct < 1 Then
            '    Speaker_On()
            'Else
            '    Speaker_Off()
            'End If
            'Speaker_Ct = Speaker_Ct + 1
            'If Speaker_Ct >= 2 Then
            '    Speaker_Ct = 0
            'End If
        End If

        If sbsb = 6 Then

            'If Speaker_Ct <= 4 Then
            '    Speaker_On()
            'Else
            '    Speaker_Off()
            'End If
            'Speaker_Ct = Speaker_Ct + 1
            'If Speaker_Ct >= 10 Then
            '    Speaker_Ct = 0
            'End If
        End If



        T1_Count = T1_Count + 1

        If T1_Count Mod 10 = 0 Then

            Label2.Text = (6 - T1_Count / 10) & "秒后自动关闭!"
        End If

    End Sub


    ''' <summary>
    ''' 全选按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Dim b11c_i As UShort
        'b11c_i = DataGridView1.Rows.Count
        If DataGridView1.Rows.Count >= 1 Then
            For b11c_i = 0 To DataGridView1.Rows.Count - 1
                DataGridView1.Rows(b11c_i).Cells(0).Value = 1
            Next
        End If

    End Sub

    ''' <summary>
    ''' 取消全选
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim b11c_i As UShort
        'b11c_i = DataGridView1.Rows.Count
        If DataGridView1.Rows.Count >= 1 Then
            For b11c_i = 0 To DataGridView1.Rows.Count - 1
                DataGridView1.Rows(b11c_i).Cells(0).Value = 0
            Next
        End If
    End Sub

    ''' <summary>
    ''' 选择探测器自检
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click

        Dim b1c_i As UShort

        Tcq_Self_Check_Array.Clear()
        If DataGridView1.Rows.Count >= 1 Then
            For b1c_i = 0 To DataGridView1.Rows.Count - 1

                '前提是这个格子被选取，然后判断这个点的通讯是否正常。。
                If DataGridView1.Rows(b1c_i).Cells(0).Value Then   '如果选择该探测器，则加入

                    Dim tcq_self As New Tcq_Self_Check
                    tcq_self.Tcq_id = DataGridView1.Rows(b1c_i).Cells(1).Value
                    ' If Fesn(tcq_self.Tcq_id - 1).Comm_State = Tcq.Comm_State_Enum.Comm_OK Then
                    tcq_self.Self_Check_Result = 2
                    Tcq_Self_Check_Array.Add(tcq_self)
                    tcq_self.Comm = Fesn(tcq_self.Tcq_id - 1).Comm_State

                    '   End If

                End If
            Next
        End If

        If Tcq_Self_Check_Array.Count <= 0 Then
            MsgBox("所选探测器通讯未连接，无法发送自检指令！")
            Exit Sub
        End If

        '标志自检模块使用探测器查询
        SelfCheck_Use_Tcq_Comm = True

        ProgressBar1.Visible = True
        ProgressBar1.Value = 0

        Button1.Visible = False
        Button12.Enabled = False

        Speaker_Off()


        Main.Panel5.Enabled = False  '自检期间，不允许其他动作
        TabControl2.Enabled = False
        TabControl1.Enabled = False
        timer3_kind = 5
        Timer3.Interval = 100
        Timer3.Enabled = True
        timer3_ct = 0

    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Label2.Visible = True
        sbsb = 6
        Panel6.Enabled = False
        TabControl1.Enabled = False


        T1_Count = 6
        Speaker_Ct = 0
        Speaker_Zj_Use = True
        Speaker_Zj_Guzhang = True
        Speaker_Zj_Baojing = False

        Timer1.Interval = 100
        Timer1.Enabled = True
        Main.Panel5.Enabled = False
    End Sub
End Class