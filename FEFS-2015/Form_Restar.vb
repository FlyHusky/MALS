Public Class Form_Restar

    Private rest_kind As Byte
    Private over_time As UShort


    Private Sub Form_Restar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.Top = Form1.Top
        Me.Left = Form1.Left
        Me.Width = Form1.Width
        Me.Height = Form1.Height

        'ProgressBar1.Value = 0
        'ProgressBar1.Maximum = Sys_node_count
        'Timer3.Interval = 40
        'Timer3.Enabled = True

        'Form1.Close()


        'If Sys_Restart Then
        '    ProgressBar1.Maximum = 20
        '    Timer3.Interval = 50
        'End If


        Dim fsc_i As UShort

        DataGridView1.Rows.Clear()
        DataGridView1.Rows.Add(Sys_node_count)

        For fsc_i = 0 To Sys_node_count - 1
            With DataGridView1
                .Rows(fsc_i).Cells(1).Value = Fesn(fsc_i).id_str
                .Rows(fsc_i).Cells(2).Value = Fesn(fsc_i).name
                .Rows(fsc_i).Cells(3).Value = ""
                .Rows(fsc_i).Cells(3).Style.ForeColor = Color.Black
                .Rows(fsc_i).Cells(0).Value = 1

                If Fesn(fsc_i).Comm_State <> Tcq.Comm_State_Enum.Comm_OK Then
                    .Rows(fsc_i).Cells(3).Value = "通讯未连接"
                    .Rows(fsc_i).Cells(3).Style.ForeColor = Color.Red
                End If



            End With
        Next



    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick

        'If Sys_Restart Then
        '    If ProgressBar1.Value >= ProgressBar1.Maximum - 1 Then
        '        Application.Restart()
        '    Else
        '       
        '    End If
        '    Exit Sub
        'End If

        ProgressBar1.Value = ProgressBar1.Value + 1

        If ProgressBar1.Value >= ProgressBar1.Maximum Then
            ProgressBar1.Value = ProgressBar1.Maximum - 20
        End If
        over_time = over_time + 1

        If over_time >= 100 Then  'chaoshi tuichu
            Application.Restart()
        End If

        If over_time = 5 Then


            If over_time Mod 20 = 0 Then
                Refresh_Table()
            End If

        End If


        '探测器远程复位完成。
        If Rest_Use_Tcq_Comm = False Then

            Timer3.Enabled = False
            ProgressBar1.Visible = False
            Refresh_Table()
            GroupBox1.Enabled = True
            GroupBox2.Enabled = True
            Main.Panel5.Enabled = True

            Label12.Visible = False
            Button12.Enabled = True

            Button1.Enabled = True
            Button2.Enabled = True

            '如果是同时复位主机和探测器，则在探测器复位完成后，复位自己程序。
            If rest_kind = 1 Then
                Sys_Close()
                Application.Restart()
            Else
                '仅仅是复位探测器。在其完成后，需要清楚 报警和 故障状态。
                Tcq_Reset_BJ_GZ()
                Main.Guzhang_Refresh_Lable = True
                Main.Jiankong_Refresh_Lable = True
            End If
        End If

    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Dim b11c_i As UShort
        'b11c_i = DataGridView1.Rows.Count
        If DataGridView1.Rows.Count >= 1 Then
            For b11c_i = 0 To DataGridView1.Rows.Count - 1
                DataGridView1.Rows(b11c_i).Cells(0).Value = 1
            Next
        End If
    End Sub

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
    ''' 单独复位，探测器
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Dim b1c_i As UShort

        Tcq_Self_Check_Array.Clear()
        If DataGridView1.Rows.Count >= 1 Then
            For b1c_i = 0 To DataGridView1.Rows.Count - 1
                If DataGridView1.Rows(b1c_i).Cells(0).Value Then   '如果选择该探测器，则加入
                    Dim tcq_self As New Tcq_Self_Check
                    tcq_self.Tcq_id = DataGridView1.Rows(b1c_i).Cells(1).Value
                    tcq_self.Comm = Fesn(tcq_self.Tcq_id - 1).Comm_State
                    'tcq_self.Self_Check_Result = 2
                    'Tcq_Self_Check_Array.Add(tcq_self)

                    ' If Fesn(b1c_i).Comm_State = Tcq.Comm_State_Enum.Comm_OK Then
                    tcq_self.Self_Check_Result = 2
                    Tcq_Self_Check_Array.Add(tcq_self)
                    'End If


                End If
            Next
        End If

        If Tcq_Self_Check_Array.Count <= 0 Then
            MsgBox("所选探测器通讯未连接，无法发送自检指令！")
            Exit Sub
        End If


        '标志自检模块使用探测器查询

        Rest_Use_Tcq_Comm = True
        Rest_Use_Tcq_Comm_OK = False

        ProgressBar1.Visible = True
        ProgressBar1.Value = 0

        Button1.Enabled = False
        Button12.Enabled = False

        Main.Panel5.Enabled = False  '自检期间，不允许其他动作
        GroupBox2.Enabled = False


        Timer3.Interval = 500
        Timer3.Enabled = True

        rest_kind = 0
        over_time = 0

    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Sys_Close()
        Application.Restart()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    

        Dim b1c_i As UShort

        Tcq_Self_Check_Array.Clear()
        If Sys_node_count >= 1 Then
            For b1c_i = 0 To Sys_node_count - 1

                Dim tcq_self As New Tcq_Self_Check

                tcq_self.Tcq_id = Fesn(b1c_i).id

                tcq_self.Comm = Fesn(b1c_i).Comm_State

                '仅对通讯正常的，进行复位。。
                ' If Fesn(b1c_i).Comm_State = Tcq.Comm_State_Enum.Comm_OK Then
                tcq_self.Self_Check_Result = 2
                Tcq_Self_Check_Array.Add(tcq_self)


                ' End If

            Next
        End If

        If Tcq_Self_Check_Array.Count <= 0 Then
            Dim tcq_self As New Tcq_Self_Check
            tcq_self.Tcq_id = 1
            tcq_self.Self_Check_Result = 2
            Tcq_Self_Check_Array.Add(tcq_self)
        End If


        '标志自检模块使用探测器查询
        Rest_Use_Tcq_Comm = True
        Rest_Use_Tcq_Comm_OK = False

        ProgressBar1.Visible = True
        ProgressBar1.Value = 0
        GroupBox1.Enabled = False
        Main.Panel5.Enabled = False  '自检期间，不允许其他动作
        GroupBox2.Enabled = False

        ''复位图显。。
        'Main.TX_RST = True

        Timer3.Interval = 500
        Timer3.Enabled = True
        rest_kind = 1
        over_time = 0
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
            GroupBox1.Enabled = True
            GroupBox2.Enabled = True



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
                        DataGridView1.Rows(tcq_sc.Tcq_id - 1).Cells(3).Value = "等待中"
                        DataGridView1.Rows(tcq_sc.Tcq_id - 1).Cells(3).Style.ForeColor = Color.Black
                    End If
                Next

            End If

        Catch ex As Exception

        End Try
    End Sub

End Class