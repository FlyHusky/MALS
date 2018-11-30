Option Strict Off
Imports FEFS.SUSI
Imports System.IO.Ports
Public Class All

    ''' <summary>
    '''  参数设置按键展开和折叠标志
    ''' </summary>
    ''' <remarks></remarks>
    Private but_set_tree As Boolean
    ''' <summary>
    ''' 实时数据按钮展开和折叠标志
    ''' </summary>
    ''' <remarks></remarks>
    Private but_view_tree As Boolean
    ''' <summary>
    ''' 参数设置按键展开后，其他按键下移此次
    ''' </summary>
    ''' <remarks></remarks>
    Private but_down_size As UShort
    ''' <summary>
    ''' 标志进入接收中断后，要接收到通讯帧的第一个数据。。。
    ''' </summary>
    ''' <remarks></remarks>
    Private Comm_Get_First_Byte As Boolean  '标志接收第一个字节
    ''' <summary>
    ''' 接收探测器发过来的数据，长度20
    ''' </summary>
    ''' <remarks></remarks>
    Private Comm_Get_Ar(20) As Byte
    ''' <summary>
    ''' 接收探测器发上来的有效通讯数据的字节数
    ''' </summary>
    ''' <remarks></remarks>
    Private Comm_Get_Byte_Count As Byte
    Public Comm_Loop_id As UShort     '当前的ID 
    Dim send_ar(1) As Byte  '主机查询指令
    Private Send_tcq(2) As Byte 'f4 00 01  查询
    ''' <summary>
    '''  ALL  窗体初始化。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub All_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '-----------------------------------------------
        but_down_size = 50
        But_sys_set.Visible = False
        But_tcq_set.Visible = False
        but_set_tree = False

        '默认窗体为列表显示。
        But_Table_View.Visible = True
        But_Map_View.Visible = True
        but_view_tree = True
        But_Table_View.Enabled = False
        But_Map_View.Enabled = True
        Button1.Enabled = False
        Button2.Top = Button2.Top + but_down_size
        Button3.Top = Button3.Top + but_down_size
        But_sys_map.Top = But_sys_map.Top + but_down_size
        But_sys_check.Top = But_sys_check.Top + but_down_size
        But_sys_reset.Top = But_sys_reset.Top + but_down_size
        But_sys_info.Top = But_sys_info.Top + but_down_size


        Comm_Loop_id = 0
        'SUSI口初始化。。
        Label6.Font = New Font("宋体", 15)
        If Susi_IO_Init() Then
            Label6.Text = "Susi 驱动完成。"
            All_Out_Off()
            Set_All_IO_Out_Dir()
        End If

        Welcom_Form.Visible = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized





        Comm_Tcq_Usart_Init()
        If tcq_usart_state Then
            Timer1.Interval = 50  '100ms定时器。。。
            Timer1.Enabled = True
        End If


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        But_set_open_close_tree()
        Button1.Enabled = False
        Button2.Enabled = True
        Button3.Enabled = True

        but_view_tree = True
        But_Table_View.Visible = True
        But_Map_View.Visible = True

        Button2.Top = Button2.Top + but_down_size
        Button3.Top = Button3.Top + but_down_size
        But_sys_map.Top = But_sys_map.Top + but_down_size
        But_sys_check.Top = But_sys_check.Top + but_down_size
        But_sys_reset.Top = But_sys_reset.Top + but_down_size
        But_sys_info.Top = But_sys_info.Top + but_down_size


        Form_Baojin.Visible = False
        Form1.Visible = True
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        But_set_open_close_tree()
        But_view_open_close_tree()


        Button2.Enabled = False
        Button1.Enabled = True
        Button3.Enabled = True
        ' Form1.Visible = False
        Form_Baojin.Visible = True
        Form_Baojin.BringToFront()
    End Sub

    ''' <summary>
    ''' 用户点击进入参数设置界面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        But_view_open_close_tree()
        Button2.Enabled = True
        Button1.Enabled = True
        Button3.Enabled = False


        But_sys_set.Visible = True
        But_tcq_set.Visible = True

        But_sys_map.Top = But_sys_map.Top + but_down_size
        But_sys_check.Top = But_sys_check.Top + but_down_size
        But_sys_reset.Top = But_sys_reset.Top + but_down_size
        But_sys_info.Top = But_sys_info.Top + but_down_size
        but_set_tree = True

        Form1.Visible = False
        Form_Baojin.Visible = False
        Form_Set.Visible = True
    End Sub

    ''' <summary>
    ''' 设置按键折叠展开处理。
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub But_set_open_close_tree()
        If but_set_tree Then
            But_sys_set.Visible = False
            But_tcq_set.Visible = False
            But_sys_map.Top = But_sys_map.Top - but_down_size
            But_sys_check.Top = But_sys_check.Top - but_down_size
            But_sys_reset.Top = But_sys_reset.Top - but_down_size
            But_sys_info.Top = But_sys_info.Top - but_down_size
            but_set_tree = False
        End If
    End Sub

    ''' <summary>
    ''' 实时数据按钮展开处理。
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub But_view_open_close_tree()
        If but_view_tree Then
            But_Table_View.Visible = False
            But_Map_View.Visible = False
            Button2.Top = Button2.Top - but_down_size
            Button3.Top = Button3.Top - but_down_size
            But_sys_map.Top = But_sys_map.Top - but_down_size
            But_sys_check.Top = But_sys_check.Top - but_down_size
            But_sys_reset.Top = But_sys_reset.Top - but_down_size
            But_sys_info.Top = But_sys_info.Top - but_down_size

            but_view_tree = False
        End If
    End Sub

    Private Sub ESC_but_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ESC_but.Click
        End
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_sys_check.Click
        All_Out_On()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_sys_reset.Click
        All_Out_Off()
    End Sub

    ''' <summary>
    ''' Timer1 定时器中断 过程
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        '1:通讯状态处理() '第一次进来时，前面是没有探测器的。。。。
        Comm_State_Work_Center(Comm_Loop_id)

        send_ar(0) = &HF7
        send_ar(1) = Fesn(Comm_Loop_id).addres
        Fesn(Comm_Loop_id).Comm_State = Tcq.Comm_State_Enum.Comm_Fail
        Tcq_port.Write(send_ar, 0, 2)  '将数据发送出去。

        Comm_Loop_id = Comm_Loop_id + 1  '下初始值是0，+1=1   结束值：49 

        If Comm_Loop_id > 49 Then
            Comm_Loop_id = 0
        End If

    End Sub

    ''' <summary>
    ''' 通讯状态事物处理中心.根据返回的上一次的通讯
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Comm_State_Work_Center(ByVal Loop_id As UShort)

        If Loop_id <= 0 Then
            Loop_id = 50
        End If

        If Loop_id > 50 Then
            Exit Sub
        End If
        Loop_id = Loop_id - 1

        If Fesn(Loop_id).Comm_State = Tcq.Comm_State_Enum.Comm_OK Then
            If Fesn(Loop_id).Comm_Fail_Count >= 3 Then  ' 先前探测器已经为通讯故障啦。
                '任务1：将故障信息管理中，此探测器的故障信息消除。。。。。 
                Dim i_csw As UShort
                Dim gz_temp As Guzhang_info
                If Guzhang_Array.Count >= 1 Then
                    For i_csw = 0 To Guzhang_Array.Count - 1
                        gz_temp = Guzhang_Array(i_csw)
                        If gz_temp.tcq_id = Loop_id Then
                            Guzhang_Array.Remove(gz_temp)
                            Refresh_Guzhang_Message()
                            Exit For
                        End If
                    Next
                End If
            End If
            Fesn(Loop_id).Comm_Fail_Count = 0
        ElseIf (Fesn(Loop_id).Comm_State = Tcq.Comm_State_Enum.Comm_Wait) Then  '通讯等待状态
            Exit Sub
        Else
            If Fesn(Loop_id).Comm_Fail_Count >= 3 Then
                Fesn(Loop_id).Comm_Fail_Count = 4
                Exit Sub
            Else
                Fesn(Loop_id).Comm_Fail_Count = Fesn(Loop_id).Comm_Fail_Count + 1
                If Fesn(Loop_id).Comm_Fail_Count >= 3 Then '累计三次通讯故障。。
                    '认定通讯故障，添加通讯故障信息
                    Dim gz_temp As New Guzhang_info
                    gz_temp.tcq_id = Loop_id
                    gz_temp.date_str = Now.Year.ToString & ":" & Now.Month.ToString & ":" & Now.Date.ToString
                    gz_temp.time_str = Now.Hour.ToString & ":" & Now.Minute.ToString & ":" & Now.Second.ToString
                    gz_temp.Guzhang_kind = 1
                    Guzhang_Array.Add(gz_temp)
                    Refresh_Guzhang_Message()
                End If
            End If
        End If

    End Sub

    ''' <summary>
    ''' 刷新故障显示框
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Refresh_Guzhang_Message()
        Dim rgm_i As UShort
        Dim rgm_gz As Guzhang_info

        rgm_i = DataGridView1.Rows.Count

        While (rgm_i > 0)
            DataGridView1.Rows.RemoveAt(rgm_i - 1)
            rgm_i = DataGridView1.Rows.Count
        End While


        If Guzhang_Array.Count >= 1 Then
            For rgm_i = 0 To Guzhang_Array.Count - 1
                rgm_gz = Guzhang_Array.Item(rgm_i)
                DataGridView1.SelectAll()
                DataGridView1.ClearSelection()
                DataGridView1.Rows.Add()
                DataGridView1.Rows(rgm_i).Cells(0).Value = rgm_i + 1
                DataGridView1.Rows(rgm_i).Cells(1).Value = rgm_gz.time_str
                DataGridView1.Rows(rgm_i).Cells(2).Value = rgm_gz.tcq_id
                DataGridView1.Rows(rgm_i).Cells(3).Value = rgm_gz.Guzhang_kind
            Next
        End If

    End Sub

    ''' <summary>
    ''' 探测器通讯口初始化，串口1,9600,N,1
    ''' </summary>
    ''' <returns> true=1</returns>
    ''' <remarks></remarks>
    Public Function Comm_Tcq_Usart_Init() As Boolean
        Try
            Tcq_port = New SerialPort("com1", 9600, Parity.None, 8, StopBits.One)
            Tcq_port.ReadBufferSize = 20
            Tcq_port.WriteBufferSize = 2
            Tcq_port.ReceivedBytesThreshold = 11
            Tcq_port.Open()
            Tcq_port.Write(send_ar, 0, 2)
            tcq_usart_state = True
            Return True
        Catch ex As Exception
            tcq_usart_state = False
            'MsgBox(ex.ToString)
            Return False
        End Try
        Return False
    End Function

    ''' <summary>
    ''' 通讯接收事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Tcq_port_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles Tcq_port.DataReceived

        Dim temp_b As Byte
        Dim temp_j As UShort

        temp_j = 0
        temp_b = 0

        Tcq_port.Read(Comm_Get_Ar, 0, 11)

        If Comm_Get_Ar(0) <> &HFC Then   '报头不对
            Exit Sub
        End If

        Dim addr_t As Byte               '地址不对
        addr_t = Fesn(Comm_Loop_id - 1).addres
        If Comm_Get_Ar(1) <> addr_t Then
            Exit Sub
        End If

        'If Comm_Get_Byte_Count = 0 Then
        '    If temp_b = &HFC Then
        '        Comm_Get_Ar(0) = &HFC
        '        Comm_Get_Byte_Count = 1
        '        Exit Sub
        '    End If

        'ElseIf (Comm_Get_Byte_Count = 1) Then
        '    Dim addr_t As Byte
        '    addr_t = comm_loop_id + 1
        '    If temp_b = addr_t Then
        '        Comm_Get_Ar(1) = temp_b
        '        Comm_Get_Byte_Count = 2
        '        Exit Sub
        '    Else
        '        Comm_Get_Byte_Count = 0  '地址不对
        '        Exit Sub
        '    End If
        'End If

        'Comm_Get_Ar(Comm_Get_Byte_Count) = temp_b
        'Comm_Get_Byte_Count = Comm_Get_Byte_Count + 1

        ' If Comm_Get_Byte_Count >= 5 Then   

        For temp_b = 2 To 8
            temp_j = temp_j + Comm_Get_Ar(temp_b)
        Next temp_b

        temp_j = temp_j Mod 256             '校验码出错
        If (temp_j \ 16 <> Comm_Get_Ar(9)) Or (temp_j Mod 16 <> Comm_Get_Ar(10)) Then
            Exit Sub
        End If
        Fesn(Comm_Loop_id - 1).Comm_State = Tcq.Comm_State_Enum.Comm_OK
        Fesn(Comm_Loop_id - 1).Comm_Fail_Count = 0
        Fesn(Comm_Loop_id - 1).IL = Cal_IL(Comm_Get_Ar(2), Comm_Get_Ar(3))
        Fesn(Comm_Loop_id - 1).T1 = Cal_T(Comm_Get_Ar(4), Comm_Get_Ar(5))
        Fesn(Comm_Loop_id - 1).T2 = Cal_T(Comm_Get_Ar(6), Comm_Get_Ar(7))
        ' End If
    End Sub

    Private Function Cal_IL(ByVal High_Byte As Byte, ByVal Low_byte As Byte) As Single
        Dim gSampleIL As Single
        gSampleIL = (High_Byte \ 16) * 100
        gSampleIL = gSampleIL + (High_Byte Mod 16) * 10

        gSampleIL = gSampleIL + Low_byte \ 16

        gSampleIL = gSampleIL + (Low_byte Mod 16) / 10

        Return gSampleIL
    End Function

    Private Function Cal_T(ByVal High_Byte As Byte, ByVal Low_byte As Byte) As Single
        Dim b As Byte
        Dim gSampleT As Single
        If High_Byte < &HA0 Then
            b = High_Byte
        Else
            b = High_Byte - &HA0
        End If
        gSampleT = (b \ 16) * 100 + (b Mod 16) * 10 _
                   + Low_byte \ 16 + (Low_byte Mod 16) / 10
        If High_Byte >= &HA0 Then
            gSampleT = (-1) * gSampleT
        End If
        Return gSampleT
    End Function

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_sys_info.Click
        ''发送一次通讯给探测器
        'Dim send_tcq_ar(1) As Byte  '主机查询指令
        'send_tcq_ar(0) = &HF7
        'send_tcq_ar(1) = 1
        'If send_tcq_ar(1) > 100 Then
        '    send_tcq_ar(1) = 1
        '    Comm_Loop_id = 0
        'End If
        'Tcq_port.Write(send_tcq_ar, 0, 2)  '将数据发送出去。
        'Comm_Get_Byte_Count = 0
        Dim gz1 As Guzhang_info

        Dim i_t As UShort
        Label7.Text = ""
        For i_t = 0 To Guzhang_Array.Count - 1
            gz1 = Guzhang_Array.Item(i_t)
            Label7.Text = Label7.Text & gz1.tcq_id & " ,"
        Next


    End Sub

    Private Sub But_Map_View_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Map_View.Click
        Form_Map_View.Show()
        But_Map_View.Enabled = False
        But_Table_View.Enabled = True
    End Sub

    Private Sub But_Table_View_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Table_View.Click
        Form1.Visible = True
        Form_Map_View.Visible = False
        But_Map_View.Enabled = True
        But_Table_View.Enabled = False
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
       Refresh_Guzhang_Message
    End Sub

 
End Class