Public Class Form_Alarm

    Private Sub Form_Alarm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.Top = Main.Panel4.Top
        Me.Left = Main.Panel4.Left
        Me.Width = Main.Panel4.Width
        Me.Height = Main.Panel4.Height
        Label1.Top = 10
        Label1.Left = (Me.Width - Label1.Width) / 2
        GroupBox1.Left = (Me.Width - GroupBox1.Width) / 2
        GroupBox1.Top = Label1.Top + Label1.Height + 10

        DGV1.Top = GroupBox1.Top + GroupBox1.Height + 10
        DGV1.Left = (Me.Width - DGV1.Width) / 2
        DGV1.Height = Me.Height - DGV1.Top - 15

        ' DGV1.Width = 734

        Panel1.Top = GroupBox1.Top + GroupBox1.Height + 60
        Panel1.Left = (Me.Width - Panel1.Width) / 2

        Timer1.Interval = 5000
        Timer1.Enabled = True
    End Sub


    ''' <summary>
    ''' 刷新数据表
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Refresh_Table()
        Dim rgm_i As UShort
        Dim rgm_gz As Alarm_info
        Try
            'Jiankong_Refresh_Lable = False

            rgm_i = DGV1.Rows.Count

            While (rgm_i > 0)   '循环删除所有的行记录
                DGV1.Rows.RemoveAt(rgm_i - 1)
                rgm_i = DGV1.Rows.Count
            End While

            If Baojing_Array.Count >= 1 Then
                For rgm_i = 0 To Baojing_Array.Count - 1
                    rgm_gz = Baojing_Array.Item(rgm_i)
                    DGV1.SelectAll()
                    DGV1.ClearSelection()
                    DGV1.Rows.Add()
                    DGV1.Rows(rgm_i).Cells(0).Value = rgm_i + 1
                    DGV1.Rows(rgm_i).Cells(1).Value = rgm_gz.time_str
                    DGV1.Rows(rgm_i).Cells(2).Value = rgm_gz.tcq_name
                    DGV1.Rows(rgm_i).Cells(3).Value = rgm_gz.tcq_id_str

                    If rgm_gz.Alarm_kind = Alarm_info.alarm_enum.IL_alarm Then
                        DGV1.Rows(rgm_i).Cells(4).Value = "电流报警"
                    ElseIf rgm_gz.Alarm_kind = Alarm_info.alarm_enum.T1_alarm Then
                        DGV1.Rows(rgm_i).Cells(4).Value = "温度1报警"
                    Else
                        DGV1.Rows(rgm_i).Cells(4).Value = "温度2报警"
                    End If

                    '把相关值显示出来，这里应该显示报警时刻发生时的值。
                    DGV1.Rows(rgm_i).Cells(5).Value = rgm_gz.date_str
                    '这里显示此条报警信息是否被用户确认。
                    If rgm_gz.alarm_sure Then
                        DGV1.Rows(rgm_i).Cells(6).Value = "已确认"
                        DGV1.Rows(rgm_i).Cells(6).Style.ForeColor = Color.Green
                    Else
                        DGV1.Rows(rgm_i).Cells(6).Value = "未确认"
                        DGV1.Rows(rgm_i).Cells(6).Style.ForeColor = Color.Red
                    End If



                Next
                DGV1.FirstDisplayedScrollingRowIndex = rgm_i - 1
            End If
        Catch ex As Exception
        End Try
    End Sub


    ''' <summary>
    ''' 如果此界面处于当前显示的界面的，那么每隔5秒钟，则自动刷新是否有报警信息需要确认。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

    End Sub
End Class