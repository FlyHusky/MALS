<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class All
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.But_sys_map = New System.Windows.Forms.Button
        Me.But_sys_check = New System.Windows.Forms.Button
        Me.ESC_but = New System.Windows.Forms.Button
        Me.ReSet_but = New System.Windows.Forms.Button
        Me.ZhuXiao_but = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.But_sys_reset = New System.Windows.Forms.Button
        Me.But_sys_info = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Button9 = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Button10 = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Tcq_port = New System.IO.Ports.SerialPort(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.But_tcq_set = New System.Windows.Forms.Button
        Me.But_sys_set = New System.Windows.Forms.Button
        Me.But_Map_View = New System.Windows.Forms.Button
        Me.But_Table_View = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.Label7 = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(46, 136)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(115, 36)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "实时数据"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(46, 194)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(115, 36)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "历史报警"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(46, 254)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(115, 36)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "参数设置"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'But_sys_map
        '
        Me.But_sys_map.Location = New System.Drawing.Point(46, 316)
        Me.But_sys_map.Name = "But_sys_map"
        Me.But_sys_map.Size = New System.Drawing.Size(115, 36)
        Me.But_sys_map.TabIndex = 3
        Me.But_sys_map.Text = "系统布线"
        Me.But_sys_map.UseVisualStyleBackColor = True
        '
        'But_sys_check
        '
        Me.But_sys_check.Location = New System.Drawing.Point(46, 382)
        Me.But_sys_check.Name = "But_sys_check"
        Me.But_sys_check.Size = New System.Drawing.Size(115, 36)
        Me.But_sys_check.TabIndex = 4
        Me.But_sys_check.Text = "设备自检"
        Me.But_sys_check.UseVisualStyleBackColor = True
        '
        'ESC_but
        '
        Me.ESC_but.Location = New System.Drawing.Point(1071, 16)
        Me.ESC_but.Name = "ESC_but"
        Me.ESC_but.Size = New System.Drawing.Size(50, 50)
        Me.ESC_but.TabIndex = 5
        Me.ESC_but.Text = "退出"
        Me.ESC_but.UseVisualStyleBackColor = True
        '
        'ReSet_but
        '
        Me.ReSet_but.Location = New System.Drawing.Point(915, 16)
        Me.ReSet_but.Name = "ReSet_but"
        Me.ReSet_but.Size = New System.Drawing.Size(50, 50)
        Me.ReSet_but.TabIndex = 6
        Me.ReSet_but.Text = "消音"
        Me.ReSet_but.UseVisualStyleBackColor = True
        '
        'ZhuXiao_but
        '
        Me.ZhuXiao_but.Location = New System.Drawing.Point(994, 16)
        Me.ZhuXiao_but.Name = "ZhuXiao_but"
        Me.ZhuXiao_but.Size = New System.Drawing.Size(50, 50)
        Me.ZhuXiao_but.TabIndex = 7
        Me.ZhuXiao_but.Text = "注销"
        Me.ZhuXiao_but.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("楷体_GB2312", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.Location = New System.Drawing.Point(41, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(385, 29)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "杭州富自电气火灾监控系统"
        '
        'But_sys_reset
        '
        Me.But_sys_reset.Location = New System.Drawing.Point(46, 446)
        Me.But_sys_reset.Name = "But_sys_reset"
        Me.But_sys_reset.Size = New System.Drawing.Size(115, 36)
        Me.But_sys_reset.TabIndex = 10
        Me.But_sys_reset.Text = "系统复位"
        Me.But_sys_reset.UseVisualStyleBackColor = True
        '
        'But_sys_info
        '
        Me.But_sys_info.Location = New System.Drawing.Point(46, 509)
        Me.But_sys_info.Name = "But_sys_info"
        Me.But_sys_info.Size = New System.Drawing.Size(115, 36)
        Me.But_sys_info.TabIndex = 11
        Me.But_sys_info.Text = "系统信息"
        Me.But_sys_info.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Button9)
        Me.Panel1.Location = New System.Drawing.Point(191, 670)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(450, 174)
        Me.Panel1.TabIndex = 12
        '
        'Button9
        '
        Me.Button9.ForeColor = System.Drawing.Color.Red
        Me.Button9.Location = New System.Drawing.Point(-1, 2)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(446, 27)
        Me.Button9.TabIndex = 0
        Me.Button9.Text = "当前报警信息"
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.DataGridView1)
        Me.Panel2.Controls.Add(Me.Button10)
        Me.Panel2.Location = New System.Drawing.Point(657, 670)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(410, 174)
        Me.Panel2.TabIndex = 13
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5})
        Me.DataGridView1.Location = New System.Drawing.Point(3, 28)
        Me.DataGridView1.Name = "DataGridView1"
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle12.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle12
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowTemplate.Height = 23
        Me.DataGridView1.Size = New System.Drawing.Size(402, 139)
        Me.DataGridView1.TabIndex = 1
        '
        'Column1
        '
        Me.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle8
        Me.Column1.HeaderText = "序号"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column1.Width = 35
        '
        'Column2
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column2.DefaultCellStyle = DataGridViewCellStyle9
        Me.Column2.HeaderText = "发生时间"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column2.Width = 73
        '
        'Column3
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column3.DefaultCellStyle = DataGridViewCellStyle10
        Me.Column3.HeaderText = "箱号"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column3.Width = 84
        '
        'Column4
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column4.DefaultCellStyle = DataGridViewCellStyle11
        Me.Column4.HeaderText = "编号"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column4.Width = 88
        '
        'Column5
        '
        Me.Column5.HeaderText = "故障类型"
        Me.Column5.Name = "Column5"
        '
        'Button10
        '
        Me.Button10.ForeColor = System.Drawing.Color.Red
        Me.Button10.Location = New System.Drawing.Point(3, 3)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(402, 24)
        Me.Button10.TabIndex = 0
        Me.Button10.Text = "当前故障信息"
        Me.Button10.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label5.Location = New System.Drawing.Point(69, 93)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 16)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "功能导航"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(61, 696)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 12)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "Label6"
        '
        'Timer1
        '
        '
        'Tcq_port
        '
        '
        'But_tcq_set
        '
        Me.But_tcq_set.Location = New System.Drawing.Point(53, 296)
        Me.But_tcq_set.Name = "But_tcq_set"
        Me.But_tcq_set.Size = New System.Drawing.Size(100, 25)
        Me.But_tcq_set.TabIndex = 20
        Me.But_tcq_set.Text = "探测器参数"
        Me.But_tcq_set.UseVisualStyleBackColor = True
        '
        'But_sys_set
        '
        Me.But_sys_set.Location = New System.Drawing.Point(53, 327)
        Me.But_sys_set.Name = "But_sys_set"
        Me.But_sys_set.Size = New System.Drawing.Size(100, 25)
        Me.But_sys_set.TabIndex = 21
        Me.But_sys_set.Text = "系统参数"
        Me.But_sys_set.UseVisualStyleBackColor = True
        '
        'But_Map_View
        '
        Me.But_Map_View.Location = New System.Drawing.Point(53, 209)
        Me.But_Map_View.Name = "But_Map_View"
        Me.But_Map_View.Size = New System.Drawing.Size(100, 25)
        Me.But_Map_View.TabIndex = 23
        Me.But_Map_View.Text = "地图显示"
        Me.But_Map_View.UseVisualStyleBackColor = True
        '
        'But_Table_View
        '
        Me.But_Table_View.Location = New System.Drawing.Point(53, 178)
        Me.But_Table_View.Name = "But_Table_View"
        Me.But_Table_View.Size = New System.Drawing.Size(100, 25)
        Me.But_Table_View.TabIndex = 22
        Me.But_Table_View.Text = "列表显示"
        Me.But_Table_View.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(46, 593)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(94, 44)
        Me.Button4.TabIndex = 24
        Me.Button4.Text = "Button4"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(491, 20)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(0, 12)
        Me.Label7.TabIndex = 25
        '
        'All
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Lavender
        Me.ClientSize = New System.Drawing.Size(1139, 778)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.But_Map_View)
        Me.Controls.Add(Me.But_Table_View)
        Me.Controls.Add(Me.But_sys_set)
        Me.Controls.Add(Me.But_tcq_set)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.But_sys_info)
        Me.Controls.Add(Me.But_sys_reset)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ZhuXiao_but)
        Me.Controls.Add(Me.ReSet_but)
        Me.Controls.Add(Me.ESC_but)
        Me.Controls.Add(Me.But_sys_check)
        Me.Controls.Add(Me.But_sys_map)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Name = "All"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents But_sys_map As System.Windows.Forms.Button
    Friend WithEvents But_sys_check As System.Windows.Forms.Button
    Friend WithEvents ESC_but As System.Windows.Forms.Button
    Friend WithEvents ReSet_but As System.Windows.Forms.Button
    Friend WithEvents ZhuXiao_but As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents But_sys_reset As System.Windows.Forms.Button
    Friend WithEvents But_sys_info As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Button10 As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Tcq_port As System.IO.Ports.SerialPort
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents But_tcq_set As System.Windows.Forms.Button
    Friend WithEvents But_sys_set As System.Windows.Forms.Button
    Friend WithEvents But_Map_View As System.Windows.Forms.Button
    Friend WithEvents But_Table_View As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
