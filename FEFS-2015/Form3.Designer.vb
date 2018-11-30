<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form3
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.C_id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.c_box_name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.C_type = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.c_net = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.c_net_id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.c_il_bj = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.c_t1_bj = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.c_t2_bj = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.c_en = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.TextBox7 = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.Button14 = New System.Windows.Forms.Button
        Me.Button13 = New System.Windows.Forms.Button
        Me.Button12 = New System.Windows.Forms.Button
        Me.Button11 = New System.Windows.Forms.Button
        Me.Button10 = New System.Windows.Forms.Button
        Me.RadioButton2 = New System.Windows.Forms.RadioButton
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.Label18 = New System.Windows.Forms.Label
        Me.ComboBox2 = New System.Windows.Forms.ComboBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.TextBox6 = New System.Windows.Forms.TextBox
        Me.TextBox5 = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.TextBox4 = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Button5 = New System.Windows.Forms.Button
        Me.Button9 = New System.Windows.Forms.Button
        Me.Button6 = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Button4 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.Button2 = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Button7 = New System.Windows.Forms.Button
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.C_id, Me.c_box_name, Me.C_type, Me.c_net, Me.c_net_id, Me.c_il_bj, Me.c_t1_bj, Me.c_t2_bj, Me.c_en})
        Me.DataGridView1.Location = New System.Drawing.Point(0, 1)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowTemplate.Height = 23
        Me.DataGridView1.Size = New System.Drawing.Size(882, 405)
        Me.DataGridView1.TabIndex = 0
        '
        'C_id
        '
        Me.C_id.HeaderText = "地址"
        Me.C_id.Name = "C_id"
        Me.C_id.ReadOnly = True
        Me.C_id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.C_id.Width = 70
        '
        'c_box_name
        '
        Me.c_box_name.HeaderText = "箱号"
        Me.c_box_name.Name = "c_box_name"
        Me.c_box_name.ReadOnly = True
        Me.c_box_name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.c_box_name.Width = 120
        '
        'C_type
        '
        Me.C_type.HeaderText = "表类型"
        Me.C_type.Name = "C_type"
        Me.C_type.ReadOnly = True
        Me.C_type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'c_net
        '
        Me.c_net.HeaderText = "归属支路"
        Me.c_net.Name = "c_net"
        Me.c_net.ReadOnly = True
        Me.c_net.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'c_net_id
        '
        Me.c_net_id.HeaderText = "支路位置"
        Me.c_net_id.Name = "c_net_id"
        Me.c_net_id.ReadOnly = True
        Me.c_net_id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'c_il_bj
        '
        Me.c_il_bj.HeaderText = "电流报警"
        Me.c_il_bj.Name = "c_il_bj"
        Me.c_il_bj.ReadOnly = True
        Me.c_il_bj.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'c_t1_bj
        '
        Me.c_t1_bj.HeaderText = "温度1报警"
        Me.c_t1_bj.Name = "c_t1_bj"
        Me.c_t1_bj.ReadOnly = True
        Me.c_t1_bj.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'c_t2_bj
        '
        Me.c_t2_bj.HeaderText = "温度2报警"
        Me.c_t2_bj.Name = "c_t2_bj"
        Me.c_t2_bj.ReadOnly = True
        Me.c_t2_bj.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'c_en
        '
        Me.c_en.HeaderText = "是否启用"
        Me.c_en.Name = "c_en"
        Me.c_en.ReadOnly = True
        Me.c_en.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.c_en.Width = 60
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.MediumAquamarine
        Me.Panel1.Controls.Add(Me.RadioButton2)
        Me.Panel1.Controls.Add(Me.RadioButton1)
        Me.Panel1.Controls.Add(Me.Button14)
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.Label21)
        Me.Panel1.Controls.Add(Me.TextBox7)
        Me.Panel1.Controls.Add(Me.Label20)
        Me.Panel1.Controls.Add(Me.Button13)
        Me.Panel1.Controls.Add(Me.Button12)
        Me.Panel1.Controls.Add(Me.Button11)
        Me.Panel1.Controls.Add(Me.Button10)
        Me.Panel1.Controls.Add(Me.ComboBox2)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.TextBox6)
        Me.Panel1.Controls.Add(Me.TextBox5)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.TextBox4)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.ComboBox1)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.TextBox2)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.TextBox1)
        Me.Panel1.Controls.Add(Me.Label19)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Location = New System.Drawing.Point(15, 257)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(882, 138)
        Me.Panel1.TabIndex = 22
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.ForeColor = System.Drawing.Color.Blue
        Me.Label21.Location = New System.Drawing.Point(791, 105)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(53, 12)
        Me.Label21.TabIndex = 52
        Me.Label21.Text = "修改成功"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label20.Location = New System.Drawing.Point(171, 18)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(80, 16)
        Me.Label20.TabIndex = 52
        Me.Label20.Text = "结束地址:"
        '
        'TextBox7
        '
        Me.TextBox7.Location = New System.Drawing.Point(248, 13)
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New System.Drawing.Size(54, 21)
        Me.TextBox7.TabIndex = 51
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label19.Location = New System.Drawing.Point(13, 18)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(80, 16)
        Me.Label19.TabIndex = 50
        Me.Label19.Text = "起始地址:"
        '
        'Button14
        '
        Me.Button14.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Button14.Location = New System.Drawing.Point(653, 36)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(73, 30)
        Me.Button14.TabIndex = 50
        Me.Button14.Text = "确定添加"
        Me.Button14.UseVisualStyleBackColor = False
        '
        'Button13
        '
        Me.Button13.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Button13.Location = New System.Drawing.Point(484, 9)
        Me.Button13.Name = "Button13"
        Me.Button13.Size = New System.Drawing.Size(91, 25)
        Me.Button13.TabIndex = 45
        Me.Button13.Text = "下一条记录"
        Me.Button13.UseVisualStyleBackColor = False
        '
        'Button12
        '
        Me.Button12.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Button12.Location = New System.Drawing.Point(364, 9)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(87, 25)
        Me.Button12.TabIndex = 44
        Me.Button12.Text = "上一条记录"
        Me.Button12.UseVisualStyleBackColor = False
        '
        'Button11
        '
        Me.Button11.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Button11.Location = New System.Drawing.Point(762, 68)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(105, 31)
        Me.Button11.TabIndex = 43
        Me.Button11.Text = "确定修改"
        Me.Button11.UseVisualStyleBackColor = False
        '
        'Button10
        '
        Me.Button10.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Button10.Location = New System.Drawing.Point(762, 20)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(105, 31)
        Me.Button10.TabIndex = 42
        Me.Button10.Text = "退    出"
        Me.Button10.UseVisualStyleBackColor = False
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(484, 81)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(47, 16)
        Me.RadioButton2.TabIndex = 41
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "屏蔽"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Location = New System.Drawing.Point(421, 81)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(59, 16)
        Me.RadioButton1.TabIndex = 40
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "不屏蔽"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label18.Location = New System.Drawing.Point(335, 81)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(80, 16)
        Me.Label18.TabIndex = 39
        Me.Label18.Text = "是否屏蔽:"
        '
        'ComboBox2
        '
        Me.ComboBox2.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8"})
        Me.ComboBox2.Location = New System.Drawing.Point(90, 107)
        Me.ComboBox2.MaxLength = 2
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(70, 22)
        Me.ComboBox2.TabIndex = 36
        Me.ComboBox2.Text = "1"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label13.Location = New System.Drawing.Point(15, 108)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(80, 16)
        Me.Label13.TabIndex = 35
        Me.Label13.Text = "归属支路:"
        '
        'TextBox6
        '
        Me.TextBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox6.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TextBox6.Location = New System.Drawing.Point(246, 108)
        Me.TextBox6.MaxLength = 5
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(56, 23)
        Me.TextBox6.TabIndex = 34
        Me.TextBox6.Text = "100"
        Me.TextBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox5
        '
        Me.TextBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox5.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TextBox5.Location = New System.Drawing.Point(246, 76)
        Me.TextBox5.MaxLength = 5
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(56, 23)
        Me.TextBox5.TabIndex = 32
        Me.TextBox5.Text = "100"
        Me.TextBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label14.Location = New System.Drawing.Point(171, 81)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(80, 16)
        Me.Label14.TabIndex = 31
        Me.Label14.Text = "T1报警值:"
        '
        'TextBox4
        '
        Me.TextBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox4.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TextBox4.Location = New System.Drawing.Point(90, 76)
        Me.TextBox4.MaxLength = 5
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(66, 23)
        Me.TextBox4.TabIndex = 30
        Me.TextBox4.Text = "500"
        Me.TextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label16.Location = New System.Drawing.Point(13, 81)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(80, 16)
        Me.Label16.TabIndex = 29
        Me.Label16.Text = "IL报警值:"
        '
        'ComboBox1
        '
        Me.ComboBox1.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"独立式:1路电流", "独立式:1路电流+1路温度", "独立式:1路电流+2路温度", "非独立式:1路电流", "非独立式:1路电流+1路温度", "非独立式:1路电流+2路温度"})
        Me.ComboBox1.Location = New System.Drawing.Point(397, 47)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(192, 22)
        Me.ComboBox1.TabIndex = 28
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label12.Location = New System.Drawing.Point(335, 50)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(72, 16)
        Me.Label12.TabIndex = 5
        Me.Label12.Text = "表类型："
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(65, 49)
        Me.TextBox2.MaxLength = 15
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(237, 21)
        Me.TextBox2.TabIndex = 3
        Me.TextBox2.Text = "一二三四五六七八九十"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label10.Location = New System.Drawing.Point(13, 50)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(56, 16)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = "箱号："
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(90, 13)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(54, 21)
        Me.TextBox1.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.SkyBlue
        Me.Panel2.Controls.Add(Me.Button7)
        Me.Panel2.Controls.Add(Me.Button5)
        Me.Panel2.Controls.Add(Me.Button9)
        Me.Panel2.Controls.Add(Me.Button6)
        Me.Panel2.Controls.Add(Me.Button1)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Button4)
        Me.Panel2.Controls.Add(Me.Button3)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Button2)
        Me.Panel2.Location = New System.Drawing.Point(15, 428)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(882, 133)
        Me.Panel2.TabIndex = 23
        '
        'Button5
        '
        Me.Button5.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Button5.Location = New System.Drawing.Point(777, 10)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(81, 37)
        Me.Button5.TabIndex = 39
        Me.Button5.Text = "退出"
        Me.Button5.UseVisualStyleBackColor = False
        '
        'Button9
        '
        Me.Button9.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Button9.Location = New System.Drawing.Point(766, 98)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(101, 25)
        Me.Button9.TabIndex = 38
        Me.Button9.Text = "添加默认500条"
        Me.Button9.UseVisualStyleBackColor = False
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Button6.Location = New System.Drawing.Point(780, 66)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(78, 25)
        Me.Button6.TabIndex = 35
        Me.Button6.Text = "清空表记录"
        Me.Button6.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Button1.Location = New System.Drawing.Point(307, 77)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(79, 30)
        Me.Button1.TabIndex = 34
        Me.Button1.Text = "删除行"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label8.Location = New System.Drawing.Point(209, 77)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(42, 14)
        Me.Label8.TabIndex = 33
        Me.Label8.Text = "100个"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label7.Location = New System.Drawing.Point(28, 98)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(231, 14)
        Me.Label7.TabIndex = 31
        Me.Label7.Text = "如要修改数量请到《基础信息设置》"
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Button4.Location = New System.Drawing.Point(421, 17)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(82, 30)
        Me.Button4.TabIndex = 30
        Me.Button4.Text = "添加单行"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Button3.Location = New System.Drawing.Point(307, 17)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(79, 30)
        Me.Button3.TabIndex = 29
        Me.Button3.Text = "修改行"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label6.Location = New System.Drawing.Point(13, 77)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(203, 14)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "绿色行为系统使用的探测器共："
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Button2.Location = New System.Drawing.Point(198, 17)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(79, 30)
        Me.Button2.TabIndex = 22
        Me.Button2.Text = "刷新数据表"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(25, 33)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 14)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "2=FEFM:IL"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(25, 57)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 14)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "4=FEFI:IL"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(101, 57)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(91, 14)
        Me.Label5.TabIndex = 27
        Me.Label5.Text = "5=FEFI:IL+1T"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(101, 33)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 14)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "3=FEFM:IL+2T"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(38, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(154, 14)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "表类型： 1=FEFM:IL+1T"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label9.Location = New System.Drawing.Point(171, 108)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(80, 16)
        Me.Label9.TabIndex = 53
        Me.Label9.Text = "T1报警值:"
        '
        'Button7
        '
        Me.Button7.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Button7.Location = New System.Drawing.Point(421, 77)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(82, 30)
        Me.Button7.TabIndex = 40
        Me.Button7.Text = "添加多行"
        Me.Button7.UseVisualStyleBackColor = False
        '
        'Form3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(924, 612)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.DataGridView1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Form3"
        Me.Text = "Form3"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents C_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents c_box_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C_type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents c_net As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents c_net_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents c_il_bj As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents c_t1_bj As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents c_t2_bj As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents c_en As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Button11 As System.Windows.Forms.Button
    Friend WithEvents Button10 As System.Windows.Forms.Button
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Button13 As System.Windows.Forms.Button
    Friend WithEvents Button12 As System.Windows.Forms.Button
    Friend WithEvents Button14 As System.Windows.Forms.Button
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents TextBox7 As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Button7 As System.Windows.Forms.Button
End Class
