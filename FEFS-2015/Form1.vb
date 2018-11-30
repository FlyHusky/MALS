''' <summary>
''' 本窗体为实时的数据显示，采用列表分页显示。
''' </summary>
''' <remarks></remarks>
Public Class Form1

    ''' <summary>
    ''' 定义20个页面选择按钮。0-19
    ''' </summary>
    ''' <remarks></remarks>
    Private But_Pages(20) As Page_Button

    Private timer2_res_ct As UShort


    ''' <summary>
    '''  设置每个页面要显示的数量。。。
    ''' </summary>
    ''' <remarks></remarks>
    Public Page_Tcq_Ct As Integer


    ''' <summary>
    ''' 探测器实时 的小窗口的值。。。
    ''' </summary>
    ''' <remarks></remarks>
    Public Tcq_Id_SS As UShort

    Private WithEvents ss As Page_Button

    Private t1_ct As UShort

    ''' <summary>
    ''' 页面显示选择按钮的总数量
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Pages_Total As Byte

    Private Timer1_Ct As Byte

    ''' <summary>
    ''' 当前页面的Page
    ''' </summary>
    ''' <remarks></remarks>
    Private Cur_Page As Byte

    ''' <summary>
    '''  Form1窗体界面设计函数。
    '''  动态依据每页要显示的探测器数量，和探测器总数量，来确定要显示的页数。
    '''  如果只需要一页显示，则不显示页面选择框。单页最多显示32个节点。
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Form1_UI_Init()

        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.Top = Main.Panel4.Top
        Me.Left = Main.Panel4.Left
        Me.Width = Main.Panel4.Width
        Me.Height = Main.Panel4.Height

        '1：软件检验系统的节点数。
        'If RenZhen Then
        '    If Sys_node_count >= 64 Then
        '        Sys_node_count = 64  '如果是认证的软件,则要求节点数量，不大于64个。
        '    End If
        'End If

        '如果节点数量小于等于40个，则每页显示20个,其它数量则显示25个，对于认证的软件，每页显示32个。
        If Sys_node_count < 41 Then
            Page_Tcq_Ct = 20
        Else
            Page_Tcq_Ct = 25
            If RenZhen Then
                Page_Tcq_Ct = 32
            End If
        End If


        '2,首先判断需要页面的数量。
        Dim page_total As Integer   '\ 这个符号表示除法，不四舍五入
        Dim iii As Integer

        If RenZhen Then  '如果是认证软件，则页面数量一直为1.
            page_total = 1
        Else
            iii = Page_Tcq_Ct * 2
            page_total = Sys_node_count \ iii
            If Sys_node_count Mod iii > 0 Then
                page_total = page_total + 1
            End If
        End If

        If page_total > 1 Then
            more_page_init()
            DataGridView1.Top = GroupBox1.Top + GroupBox1.Height + 1
            DataGridView1.Height = Me.Height - 2 - DataGridView1.Top
            DataGridView2.Top = DataGridView1.Top
            DataGridView2.Height = DataGridView1.Height
        Else
            GroupBox1.Enabled = False
            GroupBox1.Visible = False
            DataGridView1.Top = 1
            DataGridView1.Height = Me.Height - 2 - DataGridView1.Top
            DataGridView2.Top = DataGridView1.Top
            DataGridView2.Height = DataGridView1.Height
        End If

        DataGridView1.Width = (Me.Width - 6) / 2
        DataGridView2.Width = DataGridView1.Width

        DataGridView1.Left = 2
        DataGridView2.Left = DataGridView1.Width + 4

        DataGridView1.Columns(0).Width = DataGridView1.Width * 0.15
        DataGridView1.Columns(1).Width = DataGridView1.Width * 0.25
        DataGridView1.Columns(2).Width = DataGridView1.Width * 0.16
        DataGridView1.Columns(3).Width = DataGridView1.Width * 0.14
        DataGridView1.Columns(4).Width = DataGridView1.Width * 0.14
        DataGridView1.Columns(5).Width = DataGridView1.Width * 0.16

        DataGridView2.Columns(0).Width = DataGridView1.Width * 0.15
        DataGridView2.Columns(1).Width = DataGridView1.Width * 0.25
        DataGridView2.Columns(2).Width = DataGridView1.Width * 0.16
        DataGridView2.Columns(3).Width = DataGridView1.Width * 0.14
        DataGridView2.Columns(4).Width = DataGridView1.Width * 0.14
        DataGridView2.Columns(5).Width = DataGridView1.Width * 0.16

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

        Dim temp_b As Byte
        Dim row_height As UShort

        row_height = DataGridView1.Height - DataGridView1.ColumnHeadersHeight
        row_height = row_height \ Page_Tcq_Ct  '\ 这个符号表示除法，取商

        For temp_b = 1 To Page_Tcq_Ct
            DataGridView1.Rows.Add()
            DataGridView2.Rows.Add()
            DataGridView1.Rows(temp_b - 1).Height = row_height
            DataGridView2.Rows(temp_b - 1).Height = row_height
        Next

        Panel1.Visible = False
        Panel1.Top = DataGridView1.Top + (DataGridView1.Height - Panel1.Height) / 2
        Panel1.Left = DataGridView1.Left + (DataGridView1.Width - Panel1.Width) / 2

        DataGridView1.Font = New Font("宋体", 11)
        DataGridView1.Columns(0).HeaderText = "编号"
        DataGridView1.Columns(1).HeaderText = "位置"
        DataGridView1.Columns(2).HeaderText = "当前状态"
        DataGridView1.Columns(3).HeaderText = "音调"
        DataGridView1.Columns(4).HeaderText = "音量"
        DataGridView2.Font = New Font("宋体", 11)
        DataGridView2.Columns(0).HeaderText = "编号"
        DataGridView2.Columns(1).HeaderText = "位置"
        DataGridView2.Columns(2).HeaderText = "当前状态"
        DataGridView2.Columns(3).HeaderText = "音调"
        DataGridView2.Columns(4).HeaderText = "音量"
        'DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.MultiSelect = False
        DataGridView2.MultiSelect = False

    End Sub


    ''' <summary>
    ''' 当节点数量大于64的时间，设置分页显示。
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub more_page_init()

        Dim fl_i_b As Byte

        '根据探测器节点数数量，设置选着界面按钮的数量。
        '共计20个页面选择按钮，每页50个探测器，共计1000个探测器。
        Dim btop As Integer  '按键top

        GroupBox1.Location = New Point(2, 4)
        GroupBox1.Height = 35
        GroupBox1.Width = Me.Width


        btop = (GroupBox1.Height - 15) / 2   '
        '确定 停止翻页 按钮框的位置。
        Button1.Top = btop
        Button1.Left = GroupBox1.Width - Button1.Width - 15


        For fl_i_b = 0 To 19
            Dim bp As New Page_Button
            Dim str As String
            Dim page_num As UShort


            bp.Page_Num = fl_i_b  '页面按键的Page_Num值

            page_num = fl_i_b * 50 + 1
            str = page_num.ToString & "-"
            page_num = page_num + 49
            str = str & page_num.ToString  '按键显示text


            If fl_i_b = 0 Then
                bp.Location = New Point(55, btop)
            Else
                page_num = But_Pages(fl_i_b - 1).Left + But_Pages(fl_i_b - 1).Width + 10
                bp.Location = New Point(page_num, btop)
            End If


            bp.Text = str
            bp.Width = 55
            bp.Height = 23

            bp.Font = New Font("宋体", 8)

            '给页面按钮对象的Click_me，添加事件句柄
            AddHandler bp.Click_me, AddressOf Page_Button_Click

            bp.Visible = False

            But_Pages(fl_i_b) = bp
            But_Pages(fl_i_b).Enabled = False
            GroupBox1.Controls.Add(But_Pages(fl_i_b))
        Next


        Dim i_temp As Byte
        Dim k_temp As Byte

        k_temp = Sys_node_count Mod 50 '取到探测器除50后的余数。
        i_temp = Sys_node_count \ 50   '取到探测器总数除以每页50个的整数。

        If k_temp > 0 Then             '计算页面数量，
            Pages_Total = i_temp + 1
        Else
            Pages_Total = i_temp
        End If

        If Pages_Total >= 1 Then       '显示对于页面数量的 页面选择按钮
            For i_temp = 0 To Pages_Total - 1
                But_Pages(i_temp).Visible = True
                But_Pages(i_temp).Enabled = True
            Next
        End If
    End Sub
 


    Public Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '此处人工赋值，每页显示32个节点。
        '电脑比较适合每页显示20个节点，但是认证的时候，可能会设置为64个节点显示。
        '因此如果节点数量小于等于40个，那么，每页就显示20个，如果小于等于64个，则动态随记分配。
        Form1_UI_Init()
        t1_ct = 0
        Timer1_Ct = 0
        Cur_Page = 0  '从第0页开始显示
        If Pages_Total > 1 Then
            But_Pages(0).Enabled = False
        End If

        Refresh_DataGrid_View()
        Timer1.Interval = 1000
        Timer1.Enabled = True

    End Sub


    ''' <summary>
    '''  定时器功能
    ''' 1：定时翻页
    ''' 2：定时刷新  没间隔3秒
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        t1_ct = t1_ct + 1

        If Main.Main_Need_Refresh_Form1 = True Then  '需要时刷新
            Refresh_DataGrid_View()
            Main.Main_Need_Refresh_Form1 = False
            t1_ct = 0
        End If

        If t1_ct >= 8 Then   '定时刷新


            If Button1.Text = "停止翻页" And Pages_Total > 1 Then

                But_Pages(Cur_Page).Enabled = True
                But_Pages(Cur_Page).BackColor = Color.SkyBlue
                But_Pages(Cur_Page).ForeColor = Color.Blue

                Cur_Page = Cur_Page + 1
                If Cur_Page >= Pages_Total Then
                    Cur_Page = 0
                End If

                But_Pages(Cur_Page).Enabled = False
                But_Pages(Cur_Page).BackColor = Color.White
                But_Pages(Cur_Page).ForeColor = Color.Blue
            End If

            t1_ct = 0
            Refresh_DataGrid_View()

        ElseIf t1_ct Mod 3 = 0 Then   '每3秒刷新。

            Refresh_DataGrid_View()

        End If
    End Sub


    ''' <summary>
    ''' 刷新数据表格
    ''' 依据当前的Cur_Page 和 Sys_Node_Count 值来刷新显示数据
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Refresh_DataGrid_View()

        '首先判断当前页面显示探测器编号范围。
        'page0---1----50
        'page1---51--100
        'page2---101--150
        'page3---151--198  

        Dim tcq_id_start As UShort
        Dim tcq_id_end As UShort
        Dim tcq_id_loop_rf As UShort


        DataGridView1.ClearSelection()
        DataGridView2.ClearSelection()



        If Fesn(Sys_node_count - 1).Comm_State <> Tcq.Comm_State_Enum.Comm_Wait Then
            Sys_Self_Check_Ready = True
        End If

        tcq_id_start = Cur_Page * Page_Tcq_Ct * 2
        tcq_id_end = (Cur_Page + 1) * Page_Tcq_Ct * 2

        If tcq_id_end > Sys_node_count Then
            tcq_id_end = Sys_node_count
        End If

        tcq_id_loop_rf = tcq_id_start  '起始开始   0-49 ...

        Dim temp_b As Integer
        Dim temp_end As Integer

        temp_end = Page_Tcq_Ct - 1

        For temp_b = 0 To temp_end
            DataGridView1.Rows(temp_b).Cells(0).Value = ""
            DataGridView1.Rows(temp_b).Cells(1).Value = ""
            DataGridView1.Rows(temp_b).Cells(2).Value = ""
            DataGridView1.Rows(temp_b).Cells(3).Value = ""
            DataGridView1.Rows(temp_b).Cells(4).Value = ""
            DataGridView1.Rows(temp_b).Cells(5).Value = ""

            DataGridView2.Rows(temp_b).Cells(0).Value = ""
            DataGridView2.Rows(temp_b).Cells(1).Value = ""
            DataGridView2.Rows(temp_b).Cells(2).Value = ""
            DataGridView2.Rows(temp_b).Cells(3).Value = ""
            DataGridView2.Rows(temp_b).Cells(4).Value = ""
            DataGridView2.Rows(temp_b).Cells(5).Value = ""
        Next


        For temp_b = 0 To temp_end

            If Fesn(tcq_id_loop_rf).alarm Then
                DataGridView1.Rows(temp_b).Cells(0).Style.ForeColor = Color.Red
                DataGridView1.Rows(temp_b).Cells(1).Style.ForeColor = Color.Red
                DataGridView1.Rows(temp_b).Cells(2).Value = "报警打开"
            Else
                DataGridView1.Rows(temp_b).Cells(0).Style.ForeColor = Color.Black
                DataGridView1.Rows(temp_b).Cells(1).Style.ForeColor = Color.Black
                DataGridView1.Rows(temp_b).Cells(2).Style.ForeColor = Color.Black
                DataGridView1.Rows(temp_b).Cells(3).Style.ForeColor = Color.Black
                DataGridView1.Rows(temp_b).Cells(4).Style.ForeColor = Color.Black
                DataGridView1.Rows(temp_b).Cells(2).Value = "报警关闭"
            End If

            DataGridView1.Rows(temp_b).Cells(0).Value = Fesn(tcq_id_loop_rf).id_str
            DataGridView1.Rows(temp_b).Cells(1).Value = Fesn(tcq_id_loop_rf).name

            If Fesn(tcq_id_loop_rf).il_alarm_pop Then
                DataGridView1.Rows(temp_b).Cells(2).Style.ForeColor = Color.Red
            Else

            End If
 
            DataGridView1.Rows(temp_b).Cells(3).Value = Fesn(tcq_id_loop_rf).Data1
            DataGridView1.Rows(temp_b).Cells(4).Value = Fesn(tcq_id_loop_rf).Data2
            

            If Fesn(tcq_id_loop_rf).Comm_State = Tcq.Comm_State_Enum.Comm_OK Then
                DataGridView1.Rows(temp_b).Cells(5).Value = "已连接"
                DataGridView1.Rows(temp_b).Cells(5).Style.ForeColor = Color.Blue

            ElseIf Fesn(tcq_id_loop_rf).Comm_State = Tcq.Comm_State_Enum.Comm_Fail Then
                DataGridView1.Rows(temp_b).Cells(5).Value = "未连接"
                DataGridView1.Rows(temp_b).Cells(5).Style.ForeColor = Color.Red

            ElseIf Fesn(tcq_id_loop_rf).Comm_State = Tcq.Comm_State_Enum.Comm_Wait Then
                DataGridView1.Rows(temp_b).Cells(5).Value = "等待中"
                DataGridView1.Rows(temp_b).Cells(5).Style.ForeColor = Color.Blue
            Else
                DataGridView1.Rows(temp_b).Cells(5).Value = "校验错误"
                DataGridView1.Rows(temp_b).Cells(5).Style.ForeColor = Color.Red
            End If

            '通讯故障-分校验错误和非校验错误
            If Fesn(tcq_id_loop_rf).comm_Fail_Sta Then

                If Fesn(tcq_id_loop_rf).Comm_State = Tcq.Comm_State_Enum.Comm_Jy Then
                    DataGridView1.Rows(temp_b).Cells(5).Value = "校验错误"
                Else
                    DataGridView1.Rows(temp_b).Cells(5).Value = "未连接"
                End If
                DataGridView1.Rows(temp_b).Cells(5).Style.ForeColor = Color.Red
            End If


            tcq_id_loop_rf = tcq_id_loop_rf + 1
            If tcq_id_loop_rf >= tcq_id_end Then
                Exit Sub
            End If
        Next

        For temp_b = 0 To temp_end

            If Fesn(tcq_id_loop_rf).alarm Then
                DataGridView2.Rows(temp_b).Cells(0).Style.ForeColor = Color.OrangeRed
                DataGridView2.Rows(temp_b).Cells(1).Style.ForeColor = Color.OrangeRed
                DataGridView2.Rows(temp_b).Cells(2).Style.ForeColor = Color.OrangeRed
                DataGridView2.Rows(temp_b).Cells(2).Value = "报警打开"

            Else
                DataGridView2.Rows(temp_b).Cells(0).Style.ForeColor = Color.Black
                DataGridView2.Rows(temp_b).Cells(1).Style.ForeColor = Color.Black
                DataGridView2.Rows(temp_b).Cells(2).Style.ForeColor = Color.Black
                DataGridView2.Rows(temp_b).Cells(3).Style.ForeColor = Color.Black
                DataGridView2.Rows(temp_b).Cells(4).Style.ForeColor = Color.Black
                DataGridView2.Rows(temp_b).Cells(2).Value = "报警关闭"
            End If

            DataGridView2.Rows(temp_b).Cells(0).Value = Fesn(tcq_id_loop_rf).id_str
            DataGridView2.Rows(temp_b).Cells(1).Value = Fesn(tcq_id_loop_rf).name
           
            DataGridView2.Rows(temp_b).Cells(3).Value = Fesn(tcq_id_loop_rf).Data1
            DataGridView2.Rows(temp_b).Cells(4).Value = Fesn(tcq_id_loop_rf).Data2

            ''通讯状态，处理。
            'If Fesn(tcq_id_loop_rf).Comm_Fail_Count >= Sys_comm_fail_ct Then
            '    '通讯故障
            '    DataGridView2.Rows(temp_b).Cells(5).Value = "未连接"
            '    DataGridView2.Rows(temp_b).Cells(5).Style.ForeColor = Color.Red

            'Else '通讯正常

            '    DataGridView2.Rows(temp_b).Cells(5).Value = "已连接"
            '    DataGridView2.Rows(temp_b).Cells(5).Style.ForeColor = Color.Blue
            'End If


            If Fesn(tcq_id_loop_rf).Comm_State = Tcq.Comm_State_Enum.Comm_OK Then
                DataGridView2.Rows(temp_b).Cells(5).Value = "已连接"
                DataGridView2.Rows(temp_b).Cells(5).Style.ForeColor = Color.Blue

            ElseIf Fesn(tcq_id_loop_rf).Comm_State = Tcq.Comm_State_Enum.Comm_Fail Then
                DataGridView2.Rows(temp_b).Cells(5).Value = "未连接"
                DataGridView2.Rows(temp_b).Cells(5).Style.ForeColor = Color.Red

            ElseIf Fesn(tcq_id_loop_rf).Comm_State = Tcq.Comm_State_Enum.Comm_Wait Then
                DataGridView2.Rows(temp_b).Cells(5).Value = "等待中"
                DataGridView2.Rows(temp_b).Cells(5).Style.ForeColor = Color.Blue
            Else
                DataGridView2.Rows(temp_b).Cells(5).Value = "校验错误"
                DataGridView2.Rows(temp_b).Cells(5).Style.ForeColor = Color.Red
            End If

            '通讯故障-分校验错误和非校验错误
            If Fesn(tcq_id_loop_rf).comm_Fail_Sta Then

                If Fesn(tcq_id_loop_rf).Comm_State = Tcq.Comm_State_Enum.Comm_Jy Then
                    DataGridView2.Rows(temp_b).Cells(5).Value = "校验错误"
                Else
                    DataGridView2.Rows(temp_b).Cells(5).Value = "未连接"
                End If
                DataGridView2.Rows(temp_b).Cells(5).Style.ForeColor = Color.Red
            End If



            tcq_id_loop_rf = tcq_id_loop_rf + 1
            If tcq_id_loop_rf >= tcq_id_end Then
                Exit Sub
            End If
        Next


    End Sub


    ''' <summary>
    ''' 页面选择按钮
    ''' </summary>
    ''' <param name="PageNum"></param>
    ''' <remarks></remarks>
    Private Sub Page_Button_Click(ByVal PageNum As Byte)
        But_Pages(Cur_Page).Enabled = True
        But_Pages(Cur_Page).BackColor = Color.Gray
        Cur_Page = PageNum
        But_Pages(Cur_Page).Enabled = False
        But_Pages(Cur_Page).BackColor = Color.Green
        But_Pages(Cur_Page).ForeColor = Color.Blue
        Refresh_DataGrid_View()
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Button1.Text = "自动翻页" Then
            Button1.Text = "停止翻页"
        Else
            Button1.Text = "自动翻页"
        End If
    End Sub

    Private Sub DataGridView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.Click
        Main.PanDan.Visible = False
    End Sub

    '''' <summary>
    ''''  
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
    '    Dim sel_tcq As UShort
    '    Try

    '        If DataGridView1.SelectedCells(0).Value = "" Then
    '            Exit Sub
    '        End If

    '        sel_tcq = DataGridView1.SelectedRows.Item(0).Cells(0).Value
    '        If sel_tcq < 1 Or sel_tcq > Sys_node_count Then
    '            Exit Sub
    '        End If

    '        Label3.Text = DataGridView1.SelectedRows.Item(0).Cells(0).Value
    '        Label4.Text = DataGridView1.SelectedRows.Item(0).Cells(1).Value

    '        Panel1.Visible = True

    '        Label6.Text = Fesn(sel_tcq - 1).IL
    '        Label8.Text = Fesn(sel_tcq - 1).T1
    '        Label10.Text = Fesn(sel_tcq - 1).T2

    '    Catch ex As Exception

    '    End Try
    'End Sub


    ''' <summary>
    ''' 双击对应栏，显示实时的值。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim sel_tcq As UShort
        Try

            If DataGridView1.SelectedRows.Item(0).Cells(0).Value = "" Then
                Exit Sub
            End If

            sel_tcq = DataGridView1.SelectedRows.Item(0).Cells(0).Value
            If sel_tcq < 1 Or sel_tcq > Sys_node_count Then

                Exit Sub
            End If



            Main.BjqMoreFunction(sel_tcq - 1, MousePosition)





        Catch ex As Exception

        End Try
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Panel1.Visible = False
    End Sub


    ''' <summary>
    ''' 此复位按钮，仅针对于，处于报警状态的和通讯正常的 。。。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        'If Fesn(Tcq_Id_SS).Comm_State = Tcq.Comm_State_Enum.Comm_Fail and Then
        '    MsgBox("探测器通讯未连接，无法进行复位操作！")
        '    Exit Sub
        'End If
        Login_event = 7    '事件7
        Login_Level = 1    '权限至少大于等于1
        Login_Need_Level = "操作员"
        Login_Mes = "请输入'操作员'或'管理员'的密码！！！"
        LoginForm1.Show(Me)
    End Sub


    Public Sub Button3_Click()

        Tcq_Self_Check_Array.Clear()

        Dim tcq_self As New Tcq_Self_Check
        tcq_self.Tcq_id = Tcq_Id_SS + 1

        tcq_self.Self_Check_Result = 2
        Tcq_Self_Check_Array.Add(tcq_self)

        '标志自检模块使用探测器查询

        Rest_Use_Tcq_Comm = True
        Rest_Use_Tcq_Comm_OK = False
        Timer2.Interval = 1000
        Timer2.Enabled = True
        timer2_res_ct = 0

        Panel1.Visible = False
        Sys_user_level = 0

    End Sub
  


    Private Sub DataGridView2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView2.Click
        Main.PanDan.Visible = False
    End Sub

    ''' <summary>
    '''  列表框2 ，双击显示。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DataGridView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView2.DoubleClick
        Dim sel_tcq As UShort
        Try

            If DataGridView2.SelectedRows.Item(0).Cells(0).Value = "" Then
                Exit Sub
            End If

            sel_tcq = DataGridView2.SelectedRows.Item(0).Cells(0).Value
            If sel_tcq < 1 Or sel_tcq > Sys_node_count Then

                Exit Sub
            End If

            Main.BjqMoreFunction(sel_tcq - 1, MousePosition)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick

        If Rest_Use_Tcq_Comm = False Then
            Try
                Tcq_Reset_BJ_GZ()
                Main.Guzhang_Refresh_Lable = True
                Main.Jiankong_Refresh_Lable = True
            Catch ex As Exception

            End Try
            Timer2.Enabled = False
            Exit Sub
        End If

        timer2_res_ct = timer2_res_ct + 1

        If timer2_res_ct >= 100 Then
            Timer2.Enabled = False
            Exit Sub
        End If

    End Sub

    
End Class


''' <summary>
''' 自定义"页面按钮",继承于"Button"类，
''' </summary>
''' <remarks></remarks>
Public Class Page_Button
    Inherits Button
    Public Page_Num As Byte


    Event Click_me(ByVal Page_Num As Byte)

    Private Sub B_Click() Handles Me.Click
        RaiseEvent Click_me(Page_Num)
    End Sub

End Class
