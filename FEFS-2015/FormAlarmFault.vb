Imports System.IO

Public Class FormAlarmFault

    Private Sub FormAlarmFault_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None


        Me.Top = Main.Panel4.Top
        Me.Left = Main.Panel4.Left
        Me.Width = Main.Panel4.Width
        Me.Height = Main.PanXX.Top + Main.PanXX.Height - Main.Panel4.Top



        'Pan1.Width = Me.Width
        'Pan1.Height = Me.Height
        'Pan1.Top = 0
        'Pan1.Left = 0


        ButAlarm.Width = (Me.Width - 30 - 15 - 30) \ 2
        ButFault.Width = ButAlarm.Width

        ButAlarm.Left = 30
        ButFault.Left = ButAlarm.Width + ButAlarm.Left + 15

        ButAlarm.Top = ButAlarm.Left
        ButFault.Top = ButAlarm.Top


        dgridAlarm.Top = ButAlarm.Top + ButAlarm.Height + 20
        dgridFault.Top = dgridAlarm.Top

        Dim hh As Integer

        hh = Me.Height - (ButAlarm.Top + ButAlarm.Height + 20 + 40 + 20)

        dgridAlarm.Height = hh * 0.6
        dgridAlarm.Width = ButAlarm.Width

        dgridFault.Height = dgridAlarm.Height
        dgridFault.Width = dgridAlarm.Width

        dgridAlarm.Left = ButAlarm.Left

        dgridFault.Left = ButFault.Left


        'TreeView1.Width = ButAlarm.Width
        'TreeView2.Width = ButAlarm.Width

        'TreeView1.Left = ButAlarm.Left
        'TreeView2.Left = ButFault.Left

        'TreeView1.Height = Me.Height - dgridAlarm.Height - dgridAlarm.Top - 50
        'TreeView2.Height = TreeView1.Height

        'TreeView1.Top = dgridAlarm.Height + dgridAlarm.Top + 30
        'TreeView2.Top = TreeView1.Top

        'Label1.Top = TreeView1.Top - Label1.Height - 3
        'Label1.Left = TreeView1.Left

        'Label2.Top = Label1.Top
        'Label2.Left = TreeView2.Left

        TreeView1.Width = ButAlarm.Width \ 2
        TreeView1.Left = ButAlarm.Left
        TreeView1.Top = dgridAlarm.Height + dgridAlarm.Top + 30
        RichTextFile.Top = TreeView1.Top

        RichTextFile.Width = (ButFault.Left - ButAlarm.Left + ButAlarm.Width) - TreeView1.Left - TreeView1.Width

        RichTextFile.Left = TreeView1.Left + TreeView1.Width + 25


        TreeView1.Height = Me.Height - dgridAlarm.Height - dgridAlarm.Top - 50
        RichTextFile.Height = TreeView1.Height

        Label1.Top = TreeView1.Top - Label1.Height - 3
        Label1.Left = TreeView1.Left


        '演示向treeview 中添加文件字符串
        Dim d As New System.IO.DirectoryInfo(My.Application.Info.DirectoryPath & "\log\alarm-log\") '这里是你的文件夹路径
        Dim f As System.IO.FileInfo
        For Each f In d.GetFiles

            If CheckIsCurrentFile(f.Name) = False Then
                TreeView1.Nodes.Add(f.Name)
            End If

        Next



        dgridAlarm.Columns(0).Width = dgridAlarm.Width * 0.1
        'dgridAlarm.Columns(0).HeaderText = "
        dgridAlarm.Columns(1).Width = dgridAlarm.Width * 0.15
        dgridAlarm.Columns(1).HeaderText = "编号"
        dgridAlarm.Columns(2).Width = dgridAlarm.Width * 0.3
        dgridAlarm.Columns(2).HeaderText = "发生时间"
        dgridAlarm.Columns(3).Width = dgridAlarm.Width * 0.2
        dgridAlarm.Columns(3).HeaderText = "位置"
        dgridAlarm.Columns(4).Width = dgridAlarm.Width * 0.2
        dgridAlarm.Columns(4).HeaderText = "信息"

        dgridFault.Columns(0).Width = dgridAlarm.Columns(0).Width
        dgridFault.Columns(1).Width = dgridAlarm.Columns(1).Width
        dgridFault.Columns(1).HeaderText = dgridAlarm.Columns(1).HeaderText
        dgridFault.Columns(2).Width = dgridAlarm.Columns(2).Width
        dgridFault.Columns(2).HeaderText = dgridAlarm.Columns(2).HeaderText
        dgridFault.Columns(3).Width = dgridAlarm.Columns(3).Width
        dgridFault.Columns(3).HeaderText = dgridAlarm.Columns(3).HeaderText
        dgridFault.Columns(4).Width = dgridAlarm.Columns(4).Width
        dgridFault.Columns(4).HeaderText = dgridAlarm.Columns(4).HeaderText


        '表格数据初始化


        '定时器用于刷新数据表格的。
        Timer1.Interval = 2000
        Timer1.Enabled = True


        DgridAlarmDataRefresh()
        DgridFaultDataRefresh()


    End Sub


    ''' <summary>
    ''' 刷新显示数据表格-报警表格
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DgridAlarmDataRefresh()
        Dim rgm_i As UShort
        Dim rgm_gz As Alarm_info
        Try
            'Main.Jiankong_Refresh_Lable = False

            rgm_i = dgridAlarm.Rows.Count

            While (rgm_i > 0)
                dgridAlarm.Rows.RemoveAt(rgm_i - 1)
                rgm_i = dgridAlarm.Rows.Count
            End While

            If Baojing_Array.Count < 1 Then
                Exit Sub
            End If

            If Baojing_Array.Count >= 1 Then
                For rgm_i = 0 To Baojing_Array.Count - 1
                    rgm_gz = Baojing_Array.Item(Baojing_Array.Count - rgm_i - 1)
                    dgridAlarm.SelectAll()
                    dgridAlarm.ClearSelection()
                    dgridAlarm.Rows.Add()
                    dgridAlarm.Rows(rgm_i).Cells(0).Value = rgm_i + 1
                    dgridAlarm.Rows(rgm_i).Cells(1).Value = rgm_gz.tcq_id_str
                    dgridAlarm.Rows(rgm_i).Cells(2).Value = rgm_gz.time_str
                    dgridAlarm.Rows(rgm_i).Cells(3).Value = rgm_gz.tcq_name
                    dgridAlarm.Rows(rgm_i).Cells(4).Value = rgm_gz.date_str
                Next
            End If

        Catch ex As Exception
        End Try

    End Sub


    ''' <summary>
    ''' 刷新显示数据表格-故障表格
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DgridFaultDataRefresh()
        Dim rgm_i As UShort
        Dim rgm_gz As Guzhang_info

        Dim rgm_i1 As UShort
        Try

            rgm_i = dgridFault.Rows.Count

            '首先情况列表框。
            While (rgm_i > 0)
                dgridFault.Rows.RemoveAt(rgm_i - 1)
                rgm_i = dgridFault.Rows.Count
            End While



            '如果没有故障，则直接退出。
            If Guzhang_Array.Count < 1 Then
                Exit Sub
            End If

            '如果有故障信息。则按照时间发送的倒序顺序。
            If Guzhang_Array.Count >= 1 Then
                For rgm_i = 0 To Guzhang_Array.Count - 1
                    rgm_i1 = Guzhang_Array.Count - rgm_i - 1
                    rgm_gz = Guzhang_Array.Item(rgm_i1)
                    dgridFault.SelectAll()
                    dgridFault.ClearSelection()
                    dgridFault.Rows.Add()
                    dgridFault.Rows(rgm_i).Cells(0).Value = rgm_i + 1
                    dgridFault.Rows(rgm_i).Cells(1).Value = rgm_gz.tcq_id_str
                    dgridFault.Rows(rgm_i).Cells(2).Value = rgm_gz.time_str
                    dgridFault.Rows(rgm_i).Cells(3).Value = rgm_gz.tcq_name
                    dgridFault.Rows(rgm_i).Cells(4).Value = rgm_gz.date_str
                Next
            End If
        Catch ex As Exception
        End Try

    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Main.Jiankong_Refresh_Lable Then
            Main.Jiankong_Refresh_Lable = False
            ButAlarm.Text = Main.Button9.Text
            ButFault.Text = Main.Button10.Text
            DgridAlarmDataRefresh()
        End If

        If Main.Guzhang_Refresh_Lable Then
            Main.Guzhang_Refresh_Lable = False
            ButAlarm.Text = Main.Button9.Text
            ButFault.Text = Main.Button10.Text
            DgridFaultDataRefresh()
        End If


    End Sub



    Private Sub TreeView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeView1.DoubleClick
        ' MessageBox.Show("double click ")

        Try

            If TreeView1.SelectedNode.Text = "" Then
                MessageBox.Show("kong")
                Exit Sub
            End If

            Dim filename As String
            Dim filepath As String
            ' Dim filereader As StreamReader

            filename = TreeView1.SelectedNode.Text

            '报警日志文件存在安装目录下 log\alarm-log\时间.txt

            filepath = My.Application.Info.DirectoryPath & "\log\alarm-log\" & filename







            If File.Exists(filepath) = True Then
                RichTextFile.Text = ""
                RichTextFile.Text = File.ReadAllText(filepath)  '创建写入 文件对象 utf-8 编码
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

End Class