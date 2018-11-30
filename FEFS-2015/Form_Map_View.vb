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

    'Dim Lyr_pmt As MapXLib.Layer                    '电气平面图图层
    'Dim 'Lyr_tcq As MapXLib.Layer                   '探测器所在图层
    'Dim tcq_'feas As MapXLib.'features
    'Dim 'fe As 'feature

    'Private Sub Form_Map_View_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


    '    Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
    '    Me.Top = Form1.Top
    '    Me.Left = Form1.Left
    '    Me.Width = Form1.Width
    '    Me.Height = Form1.Height

    '    GroupBox1.Width = Me.Width
    '    GroupBox1.Left = 0
    '    GroupBox1.Top = 2


    '    Panel2.Width = Me.Width - 4
    '    Panel2.Left = 2
    '    Panel2.Top = GroupBox1.Height + 4
    '    Panel2.Height = Me.Height - Panel2.Top
    '    'Tcq_Map.Width = Panel2.Width - 4
    '    'Tcq_Map.Left = 2
    '    'Tcq_Map.Height = Panel2.Height - 4
    '    'Tcq_Map.Top = 2

    '    ''Tcq_Map 属性值设置。。。。
    '    'Tcq_Map.MousewheelSupport = MapXLib.MousewheelSupportConstants.miFullMousewheelSupport

    '    Panel5.Visible = False
    '    Panel1.Visible = False

    '    'Tcq_Map.Zoom = 80
    '    'Tcq_Map.CenterX = 0.476
    '    'Tcq_Map.CenterY = 0.337


    '    '根据  'Tcq_Map_array  初始化地图信息


    '    Dim tmai As Byte

    '    For tmai = 1 To 'Tcq_Map_array.Count
    '        Dim 'Tcq_Map_cl1 As 'Tcq_Map_cl
    '        'Tcq_Map_cl1 = 'Tcq_Map_array.Item(tmai - 1)
    '        Com_Maps.Items.Add('Tcq_Map_cl1.name)
    '    Next


    '    If Com_Maps.Items.Count >= 1 Then
    '        Com_Maps.SelectedIndex = 0
    '    End If

    '    Button14.Visible = False
    '    Button13.Visible = True
    'End Sub


    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        'Tcq_Map.CurrentTool = MapXLib.ToolConstants.miZoomInTool
        'Tcq_Map.MousePointer = MapXLib.CursorConstants.miZoomInCursor


    End Sub


    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        'Tcq_Map.CurrentTool = MapXLib.ToolConstants.miZoomOutTool
        'Tcq_Map.MousePointer = MapXLib.CursorConstants.miZoomOutCursor
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Tcq_Map.MousePointer = MapXLib.CursorConstants.miPanCursor
        'Tcq_Map.CurrentTool = MapXLib.ToolConstants.miPanTool
    End Sub


    'Private Sub 'Tcq_Map_DblClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles 'Tcq_Map.DblClick
    '    If 'Tcq_Map.CurrentTool = miSelectTool Then
    '        Dim ses As Selection   '  选择项集合，
    '        ses = 'Lyr_tcq.Selection
    '        If ses.Count <> 1 Then       '如果选择的元素
    '            ses.ClearSelection()
    '            Exit Sub
    '        End If

    '        'fe = ses.Item(1)
    '        Panel5.Visible = True
    '        Label_'fe_id.Text = 'fe.'featureID
    '        Label_'fe_key.Text = 'fe.'featureKey
    '        TextBox1.Text = 'fe.KeyValue
    '        TextBox2.Text = 'fe.Style.SymbolType
    '    End If
    'End Sub



    'Private Sub 'Tcq_Map_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 'Tcq_Map.SelectionChanged
    '    If 'Tcq_Map.CurrentTool = miSelectTool Then
    '        Dim ses As Selection   '  选择项集合，
    '        ses = 'Lyr_tcq.Selection


    '        If ses.Count <> 1 Then       '如果选择的元素
    '            ses.ClearSelection()
    '            Exit Sub
    '        End If
    '        Dim 'fe As 'feature
    '        'fe = ses.Item(1)
    '        MsgBox('fe.Name)
    '    End If
    'End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        'Tcq_Map.MousePointer = MapXLib.CursorConstants.miSelectCursor
        'Tcq_Map.CurrentTool = MapXLib.ToolConstants.miSelectTool
    End Sub


    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Panel5.Visible = False
    End Sub


    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        'fe.KeyValue = TextBox1.Text
        'fe.Update()
    End Sub


    ''' <summary>
    ''' 放置图标按钮。。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click

        'Tcq_Map.MousePointer = 18

        'Lyr_tcq.Editable = True

        'Tcq_Map.Layers.InsertionLayer = 'Lyr_tcq

        'Tcq_Map.CurrentTool = miArrowTool

    End Sub

    'Private Sub 'Tcq_Map_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles 'Tcq_Map.MouseClick
    '    If 'Tcq_Map.CurrentTool = miSelectTool Then
    '        MsgBox(1)
    '        Dim ses As Selection   '  选择项集合，
    '        ses = 'Lyr_tcq.Selection
    '        If ses.Count <> 1 Then       '如果选择的元素
    '            ses.ClearSelection()
    '            Exit Sub
    '        End If

    '        'fe = ses.Item(1)

    '        Dim t_id As UShort
    '        t_id = Val('fe.KeyValue)

    '        If t_id >= Sys_node_count Then
    '            Exit Sub
    '        End If
    '        Panel1.Visible = True

    '        Panel1.Left = Cursor.Position.X
    '        Panel1.Top = Cursor.Position.Y
    '    End If
    'End Sub

    '    Private Sub Tcq_Map_ClickEvent(ByVal sender As Object, ByVal e As System.EventArgs)
    '        Panel1.Visible = False
    '        If 'Tcq_Map.CurrentTool = miSelectTool Then

    '            Dim ses As Selection   '  选择项集合，
    '            ses = 'Lyr_tcq.Selection
    '            If ses.Count <> 1 Then       '如果选择的元素
    '                ses.ClearSelection()
    '                Exit Sub
    '            End If
    '            'fe = ses.Item(1)
    '2276:

    '            Dim t_id As UShort
    '            t_id = Val('fe.KeyValue)
    '            If t_id > Sys_node_count Then
    '                Exit Sub
    '            End If

    '            If t_id < 1 Then
    '                Exit Sub
    '            End If


    '            Panel1.Visible = True

    '            t_id = t_id - 1

    '            La_box_name.Text = 'fesn(t_id).name
    '            La_id_str.Text = 'fesn(t_id).id_str

    '            Panel1.Left = Cursor.Position.X - Me.Left
    '            Panel1.Top = Cursor.Position.Y - Me.Top - 50

    '            If 'fesn(t_id).Comm_State = Tcq.Comm_State_Enum.Comm_Fail Then
    '                La_com.Text = "异常"
    '                La_com.ForeColor = Color.Red

    '                La_IL.Text = "---"
    '                La_t1.Text = "---"
    '                La_T2.Text = "---"
    '                Exit Sub
    '            ElseIf 'fesn(t_id).Comm_State = Tcq.Comm_State_Enum.Comm_Wait Then
    '                La_com.Text = "等待"
    '                La_IL.Text = "---"
    '                La_t1.Text = "---"
    '                La_T2.Text = "---"
    '                La_com.ForeColor = Color.White
    '                Exit Sub
    '            Else
    '                La_com.Text = "正常"
    '                La_com.ForeColor = Color.Blue
    '            End If

    '            La_IL.Text = 'fesn(t_id).IL.ToString & "mA"
    '            La_t1.Text = 'fesn(t_id).T1.ToString & "℃"
    '            La_T2.Text = 'fesn(t_id).T2.ToString & "℃"

    '        End If
    '    End Sub


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
        'Tcq_Map.Width = Panel2.Width - 10
        'Tcq_Map.Height = Panel2.Height - 10

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



    'Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    '    Dim ii As Byte
    '    Dim 'fee As 'feature
    '    For ii = 1 To tcq_ 'feas.Count
    '        'fee = tcq_'feas.Item(ii)
    '        If 'fee.KeyValue <= Sys_node_count Then
    '            If 'fesn('fee.KeyValue - 1).Comm_State = Tcq.Comm_State_Enum.Comm_Fail Then
    '                'fee.Style.SymbolVectorColor = miColorYellow
    '            ElseIf 'fesn('fee.KeyValue - 1).Comm_State = Tcq.Comm_State_Enum.Comm_Wait Then
    '                'fee.Style.SymbolVectorColor = miColorWhite
    '            Else
    '                'fee.Style.SymbolVectorColor = miColorBlue
    '            End If
    '        End If
    '        'fee.Update()
    '    Next
    'End Sub


    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        'Tcq_Map.Zoom = 80
        'Tcq_Map.CenterX = 0.476
        'Tcq_Map.CenterY = 0.337
    End Sub


    'Private Sub Com_Maps_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Com_Maps.SelectedIndexChanged

    '    Timer1.Enabled = False

    '    Try
    '        'Tcq_Map.Geoset = Com_Maps.SelectedItem.ToString

    '        '第2层是绘图层
    '        'Tcq_Map.Layers.Item(2).Selectable = False

    '        '第1层是探测器节点层
    '        'Lyr_tcq = 'Tcq_Map.Layers.Item(1)

    '        '自动显示标志=false
    '        'Lyr_tcq.AutoLabel = False
    '        'Lyr_tcq.Selectable = True
    '        'Lyr_tcq.Editable = False

    '        ' tcq_() 'feas = 'Lyr_tcq.All'features

    '        Timer1.Interval = 2000
    '        Timer1.Enabled = True

    '    Catch ex As Exception
    '        MsgBox("图纸加载出错！！！")
    '    End Try

    'End Sub


End Class