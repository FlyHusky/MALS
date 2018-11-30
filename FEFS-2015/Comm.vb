'本模块服务于天冠报警灯，包含以下内容
'1: 通讯相关字符定义，相关函数实现
'2：先关全局参数变量设置。

Module Comm

    '--------------------------------系统全局变量-start---------------------------
    ''' <summary>
    ''' 低级操作是否需要密码，低级操作包含如下
    ''' 1：控制单个报警灯
    ''' 2：报警灯参数设置
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PassWordNeedLow As Boolean = False

    ''' <summary>
    ''' 中级操作是否需要密码，中级操作包含如下
    ''' 1：报警灯总开，总关
    ''' 2：系统复位/关机
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PassWordNeedMidd As Boolean = True

    ''' <summary>
    ''' 高级操作是否需要密码，高级操作包含如下
    ''' 1：系统关键参数设置
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PassWordNeedHigh As Boolean = True

    ''' <summary>
    ''' 主机发送通讯-进度标识
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CommProgressEnum
        ''' <summary>
        ''' 空
        ''' </summary>
        ''' <remarks></remarks>
        free = 0

        ''' <summary>
        ''' PC主机需要发送指令
        ''' </summary>
        ''' <remarks></remarks>
        PcNeedSend = 1

        ''' <summary>
        ''' PC 主机已经发送，等待接收
        ''' </summary>
        ''' <remarks></remarks>
        PcNeedRec = 2

        ''' <summary>
        ''' 终端回响正确 
        ''' </summary>
        ''' <remarks></remarks>
        ReechoSuc = 3

        ''' <summary>
        ''' 终端回响错误
        ''' </summary>
        ''' <remarks></remarks>
        ReechoFail = 4

    End Enum


    ''' <summary>
    ''' 单次通讯任务进度标识-打开报警器(记忆音调)指令-循环播放
    ''' </summary>
    ''' <remarks></remarks>
    Public PcSendAlarmOnMem As CommProgressEnum

    ''' <summary>
    ''' 单次通讯任务进度标识-打开报警器(记忆音调)指令-单曲播放
    ''' </summary>
    ''' <remarks></remarks>
    Public PcSendAlarmOnMemOnce As CommProgressEnum

    ''' <summary>
    ''' 单次通讯任务进度标识-主机需要发送关闭报警器通讯指令
    ''' </summary>
    ''' <remarks></remarks>
    Public PcSendAlarmOff As CommProgressEnum

    ''' <summary>
    ''' 单次通讯任务进度标识-打开报警，指定音量，音调，循环播放
    ''' </summary>
    ''' <remarks></remarks>
    Public PcSendAlarmOnAssLoop As CommProgressEnum

    ''' <summary>
    ''' 单次通讯任务进度标识-打开报警，指定音量，音调，单曲播放
    ''' </summary>
    ''' <remarks></remarks>
    Public PcSendAlarmOnAssOnce As CommProgressEnum

    ''' <summary>
    ''' 单次通讯任务进度标识-指定音调值
    ''' </summary>
    ''' <remarks></remarks>
    Public PcSendAlarmMusic As Byte

    ''' <summary>
    ''' 单次通讯任务进度标识-指定音量值
    ''' </summary>
    ''' <remarks></remarks>
    Public PcSendAlarmVolum As Byte

    ''' <summary>
    ''' 单次通讯任务进度标识-音量加
    ''' </summary>
    ''' <remarks></remarks>
    Public PcSendVolumAdd As CommProgressEnum

    ''' <summary>
    ''' 单次通讯任务进度标识-音量减
    ''' </summary>
    ''' <remarks></remarks>
    Public PcSendVolumRec As CommProgressEnum

    ''' <summary>
    ''' 单次通讯任务进度标识-下一曲
    ''' </summary>
    ''' <remarks></remarks>
    Public PcSendMusicNext As CommProgressEnum

    ''' <summary>
    ''' 单次通讯任务进度标识-上一曲
    ''' </summary>
    ''' <remarks></remarks>
    Public PcSendMusicPre As CommProgressEnum

    ''' <summary>
    ''' 单次通讯任务进度标识- 报警器的地址
    ''' </summary>
    ''' <remarks></remarks>
    Public PcSendBjqAddr As Integer

    ''' <summary>
    ''' 单次通讯任务进度标识- 手动查询报警器状态一次
    ''' </summary>
    ''' <remarks></remarks>
    Public PcSendChaxunOnce As Integer

    ''' <summary>
    ''' 单次通讯任务变量标识-主机发送打开/关闭 报警器的对象下标
    ''' </summary>
    ''' <remarks></remarks>
    Public PcSendBjqindex As Integer

    ''' <summary>
    ''' 单次通讯任务变量标识-主机要发送的通信数据
    ''' </summary>
    ''' <remarks></remarks>
    Public PcSendToBjq(8) As Byte


    '------------------------------------全局变量值-end-------------------------



    '-----------------------------与通讯相关的宏定义如下-----------------------
    ''' <summary>
    ''' 报警器地址最大值
    ''' </summary>
    ''' <remarks></remarks>
    Public Const BJQMAXADDR As Integer = 54


    ''' <summary>
    ''' 约定-通讯报文头
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MES_HEAD = &HDD

    ''' <summary>
    ''' 约定-通讯报文尾
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MES_TAIL = &HAA

    ''' <summary>
    ''' 约定-本机地址（即PC 上位机）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MES_LOCAL = &H66

    ''' <summary>
    ''' 约定-广播地址(报警器)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MES_ALL_BJQ_ADDR As Byte = &H55


    ''' <summary>
    ''' 报警器状态控制功能码
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MES_CONTR = &H1


    ''' <summary>
    ''' 功能码-设置通讯地址
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MES_SET_ADDR As Byte = &H3

    ''' <summary>
    ''' 功能码-报警器参数/状态查询
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MES_CHAXUN = &H5

    ''' <summary>
    ''' 功能码-音量控制
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MES_VOLUM_CTR As Byte = &H6


    ''' <summary>
    ''' 功能码-音调控制
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MES_MUSIC_CTR As Byte = &H7

    ''' <summary>
    ''' 功能码-打开报警指定音量+音调，循环播放
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MES_ALARM_ASS_LOOP As Byte = &HA

    ''' <summary>
    ''' 功能码-打开报警指定音量+音调，单曲播放
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MES_ALARM_ASS_ONCE As Byte = &HB


    ''' <summary>
    ''' 功能码-打开报警，记忆音调，音量，单曲播放
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MES_ALARM_MEM_ONCE As Byte = &H11

    '-----------------------------通讯报文-宏定义-end --------------------------


    ''' <summary>
    ''' 广播-所有报警器报警
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CommAlarmAllOn(ByRef sendData As Byte()) As Boolean
        sendData(0) = MES_HEAD  '报头
        sendData(1) = MES_LOCAL '本机地址
        sendData(2) = MES_ALL_BJQ_ADDR      '报警灯地址
        sendData(3) = MES_CONTR '控制功能码
        sendData(4) = &H0
        sendData(5) = &H1
        sendData(8) = MES_TAIL

        Dim it As Byte
        Dim sum As UInteger

        sum = 0

        For it = 0 To 5
            sum = sum + sendData(it)
        Next

        'jyh 为 sum 的 高16位； jyl 为 sum 的低 16位。

        sendData(6) = sum \ 256     ' 整除，取商
        sendData(7) = sum Mod 256   ' 整除，取余数
        CommAlarmAllOn = True
    End Function


    ''' <summary>
    ''' 广播-所有报警器 - 关闭报警
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CommAlarmAllOff(ByRef sendData As Byte()) As Boolean

        sendData(0) = MES_HEAD  '报头
        sendData(1) = MES_LOCAL '本机地址
        sendData(2) = MES_ALL_BJQ_ADDR      '报警灯地址
        sendData(3) = MES_CONTR '控制功能码
        sendData(4) = &H0
        sendData(5) = &H2
        sendData(8) = MES_TAIL

        Dim it As Byte
        Dim sum As UInteger

        sum = 0

        For it = 0 To 5
            sum = sum + sendData(it)
        Next

        'jyh 为 sum 的 高16位； jyl 为 sum 的低 16位。

        sendData(6) = sum \ 256     ' 整除，取商
        sendData(7) = sum Mod 256   ' 整除，取余数
        CommAlarmAllOff = True
    End Function


    ''' <summary>
    ''' 报文生成-主机打开报警器（记忆音调，音量循环播放）
    ''' </summary>
    ''' <param name="addr">报警灯地址-输入</param>
    ''' <param name="sendData">通讯报文素组-返回</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CommAlarmOnMem(ByVal addr As Byte, ByRef sendData As Byte()) As Boolean
        '首先对传入的参数做有效性校验
        If addr > BJQMAXADDR Then
            Return False
        End If

        sendData(0) = MES_HEAD  '报头
        sendData(1) = MES_LOCAL '本机地址
        sendData(2) = addr      '报警灯地址
        sendData(3) = MES_CONTR '控制功能码
        sendData(4) = &H0
        sendData(5) = &H1
        sendData(8) = MES_TAIL

        Dim it As Byte
        Dim sum As UInteger

        sum = 0

        For it = 0 To 5
            sum = sum + sendData(it)
        Next

        'jyh 为 sum 的 高16位； jyl 为 sum 的低 16位。

        sendData(6) = sum \ 256     ' 整除，取商
        sendData(7) = sum Mod 256   ' 整除，取余数

    End Function

    ''' <summary>
    ''' 报文生成-主机打开报警器（记忆音调，音量单曲播放）
    ''' </summary>
    ''' <param name="addr">报警灯地址-输入</param>
    ''' <param name="sendData">通讯报文素组-返回</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CommAlarmOnMemOnce(ByVal addr As Byte, ByRef sendData As Byte()) As Boolean
        '首先对传入的参数做有效性校验
        If addr > BJQMAXADDR Then '有效地址范围0~247
            Return False
        End If

        sendData(0) = MES_HEAD  '报头
        sendData(1) = MES_LOCAL '本机地址
        sendData(2) = addr      '报警灯地址
        sendData(3) = MES_ALARM_MEM_ONCE '控制功能码
        sendData(4) = &H0
        sendData(5) = &H1
        sendData(8) = MES_TAIL

        Dim it As Byte
        Dim sum As UInteger

        sum = 0

        For it = 0 To 5
            sum = sum + sendData(it)
        Next

        'jyh 为 sum 的 高16位； jyl 为 sum 的低 16位。

        sendData(6) = sum \ 256     ' 整除，取商
        sendData(7) = sum Mod 256   ' 整除，取余数

    End Function

    ''' <summary>
    ''' 报文生成-主机打开报警器（指定音调，音量循环播放）
    ''' </summary>
    ''' <param name="addr">输入-报警器地址</param>
    ''' <param name="music">输入-指定的音调</param>
    ''' <param name="volum">输入-指定的音量</param>
    ''' <param name="sendData">返回-生成好的通讯报文</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CommAlarmOnAssLoop(ByVal addr As Byte, ByVal music As Byte, ByVal volum As Byte, ByRef sendData As Byte()) As Boolean
        '1：对传入的参数做有效性校验
        If addr > BJQMAXADDR Then '有效地址范围 
            Return False
        End If

        '2： 对音调和音量做有效性校验-暂时不实现。
        sendData(0) = MES_HEAD  '报头
        sendData(1) = MES_LOCAL '本机地址
        sendData(2) = addr      '报警灯地址
        sendData(3) = MES_ALARM_ASS_LOOP  '控制功能码
        sendData(4) = music     '指定的音调
        sendData(5) = volum     '指定的音量
        sendData(8) = MES_TAIL

        Dim it As Byte
        Dim sum As UInteger

        sum = 0

        For it = 0 To 5
            sum = sum + sendData(it)
        Next

        'jyh 为 sum 的 高16位； jyl 为 sum 的低 16位。

        sendData(6) = sum \ 256     ' 整除，取商
        sendData(7) = sum Mod 256   ' 整除，取余数

    End Function

    ''' <summary>
    ''' 生成报文-主机打开报警器（指定音调，音量 单曲播放）
    ''' </summary>
    ''' <param name="addr">输入-报警器地址</param>
    ''' <param name="music">输入-指定的音调</param>
    ''' <param name="volum">输入-指定的音量</param>
    ''' <param name="sendData">返回-生成好的通讯报文</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CommAlarmOnAssOnce(ByVal addr As Byte, ByVal music As Byte, ByVal volum As Byte, ByRef sendData As Byte()) As Boolean
        '1：对传入的参数做有效性校验
        If addr > BJQMAXADDR Then '有效地址范围 
            Return False
        End If


        '2： 对音调和音量做有效性校验-暂时不实现。
        sendData(0) = MES_HEAD  '报头
        sendData(1) = MES_LOCAL '本机地址
        sendData(2) = addr      '报警灯地址
        sendData(3) = MES_ALARM_ASS_ONCE   '控制功能码
        sendData(4) = music     '指定的音调
        sendData(5) = volum     '指定的音量
        sendData(8) = MES_TAIL

        Dim it As Byte
        Dim sum As UInteger

        sum = 0

        For it = 0 To 5
            sum = sum + sendData(it)
        Next

        'jyh 为 sum 的 高16位； jyl 为 sum 的低 16位。

        sendData(6) = sum \ 256     ' 整除，取商
        sendData(7) = sum Mod 256   ' 整除，取余数

    End Function

    ''' <summary>
    ''' 报文生成-主机关闭报警器
    ''' </summary>
    ''' <param name="addr">报警灯地址-输入</param>
    ''' <param name="sendData">通讯报文素组-返回</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CommAlarmOff(ByVal addr As Byte, ByRef sendData As Byte()) As Boolean
        '首先对传入的参数做有效性校验
        If addr > BJQMAXADDR Then '有效地址范围0~247
            Return False
        End If

        sendData(0) = MES_HEAD  '报头
        sendData(1) = MES_LOCAL '本机地址
        sendData(2) = addr      '报警灯地址
        sendData(3) = MES_CONTR '控制功能码
        sendData(4) = &H0
        sendData(5) = &H2
        sendData(8) = MES_TAIL

        Dim it As Byte
        Dim sum As UInteger

        sum = 0

        For it = 0 To 5
            sum = sum + sendData(it)
        Next

        'jyh 为 sum 的 高16位； jyl 为 sum 的低 16位。

        sendData(6) = sum \ 256     ' 整除，取商
        sendData(7) = sum Mod 256   ' 整除，取余数

    End Function

    ''' <summary>
    ''' 报文生成-主机发送-音量加减
    ''' </summary>
    ''' <param name="addr">输入-报警器地址-</param>
    ''' <param name="add_rec">输入： 1=音量+1   2=音量-1 </param>
    ''' <param name="sendData">返回-通讯报文</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CommVolumAddRec(ByVal addr As Byte, ByVal add_rec As Byte, ByRef sendData As Byte()) As Boolean
        '首先对传入的参数做有效性校验
        If addr > BJQMAXADDR Then '有效地址范围0~247
            Return False
        End If

        If add_rec <> 1 And add_rec <> 2 Then
            Return False
        End If

        sendData(0) = MES_HEAD  '报头
        sendData(1) = MES_LOCAL '本机地址
        sendData(2) = addr      '报警灯地址
        sendData(3) = MES_VOLUM_CTR  '控制功能码
        sendData(4) = &H0
        sendData(5) = add_rec   '1=音量+1   2=音量-1
        sendData(8) = MES_TAIL

        Dim it As Byte
        Dim sum As UInteger

        sum = 0

        For it = 0 To 5
            sum = sum + sendData(it)
        Next

        'jyh 为 sum 的 高16位； jyl 为 sum 的低 16位。

        sendData(6) = sum \ 256     ' 整除，取商
        sendData(7) = sum Mod 256   ' 整除，取余数

    End Function

    ''' <summary>
    ''' 报文生成-主机发送-音调设置上一首/下一首
    ''' </summary>
    ''' <param name="addr">输入-报警器地址-</param>
    ''' <param name="add_rec">输入： 1=下一首   2=上一首 </param>
    ''' <param name="sendData">返回-通讯报文</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CommMusicPreNext(ByVal addr As Byte, ByVal add_rec As Byte, ByRef sendData As Byte()) As Boolean
        '首先对传入的参数做有效性校验
        If addr > BJQMAXADDR Then '有效地址范围0~247
            Return False
        End If

        If add_rec <> 1 And add_rec <> 2 Then
            Return False
        End If

        sendData(0) = MES_HEAD  '报头
        sendData(1) = MES_LOCAL '本机地址
        sendData(2) = addr      '报警灯地址
        sendData(3) = MES_MUSIC_CTR   '控制功能码
        sendData(4) = &H0
        sendData(5) = add_rec   '1=下一首   2=上一首
        sendData(8) = MES_TAIL

        Dim it As Byte
        Dim sum As UInteger

        sum = 0

        For it = 0 To 5
            sum = sum + sendData(it)
        Next

        'jyh 为 sum 的 高16位； jyl 为 sum 的低 16位。

        sendData(6) = sum \ 256     ' 整除，取商
        sendData(7) = sum Mod 256   ' 整除，取余数

    End Function

    ''' <summary>
    ''' 报文生成-主机发送-查询报警器状态和参数  
    ''' </summary>
    ''' <param name="addr">报警灯地址-输入</param>
    ''' <param name="sendData">通讯报文-返回</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CommChaxun(ByVal addr As Byte, ByRef sendData As Byte()) As Boolean
        '首先对传入的参数做有效性校验
        If addr > BJQMAXADDR Then '有效地址范围0~247
            Return False
        End If

        sendData(0) = MES_HEAD  '报头
        sendData(1) = MES_LOCAL '本机地址
        sendData(2) = addr      '报警灯地址
        sendData(3) = MES_CHAXUN '查询功能码
        sendData(4) = &H0
        sendData(5) = &H1
        sendData(8) = MES_TAIL

        Dim it As Byte
        Dim sum As UInteger

        sum = 0

        For it = 0 To 5
            sum = sum + sendData(it)
        Next

        'jyh 为 sum 的 高16位； jyl 为 sum 的低 16位。

        sendData(6) = sum \ 256     ' 整除，取商
        sendData(7) = sum Mod 256   ' 整除，取余数

    End Function

    ''' <summary>
    ''' 报文生成-主机发送-设置通讯地址报文
    ''' </summary>
    ''' <param name="addr">报警灯地址-输入</param>
    ''' <param name="sendData">通讯报文-返回</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CommSetAddr(ByVal addr As Byte, ByRef sendData As Byte()) As Boolean
        '首先对传入的参数做有效性校验
        If addr > BJQMAXADDR Then '有效地址范围0~247
            Return False
        End If

        sendData(0) = MES_HEAD              '报头
        sendData(1) = MES_LOCAL             '本机地址
        sendData(2) = MES_ALL_BJQ_ADDR      '报警灯地址
        sendData(3) = MES_SET_ADDR            '查询功能码
        sendData(4) = &H0
        sendData(5) = addr
        sendData(8) = MES_TAIL

        Dim it As Byte
        Dim sum As UInteger

        sum = 0

        For it = 0 To 5
            sum = sum + sendData(it)
        Next

        'jyh 为 sum 的 高16位； jyl 为 sum 的低 16位。

        sendData(6) = sum \ 256     ' 整除，取商
        sendData(7) = sum Mod 256   ' 整除，取余数

    End Function

    ''' <summary>
    ''' 对终端节点回响的数据进行校验
    ''' 1：校验帧的起始位，结束为，本地地址，目标地址，校验位。
    ''' </summary>
    ''' <param name="addr">终端地址</param>
    ''' <param name="recallData">终端回响数组</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function NodeRecallCheck(ByVal addr As Byte, ByRef recallData As Byte()) As Boolean
        NodeRecallCheck = False

        If recallData(0) <> MES_HEAD Then
            Return False
        End If

        If recallData(1) <> addr Then
            Return False
        End If

        If recallData(2) <> MES_LOCAL Then
            Return False
        End If

        If recallData(8) <> MES_TAIL Then
            Return False
        End If

        Dim it As Byte
        Dim sum As UInteger

        sum = 0

        For it = 0 To 5
            sum = sum + recallData(it)
        Next

        'jyh 为 sum 的 高16位； jyl 为 sum 的低 16位。

        ' 整除，取商
        If recallData(6) <> (sum \ 256) Then
            Return False
        End If


        If recallData(7) <> (sum Mod 256) Then   ' 整除，取余数
            Return False
        End If

        NodeRecallCheck = True

    End Function

    ''' <summary>
    ''' 根据鼠标当前点击的位置：找到合适的放置弹出窗体的位置
    ''' </summary>
    ''' <param name="curPos">输入：当前鼠标点击的坐标位置。 输出：合适的坐标</param>
    ''' <param name="wid">弹出窗体的宽度</param>
    ''' <param name="hei">弹出窗体的高度</param>
    ''' <param name="form_w">主窗体的宽度</param>
    ''' <param name="form_h">主窗体的高度</param>
    ''' <returns>true=找到合适位置  false=未找到合适的位置</returns>
    ''' <remarks></remarks>
    Public Function FindRightPosation(ByRef curPos As System.Drawing.Point, ByVal wid As Integer, ByVal hei As Integer, ByVal form_w As Integer, ByVal form_h As Integer) As Boolean
        '横向和纵向的长度值       优先位置选择：下，右
        Dim ws, hs As Integer
 


        '右边
        ws = form_w - curPos.X - wid
        hs = form_h - curPos.Y - hei

        If ws >= 30 And hs >= 30 Then
            curPos.X = curPos.X + 30
            curPos.Y = curPos.Y + 30
            Return True
        End If


        If ws >= 20 Then  'hs值不够
            If curPos.Y > (hei + 30) Then
                curPos.X = curPos.X + 30
                curPos.Y = curPos.Y - hei - 30
                Return True
            End If
        End If


        If hs >= 20 Then  '下方向够，右侧不够。反倒左侧。
            If curPos.X > (wid + 30) Then
                curPos.X = curPos.X - wid - 30
                curPos.Y = curPos.Y + 30
                Return True
            End If

        End If

        '两个方向都不够

        ws = curPos.X - wid - 30
        hs = curPos.Y - hei - 30

        If ws >= 30 And hs >= 30 Then
            curPos.X = ws
            curPos.Y = hs
            Return True
        End If





    End Function

End Module
