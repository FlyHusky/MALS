Option Strict Off
Imports MALS.SUSI
Imports System.IO.Ports
Imports System.IO
Imports System.Threading



Public Class Main

    Private Declare Function ExitWindowsEx Lib "user32" (ByVal uFlags As Integer, ByVal dwReserved As Integer) As Integer
    Const EWX_FORCE As Short = 4
    Const EWX_LOGOFF As Short = 0
    Const EWX_REBOOT As Short = 2
    Const EWX_SHUTDOWN As Short = 1

    Private Window_Width As Integer
    Private Window_Height As Integer

    Private Speaker_Ct As UShort

    'Public sss As Thread

    Public TX_Port As SerialPort

    Public TX_Port_State As Boolean


    ''' <summary>
    ''' 报警器通讯正常计数
    ''' </summary>
    ''' <remarks></remarks>
    Private BjqCommCt As Integer

    ''' <summary>
    ''' 报警回响数据：0-功能吗  1-数据位1   2-数据位2
    ''' </summary>
    ''' <remarks></remarks>
    Private BjqReechoData(3) As Byte

    ''' <summary>
    ''' 主程序循环查询暂停=true
    ''' 暂停的目的是用于让出资源给单机通讯调试使用。
    ''' </summary>
    ''' <remarks></remarks>
    Public Main_Chaxun_Loop_Wait As Boolean

    ''' <summary>
    ''' 单机通讯调试模块，自动查询启动=true
    ''' </summary>
    ''' <remarks></remarks>
    Private BjqCommChaxunLoop As Boolean

    ''' <summary>
    ''' 单机通讯调试模块，自动查询, 标识等待报警器通讯回响
    ''' </summary>
    ''' <remarks></remarks>
    Private BjqCommChaxunLoop_Wait_Reecho As Boolean

    ''' <summary>
    ''' true=暂停普通查询
    ''' 让出串口通讯资源，用来设置地址。
    ''' </summary>
    ''' <remarks></remarks>
    Private BJQCommChaxunWait As Boolean

    ''' <summary>
    ''' 主机发送地址设置-等待回响
    ''' </summary>
    ''' <remarks></remarks>
    Private PcSendSetAddrReechoWait As Boolean


    ''' <summary>
    ''' 系统中，处于报警状态的报警器数量
    ''' </summary>
    ''' <remarks></remarks>
    Private BjqAlarmCount As Integer


    ''' <summary>
    ''' 系统中，处于故障(通讯故障)的报警器数量
    ''' </summary>
    ''' <remarks></remarks>
    Private BjqFaultCount As Integer

    ''' <summary>
    ''' 标记地图显示，需要刷新。
    ''' </summary>
    ''' <remarks></remarks>
    Public Form_Bjd_Need_Refresh As Boolean

    ''' <summary>
    ''' 通讯监视数据流标识
    ''' </summary>
    ''' <remarks></remarks>
    Private CommDataWatch As Boolean

    ''' <summary>
    ''' 通讯监视数据流，指定的地址
    ''' 0=所有的地址
    ''' </summary>
    ''' <remarks></remarks>
    Private CommDataWatchAddr As Integer
 




    ''' <summary>
    ''' 界面时间和信息窗体显示刷新间隔（100ms计时）
    ''' </summary>
    ''' <remarks></remarks>
    Public Time_Message_Fresh_Count As Byte

    ''' <summary>
    ''' 系统关闭报警 标志 1=关闭 0=打开。
    ''' </summary>
    ''' <remarks></remarks>
    Private Mute_Flag As Byte


    ''' <summary>
    ''' 定时器1,60ms定时计数。
    ''' </summary>
    ''' <remarks></remarks>
    Public Timer1_60ms_Ct As Byte


    ''' <summary>
    ''' 定时器1，定时周期个数
    ''' </summary>
    ''' <remarks></remarks>
    Public Timer1_Tcq_Per As Byte


    ''' <summary>
    ''' timer3定时器250ms 时刻标志
    ''' </summary>
    ''' <remarks></remarks>
    Dim timer3_sj As Boolean

    ''' <summary>
    ''' timer3功能类型。1：主电源工作灯闪烁
    ''' </summary>
    ''' <remarks></remarks>
    Dim timer3_kind As Byte

    ''' <summary>
    ''' timer3定时器250ms定时计数
    ''' </summary>
    ''' <remarks></remarks>
    Public timer3_ct As UInteger


    ''' <summary>
    ''' 系统累计计时，每2s和工控机电源通讯连接一次。。
    ''' </summary>
    ''' <remarks></remarks>
    Public Atx_Power_Comm_Count As Byte

    ''' <summary>
    '''  标志等待工控机电源通讯上传。。。。。
    ''' </summary>
    ''' <remarks></remarks>
    Public Wait_ATX_ReCall As Boolean


    '--------------------与报警器相关的--------------------

    ''' <summary>
    ''' 标识等待 终端节点 查询指令的 回响
    ''' </summary>
    ''' <remarks></remarks>
    Private WaitNodeChaxunReecho As Boolean


    ''' <summary>
    ''' 标识等待 PC发送报警器打开后，报警器的回响
    ''' </summary>
    ''' <remarks></remarks>
    Private WaitNodeAlarmOnReecho As Boolean

    ''' <summary>
    ''' 标识等待 PC发送报警器关闭后，报警器的回响
    ''' </summary>
    ''' <remarks></remarks>
    Private WaitNodeAlarmOffReecho As Boolean

    '-------------------------------------------------------

    ''' <summary>
    '''  参数设置按键展开和折叠标志
    ''' </summary>
    ''' <remarks></remarks>
    Private Bo_But_Set_Tree_Opne As Boolean

    ''' <summary>
    ''' 实时数据按钮展开和折叠标志
    ''' </summary>
    ''' <remarks></remarks>
    Private Bo_But_Data_Tree_Open As Boolean

    ''' <summary>
    ''' 参数设置按键展开后，其他按键下移此次
    ''' </summary>
    ''' <remarks></remarks>
    Private Us_But_Down_Size As UShort

    ''' <summary>
    ''' 标志进入接收中断后，要接收到通讯帧的第一个数据。。。
    ''' </summary>
    ''' <remarks></remarks>
    Private Comm_Get_First_Byte As Boolean

    ''' <summary>
    ''' 接收探测器发过来的数据，长度20
    ''' </summary>
    ''' <remarks></remarks>
    Private Comm_Get_Ar(20) As Byte

    ''' <summary>
    ''' 故障报警显示框刷新标志
    ''' </summary>
    ''' <remarks></remarks>
    Public Guzhang_Refresh_Lable As Boolean

    ''' <summary>
    ''' 监控报警显示框刷新标志
    ''' </summary>
    ''' <remarks></remarks>
    Public Jiankong_Refresh_Lable As Boolean


    ''' <summary>
    ''' 当有报警和故障产生时，main 需要form1 及时刷新
    ''' </summary>
    ''' <remarks></remarks>
    Public Main_Need_Refresh_Form1 As Boolean


    ''' <summary>
    ''' 当前显示的子窗体的编号
    ''' </summary>
    ''' <remarks></remarks>
    Private Cur_Form As Cur_Form_Enum

    ''' <summary>
    ''' 当前子窗体标志枚举类型
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum Cur_Form_Enum

        Data_Table_View_Form = 1   ' 以表格的方式显示，1,2互斥
        Data_map_view_form = 2     ' 地图信息显示窗体， 


        His_Baojin_Form = 3        '报警历史记录窗体，需关闭

        Tcq_Pra_Set_Form = 4       '探测器参数设置窗体，需关闭

        Sys_Pra_Set_Form = 5       '系统参数设置窗体，需关闭

        Sys_SelfCheck_Form = 6     '系统自检窗体，需关闭

        Sys_Weihu_Form = 7         '系统维护界面-现在定义为系统的使用帮助界面

        Sys_Reset_Form = 8

        Sys_Net_Form = 9

        Sys_Fault_Form = 10

    End Enum

    Private main_power_fault_count As Byte

    Private backup_power_fault_count As Byte

    ''' <summary>
    '''  手动查询故障信息索引
    ''' </summary>
    ''' <remarks></remarks>
    Private Man_Find_GZ_Index As UShort

    ''' <summary>
    '''  手动查询报警信息索引
    ''' </summary>
    ''' <remarks></remarks>
    Private Man_Find_BJ_Index As UShort


    '----------------------------------自检模块相关变量--start---------------------------------
    ''' <summary>
    ''' 自检模块，标志等待探测器回响自检命令
    ''' </summary>
    ''' <remarks></remarks>
    Private Wait_Recall_Self_Check As Boolean

    ''' <summary>
    ''' 自检模块Loop_Index
    ''' </summary>
    ''' <remarks></remarks>
    Private Self_Check_Loop_Index As UShort

    ''' <summary>
    ''' 需要自检的探测器--地址数组
    ''' </summary>
    ''' <remarks></remarks>
    Public Self_Check_Tcq() As UShort

    ''' <summary>
    ''' 需要自检的探测器--自检结果数组
    ''' 自检完成=true   自检失败=false
    ''' </summary>
    ''' <remarks></remarks>
    Public Self_Check_Result() As Boolean

    ''' <summary>
    ''' 需要自检的探测器--自检失败重复累计
    ''' </summary>
    ''' <remarks></remarks>
    Public Self_Check_Retry As Byte
    '----------------------------------自检模块相关变量--end---------------------------------

    ' Public odbc_con1 As Odbc.OdbcConnection

    ' Public ocm1 As New OdbcCommand


    '-----------------------多线程 - 图形显示装置用变量-------------------

    ''' <summary>
    ''' true=需要发送数据给图形显示装置。
    ''' 主程序告知进程，需要发送数
    ''' </summary>
    ''' <remarks></remarks>
    Public TX_Send_Work As Boolean


    ''' <summary>
    ''' true=进程已经将信息发送给图形显示装置。
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public TX_Send_OK As Boolean

    ''' <summary>
    ''' 标志等待图显回响 。
    ''' </summary>
    ''' <remarks></remarks>
    Public Wait_Tx_Recall As Boolean

    ''' <summary>
    ''' 重发次数。
    ''' </summary>
    ''' <remarks></remarks>
    Public TX_TX_Retry As Byte


    ''' <summary>
    '''  待发送的信息对象
    ''' </summary>
    ''' <remarks></remarks>
    Public TX_Send_Obj As TX_Send_Info


    ''' <summary>
    ''' 待发送信息集合
    ''' </summary>
    ''' <remarks></remarks>
    Public TX_Array As ArrayList

    ''' <summary>
    ''' 图显序列号
    ''' </summary>
    ''' <remarks></remarks>
    Public TX_Info_Id As UInteger

    ''' <summary>
    ''' 图显通讯计时，每两秒发送一次
    ''' </summary>
    ''' <remarks></remarks>
    Public TX_Comm_Ct As UShort


    ''' <summary>
    ''' 复位图显标志
    ''' </summary>
    ''' <remarks></remarks>
    Public TX_RST As Boolean

    Private WillClose As Boolean




    Private Sub Close_Form()
        Try

            If PanDan.Visible Then
                PanDan.Visible = False
            End If

            ' Cur_Form = Cur_Form_Enum.Data_Table_View_Form

            Select Case Cur_Form

                Case Cur_Form_Enum.Data_Table_View_Form

                    Form1.Close()


                Case Cur_Form_Enum.Data_map_view_form
                    Form_Bjd.Close()
                    'Panel3.Height = (Panel6.Top - But_dh.Top + Panel6.Height) * 0.05

                    'If Panel3.Height < 32 Then
                    '    Panel3.Height = 32
                    'End If
                    'Panel2.Height = Panel3.Height
                    'Panel3.Top = Panel6.Height - Panel3.Height + Panel6.Top
                    'Panel2.Top = Panel3.Top

                Case Cur_Form_Enum.His_Baojin_Form
                    FormAlarmFault.Close()
                    PanXX.Visible = True
                    Button9.Enabled = True
                    Button10.Enabled = True

                Case Cur_Form_Enum.Sys_Pra_Set_Form
                    Admin.Close()

                    '原自检窗体，在MALS中改为了单机调试
                Case Cur_Form_Enum.Sys_SelfCheck_Form
                    'Form_SelfCheck.Close()
                    Panel4.Visible = False
                    PanDan.Visible = False

                Case Cur_Form_Enum.Tcq_Pra_Set_Form
                    Form_Set_Tcq.Close()

                    '改为通讯监视，显示main的groupbox8.
                Case Cur_Form_Enum.Sys_Net_Form
                    If CommDataWatch Then
                        CommDataWatch = False
                        Button31.Enabled = True
                        Button30.Enabled = True
                        RichTextRec.Text = ""
                        RichTextSend.Text = ""
                    End If
                    GroupBox8.Visible = False

                Case Cur_Form_Enum.Sys_Reset_Form
                    Form_Restar.Close()
            End Select

        Catch ex As Exception

        End Try


    End Sub

    Private Sub Main_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        If WillClose = False Then

            WillClose = True

            e.Cancel = True

            If MessageBox.Show("是否确定退出系统操作？", "确定", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Try

                    Sys_Close()

                    If Cur_Form = Cur_Form_Enum.Data_map_view_form Then
                        Form_Bjd.DisposeBJD()
                        Form_Bjd.Close()
                    End If

                    Application.Exit()
                Catch ex As Exception
                End Try
            Else
                WillClose = False
            End If

        End If
    End Sub

    ''' <summary>
    ''' 软件主窗体，初始化函数。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MDIParent2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'step1:设置UI界面布局
        Main_UI_Init()
        Main_Form_Init()

    End Sub

    ''' <summary>
    ''' 窗体显示界面布局初始化函数
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Main_UI_Init()
        '1:本窗体外观设置


        If Sys_Form_Style = 0 Then
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            Me.ControlBox = False
            Me.Width = Screen.PrimaryScreen.Bounds.Width
            Me.Height = Screen.PrimaryScreen.Bounds.Height
            Me.WindowState = FormWindowState.Maximized

        Else
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Fixed3D
            Me.WindowState = FormWindowState.Maximized
            Me.ControlBox = True
            Me.MaximizeBox = True
            Me.MinimizeBox = True


        End If


        ba_v = 0

        '标题栏设置
        Panel1.Top = 0
        Panel1.Left = 0
        Panel1.Width = Me.Width
        Panel1.Height = Me.Height * 0.09


        If Sys_Company_Id = 1 Then
            Panel1.BackgroundImage = My.Resources.company1
        ElseIf Sys_Company_Id = 2 Then
            Panel1.BackgroundImage = My.Resources.company2
        End If






        'Button3.Top = (Panel1.Height - Button3.Height - 3 - Label4.Height) \ 2
        'Label4.Top = Button3.Top + Button3.Height + 3
        'PicAllOn.Top = Button3.Top
        'PicAllOn.Left = Button3.Left - (Button4.Left - Button3.Left)
        'Label5.Top = Label4.Top
        'Label5.Left = PicAllOn.Left - 12
        'PicAllOff.Top = Button3.Top
        'PicAllOff.Left = PicAllOn.Left - (Button4.Left - Button3.Left)
        'Label7.Top = Label4.Top
        'Label7.Left = PicAllOff.Left - 12
        'Button4.Top = Button3.Top
        'Label3.Top = Label4.Top



        '导航栏设置
        Panel5.Left = Me.Width * 0.013
        Panel5.Top = Panel1.Height * 1.85
        Panel5.Height = Me.Height * 0.58
        Label3.Left = Button4.Left

        '如果显示器的分辨率 宽度小于1152，则缩小导航栏宽度。
        If Me.Width < 1100 Then
            Panel5.Width = Me.Width * 0.12
        Else
            Panel5.Width = Me.Width * 0.13
        End If

        '导航栏头设置
        But_dh.Top = Panel1.Height * 1.3
        But_dh.Width = Panel5.Width
        But_dh.Left = Panel5.Left


        '系统信息栏设置
        Panel6.Top = Panel5.Top + Panel5.Height + 5
        Panel6.Left = Panel5.Left

        Panel6.Height = Me.Height - Panel6.Top - Panel6.Left

        If Sys_Form_Style <> 0 Then
            Panel6.Height = Panel6.Height - 20
        End If

        Panel6.Width = Panel5.Width

        If Panel6.Width * 0.75 > Panel9.Width * 1.4 Then
            Panel9.Width = Panel9.Width * 1.4
        End If
        Panel9.Left = (Panel6.Width - Panel9.Width) \ 2

        '---------------------------------------------------------------------------------
        '设置panel4的尺寸，其尺寸同时也是各个子窗体的尺寸，非常重要
        Panel4.Left = Panel5.Left + Panel5.Width + 5
        Panel4.Width = Me.Width - Panel4.Left - Panel5.Left
        PanXX.Left = Panel4.Left
        PanXX.Width = Panel4.Width
        PanXX.Height = (Panel6.Top - But_dh.Top + Panel6.Height) * ((0.05 * Me.Width) / 768)

        If PanXX.Height < 32 Then
            PanXX.Height = 32
        End If

        If PanXX.Height > 70 Then
            PanXX.Height = 70
        End If
        PanXX.Top = Panel6.Height - PanXX.Height + Panel6.Top
        Button9.Width = (PanXX.Width - 20 - 20 - 40) \ 2
        Button10.Width = Button9.Width
        Button9.Left = 20
        Button10.Left = Button9.Width + Button9.Left + 40

        If Button9.Height < 30 Then
            Button9.Height = PanXX.Height - 6
            Button9.Top = 3

            Button10.Height = Button9.Height
            Button10.Top = 3
        End If


        'PanDan.Left = Panel5.Left + Panel5.Width + 5
        'PanDan.Width = Me.Width - PanDan.Left - Panel5.Left

        PanDanUIIinit()


        Us_But_Down_Size = Me.Height * 0.08      '1152*864 
        Dim but_height As Integer
        but_height = Panel5.Height * 0.05

        '设置6大按键的 width , height , top
        But_Data_View.Width = Panel5.Width * 0.67
        But_His_Alarm.Width = But_Data_View.Width
        But_Pra_Set.Width = But_Data_View.Width
        But_Sys_Tcq_Net.Width = But_Data_View.Width
        But_Sys_Reset.Width = But_Data_View.Width
        But_Sys_Info.Width = But_Data_View.Width
        But_Sys_SelfCheck.Width = But_Data_View.Width

        But_Data_View.Left = (Panel5.Width - But_Data_View.Width) / 2
        But_His_Alarm.Left = But_Data_View.Left
        But_Pra_Set.Left = But_Data_View.Left
        But_Sys_Tcq_Net.Left = But_Data_View.Left
        But_Sys_Reset.Left = But_Data_View.Left
        But_Sys_Info.Left = But_Data_View.Left
        But_Sys_SelfCheck.Left = But_Data_View.Left

        But_Data_View.Height = Panel5.Height * 0.06
        But_His_Alarm.Height = But_Data_View.Height
        But_Pra_Set.Height = But_Data_View.Height
        But_Sys_Tcq_Net.Height = But_Data_View.Height
        But_Sys_Reset.Height = But_Data_View.Height
        But_Sys_Info.Height = But_Data_View.Height
        But_Sys_SelfCheck.Height = But_Data_View.Height

        But_Data_View.Top = But_Data_View.Height   '距离顶部的高度为自己的高度
        But_His_Alarm.Top = But_Data_View.Top + But_Data_View.Height + but_height
        But_Pra_Set.Top = But_His_Alarm.Top + But_Data_View.Height + but_height

        But_Sys_SelfCheck.Top = But_Pra_Set.Top + But_Data_View.Height + but_height
        But_Sys_Reset.Top = But_Sys_SelfCheck.Top + But_Data_View.Height + but_height
        But_Sys_Tcq_Net.Top = But_Sys_Reset.Top + But_Data_View.Height + but_height
        But_Sys_Info.Top = But_Sys_Tcq_Net.Top + But_Data_View.Height + but_height


        '设置四个分支按键的top 
        But_Table_View.Height = But_Data_View.Height * 0.8
        But_Map_View.Height = But_Data_View.Height * 0.8

        But_Tcq_Pra_Set.Height = But_Data_View.Height * 0.8
        But_Sys_Pra_Set.Height = But_Data_View.Height * 0.8

        But_Table_View.Top = But_Data_View.Top + But_Data_View.Height + 10
        But_Map_View.Top = But_Table_View.Top + But_Table_View.Height + 15

        But_Tcq_Pra_Set.Top = But_Pra_Set.Top + But_Pra_Set.Height + 10
        But_Sys_Pra_Set.Top = But_Tcq_Pra_Set.Top + But_Tcq_Pra_Set.Height + 15

        But_Table_View.Width = But_Data_View.Width * 0.8
        But_Map_View.Width = But_Table_View.Width

        But_Sys_Pra_Set.Width = But_Table_View.Width
        But_Tcq_Pra_Set.Width = But_Table_View.Width

        But_Table_View.Left = But_Data_View.Left + But_Data_View.Width * 0.1
        But_Map_View.Left = But_Table_View.Left
        But_Table_View.Left = But_Table_View.Left

        But_Sys_Pra_Set.Left = But_Table_View.Left
        But_Tcq_Pra_Set.Left = But_Table_View.Left




        Dim i As Integer

        ComWatchAddr.Items.Add("所有")
        For i = 1 To BJQMAXADDR
            ComAddr.Items.Add(i)
            ComWatchAddr.Items.Add(i)
        Next

        ComWatchAddr.SelectedIndex = 0

        ComAddr.SelectedIndex = 0
        For i = 1 To 30
            ComCom.Items.Add(i)
        Next

        For i = 1 To &H1E
            ComVolum.Items.Add(i)
        Next

        ComVolum.SelectedIndex = 14


        For i = 1 To 12
            ComMusic.Items.Add(i)
        Next

        ComMusic.SelectedIndex = 0

    End Sub


    ''' <summary>
    ''' 单机调试时，pandan的布局处理。
    ''' pandan的尺寸，尽量不要放大，放大后不好看，而且好多控件要重新设置位置。
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub PanDanUIIinit()
        Panel4.Top = Panel1.Height + 2
        Panel4.Height = PanXX.Top - Panel4.Top - 4

        '单机调试panel
        Dim hh As Integer

        '设置单机调试panel 的宽度。 默认pandan的宽度是600，宽度最好不超过800
        hh = Panel4.Width * 0.85

        'If hh > PanDan.Width Then
        '    If hh > PanDan.Width + 200 Then
        '        PanDan.Width = PanDan.Width + 200
        '    End If
        'End If

        PanDan.Left = (Panel4.Width - PanDan.Width) \ 2 + Panel4.Left

        hh = Panel4.Height * 0.9
        If hh > PanDan.Height Then
            PanDan.Height = hh
        End If

        PanDan.Top = (Panel4.Height - PanDan.Height) \ 2 + Panel4.Top

        'hh = PanDan.Width * 0.9

        'If hh > GroupBox1.Width Then
        '    GroupBox1.Width = hh
        'End If

        'GroupBox4.Width = GroupBox1.Width
        'GroupBox5.Width = GroupBox1.Width
        'GroupBox6.Width = GroupBox1.Width

        '设置间隔
        hh = PanDan.Height - GroupBox6.Top - GroupBox6.Height - GroupBox1.Height

        If hh > 100 Then
            GroupBox1.Top = 20
            hh = hh \ 6
            GroupBox4.Top = hh + GroupBox1.Top + GroupBox1.Height
            GroupBox5.Top = hh + GroupBox4.Top + GroupBox4.Height
            GroupBox6.Top = hh + GroupBox5.Top + GroupBox5.Height
        End If
        GroupBox1.Left = (PanDan.Width - GroupBox1.Width) * 0.4
        GroupBox4.Left = GroupBox1.Left
        GroupBox5.Left = GroupBox1.Left
        GroupBox6.Left = GroupBox1.Left

        PanDan.Width = 20 + GroupBox5.Width + GroupBox5.Left

  
 

    End Sub


    ''' <summary>
    ''' Main窗体初始化，有关动态的参数。
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Main_Form_Init()

        '1:显示系统名称
        Label1.Text = Sys_name
        Me.Text = Sys_name
        Label1.Top = (Panel1.Height - Label1.Height) / 2

        Label1.Left = (Panel1.Width - Label1.Width) / 2

        '单机调试-串口选择下拉框
        ComCom.SelectedIndex = Sys_tcq_com_id - 1
        ComBtl.SelectedIndex = 1

        Bo_But_Data_Tree_Open = False
        Bo_But_Set_Tree_Opne = False

        Guzhang_Refresh_Lable = False
        Jiankong_Refresh_Lable = False
        Main_Need_Refresh_Form1 = False

        But_Table_View.Visible = False
        But_Map_View.Visible = False
        But_Tcq_Pra_Set.Visible = False
        But_Sys_Pra_Set.Visible = False

        But_Data_Tree_Open()
        But_Table_View.Enabled = False



        '软件复位初始化后，首页-图形显示。
        Form_Bjd.MdiParent = Me
        Form_Bjd.Visible = True
        Form_Bjd.Show()
        Form_Bjd.BringToFront()

        But_Data_View.Enabled = False   '菜单-父级按键
        But_Table_View.Enabled = True   '图标显示

        But_Map_View.BackColor = Color.White '当前窗体显示按钮-设置白色。
        But_Map_View.Enabled = False        '同时禁止再次点击。

        But_Table_View.BackColor = Color.PowderBlue
        Cur_Form = Cur_Form_Enum.Data_map_view_form
        Panel4.Visible = False


        Timer2.Interval = 100
        Timer2.Enabled = True
        Time_Message_Fresh_Count = 0

        Main_Chaxun_Loop_Wait = False
        BjqCommChaxunLoop = False

        BjqCommCt = 0
        BjqAlarmCount = 0
        BjqFaultCount = 0

        WillClose = False



        'Step3: 探测器通讯口初始化
        Comm_Tcq_Usart_Init()   'com1 初始化

        TX_Array = New ArrayList

        '根据系统内报警灯的数量，来动态计算出合理的上位机寻呼查询周期值。
        '通讯故障报警时间20秒。

        Timer1_Tcq_Per = 3

        If TCQ_Usart_State Then


            Timer1_Tcq_Per = 3 '3个周期


            If Sys_node_count <= 8 Then
                Timer1_Tcq_Per = 10
            End If

            If Sys_node_count <= 16 Then
                Timer1_Tcq_Per = 5
            End If


            'Step6: 开启第一次通讯查询任务
            Comm_Loop_Id = 0 '通讯循环标志变量

            '发送第一个终端的查询指令
            Send_ChaXun_Order(Fesn(Comm_Loop_Id).id, Tcq_Port)

        Else
            MsgBox("探测器通讯com" & Sys_tcq_com_id & "口，打开失败，主程序通讯监控模块功能不可用！!!!" & vbCrLf & "请检查排除串口故障或在‘参数设置’中重新设置串口参数，然后重启软件！")

            But_Data_View.Text = "com" & Sys_tcq_com_id & "打开失败"
            But_Data_View.ForeColor = Color.Red
            But_Table_View.Text = "检查或设置串口"
            But_Map_View.Text = "重启软件"
            But_Table_View.ForeColor = Color.Red
            But_Map_View.ForeColor = Color.Red
            But_Data_View.ForeColor = Color.Red


        End If


        Timer1.Interval = 100
        Timer1.Enabled = True  '开启定时器
        Timer1_60ms_Ct = 0 '60ms周期时间计数器，服务于timer1中断。


    End Sub



    ''' <summary>
    ''' 系统退出按钮
    ''' </summary> 
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

        ' If MessageBox.Show("是否确定退出系统？", "确定", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
        If Sys_Need_Pass Then
            Login_event = 6
            Login_Need_Level = User_Level_Enum.Oper
            Login_Mes = "请输入'操作员'的密码！！！"
            LoginForm1.Show(Me)
        Else
            Me.Close()
        End If
        '  End If

    End Sub
    Public Sub Button4_Click1()
        Me.Close()
    End Sub




    ''' <summary>
    ''' 关机操作
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Close_PC()
        Try
            All_Out_Off()
            System.Diagnostics.Process.Start("shutdown", "-s -t 0")
            End
        Catch ex As Exception

        End Try

    End Sub







    ''' <summary>
    ''' 实时数据显示按钮展开时，其下方的按钮下移，
    ''' 如果下方参数设置按钮为展开状态，则先收缩展开。
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub But_View_Tree_Open()
        If Bo_But_Set_Tree_Opne Then

        End If

    End Sub

    ''' <summary>
    '''  收缩已经展开的参数设置按钮树,并标志展开状态
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub But_Set_Tree_Close()
        '如果参数设置按钮树是展开状态则收缩
        If Bo_But_Set_Tree_Opne Then
            But_Tcq_Pra_Set.Visible = False
            But_Sys_Pra_Set.Visible = False
            But_Sys_Tcq_Net.Top = But_Sys_Tcq_Net.Top - Us_But_Down_Size
            But_Sys_SelfCheck.Top = But_Sys_SelfCheck.Top - Us_But_Down_Size
            But_Sys_Reset.Top = But_Sys_Reset.Top - Us_But_Down_Size
            But_Sys_Info.Top = But_Sys_Info.Top - Us_But_Down_Size
            Bo_But_Set_Tree_Opne = False
        End If
    End Sub

    ''' <summary>
    ''' 展开处于关闭状态的参数设置按钮树
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub But_Set_Tree_Open()

        '先收缩已经展开的其他树
        But_Data_Tree_Close()

        '如果参数设置按钮树是收缩状态则展开
        If Bo_But_Set_Tree_Opne = False Then
            But_Tcq_Pra_Set.Visible = True
            But_Sys_Pra_Set.Visible = True
            But_Sys_Tcq_Net.Top = But_Sys_Tcq_Net.Top + Us_But_Down_Size
            But_Sys_SelfCheck.Top = But_Sys_SelfCheck.Top + Us_But_Down_Size
            But_Sys_Reset.Top = But_Sys_Reset.Top + Us_But_Down_Size
            But_Sys_Info.Top = But_Sys_Info.Top + Us_But_Down_Size
            Bo_But_Set_Tree_Opne = True
        End If
    End Sub

    ''' <summary>
    ''' 收缩已经展开的实时数据按钮树，并标志其为收缩状态
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub But_Data_Tree_Close()
        If Bo_But_Data_Tree_Open Then
            But_Table_View.Visible = False
            But_Map_View.Visible = False
            'But_Fault_View.Visible = False

            But_His_Alarm.Top = But_His_Alarm.Top - Us_But_Down_Size '- But_Fault_View.Height
            But_Pra_Set.Top = But_Pra_Set.Top - Us_But_Down_Size ' - But_Fault_View.Height
            But_Sys_Tcq_Net.Top = But_Sys_Tcq_Net.Top - Us_But_Down_Size ' - But_Fault_View.Height
            But_Sys_SelfCheck.Top = But_Sys_SelfCheck.Top - Us_But_Down_Size ' - But_Fault_View.Height
            But_Sys_Reset.Top = But_Sys_Reset.Top - Us_But_Down_Size '- But_Fault_View.Height
            But_Sys_Info.Top = But_Sys_Info.Top - Us_But_Down_Size '- But_Fault_View.Height
            Bo_But_Data_Tree_Open = False
        End If
    End Sub

    ''' <summary>
    ''' 展开已经处于关闭状态的实时数据
    ''' 按钮树
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub But_Data_Tree_Open()
        But_Set_Tree_Close()
        If Bo_But_Data_Tree_Open = False Then
            But_Table_View.Visible = True
            But_Map_View.Visible = True
            'But_Fault_View.Visible = True

            But_His_Alarm.Top = But_His_Alarm.Top + Us_But_Down_Size '+ But_Fault_View.Height
            But_Pra_Set.Top = But_Pra_Set.Top + Us_But_Down_Size '+ But_Fault_View.Height
            But_Sys_Tcq_Net.Top = But_Sys_Tcq_Net.Top + Us_But_Down_Size '+ But_Fault_View.Height
            But_Sys_SelfCheck.Top = But_Sys_SelfCheck.Top + Us_But_Down_Size '+ But_Fault_View.Height
            But_Sys_Reset.Top = But_Sys_Reset.Top + Us_But_Down_Size '+ But_Fault_View.Height
            But_Sys_Info.Top = But_Sys_Info.Top + Us_But_Down_Size ' + But_Fault_View.Height

            Bo_But_Data_Tree_Open = True
        End If
    End Sub


    Private Sub But_Pra_Set_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If Sys_user_level < 2 Then
            MsgBox("无此操作权限！！！")
            Exit Sub
        End If

        Panel4.Visible = False
        But_Set_Tree_Open()
        But_Form_Enable_Work()
        But_Pra_Set.Enabled = False
        But_Tcq_Pra_Set.Enabled = False
        But_Sys_Pra_Set.Enabled = True
        Cur_Form = Cur_Form_Enum.Sys_Pra_Set_Form
        Form_Set_Tcq.MdiParent = Me
        Form_Set_Tcq.Show()
        Form_Set_Tcq.BringToFront()
    End Sub


    ''' <summary>
    ''' 探测器通讯口初始化
    ''' </summary>
    ''' <returns> true=1</returns>
    ''' <remarks></remarks>
    Public Function Comm_Tcq_Usart_Init() As Boolean

        Dim com_str As String

        com_str = "com" & Sys_tcq_com_id.ToString

        Try
            Tcq_Port = New SerialPort(com_str, 9600, Parity.None, 8, StopBits.One)
            Tcq_Port.ReadBufferSize = 1024

            Tcq_Port.WriteBufferSize = 1024
            Tcq_Port.ReceivedBytesThreshold = 1
            Tcq_Port.Open()
            TCQ_Usart_State = True
            Return True
        Catch ex As Exception
            TCQ_Usart_State = False

            ' MessageBox.Show(com_str & "通讯口初始化失败,请检查串口设备!")

            Return False
        End Try
        Return False
    End Function



    Public Function Comm_Tcq_Usart_Init2() As Boolean
        Dim com_str As String
        com_str = "com3"

        Try
            Tcq_Port3 = New SerialPort(com_str, 9600, Parity.None, 8, StopBits.One)
            Tcq_Port3.ReadBufferSize = 1024
            Tcq_Port3.WriteBufferSize = 1024
            Tcq_Port3.ReceivedBytesThreshold = 1
            Tcq_Port3.Open()
            Return True
        Catch ex As Exception
            MessageBox.Show(com_str & "通讯口初始化失败,请检查串口设备!")
            Return False
        End Try
        Return False
    End Function


    ''' <summary>
    ''' 打开新的串口
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsOpenPort() As Boolean

        Try

        Catch ex As Exception

        End Try


    End Function






    ''' <summary>
    '''  工控主机通讯口初始化
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Atx_Power_Usart_Init() As Boolean

        Dim com_str As String
        Select Case Sys_power_com_id
            Case 1
                com_str = "com1"
            Case 2
                com_str = "com2"
            Case 3
                com_str = "com3"
            Case 4
                com_str = "com4"
            Case 5
                com_str = "com5"
            Case 6
                com_str = "com6"
            Case Else
                com_str = "com1"
        End Select
        Try
            ATX_Port = New SerialPort(com_str, 9600, Parity.None, 8, StopBits.One)
            ATX_Port.ReadBufferSize = 1024
            ATX_Port.WriteBufferSize = 1024
            ATX_Port.Open()
            ATX200_Usart_State = True

            'str_log = Now.ToString & vbCrLf & ":探测器通讯口初始化正常！" & vbCrLf & vbCrLf
            'My.Computer.FileSystem.WriteAllText(Sys_Log_Path, str_log, True)



            Return True
        Catch ex As Exception
            ATX200_Usart_State = False
            'str_log = Now.ToString & vbCrLf & "：ATX-200通讯口初始化失败！" & vbCrLf & vbCrLf
            'My.Computer.FileSystem.WriteAllText(Sys_Log_Path, str_log, True)

            Return False
        End Try
        Return False
    End Function





    ''' <summary>
    ''' 电流值计算
    ''' </summary>
    ''' <param name="High_Byte"></param>
    ''' <param name="Low_byte"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Cal_IL(ByVal High_Byte As Byte, ByVal Low_byte As Byte) As Single
        Dim gSampleIL As Single
        gSampleIL = (High_Byte \ 16) * 100
        gSampleIL = gSampleIL + (High_Byte Mod 16) * 10

        gSampleIL = gSampleIL + Low_byte \ 16

        gSampleIL = gSampleIL + (Low_byte Mod 16) / 10

        Return gSampleIL
    End Function

    ''' <summary>
    ''' 温度值计算
    ''' </summary>
    ''' <param name="High_Byte"></param>
    ''' <param name="Low_byte"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' 刷新故障显示框,故障的 DataGridView1
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Refresh_Guzhang_Message()
        Dim rgm_i As UShort
        Dim rgm_gz As Guzhang_info

        Dim rgm_i1 As UShort
        Try
            'Guzhang_Refresh_Lable = False

            'rgm_i = DataGridView1.Rows.Count

            ''首先情况列表框。
            'While (rgm_i > 0)
            '    DataGridView1.Rows.RemoveAt(rgm_i - 1)
            '    rgm_i = DataGridView1.Rows.Count
            'End While


            Button10.Text = "当前故障信息共" & Guzhang_Array.Count & "条"

            '如果没有故障，则直接退出。
            If Guzhang_Array.Count < 1 Then
                'La_Date_GZ.Text = "---"
                'La_Time_GZ.Text = "---"
                'La_Addr_GZ.Text = "---"
                'La_Box_GZ.Text = "---"
                'La_Kind_GZ.Text = "---"
                'La_GZ_Index.Text = "当前无故障信息"
                Man_Find_GZ_Index = 0   '表示当前没有故障信息
                Guzhang_Lamp_Sta = False
                Exit Sub
            End If

            '如果有故障信息。则按照时间发送的倒序顺序。
            If Guzhang_Array.Count >= 1 Then
                For rgm_i = 0 To Guzhang_Array.Count - 1
                    rgm_i1 = Guzhang_Array.Count - rgm_i - 1
                    rgm_gz = Guzhang_Array.Item(rgm_i1)
                    'DataGridView1.SelectAll()
                    'DataGridView1.ClearSelection()
                    'DataGridView1.Rows.Add()
                    'DataGridView1.Rows(rgm_i).Cells(0).Value = rgm_i + 1
                    'DataGridView1.Rows(rgm_i).Cells(1).Value = rgm_gz.time_str
                    'DataGridView1.Rows(rgm_i).Cells(2).Value = rgm_gz.tcq_name
                    'DataGridView1.Rows(rgm_i).Cells(3).Value = rgm_gz.tcq_id_str
                    'If rgm_gz.Guzhang_kind = Guzhang_info.Guzhang_enum.Comm_Error Then
                    '    DataGridView1.Rows(rgm_i).Cells(4).Value = "通讯故障"
                    'ElseIf rgm_gz.Guzhang_kind = Guzhang_info.Guzhang_enum.IL_Error Then
                    '    DataGridView1.Rows(rgm_i).Cells(4).Value = "电流传感器故障"
                    'ElseIf rgm_gz.Guzhang_kind = Guzhang_info.Guzhang_enum.T1_Error Then
                    '    DataGridView1.Rows(rgm_i).Cells(4).Value = "温度1传感器故障"
                    'ElseIf rgm_gz.Guzhang_kind = Guzhang_info.Guzhang_enum.T2_Error Then
                    '    DataGridView1.Rows(rgm_i).Cells(4).Value = "温度2传感器故障"
                    'ElseIf rgm_gz.Guzhang_kind = Guzhang_info.Guzhang_enum.Main_Power_Error Then
                    '    DataGridView1.Rows(rgm_i).Cells(4).Value = "主电源异常"
                    'ElseIf rgm_gz.Guzhang_kind = Guzhang_info.Guzhang_enum.Backup_Power_Error Then
                    '    DataGridView1.Rows(rgm_i).Cells(4).Value = "备电源异常"
                    'End If
                Next
                ' DataGridView1.FirstDisplayedScrollingRowIndex = 0

                '认证软件，要求，故障手动查询，只能查询一个记录。
                '当故障信息集合发送改变时，将查询定位到第一个。
                If RenZhen Then

                    Man_Find_GZ_Index = 2
                    But_GZ_Find_Pre_Click1()  '初始化显示第一条数据
                End If

            End If




        Catch ex As Exception
        End Try


    End Sub


    ''' <summary>
    ''' 手动查询时,获取上一条或者下一条故障信息
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Get_Next_GZ_Info(ByVal index As UShort)
        Dim str_timedate(1) As String
        Dim gng_gz As Guzhang_info
        Try
            gng_gz = Guzhang_Array.Item(index)
            str_timedate = Split(gng_gz.time_str, " ")

            'La_Date_GZ.Text = gng_gz.time_str
            'La_Time_GZ.Text = gng_gz.date_str

            'La_Date_GZ.Text = str_timedate(0)
            'La_Time_GZ.Text = str_timedate(1)


            'La_Addr_GZ.Text = gng_gz.tcq_id_str
            'La_Box_GZ.Text = gng_gz.tcq_name

            'If gng_gz.Guzhang_kind = Guzhang_info.Guzhang_enum.Comm_Error Then
            '    La_Kind_GZ.Text = "通讯故障"
            'ElseIf gng_gz.Guzhang_kind = Guzhang_info.Guzhang_enum.IL_Error Then
            '    La_Kind_GZ.Text = "电流传感器故障"
            'ElseIf gng_gz.Guzhang_kind = Guzhang_info.Guzhang_enum.T1_Error Then
            '    La_Kind_GZ.Text = "温度1传感器故障"
            'ElseIf gng_gz.Guzhang_kind = Guzhang_info.Guzhang_enum.T2_Error Then
            '    La_Kind_GZ.Text = "温度2传感器故障"
            'ElseIf gng_gz.Guzhang_kind = Guzhang_info.Guzhang_enum.Main_Power_Error Then
            '    La_Kind_GZ.Text = "主电源异常"
            'ElseIf gng_gz.Guzhang_kind = Guzhang_info.Guzhang_enum.Backup_Power_Error Then
            '    La_Kind_GZ.Text = "备电源异常"
            'End If

        Catch ex As Exception

        End Try
    End Sub


    ''' <summary>
    ''' 故障手动查询，向后一条信息
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub But_GZ_Find_Next_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        But_GZ_Find_Next_Click1()
    End Sub

    ''' <summary>
    ''' 故障手动查询，前一条信息
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub But_GZ_Find_Pre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        But_GZ_Find_Pre_Click1()
    End Sub


    Private Sub But_GZ_Find_Pre_Click1()
        Dim bgfp_i As UShort
        ' Dim bgfp_k As UShort
        'bgfp_i = DataGridView1.Rows.Count

        Try
            bgfp_i = Guzhang_Array.Count

            If bgfp_i < 1 Then  '如果故障对象集为空，则说明没有故障。
                'La_Date_GZ.Text = "---"
                'La_Time_GZ.Text = "---"
                'La_Addr_GZ.Text = "---"
                'La_Box_GZ.Text = "---"
                'La_Kind_GZ.Text = "---"
                'La_GZ_Index.Text = "当前无故障信息"
                Man_Find_GZ_Index = 0   '表示当前没有故障信息
                Exit Sub
            End If

            '数据集中有故障信息。显示顺序与故障集合的顺序刚好相反。
            '当前为第1条数据，即为数据集合的最后一个对象。

            '第1条 ----->  第count-1 条

            '第count条---> 第 0条


            'If Man_Find_GZ_Index = 1 Then  '如果是第一条，向前一条，则循环到第 count条。
            '    Man_Find_GZ_Index = Guzhang_Array.Count
            '    La_GZ_Index.Text = "当前为第" & Man_Find_GZ_Index & "条"
            '    bgfp_k = Guzhang_Array.Count - Man_Find_GZ_Index
            '    Get_Next_GZ_Info(bgfp_k)
            'Else
            '    Man_Find_GZ_Index = Man_Find_GZ_Index - 1
            '    La_GZ_Index.Text = "当前为第" & Man_Find_GZ_Index & "条"
            '    bgfp_k = Guzhang_Array.Count - Man_Find_GZ_Index
            '    Get_Next_GZ_Info(bgfp_k)
            'End If
        Catch ex As Exception

        End Try

    End Sub


    Private Sub But_GZ_Find_Next_Click1()
        Dim bgfp_i As UShort
        Dim bgfp_k As UShort
        'bgfp_i = DataGridView1.Rows.Count

        Try
            bgfp_i = Guzhang_Array.Count

            If bgfp_i < 1 Then  '如果故障对象集为空，则说明没有故障。
                'La_Date_GZ.Text = "---"
                'La_Time_GZ.Text = "---"
                'La_Addr_GZ.Text = "---"
                'La_Box_GZ.Text = "---"
                'La_Kind_GZ.Text = "---"
                'La_GZ_Index.Text = "当前无故障信息"
                'Man_Find_GZ_Index = 0   '表示当前没有故障信息
                Exit Sub
            End If

            '数据集中有故障信息。显示顺序与故障集合的顺序刚好相反。
            '当前为第1条数据，即为数据集合的最后一个对象。

            '第1条 ----->  第count-1 条

            '第count条---> 第 0条


            If Man_Find_GZ_Index = Guzhang_Array.Count Then  '如果是第一条，向前一条，则循环到第 count条。
                Man_Find_GZ_Index = 1
                '   La_GZ_Index.Text = "当前为第" & Man_Find_GZ_Index & "条"
                bgfp_k = Guzhang_Array.Count - Man_Find_GZ_Index
                Get_Next_GZ_Info(bgfp_k)
            Else
                Man_Find_GZ_Index = Man_Find_GZ_Index + 1
                '  La_GZ_Index.Text = "当前为第" & Man_Find_GZ_Index & "条"
                bgfp_k = Guzhang_Array.Count - Man_Find_GZ_Index
                Get_Next_GZ_Info(bgfp_k)
            End If
        Catch ex As Exception

        End Try
    End Sub



    ''' <summary>
    ''' 刷新监控报警显示框,报警 DataGridView2
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Refresh_Jiankong_Message()
        'Dim rgm_i As UShort
        'Dim rgm_gz As Alarm_info
        'Try
        '    Jiankong_Refresh_Lable = False

        '    rgm_i = DataGridView2.Rows.Count

        '    While (rgm_i > 0)
        '        DataGridView2.Rows.RemoveAt(rgm_i - 1)
        '        rgm_i = DataGridView2.Rows.Count
        '    End While

        '    Button9.Text = "当前报警信息共" & Baojing_Array.Count & "条"

        '    If Baojing_Array.Count < 1 Then
        '        La_Date_BJ.Text = "---"
        '        La_Time_BJ.Text = "---"
        '        La_Addr_BJ.Text = "---"
        '        La_Box_BJ.Text = "---"
        '        La_Kind_BJ.Text = "---"
        '        La_BJ_Index.Text = "当前无报警信息"
        '        La_BJ_Value.Text = "---"
        '        Man_Find_BJ_Index = 0
        '        Baojing_Lamp_Sta = False
        '        JDQ_Off()


        '        Exit Sub

        '    End If

        '    If Baojing_Array.Count >= 1 Then

        '        JDQ_On()

        '        For rgm_i = 0 To Baojing_Array.Count - 1

        '            rgm_gz = Baojing_Array.Item(Baojing_Array.Count - rgm_i - 1)
        '            DataGridView2.SelectAll()
        '            DataGridView2.ClearSelection()
        '            DataGridView2.Rows.Add()
        '            DataGridView2.Rows(rgm_i).Cells(0).Value = rgm_i + 1
        '            DataGridView2.Rows(rgm_i).Cells(1).Value = rgm_gz.time_str
        '            DataGridView2.Rows(rgm_i).Cells(2).Value = rgm_gz.tcq_name
        '            DataGridView2.Rows(rgm_i).Cells(3).Value = rgm_gz.tcq_id_str

        '            If rgm_gz.Alarm_kind = Alarm_info.alarm_enum.IL_alarm Then
        '                DataGridView2.Rows(rgm_i).Cells(4).Value = "电流报警"
        '            ElseIf rgm_gz.Alarm_kind = Alarm_info.alarm_enum.T1_alarm Then
        '                DataGridView2.Rows(rgm_i).Cells(4).Value = "温度1报警"
        '            Else
        '                DataGridView2.Rows(rgm_i).Cells(4).Value = "温度2报警"
        '            End If
        '        Next
        '        'DataGridView2.FirstDisplayedScrollingRowIndex = rgm_i - 1
        '        '认证软件，要求，手动查询，只能查询一个记录。
        '        '当故集合发送改变时，将查询定位到第一个。
        '        If RenZhen Then

        '            Man_Find_BJ_Index = 2
        '            But_BJ_Find_Pre_Click1()  '初始化显示第一条数据
        '        End If


        '    End If
        'Catch ex As Exception
        'End Try
    End Sub


    ''' <summary>
    ''' 手动查询时,获取上一条或者下一条报警信息
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Get_Next_BJ_Info(ByVal index As UShort)
        'Dim str_timedate(1) As String
        'Dim gng_bj As Alarm_info
        'Try
        '    gng_bj = Baojing_Array.Item(index)
        '    str_timedate = Split(gng_bj.time_str, " ")


        '    La_Date_BJ.Text = str_timedate(0)
        '    La_Time_BJ.Text = str_timedate(1)


        '    La_Addr_BJ.Text = gng_bj.tcq_id_str
        '    La_Box_BJ.Text = gng_bj.tcq_name

        '    If gng_bj.Alarm_kind = Alarm_info.alarm_enum.IL_alarm Then
        '        La_Kind_BJ.Text = "电流报警"
        '    ElseIf gng_bj.Alarm_kind = Alarm_info.alarm_enum.T1_alarm Then
        '        La_Kind_BJ.Text = "温度1报警"
        '    Else
        '        La_Kind_BJ.Text = "温度2报警"
        '    End If

        '    La_BJ_Value.Text = gng_bj.date_str

        'Catch ex As Exception

        'End Try
    End Sub


    ''' <summary>
    ''' 报警手动查询，向后一条信息
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub But_BJ_Find_Pre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        But_BJ_Find_Pre_Click1()
    End Sub

    ''' <summary>
    ''' 报警手动查询，前一条信息
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub But_BJ_Find_Next_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        But_BJ_Find_Next_Click1()
    End Sub


    Private Sub But_BJ_Find_Pre_Click1()
        Dim bgfp_i As UShort
        Dim bgfp_k As UShort
        'bgfp_i = DataGridView1.Rows.Count

        Try
            bgfp_i = Baojing_Array.Count

            If bgfp_i < 1 Then  '如果报警对象集为空，则说明没有故障。
                'La_Date_BJ.Text = "---"
                'La_Time_BJ.Text = "---"
                'La_Addr_BJ.Text = "---"
                'La_Box_BJ.Text = "---"
                'La_Kind_BJ.Text = "---"
                'La_BJ_Index.Text = "当前无报警信息"
                'La_BJ_Value.Text = "---"
                'Man_Find_BJ_Index = 0   '表示当前没有bj信息
                Exit Sub
            End If

            '数据集中有报警信息。显示顺序与bj集合的顺序刚好相反。
            '当前为第1条数据，即为数据集合的最后一个对象。
            '第1条 ----->  第count-1 条
            '第count条---> 第 0条


            If Man_Find_BJ_Index = 1 Then  '如果是第一条，向前一条，则循环到第 count条。
                Man_Find_BJ_Index = Baojing_Array.Count
                'La_BJ_Index.Text = "当前为第" & Man_Find_BJ_Index & "条"
                bgfp_k = Baojing_Array.Count - Man_Find_BJ_Index
                Get_Next_BJ_Info(bgfp_k)
            Else
                Man_Find_BJ_Index = Man_Find_BJ_Index - 1
                ' La_BJ_Index.Text = "当前为第" & Man_Find_BJ_Index & "条"
                bgfp_k = Baojing_Array.Count - Man_Find_BJ_Index
                Get_Next_BJ_Info(bgfp_k)
            End If
        Catch ex As Exception

        End Try

    End Sub


    Private Sub But_BJ_Find_Next_Click1()
        Dim bgfp_i As UShort
        Dim bgfp_k As UShort
        Try
            bgfp_i = Baojing_Array.Count

            If bgfp_i < 1 Then  '如果故障对象集为空，则说明没有故障。
                'La_Date_BJ.Text = "---"
                'La_Time_BJ.Text = "---"
                'La_Addr_BJ.Text = "---"
                'La_Box_BJ.Text = "---"
                'La_Kind_BJ.Text = "---"
                'La_BJ_Index.Text = "当前无报警信息"
                'La_BJ_Value.Text = "---"
                Man_Find_BJ_Index = 0   '表示当前没有故障信息
                Exit Sub
            End If

            '数据集中有故障信息。显示顺序与bj集合的顺序刚好相反。
            '当前为第1条数据，即为数据集合的最后一个对象。
            '第1条 ----->  第count-1 条
            '第count条---> 第 0条


            If Man_Find_BJ_Index = Baojing_Array.Count Then  '如果是第一条，向前一条，则循环到第 count条。
                Man_Find_BJ_Index = 1
                'La_BJ_Index.Text = "当前为第" & Man_Find_BJ_Index & "条"
                bgfp_k = Baojing_Array.Count - Man_Find_BJ_Index
                Get_Next_BJ_Info(bgfp_k)
            Else
                Man_Find_BJ_Index = Man_Find_BJ_Index + 1
                'La_BJ_Index.Text = "当前为第" & Man_Find_BJ_Index & "条"
                bgfp_k = Baojing_Array.Count - Man_Find_BJ_Index
                Get_Next_BJ_Info(bgfp_k)
            End If
        Catch ex As Exception

        End Try
    End Sub



    ''' <summary>
    ''' 定时器固定定时时间：100ms
    ''' Timer1定时终端函数，函数功能如下
    ''' 1:完成对上一次探测器回响数据的接收处理
    ''' 2:发送下一个探测器的查询(或设置)通讯指令
    ''' 
    ''' 2018年10月29日10:35:49  欲修改定时器逻辑
    ''' 第1个定时周期内，发送数据，
    ''' 最后一个定时周期内，接收处理数据。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        '定时计数器加1 
        Timer1_60ms_Ct = Timer1_60ms_Ct + 1
        '--------------------------第一个周期--接收任务-start-------------------------
        '可将设置Timer1_60ms_Ct = 2，约等待终端回响时长达200ms。
        If Timer1_60ms_Ct = 2 Then
            '-------------------------优先接收，非普通查询的通讯回响--------------------------
            PC_Wait_Others_Orders_Reecho()
            '-------------------次级接收：处理主程序里的普通通讯查询操作。--------------------
            If WaitNodeChaxunReecho Then
                WaitNodeChaxunReecho = False '普通通讯查询任务标识符

                Try
                    Get_Input_Data2(Tcq_Port)
                Catch ex As Exception

                End Try

                Jiankong_Alarm_Work()        '终端报警事物处理模块
                Comm_State_Work()            '通讯事物处理模块
            End If
            '---------------------------------------------------------------------------------
            Exit Sub

        End If
        '--------------------------第一个周期--接收任务-end-----------------------



        '------------------------最后一个周期，发送任务-start--------------------
        If Timer1_60ms_Ct >= Timer1_Tcq_Per Then
            Timer1_60ms_Ct = 0

            '优先发送：其它的非普通查询的通讯指令。
            'PC_Send_Others_Orders 函数中，如果发送了非主程序通讯的程序，则返回true
            If PC_Send_Others_Orders() <> True Then

                '最低级发送：主程序中的普通查询
                If Main_Chaxun_Loop_Wait = False Then
                    If TCQ_Usart_State Then
                        Comm_Loop_Id = Comm_Loop_Id + 1  '寻呼查询指针加1
                        If Comm_Loop_Id > Sys_node_count - 1 Then
                            Comm_Loop_Id = 0
                        End If
                        '主程序常规通讯查询
                        Send_ChaXun_Order(Fesn(Comm_Loop_Id).addr, Tcq_Port)
                        '标记等待终端 查询回响 
                        WaitNodeChaxunReecho = True
                    End If
                End If
            End If
        End If
        '---------------------------最后一个周期，发送任务-end-------------------

    End Sub


    ''' <summary>
    ''' 查询并接收处理非普通查询通讯指令的通讯任务，包括以下：
    ''' 1：报警打开/关闭
    ''' 2：音量加减
    ''' 3：上一曲，下一曲
    ''' </summary>
    ''' <remarks></remarks>
    Private Function PC_Wait_Others_Orders_Reecho() As Boolean

        '1： 接收回响：报警打开-记忆音量，音调，循环播放
        If PcSendAlarmOnMem = CommProgressEnum.PcNeedRec Then
            If Get_Reecho_Data(Tcq_Port) Then
                PcSendAlarmOnMem = CommProgressEnum.ReechoSuc
                LaFun.Text = "打开报警(记忆音乐循环播放)-通讯发送成功"
                LaFun.ForeColor = Color.Blue
                ManOpenAlarm(PcSendBjqindex)

            Else
                PcSendAlarmOnMem = CommProgressEnum.ReechoFail
                LaFun.Text = "打开报警(记忆音乐循环播放)-通讯发送失败"
                LaFun.ForeColor = Color.Red
            End If

            '2接收回响：报警关闭
        ElseIf PcSendAlarmOff = CommProgressEnum.PcNeedRec Then
            If Get_Reecho_Data(Tcq_Port) Then
                PcSendAlarmOff = CommProgressEnum.ReechoSuc
                LaFun.Text = "关闭报警-通讯发送成功"
                LaFun.ForeColor = Color.Blue
                ManCloseAlarm(PcSendBjqindex)
            Else
                PcSendAlarmOff = CommProgressEnum.ReechoFail
                LaFun.Text = "关闭报警-通讯发送失败"
                LaFun.ForeColor = Color.Red
            End If

            '3接收回响： 报警打开-记忆音量，音调，单曲播放
        ElseIf PcSendAlarmOnMemOnce = CommProgressEnum.PcNeedRec Then
            If Get_Reecho_Data(Tcq_Port) Then
                PcSendAlarmOnMemOnce = CommProgressEnum.ReechoSuc
                LaFun.Text = "打开报警(记忆音乐单曲播放)-通讯发送成功"
                LaFun.ForeColor = Color.Blue
                ManOpenAlarm(PcSendBjqindex)
            Else
                PcSendAlarmOnMemOnce = CommProgressEnum.ReechoFail
                LaFun.Text = "打开报警(记忆音乐单曲播放)-通讯发送失败"
                LaFun.ForeColor = Color.Red
            End If

            '4接收回响： 报警打开-指定音量，音调，循环播放
        ElseIf PcSendAlarmOnAssLoop = CommProgressEnum.PcNeedRec Then
            If Get_Reecho_Data(Tcq_Port) Then
                PcSendAlarmOnAssLoop = CommProgressEnum.ReechoSuc
                LaFun.Text = "打开报警(指定音乐循环播放)-通讯发送成功"
                LaFun.ForeColor = Color.Blue
                ManOpenAlarm(PcSendBjqindex)
            Else
                PcSendAlarmOnAssLoop = CommProgressEnum.ReechoFail
                LaFun.Text = "打开报警(指定音乐循环播放)-通讯发送失败"
                LaFun.ForeColor = Color.Red
            End If

            '5接收回响： 报警打开-指定音量，音调，单曲播放
        ElseIf PcSendAlarmOnAssOnce = CommProgressEnum.PcNeedRec Then
            If Get_Reecho_Data(Tcq_Port) Then
                PcSendAlarmOnAssOnce = CommProgressEnum.ReechoSuc
                LaFun.Text = "打开报警(指定音乐单曲播放)-通讯发送成功"
                LaFun.ForeColor = Color.Blue
                ManOpenAlarm(PcSendBjqindex)
            Else
                PcSendAlarmOnAssOnce = CommProgressEnum.ReechoFail
                LaFun.Text = "打开报警(指定音乐单曲播放)-通讯发送失败"
                LaFun.ForeColor = Color.Red
            End If

            '5-1接收回响： 手动查询一次
        ElseIf PcSendChaxunOnce = CommProgressEnum.PcNeedRec Then
            If Get_Reecho_Data(Tcq_Port) Then
                PcSendChaxunOnce = CommProgressEnum.ReechoSuc
                If BjqReechoData(0) >= 1 Then
                    Label29.Text = "报警打开"
                    Label29.ForeColor = Color.Red
                Else
                    Label29.Text = "报警关闭"
                    Label29.ForeColor = Color.Black
                End If

                Label30.Text = BjqReechoData(1).ToString
                Label31.Text = BjqReechoData(2).ToString
                BjqCommCt = BjqCommCt + 1

                Label28.Text = "正常+" & BjqCommCt.ToString
                Label28.ForeColor = Color.Blue
                LaFun.Text = "手动查询-通讯发送成功"
                LaFun.ForeColor = Color.Blue

            Else
                PcSendChaxunOnce = CommProgressEnum.ReechoFail
                Label28.Text = "失败+" & BjqCommCt.ToString
                Label28.ForeColor = Color.Red
                LaFun.Text = "手动查询-通讯发送失败"
                LaFun.ForeColor = Color.Red

            End If


            '6接收回响： 音量加
        ElseIf PcSendVolumAdd = CommProgressEnum.PcNeedRec Then
            If Get_Reecho_Data(Tcq_Port) Then
                PcSendVolumAdd = CommProgressEnum.ReechoSuc
                LaFun.Text = "音量加-通讯发送成功"
                Label31.Text = BjqReechoData(2).ToString
                LaFun.ForeColor = Color.Blue
            Else
                PcSendVolumAdd = CommProgressEnum.ReechoFail
                LaFun.Text = "音量加-通讯发送失败"
                LaFun.ForeColor = Color.Red
            End If

            '7接收回响： 音量减
        ElseIf PcSendVolumRec = CommProgressEnum.PcNeedRec Then
            If Get_Reecho_Data(Tcq_Port) Then
                PcSendVolumRec = CommProgressEnum.ReechoSuc
                LaFun.Text = "音量减-通讯发送成功"
                Label31.Text = BjqReechoData(2).ToString
                LaFun.ForeColor = Color.Blue
            Else
                PcSendVolumRec = CommProgressEnum.ReechoFail
                LaFun.Text = "音量减-通讯发送失败"
                LaFun.ForeColor = Color.Red
            End If

            '8接收回响： 下一曲
        ElseIf PcSendMusicNext = CommProgressEnum.PcNeedRec Then
            If Get_Reecho_Data(Tcq_Port) Then
                PcSendMusicNext = CommProgressEnum.ReechoSuc
                LaFun.Text = "下一曲-通讯发送成功"
                Label30.Text = BjqReechoData(2).ToString
                LaFun.ForeColor = Color.Blue
            Else
                PcSendMusicNext = CommProgressEnum.ReechoFail
                LaFun.Text = "下一曲-通讯发送失败"
                LaFun.ForeColor = Color.Red
            End If

            '9接收回响： 上一曲
        ElseIf PcSendMusicPre = CommProgressEnum.PcNeedRec Then
            If Get_Reecho_Data(Tcq_Port) Then
                PcSendMusicPre = CommProgressEnum.ReechoSuc
                LaFun.Text = "上一曲-通讯发送成功"
                Label30.Text = BjqReechoData(2).ToString
                LaFun.ForeColor = Color.Blue
            Else
                PcSendMusicPre = CommProgressEnum.ReechoFail
                LaFun.Text = "上一曲-通讯发送失败"
                LaFun.ForeColor = Color.Red
            End If

            '最低优先级-接收处理-单击调试中的-自动查询
        ElseIf BjqCommChaxunLoop_Wait_Reecho Then
            BjqCommChaxunLoop_Wait_Reecho = False

            If Get_Reecho_Data_Auto_Chaxun(Tcq_Port) Then
                PcSendChaxunOnce = CommProgressEnum.ReechoSuc
                If BjqReechoData(0) >= 1 Then
                    Label29.Text = "报警打开"
                    Label29.ForeColor = Color.Red
                Else
                    Label29.Text = "报警关闭"
                    Label29.ForeColor = Color.Black
                End If

                Label30.Text = BjqReechoData(1).ToString
                Label31.Text = BjqReechoData(2).ToString
                BjqCommCt = BjqCommCt + 1
                Label28.Text = "正常 " & BjqCommCt.ToString
                Label28.ForeColor = Color.Blue
            Else
                BjqCommCt = BjqCommCt + 1
                Label30.Text = "--"
                Label31.Text = "--"
                Label29.Text = "--"
                Label29.ForeColor = Color.Black
                Label28.ForeColor = Color.Black
                Label28.Text = "故障 " & BjqCommCt.ToString
            End If

            If BjqCommCt >= 30000 Then
                BjqCommCt = 0
            End If


        End If


    End Function


    ''' <summary>
    ''' 发送其它的非普通查询的通讯任务
    ''' 1：报警打开/关闭
    ''' 2：音量加减
    ''' 3：上一曲，下一曲
    ''' </summary>
    ''' <remarks></remarks>
    Private Function PC_Send_Others_Orders() As Boolean

        PC_Send_Others_Orders = False

        '1:主机发送打开报警器-记忆音调/量-循环播放 
        If PcSendAlarmOnMem = CommProgressEnum.PcNeedSend Then
            WaitNodeAlarmOnReecho = True
            Send_Other_Order(Tcq_Port)
            PcSendAlarmOnMem = CommProgressEnum.PcNeedRec
            WaitNodeChaxunReecho = False
            Return True

            '2:主机发送打开报警器-记忆音调/量-循环播放 
        ElseIf PcSendAlarmOnMemOnce = CommProgressEnum.PcNeedSend Then
            WaitNodeAlarmOnReecho = True
            Send_Other_Order(Tcq_Port)
            PcSendAlarmOnMemOnce = CommProgressEnum.PcNeedRec
            WaitNodeChaxunReecho = False
            Return True

            '3:主机发送打开报警器-指定音调/量-循环播放 
        ElseIf PcSendAlarmOnAssLoop = CommProgressEnum.PcNeedSend Then
            WaitNodeAlarmOnReecho = True
            Send_Other_Order(Tcq_Port)
            PcSendAlarmOnAssLoop = CommProgressEnum.PcNeedRec
            WaitNodeChaxunReecho = False
            Return True

            '4:主机发送打开报警器-指定音调/量-单曲播放 
        ElseIf PcSendAlarmOnAssOnce = CommProgressEnum.PcNeedSend Then
            WaitNodeAlarmOnReecho = True
            Send_Other_Order(Tcq_Port)
            PcSendAlarmOnAssOnce = CommProgressEnum.PcNeedRec
            WaitNodeChaxunReecho = False
            Return True

            '5: 主机发送关闭报警器 
        ElseIf PcSendAlarmOff = CommProgressEnum.PcNeedSend Then
            WaitNodeAlarmOffReecho = True
            Send_Other_Order(Tcq_Port)
            PcSendAlarmOff = CommProgressEnum.PcNeedRec
            WaitNodeChaxunReecho = False
            Return True

            '6: 主机发送-手动查询一次
        ElseIf PcSendChaxunOnce = CommProgressEnum.PcNeedSend Then
            WaitNodeAlarmOffReecho = True
            Send_Other_Order(Tcq_Port)
            PcSendChaxunOnce = CommProgressEnum.PcNeedRec
            WaitNodeChaxunReecho = False
            Return True

            '6: 主机发送音量加
        ElseIf PcSendVolumAdd = CommProgressEnum.PcNeedSend Then
            WaitNodeAlarmOffReecho = True
            Send_Other_Order(Tcq_Port)
            PcSendVolumAdd = CommProgressEnum.PcNeedRec
            WaitNodeChaxunReecho = False
            Return True

            '7: 主机发送音量减
        ElseIf PcSendVolumRec = CommProgressEnum.PcNeedSend Then
            WaitNodeAlarmOffReecho = True
            Send_Other_Order(Tcq_Port)
            PcSendVolumRec = CommProgressEnum.PcNeedRec
            WaitNodeChaxunReecho = False
            Return True

            '8: 主机发送音调下一首
        ElseIf PcSendMusicNext = CommProgressEnum.PcNeedSend Then
            WaitNodeAlarmOffReecho = True
            Send_Other_Order(Tcq_Port)
            PcSendMusicNext = CommProgressEnum.PcNeedRec
            WaitNodeChaxunReecho = False
            Return True

            '9: 主机发送音调上一首
        ElseIf PcSendMusicPre = CommProgressEnum.PcNeedSend Then
            WaitNodeAlarmOffReecho = True
            Send_Other_Order(Tcq_Port)
            PcSendMusicPre = CommProgressEnum.PcNeedRec
            WaitNodeChaxunReecho = False
            Return True

            '最低优先级发送：单机调试中的自动查询
        ElseIf BjqCommChaxunLoop Then
            Dim addr As Byte
            addr = Val(ComAddr.SelectedIndex + 1)               '取到地址
            Send_ChaXun_Order(addr, Tcq_Port)      '发送数据
            BjqCommChaxunLoop_Wait_Reecho = True
            Return True
        End If



    End Function



    ''' <summary>
    ''' 与ATX-200,通讯实物处理。
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ATX_Power_Comm()
        '如果是东阳的项目，用的不是ATX-200

        Try
            If DongYang Then
                If Atx_Power_Comm_Count >= 30 Then
                    Atx_Power_Comm_Count = 0
                    Get_ATX_State_Dongyang() '读取GPIO的第七位，高电平为电池供电，低电平为市电供电。
                Else
                    Atx_Power_Comm_Count = Atx_Power_Comm_Count + 1
                End If
            Else
                '接收ATX-200发来的通讯
                If Wait_ATX_ReCall Then
                    Wait_ATX_ReCall = False
                    If Get_ATX_Comm_Info() Then
                        ATX_200_Comm_Fail_Count = 0
                    Else
                        ATX_200_Comm_Fail_Count = ATX_200_Comm_Fail_Count + 1
                        If ATX_200_Comm_Fail_Count >= 5 Then
                            ATX_200_Comm_Fail_Count = 5
                        End If
                    End If
                End If

                'Work4:每隔2秒钟时间，向工控机发送查询指令，并标记在下一个60ms时刻，接收数据
                If Atx_Power_Comm_Count >= 30 Then
                    Atx_Power_Comm_Count = 0
                    If ATX200_Usart_State Then
                        Dim send_atx(4) As Char
                        send_atx(0) = "C"
                        send_atx(1) = "0"
                        send_atx(2) = "0"
                        send_atx(3) = "E"
                        ATX_Port.Write(send_atx, 0, 4)  '将数据发送出去。
                        ATX_Port.DiscardInBuffer()
                        Wait_ATX_ReCall = True  '标志在下一个60ms来的时候读取串口数据
                    End If
                End If

                Atx_Power_Comm_Count = Atx_Power_Comm_Count + 1
            End If

        Catch ex As Exception

        End Try


    End Sub

    ''' <summary>
    ''' 系统自检时，发送自检命令给探测器。
    ''' order=1 表示复位
    ''' order=2 表示自检
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Sys_Tcq_SelfCheck_Comm(ByVal order As Byte)
    End Sub


    ''' <summary>
    ''' 接收探测器的回响信号。如果地址正确，回响正确，则返回true
    ''' </summary>
    ''' <param name="addr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Get_Recall_Input(ByVal addr As UShort, ByRef tcq_p As SerialPort) As Boolean
        Dim Byte_ct As UShort
        Dim Rec_array_t() As Byte
        Dim i_temp As UShort

        Get_Recall_Input = False

        Try
            '获取接收缓冲区的字节数
            Byte_ct = tcq_p.BytesToRead

            '判断接收的字节数，最少为3个字节，即探测器的回响信号。如果少于3个，则直接退出。
            If Byte_ct <= 2 Then
                Exit Function
            End If

            '接收口数据过多，则只取前200个数据。
            If Byte_ct >= 200 Then
                Byte_ct = 200
            End If

            '重定义接收缓存数据数组并接收数据
            ReDim Rec_array_t(Byte_ct - 1)
            tcq_p.Read(Rec_array_t, 0, Byte_ct)

            '报文头判断 回响报文 头为FD
GID_Loop11:  '采用循环滤除判断正确报文头方式
            If (Rec_array_t(0) <> &HFD) Then

                If Byte_ct > 2 Then
                    For i_temp = 1 To Byte_ct - 1
                        Rec_array_t(i_temp - 1) = Rec_array_t(i_temp)
                    Next
                    Byte_ct = Byte_ct - 1
                    GoTo GID_Loop11
                End If
            End If

            '程序走到此处，如果报文头一直不对，则 Byte_ct的值很小或等于0
            '以FA和FC开始的报文，字节数量都为13个。
            If Byte_ct < 3 Then
                Exit Function
            End If

            '判断地址是否正确
            Dim addr_t As UShort
            addr_t = Rec_array_t(1) * 256 + Rec_array_t(2)

            If addr <> addr_t Then
                Exit Function
            End If

            Get_Recall_Input = True

        Catch ex As Exception
        End Try

    End Function



    ''' <summary>
    ''' 获取系统工作电源状态 for 东阳。。。
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Get_ATX_State_Dongyang() As Boolean
    End Function

    ''' <summary>
    ''' Timer2:100ms定时器。
    ''' 任务1:1秒计时刷新时间显示。
    ''' 任务2：用于单机调试中通讯数据的发送和接收处理。（此时应关闭timer1定时器）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick

        Time_Message_Fresh_Count = Time_Message_Fresh_Count + 1

        '每100ms的时间进入到此定时器，判断是否有通讯任务
        '报警，打开，关闭，音量调节在前，自动查询在后。




        'Work-1:每1s种刷新窗体-----故障报警窗体
        If Time_Message_Fresh_Count >= 10 Then
            '这里接收判断是否有设置地址的数据接收任务。
            If PcSendSetAddrReechoWait Then
                If Get_Reecho_Data(Tcq_Port) Then
                    PcSendSetAddrReechoWait = False
                    LaSetAddr.Text = "地址设置成功！"
                    LaSetAddr.ForeColor = Color.Blue
                Else
                    PcSendSetAddrReechoWait = False
                    LaSetAddr.Text = "地址设置失败！"
                    LaSetAddr.ForeColor = Color.Red
                End If
            End If
            Label_Date.Text = Now.Date
            Label_Time.Text = Now.Hour.ToString & ":" & Now.Minute.ToString & ":" & Now.Second.ToString
            Select Case Weekday(Now)
                Case 1
                    Label2.Text = "星期日"
                Case 2
                    Label2.Text = "星期一"
                Case 3
                    Label2.Text = "星期二"
                Case 4
                    Label2.Text = "星期三"
                Case 5
                    Label2.Text = "星期四"
                Case 6
                    Label2.Text = "星期五"
                Case 7
                    Label2.Text = "星期六"
            End Select

            'If Guzhang_Refresh_Lable Then
            '    Refresh_Guzhang_Message()
            'End If

            'If Jiankong_Refresh_Lable Then
            '    Refresh_Jiankong_Message()
            'End If
            Time_Message_Fresh_Count = 0
        End If

    End Sub



    ''' <summary>
    ''' 打开图形显示装置的串口
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function TX_Usart_Init() As Boolean
        Try
            TX_Port = New SerialPort("com4", 9600, Parity.None, 8, StopBits.One)
            TX_Port.ReadBufferSize = 1024

            TX_Port.WriteBufferSize = 1024
            TX_Port.ReceivedBytesThreshold = 1
            TX_Port.Open()

            TX_Port_State = True

            Return True
        Catch ex As Exception
            TX_Port_State = False
        End Try

    End Function



    ''' <summary>
    '''  声光控制线程，定时睡眠100ms.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SGKZ() As Boolean

        While (1)
            Thread.Sleep(100)
            If (Sys_Mute = False) And (Speaker_Zj_Use = False) Then

                '监控报警
                If Zll = 1 Then
                    If Speaker_Ct < 1 Then  '200ms响 100ms不响
                        Speaker_On()
                    Else
                        Speaker_Off()
                    End If
                    Speaker_Ct = Speaker_Ct + 1
                    If Speaker_Ct >= 2 Then
                        Speaker_Ct = 0
                    End If


                ElseIf Zll = 2 Then
                    '-----------------故障报警，500ms 发声，700ms静音----------------
                    If Speaker_Ct <= 4 Then
                        Speaker_On()
                    Else
                        Speaker_Off()
                    End If
                    Speaker_Ct = Speaker_Ct + 1
                    If Speaker_Ct >= 10 Then
                        Speaker_Ct = 0
                    End If
                End If
                'Else
                '    Speaker_Ct = 0

            ElseIf Speaker_Zj_Use And Speaker_Zj_Baojing Then

                If Speaker_Ct < 1 Then  '200ms响 100ms不响
                    Speaker_On()
                Else
                    Speaker_Off()
                End If
                Speaker_Ct = Speaker_Ct + 1
                If Speaker_Ct >= 2 Then
                    Speaker_Ct = 0
                End If
            ElseIf Speaker_Zj_Use And Speaker_Zj_Guzhang Then
                If Speaker_Ct <= 4 Then
                    Speaker_On()
                Else
                    Speaker_Off()
                End If
                Speaker_Ct = Speaker_Ct + 1
                If Speaker_Ct >= 10 Then
                    Speaker_Ct = 0
                End If
            End If


            If Guzhang_Lamp_Zj_Use = False Then
                If Guzhang_Lamp_Sta Then
                    Guzhang_Lamp_On()
                Else
                    Guzhang_Lamp_Off()
                End If
            End If

            If Baojing_Lamp_Zj_Use = False Then
                If Baojing_Lamp_Sta Then
                    Baojing_Lamp_On()
                Else
                    Baojing_Lamp_Off()
                End If
            End If

            '接着处理 与图形显示装置通讯。
            '如果图形串口正常。

            Try

                If TX_Port_State Then

                    TX_Comm_Ct = TX_Comm_Ct + 1

                    If TX_RST Then   '图显复位 
                        RST_TX_Order()
                        Thread.Sleep(100)
                        RST_TX_Order()
                        TX_RST = False
                    End If

                    If TX_Comm_Ct >= 20 Then
                        TX_Comm_Ct = 0
                        '发送询问 
                        XunWen()
                    Else
                        If TX_Array.Count > 0 Then
                            TX_TX_Work()
                        End If
                    End If

                End If

            Catch ex As Exception

            End Try

        End While

    End Function



    Private Function TX_TX_Work() As Boolean
        'TX_Send_OK = False 表示，需要发送信息给图显，
        '发送信息完成后，等待图显回响 ，若连续两次没有回响，则不在发送。
        If TX_Send_OK = False Then

            '如果是等待图显回响 
            If Wait_Tx_Recall Then
                '读取串口数据，
                Dim Byte_ct As UShort
                Dim Rec_array_t() As Byte
                '获取接收缓冲区的字节数
                Byte_ct = TX_Port.BytesToRead
                '判断接收的字节数，最少为3个字节，即探测器的回响信号。如果少于3个，则直接退出。
                'If Byte_ct <= 2 Then
                '    Exit Function
                'End If

                '接收口数据过多，则只取前200个数据。
                If Byte_ct >= 200 Then
                    Byte_ct = 200
                End If

                '重定义接收缓存数据数组并接收数据
                ReDim Rec_array_t(Byte_ct - 1)
                TX_Port.Read(Rec_array_t, 0, Byte_ct)


                '如果数据正确，则 TX_Send_OK=true  然后返回，推出。
                If Byte_ct > 0 Then
                    TX_Send_OK = True
                    Wait_Tx_Recall = False
                    Exit Function
                End If

                If TX_TX_Retry >= 3 Then '如果连续发送2次仍然每晚餐。则退出 。
                    TX_Send_OK = True
                    Wait_Tx_Recall = False
                    Exit Function
                End If

                TX_TX_Retry = TX_TX_Retry + 1

                '在次发送通讯给图显 
                TX_Send()


            Else
                '发送通讯给图显
                TX_Send()
                Wait_Tx_Recall = True
                TX_TX_Retry = 1
            End If
        End If
    End Function





    ''' <summary>
    '''  发送通讯参数给图显 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function TX_Send() As Boolean
        Dim send_ar() As Byte            '主机查询指令
        Dim asci As UShort

        Dim ttc(8) As Char
        Dim i As Byte
        Dim k As Byte

        TX_Port.DiscardOutBuffer()

        If TX_Send_Obj.info_kind <= 3 Then
            ReDim send_ar(30)

            '         addr  
            ' @FIR001 001 001 1 17 0602 1627 006156#
            send_ar(0) = Asc("@")
            If TX_Send_Obj.info_kind = 1 Then   '报警 
                send_ar(1) = Asc("F")
                send_ar(2) = Asc("I")
                send_ar(3) = Asc("R")
                send_ar(13) = Asc(TX_Send_Obj.baojin_kind)   '报警类型。
            ElseIf TX_Send_Obj.info_kind = 2 Then  '故障信息 
                send_ar(1) = Asc("F")
                send_ar(2) = Asc("A")
                send_ar(3) = Asc("U")
                send_ar(13) = Asc(TX_Send_Obj.guzhang_kind)  ' Asc("1")   '故障类型。
            Else                            '故障恢复
                send_ar(1) = Asc("F")
                send_ar(2) = Asc("A")
                send_ar(3) = Asc("R")
                send_ar(13) = Asc(TX_Send_Obj.guzhang_kind)    '故障类型。
            End If


            send_ar(4) = Asc("0")
            send_ar(5) = Asc("0")
            send_ar(6) = Asc("1")  '回路号，不知道是不是通讯之路的意思。

            asci = TX_Send_Obj.tcq_id \ 100
            send_ar(7) = Asc(asci.ToString)

            asci = TX_Send_Obj.tcq_id Mod 100
            asci = asci \ 10
            send_ar(8) = Asc(asci.ToString)

            asci = TX_Send_Obj.tcq_id Mod 10
            send_ar(9) = Asc(asci.ToString)  '这里要设置为探测器地址。


            '电流是通道1   
            '温度1是通道2   温度2是通道3 。
            send_ar(10) = Asc("0")
            send_ar(11) = Asc("0")
            send_ar(12) = Asc(TX_Send_Obj.tongdao)  '通道号 


            send_ar(24) = Asc("1")
            send_ar(25) = Asc("1")
            send_ar(26) = Asc("1")

            send_ar(27) = Asc("2")
            send_ar(28) = Asc("2")
            send_ar(29) = Asc("2")


            '  tstr = Format(Now(), "yy/MM/dd/HH/mm")
            ttc = TX_Send_Obj.happen_time.ToCharArray
            k = 14
            For i = 0 To ttc.Length - 1
                If ttc(i) <> "/" Then
                    send_ar(k) = Asc(ttc(i))
                    k = k + 1
                End If
            Next
            send_ar(30) = Asc("#")
            TX_Port.Write(send_ar, 0, 31)  '将数据发送出去。
            TX_Port.DiscardInBuffer()

        Else  '主/备电故障和恢复。

            ReDim send_ar(20)
            send_ar(0) = Asc("@")

            If TX_Send_Obj.info_kind = 4 Then
                send_ar(1) = Asc("A")
                send_ar(2) = Asc("C")
                send_ar(3) = Asc("F")

            ElseIf TX_Send_Obj.info_kind = 5 Then
                send_ar(1) = Asc("A")
                send_ar(2) = Asc("C")
                send_ar(3) = Asc("N")

            ElseIf TX_Send_Obj.info_kind = 6 Then
                send_ar(1) = Asc("B")
                send_ar(2) = Asc("T")
                send_ar(3) = Asc("F")

            ElseIf TX_Send_Obj.info_kind = 7 Then
                send_ar(1) = Asc("B")
                send_ar(2) = Asc("T")
                send_ar(3) = Asc("N")
            End If

            ttc = TX_Send_Obj.happen_time.ToCharArray
            k = 4
            For i = 0 To ttc.Length - 1
                If ttc(i) <> "/" Then
                    send_ar(k) = Asc(ttc(i))
                    k = k + 1
                End If
            Next
            send_ar(14) = Asc("1")
            send_ar(15) = Asc("1")
            send_ar(16) = Asc("1")

            send_ar(17) = Asc("2")
            send_ar(18) = Asc("2")
            send_ar(19) = Asc("2")

            send_ar(20) = Asc("#")
            TX_Port.Write(send_ar, 0, 21)  '将数据发送出去。
            TX_Port.DiscardInBuffer()
        End If



    End Function


    ''' <summary>
    ''' 主机发送询问指令给图显 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function XunWen() As Boolean
        Dim send_ar() As Byte            '主机查询指令
        TX_Port.DiscardOutBuffer()

        ReDim send_ar(13)

        send_ar(0) = Asc("@")
        send_ar(1) = Asc("I")
        send_ar(2) = Asc("N")
        send_ar(3) = Asc("Q")

        send_ar(4) = Asc("0")
        send_ar(5) = Asc("0")
        send_ar(6) = Asc("0")

        send_ar(7) = Asc("0")
        send_ar(8) = Asc("0")
        send_ar(9) = Asc("0")

        send_ar(10) = Asc("0")
        send_ar(11) = Asc("0")
        send_ar(12) = Asc("0")

        send_ar(13) = Asc("#")
        TX_Port.Write(send_ar, 0, 14)  '将数据发送出去。
        TX_Port.DiscardInBuffer()

    End Function

    ''' <summary>
    ''' 复位图形显示装置
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RST_TX_Order() As Boolean
        Dim send_ar() As Byte            '主机查询指令
        Dim t_str As String

        TX_Port.DiscardOutBuffer()

        ReDim send_ar(20)

        send_ar(0) = Asc("@")
        send_ar(1) = Asc("R")
        send_ar(2) = Asc("S")
        send_ar(3) = Asc("T")

        Dim ttc(8) As Char
        'Dim tstr As String
        Dim i As Byte
        Dim k As Byte

        t_str = Format(Now(), "yy/MM/dd/HH/mm")
        ttc = TX_Send_Obj.happen_time.ToCharArray
        k = 4
        For i = 0 To ttc.Length - 1
            If ttc(i) <> "/" Then
                send_ar(k) = Asc(ttc(i))
                k = k + 1
            End If
        Next


        send_ar(14) = Asc("0")
        send_ar(15) = Asc("0")
        send_ar(16) = Asc("0")

        send_ar(17) = Asc("0")
        send_ar(18) = Asc("0")
        send_ar(19) = Asc("0")

        send_ar(20) = Asc("#")
        TX_Port.Write(send_ar, 0, 21)  '将数据发送出去。
        TX_Port.DiscardInBuffer()

    End Function


    ''' <summary>
    ''' 主机通过RS232串口，向终端发送查询通讯帧
    ''' 同时标记了等待终端查询返回的标识 
    ''' </summary>
    ''' <param name="nodeAddr">节点(终端)地址</param>
    ''' <param name="commPort">主机串口</param>
    ''' <remarks></remarks>
    Private Sub Send_ChaXun_Order(ByVal nodeAddr As UShort, ByRef commPort As SerialPort)

        Dim send_ar(8) As Byte   '0-8 共计9个
        CommChaxun(nodeAddr, send_ar)      '生成通讯帧，函数定义在comm.vb中。
        Try
            commPort.DiscardOutBuffer()     '清空串口发送缓存区
            commPort.Write(send_ar, 0, 9)   '将数据发送出去。
            commPort.DiscardInBuffer()

            If CommDataWatch Then

                If ComWatchAddr.SelectedIndex = 0 Then
                    Dim str_data As String
                    str_data = ""
                    For Each da As Byte In send_ar
                        str_data = str_data & " " & Hex(da)
                    Next
                    RichTextSend.Text = RichTextSend.Text & vbCrLf & str_data

                    If RichTextSend.TextLength >= 10000 Then
                        RichTextSend.Text = ""
                    End If
                Else
                    If Comm_Loop_Id = ComWatchAddr.SelectedIndex - 1 Then
                        Dim str_data As String
                        str_data = ""
                        For Each da As Byte In send_ar
                            str_data = str_data & " " & Hex(da)
                        Next
                        RichTextSend.Text = RichTextSend.Text & vbCrLf & str_data

                        If RichTextSend.TextLength >= 10000 Then
                            RichTextSend.Text = ""
                        End If
                    End If
                End If

            End If


        Catch ex As Exception
            But_Data_View.Text = "com" & Sys_tcq_com_id & "发生故障"
            But_Data_View.ForeColor = Color.Red
            But_Table_View.Text = "检查或设置串口"
            But_Map_View.Text = "重启软件"
            But_Table_View.ForeColor = Color.Red
            But_Map_View.ForeColor = Color.Red
            But_Data_View.ForeColor = Color.Red
            Main_Chaxun_Loop_Wait = True
            Timer1.Enabled = False
            MessageBox.Show("当前使用的串口com" & Sys_tcq_com_id & "发生故障，请检查串口")
        End Try

    End Sub




    ''' <summary>
    '''  主机通过RS232串口，向终端发送通讯
    '''  数据PcSendToBjq(9)，在 Form_Bjd.vb 中已经生成好了。
    ''' </summary>
    ''' <param name="commPort">节点(终端)地址</param>
    ''' <remarks></remarks>
    Private Sub Send_Other_Order(ByRef commPort As SerialPort)

        commPort.DiscardOutBuffer()         '清空串口发送缓存区
        commPort.Write(PcSendToBjq, 0, 9)  '将数据发送出去。
        commPort.DiscardInBuffer()

    End Sub


    ''' <summary>
    ''' 主机向下发送探测器自检指令
    ''' </summary>
    ''' <param name="tcq_addr">探测器的地址</param>
    ''' <remarks></remarks>
    Private Sub Send_Self_Check_Order(ByVal tcq_addr As UShort, ByRef tcq_p As SerialPort)
        Dim send_ar(4) As Byte            '主机查询指令
        Dim jy_t As UShort
        tcq_p.DiscardOutBuffer()
        send_ar(0) = &HF2
        send_ar(1) = tcq_addr \ 256
        send_ar(2) = tcq_addr Mod 256
        jy_t = send_ar(0)
        jy_t = jy_t + send_ar(1)
        jy_t = jy_t + send_ar(2)
        jy_t = jy_t Mod 256
        send_ar(3) = jy_t \ 16
        send_ar(4) = jy_t Mod 16

        tcq_p.Write(send_ar, 0, 5)  '将数据发送出去。
        tcq_p.DiscardInBuffer()

    End Sub




    ''' <summary>
    ''' 主机向下发送探测器guanbi  mingling
    ''' </summary>
    ''' <param name="tcq_addr">探测器的地址</param>
    ''' <remarks></remarks>
    Private Sub Send_Mute_Order(ByVal tcq_addr As UShort, ByRef tcq_p As SerialPort)
        Dim send_ar(5) As Byte            '主机查询指令
        Dim jy_t As UShort
        tcq_p.DiscardOutBuffer()
        send_ar(0) = &HF6
        send_ar(1) = tcq_addr \ 256
        send_ar(2) = tcq_addr Mod 256
        send_ar(3) = Mute_Flag

        jy_t = send_ar(0)
        jy_t = jy_t + send_ar(1)
        jy_t = jy_t + send_ar(2)
        jy_t = jy_t + send_ar(3)
        jy_t = jy_t Mod 256
        send_ar(4) = jy_t \ 16
        send_ar(5) = jy_t Mod 16

        tcq_p.Write(send_ar, 0, 6)  '将数据发送出去。
        tcq_p.DiscardInBuffer()

    End Sub



    ''' <summary>
    ''' 主机向下发送探测器自检指令
    ''' </summary>
    ''' <param name="tcq_addr">探测器的地址</param>
    ''' <remarks></remarks>
    Private Sub Send_Rest_Order(ByVal tcq_addr As UShort, ByRef tcq_p As SerialPort)
        Dim send_ar(4) As Byte            '主机查询指令
        Dim jy_t As UShort
        tcq_p.DiscardOutBuffer()
        send_ar(0) = &HF1
        send_ar(1) = tcq_addr \ 256
        send_ar(2) = tcq_addr Mod 256
        jy_t = send_ar(0)
        jy_t = jy_t + send_ar(1)
        jy_t = jy_t + send_ar(2)
        jy_t = jy_t Mod 256
        send_ar(3) = jy_t \ 16
        send_ar(4) = jy_t Mod 16

        tcq_p.Write(send_ar, 0, 5)  '将数据发送出去。
        tcq_p.DiscardInBuffer()

    End Sub



    ''' <summary>
    ''' 系统复位发送指令
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Sys_Res_Sub()

    End Sub

    ''' <summary>
    ''' 一次读取完串口里面所有的数据，然后判断校验数据。
    ''' 数据接收采用滤除判断正确报文头和报文字节数，同时按规则校验。
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Get_Input_Data()

        Dim Byte_ct As UShort
        Dim Rec_array_t() As Byte
        Dim i_temp As UShort

        '获取接收缓冲区的字节数
        Byte_ct = Tcq_Port.BytesToRead

        '判断接收的字节数，最少为3个字节，即探测器的回响信号。如果少于3个，则直接退出。
        '2017年8月7日13:29:15

        '如果没有接收到一个字节数据，则判定通讯失败。2017年8月7日13:31:08 增加



        If Byte_ct < 1 Then
            Fesn(Comm_Loop_Id).Comm_State = Tcq.Comm_State_Enum.Comm_Fail '2017年8月7日13:31:08 增加
            ' Fesn(Comm_Loop_Id).Comm_Fail_Count = Fesn(Comm_Loop_Id).Comm_Fail_Count + 1
            Exit Sub
        End If

        'a   Fesn(Comm_Loop_Id).Comm_State = Tcq.Comm_State_Enum.Comm_Jy  '其它状态，设置为等校验状态。2017年8月7日13:31:08 增加
        'a   Fesn(Comm_Loop_Id).Comm_Fail_Count = 0   '只有收到一个字节，就清零计算。2017年8月7日13:31:08 增加


        '接收口数据过多，则只取前200个数据。
        If Byte_ct >= 200 Then
            Byte_ct = 200
        End If

        '重定义接收缓存数据数组并接收数据
        ReDim Rec_array_t(Byte_ct - 1)
        Tcq_Port.Read(Rec_array_t, 0, Byte_ct)

        '报文头判断(此处只处理FA和FC)
GID_Loop1:  '采用循环滤除判断正确报文头方式
        ' If (Rec_array_t(0) <> &HFA) And (Rec_array_t(0) <> &HFC) And (Rec_array_t(0) <> &HFB) Then

        If (Rec_array_t(0) <> &HFA) And (Rec_array_t(0) <> &HFC) And (Rec_array_t(0) <> &HFB) Then

            '如果报头不是FA 或者 FC,或者 FB则将第二个挪到第一个。。。
            If Byte_ct > 2 Then
                For i_temp = 1 To Byte_ct - 1
                    Rec_array_t(i_temp - 1) = Rec_array_t(i_temp)
                Next
                Byte_ct = Byte_ct - 1
                GoTo GID_Loop1
            End If
        End If

        '程序走到此处，如果报文头一直不对，则 Byte_ct的值很小或等于0
        '以FA和FC开始的报文，字节数量都为13个。
        If Byte_ct < 13 Then
            Exit Sub
        End If

        '判断地址是否正确
        Dim addr_t As UShort
        addr_t = Rec_array_t(1) * 256 + Rec_array_t(2)

        If Fesn(Comm_Loop_Id).id <> addr_t Then
            Exit Sub
        End If

        '判断校验码是否正确
        Dim jy As UShort
        Dim jy_h As Byte
        Dim jy_l As Byte


        If Rec_array_t(0) = &HFB Then

            If Byte_ct < 25 Then
                Dim i_ct As UInteger
                Dim i_cz As Byte


                i_cz = 25 - Byte_ct

                i_ct = 0

                '通讯修改后，，发现不能一次读完25个字节。。这里加入代码，接着读串口。
                While (i_ct < 10000)

                    If Tcq_Port.BytesToRead >= i_cz Then

                        ReDim Preserve Rec_array_t(24)

                        Dim la_ar(i_cz - 1) As Byte

                        Tcq_Port.Read(la_ar, 0, i_cz)

                        For jy = 0 To i_cz - 1
                            Rec_array_t(Byte_ct + jy) = la_ar(jy)
                        Next

                        Byte_ct = 25

                        Exit While
                    End If

                    i_ct = i_ct + 1
                End While
            End If

            If Byte_ct < 25 Then
                Exit Sub
            End If


            jy = 0
            For i_temp = 3 To 22
                jy = jy + Rec_array_t(i_temp)
                jy = jy Mod 256
            Next i_temp

            jy_h = jy \ 16
            jy_l = jy Mod 16

            '标志通讯校验码错误。。
            If jy_h <> Rec_array_t(23) Or jy_l <> Rec_array_t(24) Then
                Fesn(Comm_Loop_Id).Comm_State = Tcq.Comm_State_Enum.Comm_Jy
                Exit Sub
            End If
        Else
            jy = 0
            For i_temp = 3 To 10
                jy = jy + Rec_array_t(i_temp)
                jy = jy Mod 256
            Next i_temp

            jy_h = jy \ 16
            jy_l = jy Mod 16

            '标志通讯校验码错误。。
            If jy_h <> Rec_array_t(11) Or jy_l <> Rec_array_t(12) Then
                '  Fesn(Comm_Loop_Id).Comm_State = Tcq.Comm_State_Enum.Comm_Jy
                Exit Sub
            End If
        End If


        '报文校验正确，则标志通讯正常，标志通讯正常。
        Fesn(Comm_Loop_Id).Comm_State = Tcq.Comm_State_Enum.Comm_OK
        Fesn(Comm_Loop_Id).Comm_Fail_Count = 0

        If Rec_array_t(0) = &HFC Then
            Fesn(Comm_Loop_Id).IL = Cal_IL(Rec_array_t(3), Rec_array_t(4))
            Fesn(Comm_Loop_Id).T1 = Cal_T(Rec_array_t(5), Rec_array_t(6))   '不管有没有T1都算一次
            Fesn(Comm_Loop_Id).T2 = Cal_T(Rec_array_t(7), Rec_array_t(8))   '不管有木有T2，都算一次
            Fesn(Comm_Loop_Id).alarm = Rec_array_t(9)    '报警状态标志位
            Fesn(Comm_Loop_Id).guzhang = Rec_array_t(10) '故障标志位。

            '2017-12-16 号，增加兼容旧表通讯协议。 旧表只会发FC 实时数据。
            '代码逻辑，上位机器，在此处判断，表是否处于报警状态。如果是的，就更新本地的最大实时值

            If (Fesn(Comm_Loop_Id).alarm And &H80) = &H80 Then
                Fesn(Comm_Loop_Id).IL = Fesn(Comm_Loop_Id).IL * 10
            End If


            If (Fesn(Comm_Loop_Id).il_alarm_pop) Then   '电流报警 
                If (Fesn(Comm_Loop_Id).IL > Fesn(Comm_Loop_Id).IL_Baojin_Max) Then
                    Fesn(Comm_Loop_Id).IL_Baojin_Max = Fesn(Comm_Loop_Id).IL
                End If
            End If


            Fesn(Comm_Loop_Id).IL_BJ = Fesn(Comm_Loop_Id).IL



            If (Fesn(Comm_Loop_Id).t1_alarm_pop) Then   't1报警 
                If (Fesn(Comm_Loop_Id).T1 > Fesn(Comm_Loop_Id).T1_Baojin_Max) Then
                    Fesn(Comm_Loop_Id).T1_Baojin_Max = Fesn(Comm_Loop_Id).T1
                End If
            End If

            Fesn(Comm_Loop_Id).T1_BJ = Fesn(Comm_Loop_Id).T1


            If (Fesn(Comm_Loop_Id).t2_alarm_pop) Then   't2报警 
                If (Fesn(Comm_Loop_Id).T2 > Fesn(Comm_Loop_Id).T2_Baojin_Max) Then
                    Fesn(Comm_Loop_Id).T2_Baojin_Max = Fesn(Comm_Loop_Id).T2
                End If
            End If

            Fesn(Comm_Loop_Id).T2_BJ = Fesn(Comm_Loop_Id).T2
            '2017-12-16 号，end



        ElseIf Rec_array_t(0) = &HFB Then
            '2017-5-23日通讯协议，修改为，当探测器报警的时候，回响FB,总计25个字节。
            Fesn(Comm_Loop_Id).IL = Cal_IL(Rec_array_t(3), Rec_array_t(4))
            Fesn(Comm_Loop_Id).T1 = Cal_T(Rec_array_t(5), Rec_array_t(6))   '不管有没有T1都算一次
            Fesn(Comm_Loop_Id).T2 = Cal_T(Rec_array_t(7), Rec_array_t(8))   '不管有木有T2，都算一次
            Fesn(Comm_Loop_Id).alarm = Rec_array_t(9)    '标志状态标志位
            Fesn(Comm_Loop_Id).guzhang = Rec_array_t(10) '故障标志位。


            Fesn(Comm_Loop_Id).IL_BJ = Cal_T(Rec_array_t(11), Rec_array_t(12))

            Fesn(Comm_Loop_Id).IL_Baojin_Max = Cal_T(Rec_array_t(13), Rec_array_t(14))

            Fesn(Comm_Loop_Id).T1_BJ = Cal_T(Rec_array_t(15), Rec_array_t(16))

            Fesn(Comm_Loop_Id).T1_Baojin_Max = Cal_T(Rec_array_t(17), Rec_array_t(18))

            Fesn(Comm_Loop_Id).T2_BJ = Cal_T(Rec_array_t(19), Rec_array_t(20))

            Fesn(Comm_Loop_Id).T2_Baojin_Max = Cal_T(Rec_array_t(21), Rec_array_t(22))


            '
            If (Fesn(Comm_Loop_Id).alarm And &H1) = &H1 Then
                If Fesn(Comm_Loop_Id).IL_Baojin_Max < 300 Then
                    Fesn(Comm_Loop_Id).IL_Baojin_Max = Fesn(Comm_Loop_Id).IL_Baojin_Max * 10
                End If
                If Fesn(Comm_Loop_Id).IL_BJ < 300 Then
                    Fesn(Comm_Loop_Id).IL_BJ = Fesn(Comm_Loop_Id).IL_BJ * 10
                End If
            End If


            If (Fesn(Comm_Loop_Id).alarm And &H80) = &H80 Then
                Fesn(Comm_Loop_Id).IL = Fesn(Comm_Loop_Id).IL * 10
            End If





        Else '接收到设置参数。。。。。
            Dim temp_IL As Single
            Dim temp_T1 As Single
            Dim temp_T2 As Single
            Dim temp_type As UShort
            Dim temp_type_pc As UShort

            temp_IL = Cal_IL(Rec_array_t(3), Rec_array_t(4))
            temp_T1 = Cal_T(Rec_array_t(5), Rec_array_t(6))
            temp_T2 = Cal_T(Rec_array_t(7), Rec_array_t(8))

            'Rec_array_t(8)=0x01  独立式   ;   Rec_array_t(8)=0x02=非独立式
            'Rec_array_t(9)=00 无温度 ，01=1个温度  02=两个温度。

            temp_type = Rec_array_t(9) * 16 + Rec_array_t(10)

            Select Case temp_type
                Case Tcq_type_enum.FEFI_IL
                    temp_type_pc = Tcq_type_pc_enum.FEFI_IL

                Case Tcq_type_enum.FEFI_IL_T1
                    temp_type_pc = Tcq_type_pc_enum.FEFI_IL_T1

                Case Tcq_type_enum.FEFI_IL_T2
                    temp_type_pc = Tcq_type_pc_enum.FEFI_IL_T2

                Case Tcq_type_enum.FEFM_IL
                    temp_type_pc = Tcq_type_pc_enum.FEFM_IL

                Case Tcq_type_enum.FEFM_IL_T1
                    temp_type_pc = Tcq_type_pc_enum.FEFM_IL_T1

                Case Tcq_type_enum.FEFM_Il_T2
                    temp_type_pc = Tcq_type_pc_enum.FEFM_Il_T2

                Case Else
                    temp_type_pc = Tcq_type_pc_enum.FEFI_IL

            End Select



            temp_IL = temp_IL * 10
            temp_T1 = temp_T1 * 10
            temp_T2 = temp_T2 * 10

            '此处回响 FE +

            Dim Recall_ar(2) As Byte            '主机查询指令
            Tcq_Port.DiscardOutBuffer()

            Recall_ar(0) = &HFE
            Recall_ar(1) = Rec_array_t(1)
            Recall_ar(2) = Rec_array_t(2)

            Tcq_Port.Write(Recall_ar, 0, 3)  '将数据发送出去。
            Tcq_Port.Write(Recall_ar, 0, 3)  '将数据发送出去。
            Dim sql_str As String

            sql_str = " update tcq_info set il_bj=" & temp_IL _
            & " ,t1_bj=" & temp_T1 _
            & " ,t2_bj=" & temp_T2 _
            & " , type=" & temp_type_pc _
            & " where id=" & Fesn(Comm_Loop_Id).id
            If Sql_Exe(sql_str) Then
                Fesn(Comm_Loop_Id).IL_Baojin = temp_IL
                Fesn(Comm_Loop_Id).T1_baojin = temp_T1
                Fesn(Comm_Loop_Id).T2_baojin = temp_T2
            End If
        End If
    End Sub


    ''' <summary>
    ''' 一次读取完串口里面所有的数据，然后判断校验数据。
    ''' 数据接收采用滤除判断正确报文头和报文字节数，同时按规则校验。
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Get_Input_Data2(ByRef tcq_p As SerialPort)

        Dim Byte_ct As UShort
        Dim Rec_array_t() As Byte
        Dim i_temp As UShort

        '获取接收缓冲区的字节数
        Byte_ct = tcq_p.BytesToRead

        'If Comm_Loop_Id = 1 Then
        '    i_temp = 0
        'End If

        If Byte_ct < 1 Then
            Fesn(Comm_Loop_Id).Comm_Fail_Count = Fesn(Comm_Loop_Id).Comm_Fail_Count + 1

            If CommDataWatch Then

                If ComWatchAddr.SelectedIndex = 0 Then
                    RichTextRec.Text = RichTextRec.Text & vbCrLf & "未接收到数据！"
                    If RichTextRec.TextLength >= 10000 Then
                        RichTextRec.Text = ""
                    End If
                Else
                    If Comm_Loop_Id = ComWatchAddr.SelectedIndex - 1 Then
                        RichTextRec.Text = RichTextRec.Text & vbCrLf & "未接收到数据！"
                        If RichTextRec.TextLength >= 10000 Then
                            RichTextRec.Text = ""
                        End If
                    End If

                End If





            End If

            Exit Sub
        End If

        '接收口数据过多，则只取前200个数据。
        If Byte_ct >= 200 Then
            Byte_ct = 200
        End If

        '重定义接收缓存数据数组并接收数据
        ReDim Rec_array_t(Byte_ct - 1)
        tcq_p.Read(Rec_array_t, 0, Byte_ct)


        If CommDataWatch Then


            If ComWatchAddr.SelectedIndex = 0 Then
                Dim str_data As String
                str_data = ""
                For Each da As Byte In Rec_array_t
                    str_data = str_data & " " & Hex(da)
                Next
                RichTextRec.Text = RichTextRec.Text & vbCrLf & str_data

                If RichTextRec.TextLength >= 10000 Then
                    RichTextRec.Text = ""
                End If
            Else
                If Comm_Loop_Id = ComWatchAddr.SelectedIndex - 1 Then
                    Dim str_data As String
                    str_data = ""
                    For Each da As Byte In Rec_array_t
                        str_data = str_data & " " & Hex(da)
                    Next
                    RichTextRec.Text = RichTextRec.Text & vbCrLf & str_data

                    If RichTextRec.TextLength >= 10000 Then
                        RichTextRec.Text = ""
                    End If
                End If
            End If

        End If






        '采用循环滤除判断正确报文头方式, 起始位(&HDD)+本机地址
        '暂时只判断起始位&HDD， 日后完善增加+本地地址。
GID_Loop1:

        If (Rec_array_t(0) <> MES_HEAD) Then

            '如果报头不是&HDD,则将第二个挪到第一个。。。
            If Byte_ct > 2 Then
                For i_temp = 1 To Byte_ct - 1
                    Rec_array_t(i_temp - 1) = Rec_array_t(i_temp)
                Next
                Byte_ct = Byte_ct - 1
                GoTo GID_Loop1
            End If
        End If

        '程序走到此处，如果报文头一直不对，则 Byte_ct的值很小或等于0
        '回响报文长度9个字节。
        If Byte_ct <> 9 Then
            Fesn(Comm_Loop_Id).Comm_Fail_Count = Fesn(Comm_Loop_Id).Comm_Fail_Count + 1
            Exit Sub
        End If

        '对终端数据进行校验,调用函数定义在comm.vb 中
        If NodeRecallCheck(Fesn(Comm_Loop_Id).addr, Rec_array_t) = False Then
            'Fesn(Comm_Loop_Id).Comm_Fail_Count = Fesn(Comm_Loop_Id).Comm_Fail_Count + 1
            '标志通行校验错误。
            Fesn(Comm_Loop_Id).Comm_State = Tcq.Comm_State_Enum.Comm_Jy
            Exit Sub
        End If

        '报文校验正确，则标志通讯正常，标志通讯正常。
        Fesn(Comm_Loop_Id).Comm_State = Tcq.Comm_State_Enum.Comm_OK
        Fesn(Comm_Loop_Id).Comm_Fail_Count = 0



        Fesn(Comm_Loop_Id).alarm = Rec_array_t(3)    '报警状态标志位 0=关闭，1=打开。
        Fesn(Comm_Loop_Id).Data1 = Rec_array_t(4)    '保存音调值
        Fesn(Comm_Loop_Id).Data2 = Rec_array_t(5)    '保存音量值


    End Sub


    ''' <summary>
    '''  PC主机发送的打开/关闭/参数设置指令后
    '''  在此函数内接收终端回响
    ''' </summary>
    ''' <remarks></remarks>
    Private Function Get_Reecho_Data(ByRef tcq_p As SerialPort) As Boolean
        Dim Byte_ct As UShort
        Dim Rec_array_t() As Byte
        Dim i_temp As UShort

        Get_Reecho_Data = False

        '获取接收缓冲区的字节数
        Byte_ct = tcq_p.BytesToRead

        If Byte_ct < 1 Then
            Exit Function
        End If


        '接收口数据过多，则只取前200个数据。
        If Byte_ct >= 200 Then
            Byte_ct = 200
        End If

        '重定义接收缓存数据数组并接收数据
        ReDim Rec_array_t(Byte_ct - 1)
        tcq_p.Read(Rec_array_t, 0, Byte_ct)


        If Byte_ct < 8 Then
            Exit Function
        End If

        '采用循环滤除判断正确报文头方式, 起始位(&HDD)+本机地址
        '暂时只判断起始位&HDD， 日后完善增加+本地地址。
GRD_Loop1:

        If (Rec_array_t(0) <> MES_HEAD) Then

            '如果报头不是&HDD,则将第二个挪到第一个。。。
            If Byte_ct > 2 Then
                For i_temp = 1 To Byte_ct - 1
                    Rec_array_t(i_temp - 1) = Rec_array_t(i_temp)
                Next
                Byte_ct = Byte_ct - 1
                GoTo GRD_Loop1
            End If
        End If

        '程序走到此处，如果报文头一直不对，则 Byte_ct的值很小或等于0
        '回响报文长度9个字节。
        If Byte_ct <> 9 Then
            Exit Function
        End If

        '对终端数据进行校验,调用函数定义在comm.vb 中
        If PcSendSetAddrReechoWait Then   '设置地址，的回响
            Dim adr As Integer

            ' adr = Val(ComAddr.SelectedIndex + 1)
            adr = Fesn(ComAddr.SelectedIndex).addr

            If NodeRecallCheck(adr, Rec_array_t) = False Then
                Exit Function
            End If
        Else
            '其他的通讯回响校验 --报警打开/关闭的回响
            If NodeRecallCheck(PcSendBjqAddr, Rec_array_t) = False Then
                Fesn(PcSendBjqindex).Comm_State = Tcq.Comm_State_Enum.Comm_Jy
                Exit Function
            End If

        End If

        If PcSendAlarmOnMem = CommProgressEnum.PcNeedRec Then
            '打开报警 回响 数据第2位是 1
            If Rec_array_t(5) <> &H1 Then
                Exit Function
            End If
        End If

        If PcSendAlarmOff = CommProgressEnum.PcNeedRec Then
            '关闭报警 回响 数据第2位是 2
            If Rec_array_t(5) <> &H2 Then
                Exit Function
            End If
        End If

        BjqReechoData(0) = Rec_array_t(3)
        BjqReechoData(1) = Rec_array_t(4)
        BjqReechoData(2) = Rec_array_t(5)

        Get_Reecho_Data = True
    End Function




    ''' <summary>
    '''  PC主机发送-单机调试-自动查询
    '''  在此函数内接收终端回响
    ''' </summary>
    ''' <remarks></remarks>
    Private Function Get_Reecho_Data_Auto_Chaxun(ByRef tcq_p As SerialPort) As Boolean
        Dim Byte_ct As UShort
        Dim Rec_array_t() As Byte
        Dim i_temp As UShort

        Get_Reecho_Data_Auto_Chaxun = False

        '获取接收缓冲区的字节数
        Byte_ct = tcq_p.BytesToRead

        If Byte_ct < 8 Then
            Get_Reecho_Data_Auto_Chaxun = False
            Exit Function
        End If

        '接收口数据过多，则只取前200个数据。
        If Byte_ct >= 200 Then
            Byte_ct = 200
        End If

        '重定义接收缓存数据数组并接收数据
        ReDim Rec_array_t(Byte_ct - 1)
        tcq_p.Read(Rec_array_t, 0, Byte_ct)

        '采用循环滤除判断正确报文头方式, 起始位(&HDD)+本机地址
        '暂时只判断起始位&HDD， 日后完善增加+本地地址。
GRD_Loop1:

        If (Rec_array_t(0) <> MES_HEAD) Then

            '如果报头不是&HDD,则将第二个挪到第一个。。。
            If Byte_ct > 2 Then
                For i_temp = 1 To Byte_ct - 1
                    Rec_array_t(i_temp - 1) = Rec_array_t(i_temp)
                Next
                Byte_ct = Byte_ct - 1
                GoTo GRD_Loop1
            End If
        End If

        '程序走到此处，如果报文头一直不对，则 Byte_ct的值很小或等于0
        '回响报文长度9个字节。
        If Byte_ct < 8 Then
            Get_Reecho_Data_Auto_Chaxun = False
            Exit Function
        End If

        '对终端数据进行校验,调用函数定义在comm.vb 中
        Dim adr As Integer
        adr = Val(ComAddr.SelectedIndex + 1)
        If NodeRecallCheck(adr, Rec_array_t) = False Then
            Exit Function
        End If

        BjqReechoData(0) = Rec_array_t(3)
        BjqReechoData(1) = Rec_array_t(4)
        BjqReechoData(2) = Rec_array_t(5)
        Get_Reecho_Data_Auto_Chaxun = True
    End Function






    ''' <summary>
    ''' 主机已经发送出查询信息，等待探测器回响，等待时间20000.个计数值。。。。。
    ''' </summary>
    ''' <remarks></remarks>
    Public Function Wait_Tcq_ReCall() As Boolean

        Dim Wait_i As UShort   '循环等待计数计时=20000
        Dim Byte_ct As UShort
        Dim Rec_array(20) As Byte
        Dim Rec_array_t() As Byte
        Dim Rec_last_length As Byte
        Rec_last_length = 0

        For Wait_i = 0 To 20000
            Byte_ct = Tcq_Port.BytesToRead
            If Byte_ct > 0 Then
                ReDim Rec_array_t(Byte_ct - 1)
                Tcq_Port.Read(Rec_array_t, 0, Byte_ct)
                '将当前的数组追加到先前的数组。。。。
                Dim i_temp As Byte
                For i_temp = LBound(Rec_array_t) To UBound(Rec_array_t)
                    Rec_array(Rec_last_length) = Rec_array_t(i_temp)
                    Rec_last_length = Rec_last_length + 1
                    If Rec_last_length > 19 Then   '防止溢出。。。。。
                        Rec_last_length = 19
                    End If
                Next

                '判断和纠正信息帧
Rec_data_jiuzheng:
                If (Rec_array(0) <> &HFA) And (Rec_array(0) <> &HFC) Then
                    '如果报头不是FA 或者 FC,则将第二个挪到第一个。。。
                    If Rec_last_length > 1 Then
                        For i_temp = 1 To Rec_last_length - 1
                            Rec_array(i_temp - 1) = Rec_array(i_temp)
                        Next
                        Rec_last_length = Rec_last_length - 1
                        GoTo Rec_data_jiuzheng
                    Else
                        Rec_last_length = 0 '舍弃
                    End If
                End If

                If ((Rec_array(0) = &HFA) Or (Rec_array(0) = &HFC)) And Rec_last_length = 13 Then
                    Exit For
                End If
            Else
                GoTo nextfor
            End If

nextfor:
        Next

        If Rec_last_length <> 13 Then   '字节数目不对。
            Return False
        End If

        Dim addr_t As UShort          '判断地址 地址不对
        addr_t = Rec_array(1) * 256 + Rec_array(2)
        If Fesn(Comm_Loop_Id).id <> addr_t Then    'id值 就是地址。。。。。
            Return False
        End If

        Dim jy As UShort
        Dim jy_h As Byte
        Dim jy_l As Byte
        jy = 0
        For Wait_i = 3 To 10
            jy = jy + Rec_array(Wait_i)
            jy = jy Mod 256
        Next Wait_i

        jy_h = jy \ 16
        jy_l = jy Mod 16
        If jy_h <> Rec_array(11) Or jy_l <> Rec_array(12) Then
            Fesn(Comm_Loop_Id).Comm_State = Tcq.Comm_State_Enum.Comm_Jy   '标志通讯校验码错误。。
            Return False
        End If

        If Rec_array(0) = &HFC Then
            Fesn(Comm_Loop_Id).Comm_State = Tcq.Comm_State_Enum.Comm_OK
            Fesn(Comm_Loop_Id).IL = Cal_IL(Rec_array(3), Rec_array(4))
            Fesn(Comm_Loop_Id).T1 = Cal_T(Rec_array(5), Rec_array(6))   '不管有没有T1都算一次
            Fesn(Comm_Loop_Id).T2 = Cal_T(Rec_array(7), Rec_array(8))   '不管有木有T2，都算一次
            Fesn(Comm_Loop_Id).alarm = Rec_array(9)   '标志状态标志位
            Fesn(Comm_Loop_Id).guzhang = Rec_array(10) '故障标志位。
        Else '接收到设置参数。。。。。
            Fesn(Comm_Loop_Id).Comm_State = Tcq.Comm_State_Enum.Comm_OK
            Dim temp_IL As Single
            Dim temp_T1 As Single
            Dim temp_T2 As Single
            temp_IL = Cal_IL(Rec_array(3), Rec_array(4))
            temp_T1 = Cal_T(Rec_array(5), Rec_array(6))
            temp_T2 = Cal_T(Rec_array(7), Rec_array(8))


            '此处回响 FE +

            Dim Recall_ar(2) As Byte            '主机查询指令
            Tcq_Port.DiscardOutBuffer()

            Recall_ar(0) = &HFE
            Recall_ar(1) = Rec_array(1)
            Recall_ar(2) = Rec_array(2)

            Tcq_Port.Write(Recall_ar, 0, 3)  '将数据发送出去。



            Tcq_Port.Write(Recall_ar, 0, 3)  '将数据发送出去。



            Dim sql_str As String

            sql_str = " update tcq_info set il_bj=" & temp_IL _
            & " ,t1_bj=" & temp_T1 _
            & " ,t2_bj=" & temp_T2 _
            & " where id=" & Fesn(Comm_Loop_Id).id
            If Sql_Exe(sql_str) Then
                Fesn(Comm_Loop_Id).IL_Baojin = temp_IL
                Fesn(Comm_Loop_Id).T1_baojin = temp_T1
                Fesn(Comm_Loop_Id).T2_baojin = temp_T2
            End If
        End If
        Wait_Tcq_ReCall = True
    End Function

    ''' <summary>
    ''' 主机下发设置参数到探测器，等待探测器收到数据后回响。FD+addrh+addrl
    ''' </summary>
    ''' <remarks></remarks>
    Public Function Wait_Tcq_Pra_ReCall(ByRef tcq_p As SerialPort) As Boolean

        Dim Wait_i As UShort   '循环等待计数计时=20000
        Dim Byte_ct As UShort
        Dim Rec_array(20) As Byte
        Dim Rec_array_t() As Byte
        Dim Rec_last_length As Byte
        Rec_last_length = 0

        For Wait_i = 0 To 20000
            Byte_ct = tcq_p.BytesToRead
            If Byte_ct > 0 Then
                ReDim Rec_array_t(Byte_ct - 1)
                tcq_p.Read(Rec_array_t, 0, Byte_ct)
                '将当前的数组追加到先前的数组。。。。
                Dim i_temp As Byte
                For i_temp = LBound(Rec_array_t) To UBound(Rec_array_t)
                    Rec_array(Rec_last_length) = Rec_array_t(i_temp)
                    Rec_last_length = Rec_last_length + 1
                    If Rec_last_length > 5 Then   '防止溢出。。。。。
                        Rec_last_length = 5
                    End If
                Next

                '判断和纠正信息帧
Rec_data_jz:
                If Rec_array(0) <> &HFD Then
                    '如果报头不是Fd,则将第二个挪到第一个。。。
                    If Rec_last_length > 1 Then
                        For i_temp = 1 To Rec_last_length - 1
                            Rec_array(i_temp - 1) = Rec_array(i_temp)
                        Next
                        Rec_last_length = Rec_last_length - 1
                        GoTo Rec_data_jz
                    Else
                        Rec_last_length = 0 '舍弃
                    End If
                End If

                If (Rec_array(0) = &HFD) And Rec_last_length >= 2 Then
                    Dim addr_t As UShort          '判断地址 地址不对
                    addr_t = Rec_array(1) * 256 + Rec_array(2)
                    If Fesn(Pra_Down_Tcq_Id).id = addr_t Then    'id值 就是地址。。。。。
                        Return True
                    End If

                End If

            End If

        Next
        Return False

    End Function

    ''' <summary>
    ''' 声光提示任务，报警，故障指示灯亮。
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Zll_Work()

        If Guzhang_Alarm_Come Then

            'zll=0 表示，没有声音提示，或者被消音。
            If Zll = 0 Then
                Zll = 2  '监控报警声音，优先于故障报警声音
            End If
            Guzhang_Alarm_Come = False
            ' Guzhang_Lamp_On()
            Sys_Mute = False
            Guzhang_Lamp_Sta = True
        End If

        If Baojing_Alarm_Come Then
            Zll = 1     '监控报警声音，优先于故障报警声音
            ' Baojing_Lamp_On()
            Baojing_Alarm_Come = False
            Sys_Mute = False
            Baojing_Lamp_Sta = True
        End If

    End Sub



    ''' <summary>
    ''' 温度1报警处理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub T1_Alarm_Work()
        '2:判断温度1报警
        If Fesn(Comm_Loop_Id).T1_Enable Then
            If (Fesn(Comm_Loop_Id).alarm And &H2) = &H2 Then
                If Fesn(Comm_Loop_Id).t1_alarm_pop = False Then
                    '报警先前没有弹出,这里弹出报警，并记录报警最大值
                    Dim alarm_temp As New Alarm_info
                    alarm_temp.tcq_id_str = Fesn(Comm_Loop_Id).id_str
                    alarm_temp.tcq_id = Fesn(Comm_Loop_Id).id
                    alarm_temp.time_str = Now
                    alarm_temp.Alarm_kind = Alarm_info.alarm_enum.T1_alarm
                    alarm_temp.tcq_name = Fesn(Comm_Loop_Id).name
                    '添加报警确认和报警值两个变量 2017年4月27日00:16:19
                    alarm_temp.alarm_sure = False
                    alarm_temp.date_str = Fesn(Comm_Loop_Id).T1_BJ.ToString & "℃"
                    Baojing_Array.Add(alarm_temp)

                    Fesn(Comm_Loop_Id).t1_alarm_pop = True

                    Add_Record_To_His_Alarm(Comm_Loop_Id, 1)
                    Baojing_Alarm_Come = True
                    Jiankong_Refresh_Lable = True
                    Main_Need_Refresh_Form1 = True
                Else
                    '报警已经弹出，比较更新报警T1最大值
                    'If Fesn(Comm_Loop_Id).T1 > Fesn(Comm_Loop_Id).T1_Baojin_Max Then
                    '    Fesn(Comm_Loop_Id).T1_Baojin_Max = Fesn(Comm_Loop_Id).T1
                    'End If
                End If
            End If
        End If
    End Sub


    ''' <summary>
    ''' 温度2报警处理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub T2_Alarm_Work()
        '2-3:判断温度2报警
        If Fesn(Comm_Loop_Id).T2_Enable Then
            If (Fesn(Comm_Loop_Id).alarm And &H4) = &H4 Then
                If Fesn(Comm_Loop_Id).t2_alarm_pop = False Then
                    '报警先前没有弹出,这里弹出报警，并记录报警最大值
                    Dim alarm_temp As New Alarm_info
                    alarm_temp.tcq_id_str = Fesn(Comm_Loop_Id).id_str
                    alarm_temp.tcq_id = Fesn(Comm_Loop_Id).id
                    alarm_temp.time_str = Now
                    alarm_temp.Alarm_kind = Alarm_info.alarm_enum.T2_alarm
                    alarm_temp.tcq_name = Fesn(Comm_Loop_Id).name
                    '添加报警确认和报警值两个变量 2017年4月27日00:16:19

                    alarm_temp.alarm_sure = False
                    If (Fesn(Comm_Loop_Id).T2_BJ < 10) Then
                        Fesn(Comm_Loop_Id).T2_BJ = Fesn(Comm_Loop_Id).T2
                    End If

                    alarm_temp.date_str = Fesn(Comm_Loop_Id).T2_BJ.ToString & "℃"
                    Baojing_Array.Add(alarm_temp)
                    Fesn(Comm_Loop_Id).t2_alarm_pop = True
                    Fesn(Comm_Loop_Id).t2_alarm_pop = Fesn(Comm_Loop_Id).T2
                    'Fesn(Comm_Loop_Id).T2_Baojin_Max = Fesn(Comm_Loop_Id).T2
                    Dim txoo As TX_Send_Info
                    txoo = New TX_Send_Info
                    txoo.baojin_kind = "2"
                    txoo.happen_time = Format(Now(), "yy/MM/dd/HH/mm")
                    txoo.tcq_id = Fesn(Comm_Loop_Id).id
                    txoo.info_kind = 1
                    TX_Info_Id = TX_Info_Id + 1
                    txoo.id = TX_Info_Id
                    txoo.tongdao = "3"
                    TX_Array.Add(txoo)

                    Add_Record_To_His_Alarm(Comm_Loop_Id, 2)
                    Baojing_Alarm_Come = True
                    Jiankong_Refresh_Lable = True
                    Main_Need_Refresh_Form1 = True
                Else
                    '报警已经弹出，比较更新报警T2最大值
                    'If Fesn(Comm_Loop_Id).T2 > Fesn(Comm_Loop_Id).T2_Baojin_Max Then
                    '    Fesn(Comm_Loop_Id).T2_Baojin_Max = Fesn(Comm_Loop_Id).T2
                    'End If
                End If
            End If
        End If
    End Sub


    ''' <summary>
    ''' 判断状态是否发生改变，报警从关闭到打开：添加到消息队列。
    ''' 同时将信息记录到文件。
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub IL_Alarm_Wrok()

        Dim alarm_temp As New Alarm_info

        '标记 报警状态已经弹出。
        Fesn(Comm_Loop_Id).il_alarm_pop = True
        alarm_temp.tcq_id_str = Fesn(Comm_Loop_Id).id_str  '地址编号，字符串类型。
        alarm_temp.tcq_id = Fesn(Comm_Loop_Id).id
        alarm_temp.time_str = Now               '事件
        alarm_temp.Alarm_kind = Alarm_info.alarm_enum.IL_alarm  '报警类型。
        alarm_temp.tcq_name = Fesn(Comm_Loop_Id).name '所在位置。
        '添加报警确认和报警值两个变量 2017年4月27日00:16:19
        alarm_temp.alarm_sure = False
        alarm_temp.date_str = "报警打开"
        Baojing_Array.Add(alarm_temp)   '添加到队列

        Baojing_Alarm_Come = True       '标志系统发出报警提示，可以给PC主机自己用。
        Jiankong_Refresh_Lable = True   '标识需要刷新
        Main_Need_Refresh_Form1 = True  '标识form1 需要刷新。

    End Sub


    ''' <summary>
    ''' 探测器的监控报警事物处理(IL + T1 + T2)
    ''' 判断记录IL，T1，T2报警，并标记弹出报警
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Jiankong_Alarm_Work()

        '如果本次通讯失败，直接退出
        If Fesn(Comm_Loop_Id).Comm_State <> Tcq.Comm_State_Enum.Comm_OK Then
            Exit Sub
        End If


        If Fesn(Comm_Loop_Id).alarm Then  '报警打开-判断从关闭到打开的状态。
            '如果报警已经弹出，就返回。
            If Fesn(Comm_Loop_Id).il_alarm_pop Then
                Return
            End If
            ' 记录报警事件，更新报警器状态。
            ManOpenAlarm(Comm_Loop_Id)

        Else    '报警关闭
            '判断从打开-到关闭的状态。
            If Fesn(Comm_Loop_Id).il_alarm_pop Then
                ManCloseAlarm(Comm_Loop_Id)
            End If

        End If

    End Sub

    ''' <summary>
    ''' 记录报警事件-打开，更新报警器状态。
    ''' 此过程会被两个地方调用：
    ''' 1：通过查询方式得到的，报警器报警打开。
    ''' 2：人工下发报警指令给报警器，报警器成功执行后。
    ''' </summary>
    ''' <param name="bjdIndex">报警灯的下标</param>
    ''' <remarks></remarks>
    Private Sub ManOpenAlarm(ByVal bjdIndex As Byte)

        '如果是单机调试下，触发的，则不记录。
        If Main_Chaxun_Loop_Wait Then
            Return
        End If


        Dim alarm_temp As New Alarm_info
        '标记 报警状态已经弹出。
        Fesn(bjdIndex).il_alarm_pop = True
        Fesn(bjdIndex).alarm = 1   '主要给 调用2 用。
        alarm_temp.tcq_id_str = Fesn(bjdIndex).id_str  '地址编号，字符串类型。
        alarm_temp.tcq_id = Fesn(bjdIndex).id
        alarm_temp.time_str = Now               '事件
        alarm_temp.Alarm_kind = Alarm_info.alarm_enum.IL_alarm  '报警类型。
        alarm_temp.tcq_name = Fesn(bjdIndex).name '所在位置。
        alarm_temp.alarm_sure = False
        alarm_temp.date_str = "报警打开"
        Baojing_Array.Add(alarm_temp)   '添加到队列

        '记录报警信息到日志文件。
        Dim str_alarm As String  '报警信息字符串生成
        str_alarm = alarm_temp.date_str & " " & alarm_temp.time_str & " " & alarm_temp.tcq_id_str & "#报警器 " & alarm_temp.tcq_name
        SaveAlarmLog(str_alarm)

        Baojing_Alarm_Come = True       '标志系统发出报警提示，可以给PC主机自己用。
        Jiankong_Refresh_Lable = True   '标识需要刷新
        Main_Need_Refresh_Form1 = True  '标识form1 需要刷新。
        Form_Bjd_Need_Refresh = True   '告诉报警器的图形窗体，及时刷新。。。
        BjqAlarmCount = BjqAlarmCount + 1
        Button9.Text = "当前报警打开的报警器有" & BjqAlarmCount & "个"




    End Sub

    ''' <summary>
    ''' 记录报警事件-关闭，更新报警器状态。
    ''' </summary>
    ''' <param name="bjdIndex"></param>
    ''' <remarks></remarks>
    Private Sub ManCloseAlarm(ByVal bjdIndex As Byte)

        Dim alarm_temp As New Alarm_info
        Fesn(bjdIndex).il_alarm_pop = False
        Fesn(bjdIndex).alarm = 0   '主要给 调用2 用。
        alarm_temp.tcq_id_str = Fesn(bjdIndex).id_str  '地址编号，字符串类型。
        alarm_temp.tcq_id = Fesn(bjdIndex).id
        alarm_temp.time_str = Now               '事件
        alarm_temp.Alarm_kind = Alarm_info.alarm_enum.IL_alarm  '报警类型。
        alarm_temp.tcq_name = Fesn(bjdIndex).name '所在位置。

        alarm_temp.alarm_sure = False
        alarm_temp.date_str = "报警关闭"
        Baojing_Array.Add(alarm_temp)   '添加到队列

        '记录报警信息到日志文件。
        Dim str_alarm As String  '报警信息字符串生成
        str_alarm = alarm_temp.date_str & " " & alarm_temp.time_str & " " & alarm_temp.tcq_id_str & "#报警器 " & alarm_temp.tcq_name
        SaveAlarmLog(str_alarm)

        If BjqAlarmCount >= 1 Then
            BjqAlarmCount = BjqAlarmCount - 1
        End If
        Button9.Text = "当前报警打开的报警器有" & BjqAlarmCount & "个"
        Jiankong_Refresh_Lable = True   '标识需要刷新
        Main_Need_Refresh_Form1 = True  '标识form1 需要刷新。
        Form_Bjd_Need_Refresh = True   '告诉报警器的图形窗体，及时刷新。。。
    End Sub





    ''' <summary>
    ''' 剩余电流互感器故障报警处理：弹出故障报警or恢复正常
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub IL_Fault_Work()
        Dim guzhang_temp1 As New Guzhang_info
        Dim i_csw As UShort

        '1判断剩余电流故障
        ' 如果没有弹出il故障，则弹出，如果已经弹出，则忽略。
        If (Fesn(Comm_Loop_Id).guzhang And &H3) >= 1 Then
            If Fesn(Comm_Loop_Id).il_error_pop = False Then
                guzhang_temp1.tcq_id_str = Fesn(Comm_Loop_Id).id_str
                guzhang_temp1.tcq_id = Fesn(Comm_Loop_Id).id
                guzhang_temp1.time_str = Now
                guzhang_temp1.Guzhang_kind = Guzhang_info.Guzhang_enum.IL_Error
                guzhang_temp1.tcq_name = Fesn(Comm_Loop_Id).name
                Guzhang_Array.Add(guzhang_temp1) '故障数组，添加元素
                Fesn(Comm_Loop_Id).il_error_pop = True '标志对象。
                Guzhang_Alarm_Come = True   '故障报警激活
                Guzhang_Refresh_Lable = True '刷新显示故障框激活。
                Main_Need_Refresh_Form1 = True

                Dim txoo As TX_Send_Info
                txoo = New TX_Send_Info
                txoo.guzhang_kind = "A"  '41

                txoo.happen_time = Format(Now(), "yy/MM/dd/HH/mm")
                txoo.tcq_id = Fesn(Comm_Loop_Id).id
                txoo.info_kind = 2
                txoo.tongdao = "1"
                TX_Info_Id = TX_Info_Id + 1
                txoo.id = TX_Info_Id
                TX_Array.Add(txoo)
            End If
            '剩余电流互感器故障恢复，或者一直就是好的。
        ElseIf Fesn(Comm_Loop_Id).il_error_pop Then
            Fesn(Comm_Loop_Id).il_error_pop = False
            If Guzhang_Array.Count >= 1 Then
                For i_csw = 0 To Guzhang_Array.Count - 1
                    guzhang_temp1 = Guzhang_Array(i_csw)
                    '判断故障对象的探测器id号和故障类型都匹配
                    If (guzhang_temp1.tcq_id_str = Fesn(Comm_Loop_Id).id_str) And (guzhang_temp1.Guzhang_kind = Guzhang_info.Guzhang_enum.IL_Error) Then
                        ' Guzhang_Array.Remove(guzhang_temp2)
                        Guzhang_Array.RemoveAt(i_csw)
                        Guzhang_Refresh_Lable = True
                        Exit For
                    End If
                Next
                '这里添加故障恢复后喇叭和指示灯事物
                Close_Guzhang_Alarm()
                Dim txoo As TX_Send_Info
                txoo = New TX_Send_Info
                txoo.guzhang_kind = "A"  '41
                txoo.happen_time = Format(Now(), "yy/MM/dd/HH/mm")
                txoo.tcq_id = Fesn(Comm_Loop_Id).id
                txoo.info_kind = 3
                txoo.tongdao = "1"
                TX_Info_Id = TX_Info_Id + 1
                txoo.id = TX_Info_Id
                TX_Array.Add(txoo)
            End If
        End If
    End Sub


    ''' <summary>
    ''' 温度1故障报警处理： 弹出报警 or 恢复正常
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub T1_Fault_Work()
        Dim guzhang_temp2 As New Guzhang_info
        Dim i_csw As UShort
        '2:判断温度1故障
        If Fesn(Comm_Loop_Id).T1_Enable Then
            If (Fesn(Comm_Loop_Id).guzhang And &HC) >= 4 Then
                If Fesn(Comm_Loop_Id).t1_error_pop = False Then
                    guzhang_temp2.tcq_id_str = Fesn(Comm_Loop_Id).id_str
                    guzhang_temp2.tcq_id = Fesn(Comm_Loop_Id).id
                    guzhang_temp2.time_str = Now
                    guzhang_temp2.Guzhang_kind = Guzhang_info.Guzhang_enum.T1_Error
                    guzhang_temp2.tcq_name = Fesn(Comm_Loop_Id).name
                    Guzhang_Array.Add(guzhang_temp2)
                    Fesn(Comm_Loop_Id).t1_error_pop = True
                    Guzhang_Alarm_Come = True
                    Guzhang_Refresh_Lable = True
                    Main_Need_Refresh_Form1 = True

                    Dim txoo As TX_Send_Info
                    txoo = New TX_Send_Info
                    txoo.guzhang_kind = "B"  '41
                    txoo.happen_time = Format(Now(), "yy/MM/dd/HH/mm")
                    txoo.tcq_id = Fesn(Comm_Loop_Id).id
                    txoo.info_kind = 2
                    txoo.tongdao = "2"
                    TX_Info_Id = TX_Info_Id + 1
                    txoo.id = TX_Info_Id
                    TX_Array.Add(txoo)
                End If
            ElseIf Fesn(Comm_Loop_Id).t1_error_pop Then
                Fesn(Comm_Loop_Id).t1_error_pop = False
                If Guzhang_Array.Count >= 1 Then
                    For i_csw = 0 To Guzhang_Array.Count - 1
                        guzhang_temp2 = Guzhang_Array(i_csw)
                        If (guzhang_temp2.tcq_id_str = Fesn(Comm_Loop_Id).id_str) And (guzhang_temp2.Guzhang_kind = Guzhang_info.Guzhang_enum.T1_Error) Then
                            Guzhang_Array.Remove(guzhang_temp2)
                            Guzhang_Refresh_Lable = True
                            Exit For
                        End If
                    Next
                    '这里添加故障恢复后喇叭和指示灯事物
                    Close_Guzhang_Alarm()
                    Dim txoo As TX_Send_Info
                    txoo = New TX_Send_Info
                    txoo.guzhang_kind = "B"  '41
                    txoo.happen_time = Format(Now(), "yy/MM/dd/HH/mm")
                    txoo.tcq_id = Fesn(Comm_Loop_Id).id
                    txoo.info_kind = 3
                    txoo.tongdao = "2"
                    TX_Info_Id = TX_Info_Id + 1
                    txoo.id = TX_Info_Id
                    TX_Array.Add(txoo)
                End If
            End If
        End If
    End Sub


    ''' <summary>
    ''' 温度2故障报警处理: 弹出报警 or 恢复正常
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub T2_Fault_Work()
        Dim guzhang_temp3 As New Guzhang_info
        Dim i_csw As UShort
        '3:判断温度2报警
        If Fesn(Comm_Loop_Id).T2_Enable Then
            If (Fesn(Comm_Loop_Id).guzhang And &H30) >= &H10 Then
                If Fesn(Comm_Loop_Id).t2_error_pop = False Then
                    guzhang_temp3.tcq_id_str = Fesn(Comm_Loop_Id).id_str
                    guzhang_temp3.tcq_id = Fesn(Comm_Loop_Id).id
                    guzhang_temp3.time_str = Now
                    guzhang_temp3.Guzhang_kind = Guzhang_info.Guzhang_enum.T2_Error
                    guzhang_temp3.tcq_name = Fesn(Comm_Loop_Id).name
                    Guzhang_Array.Add(guzhang_temp3)
                    Fesn(Comm_Loop_Id).t2_error_pop = True
                    Guzhang_Alarm_Come = True
                    Guzhang_Refresh_Lable = True
                    Main_Need_Refresh_Form1 = True

                    Dim txoo As TX_Send_Info
                    txoo = New TX_Send_Info
                    txoo.guzhang_kind = "B"  '41
                    txoo.happen_time = Format(Now(), "yy/MM/dd/HH/mm")
                    txoo.tcq_id = Fesn(Comm_Loop_Id).id
                    txoo.info_kind = 2
                    txoo.tongdao = "3"
                    TX_Info_Id = TX_Info_Id + 1
                    txoo.id = TX_Info_Id
                    TX_Array.Add(txoo)
                End If
            ElseIf Fesn(Comm_Loop_Id).t2_error_pop Then
                Fesn(Comm_Loop_Id).t2_error_pop = False
                If Guzhang_Array.Count >= 1 Then
                    For i_csw = 0 To Guzhang_Array.Count - 1
                        guzhang_temp3 = Guzhang_Array(i_csw)
                        If (guzhang_temp3.tcq_id_str = Fesn(Comm_Loop_Id).id_str) And (guzhang_temp3.Guzhang_kind = Guzhang_info.Guzhang_enum.T2_Error) Then
                            Guzhang_Array.Remove(guzhang_temp3)
                            Guzhang_Refresh_Lable = True
                            Exit For
                        End If
                    Next
                    '这里添加故障恢复后喇叭和指示灯事物
                    Close_Guzhang_Alarm()
                    Dim txoo As TX_Send_Info
                    txoo = New TX_Send_Info
                    txoo.guzhang_kind = "B"  '41
                    txoo.happen_time = Format(Now(), "yy/MM/dd/HH/mm")
                    txoo.tcq_id = Fesn(Comm_Loop_Id).id
                    txoo.info_kind = 3
                    txoo.tongdao = "3"
                    TX_Info_Id = TX_Info_Id + 1
                    txoo.id = TX_Info_Id
                    TX_Array.Add(txoo)
                End If
            End If
        End If
    End Sub


    ''' <summary>
    ''' 探测器的故障报警事物处理IL + T1 + T2。 弹出故障 or 恢复正常
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Guzhang_Alarm_Work()
        '如果本次通讯失败，直接退出
        If Fesn(Comm_Loop_Id).Comm_State <> Tcq.Comm_State_Enum.Comm_OK Then
            Exit Sub
        End If

        '如果关闭 故障报警，就直接退出。
        If (ILT12_fault_on = False) Then
            Exit Sub
        End If

        IL_Fault_Work()

        T1_Fault_Work()

        T2_Fault_Work()

    End Sub

    ''' <summary>
    ''' 探测器的通讯状态事物处理
    ''' 主要判断终端是否通讯故障。
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Comm_State_Work()

        Dim guzhang_temp As New Guzhang_info

        '1: 判断通讯状态从 正常 到 故障 ，并弹出通讯故障信息。
        If Fesn(Comm_Loop_Id).Comm_Fail_Count >= Sys_comm_fail_ct Then    '通讯查询连续失败次数 > Sys_comm_fail_ct

            Fesn(Comm_Loop_Id).Comm_Fail_Count = Sys_comm_fail_ct + 1

            'comm_Fail_Sta 为终端通讯故障标识 true= 确定通讯故障   false=通讯正常
            If Fesn(Comm_Loop_Id).comm_Fail_Sta <> True Then

                Fesn(Comm_Loop_Id).comm_Fail_Sta = True   '标记这个电表，通讯已经故障
                Fesn(Comm_Loop_Id).Comm_State = Tcq.Comm_State_Enum.Comm_Fail

                '弹出通讯故障警告
                guzhang_temp.tcq_id_str = Fesn(Comm_Loop_Id).id_str
                guzhang_temp.time_str = Now
                guzhang_temp.Guzhang_kind = Guzhang_info.Guzhang_enum.Comm_Error
                guzhang_temp.tcq_name = Fesn(Comm_Loop_Id).name
                guzhang_temp.date_str = "通讯故障"
                Guzhang_Array.Add(guzhang_temp)

                '记录故障信息到日志文件。
                Dim str_alarm As String  '报警信息字符串生成
                str_alarm = guzhang_temp.date_str & " " & guzhang_temp.time_str & " " & guzhang_temp.tcq_id_str & "#报警器 " & guzhang_temp.tcq_name
                SaveAlarmLog(str_alarm)

                Guzhang_Refresh_Lable = True
                Guzhang_Alarm_Come = True
                Main_Need_Refresh_Form1 = True
                BjqFaultCount = BjqFaultCount + 1
                Button10.Text = "当前通讯故障的报警器有" & BjqFaultCount & "个"
            End If

        End If


        '2:判断通讯是否从故障 恢复到 正常状态，

        If Fesn(Comm_Loop_Id).Comm_State = Tcq.Comm_State_Enum.Comm_OK And Fesn(Comm_Loop_Id).comm_Fail_Sta = True Then
            Fesn(Comm_Loop_Id).comm_Fail_Sta = False
            Fesn(Comm_Loop_Id).Comm_Fail_Count = 0

            guzhang_temp.tcq_id_str = Fesn(Comm_Loop_Id).id_str
            guzhang_temp.time_str = Now
            guzhang_temp.Guzhang_kind = Guzhang_info.Guzhang_enum.Comm_Error
            guzhang_temp.tcq_name = Fesn(Comm_Loop_Id).name
            guzhang_temp.date_str = "通讯恢复"
            Guzhang_Array.Add(guzhang_temp)

            '记录故障信息到日志文件。
            Dim str_alarm As String  '报警信息字符串生成
            str_alarm = guzhang_temp.date_str & " " & guzhang_temp.time_str & " " & guzhang_temp.tcq_id_str & "#报警器 " & guzhang_temp.tcq_name
            SaveAlarmLog(str_alarm)


            Guzhang_Refresh_Lable = True
            Guzhang_Alarm_Come = True
            Main_Need_Refresh_Form1 = True

            If BjqFaultCount >= 1 Then
                BjqFaultCount = BjqFaultCount - 1
            End If

            Button10.Text = "当前通讯故障的报警器有" & BjqFaultCount & "个"
        End If

    End Sub




    ''' <summary>
    ''' 接收工控机的回响的数据，并判断主备电源状态同时控制主备电源指示灯
    ''' </summary>
    ''' <remarks></remarks>
    Private Function Get_ATX_Comm_Info() As Boolean
        Dim Byte_ct As UShort
        Dim Rec_array_t() As Char
        Dim z_p As Byte
        Dim b_p As Byte
        Dim i_csw As Integer

        Byte_ct = ATX_Port.BytesToRead
        Dim guzhang_temp As New Guzhang_info

        If Byte_ct >= 12 Then
            ReDim Rec_array_t(Byte_ct - 1)
            ATX_Port.Read(Rec_array_t, 0, Byte_ct)

            If Rec_array_t(0) = "C" And Rec_array_t(1) = "0" Then
                ba_v = Rec_array_t(2) & Rec_array_t(3) & Rec_array_t(4) & Rec_array_t(5)

                z_p = Val(Rec_array_t(8))   '1：表示市电正常  0：市电不正常
                b_p = Val(Rec_array_t(9))

                ''市电不正常啦。。。。发出报警
                If z_p <> 1 And Main_Power_State = 1 Then
                    main_power_fault_count = main_power_fault_count + 1
                    If main_power_fault_count >= 10 Then
                        Guzhang_Alarm_Come = True
                        Main_Power_State = &H30
                        guzhang_temp.tcq_id_str = "----"
                        guzhang_temp.time_str = Now
                        guzhang_temp.Guzhang_kind = Guzhang_info.Guzhang_enum.Main_Power_Error
                        guzhang_temp.tcq_name = "系统主电源"
                        Guzhang_Array.Add(guzhang_temp)
                        Guzhang_Refresh_Lable = True

                        Dim txoo As TX_Send_Info
                        txoo = New TX_Send_Info
                        txoo.guzhang_kind = "P"
                        txoo.happen_time = Format(Now(), "yy/MM/dd/HH/mm")
                        txoo.tcq_id = Fesn(Comm_Loop_Id).id
                        txoo.info_kind = 4
                        txoo.tongdao = "1"
                        TX_Info_Id = TX_Info_Id + 1
                        txoo.id = TX_Info_Id
                        TX_Array.Add(txoo)
                    End If
                ElseIf z_p = 1 And Main_Power_State <> 1 Then     '市电变为正常。
                    Main_Power_State = 1
                    If Guzhang_Array.Count >= 1 Then
                        For i_csw = 0 To Guzhang_Array.Count - 1
                            guzhang_temp = Guzhang_Array(i_csw)
                            If (guzhang_temp.Guzhang_kind = Guzhang_info.Guzhang_enum.Main_Power_Error) Then
                                Guzhang_Array.Remove(guzhang_temp)
                                Guzhang_Refresh_Lable = True
                                Exit For
                            End If
                        Next
                        '这里添加故障恢复后喇叭和指示灯事物
                        Close_Guzhang_Alarm()
                        Dim txoo As TX_Send_Info
                        txoo = New TX_Send_Info
                        txoo.guzhang_kind = "P"
                        txoo.happen_time = Format(Now(), "yy/MM/dd/HH/mm")
                        txoo.tcq_id = Fesn(Comm_Loop_Id).id
                        txoo.info_kind = 5
                        txoo.tongdao = "1"
                        TX_Info_Id = TX_Info_Id + 1
                        txoo.id = TX_Info_Id
                        TX_Array.Add(txoo)

                    End If
                End If

                If z_p = 1 Then
                    main_power_fault_count = 0
                End If

                ' Back_Power_State  正常=0x30=0 电压低=0x31=1  短路=0x32=2  开路=0x33=3
                ' 电池状态从正常到不正常。。。
                If b_p <> 0 And Back_Power_State = 0 Then
                    backup_power_fault_count = backup_power_fault_count + 1
                    If backup_power_fault_count >= 5 Then
                        Back_Power_State = b_p
                        Guzhang_Alarm_Come = True
                        guzhang_temp.tcq_id_str = "----"
                        guzhang_temp.time_str = Now
                        guzhang_temp.Guzhang_kind = Guzhang_info.Guzhang_enum.Backup_Power_Error
                        guzhang_temp.tcq_name = "系统备电源"
                        Guzhang_Array.Add(guzhang_temp)
                        Guzhang_Refresh_Lable = True
                        Dim txoo As TX_Send_Info
                        txoo = New TX_Send_Info
                        txoo.guzhang_kind = "P"
                        txoo.happen_time = Format(Now(), "yy/MM/dd/HH/mm")
                        txoo.tcq_id = Fesn(Comm_Loop_Id).id
                        txoo.info_kind = 6
                        txoo.tongdao = "1"
                        TX_Info_Id = TX_Info_Id + 1
                        txoo.id = TX_Info_Id
                        TX_Array.Add(txoo)
                    End If

                    '电池状态从不正常恢复到正常状态。。
                ElseIf b_p = 0 And Back_Power_State <> 0 Then
                    Back_Power_State = 0
                    Dim iii As Integer
                    iii = Guzhang_Array.Count - 1

                    If Guzhang_Array.Count >= 1 Then
                        For i_csw = 0 To iii
                            guzhang_temp = Guzhang_Array(i_csw)
                            If (guzhang_temp.Guzhang_kind = Guzhang_info.Guzhang_enum.Backup_Power_Error) Then
                                Guzhang_Array.Remove(guzhang_temp)
                                Guzhang_Refresh_Lable = True

                            End If
                        Next
                        '这里添加故障恢复后喇叭和指示灯事物
                        Close_Guzhang_Alarm()
                        Dim txoo As TX_Send_Info
                        txoo = New TX_Send_Info
                        txoo.guzhang_kind = "P"
                        txoo.happen_time = Format(Now(), "yy/MM/dd/HH/mm")
                        txoo.tcq_id = Fesn(Comm_Loop_Id).id
                        txoo.info_kind = 7
                        txoo.tongdao = "1"
                        TX_Info_Id = TX_Info_Id + 1
                        txoo.id = TX_Info_Id
                        TX_Array.Add(txoo)
                    End If
                End If

                If b_p = 0 Then
                    backup_power_fault_count = 0
                End If


                Return True
            End If
        End If

        Return False
    End Function

    ''' <summary>
    ''' 历史报警故障分为两个部分：
    ''' 1：软件开始运行以来，所检测到报警和故障。
    ''' 2：以前的报警和故障信息(存在日志文件总，软件列出文件，由用户点击查阅)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub But_His_Alarm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_His_Alarm.Click

        Sys_user_level = 0

        Panel4.Visible = False

        But_Form_Enable_Work()
        But_Data_Tree_Close()
        But_Set_Tree_Close()
        But_His_Alarm.Enabled = False
        Close_Form()
        Cur_Form = Cur_Form_Enum.His_Baojin_Form

        PanXX.Visible = False

        FormAlarmFault.MdiParent = Me
        FormAlarmFault.Show()
        FormAlarmFault.BringToFront()

        But_His_Alarm.BackColor = Color.White
        But_Data_View.Enabled = True

        Button9.Enabled = False
        Button10.Enabled = False

    End Sub

    ''' <summary>
    '''  将当前的窗体button使能
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub But_Form_Enable_Work()

        '将所有按钮背景色改为 powerblue

        Select Case Cur_Form
            Case Cur_Form_Enum.Data_map_view_form
                But_Data_View.Enabled = True
                But_Data_View.BackColor = Color.PowderBlue

            Case Cur_Form_Enum.Data_Table_View_Form
                But_Data_View.Enabled = True
                But_Data_View.BackColor = Color.PowderBlue
            Case Cur_Form_Enum.His_Baojin_Form
                But_His_Alarm.Enabled = True
                But_His_Alarm.BackColor = Color.PowderBlue
            Case Cur_Form_Enum.Sys_Weihu_Form
                But_Sys_Info.Enabled = True
                But_Sys_Info.BackColor = Color.PowderBlue
            Case Cur_Form_Enum.Sys_Net_Form
                But_Sys_Tcq_Net.Enabled = True
                But_Sys_Tcq_Net.BackColor = Color.PowderBlue


            Case Cur_Form_Enum.Sys_Reset_Form
                But_Sys_Reset.Enabled = True
                But_Sys_Reset.BackColor = Color.PowderBlue
            Case Cur_Form_Enum.Sys_SelfCheck_Form
                But_Sys_SelfCheck.Enabled = True
                But_Sys_SelfCheck.BackColor = Color.PowderBlue
            Case Cur_Form_Enum.Sys_Pra_Set_Form
                But_Pra_Set.Enabled = True
                But_Pra_Set.BackColor = Color.PowderBlue
            Case Cur_Form_Enum.Tcq_Pra_Set_Form
                But_Pra_Set.Enabled = True
                But_Pra_Set.BackColor = Color.PowderBlue
        End Select
    End Sub

    ''' <summary>
    ''' 系统参数设置按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub But_Sys_Pra_Set_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Sys_Pra_Set.Click

        But_Tcq_Pra_Set.Enabled = True
        But_Sys_Pra_Set.Enabled = False
        But_Tcq_Pra_Set.BackColor = Color.PowderBlue
        But_Sys_Pra_Set.BackColor = Color.White
        Close_Form()
        Cur_Form = Cur_Form_Enum.Sys_Pra_Set_Form
        Admin.MdiParent = Me
        Admin.Show()
        Admin.BringToFront()

    End Sub

    ''' <summary>
    ''' 探测器参数设置按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub But_Tcq_Pra_Set_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Tcq_Pra_Set.Click
        But_Tcq_Pra_Set.Enabled = False
        But_Sys_Pra_Set.Enabled = True

        But_Tcq_Pra_Set.BackColor = Color.White
        But_Sys_Pra_Set.BackColor = Color.PowderBlue

        Form_Set_Tcq.MdiParent = Me
        Form_Set_Tcq.Show()
        Form_Set_Tcq.Refresh()
        Form_Set_Tcq.BringToFront()
        Close_Form()
        Cur_Form = Cur_Form_Enum.Tcq_Pra_Set_Form


    End Sub

    ''' <summary>
    ''' 探测器布线网络按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub But_Sys_Tcq_Net_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Sys_Tcq_Net.Click

        But_Form_Enable_Work()
        But_Data_Tree_Close()
        But_Set_Tree_Close()

        Close_Form()
        But_Sys_Tcq_Net.Enabled = False
        But_Sys_Tcq_Net.BackColor = Color.White

        Cur_Form = Cur_Form_Enum.Sys_Net_Form

        GroupBox8.Top = Panel4.Top
        GroupBox8.Left = Panel4.Left
        GroupBox8.Height = Panel4.Height
        GroupBox8.Width = Panel4.Width
        GroupBox8.Show()

        Panel3.Height = (Panel8.Height - Panel3.Top * 4) \ 2

        Panel7.Height = Panel3.Height
        Panel7.Top = Panel3.Top * 2 + Panel3.Height









    End Sub


    ''' <summary>
    ''' 单机调试
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub But_Sys_SelfCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Sys_SelfCheck.Click
        If MessageBox.Show("进入单机通讯调试后，系统《实时监控》功能模块将停止，是否继续？", "确定操作", MessageBoxButtons.OKCancel) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If


        If Sys_Need_Pass Then
            Login_event = 5    '事件1
            Login_Need_Level = User_Level_Enum.Maner
            Login_Mes = "请输入'管理员'的密码！！！"
            LoginForm1.Show(Me)
        Else
            But_Sys_SelfCheck_Click1()
        End If


    End Sub



    Public Sub But_Sys_SelfCheck_Click1()
        But_Data_View.Text = "请重启软件"
        But_Data_View.ForeColor = Color.Red
        But_Table_View.Text = "功能已停止"
        But_Map_View.Text = "请重启软件"
        But_Table_View.ForeColor = Color.Red
        But_Map_View.ForeColor = Color.Red
        '标记主程序查询暂停
        Main_Chaxun_Loop_Wait = True

        But_Sys_SelfCheck.BackColor = Color.White

        But_Form_Enable_Work()
        But_Data_Tree_Close()
        But_Set_Tree_Close()

        But_Sys_SelfCheck.Enabled = False
        Close_Form()


        '原自检窗体，改为单机调试界面
        Cur_Form = Cur_Form_Enum.Sys_SelfCheck_Form

        PanDanUIIinit()

        GroupBox1.Enabled = True
        GroupBox4.Enabled = True

        PanAutoChaxun.Visible = True

        PanDan.Visible = True
        LaPass.Text = 1
        Panel4.Visible = True

        GroupBox1.Visible = True
        GroupBox5.Visible = True
        GroupBox4.Visible = True
        GroupBox6.Visible = True

        PanDan.BringToFront()

        If TCQ_Usart_State <> True Then
            GroupBox5.Enabled = False
            GroupBox4.Enabled = False
            GroupBox6.Enabled = False
        Else
            Button1.Text = "串口已打开"
            GroupBox1.Enabled = True
            GroupBox5.Enabled = True
            GroupBox4.Enabled = True
            GroupBox6.Enabled = True
        End If

        BjqCommCt = 0
        Label28.Text = BjqCommCt.ToString

        '默认的查询间隔时间为1秒钟查询一次。
        ComboBox2.SelectedIndex = 3
        ComAddr.SelectedIndex = 0
        LaSetAddr.Visible = False
        Label10.Text = ComAddr.Text

    End Sub
 


    ''' <summary>
    ''' 系统复位按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub But_Sys_Reset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Sys_Reset.Click
        If MessageBox.Show("是否确定软件复位操作？", "确定", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            If Sys_Need_Pass Then

                Login_event = 4
                Login_Need_Level = User_Level_Enum.Oper
                Login_Mes = "请输入'操作员'的密码！！！"
                LoginForm1.Show(Me)
            Else
                Sys_Close()
                WillClose = True
                Application.Restart()
            End If
        End If

    End Sub

    Public Function SysRestart() As Boolean
        Sys_Close()
        WillClose = True
        Application.Restart()
    End Function



    Public Sub But_Sys_Reset_Click1()
        Sys_Close()
        WillClose = True
        Application.Restart()
    End Sub


    ''' <summary>
    '''  实时信息显示按钮-分图形显示和表格显示。默认显示图形
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub But_Data_View_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Data_View.Click

        Sys_user_level = 0

        Panel4.Visible = False
        But_Data_Tree_Open()
        But_Form_Enable_Work()
        Close_Form()

        Form_Bjd.MdiParent = Me
        Form_Bjd.Visible = True
        Form_Bjd.Show()
        Form_Bjd.BringToFront()

        But_Data_View.Enabled = False   '菜单-父级按键
        But_Table_View.Enabled = True   '图标显示

        But_Map_View.BackColor = Color.White '当前窗体显示按钮-设置白色。
        But_Map_View.Enabled = False        '同时禁止再次点击。

        But_Table_View.BackColor = Color.PowderBlue
        Cur_Form = Cur_Form_Enum.Data_map_view_form


    End Sub

    ''' <summary>
    ''' 数据图表显示按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub But_Table_View_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Table_View.Click
        Sys_user_level = 0
        Form1.MdiParent = Me
        Form1.Show()
        Form1.BringToFront()

        But_Table_View.Enabled = False
        But_Table_View.BackColor = Color.White

        But_Map_View.Enabled = True
        But_Map_View.BackColor = Color.PowderBlue

        Close_Form() '关闭先去打开的窗体。

        Cur_Form = Cur_Form_Enum.Data_Table_View_Form


        'But_Fault_View.BackColor = Color.PowderBlue
    End Sub

    ''' <summary>
    ''' 图形显示报警灯
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub But_Map_View_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Map_View.Click
        Sys_user_level = 0
        Form_Bjd.MdiParent = Me
        Form_Bjd.Visible = True
        Form_Bjd.Show()
        Form_Bjd.BringToFront()
        But_Data_View.Enabled = False
        But_Table_View.Enabled = True
        Close_Form()
        But_Map_View.BackColor = Color.White
        But_Table_View.BackColor = Color.PowderBlue
        Cur_Form = Cur_Form_Enum.Data_map_view_form

    End Sub


    ''' <summary>
    '''  故障信息显示查询窗体
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub But_Fault_View_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Fault_View.Click

        Sys_user_level = 0
        Panel4.Visible = False

        But_Fault_View.Enabled = False
        But_Table_View.Enabled = True
        But_Map_View.Enabled = True
        Close_Form()
        Cur_Form = Cur_Form_Enum.Sys_Fault_Form

        Form_Fault.MdiParent = Me
        Form_Fault.Show()
        Form_Fault.BringToFront()

        But_Map_View.BackColor = Color.PowderBlue
        But_Fault_View.BackColor = Color.White
        But_Table_View.BackColor = Color.PowderBlue

    End Sub

    '''' <summary>
    '''' 系统锁定按钮
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
    '    Sys_Lock = True
    '    LoginForm1.Show()
    'End Sub

    '''' <summary>
    '''' 消音按钮
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
    '    Sys_Mute = True
    '    Speaker_Off()
    '    Zll = 0
    'End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'map1.CurrentTool = MapXLib.ToolConstants.miZoomInTool
        'map1.MousePointer = MapXLib.CursorConstants.miZoomInCursor
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'map1.CurrentTool = MapXLib.ToolConstants.miZoomOutTool
        'map1.MousePointer = MapXLib.CursorConstants.miZoomOutCursor
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'map1.MousePointer = MapXLib.CursorConstants.miPanCursor
        'map1.CurrentTool = MapXLib.ToolConstants.miPanTool
    End Sub


    ''' <summary>
    ''' 监控报警声响测试
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Baojing_Speaker_Test()
        '闪烁测试: 
        timer3_ct = 2
        timer3_kind = 5
        Timer3.Interval = 100
        Timer3.Enabled = True
        Speaker_On()
    End Sub

    ''' <summary>
    ''' 故障报警声响测试
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Guzhang_Speaker_Test()
        '闪烁测试: 
        timer3_ct = 0
        timer3_kind = 3
        Timer3.Interval = 100
        Timer3.Enabled = True
        Speaker_On()
    End Sub


    '''' <summary>
    '''' 系统布线图，全屏显示按钮
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
    '    If Button11.Text = "全" Then
    '        Panel4.Width = Me.Width
    '        Panel4.Height = Me.Height
    '        Panel4.Left = 0
    '        Panel4.Top = 0

    '        'map1.Size = New Size(New Point(1150, 800))
    '        GroupBox1.Width = Panel4.Width
    '        Button11.Text = "局"
    '    Else
    '        Panel4.Width = 910
    '        Panel4.Height = 580
    '        Panel4.Left = 180
    '        Panel4.Top = 90

    '        Button11.Text = "全"
    '        GroupBox1.Width = Panel4.Width
    '    End If


    'End Sub

    ''' <summary>
    ''' 主窗体隐藏自己的所有控件
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Hide_All_Controls()
        Me.Panel1.Visible = False
        Me.Panel4.Visible = False
        Me.Panel6.Visible = False
        Me.Panel5.Visible = False


    End Sub


    Private Sub But_Pra_Set_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Pra_Set.Click
        'If Sys_user_level < 2 Then

        '    Login_event = 1    '事件1
        '    Login_Level = 2    '权限至少大于等于1

        '    Login_Need_Level = "管理员"
        '    Login_Mes = "请输入'管理员'的密码！！！"
        '    LoginForm1.Show(Me)

        'Else
        '    But_Pra_Set_Click_2()
        'End If


        If Sys_Need_Pass Then
            Login_event = 1    '事件1
            Login_Level = 2    '权限至少大于等于1
            Login_Need_Level = User_Level_Enum.Maner
            Login_Mes = "请输入'管理员'的密码！！！"
            LoginForm1.Show(Me)
        Else
            But_Pra_Set_Click_2()
        End If

    End Sub

    '承接上面的响应事件
    Public Sub But_Pra_Set_Click_2()
        Panel4.Visible = False
        But_Set_Tree_Open()   '按钮树的折叠与显示
        But_Form_Enable_Work()
        But_Pra_Set.Enabled = False
        But_Tcq_Pra_Set.Enabled = False
        But_Sys_Pra_Set.Enabled = True
        Close_Form()
        Cur_Form = Cur_Form_Enum.Tcq_Pra_Set_Form
        Form_Set_Tcq.MdiParent = Me
        Form_Set_Tcq.Show()
        Form_Set_Tcq.BringToFront()
        But_Tcq_Pra_Set.BackColor = Color.White
        But_Sys_Pra_Set.BackColor = Color.PowderBlue
    End Sub


    ''' <summary>
    ''' 全屏按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Panel4.Width = Me.Width
        Panel4.Height = Me.Height
        Panel4.Left = 0
        Panel4.Top = 0



    End Sub

    ''' <summary>
    ''' 缩小按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Panel4.Top = Form1.Top
        Panel4.Left = Form1.Left
        Panel4.Width = Form1.Width
        Panel4.Height = Form1.Height


        'map1.Top = GroupBox1.Top + GroupBox1.Height + 6
        'map1.Left = 5
        'map1.Width = Panel4.Width - 10
        'map1.Height = Panel4.Height - 'map1.Top - 5

    End Sub



    Private Sub But_Sys_Info_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Sys_Info.Click

        MessageBox.Show("此功能暂时不可用")
        Return

        'Sys_user_level = 0
        'Panel4.Visible = False
        'But_Form_Enable_Work()
        'But_Data_Tree_Close()
        'But_Set_Tree_Close()
        'But_Sys_Info.Enabled = False
        'Close_Form()
        'Cur_Form = Cur_Form_Enum.Sys_Weihu_Form
        'Form_Help.MdiParent = Me
        'Form_Help.Show()
        'Form_Help.BringToFront()
        'But_Sys_Info.BackColor = Color.White
    End Sub

    Public Sub But_Sys_Info_Click1()
        'But_Sys_Info.BackColor = Color.White
        'Panel4.Visible = False
        'But_Form_Enable_Work()
        'But_Data_Tree_Close()
        'But_Set_Tree_Close()

        'But_Sys_Info.Enabled = False
        'Close_Form()
        'Cur_Form = Cur_Form_Enum.Sys_Weihu_Form
        'Form_Weihu.MdiParent = Me
        'Form_Weihu.Show()
        'Form_Weihu.BringToFront()
    End Sub




    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        Dim send_ar(4) As Byte
        Dim jy_t As UShort
        Tcq_Port.DiscardOutBuffer()
        send_ar(0) = &HF1
        send_ar(1) = Fesn(Comm_Loop_Id).id \ 256
        send_ar(2) = Fesn(Comm_Loop_Id).id Mod 256
        jy_t = send_ar(0)
        jy_t = jy_t + send_ar(1)
        jy_t = jy_t + send_ar(2)
        jy_t = jy_t Mod 256
        send_ar(3) = jy_t \ 16
        send_ar(4) = jy_t Mod 16
        Fesn(Comm_Loop_Id).Comm_State = Tcq.Comm_State_Enum.Comm_Fail '先假设通讯失败
        Tcq_Port.Write(send_ar, 0, 5)  '将数据发送出去。
        Comm_Loop_Id = Comm_Loop_Id + 1


        If Comm_Loop_Id >= Sys_node_count Then
            Timer3.Enabled = False
        End If

    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Form3.MdiParent = Me
        Form3.Show()
        Form3.BringToFront()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ' System.Diagnostics.Process.Start("mysqldump", "-uroot -p885200 fefs_db > e:/3.sql")
        'Shell("cmd.exe /c mysqldump -uroot -p885200 fefs_db > e:/3.sql", AppWinStyle.NormalFocus)

        Dim tso As New TX_Send_Info

        TX_Info_Id = TX_Info_Id + 1
        With tso
            .baojin_kind = 1
            .id = TX_Info_Id
            .info_kind = 1
            .tcq_id = TX_Info_Id
            .happen_time = Format(Now(), "yy/MM/dd/HH/mm")
        End With

        TX_Array.Add(tso)


    End Sub

    ''' <summary>
    ''' 故障恢复后清除故障信息框信息同时关闭故障声音。
    ''' 实现逻辑：若当前没有处于监控报警状态,那么当有故障报警恢复之后，
    ''' 就进行消音操作，同时判断，故障是否全部没有，则关闭故障指示灯
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Close_Guzhang_Alarm()
        'If (Sys_Mute <> True) And (Zll = 2) Then '喇叭在响，故障报警
        If Zll = 2 Then '喇叭在响，故障报警
            Sys_Mute = True
            Speaker_Off()  '关闭声音
        End If

        '判断故障列表为空，然后关闭故障指示灯
        If Guzhang_Array.Count <= 0 Then
            Guzhang_Lamp_Off()
            Guzhang_Lamp_Sta = False
        End If

    End Sub


    Private Sub La_Main_Power_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles La_Main_Power.DoubleClick
        'Sys_Close()
        'If Cur_Form = Cur_Form_Enum.Data_map_view_form Then
        '    Form_Bjd.DisposeBJD()
        '    Form_Bjd.Close()
        'End If
        'Application.Exit()
        ''End
    End Sub


    Private Sub Button11_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Panel4.Visible = True
        Panel4.BringToFront()
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click

        ' MessageBox.Show("99")
        Sys_user_level = 0

        Panel4.Visible = False

        But_Form_Enable_Work()
        But_Data_Tree_Close()
        But_Set_Tree_Close()
        But_His_Alarm.Enabled = False
        Close_Form()
        Cur_Form = Cur_Form_Enum.His_Baojin_Form

        PanXX.Visible = False

        FormAlarmFault.MdiParent = Me
        FormAlarmFault.Show()
        FormAlarmFault.BringToFront()

        But_His_Alarm.BackColor = Color.White
        But_Data_View.Enabled = True

        Button9.Enabled = False
        Button10.Enabled = False

    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Sys_user_level = 0

        Panel4.Visible = False

        But_Form_Enable_Work()
        But_Data_Tree_Close()
        But_Set_Tree_Close()
        But_His_Alarm.Enabled = False
        Close_Form()
        Cur_Form = Cur_Form_Enum.His_Baojin_Form

        PanXX.Visible = False

        FormAlarmFault.MdiParent = Me
        FormAlarmFault.Show()
        FormAlarmFault.BringToFront()

        But_His_Alarm.BackColor = Color.White
        But_Data_View.Enabled = True

        Button9.Enabled = False
        Button10.Enabled = False

    End Sub


    ''' <summary>
    '''  用户点击 报警和故障信息总条数按钮是，调用的显示窗体过程。
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Alarm_Guzhang_Form_show()
        'If Cur_Form = Cur_Form_Enum.Data_map_view_form Then
        '    Exit Sub
        'End If
        'But_Map_View.Enabled = False
        'But_Table_View.Enabled = True
        'But_Fault_View.Enabled = True

        'Panel2.Top = Panel4.Top
        'Panel2.Height = Me.Height - Panel2.Top - 20
        'Panel3.Top = Panel4.Top
        'Panel3.Height = Me.Height - Panel2.Top - 20
        'Panel2.BringToFront()
        'Panel3.BringToFront()

        'If RenZhen Then '如果是认证软件，则不显示datagridview1和datagridview2.
        '    DataGridView1.Visible = False
        '    DataGridView2.Visible = False

        '    GroupBox2.Top = DataGridView1.Top + 15
        '    GroupBox3.Top = DataGridView2.Top + 15
        '    GroupBox2.Left = DataGridView1.Left
        '    GroupBox3.Left = DataGridView2.Left


        '    If DataGridView1.Width > GroupBox2.Width Then
        '        GroupBox2.Left = (Panel2.Width - GroupBox2.Width) / 2
        '        GroupBox3.Left = (Panel3.Width - GroupBox3.Width) / 2
        '    End If

        '    GroupBox2.Visible = True
        '    GroupBox3.Visible = True


        '    Man_Find_GZ_Index = 2
        '    But_GZ_Find_Pre_Click1()  '初始化显示第一条数据


        'Else
        '    DataGridView1.Visible = True
        '    DataGridView2.Visible = True
        '    DataGridView1.Height = Panel2.Height
        '    DataGridView2.Height = Panel2.Height
        'End If


        'But_Map_View.BackColor = Color.White
        ''But_Fault_View.BackColor = Color.PowderBlue
        'But_Table_View.BackColor = Color.PowderBlue

        'Close_Form()
        'Cur_Form = Cur_Form_Enum.Data_map_view_form
    End Sub


    ''' <summary>
    ''' 软件软复位。 依据国标要求，软件复位后在20ms内，仍存在的故障和报警能建立。
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Sys_Soft_Reset()

    End Sub


    ''' <summary>
    ''' 探测器自检程序，配套自检模块
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Tcq_Self_Check()

    End Sub



    ''' <summary>
    ''' 恢复探测器的报警操作。。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Label2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label2.DoubleClick

        'Dim b1c_i As UShort

        'Tcq_Self_Check_Array.Clear()

        'For b1c_i = 0 To Sys_node_count - 1
        '    Dim tcq_self As New Tcq_Self_Check
        '    tcq_self.Tcq_id = Fesn(b1c_i).id
        '    tcq_self.Self_Check_Result = 2
        '    Tcq_Self_Check_Array.Add(tcq_self)
        'Next


        'Mute_Use_Tcq_Comm = True
        'Mute_Use_Tcq_Comm_OK = False
        'Mute_Flag = 0


    End Sub


    ''' <summary>
    ''' 双击时间标签，可以发送屏蔽 到所有探测器，屏蔽所有报警。同时主机也不再进行监控和故障报警。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Label_Time_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label_Time.DoubleClick

        'Dim b1c_i As UShort

        'Tcq_Self_Check_Array.Clear()

        'For b1c_i = 0 To Sys_node_count - 1
        '    Dim tcq_self As New Tcq_Self_Check
        '    tcq_self.Tcq_id = Fesn(b1c_i).id
        '    tcq_self.Self_Check_Result = 2
        '    Tcq_Self_Check_Array.Add(tcq_self)
        'Next


        'Mute_Use_Tcq_Comm = True
        'Mute_Use_Tcq_Comm_OK = False
        'Mute_Flag = 1
    End Sub

    ''' <summary>
    '''  向数据库His_Alarm表中添加一条报警记录。
    ''' </summary>
    ''' <param name="tcq_id">发生报警的探测器的id属性</param>
    ''' <param name="alarm_kind">报警类型:0=IL报警,1=T1报警,2=T2报警</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Add_Record_To_His_Alarm1(ByVal tcq_id As UInteger, ByVal alarm_kind As Byte) As Boolean
        Dim sql_str As String
        Dim date_time(2) As String

        Dim t1_value As String
        Dim t2_value As String

        Try
            If Fesn(tcq_id).T1_Enable Then
                t1_value = Fesn(tcq_id).T1.ToString

                '如果探测器故障，则
                If Fesn(tcq_id).t1_error_pop = True Then
                    t1_value = " F "
                End If

            Else
                t1_value = "---"
            End If

            If Fesn(tcq_id).T2_Enable Then
                t2_value = Fesn(tcq_id).T2.ToString
                If Fesn(tcq_id).t2_error_pop = True Then
                    t2_value = " F "
                End If
            Else
                t2_value = "---"
            End If
            'insert into his_alarm values('2015-5-4','11:12:15','1层井道',0,900.5,21.1,21.2);

            date_time = Split(Now.ToString, , 2)

            If alarm_kind = 0 Then
                sql_str = "insert into his_alarm values('" & date_time(0) & "','" & date_time(1) & "','" _
                          & Fesn(tcq_id).id_str & "', '" _
                           & Fesn(tcq_id).name & "'," _
                           & alarm_kind & "," _
                           & Fesn(tcq_id).IL_BJ.ToString & "," _
                           & t1_value & "," _
                           & t2_value & ")"


            ElseIf alarm_kind = 1 Then
                sql_str = "insert into his_alarm values('" & date_time(0) & "','" & date_time(1) & "','" _
                           & Fesn(tcq_id).id_str & "', '" _
                          & Fesn(tcq_id).name & "'," _
                          & alarm_kind & "," _
                          & Fesn(tcq_id).IL & "," _
                          & Fesn(tcq_id).T1_BJ & "," _
                          & t2_value & ")"
            Else
                sql_str = "insert into his_alarm values('" & date_time(0) & "','" & date_time(1) & "','" _
                           & Fesn(tcq_id).id_str & "', '" _
                          & Fesn(tcq_id).name & "'," _
                          & alarm_kind & "," _
                          & Fesn(tcq_id).IL & "," _
                          & t1_value & "," _
                          & Fesn(tcq_id).T2_BJ & ")"
            End If
            'ocm1.CommandText = sql_str
            'ocm1.ExecuteNonQuery()
        Catch ex As Exception
        End Try
    End Function

    'Private Sub La_Main_Power_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles La_Main_Power.Click

    '    Sys_Close()

    '    If Cur_Form = Cur_Form_Enum.Data_map_view_form Then
    '        Form_Bjd.DisposeBJD()
    '        Form_Bjd.Close()
    '    End If

    'End Sub


    '广播-让所有的报警灯停止报警
    Private Sub PicAllOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PicAllOff.Click
        If MessageBox.Show("是否确定操作？关闭所有报警器报警？", "请确定", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        '广播协议不用接收回响，此处，直接停止主程序的通讯，
        '在此处使用串口，发送2次广播协议，每次间隔1000秒
        Timer1.Stop()
        Timer1.Enabled = False

        '1:生成广播通讯，数据帧
        CommAlarmAllOff(PcSendToBjq)
        Tcq_Port.DiscardOutBuffer()         '清空串口发送缓存区
        Tcq_Port.Write(PcSendToBjq, 0, 9)   '将数据发送出去。
        Tcq_Port.DiscardInBuffer()
        System.Threading.Thread.Sleep(500)

        ' CommAlarmAllOff(PcSendToBjq)
        Tcq_Port.DiscardOutBuffer()         '清空串口发送缓存区
        Tcq_Port.Write(PcSendToBjq, 0, 9)   '将数据发送出去。
        Tcq_Port.DiscardInBuffer()
        System.Threading.Thread.Sleep(500)
        Timer1.Enabled = True
    End Sub

    '广播- 让所有的报警灯报警
    Private Sub PicAllOn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PicAllOn.Click

        If MessageBox.Show("是否确定操作？打开所有报警器报警？", "请确定", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        '广播协议不用接收回响，此处，直接停止主程序的通讯，
        '在此处使用串口，发送2次广播协议，每次间隔1000秒
        Timer1.Stop()
        Timer1.Enabled = False

        '1:生成广播通讯，数据帧
        CommAlarmAllOn(PcSendToBjq)
        Tcq_Port.DiscardOutBuffer()         '清空串口发送缓存区
        Tcq_Port.Write(PcSendToBjq, 0, 9)   '将数据发送出去。
        Tcq_Port.DiscardInBuffer()
        System.Threading.Thread.Sleep(500)

        'CommAlarmAllOn(PcSendToBjq)
        Tcq_Port.DiscardOutBuffer()         '清空串口发送缓存区
        Tcq_Port.Write(PcSendToBjq, 0, 9)   '将数据发送出去。
        Tcq_Port.DiscardInBuffer()
        System.Threading.Thread.Sleep(500)
        Timer1.Enabled = True

    End Sub


    ''' <summary>
    ''' 系统软复位
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SysReset()
        Timer1.Enabled = False

        If TCQ_Usart_State Then

            '重置相关的变量
            Dim i As Integer
            For i = 0 To Sys_node_count - 1
                Fesn(i).alarm = 0
                Fesn(i).comm_Fail_Sta = False
                Fesn(i).Comm_State = Tcq.Comm_State_Enum.Comm_Wait
                Fesn(i).Data1 = 0
                Fesn(i).Data2 = 0
                Fesn(i).il_alarm_pop = False
            Next

            PcSendAlarmOff = CommProgressEnum.free
            PcSendAlarmOnMem = CommProgressEnum.free

            Baojing_Array.Clear()
            Guzhang_Array.Clear()
            BjqFaultCount = 0
            BjqAlarmCount = 0

            Main_Need_Refresh_Form1 = True



            ' 开启第一次通讯查询任务
            Comm_Loop_Id = 0 '通讯循环标志变量
            '发送第一个终端的查询指令
            Send_ChaXun_Order(Fesn(Comm_Loop_Id).id, Tcq_Port)
            Timer1_60ms_Ct = 0 '60ms周期时间计数器，服务于timer1中断。
            Timer1.Interval = 100
            Timer1.Enabled = True  '开启定时器



        End If



    End Sub

    ''' <summary>
    '''  设置报警器的地址。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButSetBjqAddr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButSetBjqAddr.Click

        Dim addr As Integer

        addr = Val(ComAddr.SelectedIndex + 1)

        If addr <= 0 Or addr > BJQMAXADDR Then
            MessageBox.Show("地址超限！")
            Exit Sub
        End If

        '生成通讯数据
        CommSetAddr(addr, PcSendToBjq)
        Send_Other_Order(Tcq_Port)

        PcSendSetAddrReechoWait = True
        Time_Message_Fresh_Count = 0

        LaSetAddr.Text = "发送中"
        LaSetAddr.Visible = True

    End Sub


    Private Sub ComAddr_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComAddr.SelectedIndexChanged
        LaSetAddr.Visible = False
        Label10.Text = ComAddr.Text
    End Sub


    ''' <summary>
    ''' 检查串口是否可用
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        '检查当前串口号。
        Dim ckh As Byte
        Dim btl As Byte
        Dim str_btl As Integer
        Dim str_com As String


        ckh = ComCom.SelectedIndex + 1
        btl = ComBtl.SelectedIndex + 1 '波特率

        If btl = 1 Then
            str_btl = 4800
        ElseIf btl = 2 Then
            str_btl = 9600
        ElseIf btl = 3 Then
            str_btl = 19200
        ElseIf btl = 4 Then
            str_btl = 38400
        End If

        str_com = "com" & ckh.ToString

        Try
            '先关闭当前的串口
            If TCQ_Usart_State = True Then
                Tcq_Port.Close()
            End If

            '打开其他串口
            Tcq_Port = New SerialPort(str_com, str_btl, Parity.None, 8, StopBits.One)
            Tcq_Port.ReadBufferSize = 1024

            Tcq_Port.WriteBufferSize = 1024
            Tcq_Port.ReceivedBytesThreshold = 1
            Tcq_Port.Open()
            TCQ_Usart_State = True
            MessageBox.Show("串口打开成功")

            Label28.Text = BjqCommCt.ToString

            GroupBox4.Enabled = True
            GroupBox5.Enabled = True
            GroupBox6.Enabled = True

        Catch ex As Exception
            TCQ_Usart_State = False
            MessageBox.Show("串口打开失败！")
        End Try



    End Sub

    ''' <summary>
    ''' 打开报警，记忆音乐循环播放
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click

        '加入密码权限验证。
        If PasswordCheck() = False Then
            Exit Sub
        End If


        PcSendBjqAddr = Val(Label10.Text)
        PcSendBjqindex = Val(LaBjqIndex.Text)

        CommAlarmOnMem(PcSendBjqAddr, PcSendToBjq)
        PcSendAlarmOnMem = CommProgressEnum.PcNeedSend

        LaFun.Text = "打开报警(记忆音乐循环播放)-通讯发送中......"
        LaFun.ForeColor = Color.Black

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Function PasswordCheck() As Boolean
        If Sys_Need_Pass = False Then
            Return True
        End If

        If LaPass.Text = "1" Then
            Return True
        End If

        '提示验证权限。

        Login_event = 10    '事件1
        Login_Need_Level = User_Level_Enum.Oper
        Login_Mes = "请输入'操作员'的密码！！！"
        LoginForm1.Show(Me)
        Return False

    End Function



    ''' <summary>
    ''' 打开报警，记忆音 单曲播放
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button7_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        '加入密码权限验证。
        If PasswordCheck() = False Then
            Exit Sub
        End If

        PcSendBjqAddr = Val(Label10.Text)
        PcSendBjqindex = Val(LaBjqIndex.Text)

        CommAlarmOnMemOnce(PcSendBjqAddr, PcSendToBjq)
        PcSendAlarmOnMemOnce = CommProgressEnum.PcNeedSend

        LaFun.Text = "打开报警(记忆音乐单曲播放)-通讯发送中......"
        LaFun.ForeColor = Color.Black

    End Sub

    ''' <summary>
    ''' 打开报警-指定音调，音量 循环播放
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        '加入密码权限验证。
        If PasswordCheck() = False Then
            Exit Sub
        End If
        PcSendBjqAddr = Val(Label10.Text)
        PcSendBjqindex = Val(LaBjqIndex.Text)

        Dim volum As Integer
        Dim music As Integer
        volum = Val(ComVolum.Text)
        music = Val(ComMusic.Text)
        CommAlarmOnAssLoop(PcSendBjqAddr, music, volum, PcSendToBjq)
        PcSendAlarmOnAssLoop = CommProgressEnum.PcNeedSend
        LaFun.Text = "打开报警(指定音乐循环播放)-通讯发送中......"
        LaFun.ForeColor = Color.Black
    End Sub

    ''' <summary>
    ''' 打开报警-指定音调，音量 单曲播放
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        '加入密码权限验证。
        If PasswordCheck() = False Then
            Exit Sub
        End If
        'PcSendBjqAddr = Val(ComAddr.SelectedIndex + 1)
        'PcSendBjqindex = ComAddr.SelectedIndex
        PcSendBjqAddr = Val(Label10.Text)
        PcSendBjqindex = Val(LaBjqIndex.Text)

        Dim volum As Integer
        Dim music As Integer
        volum = Val(ComVolum.Text)
        music = Val(ComMusic.Text)
        CommAlarmOnAssOnce(PcSendBjqAddr, music, volum, PcSendToBjq)
        PcSendAlarmOnAssOnce = CommProgressEnum.PcNeedSend
        LaFun.Text = "打开报警(记忆音乐单曲播放)-通讯发送中......"
        LaFun.ForeColor = Color.Black
    End Sub


    ''' <summary>
    ''' 主机发送关闭报警
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        '加入密码权限验证。
        If PasswordCheck() = False Then
            Exit Sub
        End If
        PcSendBjqAddr = Val(Label10.Text)
        PcSendBjqindex = Val(LaBjqIndex.Text)
        CommAlarmOff(PcSendBjqAddr, PcSendToBjq)
        PcSendAlarmOff = CommProgressEnum.PcNeedSend
        LaFun.Text = "关闭报警-通讯发送中......"
        LaFun.ForeColor = Color.Black
    End Sub


    ''' <summary>
    ''' 手动查询报警灯状态-一次通讯
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button6_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click


        PcSendBjqAddr = Val(Label10.Text)
        PcSendBjqindex = Val(LaBjqIndex.Text)

        CommChaxun(PcSendBjqAddr, PcSendToBjq)
        PcSendChaxunOnce = CommProgressEnum.PcNeedSend
        LaFun.Text = "手动查询-通讯发送中......"
        LaFun.ForeColor = Color.Black
    End Sub

    ''' <summary>
    ''' 用户点击自动查询-按周期查询
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If Button5.Text = "自动查询" Then
            Button5.Text = "停止自动查询"
            BjqCommChaxunLoop = True

            '设置查询周期ms，   200,400,500,1000  ,最少为300ms
            '测试中发现，终端不能很快
            If ComboBox2.Text = "200" Then
                Timer1_Tcq_Per = 3
            ElseIf ComboBox2.Text = "400" Then
                Timer1_Tcq_Per = 4
            ElseIf ComboBox2.Text = "500" Then
                Timer1_Tcq_Per = 5
            Else         '默认，1秒钟查询一次
                Timer1_Tcq_Per = 10
            End If


        Else
            Button5.Text = "自动查询"
            BjqCommChaxunLoop = False
        End If
    End Sub

    ''' <summary>
    ''' 用户点击-音量加1
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button13_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        '加入密码权限验证。
        If PasswordCheck() = False Then
            Exit Sub
        End If
        PcSendBjqAddr = Val(Label10.Text)
        PcSendBjqindex = PcSendBjqindex = Val(LaBjqIndex.Text)
        CommVolumAddRec(PcSendBjqAddr, 1, PcSendToBjq)
        PcSendVolumAdd = CommProgressEnum.PcNeedSend
        LaFun.Text = "音量加1-通讯发送中......"
        LaFun.ForeColor = Color.Black
    End Sub

    ''' <summary>
    ''' 用户点击--音量减1
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button14_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        '加入密码权限验证。
        If PasswordCheck() = False Then
            Exit Sub
        End If
        PcSendBjqAddr = Val(Label10.Text)
        PcSendBjqindex = Val(LaBjqIndex.Text)
        CommVolumAddRec(PcSendBjqAddr, 2, PcSendToBjq)
        PcSendVolumRec = CommProgressEnum.PcNeedSend
        LaFun.Text = "音量减1-通讯发送中......"
        LaFun.ForeColor = Color.Black
    End Sub

    ''' <summary>
    ''' 用户点击--上一曲
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        '加入密码权限验证。
        If PasswordCheck() = False Then
            Exit Sub
        End If
        PcSendBjqAddr = Val(Label10.Text)
        PcSendBjqindex = PcSendBjqindex = Val(LaBjqIndex.Text)
        CommMusicPreNext(PcSendBjqAddr, 2, PcSendToBjq)
        PcSendMusicPre = CommProgressEnum.PcNeedSend
        LaFun.Text = "上一曲-通讯发送中......"
        LaFun.ForeColor = Color.Black
    End Sub

    ''' <summary>
    ''' 用户点击--下一曲
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        '加入密码权限验证。
        If PasswordCheck() = False Then
            Exit Sub
        End If
        PcSendBjqAddr = Val(Label10.Text)
        PcSendBjqindex = Val(LaBjqIndex.Text)
        CommMusicPreNext(PcSendBjqAddr, 1, PcSendToBjq)
        PcSendMusicNext = CommProgressEnum.PcNeedSend
        LaFun.Text = "下一曲-通讯发送中......"
        LaFun.ForeColor = Color.Black
    End Sub

    ''' <summary>
    ''' 打开报警器的单机调试面板，同时隐藏其他的控件
    ''' </summary>
    ''' <param name="bjqIndex"> 报警器的下标值 </param>
    ''' <param name="pos"> 鼠标点击的位置，用于定位panel的显示位置 </param>
    ''' <remarks></remarks>
    Public Sub BjqMoreFunction(ByVal bjqIndex As Integer, ByVal pos As System.Drawing.Point)
        GroupBox1.Visible = False
        GroupBox4.Visible = False

        GroupBox5.Top = 20
        GroupBox5.Width = 600
        GroupBox5.Height = 165

        GroupBox6.Width = 600
        GroupBox6.Height = 220

        GroupBox6.Top = GroupBox5.Top + GroupBox5.Height + 20


        PanDan.Width = 20 + GroupBox5.Width + 20
        PanDan.Height = 20 + GroupBox5.Height + 20 + GroupBox6.Height + 20
        GroupBox5.Left = 20
        GroupBox6.Left = 20

        GroupBox5.Enabled = True
        GroupBox6.Enabled = True
        GroupBox5.Show()
        GroupBox6.Show()

        If FindRightPosation(pos, PanDan.Width, PanDan.Height, Me.Width, Me.Height) Then
            PanDan.Location = pos
        End If

        PanDan.Visible = True
        LaPass.Text = 0
        PanDan.BringToFront()
        PanAutoChaxun.Visible = False
        '地址
        ' Label10.Text = (bjqIndex + 1).ToString

        Label10.Text = Fesn(bjqIndex).addr

        '利用标签，记录，报警器的下标值。
        LaBjqIndex.Text = bjqIndex.ToString

        If Fesn(bjqIndex).Comm_State = Tcq.Comm_State_Enum.Comm_OK Then
            Label28.Text = "正常"
            Label28.ForeColor = Color.Blue
            Label30.Text = Fesn(bjqIndex).Data1
            Label31.Text = Fesn(bjqIndex).Data2

            If Fesn(bjqIndex).alarm Then
                Label29.Text = "报警打开"
                Label29.ForeColor = Color.Red
            Else
                Label29.Text = "报警关闭"
                Label29.ForeColor = Color.Blue
            End If
        Else
            '通讯不正常
            Label28.Text = "故障"
            Label28.ForeColor = Color.Red
            Label28.ForeColor = Color.Red
            Label30.Text = "未知"
            Label31.Text = "未知"
            If Fesn(bjqIndex).alarm Then
                Label29.Text = "报警打开(历史)"
                Label29.ForeColor = Color.Red
            Else
                Label29.Text = "未知"
            End If


        End If

    End Sub


    Private Sub Button31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button31.Click
        CommDataWatch = True

        If ComWatchAddr.Text = "所有" Then
            CommDataWatchAddr = 0
        Else
            CommDataWatchAddr = Val(ComWatchAddr.Text)
        End If
        Button31.Enabled = False

    End Sub


    Private Sub Button30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button30.Click
        CommDataWatch = False
        Button30.Enabled = False
        Button31.Enabled = True
    End Sub

    Private Sub Button33_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button33.Click
        RichTextSend.Text = ""
    End Sub

    Private Sub Button32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button32.Click
        RichTextRec.Text = ""
    End Sub

    Private Sub ComCom_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComCom.SelectedIndexChanged
        Button1.Text = "打开串口"
        GroupBox4.Enabled = False
        GroupBox5.Enabled = False
        GroupBox6.Enabled = False

        If Button5.Text = "停止自动查询" Then
            BjqCommChaxunLoop = False
            Button5.Text = "自动查询"
        End If



    End Sub

    Private Sub ComBtl_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComBtl.SelectedIndexChanged
        Button1.Text = "打开串口"
        GroupBox4.Enabled = False
        GroupBox5.Enabled = False
        GroupBox6.Enabled = False
        If Button5.Text = "停止自动查询" Then
            BjqCommChaxunLoop = False
            Button5.Text = "自动查询"
        End If

    End Sub




    ''' <summary>
    ''' 报警全关按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        If Sys_Need_Pass Then
            Login_event = 8
            Login_Need_Level = User_Level_Enum.Oper
            Login_Mes = "请输入'操作员'的密码！！！"
            LoginForm1.Show(Me)
        Else
            Button20_Click1()
        End If
    End Sub


    ' 报警全关按钮
    Public Sub Button20_Click1()
        If MessageBox.Show("是否确定操作？关闭所有报警器报警？", "请确定", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        If TCQ_Usart_State = False Then
            MessageBox.Show("电脑串口打开失败，请检查串口，重启软件！！！")
            Exit Sub
        End If

        '广播协议不用接收回响，此处，直接停止主程序的通讯，
        '在此处使用串口，发送2次广播协议，每次间隔1000秒
        Timer1.Stop()
        Timer1.Enabled = False

        Try
            '1:生成广播通讯，数据帧
            CommAlarmAllOff(PcSendToBjq)
            Tcq_Port.DiscardOutBuffer()         '清空串口发送缓存区
            Tcq_Port.Write(PcSendToBjq, 0, 9)   '将数据发送出去。
            Tcq_Port.DiscardInBuffer()
            System.Threading.Thread.Sleep(500)

            ' CommAlarmAllOff(PcSendToBjq)
            Tcq_Port.DiscardOutBuffer()         '清空串口发送缓存区
            Tcq_Port.Write(PcSendToBjq, 0, 9)   '将数据发送出去。
            Tcq_Port.DiscardInBuffer()
        Catch ex As Exception
            MessageBox.Show("操作失败,请检查串口！！！" & vbCrLf & ex.ToString)
        End Try


        System.Threading.Thread.Sleep(500)
        Timer1.Enabled = True
    End Sub


    '报警全开按钮
    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        If Sys_Need_Pass Then
            Login_event = 7
            Login_Need_Level = User_Level_Enum.Oper
            Login_Mes = "请输入'操作员'的密码！！！"
            LoginForm1.Show(Me)
        Else
            Button21_Click1()
        End If
    End Sub

    '报警全开。
    Public Sub Button21_Click1()
        If MessageBox.Show("是否确定操作？打开所有报警器报警？", "请确定", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        If TCQ_Usart_State = False Then
            MessageBox.Show("电脑串口打开失败，请检查串口，重启软件！！！")
            Exit Sub
        End If
        '广播协议不用接收回响，此处，直接停止主程序的通讯，
        '在此处使用串口，发送2次广播协议，每次间隔1000秒
        Timer1.Stop()
        Timer1.Enabled = False
        Try
            '1:生成广播通讯，数据帧
            CommAlarmAllOn(PcSendToBjq)
            Tcq_Port.DiscardOutBuffer()         '清空串口发送缓存区
            Tcq_Port.Write(PcSendToBjq, 0, 9)   '将数据发送出去。
            Tcq_Port.DiscardInBuffer()
            System.Threading.Thread.Sleep(500)

            'CommAlarmAllOn(PcSendToBjq)
            Tcq_Port.DiscardOutBuffer()         '清空串口发送缓存区
            Tcq_Port.Write(PcSendToBjq, 0, 9)   '将数据发送出去。
            Tcq_Port.DiscardInBuffer()
        Catch ex As Exception
            MessageBox.Show("操作失败,请检查串口！！！" & vbCrLf & ex.ToString)
        End Try
        System.Threading.Thread.Sleep(500)
        Timer1.Enabled = True
    End Sub





End Class

'MALS 编写思路，还是要把单机通讯调试的代码放入到timer1,循环查询中去。
'设置一个新的变量，用于标识，主程序的通讯查询和 单机调试时的循环查询。
'动态设置用户选择的串口。


