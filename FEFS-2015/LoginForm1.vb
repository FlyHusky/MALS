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
        Dim temp_user As FEFS_USER

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
        If (text_name = "管理员") And (text_password = "63206455") Then
            Sys_user_name = "fzdq"
            Sys_user_level = 4
            Main.Enabled = True
            Sys_Lock = False
            Login_Work()
            Me.Close()
            Exit Sub
        End If



        temp_user = New FEFS_USER
        temp_user.user_name = text_name
        temp_user.user_password = text_password
        temp_user.user_level = ""

        Dim lfi As Integer
        Dim name_ck As Boolean
        Dim pass_ck As Boolean

        name_ck = False
        pass_ck = False

        '遍历用户对象
        For lfi = 0 To Fefs_Users.Length - 1
            If temp_user.user_name = Fefs_Users(lfi).user_name Then  '判断用户名
                name_ck = True
                If temp_user.user_password = Fefs_Users(lfi).user_password Then
                    temp_user.user_level = Fefs_Users(lfi).user_level
                    pass_ck = True
                    Exit For
                End If
            End If
        Next


        If name_ck = False Then
            Label1.Text = "用户名错误！！！"
            Label1.Visible = True
            Exit Sub
        End If

        If pass_ck = False Then
            Label1.Text = "密码错误！！！"
            Label1.Visible = True
            Exit Sub
        End If


        '管理员的  Sys_user_level = 2
        '操作员的  Sys_user_level =1
        '超级用户的  Sys_user_level = 3

        '判断用户权限是否够，管理员的 Sys_user_level=2

        If temp_user.user_level = "管理员" Then
            Main.Enabled = True
            Sys_user_level = 2
            Sys_user_name = text_name
            Sys_Lock = False
            Login_Work()
            Me.Close()
        Else  '登录的密码为 操作员 权限

            '但是需要的权限为管理员，
            If Login_Need_Level = "管理员" Then
                Label1.Text = "请用更高一级得'管理员'身份账号登录！！！"
            Else
                Main.Enabled = True
                Sys_user_level = 1
                Sys_user_name = text_name
                Sys_Lock = False
                Login_Work()
                Me.Close()
            End If
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

        Dim lfi As Integer
        For lfi = 0 To Fefs_Users.Length - 1
            ComboBox1.Items.Add(Fefs_Users(lfi).user_name)
            If Login_Need_Level = Fefs_Users(lfi).user_level Then
                ComboBox1.SelectedIndex = lfi
            End If
        Next

        Label1.Visible = False
        Me.Top = 850
        Me.Left = 350

        ' Me.Top = (Main.Height - Me.Height) \ 2
        ' Me.Left = (Main.Width - Me.Width) \ 2

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
            Main.But_Sys_Info_Click1()

        ElseIf Login_event = 6 Then
            Main.Close_PC()

            'form1 界面复位单个探测器。
        ElseIf Login_event = 7 Then
            Form1.Button3_Click()

        ElseIf Login_event = 8 Then
            Main.Timer2.Enabled = False
            Sys_Close()
            Application.Restart()
        End If


    End Sub





End Class
