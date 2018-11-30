Public Class LoginForm1

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click

        Dim text_name As String
        Dim text_password As String

        text_name = Trim(ComboBox1.Text)
        text_password = Trim(PasswordTextBox.Text)

        If text_name = "" Then
            MsgBox("用户名不能为空！")
            Exit Sub
        End If

        If text_password = "" Then
            MsgBox("密码不能为空！")
            Exit Sub
        End If

        '判断是否为超级用户
        'If (text_name = "管理员") And (text_password = "63206455") Then
        '    Sys_user_name = "fzdq"
        '    Sys_user_level = 4
        '    Main.Enabled = True
        '    Sys_Lock = False
        '    Login_Work()
        '    Me.Close()
        '    Exit Sub
        'End If

        Dim pass_ck As Boolean

        If text_name = "管理员" Then
            If text_password = Sys_Maner_Pass Then
                pass_ck = True
                Sys_user_level = User_Level_Enum.Maner

            ElseIf text_password = SUPER_PASSWORD_GLY Then
                pass_ck = True
                Sys_user_level = User_Level_Enum.ManerSuper
            ElseIf text_password = SUPER_PASSWORD_ENGER Then
                pass_ck = True
                Sys_user_level = User_Level_Enum.Enger
            Else
                pass_ck = False
                Sys_user_level = User_Level_Enum.Common
            End If
        Else
            If text_password = Sys_Oper_Pass Then
                pass_ck = True
                Sys_user_level = User_Level_Enum.Oper
            Else
                pass_ck = False
            End If
        End If


        If pass_ck = False Then
            Label1.Text = "密码错误！！！"
            Label1.Visible = True
            Exit Sub
        End If


        If Login_Need_Level = User_Level_Enum.Maner And text_name <> "管理员" Then
            Label1.Text = "请用更高一级得'管理员'身份账号登录！！！"
            ComboBox1.SelectedIndex = 1
            Exit Sub
        End If

        If text_name = "管理员" Then
            Main.Enabled = True
            Sys_user_name = text_name
            Sys_Lock = False
            Login_Work()
            Me.Close()
        End If

        If text_name = "操作员" Then
            Main.Enabled = True
            Sys_user_name = text_name
            Sys_Lock = False
            Login_Work()
            Me.Close()
        End If



    End Sub




    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click

        If Login_event = 8 Then
            Reset_Form_Show = False
        End If

        Main.Enabled = True
        Me.Close()
    End Sub

    Private Sub LoginForm1_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Main.Enabled = True
        Main.BringToFront()
        If Login_event = 8 Then
            Reset_Form_Show = False
        End If

    End Sub

    Private Sub LoginForm1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label2.Text = Login_Mes
        Label2.Visible = True

        If Login_Need_Level = User_Level_Enum.Oper Then
            ComboBox1.SelectedIndex = 0
        Else
            ComboBox1.SelectedIndex = 1
        End If

        Label1.Visible = False

        ' Me.Top = 850
        ' Me.Left = 350

        Me.Top = (Main.Height - Me.Height) \ 2
        Me.Left = (Main.Width - Me.Width) \ 2

        Main.Enabled = False

        ''用户锁定
        If Sys_Lock Then
            Cancel.Enabled = False
            PasswordTextBox.Text = ""
        End If

        Cancel.Focus()

    End Sub

    Public Function Check_User() As Boolean
        Dim sql_str As String
        sql_str = " select count(*) from user_info"
        If Find_Sql_Exe(sql_str) = 1 Then
            Return True
        End If
        Return False

    End Function

 
    Private Sub UsernameLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UsernameLabel.Click
        'Sys_user_name = "admin"
        'Sys_user_level = 4
        'Main.Enabled = True
        'Sys_Lock = False
        'Me.Close()
    End Sub

    '用户输入正确密码后的事件响应。。。。。。
    ''' <summary>
    ''' Login_event 数值定义
    ''' 1：进入参数设置
    ''' 2：参数设置窗体里面用的
    ''' 3：自检用-MALS不用
    ''' 4：系统复位
    ''' 5：单机调试
    ''' 6：退出系统
    ''' 7：报警全开
    ''' 9: 图形显示窗体-打开或关闭报警。
    ''' 10：其它通讯指令发送权限
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Login_Work()
        If Login_event = 1 Then
            Main.But_Pra_Set_Click_2()
        ElseIf Login_event = 2 Then
            Admin.Button8_Click1()
        ElseIf Login_event = 3 Then
            Main.But_Sys_SelfCheck_Click1()
        ElseIf Login_event = 4 Then
            Main.But_Sys_Reset_Click1()

        ElseIf Login_event = 5 Then
            Main.But_Sys_SelfCheck_Click1()

        ElseIf Login_event = 6 Then
            Main.Button4_Click1()
        ElseIf Login_event = 7 Then
            Main.Button21_Click1()

        ElseIf Login_event = 8 Then
            Main.Button20_Click1()
        ElseIf Login_event = 9 Then
            Form_Bjd.TurnOnOffBjq()
        ElseIf Login_event = 10 Then
            Main.LaPass.Text = "1"

        ElseIf Login_event = 11 Then
            Form_Bjd.HaBjq1()
        End If


    End Sub





End Class
