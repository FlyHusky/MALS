Imports System.Runtime.InteropServices
Public Class SUSI
    <DllImport("Susi.dll")> Public Shared Function SusiDllGetLastError() As Integer
    End Function
    <DllImport("Susi.dll")> Public Shared Sub SusiDllGetVersion(ByRef major As UInteger, ByRef minor As UInteger)
    End Sub
    <DllImport("Susi.dll")> Public Shared Function SusiDllInit() As Boolean
    End Function
    <DllImport("Susi.dll")> Public Shared Function SusiDllUnInit() As Boolean
    End Function
    <DllImport("Susi.dll")> Public Shared Function SusiCoreAvailable() As Integer
        'Check if GPIO driver is availale
        '-1 fail  0:palatform does not support susiIO  1:success
    End Function

    <DllImport("Susi.dll")> Public Shared Function SusiIOWriteMultiEx(ByVal pins As UInteger, ByVal pin_val As UInteger) As Boolean
        'pins-11110000 表示：只操作高四位的值，不操作低四位的值。
    End Function

    <DllImport("Susi.dll")> Public Shared Function SusiIOWriteEx(ByVal PinNum As Byte, ByVal status As Boolean) As Boolean
    End Function

    <DllImport("Susi.dll")> Public Shared Function SusiIOSetDirectionMulti(ByVal pins As UInteger, ByRef pin_dir As UInteger) As Boolean

    End Function

    <DllImport("Susi.dll")> Public Shared Function SusiIOReadEx(ByVal PinNum As Byte, ByRef status As Boolean) As Boolean
    End Function

    '主机SUSI输入输出说明：2017年4月21日12:33:20
    '本工控机共有8个GPIO口，可做为输入输出使用，
    '在本处使用了7个输出，1个输入（第7个口子）。
    '0：主电工作，  
    '1：备电工作
    '2：监控报警
    '3：故障报警
    '4-高，5低--》喇叭
    '6： 控制输出
    '7：复位输入。

    ''' <summary>
    ''' 主机SUSI口初始化，同时标记ATX200_Usart_State。返回 true=成功  false=失败
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Susi_IO_Init() As Boolean
        'Dim return_val As Boolean
        'return_val = False
        'Try
        '    If SusiDllInit() Then
        '        If SusiCoreAvailable() = 1 Then
        '            SUSI_State = True
        '            return_val = True
        '        End If
        '    End If
        'Catch ex As Exception
        '    return_val = False
        '    SUSI_State = False
        'End Try
        'Return return_val
    End Function

    ''' <summary>
    '''   配置扬声器
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Set_Speaker() As Boolean
        'Dim retun_val As Boolean
        'Dim pin_dir As UInteger
        'pin_dir = 0

        'If SUSI_State = False Then
        '    Return False
        'End If

        'Try
        '    retun_val = SusiIOSetDirectionMulti(15, pin_dir)
        'Catch ex As Exception
        '    retun_val = False
        'End Try
        'Return retun_val
    End Function

    ''' <summary>
    '''  设置7个位输出方向，一个位输入方向。。
    '''  同时关闭所有的输出。
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Set_All_IO_Out_Dir() As Boolean
        'Dim retun_val As Boolean
        'Dim pin_dir As UInteger

        'If SUSI_State = False Then
        '    Return False
        'End If

        'pin_dir = 0
        'Try
        '    '设置bit0-6为输出，bit7为输入            1000 0000
        '    retun_val = SusiIOSetDirectionMulti(255, 128)

        '    '关闭bit0-6的输出。
        '    SusiIOWriteMultiEx(127, 0)

        '    'SusiIOWriteEx(7, 1)
        '    'SusiIOWriteEx(4, 0)

        'Catch ex As Exception
        '    retun_val = False
        'End Try
        'Return retun_val
    End Function




    ''' <summary>
    '''  所有的输出清零，关闭指示灯，关闭继电器输出，关闭喇叭
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function All_Out_Off() As Boolean
        'Dim retun_val As Boolean
        'Dim pin_dir As UInteger

        'If SUSI_State = False Then
        '    Return False
        'End If

        'pin_dir = 0
        'Try
        '    '设置bit0-6为输出，bit7为输入            1000 0000
        '    retun_val = SusiIOSetDirectionMulti(255, 128)

        '    '关闭bit0-6的输出。
        '    SusiIOWriteMultiEx(127, 0)

        '    'SusiIOWriteEx(7, 1)
        '    'SusiIOWriteEx(4, 0)

        'Catch ex As Exception
        '    retun_val = False
        'End Try
        'Return retun_val
    End Function

    ''' <summary>
    '''  所有的输出使能
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function All_Out_On() As Boolean
        'Dim retun_val As Boolean

        'If SUSI_State = False Then
        '    Return False
        'End If

        'Try
        '    retun_val = SusiIOWriteMultiEx(127, 127)
        'Catch ex As Exception
        '    retun_val = False
        'End Try
        'Return retun_val
    End Function

    ''' <summary>
    '''  喇叭声输出
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Speaker_On() As Boolean
        'Dim retun_val As Boolean

        'If SUSI_State = False Then
        '    Return False
        'End If

        'Try
        '    SusiIOWriteEx(4, 1)
        '    SusiIOWriteEx(5, 0)
        'Catch ex As Exception
        '    retun_val = False
        'End Try
        'Return retun_val
    End Function

    ''' <summary>
    '''  喇叭声关闭
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Speaker_Off() As Boolean
        'Dim retun_val As Boolean

        'If SUSI_State = False Then
        '    Return False
        'End If

        'Try
        '    SusiIOWriteEx(4, 0)
        '    SusiIOWriteEx(5, 0)
        'Catch ex As Exception
        '    retun_val = False
        'End Try
        'Return retun_val
    End Function

    ''' <summary>
    ''' 获取程序复位信号，返回值int
    ''' 0:出错
    ''' 1：不复位信号
    ''' 2：复位信号
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Get_Res_Singal() As Integer
        Dim retun_val As Boolean
        Dim ret As Integer
        Dim stats As Boolean
        ret = 0


        If SUSI_State = False Then
            Exit Function
        End If


        Try
            retun_val = SusiIOReadEx(7, stats)
            If retun_val Then
                If stats Then
                    ret = 2 '不复位信号
                Else
                    ret = 1 '复位信号
                End If
            Else
                ret = 0
                Exit Function
            End If

        Catch ex As Exception
            ret = 0
        End Try

        Return ret

    End Function


    ''' <summary>
    '''  主电源灯泡亮
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Main_Power_Lamp_On() As Boolean


        'Dim retun_val As Boolean

        'If SUSI_State = False Then
        '    Return False
        'End If



        'Try
        '    retun_val = SusiIOWriteEx(0, 1)
        'Catch ex As Exception
        '    retun_val = False
        'End Try
        'Return retun_val
    End Function

    ''' <summary>
    '''  主电源灯泡灭
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Main_Power_Lamp_Off() As Boolean
        'Dim retun_val As Boolean

        'If SUSI_State = False Then
        '    Return False
        'End If


        'Try
        '    retun_val = SusiIOWriteEx(0, 0)
        'Catch ex As Exception
        '    retun_val = False
        'End Try
        'Return retun_val
    End Function

    ''' <summary>
    '''  备电源灯泡亮
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Back_Power_Lamp_On() As Boolean
        'Dim retun_val As Boolean
        'If SUSI_State = False Then
        '    Return False
        'End If


        'Try
        '    retun_val = SusiIOWriteEx(1, 1)
        'Catch ex As Exception
        '    retun_val = False
        'End Try
        'Return retun_val
    End Function

    ''' <summary>
    '''  备电源灯泡灭
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Back_Power_Lamp_Off() As Boolean
        'Dim retun_val As Boolean

        'If SUSI_State = False Then
        '    Return False
        'End If


        'Try
        '    retun_val = SusiIOWriteEx(1, 0)
        'Catch ex As Exception
        '    retun_val = False
        'End Try
        'Return retun_val
    End Function

    ''' <summary>
    '''  报警灯泡亮
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Baojing_Lamp_On() As Boolean
        'Dim retun_val As Boolean

        'If SUSI_State = False Then
        '    Return False
        'End If


        'Try
        '    retun_val = SusiIOWriteEx(2, 1)
        'Catch ex As Exception
        '    retun_val = False
        'End Try
        'Return retun_val
    End Function

    ''' <summary>
    '''  报警灯泡灭
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Baojing_Lamp_Off() As Boolean
        'Dim retun_val As Boolean

        'If SUSI_State = False Then
        '    Return False
        'End If


        'Try
        '    retun_val = SusiIOWriteEx(2, 0)
        'Catch ex As Exception
        '    retun_val = False
        'End Try
        'Return retun_val
    End Function

    ''' <summary>
    '''  故障灯泡亮
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Guzhang_Lamp_On() As Boolean
        'Dim retun_val As Boolean

        'If SUSI_State = False Then
        '    Return False
        'End If


        'Try
        '    retun_val = SusiIOWriteEx(3, 1)
        'Catch ex As Exception
        '    retun_val = False
        'End Try
        'Return retun_val
    End Function

    ''' <summary>
    '''  故障灯泡灭
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Guzhang_Lamp_Off() As Boolean
        'Dim retun_val As Boolean

        'If SUSI_State = False Then
        '    Return False
        'End If


        'Try
        '    retun_val = SusiIOWriteEx(3, 0)
        'Catch ex As Exception
        '    retun_val = False
        'End Try
        'Return retun_val
    End Function

    ''' <summary>
    '''  继电器输出
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function JDQ_On() As Boolean
        'Dim retun_val As Boolean

        'If SUSI_State = False Then
        '    Return False
        'End If


        'Try
        '    retun_val = SusiIOWriteEx(6, 1)
        'Catch ex As Exception
        '    retun_val = False
        'End Try
        'Return retun_val
    End Function

    ''' <summary>
    '''  继电器不输出。
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function JDQ_Off() As Boolean
        'Dim retun_val As Boolean

        'If SUSI_State = False Then
        '    Return False
        'End If


        'Try
        '    retun_val = SusiIOWriteEx(6, 0)
        'Catch ex As Exception
        '    retun_val = False
        'End Try
        'Return retun_val
    End Function


End Class

