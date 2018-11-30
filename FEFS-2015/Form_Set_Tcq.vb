Imports System.IO

Public Class Form_Set_Tcq


    'Dim tcq_id_tar As UShort
    'Dim tcq_name As String
    'Dim tcq_IL As UShort
    'Dim tcq_T1 As UShort
    'Dim tcq_T2 As UShort
    'Dim tcq_kind As Byte


    Private Sub Form_Set1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.Top = Main.Panel4.Top
        Me.Left = Main.Panel4.Left
        Me.Width = Main.Panel4.Width
        Me.Height = Main.Panel4.Height

        If Me.Width * 0.75 > Panel1.Width Then
            Panel1.Width = Me.Width * 0.75
        End If

        If Me.Height * 0.75 > Panel1.Height Then
            Panel1.Height = Me.Height * 0.75
        End If

        Panel1.Top = (Me.Height - Panel1.Height) \ 2
        Panel1.Left = (Me.Width - Panel1.Width) \ 2




        DataGridView1.Font = New Font("宋体", 11)

        With DataGridView1
            .Columns(0).Width = .Width * 0.1
            .Columns(1).Width = .Width * 0.2
            .Columns(2).Width = .Width * 0.3
            .Columns(3).Width = .Width * 0.3
            ' .Columns(4).Width = .Width * 0.15
        End With

        refresh_table()


        Label2.Text = "1：一般情况下报警器<编号>和<地址>是相同，一一对应，无需修改设置。"
        Label3.Text = "2：设置<位置>时，字数不应超过个10个字符，否则将有可能无法完全显示。"
        Label4.Text = "3：<屏蔽>设置为1时，系统将不对此报警器监控，适用于报警器在维护维修期。"
        Label5.Text = "4：鼠标点击要修改的数据，单元格变绿后，直接输入新的数据。数据全部修改好后，单击<保存>按钮。"
    End Sub


    ''' <summary>
    ''' 刷新表格
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub refresh_table()

        DataGridView1.Rows.Clear()

        Dim i_temp As UShort
        i_temp = 0
        For Each tcqf As Tcq In Fesn

            DataGridView1.Rows.Add()
            With DataGridView1.Rows(i_temp)
                .Cells(0).Value = tcqf.id_str
                .Cells(1).Value = tcqf.addr
                .Cells(2).Value = tcqf.name
                .Cells(3).Value = tcqf.enable
                ' .Cells(4).Value = tcqf.net
                .Height = 30

                .Cells(0).ReadOnly = True
                .Cells(1).ReadOnly = False
                .Cells(2).ReadOnly = False
                .Cells(3).ReadOnly = False
                ' .Cells(4).ReadOnly = False

            End With
            i_temp = i_temp + 1

            'If i_temp >= Sys_node_count Then
            '    Exit Sub
            'End If

        Next
    End Sub



    ''' <summary>
    ''' 刷新表格
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        refresh_table()
    End Sub


    ''' <summary>
    ''' 保存按钮。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If MessageBox.Show("是否确定保存数据？", "确定", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If


        If AssertData() Then

            If CheckDataIsChange() Then

                '提取数据保存到文本文件。

                If SaveTcqInfoIntoFile() Then
                    MessageBox.Show("数据保存成功！")
                Else
                    MessageBox.Show("数据保存失败！")
                End If

            End If

        End If


    End Sub


    Private Function SaveTcqInfoIntoFile() As Boolean

        Dim data_file_path As String
        Dim df_sw As StreamWriter

        Try
            '系统数据备份文件sys_main_info，存放位置在 安装路径+ \backup_data 文件夹内。
            data_file_path = My.Application.Info.DirectoryPath & "\backup_data\tcq_info"
            If File.Exists(data_file_path) <> True Then
                File.Delete(data_file_path)
            End If

            df_sw = File.CreateText(data_file_path)    '创建写入 文件对象 utf-8 编码


            Dim i As UShort
            Dim tcq_info_str As String

            For i = 0 To DataGridView1.RowCount - 1
                '                 编号@%  地址 @%     位置   @%   预留1 @%  预留2
                '插入字符串效果如：1  @%    1  @%   1#区域   @%     0   @%    0
                tcq_info_str = ""
                With DataGridView1.Rows(i)
                    tcq_info_str = .Cells(0).Value.ToString & "@%"
                    tcq_info_str = tcq_info_str & .Cells(1).Value.ToString & "@%"
                    tcq_info_str = tcq_info_str & .Cells(2).Value.ToString & "@%"
                    tcq_info_str = tcq_info_str & .Cells(3).Value.ToString & "@%"
                    tcq_info_str = tcq_info_str & "0@%"
                End With
                df_sw.WriteLine(tcq_info_str)
            Next
            df_sw.Flush()
            df_sw.Close()


            '同步更新数据。

            For i = 0 To DataGridView1.RowCount - 1
                With DataGridView1.Rows(i)
                    Fesn(i).addr = Val(.Cells(1).Value)
                    Fesn(i).name = .Cells(2).Value.ToString
                    Fesn(i).enable = Val(.Cells(3).Value)
                End With
            Next

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        SaveTcqInfoIntoFile = False
    End Function




    ''' <summary>
    ''' 检查用户是否修改了数据
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckDataIsChange() As Boolean
        Dim i As Integer

        For i = 0 To Sys_node_count - 1
            If Fesn(i).addr <> Val(DataGridView1.Rows(i).Cells(1).Value) Then
                Return True
            End If

            If Fesn(i).name <> DataGridView1.Rows(i).Cells(1).Value.ToString Then
                Return True
            End If

            If Fesn(i).enable <> Val(DataGridView1.Rows(i).Cells(2).Value) Then
                Return True
            End If
        Next

        CheckDataIsChange = False

    End Function


    ''' <summary>
    ''' 检查数据是否合规。
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AssertData() As Boolean
        Dim i As Integer

        For i = 0 To Sys_node_count - 1

            If IsNumeric(DataGridView1.Rows(i).Cells(1).Value) Then

                If Val(DataGridView1.Rows(i).Cells(1).Value) > BJQMAXADDR Then
                    MessageBox.Show("<地址>值-不能超过最大值:" & BJQMAXADDR)

                    DataGridView1.Rows(i).Cells(1).Selected = True
                    Return False
                End If

                If Val(DataGridView1.Rows(i).Cells(1).Value) < 1 Then
                    MessageBox.Show("<地址>值-不能小于1")
                    DataGridView1.Rows(i).Cells(1).Selected = True
                    Return False
                End If

            Else
                DataGridView1.Rows(i).Cells(3).Selected = True
                MessageBox.Show("<地址>值-请设置为整数！")
                DataGridView1.Rows(i).Cells(1).Selected = True
                Return False
            End If



            Dim aname As String
            aname = ""
            aname = Trim(DataGridView1.Rows(i).Cells(2).Value)


            If Len(aname) < 3 Then
                DataGridView1.Rows(i).Cells(2).Selected = True
                MessageBox.Show("<位置>值不至少为3个字符！")
                Return False
            End If


            If DataGridView1.Rows(i).Cells(2).Value.ToString.Length > 15 Then
                DataGridView1.Rows(i).Cells(2).Selected = True
                MessageBox.Show("<位置>值超长，不能超过15个字符！")
                Return False
            End If


            If IsNumeric(DataGridView1.Rows(i).Cells(3).Value) Then

                If Val(DataGridView1.Rows(i).Cells(3).Value) >= 1 Then
                    DataGridView1.Rows(i).Cells(3).Value = 1
                Else
                    DataGridView1.Rows(i).Cells(3).Value = 0
                End If
            Else
                DataGridView1.Rows(i).Cells(3).Selected = True
                MessageBox.Show("<屏蔽>值-请设置1或0！")
                Return False
            End If

        Next
        AssertData = True
    End Function


End Class