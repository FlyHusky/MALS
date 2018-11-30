Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.IO
Imports System.Threading

Public Class Welcom_Form

    Private mysql_stats As Boolean
    Private sthread As Thread


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        '---------------------vb 代码程序测试区域--------------------
        'Dim i_pages As Integer
        'Dim page_node_ct As Integer
        'Dim node_tatal As Integer
        'page_node_ct = 32
        'iii = page_node_ct * 2

        'node_tatal = 30
        'i_pages = node_tatal \ iii

        'If node_tatal Mod iii > 0 Then
        '    i_pages = i_pages + 1
        'End If
      

        'Dim send_ar() As Byte            '主机查询指令
        ''Dim asci As UShort


        'ReDim send_ar(30)
        'Dim date_time(4) As String

        'Dim ttc(8) As Char
        'Dim uuc(20) As Char

        'Dim tstr As String
        'tstr = Format(Now(), "yy/MM/dd/HH/mm")
        'ttc = tstr.ToCharArray

        'Dim i As Byte
        'Dim k As Byte

        'k = 0
        'For i = 0 To ttc.Length - 1
        '    If ttc(i) <> "/" Then
        '        uuc(k) = ttc(i)
        '        k = k + 1
        '    End If

        'Next


        '' addr  
        '' @FIR001 001 001 1 17 0602 1627 006156#
        'send_ar(0) = Asc("@")
        'send_ar(1) = Asc("F")
        'send_ar(2) = Asc("I")
        'send_ar(3) = Asc("R")

        'send_ar(4) = Asc("0")
        'send_ar(5) = Asc("0")
        'send_ar(6) = Asc("1")

        'asci = 123 \ 100
        'send_ar(7) = Asc(asci.ToString)

        'asci = 123 Mod 100
        'asci = asci \ 10
        'send_ar(8) = Asc(asci.ToString)

        'asci = 123 Mod 10
        'send_ar(9) = Asc(asci.ToString)  '这里要设置为探测器地址。



        '-------------------------------------------------------------

        Me.WindowState = FormWindowState.Maximized
        ' Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None

        Dim my_top As Integer  '屏幕上方留白高度
        my_top = 50

        mysql_stats = False

        Scr_H = Me.Height
        Scr_W = Me.Width

        Panel1.Width = Me.Width - 100
        Panel1.Height = Me.Height - 150
        Panel1.Location = New Point(50, my_top)

        Panel1.BackgroundImageLayout = ImageLayout.Stretch
        ProgressBar1.Location = New Point(50, Panel1.Height + my_top)
        ProgressBar1.Width = Panel1.Width
        ProgressBar1.Height = 30


        '----------------数据初始化，从文件或数据库加载---------------
        '  Sys_Init()

        sthread = New Thread(AddressOf Sys_Init)
        sthread.Start()

        '-------------------------------------------------------------


        Lbl_Sys_int.Location = New Point(50, Panel1.Height + ProgressBar1.Height + my_top)
        Lbl_Sys_int.Text = "系统初始化准备中。。。"

        'Label1.Text = Sys_name
        Label1.Text = "报警器监控系统"

        Dim center As Integer
        center = CInt((Panel1.Width - Label1.Width) / 2)
        Label1.Left = center
        Label1.Top = CInt(Panel1.Height * 0.3)

        center = (Panel1.Width - Label2.Width) \ 2
        Label2.Left = center
        Label2.Top = CInt(Panel1.Height * 0.5)

        Timer1.Interval = 100
        Timer1.Enabled = True
        ProgressBar1.Maximum = 20
    End Sub



    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        ProgressBar1.Value = ProgressBar1.Value + 1

        If ProgressBar1.Value >= 20 Then
            Me.Visible = False
            Timer1.Enabled = False
            Sys_user_level = 0 '系统最低权限1。。。。2=系统操作员   3=系统管理员
            Main.Visible = True
            Timer1.Enabled = False
        ElseIf ProgressBar1.Value = 10 Then
            Main.Visible = False
        End If

    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click
        Timer1.Interval = 30
    End Sub

  
End Class
