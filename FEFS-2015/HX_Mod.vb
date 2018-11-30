Imports System.Data.Odbc
Imports MALS.SUSI
Imports System.IO
'系统核心模块
Module HX_Mod

    ''' <summary>
    ''' true=送检认证； false=普通生产
    ''' 在软件界面，不能说分认证和非认证软件，
    ''' 委婉的说为是否兼容旧版。如果选择不兼容，表示此版软件是认证的软。
    ''' 如果选择兼容表示此软件是普通生产的时的软。
    ''' </summary>
    ''' <remarks></remarks>
    Public RenZhen As Boolean

    ''' <summary>
    ''' true=系统电源为老的IGB，表示此软件适用于东阳的非标的四台监控主机。
    ''' false=系统电源为ATX-200
    ''' </summary>
    ''' <remarks></remarks>
    Public DongYang As Boolean

    ''' <summary>
    ''' 标志复位验证窗体已经显示
    ''' </summary>
    ''' <remarks></remarks>
    Public Reset_Form_Show As Boolean

    '-------------------------系统基础参数-存在sys_main_info 文件中--------------------
    ''' <summary>
    ''' 系统软件名称
    ''' </summary>
    ''' <remarks></remarks>
    Public Sys_name As String          '软件系统名称

    ''' <summary>
    ''' 系统中探测器的节点数量
    ''' </summary>
    ''' <remarks></remarks>
    Public Sys_node_count As Integer

    ''' <summary>
    ''' 主程序通讯串口:1=com1, 2=com2 ,3=com3 .....
    ''' </summary>
    ''' <remarks></remarks>
    Public Sys_tcq_com_id As Byte

    ''' <summary>
    ''' 主程序通讯串口波特率: 1=4800, 2=9600, 3=19200
    ''' </summary>
    ''' <remarks></remarks>
    Public Sys_tcq_com_btl As Byte

    ''' <summary>
    ''' 终端连续失联超限次数
    ''' </summary>
    ''' <remarks></remarks>
    Public Sys_Comm_Fail_Ct As Byte

    ''' <summary>
    ''' 系统窗体样式 0=全屏  1=窗体  
    ''' </summary>
    ''' <remarks></remarks>
    Public Sys_Form_Style As Byte

    ''' <summary>
    ''' 系统权限验证启用标识,启用>=1 ,不启用=0
    ''' </summary>
    ''' <remarks></remarks>
    Public Sys_Need_Pass As Byte
    'Public Sys_Retain_Data1 As Byte


    ''' <summary>
    ''' 公司id,用于加载主体背景  1=天冠，加载company1.jpg   2=亚松，加载commpany2.jpg  其它=commpany.jgp
    ''' </summary>
    ''' <remarks></remarks>
    Public Sys_Company_Id As Byte

    '管理员的超级密码-可提供给用户。
    Public Const SUPER_PASSWORD_GLY As String = "119120911"

    '系统集成调试人员的密码-不可提供给用户。
    Public Const SUPER_PASSWORD_ As String = "HZTG20182019"


    ' 系统参数预留2
    'Public Sys_Retain_Data2 As Byte

    '---------------------------系统基础参数-end-------------



    Public Sys_power_com_id As Byte    '工控电源通讯com 口编号
    Public Sys_comm_fail_ct1 As Byte
    Public Sys_dis_pages As Integer        '分页显示的页面数量
    Public Sys_page_dis_count As Byte  '每个界面显示多少个探测器数量。
    Public Sys_user_name As String
    Public Sys_Fanye As Byte           '系统翻页时间
    Public Sys_start_date As String    '系统开机的那天时间，用于做系统日志。。。
    Public Sys_Log_Path As String      '系统当天的日志路径
    Public net_map_array As ArrayList
    Public tcq_map_array As ArrayList

    ''软件自己重启，无须复位终端探测器。。。主要用在系统参数修改后的软件重启。。。。
    Public Sys_Restart As Boolean


    '用户登录输入密码时的 事件记录
    Public Login_event As Byte

    '用户登录输入密码时的 用户权限要求
    Public Login_Level As Byte

    Public Sys_Res_ct As Byte   '记录案件

    ''' <summary>
    ''' 标志自检模块暂时控制声光器件
    ''' </summary>
    ''' <remarks></remarks>
    Public SelfCheck_Use_LampAndSpeaker As Boolean


    ''' <summary>
    ''' 自检时和探测器实时通讯
    ''' </summary>
    ''' <remarks></remarks>
    Public SelfCheck_Use_Tcq_Comm As Boolean

    Public SelfCheck_Use_Tcq_Comm_OK As Boolean

    Public Rest_Use_Tcq_Comm As Boolean
    Public Rest_Use_Tcq_Comm_OK As Boolean

    Public Mute_Use_Tcq_Comm As Boolean
    Public Mute_Use_Tcq_Comm_OK As Boolean

    ''' <summary>
    ''' 用户权限-枚举
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum User_Level_Enum
        ''' <summary>
        '''  普通用户
        ''' </summary>
        ''' <remarks></remarks>
        Common = 0

        ''' <summary>
        ''' 操作员
        ''' </summary>
        ''' <remarks></remarks>
        Oper = 1

        ''' <summary>
        ''' 管理员
        ''' </summary>
        ''' <remarks></remarks>
        Maner = 2

        ''' <summary>
        '''  调试工程师
        ''' </summary>
        ''' <remarks></remarks>
        Enger = 4

    End Enum


    ''' <summary>
    ''' 0=普用户，
    ''' 1=操作员，
    ''' 2=管理员，
    ''' 3=超级用户
    ''' </summary>
    ''' <remarks></remarks>
    Public Sys_user_level As User_Level_Enum


    ''' <summary>
    ''' 系统锁定
    ''' </summary>
    ''' <remarks></remarks>
    Public Sys_Lock As Boolean

    ''' <summary>
    ''' 电池电压
    ''' </summary>
    ''' <remarks></remarks>
    Public ba_v As String



    Public Pra_Down_Tcq_Id As Integer     '主机下发参数的探测器id
    Public Pra_Down_Tcq_State As Boolean  '主机下发参数到探测器是否成功。。。


    Public Map_Names() As String          '地图信息。。。。
    Public Map_Nodes() As String          '每张地图包含的探测器编号


    ''' <summary>
    ''' 主机下发探测器参数。。。。。
    ''' </summary>
    ''' <remarks></remarks>
    Public Pra_Send_Array(13) As Byte

    ''' <summary>
    ''' 总的故障类型：无故障=0   监控报警=1  故障报警=2
    ''' </summary>
    ''' <remarks></remarks>
    Public Zll As Byte

    ''' <summary>
    ''' 系统消音标志
    ''' </summary>
    ''' <remarks></remarks>
    Public Sys_Mute As Boolean

    ''' <summary>
    ''' 系统电源  1=主电源  2=备电源
    ''' </summary>
    ''' <remarks></remarks>
    Public Sys_Who_Supply_Power As Byte

    ''' <summary>
    ''' 主电源状态：正常=0x31=1   不正常=0x30=0
    ''' </summary>
    ''' <remarks></remarks>
    Public Main_Power_State As Byte

    ''' <summary>
    ''' 备电源状态： 正常=0x30=0 电压低=0x31=1  短路=0x32=2  开路=0x33=3
    ''' </summary>
    ''' <remarks></remarks>
    Public Back_Power_State As Byte

    ''' <summary> 
    ''' 工控机电源通讯失败累计次数。
    ''' </summary>
    ''' <remarks></remarks>
    Public ATX_200_Comm_Fail_Count As Byte

    ''' <summary>
    '''  系统若上电后通讯连接过所有探测器，
    '''  则可以进行自检，否则就等。。。。。
    ''' </summary>
    ''' <remarks></remarks>
    Public Sys_Self_Check_Ready As Boolean

    ''' <summary>
    ''' 故障信息的集合素组
    ''' </summary>
    ''' <remarks></remarks>
    Public Guzhang_Array As New ArrayList

    ''' <summary>
    ''' 标志有故障报警来，提示程序声光报警
    ''' </summary>
    ''' <remarks></remarks>
    Public Guzhang_Alarm_Come As Boolean

    ''' <summary>
    ''' true=故障灯常亮 false=故障灯常灭。
    ''' </summary>
    ''' <remarks></remarks>
    Public Guzhang_Lamp_Sta As Boolean

    ''' <summary>
    ''' true=报警灯常亮 false=报警灯常灭。
    ''' </summary>
    ''' <remarks></remarks>
    Public Baojing_Lamp_Sta As Boolean


    ''' <summary>
    ''' true:自检占用此指示灯
    ''' </summary>
    ''' <remarks></remarks>
    Public Baojing_Lamp_Zj_Use As Boolean

    ''' <summary>
    ''' true:故障占用此指示灯
    ''' </summary>
    ''' <remarks></remarks>
    Public Guzhang_Lamp_Zj_Use As Boolean

    Public MainPower_Lamp_Zj_Use As Boolean

    Public BackPower_Lamp_Zj_Use As Boolean

    Public Speaker_Zj_Use As Boolean

    ''' <summary>
    ''' 自检使用报警
    ''' </summary>
    ''' <remarks></remarks>
    Public Speaker_Zj_Baojing As Boolean

    Public Speaker_Zj_Guzhang As Boolean

    ''' <summary>
    ''' 报警信息的集合素组
    ''' </summary>
    ''' <remarks></remarks>
    Public Baojing_Array As New ArrayList

    Public Tcq_Self_Check_Array As ArrayList

    ''' <summary>
    ''' 标志有监控报警来，提示程序声光报警
    ''' </summary>
    ''' <remarks></remarks>
    Public Baojing_Alarm_Come As Boolean

    ''' <summary>
    '''  系统电气火灾探测器对象数组
    ''' </summary>
    ''' <remarks></remarks>
    Public Fesn() As Tcq

    Public Fefs_Users() As FEFS_USER
    ''' <summary>
    ''' 用户登录时提醒信息，会现在在LoginForm窗体
    ''' </summary>
    ''' <remarks></remarks>
    Public Login_Mes As String

    ''' <summary>
    ''' 需要用户以何种身份登录
    ''' </summary>
    ''' <remarks></remarks>
    Public Login_Need_Level As String



    ''' <summary>
    ''' 系统定时巡查探测器的当前编号
    ''' </summary>
    ''' <remarks></remarks>
    Public Comm_Loop_Id As UShort      '定义探测器巡检id....

    Public Comm_Loop_First As Boolean  '定义

    ''' <summary>
    ''' 主机GPIO口状态 true=正常 false=异常
    ''' </summary>
    ''' <remarks></remarks>
    Public SUSI_State As Boolean       'SUSI口的状态=false  表示 SUSI口不可用。

    Public Mysql_State As Boolean      'mysql 数据库状态

    ''' <summary>
    ''' 主机连接探测器的RS232串口通讯状态 true=正常 false=异常
    ''' </summary>
    ''' <remarks></remarks>
    Public TCQ_Usart_State As Boolean

    ''' <summary>
    ''' 主机连接ATX200工控机电源的RS232串口通讯状态 true=正常 false=异常
    ''' </summary>
    ''' <remarks></remarks>s
    Public ATX200_Usart_State As Boolean




    ''' <summary>
    ''' 温度1报警开启标志   on=开启报警  false=关闭报警功能，add time  2017年12月25日11:15:51
    ''' </summary>
    ''' <remarks></remarks>
    Public T1_alarm_on As Boolean

    ''' <summary>
    ''' 温度2报警开启标志   on=开启报警  false=关闭报警功能，add time  2017年12月25日11:15:51
    ''' </summary>
    ''' <remarks></remarks>
    Public T2_alarm_on As Boolean

    ''' <summary>
    ''' 漏电流报警开启标志   on=开启报警  false=关闭报警功能，add time  2017年12月25日11:15:51
    ''' </summary>
    ''' <remarks></remarks>
    Public IL_alarm_on As Boolean

    ''' <summary>
    ''' 所有传感器开路,短路后故障报警开启标志   on=开启报警  false=关闭报警功能，add time  2017年12月25日11:15:51
    ''' </summary>
    ''' <remarks></remarks>
    Public ILT12_fault_on As Boolean

    ''' <summary>
    '''   故障和报警时 扬声器开启标志   on=开启  false=关闭，add time  2017年12月25日11:15:51
    ''' </summary>
    ''' <remarks></remarks>
    Public Speaker_work_on As Boolean

    '1:独立式:1路电流
    '2:独立式:1路电流+1路温度
    '3:独立式:1路电流+2路温度
    '4:非独立式:1路电流
    '5:非独立式:1路电流+1路温度
    '6:非独立式:1路电流+2路温度
    '终于有时间可以用上枚举类型了。。。2017年12月28日23:17:45
    '主机数据库内定义的tcq类型。
    Public Enum Tcq_type_pc_enum As UShort
        FEFM_IL = 1
        FEFM_IL_T1 = 2
        FEFM_Il_T2 = 3
        FEFI_IL = 4
        FEFI_IL_T1 = 5
        FEFI_IL_T2 = 6
    End Enum

    '电气火灾探测器表中，存储的型号
    Public Enum Tcq_type_enum As UShort
        FEFM_IL = &H10
        FEFM_IL_T1 = &H11
        FEFM_Il_T2 = &H12
        FEFI_IL = &H20
        FEFI_IL_T1 = &H21
        FEFI_IL_T2 = &H22
    End Enum

    ''' <summary>
    ''' 参数发送时的net
    ''' </summary>
    ''' <remarks></remarks>
    Public Send_Pra_Net As UShort


    ''' <summary>
    ''' 通讯调试状态。
    ''' </summary>
    ''' <remarks></remarks>
    Public Comm_Debug_Sta As Boolean
    Public Comm_Debug_Id As UShort

    Public Const db_con_str = "providerName=System.Data.Odbc; Dsn=ms;uid=root;pwd=885200"  '数据库连接字符串

    Private str_log As String

    ''' <summary>
    ''' 系统日志文件
    ''' </summary>
    ''' <remarks></remarks>
    Private file_log As StreamWriter

    ''' <summary>
    ''' 系统日志文件状态 true=文件正常，可以写入; false = 文件不可用
    ''' </summary>
    ''' <remarks></remarks>
    Private file_log_sta As Boolean



    ''' <summary>
    ''' 报警记录日志文件
    ''' </summary>
    ''' <remarks></remarks>
    Private alarm_log As StreamWriter

    ''' <summary>
    ''' 报警日志文件状态 true=文件正常可用; false= 文件不可用
    ''' </summary>
    ''' <remarks></remarks>
    Private alarm_log_sta As Boolean


    ''' <summary>
    ''' 故障记录日志文件
    ''' </summary>
    ''' <remarks></remarks>
    Private fault_log As StreamWriter

    ''' <summary>
    ''' 系统日志文件路径
    ''' </summary>
    ''' <remarks></remarks>
    Private file_path As String

    Private alarm_file_path As String

    Private odbc_con As Odbc.OdbcConnection

    ''' <summary>
    ''' 系统初始化(1:读取数据信息。 2:初始化系统核心变量)
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Sys_Init()

        '默认开启所有的报警功能。
        T1_alarm_on = True
        T2_alarm_on = True
        IL_alarm_on = True
        ILT12_fault_on = True
        Speaker_work_on = True
        Comm_Debug_Sta = False
        Comm_Debug_Id = 0


        '1-创建系统日志文件：以当前日期+时间做文件名。
        Dim hs As String
        Sys_start_date = Format(Now, "yyyy-MM-dd")       '获取当前日期
        hs = "-" & Now.Hour.ToString & "-" & Now.Minute.ToString    '获取当前的时间-1
        '系统日志文件存在安装目录下 log\时间.txt
        file_path = My.Application.Info.DirectoryPath & "\log\" & Sys_start_date & ".txt"

        '1.1 尝试打开或创建日志文件。
        Try
            If File.Exists(file_path) <> True Then
                file_log = File.CreateText(file_path)    '创建写入 文件对象 utf-8 编码
            Else
                file_log = File.AppendText(file_path)    '创建追加 文件对象 utf-8 编码
            End If
            file_log_sta = True
        Catch ex As Exception
            file_log_sta = False
            'MessageBox.Show("系统日志文件，创建失败！")
        End Try


        '2. 报警日志文件存在安装目录下 log\alarm-log\时间.txt
        '时间例子： 2018-10-31-10-01  标识 2018年10月31号，10点1分。
        '报警日志文件-按每次启动来保存信息。
        alarm_file_path = My.Application.Info.DirectoryPath & "\log\alarm-log\" & Sys_start_date & hs & ".txt"


        Try
            If File.Exists(file_path) <> True Then
                alarm_log = File.CreateText(alarm_file_path)    '创建写入 文件对象 utf-8 编码
            Else
                alarm_log = File.AppendText(alarm_file_path)    '创建追加 文件对象 utf-8 编码
            End If
            alarm_log_sta = True

        Catch ex As Exception
            'MessageBox.Show("系统日志文件，创建失败！")
            alarm_log_sta = False
        End Try

        SaveSysLog("############ " & Now.ToString & " 程序启动 ############")
        SaveAlarmLog("############ " & Now.ToString & " 系统启动 ############")


        'MALS 直接从 文件中加载信息
        If Sys_Main_Info_file_Init() Then
            SaveSysLog("#从数据文件sys_main_info加载-系统基础信息完成")
        Else

            SaveSysLog("#从数据文件sys_main_info加载-系统基础信息出错，将直接使用固化参数1个节点，认证模式")
            Sys_Base_Default_Info_Init()
        End If

        ' End If
        '

        file_log.Flush()


        '2:加载数据库“探测器信息表”
        'If Sys_Tcq_info_Init() Then
        '     file_log.WriteLine("#加载数据库中-探测器信息表-完成")
        'Else
        '  file_log.WriteLine("#加载数据库中探测器出错，将从备份文件加载探测器信息！")

        'MALS 直接从文件中加载信息
        If Sys_Tcq_Info_File_Init() Then
            SaveSysLog("#从备份文件加载探测器信息，完成！！")
        Else
            SaveSysLog("#从备份文件加载探测器信息出错，将直接使用软件固化参数，16个探测器，2个温度，500,100,100")
            Set_Tcq_Default_Info_Init()
        End If

        Sys_user_level = 0
        Sys_user_name = ""

        Sys_Restart = False

        SelfCheck_Use_LampAndSpeaker = False
        SelfCheck_Use_Tcq_Comm = False
        Rest_Use_Tcq_Comm = False
        Mute_Use_Tcq_Comm = False


        Guzhang_Array.Clear()
        Baojing_Array.Clear()
        Guzhang_Alarm_Come = False
        Baojing_Alarm_Come = False
        Guzhang_Lamp_Sta = False
        Baojing_Lamp_Sta = False
        MainPower_Lamp_Zj_Use = False
        BackPower_Lamp_Zj_Use = False
        Baojing_Lamp_Zj_Use = False
        Guzhang_Lamp_Zj_Use = False
        Speaker_Zj_Use = False


        Zll = 0   '无故障
        Sys_Mute = False
        Sys_Who_Supply_Power = 1
        Main_Power_State = 1
        Back_Power_State = 0
        Sys_Self_Check_Ready = False
        Sys_Lock = False


        '3:系统核心变量初始化
        Comm_Loop_Id = 0                  '通讯巡检id 从零开始。。。
        Pra_Down_Tcq_Id = -1              '下发参数标志
        Comm_Loop_First = True            '第一次标志


        '界面页面显示设置
        Sys_page_dis_count = 100
        If Sys_node_count > 100 Then
            Sys_dis_pages = Sys_node_count \ 100
            If (Sys_node_count Mod 100 <> 0) Then
                Sys_dis_pages = Sys_dis_pages + 1
            End If
        Else
            Sys_dis_pages = 1
        End If

    End Sub

    ''' <summary>
    '''  保存系统日志信息 
    ''' </summary>
    ''' <param name="mes"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveSysLog(ByVal mes As String) As Boolean
        '如果系统文件不可用，则直接返回。
        If file_log_sta = False Then
            Return False
        End If
        Try
            file_log.WriteLine(mes)
            file_log.Flush()
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' 保存报警信息到日志
    ''' </summary>
    ''' <param name="strlog">报警信息字符串</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAlarmLog(ByVal strlog As String) As Boolean
        If alarm_log_sta = False Then
            Return False
        End If
        Try
            alarm_log.WriteLine(strlog)
            alarm_log.Flush()
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function



    ''' <summary>
    '''  系统基础信息初始化:读取\backup_data\sys_main_info文件。
    ''' </summary> 
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Sys_Main_Info_file_Init() As Boolean

        Sys_Main_Info_file_Init = False

        Try
            Dim data_file_path As String

            '系统数据备份文件sys_main_info，存放位置在 安装路径+ \backup_data 文件夹内。
            data_file_path = My.Application.Info.DirectoryPath & "\backup_data\sys_main_info"

            If File.Exists(data_file_path) <> True Then
                Dim df_sw As StreamWriter = File.CreateText(data_file_path)
                '数据文件不存在，则创建。将关键的基础信息，写入到数据文件中。
                df_sw.WriteLine("-----------------系统名称#报警监控系统")
                df_sw.WriteLine("---------------报警灯数量#8")
                df_sw.WriteLine("-----------主程序通讯串口#1")
                df_sw.WriteLine("-------------------波特率#2") '波特率 1=4800  2=9600 3=19200
                df_sw.WriteLine("---报警器连续失联超限次数#3")
                df_sw.WriteLine("-------全屏显示or窗口显示#0") '0=全屏
                df_sw.WriteLine("--------------预留控制位1#0")
                df_sw.WriteLine("--------------预留控制位2#0")
                df_sw.Flush()
                df_sw.Close()
            End If

            Dim sr As StreamReader = File.OpenText(data_file_path)
            Dim data(20) As String

            Dim i As Integer
            i = 0

            Do While sr.Peek() >= 0

                Dim datas() = (Split(sr.ReadLine, "#"))
                data(i) = datas(1)
                i = i + 1
            Loop
            sr.Close()

            Sys_name = data(0)                  '系统标题名
            Sys_node_count = CInt(data(1))     '终端总数量  
            Sys_tcq_com_id = CByte(data(2))       '探测器通讯口
            Sys_tcq_com_btl = CByte(data(3))      '波特率 1=4800  2=9600 3=19200
            Sys_Comm_Fail_Ct = CByte(data(4))     '通讯失联累计次数
            Sys_Form_Style = CByte(data(5))
            Sys_Need_Pass = CByte(data(6))
            Sys_Company_Id = CByte(data(7))

            If Sys_node_count < 1 Then   '系统节点数量最少为1个
                Sys_node_count = 1
            End If

            Sys_Main_Info_file_Init = True
        Catch ex As Exception

        End Try


    End Function

    ''' <summary>  
    ''' 系统基础信息初始化:加载的默认信息。
    ''' Msyql 数据库和备份文本文件sys_main_info 均失效后，加载的默认信息。
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Sys_Base_Default_Info_Init()
        Sys_name = "报警器监控系统"
        Sys_node_count = 32
        Sys_tcq_com_id = 1      '默认com1 口
        Sys_tcq_com_btl = 2     '波特率 1=4800  2=9600 3=19200
        Sys_Comm_Fail_Ct = 3    '通讯失联累计次数
        Sys_Form_Style = 0      '全屏
        Sys_Need_Pass = 1       '需要密码
        Sys_Company_Id = 0
    End Sub

    ''' <summary>
    ''' 系统基础信息初始化，读取数据表sys_main_info 
    ''' 1:系统名称  
    ''' 2:探测器数量         
    ''' 3:探测器通讯口
    ''' 4:工控机电源通讯口     
    ''' 5:信息翻页时间  
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Sys_Base_Info_Init() As Boolean

        'Sys_Base_Info_Init = False

        'Try

        '    'Dim odbc_con As Odbc.OdbcConnection
        '    'odbc_con = New Odbc.OdbcConnection(db_con_str)

        '    Dim odbc_cmd As New Odbc.OdbcCommand("select * from sys_main_info")

        '    Dim odbc_ada As New Odbc.OdbcDataAdapter()

        '    odbc_ada.SelectCommand = odbc_cmd

        '    odbc_ada.SelectCommand.Connection = odbc_con

        '    Dim dataset As New DataSet()
        '    odbc_ada.Fill(dataset, "sys_main_info")

        '    If dataset.Tables("sys_main_info").Rows.Count <= 0 Then
        '        Return False
        '    End If

        '    With dataset.Tables("sys_main_info").Rows(0)

        '        'Sys_name = .Item("sys_name")                 '系统标题名
        '        'Sys_node_count = .Item("sys_node_count")     '探测器总数量 默认16
        '        'Sys_tcq_com_id = .Item("sys_tcq_com")        '探测器通讯口 默认1
        '        'Sys_power_com_id = .Item("sys_power_com")    '工控机电源通讯口 默认2
        '        'Sys_Fanye = .Item("sys_fanye")               '信息翻页时间默认5秒
        '        'Sys_comm_fail_ct = .Item("sys_comm_fail_ct") '通讯失败，连续累计次数







        '        '先判断表中是否有t1_alarm_on 字段。 温度1报警启用标志。
        '        If dataset.Tables("sys_main_info").Columns.Contains("t1_alarm_on") Then
        '            If .Item("t1_alarm_on") > 0 Then
        '                T1_alarm_on = True
        '            Else
        '                T1_alarm_on = False
        '            End If
        '        Else
        '            T1_alarm_on = True
        '        End If


        '        '先判断表中是否有t2_alarm_on 字段。 温度1报警启用标志。
        '        If dataset.Tables("sys_main_info").Columns.Contains("t2_alarm_on") Then
        '            If .Item("t2_alarm_on") > 0 Then
        '                T2_alarm_on = True
        '            Else
        '                T2_alarm_on = False
        '            End If
        '        Else
        '            T2_alarm_on = True
        '        End If

        '        '先判断表中是否有IL_alarm_on 字段。 IL报警启用标志。
        '        If dataset.Tables("sys_main_info").Columns.Contains("il_alarm_on") Then
        '            If .Item("il_alarm_on") > 0 Then
        '                IL_alarm_on = True
        '            Else
        '                IL_alarm_on = False
        '            End If
        '        Else
        '            IL_alarm_on = True
        '        End If


        '        '全局的 传感器故障报警启用标志。
        '        If dataset.Tables("sys_main_info").Columns.Contains("ilt_falut_on") Then
        '            If .Item("ilt_falut_on") > 0 Then
        '                ILT12_fault_on = True
        '            Else
        '                ILT12_fault_on = False
        '            End If
        '        Else
        '            ILT12_fault_on = True
        '        End If




        '        If dataset.Tables("sys_main_info").Columns.Contains("speaker_work_on ") Then
        '            If .Item("speaker_work_on") > 0 Then
        '                Speaker_work_on = True
        '            Else
        '                Speaker_work_on = False
        '            End If
        '        Else
        '            Speaker_work_on = True
        '        End If


        '        If Sys_Fanye < 5 Then
        '            Sys_Fanye = 5
        '        End If

        '        ' odbc_con.Close()             '关闭到数据库的连接

        '        If Sys_node_count < 1 Then   '系统节点数量最少为1个
        '            Sys_node_count = 1
        '        End If

        '        If Sys_name = "" Or Sys_name.Length < 7 Then
        '            Sys_name = "FEFS电气火灾监控系统"
        '        End If
        '    End With
        '    Sys_Base_Info_Init = True

        'Catch ex As Exception

        'End Try

    End Function

    ''' <summary>
    '''  系统用户初始化，读取数据库中user_info表，初始化用户对象数组 Fefs_Users
    '''  如果数据中表信息不正确，则返回false
    '''  
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Sys_User_Init() As Boolean
        Try
            ' Dim odbc_con As Odbc.OdbcConnection
            Dim odbc_cmd As New Odbc.OdbcCommand("select * from user_info")
            Dim odbc_ada As New Odbc.OdbcDataAdapter()
            Dim user_count As UShort
            ' odbc_con = New Odbc.OdbcConnection(db_con_str)
            odbc_ada.SelectCommand = odbc_cmd
            odbc_ada.SelectCommand.Connection = odbc_con
            'odbc_con.Open()

            Dim dataset As New DataSet()
            odbc_ada.Fill(dataset, "user_info")

            user_count = dataset.Tables("user_info").Rows.Count

            '如果信息小于2个，则系统报错数据库错误。

            '这里要求 这个对象数组必须有且仅有两个，一个是管理员 一个操作员。不符合，就错误

            If user_count <> 2 Then
                'str_log = Now.ToString & vbCrLf & "数据库中user_info表记录少于2,将填充默认信息到数据库中！" & vbCrLf & vbCrLf
                'My.Computer.FileSystem.WriteAllText(Sys_Log_Path, str_log, True)
                Return False
            End If

            ReDim Fefs_Users(user_count - 1)

            Dim temp_i As Integer

            For temp_i = 0 To user_count - 1

                Fefs_Users(temp_i) = New FEFS_USER       '实例化探测器对象数组。。。。

                With dataset.Tables("user_info").Rows(temp_i)
                    Fefs_Users(temp_i).user_name = .Item("user_name").ToString
                    Fefs_Users(temp_i).user_password = .Item("user_password").ToString
                    Fefs_Users(temp_i).user_level = .Item("user_level").ToString
                End With
            Next

            '  odbc_con.Close()

            If (Fefs_Users(0).user_name = "操作员") Then
                If (Fefs_Users(1).user_name = "管理员") Then
                    Return True
                End If
            End If

            If (Fefs_Users(1).user_name = "操作员") Then
                If (Fefs_Users(0).user_name = "管理员") Then
                    Return True
                End If
            End If

            Return False

        Catch ex As Exception
            'str_log = Now.ToString & vbCrLf & "读取系统数据库[user_info]表错误，将要填充默认信息。" & vbCrLf & vbCrLf
            'My.Computer.FileSystem.WriteAllText(Sys_Log_Path, str_log, True)
            Return False
        End Try

        Sys_User_Init = True

    End Function

    Public Function Sys_User_Init_File() As Boolean


        Try
            Dim data_file_path As String
            ReDim Fefs_Users(1)

            '系统数据备份文件sys_main_info，存放位置在 安装路径+ \backup_data 文件夹内。
            data_file_path = My.Application.Info.DirectoryPath & "\backup_data\GLY_PASS"

            If File.Exists(data_file_path) <> True Then
                Dim df_sw As StreamWriter = File.CreateText(data_file_path)
                df_sw.WriteLine("123456")
                df_sw.Flush()
                df_sw.Close()
            End If

            Dim sr As StreamReader = File.OpenText(data_file_path)
            Fefs_Users(0) = New FEFS_USER
            Fefs_Users(0).user_name = "管理员"
            Fefs_Users(0).user_password = sr.ReadLine
            Fefs_Users(0).user_level = "管理员"
            sr.Close()

            data_file_path = My.Application.Info.DirectoryPath & "\backup_data\CZY_PASS"
            If File.Exists(data_file_path) <> True Then
                Dim df_sw As StreamWriter = File.CreateText(data_file_path)
                df_sw.WriteLine("123456")
                df_sw.Flush()
                df_sw.Close()
            End If

            Dim sr1 As StreamReader = File.OpenText(data_file_path)

            Fefs_Users(1) = New FEFS_USER
            Fefs_Users(1).user_name = "操作员"
            Fefs_Users(1).user_password = sr1.ReadLine
            Fefs_Users(1).user_level = "操作员"
            sr1.Close()

            Return True

        Catch ex As Exception
            Return False
        End Try

        Return False
    End Function


    ''' <summary>
    ''' 系统初始化时从数据库加载探测器地图信息
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function tcq_map_Init() As Boolean

        Try
            ' Dim odbc_con As Odbc.OdbcConnection
            Dim odbc_cmd As New Odbc.OdbcCommand("select * from tcq_map")
            Dim odbc_ada As New Odbc.OdbcDataAdapter()
            '  odbc_con = New Odbc.OdbcConnection(db_con_str)
            odbc_ada.SelectCommand = odbc_cmd
            odbc_ada.SelectCommand.Connection = odbc_con
            odbc_con.Open()

            Dim dataset As New DataSet()
            odbc_ada.Fill(dataset, "tcq_map")

            If dataset.Tables("tcq_map").Rows.Count < 1 Then
                'str_log = Now.ToString & vbCrLf & "数据库中tcq_map表记录为空！" & vbCrLf & vbCrLf
                'My.Computer.FileSystem.WriteAllText(Sys_Log_Path, str_log, True)
                odbc_con.Close()
                Return False
            End If

            For temp_i = 1 To dataset.Tables("tcq_map").Rows.Count
                With dataset.Tables("tcq_map").Rows(temp_i - 1)
                    Dim tcq_map1 As tcq_map_cl
                    tcq_map1 = New tcq_map_cl
                    tcq_map1.id = .Item("id")
                    tcq_map1.name = .Item("name")
                    Dim s1 As String
                    s1 = .Item("array")
                    If s1 <> "" Then
                        Dim s2() As String
                        s2 = Split(s1, ",")
                        Dim ii As Integer
                        If s2.Length > 0 Then
                            ReDim tcq_map1.tcqs(s2.Length - 1)
                        End If
                        For ii = LBound(s2) To UBound(s2)
                            tcq_map1.tcqs(ii) = Val(s2(ii))
                        Next
                        tcq_map_array.Add(tcq_map1)
                    End If
                End With
            Next
            odbc_con.Close()

        Catch ex As Exception
            'str_log = Now.ToString & vbCrLf & "读取系统数据库[tcq_map]表错误!!" & vbCrLf & vbCrLf
            'My.Computer.FileSystem.WriteAllText(Sys_Log_Path, str_log, True)
            Return False
        End Try

        Return True

    End Function

    ''' <summary>
    ''' 从Mysql数据库加载探测器表内的信息，同时初始化探测器对象数组
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Sys_Tcq_info_Init() As Boolean

        Sys_Tcq_info_Init = False
        Try
            ' Dim odbc_con As Odbc.OdbcConnection
            Dim odbc_cmd As New Odbc.OdbcCommand("select * from tcq_info")
            Dim odbc_ada As New Odbc.OdbcDataAdapter()
            '  odbc_con = New Odbc.OdbcConnection(db_con_str)
            odbc_ada.SelectCommand = odbc_cmd
            odbc_ada.SelectCommand.Connection = odbc_con
            odbc_con.Open()

            Dim dataset As New DataSet()
            odbc_ada.Fill(dataset, "tcq_info")

            '如果tcq_info 的探测器信息小于10个，则系统报错数据库错误。
            If dataset.Tables("tcq_info").Rows.Count < 1 Then
                'str_log = Now.ToString & vbCrLf & "数据库中tcq_info表记录少于1,将填充默认信息到数据库中！" & vbCrLf & vbCrLf
                'My.Computer.FileSystem.WriteAllText(Sys_Log_Path, str_log, True)

                file_log.WriteLine("############ 数据库中tcq_info表记录少于1,出错！！！############")
                file_log.Flush()
                Return False
            End If

            '如果记录表中的实际数量小于sys_node_count
            If dataset.Tables("tcq_info").Rows.Count < Sys_node_count Then

                'str_log = Now.ToString & vbCrLf & "数据库中的tcq_info表的总数量小于设定的节点数量,以tcq_info数量为准" & vbCrLf & vbCrLf
                'My.Computer.FileSystem.WriteAllText(Sys_Log_Path, str_log, True)

                file_log.WriteLine("############ 数据库中的tcq_info表的总数量小于系统节点数量,以tcq_info数量为准- ############")
                file_log.Flush()
                Sys_node_count = dataset.Tables("tcq_info").Rows.Count  '重新给Sys_node_count 赋值
            End If

            ReDim Fesn(Sys_node_count - 1)   '重新定义探测器对象数组，因为数组下标从0开始，所以剪1.

            Dim temp_i As Integer

            For temp_i = 0 To Sys_node_count - 1

                Fesn(temp_i) = New Tcq()      '实例化探测器对象数组。。。。

                With dataset.Tables("tcq_info").Rows(temp_i)

                    Fesn(temp_i).id = .Item("id")

                    '探测器编号(即物理通讯地址)
                    If Fesn(temp_i).id < 10 Then
                        Fesn(temp_i).id_str = "000" & Fesn(temp_i).id.ToString
                    ElseIf Fesn(temp_i).id < 100 Then
                        Fesn(temp_i).id_str = "00" & Fesn(temp_i).id.ToString
                    ElseIf Fesn(temp_i).id < 1000 Then
                        Fesn(temp_i).id_str = "0" & Fesn(temp_i).id.ToString
                    Else
                        Fesn(temp_i).id_str = temp_i.ToString
                    End If

                    '探测器名称
                    If IsDBNull(.Item("box_name")) Then
                        Fesn(temp_i).name = "----"
                    Else
                        Fesn(temp_i).name = .Item("box_name")
                    End If

                    '剩余电流报警值
                    Fesn(temp_i).IL_Baojin = .Item("il_bj")

                    '探测器归属的通讯支路
                    If IsDBNull(.Item("net")) Then
                        Fesn(temp_i).net = 1
                    Else
                        Fesn(temp_i).net = .Item("net")
                    End If

                    '探测器所在通讯支路中的位置
                    If IsDBNull(.Item("net_id")) Then
                        Fesn(temp_i).net_id = 1
                    Else
                        Fesn(temp_i).net_id = .Item("net_id")
                    End If

                    '温度1和温度2报警(不管表是否配有温度)
                    Fesn(temp_i).T1_baojin = .Item("t1_bj")
                    Fesn(temp_i).T2_baojin = .Item("t2_bj")

                    '探测器的类型   
                    Fesn(temp_i).type = .Item("type")
                    ' Fesn(temp_i).T1_Enable = True
                    ' Fesn(temp_i).T2_Enable = True

                    If Fesn(temp_i).type = Tcq_type_pc_enum.FEFI_IL_T2 Or Fesn(temp_i).type = Tcq_type_pc_enum.FEFM_Il_T2 Then
                        Fesn(temp_i).T1_Enable = True
                        Fesn(temp_i).T2_Enable = True

                    ElseIf Fesn(temp_i).type = Tcq_type_pc_enum.FEFI_IL_T1 Or Fesn(temp_i).type = Tcq_type_pc_enum.FEFM_IL_T1 Then
                        Fesn(temp_i).T1_Enable = True
                        Fesn(temp_i).T2_Enable = False
                    Else
                        Fesn(temp_i).T1_Enable = False
                        Fesn(temp_i).T2_Enable = False
                    End If

                    '探测器使能>0, 屏蔽=0
                    If .Item("enable") > 0 Then
                        Fesn(temp_i).enable = True
                    Else
                        Fesn(temp_i).enable = False
                    End If

                    '探测器通讯失败延时次数
                    Fesn(temp_i).Comm_Fail_Count = 0

                    '探测器通讯状态(开始为等待状态)
                    Fesn(temp_i).Comm_State = Tcq.Comm_State_Enum.Comm_Wait

                    '对象其它值初始化
                    Fesn(temp_i).il_alarm_pop = False
                    Fesn(temp_i).t1_alarm_pop = False
                    Fesn(temp_i).t2_alarm_pop = False
                    Fesn(temp_i).il_error_pop = False
                    Fesn(temp_i).t1_error_pop = False
                    Fesn(temp_i).t2_error_pop = False
                    Fesn(temp_i).IL_Baojin_Max = 0
                End With
            Next

            ' odbc_con.Close()

        Catch ex As Exception
            file_log.WriteLine("############ 读取数据库中的tcq_info表时，出错！！！- ############")
            file_log.Flush()
            Return False
        End Try
        Sys_Tcq_info_Init = True
    End Function


    ''' <summary>
    ''' 从文本文件加载探测器表内的信息，同时初始化探测器对象数组
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Sys_Tcq_Info_File_Init() As Boolean

        Sys_Tcq_Info_File_Init = False

        Try
            Dim data_file_path As String

            '系统数据备份文件sys_main_info，存放位置在 安装路径+ \backup_data 文件夹内。
            data_file_path = My.Application.Info.DirectoryPath & "\backup_data\tcq_info"

            If File.Exists(data_file_path) <> True Then
                '如果报警器的数据文件不存在，就创建一个 
                CreateBJQData(data_file_path)
            End If

            Dim sr As StreamReader = File.OpenText(data_file_path)
            Dim i As Integer
            Dim tcq_str_array As New ArrayList

            Do While sr.Peek() >= 0
                Dim tcq_str As String
                tcq_str = sr.ReadLine
                tcq_str_array.Add(tcq_str)
            Loop
            sr.Close()

            '探测器对象这里为64个。跟系统sys_node_ct 无关。
            ReDim Fesn(tcq_str_array.Count - 1)


            For i = 0 To tcq_str_array.Count - 1
                Dim tcq_str As String
                tcq_str = tcq_str_array.Item(i).ToString
                Dim datas() = (Split(tcq_str, "@%"))

                Fesn(i) = New Tcq

                '                 编号@%  地址 @%     位置   @%   预留1 @%  预留2
                '插入字符串效果如：1  @%    1  @%   1#区域   @%     0   @%    0


                Fesn(i).id = Val(datas(0))  '编号。。。唯一的，不允许重复。

                Fesn(i).id_str = Fesn(i).id.ToString
                Fesn(i).addr = Val(datas(1)) '地址
                Fesn(i).name = datas(2)   '这里为位置，箱号的意思。

                Fesn(i).enable = Val(datas(3))    '-屏蔽功能。

                Fesn(i).net = Val(datas(4))     '预留2

                '探测器通讯失败延时次数
                Fesn(i).Comm_Fail_Count = 0

                '探测器通讯状态(开始为等待状态)
                Fesn(i).Comm_State = Tcq.Comm_State_Enum.Comm_Wait

                '对象其它值初始化
                Fesn(i).Comm_Fail_Count = 0
                Fesn(i).Comm_State = Tcq.Comm_State_Enum.Comm_Wait
                Fesn(i).il_alarm_pop = False
                Fesn(i).t1_alarm_pop = False
                Fesn(i).t2_alarm_pop = False
                Fesn(i).il_error_pop = False
                Fesn(i).t1_error_pop = False
                Fesn(i).t2_error_pop = False
                Fesn(i).IL_Baojin_Max = 0
                Fesn(i).T1_Baojin_Max = 0
                Fesn(i).T2_Baojin_Max = 0
                Fesn(i).comm_Fail_Sta = False
                Fesn(i).Pre_comm = 0
            Next

            '如果系统节点数量，大于文件中探测器节点数，则以探测器节点数量为准。
            If Sys_node_count > tcq_str_array.Count Then
                Sys_node_count = tcq_str_array.Count
            End If

            Sys_Tcq_Info_File_Init = True

        Catch ex As Exception

        End Try

    End Function


    ''' <summary>
    ''' 没有报警灯的数据文件，直接初始化一些点数，放入到文件中。
    ''' 创建报警器数量： BJQMAXADDR
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateBJQData(ByVal data_file_path As String) As Boolean

        Dim df_sw As StreamWriter = File.CreateText(data_file_path)

        Dim tcq_ii As UShort
        Dim tcq_info_str As String

        For tcq_ii = 1 To BJQMAXADDR
            '                 编号@%  地址 @%     位置   @%   预留1 @%  预留2
            '插入字符串效果如：1  @%    1  @%   1#区域   @%     0   @%    0

            tcq_info_str = tcq_ii.ToString & "@%" & tcq_ii.ToString & "@%" & tcq_ii.ToString & "#区域@%0@%0"
            df_sw.WriteLine(tcq_info_str)
        Next
        df_sw.Flush()
        df_sw.Close()
    End Function


    ''' <summary>
    ''' Msyql 数据库与文本文件均失效后，直接固化探测器参数
    ''' 固化参数：16个探测器，2个温度，电流报警500，温度1-2 报警100
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Set_Tcq_Default_Info_Init()
        Sys_node_count = 16
        ReDim Fesn(Sys_node_count - 1)
        Dim temp_i As Byte
        For temp_i = 0 To 15
            Fesn(temp_i) = New Tcq()
            Fesn(temp_i).id = temp_i + 1
            Fesn(temp_i).id_str = "000" & Fesn(temp_i).id.ToString
            Fesn(temp_i).name = Fesn(temp_i).id.ToString & "ALE"
            Fesn(temp_i).IL_Yujin = 500
            Fesn(temp_i).IL_Baojin = 500
            Fesn(temp_i).net = 1       '默认归属的通讯支路为1
            Fesn(temp_i).net_id = temp_i + 1
            Fesn(temp_i).T1_baojin = 100
            Fesn(temp_i).T1_Yujin = 100
            Fesn(temp_i).T2_baojin = 100
            Fesn(temp_i).T2_Yujin = 100

            If temp_i > 4 Then
                Fesn(temp_i).type = 3
            Else
                Fesn(temp_i).type = 0
            End If


            If Fesn(temp_i).type = 0 Then
                Fesn(temp_i).T1_Enable = True
                Fesn(temp_i).T2_Enable = True
            ElseIf Fesn(temp_i).type = 1 Or Fesn(temp_i).type = 3 Then
                Fesn(temp_i).T1_Enable = True
                Fesn(temp_i).T2_Enable = False
            Else
                Fesn(temp_i).T1_Enable = False
                Fesn(temp_i).T2_Enable = False
            End If

            Fesn(temp_i).Comm_Fail_Count = 0
            Fesn(temp_i).Comm_State = Tcq.Comm_State_Enum.Comm_Wait
            Fesn(temp_i).il_alarm_pop = False
            Fesn(temp_i).t1_alarm_pop = False
            Fesn(temp_i).t2_alarm_pop = False
            Fesn(temp_i).il_error_pop = False
            Fesn(temp_i).t1_error_pop = False
            Fesn(temp_i).t2_error_pop = False
            Fesn(temp_i).IL_Baojin_Max = 0
            Fesn(temp_i).T1_Baojin_Max = 0
            Fesn(temp_i).T2_Baojin_Max = 0
            Fesn(temp_i).comm_Fail_Sta = False
            Fesn(temp_i).Pre_comm = 0
        Next
    End Sub



    ''' <summary>
    ''' 向数据库中插入1023条探测器数据。。。。。。
    ''' 101-1023
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Add_tcq_into_table() As Boolean

        If MsgBox("即将清空Tcq_Inof表的所有数据，然后插入默认的500记录，请问是否继续？", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Exit Function
        End If

        MsgBox("预计耗时10秒，期间请勿进行其他操作！")

        Try
            Dim odbc_con1 As Odbc.OdbcConnection
            odbc_con1 = New Odbc.OdbcConnection(db_con_str)
            Dim ocm As New OdbcCommand

            Dim temp_i As UShort
            odbc_con1.Open()
            ocm.Connection = odbc_con1
            '循环向 tcq_info 中插入500条数据。。。

            ''''下面几行代码演示如何取到返回的数字
            'Dim odbc_reader As OdbcDataReader
            'ocm.CommandText = "select count(*)  from tcq_info"
            'odbc_reader = ocm.ExecuteReader
            'If odbc_reader.Read Then
            '    MsgBox(odbc_reader.GetValue(0))
            'End If
            'odbc_reader.Close()
            '首先清空所有的数据
            ocm.CommandText = "truncate table tcq_info"
            ocm.ExecuteNonQuery()
            For temp_i = 1 To 500
                ocm.CommandText = "insert into tcq_info values(" & temp_i & ", '" & temp_i & "配电箱',1,1,1,500.0,100.0,100.0,1)"
                ocm.ExecuteNonQuery()
            Next
            odbc_con1.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return True


    End Function

    ''' <summary>
    '''  执行一条SQL语句，并返还是否成功
    ''' </summary>
    ''' <param name="sql_str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Sql_Exe(ByVal sql_str As String) As Boolean
        Dim i As Integer
        Try
            Dim odbc_con1 As Odbc.OdbcConnection
            odbc_con1 = New Odbc.OdbcConnection(db_con_str)
            Dim ocm As New OdbcCommand

            odbc_con1.Open()
            ocm.Connection = odbc_con1

            ocm.CommandText = sql_str


            i = ocm.ExecuteNonQuery()

            odbc_con1.Close()
        Catch ex As Exception

            Return False
        End Try
        If i = 0 Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    '''  执行一条SQL语句，并返还是否成功
    ''' </summary>
    ''' <param name="sql_str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Sql_Exe1(ByVal sql_str As String, ByRef mes As String) As Boolean
        Dim i As Integer
        Try
            Dim odbc_con1 As Odbc.OdbcConnection
            odbc_con1 = New Odbc.OdbcConnection(db_con_str)
            Dim ocm As New OdbcCommand

            odbc_con1.Open()
            ocm.Connection = odbc_con1

            ocm.CommandText = sql_str


            i = ocm.ExecuteNonQuery()

            odbc_con1.Close()
        Catch ex As Exception
            mes = ex.ToString
            Return False
        End Try
        If i = 0 Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    '''  向数据库His_Alarm表中添加一条报警记录。
    ''' </summary>
    ''' <param name="tcq_id">发生报警的探测器的id属性</param>
    ''' <param name="alarm_kind">报警类型:0=IL报警,1=T1报警,2=T2报警</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Add_Record_To_His_Alarm(ByVal tcq_id As Integer, ByVal alarm_kind As Byte) As Boolean
        Dim sql_str As String
        Dim date_time(2) As String

        Dim t1_value As String
        Dim t2_value As String


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




        If Sql_Exe(sql_str) Then
            Return True
        End If

        Return False

    End Function


    ''' <summary>
    '''  查询sql语句()，返回数据集的行数
    ''' </summary>
    ''' <param name="sql_str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Find_Sql_Exe(ByVal sql_str As String) As Integer
        Find_Sql_Exe = 0
        Try
            Dim odbc_con1 As Odbc.OdbcConnection
            odbc_con1 = New Odbc.OdbcConnection(db_con_str)
            Dim ocm As New OdbcCommand
            odbc_con1.Open()
            ocm.Connection = odbc_con1
            ocm.CommandText = sql_str
            Dim i As Integer
            ' i = ocm.ExecuteNonQuery()
            i = ocm.ExecuteScalar()
            odbc_con1.Close()
            Return i
        Catch ex As Exception
            Return 0
        End Try
        Return True
    End Function


    ''' <summary>
    ''' 用户登录检查用户名和密码是否正确。
    ''' </summary>
    ''' <param name="name">用户名</param>
    ''' <param name="pass">密码</param>
    ''' <returns>用户权限（1,2,3）,返回0表示用户名或密码错误</returns>
    ''' <remarks></remarks>
    Public Function User_Login(ByVal name As String, ByVal pass As String) As Byte
        Try
            Dim sql_str As String
            sql_str = " select user_level from user_info where user_name='" & name & "'" & " and user_password='" & pass & "'"

            Dim odbc_con1 As Odbc.OdbcConnection
            Dim odbc_cmd As New Odbc.OdbcCommand(sql_str)
            Dim odbc_ada As New Odbc.OdbcDataAdapter()

            odbc_con1 = New Odbc.OdbcConnection(db_con_str)
            odbc_ada.SelectCommand = odbc_cmd
            odbc_ada.SelectCommand.Connection = odbc_con1
            Dim dataset As New DataSet()
            odbc_ada.Fill(dataset, "user_info")

            If dataset.Tables("user_info").Rows.Count <> 0 Then
                User_Login = dataset.Tables("user_info").Rows(0).Item(0)
            Else
                User_Login = 0
            End If

            odbc_con1.Close()

        Catch ex As Exception
            User_Login = 0
        End Try

    End Function

    ''' <summary>
    ''' 探测器复位后，清楚其报警和故障状态。
    ''' 复位集合：Tcq_Self_Check_Array
    ''' 报警集合：  Guzhang_Array
    ''' 故障集合： Guzhang_Array
    ''' 其它Fesn() 对象属性。。
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Tcq_Reset_BJ_GZ() As Boolean

        '1:清除 报警 集合

        Dim tsc As Tcq_Self_Check
        Dim guzhang_tcq As New Guzhang_info
        Dim i_csw As Integer

        Dim alarm_tcq As New Alarm_info

        '循环遍历 复位探测器集合
        For Each tsc In Tcq_Self_Check_Array

            'For Each guzhang_tcq In Guzhang_Array
            '    If tsc.Tcq_id = guzhang_tcq.tcq_id Then
            '        Guzhang_Array.Remove(guzhang_tcq.tcq_id)
            '    End If
            'Next

            If tsc.Self_Check_Result = 1 Then  '如果这个点复位完成，则清除 。

                '故障有 三种，1=电流， 2=温度1，  3=温度2 ，不考虑

                If Fesn(tsc.Tcq_id - 1).il_error_pop Then
                    If Guzhang_Array.Count >= 1 Then
                        For i_csw = 0 To Guzhang_Array.Count - 1
                            guzhang_tcq = Guzhang_Array(i_csw)
                            If (guzhang_tcq.tcq_id = tsc.Tcq_id) Then
                                Guzhang_Array.RemoveAt(i_csw)
                                Exit For
                            End If
                        Next
                    End If
                    Fesn(tsc.Tcq_id - 1).il_error_pop = False
                End If

                '第2次清除 
                If Fesn(tsc.Tcq_id - 1).t1_error_pop Then
                    If Guzhang_Array.Count >= 1 Then
                        For i_csw = 0 To Guzhang_Array.Count - 1
                            guzhang_tcq = Guzhang_Array(i_csw)
                            If (guzhang_tcq.tcq_id = tsc.Tcq_id) Then
                                Guzhang_Array.RemoveAt(i_csw)
                                Exit For
                            End If
                        Next
                    End If
                    Fesn(tsc.Tcq_id - 1).t1_error_pop = False
                End If


                '第3次
                If Fesn(tsc.Tcq_id - 1).t1_error_pop Then
                    If Guzhang_Array.Count >= 1 Then
                        For i_csw = 0 To Guzhang_Array.Count - 1
                            guzhang_tcq = Guzhang_Array(i_csw)
                            If (guzhang_tcq.tcq_id = tsc.Tcq_id) Then
                                Guzhang_Array.RemoveAt(i_csw)
                                Exit For
                            End If
                        Next
                    End If
                    Fesn(tsc.Tcq_id - 1).t2_error_pop = False
                End If


                ' 清除电流报警信息
                If Fesn(tsc.Tcq_id - 1).il_alarm_pop Then
                    If Baojing_Array.Count >= 1 Then
                        For i_csw = 0 To Baojing_Array.Count - 1
                            alarm_tcq = Baojing_Array(i_csw)
                            If (alarm_tcq.tcq_id = tsc.Tcq_id) Then
                                Baojing_Array.RemoveAt(i_csw)
                                Exit For
                            End If
                        Next
                    End If
                    Fesn(tsc.Tcq_id - 1).il_alarm_pop = False
                End If

                ' 清除T1报警信息
                If Fesn(tsc.Tcq_id - 1).t1_alarm_pop Then
                    If Baojing_Array.Count >= 1 Then
                        For i_csw = 0 To Baojing_Array.Count - 1
                            alarm_tcq = Baojing_Array(i_csw)
                            If (alarm_tcq.tcq_id = tsc.Tcq_id) Then
                                Baojing_Array.RemoveAt(i_csw)
                                Exit For
                            End If
                        Next
                    End If
                    Fesn(tsc.Tcq_id - 1).t1_alarm_pop = False
                End If

                ' 清除T2报警信息
                If Fesn(tsc.Tcq_id - 1).t2_alarm_pop Then
                    If Baojing_Array.Count >= 1 Then
                        For i_csw = 0 To Baojing_Array.Count - 1
                            alarm_tcq = Baojing_Array(i_csw)
                            If (alarm_tcq.tcq_id = tsc.Tcq_id) Then
                                Baojing_Array.RemoveAt(i_csw)
                                Exit For
                            End If
                        Next
                    End If
                    Fesn(tsc.Tcq_id - 1).t2_alarm_pop = False
                End If
            End If
        Next

    End Function

    ''' <summary>
    ''' 释放系统控制的资源，
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Sys_Close() As Boolean

        Try

            ' Dim strlog As String 
            file_log.WriteLine("--------------" & Now.ToString & " 系统关闭--------------")
            file_log.Flush()
            '关闭系统日志文件。
            file_log.Close()

            SaveAlarmLog("--------------" & Now.ToString & " 系统关闭--------------")
            alarm_log.Close()

        Catch ex As Exception
        End Try

    End Function


    ''' <summary>
    ''' 将管理员的密码保存到备份文件中去。
    ''' </summary>
    ''' <param name="pass"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save_GLY_Pass_Into_File(ByVal pass As String) As Boolean
        Dim data_file_path As String
        Try
            '系统数据备份文件sys_main_info，存放位置在 安装路径+ \backup_data 文件夹内。
            data_file_path = My.Application.Info.DirectoryPath & "\backup_data\GLY_PASS"
            Dim df_sw As StreamWriter = File.CreateText(data_file_path)
            df_sw.WriteLine(pass)
            df_sw.Flush()
            df_sw.Close()
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function


    ''' <summary>
    ''' 将管理员的密码保存到备份文件中去。
    ''' </summary>
    ''' <param name="pass"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save_CZY_Pass_Into_File(ByVal pass As String) As Boolean
        Dim data_file_path As String
        Try
            '系统数据备份文件sys_main_info，存放位置在 安装路径+ \backup_data 文件夹内。
            data_file_path = My.Application.Info.DirectoryPath & "\backup_data\CZY_PASS"
            Dim df_sw As StreamWriter = File.CreateText(data_file_path)
            df_sw.WriteLine(pass)
            df_sw.Flush()
            df_sw.Close()
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function


    ''' <summary>
    ''' 服务于FormAlarmFault.vb 检查要加载的文件，是不是当前的报警日志文件
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckIsCurrentFile(ByVal fileName As String) As Boolean
        ' alarm_log()
        '  Dim name As String
        ' alarm_file_path = My.Application.Info.DirectoryPath & "\log\alarm-log\" & Sys_start_date & ".txt"
        ' fileName = My.Application.Info.DirectoryPath & "\log\alarm-log\" & fileName

        If alarm_file_path.EndsWith(fileName) Then
            Return True
        End If
        CheckIsCurrentFile = False
    End Function


End Module
