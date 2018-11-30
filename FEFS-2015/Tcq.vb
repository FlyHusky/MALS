Imports System.Array
''' <summary>
''' 探测器类
''' </summary>
''' <remarks></remarks>
Public Class Tcq

    Public Enum Comm_State_Enum
        ''' <summary>
        '''  连续通讯查询失败次数>= Sys_comm_fail_ct
        ''' </summary>
        ''' <remarks></remarks>
        Comm_Fail = 1

        ''' <summary>
        ''' 用于初始上电时的，等待通讯状态。
        ''' </summary>
        ''' <remarks></remarks>
        Comm_Wait = 2

        ''' <summary>
        ''' 终端只要有一次和主机通讯正常，即视为通讯OK
        ''' </summary>
        ''' <remarks></remarks>
        Comm_OK = 3

        ''' <summary>
        '''  当通讯帧数据的校验码错误时，标记校验错误。
        ''' </summary>
        ''' <remarks></remarks>
        Comm_Jy = 4
    End Enum
 

    ''' <summary>
    ''' 探测器编号，一般也是探测器的物理地址, 唯一，不可重复,同数据库中的id
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Public id As UShort


    ''' <summary>
    ''' 报警器的地址。编号，可以不等于地址。
    ''' </summary>
    ''' <remarks></remarks>
    Public addr As UShort

    ''' <summary>
    ''' 探测器是否启用
    ''' </summary>
    ''' <remarks></remarks>
    Public enable As Byte

    ''' <summary>
    ''' 探测器的编号,意义等于ID，字符串显示 ex:【0001】
    ''' </summary>
    ''' <remarks></remarks>
    Public id_str As String

    ''' <summary>
    ''' 报警状态标志位 0=关闭，1=打开。
    ''' </summary>
    ''' <remarks></remarks>
    Public alarm As Byte

    ''' <summary>
    ''' 硬件故障标志位
    ''' </summary>
    ''' <remarks></remarks>
    Public guzhang As Byte

    ''' <summary>
    ''' 弹出电流报警标志。。
    ''' </summary>
    ''' <remarks></remarks>
    Public il_alarm_pop As Boolean

    ''' <summary>
    '''  弹出T1报警标志。。
    ''' </summary>
    ''' <remarks></remarks>
    Public t1_alarm_pop As Boolean

    ''' <summary>
    ''' 弹出T2报警标志。。
    ''' </summary>
    ''' <remarks></remarks>
    Public t2_alarm_pop As Boolean


    ''' <summary>
    ''' 剩余电流传感器开路或短路故障弹出标志
    ''' </summary>
    ''' <remarks></remarks>
    Public il_error_pop As Boolean

    ''' <summary>
    ''' T1传感器开路短路故障弹出标志
    ''' </summary>
    ''' <remarks></remarks>
    Public t1_error_pop As Boolean

    ''' <summary>
    ''' T2传感器开路短路故障弹出标志
    ''' </summary>
    ''' <remarks></remarks>
    Public t2_error_pop As Boolean

    ''' <summary>
    ''' 探测器的表类型：           探测器端
    ''' 0：独立式 IL + 2T         0x12
    ''' 1：独立式 IL + 1T         0x11
    ''' 2: 独立式 IL              0x10
    ''' 3：非独立式 IL + 1T       0x21
    ''' 4: 非独立式 IL            0x20
    ''' 5: 非独立式 IL + 2T       0x22
    ''' 
    ''' 探测器端：独立式的0001 
    ''' </summary>
    ''' <remarks></remarks>
    Public type As Byte


    ''' <summary>
    ''' 报警器数据为1:当前音调
    ''' </summary>
    ''' <remarks></remarks>
    Public Data1 As Byte


    ''' <summary>
    ''' 报警器数据为2:当前音量
    ''' </summary>
    ''' <remarks></remarks>
    Public Data2 As Byte


    ''' <summary>
    '''  温度1传感器 使用标志
    ''' </summary>
    ''' <remarks></remarks>
    Public T1_Enable As Boolean

    ''' <summary>
    '''  温度2传感器 使用标志
    ''' </summary>
    ''' <remarks></remarks>
    Public T2_Enable As Boolean

    ''' <summary>
    ''' 探测器所在箱体的名称
    ''' </summary>
    ''' <remarks></remarks>
    Public name As String

    ''' <summary>
    ''' 电流预警值
    ''' </summary>
    ''' <remarks></remarks>
    Public IL_Yujin As UShort

    ''' <summary>
    ''' 电流报警值
    ''' </summary>
    ''' <remarks></remarks>
    Public IL_Baojin As Single

    ''' <summary>
    ''' 温度1预警值
    ''' </summary>
    ''' <remarks></remarks>
    Public T1_Yujin As UShort

    ''' <summary>
    '''  温度1报警值
    ''' </summary>
    ''' <remarks></remarks>
    Public T1_baojin As Single

    ''' <summary>
    '''  温度2预警值
    ''' </summary>
    ''' <remarks></remarks>
    Public T2_Yujin As UShort

    ''' <summary>
    '''  温度2报警值
    ''' </summary>
    ''' <remarks></remarks>
    Public T2_baojin As Single
 
    ''' <summary>
    ''' 通信状态
    ''' </summary>
    ''' <remarks></remarks>
    Public Comm_State As Comm_State_Enum

    ''' <summary>
    ''' 通讯故障累加>3次，则通讯故障，报警
    ''' </summary>
    ''' <remarks></remarks>
    Public Comm_Fail_Count As Byte

    ''' <summary>
    ''' true=故障报警弹出， fasle=没有故障
    ''' </summary>
    ''' <remarks></remarks>
    Public comm_Fail_Sta As Boolean

    ''' <summary>
    ''' 探测器所归属的通讯支路
    ''' </summary>
    ''' <remarks></remarks>
    Public net As Byte

    ''' <summary>
    ''' 探测器在所归属的通讯支路的顺序位置
    ''' </summary>
    ''' <remarks></remarks>
    Public net_id As Byte

    ''' <summary>
    '''实时的剩余电流值
    ''' </summary>
    ''' <remarks></remarks>
    Public IL As Single


    Public IL_BJ As Single

    Public T1_BJ As Single

    Public T2_BJ As Single




    ''' <summary>
    ''' 实时的温度T1
    ''' </summary>
    ''' <remarks></remarks>
    Public T1 As Single

    ''' <summary>
    ''' 实时的温度值T2
    ''' </summary>
    ''' <remarks></remarks>
    Public T2 As Single

    ''' <summary>
    ''' 电流报警时的最大值
    ''' </summary>
    ''' <remarks></remarks>
    Public IL_Baojin_Max As Single

    ''' <summary>
    ''' T1报警时的最大值
    ''' </summary>
    ''' <remarks></remarks>
    Public T1_Baojin_Max As Single

    ''' <summary>
    ''' T2报警时的最大值
    ''' </summary>
    ''' <remarks></remarks>
    Public T2_Baojin_Max As Single

    ''' <summary>
    ''' 先前的通讯状态 1=已连接  0=未连接
    ''' </summary>
    ''' <remarks></remarks>
    Public Pre_comm As Byte

End Class

''' <summary>
''' 故障报警信息对象
''' </summary>
''' <remarks></remarks>
Public Class Guzhang_info
    Public Enum Guzhang_enum
        Comm_Error  '通讯故障
        IL_Error    '电流传感器故障
        T1_Error    'T1传感器故障
        T2_Error    'T2传感器故障
        Main_Power_Error         '主电源故障
        Backup_Power_Error       '背电源故障
    End Enum
    Public tcq_id_str As String  '发生故障的探测器id.如果是系统故障代码为：1000之后的。
    Public time_str As String    '故障发生时间
    Public date_str As String    '故障发生日期
    Public Guzhang_kind As Guzhang_enum  '故障类型（1：通讯故障 ，2:主电源欠压，3：背电源欠压）
    Public tcq_name As String    '探测器箱号 
    Public tcq_id As UShort  '探测器的地址。
End Class


''' <summary>
''' 监控报警信息对象
''' </summary>
''' <remarks></remarks>
Public Class Alarm_info
    Public Enum alarm_enum
        IL_alarm
        T1_alarm
        T2_alarm
    End Enum
    Public tcq_id_str As String  '发生报警的探测器id_str
    Public tcq_id As UShort
    Public time_str As String    '报警发生的时间
    Public date_str As String    '报警发生的日期
    Public Alarm_kind As alarm_enum  '报警类型
    Public tcq_name As String        '探测器箱号
    Public alarm_data As String      '报警时相关的参数值
    Public alarm_sure As Boolean     '报警是否确定
    Public sql_str As String     '数据库字符串
End Class


''' <summary>
''' FEFS系统用户类
''' </summary>
''' <remarks></remarks>
Public Class FEFS_USER
    ''' <summary>
    '''  用户名(小于10个字符)
    ''' </summary>
    ''' <remarks></remarks>
    Public user_name As String
    ''' <summary>
    '''  用户密码(小于20个字符)
    ''' </summary>
    ''' <remarks></remarks>
    Public user_password As String
    ''' <summary>
    ''' 用户权限(操作员or管理员)
    ''' </summary>
    ''' <remarks></remarks>
    Public user_level As String
End Class





Public Class net_map_cl
    Public id As Byte
    Public name As String
    Public tcqs() As Byte
End Class

Public Class tcq_map_cl
    Public id As Byte
    Public name As String
    Public tcqs() As Byte
End Class


Public Class Tcq_Self_Check
    Public Tcq_id As UShort
    ''' <summary>
    ''' 自检结果 2=等待 1=完成 0=失败
    ''' </summary>
    ''' <remarks></remarks>
    Public Self_Check_Result As Byte

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Comm As Boolean


End Class

''' <summary>
'''  待发送的图像显示信息对象类。
''' </summary>
''' <remarks></remarks>
Public Class TX_Send_Info


    ''' <summary>
    ''' 对象序号
    ''' </summary>
    ''' <remarks></remarks>
    Public id As UShort


    ''' <summary>
    ''' 探测器地址
    ''' </summary>
    ''' <remarks></remarks>
    Public tcq_id As UShort

    ''' <summary>
    '''  信息类型 1=产生报警  2=产生故障   3=故障恢复. 4=主电故障  5=主电恢复  6=备电故障  7=备电恢复 
    ''' </summary>
    ''' <remarks></remarks>
    Public info_kind As Byte

    ''' <summary>
    ''' 报警类型 1=漏电流报警   2=温度1/2报警
    ''' </summary>
    ''' <remarks></remarks>
    Public baojin_kind As String


    ''' <summary>
    ''' A=漏电故障   B=温度故障   P=通讯故障
    ''' </summary>
    ''' <remarks></remarks>
    Public guzhang_kind As String

    ''' <summary>
    ''' 发生时间
    ''' </summary>
    ''' <remarks></remarks>
    Public happen_time As String


    ''' <summary>
    ''' 通道 1=电流 2=温度1  3=温度2
    ''' </summary>
    ''' <remarks></remarks>
    Public tongdao As String

End Class
