'Imports MapXLib.ColorConstants
'Imports MapXLib.CursorConstants
'Imports MapXLib.ToolConstants
'Imports MapXLib

''' <summary>
''' 程序编写笔记：
''' 1：分解出来探测器节点的图层，离散出探测器节点集合。
''' 2：探测器符号颜色定义如下
'''     1：绿色长亮---正常。
'''     2：黄色长亮---通讯异常，黄色闪烁--其他故障。
'''     3：红色闪烁---监控报警。
''' </summary>
''' <remarks></remarks>

Public Class Form_Map_View



    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Panel5.Visible = False
    End Sub

    '全屏按钮
    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Button13.Visible = False
        Button14.Visible = True
        Main.But_dh.Visible = False
        Me.Top = 0
        Me.Left = 0
        Me.WindowState = FormWindowState.Maximized
        Panel2.Left = 4
        Main.Panel1.Visible = False

        Main.Panel4.Visible = False
        Main.Panel6.Visible = False
        Main.Panel5.Visible = False
        Panel2.Width = Me.Width - 10
        Panel2.Height = Me.Height - 60
   
    End Sub


    '局部显示按钮
    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Button13.Visible = True
        Button14.Visible = False

        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Normal

        Me.Top = Form1.Top
        Me.Left = Form1.Left
        Me.Width = Form1.Width
        Me.Height = Form1.Height

        Main.But_dh.Visible = True
        Panel2.Width = Me.Width - 4
        Panel2.Left = 2

        Panel2.Top = GroupBox1.Height + 4
        Panel2.Height = Me.Height - Panel2.Top

        'Tcq_Map.Width = Panel2.Width - 4
        'Tcq_Map.Left = 2
        'Tcq_Map.Height = Panel2.Height - 4
        'Tcq_Map.Top = 2

        Main.Panel1.Visible = True
        Main.Panel6.Visible = True
        Main.Panel5.Visible = True

    End Sub


End Class