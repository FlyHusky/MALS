Public Class Form_Bjd


    Private Bjd1Sta As Byte '报警灯1的状态   


    '动态模拟图窗体下面，最多动态生成64个报警灯，若超过此数量，请使用表格窗体。
    Private BjdButton(0 To 63) As PictureBox  '报警灯的 打开/关闭 按钮图标

    Private BjdLamp(0 To 63) As PictureBox    '报警灯的 指示灯图标

    Private BjdLabel(0 To 63) As Label        '报警灯的 文字图标

    Private BjdCount As Integer               '报警灯的总显示数量


    ''' <summary>
    ''' 发送通讯指令，查询结果等待 的 溢出时间 
    ''' 在timer2中使用到，单周期200ms
    ''' </summary>
    ''' <remarks></remarks>
    Private SendTimeOverCt As Integer

    Private BjdBut(0 To 8) As Button

    Private timer1_ct As Integer


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.Top = Main.Panel4.Top
        Me.Left = Main.Panel4.Left
        Me.Width = Main.Panel4.Width
        Me.Height = Main.Panel4.Height

        Bjd1Sta = 0

        '设置panel1 尺寸

        Panel1.Left = 15

        Panel1.Width = Me.Width - Panel1.Left * 2
        Panel1.Height = Me.Height - Panel1.Top - 10   '底部预留10

        Dim gledf As Integer

        '计算groupbox 高度，依据 BJDpanelStd.height,  
        '两个相邻的报警灯 间隔不应  自身1/2  
        'groupbox top.botoom 值最小为20

        Dim gtop As Integer

        gtop = (Panel1.Height - 20 - 20 - BJDpanelStd.Height * 8) \ 7

        If gtop > BJDpanelStd.Height * 0.5 Then
            gtop = BJDpanelStd.Height * 0.5
        End If


        '如果高度不够，重新定义 BJDpanelStd的高度。
        If gtop < 10 Then
            BJDpanelStd.Height = (Panel1.Height - 90) \ 8
            gtop = 10
        End If


        '计算宽度。
        If Panel1.Width < BJDpanelStd.Width * 4 Then
            BJDpanelStd.Width = (Panel1.Width - 50) \ 4
            gledf = 10
        Else
            '宽度比较大的情况， 最大的间隔宽度，不超过1\3 的width ,

            If (Panel1.Width - BJDpanelStd.Width * 4) \ 5 > BJDpanelStd.Width * 0.33 Then
                '将间隔设置为0.33
                gledf = BJDpanelStd.Width * 0.33
                BJDpanelStd.Width = (Panel1.Width - gledf * 5) \ 4
            Else
                gledf = (Panel1.Width - (BJDpanelStd.Width * 4)) \ 5
            End If

        End If


        BJDgroup1.Height = gtop * 7 + BJDpanelStd.Height * 8
        BJDgroup1.Top = (Panel1.Height - BJDgroup1.Height) \ 2


        BJDgroup1.Left = gledf
        BJDgroup1.Width = BJDpanelStd.Width
        BJDgroup1.Show()

        BJDgroup2.Left = BJDgroup1.Width + BJDgroup1.Left + gledf
        BJDgroup2.Top = BJDgroup1.Top
        BJDgroup2.Height = BJDgroup1.Height
        BJDgroup2.Width = BJDgroup1.Width
        'BJDgroup2.Show()

        BJDgroup3.Left = BJDgroup2.Width + BJDgroup2.Left + gledf
        BJDgroup3.Top = BJDgroup1.Top
        BJDgroup3.Height = BJDgroup1.Height
        BJDgroup3.Width = BJDgroup1.Width
        'BJDgroup3.Show()

        BJDgroup4.Left = BJDgroup3.Width + BJDgroup3.Left + gledf
        BJDgroup4.Top = BJDgroup1.Top
        BJDgroup4.Height = BJDgroup1.Height
        BJDgroup4.Width = BJDgroup1.Width
        'BJDgroup4.Show()



        '设置进度条的样式
        PanPro.Width = Me.Width \ 2
        PanPro.Height = 100
        PanPro.Top = (Me.Height - PanPro.Top) \ 2
        PanPro.Left = (Me.Width - PanPro.Width) \ 2
        PanPro.Visible = False

        '根据系统中节点的数量，来初始化图形报警灯,最大图形化数量32只
        BjdCount = Sys_node_count
        If (BjdCount >= 32) Then
            BjdCount = 32
        End If
        AddBJQ(BjdCount)

        '图形刷新。
        Main.Form_Bjd_Need_Refresh = False
        Map_Refresh()


        '500ms的定时器，用于界面的刷新
        Timer1.Interval = 500
        Timer1.Start()
        timer1_ct = 0
    End Sub

    ''' <summary>
    ''' 向本窗体中添加报警器
    ''' </summary>
    ''' <param name="ct">报警器数量：不能大于32</param>
    ''' <remarks></remarks>
    Private Sub AddBJQ(ByVal ct As Integer)

        BJDgroup1.Visible = False
        BJDgroup2.Visible = False
        BJDgroup3.Visible = False
        BJDgroup4.Visible = False

        If ct <= 8 Then
            AddBjdToGroup(BJDgroup1, 0, ct - 1)
            BJDgroup1.Visible = True
        ElseIf ct <= 16 Then
            AddBjdToGroup(BJDgroup1, 0, 7)
            AddBjdToGroup(BJDgroup2, 8, ct - 1)
            BJDgroup1.Visible = True
            BJDgroup2.Visible = True
        ElseIf ct <= 24 Then
            AddBjdToGroup(BJDgroup1, 0, 7)
            AddBjdToGroup(BJDgroup2, 8, 15)
            AddBjdToGroup(BJDgroup3, 16, ct - 1)
            BJDgroup1.Visible = True
            BJDgroup2.Visible = True
            BJDgroup3.Visible = True
        ElseIf ct <= 32 Then
            AddBjdToGroup(BJDgroup1, 0, 7)
            AddBjdToGroup(BJDgroup2, 8, 15)
            AddBjdToGroup(BJDgroup3, 16, 23)
            AddBjdToGroup(BJDgroup4, 24, ct - 1)
            BJDgroup1.Visible = True
            BJDgroup2.Visible = True
            BJDgroup3.Visible = True
            BJDgroup4.Visible = True
        End If
    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        If Main.Form_Bjd_Need_Refresh Then
            Main.Form_Bjd_Need_Refresh = False
            Map_Refresh()
            timer1_ct = 0
        Else
            timer1_ct = timer1_ct + 1

            If timer1_ct >= 3 Then '1.5秒
                Map_Refresh()
                timer1_ct = 0
            End If

        End If

    End Sub


    ''' <summary>
    '''  图形界面刷新
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Map_Refresh()

        Dim i As Integer

        '通讯正常-绿色按钮，  故障-红色按钮
        For i = 0 To BjdCount - 1
            If Fesn(i).Comm_State = Tcq.Comm_State_Enum.Comm_OK Then
                BjdButton(i).Image = BJDbuttonStdGreen.Image
                '通讯正常状态，在显示报警的状态。
                If Fesn(i).alarm = 1 Then
                    BjdLamp(i).Image = BJDlampStdRed.Image
                Else
                    BjdLamp(i).Image = BJDlampStdGreen.Image
                End If
            Else
                BjdButton(i).Image = BJDbuttonStd.Image
                If Fesn(i).alarm = 1 Then
                    BjdLamp(i).Image = BJDlampStdRed.Image
                Else
                    BjdLamp(i).Image = BJDlampStd.Image
                End If

            End If
        Next

    End Sub

    ' 演示如何动态的添加控件组，同时添加事件
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim i As Integer
        Dim nTop As Long = 0, nLeft As Long = 0
        Dim Label(0 To 3) As Label
        For i = 0 To 3
            Label(i) = New Label
            ' Controls.Add(Label(i))         '控件添加到窗体。
            GroupBox2.Controls.Add(Label(i)) '控件添加到groupbox2
            With Label(i)
                .Height = 30
                .Left = 50
                .Top = 40 * i
                .BorderStyle = BorderStyle.FixedSingle
                .Visible = True
                .Text = "Label" & i
                nLeft += .Width + 10
                AddHandler Label(i).Click, AddressOf label_Click '添加click事件
            End With
        Next i
    End Sub

    '标签的click事件，点击该标签后，释放该控件资源
    Private Sub label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim label1 As Label = CType(sender, Label) '获取当前操作的控件对象，只有这样才能对该控件进行操作
        ' Me.Controls.Remove(label1) '将控件移除
        'label1.Dispose() '释放控件资源
        MessageBox.Show(label1.Text)
    End Sub

    ''' <summary>
    ''' 8通道按钮。用户点击后，系统节点数目设置为8个，图像显示界面同步显示8个报警灯。
    ''' 动态生成8个按钮(image),8个指示灯(image)，8个文本。同时个8个按钮添加单击事件。
    ''' PS: 动态添加的控件，在推出程序前，要用.Dispose() 释放，否则，程序退出报错
    ''' 
    ''' 窗体最多显示32个的报警灯模拟图(待测试在最低1024*768分辨率下的数量)
    ''' 页面布局设置：
    ''' 4个Groupbox，每个Groupbox 内显示8个报警灯模拟图，总计最多显示32个。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
      
        'If BjdCount = 8 Then
        '    MessageBox.Show("当前报警器数量是" & BjdCount & "个,无需更改")
        '    Exit Sub
        'End If

        'If MessageBox.Show("当前报警器数量是" & BjdCount & "个", "请确定", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
        '    Exit Sub
        'End If
        'DisposeBJD()
        'BjdCount = 8
        'Sys_node_count = 8
        'AddBJQ(BjdCount)
        'Main.SysReset()
        TextBox1.Text = 8
        HaBjq()
    End Sub


    ''' <summary>
    ''' 向groupbox1 添加报警灯控件，数量不大于8个。
    ''' </summary>
    ''' <param name="ct">报警灯数量，不能大于8个</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AddBjdToGroup1(ByVal ct As Integer) As Boolean
        Dim ntop As Integer

        ntop = 30

        '动态生成8个报警灯。

        ct = ct - 1

        For i = 0 To ct

            BjdButton(i) = New PictureBox ' Button 

            BJDgroup1.Controls.Add(BjdButton(i))

            BjdButton(i).Height = BJDbuttonStd.Height

            BjdButton(i).Width = BJDbuttonStd.Width

            BjdButton(i).Image = BJDbuttonStd.Image.Clone

            BjdButton(i).SizeMode = PictureBoxSizeMode.StretchImage

            BjdButton(i).Top = ntop

            BjdButton(i).Name = "BjdButton" & i

            ntop = ntop + BjdButton(i).Height + 15

            BjdButton(i).Left = BJDbuttonStd.Left

            AddHandler BjdButton(i).Click, AddressOf BjdButton_Click '添加click事件
        Next


        ntop = BJDlampStd.Top - BJDbuttonStd.Top

        For i = 0 To ct
            BjdLamp(i) = New PictureBox ' Button 
            BJDgroup1.Controls.Add(BjdLamp(i))
            BjdLamp(i).Height = BJDlampStd.Height
            BjdLamp(i).Width = BJDlampStd.Width

            BjdLamp(i).Image = BJDlampStd.Image.Clone

            BjdLamp(i).SizeMode = PictureBoxSizeMode.StretchImage

            BjdLamp(i).Top = BjdButton(i).Top + ntop

            BjdLamp(i).Name = "BjdLamp" & i

            BjdLamp(i).Left = BJDlampStd.Left

        Next


        ntop = BJDlabelStd.Top - BJDbuttonStd.Top

        For i = 0 To ct
            BjdLabel(i) = New Label  ' Button 
            BJDgroup1.Controls.Add(BjdLabel(i))

            BjdLabel(i).Height = BJDlabelStd.Height
            ' BjdLabel(i).Width = BJDlabelStd.Width
            Dim ins As Integer
            ins = i + 1

            BjdLabel(i).Text = BJDlabelStd.Text & ins.ToString

            BjdLabel(i).Font = BJDlabelStd.Font


            BjdLabel(i).Top = BjdButton(i).Top + ntop

            BjdLabel(i).Name = "BjdLabel" & i

            BjdLabel(i).Left = BJDlabelStd.Left
            BjdLabel(i).AutoSize = True

        Next

    End Function

    ''' <summary>
    ''' 向groupbox2 添加报警灯控件，数量不大于8个。
    ''' </summary>
    ''' <param name="ct">报警灯数量，不能大于8个</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AddBjdToGroup2(ByVal ct As Integer) As Boolean
        Dim ntop As Integer

        ntop = 30

        '动态生成8个报警灯。
        'ct = 2   ' 编号- 9,10.  下标 8,9 

        ct = ct + 7    'ct = 9 

        For i = 8 To ct

            BjdButton(i) = New PictureBox ' Button 

            BJDgroup2.Controls.Add(BjdButton(i))

            BjdButton(i).Height = BJDbuttonStd.Height

            BjdButton(i).Width = BJDbuttonStd.Width

            BjdButton(i).Image = BJDbuttonStd.Image.Clone

            BjdButton(i).SizeMode = PictureBoxSizeMode.StretchImage

            BjdButton(i).Top = ntop

            BjdButton(i).Name = "BjdButton" & i

            ntop = ntop + BjdButton(i).Height + 15

            BjdButton(i).Left = BJDbuttonStd.Left

            AddHandler BjdButton(i).Click, AddressOf BjdButton_Click '添加click事件
        Next


        ntop = BJDlampStd.Top - BJDbuttonStd.Top

        For i = 8 To ct
            BjdLamp(i) = New PictureBox ' Button 
            BJDgroup2.Controls.Add(BjdLamp(i))
            BjdLamp(i).Height = BJDlampStd.Height
            BjdLamp(i).Width = BJDlampStd.Width

            BjdLamp(i).Image = BJDlampStd.Image.Clone

            BjdLamp(i).SizeMode = PictureBoxSizeMode.StretchImage

            BjdLamp(i).Top = BjdButton(i).Top + ntop

            BjdLamp(i).Name = "BjdLamp" & i

            BjdLamp(i).Left = BJDlampStd.Left

        Next


        ntop = BJDlabelStd.Top - BJDbuttonStd.Top

        For i = 8 To ct
            BjdLabel(i) = New Label  ' Button 
            BJDgroup2.Controls.Add(BjdLabel(i))

            BjdLabel(i).Height = BJDlabelStd.Height
            ' BjdLabel(i).Width = BJDlabelStd.Width
            Dim ins As Integer
            ins = i + 1

            BjdLabel(i).Text = BJDlabelStd.Text & ins.ToString

            BjdLabel(i).Font = BJDlabelStd.Font


            BjdLabel(i).Top = BjdButton(i).Top + ntop

            BjdLabel(i).Name = "BjdLabel" & i

            BjdLabel(i).Left = BJDlabelStd.Left
            BjdLabel(i).AutoSize = True

        Next

        BJDgroup2.Visible = True

    End Function

    ''' <summary>
    '''   动态往groupbox中添加报警灯
    ''' </summary>
    ''' <param name="groupbox">BJDgroup1,BJDgroup2,BJDgroup3,BJDgroup4</param>
    ''' <param name="indexStart">报警灯开始下标</param>
    ''' <param name="indexEnd">报警灯结束下标</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AddBjdToGroup(ByRef groupbox As GroupBox, ByVal indexStart As Integer, ByVal indexEnd As Integer) As Boolean
        Dim ntop As Integer
        ntop = 30
        '动态生成8个报警灯。
        'ct = 2   ' 编号- 9,10.  下标 8,9 

        Dim i As Integer
        Dim top As Integer


        '动态计算，高度间隔
        If (indexEnd - indexStart) = 7 Then

            'If (groupbox.Height - BJDbuttonStd.Height * 8) >  Then

            'End If

            ntop = (groupbox.Height - BJDbuttonStd.Height * 8) \ 9

        End If
        top = ntop


        For i = indexStart To indexEnd

            BjdButton(i) = New PictureBox ' Button 

            groupbox.Controls.Add(BjdButton(i))

            BjdButton(i).Height = BJDbuttonStd.Height

            BjdButton(i).Width = BJDbuttonStd.Width

            BjdButton(i).Image = BJDbuttonStd.Image.Clone

            BjdButton(i).SizeMode = PictureBoxSizeMode.StretchImage

            BjdButton(i).Top = top

            BjdButton(i).Name = "BjdButton" & i

            top = top + BjdButton(i).Height + ntop

            BjdButton(i).Left = BJDbuttonStd.Left

            '添加click事件
            AddHandler BjdButton(i).Click, AddressOf BjdButton_Click


        Next


        ntop = BJDlampStd.Top - BJDbuttonStd.Top

        For i = indexStart To indexEnd
            BjdLamp(i) = New PictureBox ' Button 
            groupbox.Controls.Add(BjdLamp(i))
            BjdLamp(i).Height = BJDlampStd.Height
            BjdLamp(i).Width = BJDlampStd.Width

            BjdLamp(i).Image = BJDlampStd.Image.Clone

            BjdLamp(i).SizeMode = PictureBoxSizeMode.StretchImage

            BjdLamp(i).Top = BjdButton(i).Top + ntop

            BjdLamp(i).Name = "BjdLamp" & i

            BjdLamp(i).Left = BJDlampStd.Left

        Next


        ntop = BJDlabelStd.Top - BJDbuttonStd.Top

        For i = indexStart To indexEnd
            BjdLabel(i) = New Label  ' Button 
            groupbox.Controls.Add(BjdLabel(i))

            BjdLabel(i).Height = BJDlabelStd.Height
            ' BjdLabel(i).Width = BJDlabelStd.Width
            Dim ins As Integer
            ins = i + 1

            BjdLabel(i).Text = Fesn(i).name

            BjdLabel(i).Font = BJDlabelStd.Font

            BjdLabel(i).Top = BjdButton(i).Top + ntop

            BjdLabel(i).Name = "BjdLabel" & i

            BjdLabel(i).Left = BJDlabelStd.Left
            BjdLabel(i).AutoSize = True

            '添加click事件
            AddHandler BjdLabel(i).DoubleClick, AddressOf BjdLabel_DoubleClick

        Next

        groupbox.Visible = True

    End Function

    ''' <summary>
    '''  释放动态生成的报警灯
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DisposeBJD() As Boolean
        Try
            BJDgroup1.Visible = False
            BJDgroup2.Visible = False
            BJDgroup3.Visible = False
            BJDgroup4.Visible = False

            Dim dbi As Integer
            dbi = BjdCount - 1
            For dbi = 0 To dbi
                BjdButton(dbi).Dispose()
                BjdLabel(dbi).Dispose()
                BjdLamp(dbi).Dispose()
            Next

        Catch ex As Exception
            Return False
        End Try


        Return True
    End Function

    ''' <summary>
    '''  通道图标-双击事件-弹出详细的报警器控制面板
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BjdLabel_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim labjq As Label = CType(sender, Label)
        ' MessageBox.Show(bjdbutton.Name)
        ' "BjdButton" & i   '取到 数组i 即 0,1,2,3,4,5,6,7，
        Dim datas() = (Split(labjq.Name, "el"))
        Dim bjqindex As Integer
        Try
            bjqindex = Val(datas(1))
            'MessageBox.Show(bjqindex)
            ' Main.BjqMoreFunction(bjqindex)
            ' MessageBox.Show(MousePosition.ToString & " x=" & MousePosition.X.ToString & " Y= " & MousePosition.Y.ToString)
            Dim pos As System.Drawing.Point

            pos = MousePosition

            Main.BjqMoreFunction(bjqindex, MousePosition)


        Catch ex As Exception
            Return
        End Try

    End Sub

    ''' <summary>
    ''' 动态控件-报警器按钮-事件
    ''' 根据报警灯当前的状态，按需发送主机打开/关闭报警器
    ''' 1：打开报警器：发送主机打开报警器-记忆音调，音量循环播放
    ''' 2：关闭报警器：发送主机关闭报警器
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BjdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim bjdbutton As PictureBox = CType(sender, PictureBox)
        ' "BjdButton" & i   '取到 数组i 即 0,1,2,3,4,5,6,7，
        Dim datas() = (Split(bjdbutton.Name, "n"))
        Dim bjdindex As Integer
        Try
            bjdindex = Val(datas(1))
        Catch ex As Exception
            Return
        End Try


        '1: 先判断 通讯状态，如果不正常，则提示用户是否继续，代码日后补全
        If Fesn(bjdindex).comm_Fail_Sta Then
            MessageBox.Show("报警器通讯故障，无法操作打开/关闭")
            Return
        End If

        If Fesn(bjdindex).Comm_State <> Tcq.Comm_State_Enum.Comm_OK Then
            MessageBox.Show("报警器通讯故障，无法操作打开/关闭")
            Return
        End If

        '2： 根据当前报警灯的状态，选择发送打开/关闭的通讯指令
        PcSendBjqAddr = Fesn(bjdindex).addr
        PcSendBjqindex = bjdindex


        Dim messtr As String

        If Fesn(bjdindex).alarm Then
            messtr = "是否关闭此路报警"
        Else
            messtr = "是否打开此路报警"
        End If

        If MessageBox.Show(messtr, "确定", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If


        If Sys_Need_Pass Then
            Login_event = 9
            Login_Need_Level = User_Level_Enum.Oper
            Login_Mes = "请输入'操作员'的密码！！！"
            LoginForm1.Show(Me)
        Else
            TurnOnOffBjq()
        End If

     
    End Sub

    Public Sub TurnOnOffBjq()

        If Fesn(PcSendBjqindex).alarm Then
            '关闭报警
            PcSendAlarmOff = CommProgressEnum.PcNeedSend
            CommAlarmOff(PcSendBjqAddr, PcSendToBjq)
            Timer2.Interval = 100
            Timer2.Enabled = True
            SendTimeOverCt = 20 ' 前2秒等待，后2秒显示延时
            PanPro.Visible = True
            Pro1.Maximum = 20
            Pro1.Value = 0
            laPro.Text = "发送中....."
            laPro.Left = (PanPro.Width - laPro.Width) \ 2
            AllGroupboxDisable()
        Else
            '打开报警(记忆音调)
            CommAlarmOnMem(PcSendBjqAddr, PcSendToBjq)
            PcSendAlarmOnMem = CommProgressEnum.PcNeedSend
            Timer2.Interval = 100
            Timer2.Enabled = True  'time2用于查询通讯执行结果和进度显示
            PanPro.Visible = True
            Pro1.Maximum = 20
            Pro1.Value = 0
            laPro.Text = "发送中....."
            laPro.Left = (PanPro.Width - laPro.Width) \ 2
            AllGroupboxDisable()
        End If
    End Sub


    '''' <summary>
    '''' 释放控件
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    For i = 0 To 3
    '        BjdButton(i).Dispose()
    '    Next
    'End Sub


    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim ntop As Integer
        ntop = 30
        For i = 0 To 3
            BjdBut(i) = New Button
            GroupBox2.Controls.Add(BjdBut(i))
            BjdBut(i).Height = 60
            BjdBut(i).Width = 60
            'BjdBut(i).Image = Button7.Image.Clone
            BjdBut(i).Top = ntop
            ntop = ntop + BjdBut(i).Height + 20
            BjdBut(i).Left = 30
            '  AddHandler BjdButton(i).Click, AddressOf BjdButton_Click '添加click事件
        Next
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick

        SendTimeOverCt = SendTimeOverCt + 1

        Pro1.Value = Pro1.Value + 1


        If PcSendAlarmOnMem > 0 Then

            If PcSendAlarmOnMem = CommProgressEnum.ReechoSuc Then
                PcSendAlarmOnMem = CommProgressEnum.free
                laPro.Text = "报警打开成功"
                laPro.ForeColor = Color.Blue
                Pro1.Value = Pro1.Maximum
                Timer2.Enabled = False
                Timer3.Interval = 1000
                Timer3.Enabled = True
                Exit Sub
            End If


            If Pro1.Value >= Pro1.Maximum Then
                PcSendAlarmOnMem = CommProgressEnum.free
                laPro.Text = "报警打开失败"
                laPro.ForeColor = Color.Red
                Pro1.Value = Pro1.Maximum
                Timer2.Enabled = False
                Timer3.Interval = 1000
                Timer3.Enabled = True
                Exit Sub
            End If

            Exit Sub

        End If


        If PcSendAlarmOff > 0 Then
            If PcSendAlarmOff = CommProgressEnum.ReechoSuc Then
                PcSendAlarmOff = CommProgressEnum.free
                laPro.Text = "报警关闭完成"
                laPro.ForeColor = Color.Blue
                Pro1.Value = Pro1.Maximum
                Timer2.Enabled = False
                Timer3.Interval = 1000
                Timer3.Enabled = True
                Exit Sub
            End If

            If Pro1.Value >= Pro1.Maximum Then
                PcSendAlarmOff = CommProgressEnum.free
                laPro.Text = "报警关闭失败"
                laPro.ForeColor = Color.Red
                Pro1.Value = Pro1.Maximum
                Timer2.Enabled = False
                Timer3.Interval = 1000
                Timer3.Enabled = True
                Exit Sub
            End If
            Exit Sub
        End If
    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        AllGroupboxEnable()
        Pro1.Value = 0
        PanPro.Visible = False
        Timer3.Enabled = False
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

        'If BjdCount = 16 Then
        '    MessageBox.Show("当前报警器数量是" & BjdCount & "个,无需更改")
        '    Exit Sub
        'End If

        'If MessageBox.Show("当前报警器数量是" & BjdCount & "个,请确定是否修改数量？", "请确定", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
        '    Exit Sub
        'End If

        'DisposeBJD()
        'BjdCount = 16
        'Sys_node_count = 16
        'AddBJQ(BjdCount)
        'Main.SysReset()
        TextBox1.Text = 16
        HaBjq()
    End Sub



    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        'If BjdCount = 24 Then
        '    MessageBox.Show("当前报警器数量是" & BjdCount & "个,无需更改")
        '    Exit Sub
        'End If

        'If MessageBox.Show("当前报警器数量是" & BjdCount & "个,请确定是否修改？", "请确定", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
        '    Exit Sub
        'End If

        'DisposeBJD()
        'BjdCount = 24
        'Sys_node_count = 24
        'AddBJQ(BjdCount)
        'Main.SysReset()

        TextBox1.Text = 24
        HaBjq()
    End Sub




    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        'If BjdCount = 32 Then
        '    MessageBox.Show("当前报警器数量是" & BjdCount & "个,无需更改")
        '    Exit Sub
        'End If

        'If MessageBox.Show("当前报警器数量是" & BjdCount & "个,请确定是否修改？", "请确定", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
        '    Exit Sub
        'End If
        'DisposeBJD()
        'BjdCount = 32
        'Sys_node_count = 32
        'AddBJQ(BjdCount)
        'Main.SysReset()

        TextBox1.Text = 32
        HaBjq()
    End Sub


    Private Function HaBjq() As Boolean

        Dim ct As Integer
        ct = CInt(TextBox1.Text)

        If ct <= 0 Or ct >= 33 Then
            MessageBox.Show("报警器的有效数量在1-32之间！！！")
            Exit Function
        End If



        If BjdCount = ct Then
            MessageBox.Show("当前报警器数量是" & BjdCount & "个,无需更改")
            Exit Function
        End If

        If MessageBox.Show("当前报警器数量是" & BjdCount & "个,请确定是否修改？", "请确定", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            Exit Function
        End If

        If Sys_Need_Pass Then

            Login_event = 11    '事件1
            Login_Need_Level = User_Level_Enum.Maner
            Login_Mes = "请输入'管理员'的密码！！！"
            LoginForm1.Show(Me)

        Else
            HaBjq1()
        End If


    End Function

    Public Function HaBjq1() As Boolean
        Dim ct As Integer
        ct = CInt(TextBox1.Text)
        DisposeBJD()
        BjdCount = ct
        Sys_node_count = ct
        AddBJQ(BjdCount)
        Main.SysReset()
    End Function





    Private Sub AllGroupboxDisable()
        BJDgroup1.Enabled = False
        BJDgroup2.Enabled = False
        BJDgroup3.Enabled = False
        BJDgroup4.Enabled = False
    End Sub

    ''' <summary>
    ''' 使能所有的报警器 groupbox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AllGroupboxEnable()
        BJDgroup1.Enabled = True
        BJDgroup2.Enabled = True
        BJDgroup3.Enabled = True
        BJDgroup4.Enabled = True
    End Sub

    Private Sub Panel1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel1.Click
        If Main.PanDan.Visible Then
            Main.PanDan.Visible = False
            Main.LaPass.Text = 0
        End If
    End Sub

    Private Sub BJDgroup1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BJDgroup1.Click
        If Main.PanDan.Visible Then
            Main.PanDan.Visible = False
            Main.LaPass.Text = 0
        End If
    End Sub

    Private Sub BJDgroup2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BJDgroup2.Click
        If Main.PanDan.Visible Then
            Main.PanDan.Visible = False
            Main.LaPass.Text = 0
        End If
    End Sub

    Private Sub BJDgroup3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BJDgroup3.Click
        If Main.PanDan.Visible Then
            Main.PanDan.Visible = False
            Main.LaPass.Text = 0
        End If
    End Sub

    Private Sub BJDgroup4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BJDgroup4.Click
        If Main.PanDan.Visible Then
            Main.PanDan.Visible = False
            Main.LaPass.Text = 0
        End If
    End Sub



End Class
