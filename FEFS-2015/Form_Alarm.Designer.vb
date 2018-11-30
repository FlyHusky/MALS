<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_Alarm
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.DGV1 = New System.Windows.Forms.DataGridView
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label4 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.La_date = New System.Windows.Forms.Label
        Me.La_kind = New System.Windows.Forms.Label
        Me.La_time = New System.Windows.Forms.Label
        Me.La_value = New System.Windows.Forms.Label
        Me.La_id = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Data = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.sure = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.DGV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DGV1
        '
        Me.DGV1.AllowUserToAddRows = False
        Me.DGV1.AllowUserToDeleteRows = False
        Me.DGV1.AllowUserToResizeColumns = False
        Me.DGV1.AllowUserToResizeRows = False
        Me.DGV1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.DGV1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("新宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DGV1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn8, Me.DataGridViewTextBoxColumn9, Me.DataGridViewTextBoxColumn10, Me.Data, Me.sure})
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle9.Font = New System.Drawing.Font("新宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.Color.Red
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.PowderBlue
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Red
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGV1.DefaultCellStyle = DataGridViewCellStyle9
        Me.DGV1.EnableHeadersVisualStyles = False
        Me.DGV1.Location = New System.Drawing.Point(151, 117)
        Me.DGV1.MultiSelect = False
        Me.DGV1.Name = "DGV1"
        Me.DGV1.ReadOnly = True
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle10.Font = New System.Drawing.Font("新宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV1.RowHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.DGV1.RowHeadersVisible = False
        Me.DGV1.RowTemplate.Height = 23
        Me.DGV1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGV1.Size = New System.Drawing.Size(734, 246)
        Me.DGV1.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("新宋体", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(400, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(210, 24)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "当前报警信息查询"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("新宋体", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label2.Location = New System.Drawing.Point(33, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(142, 21)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "报警总数量:5"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("新宋体", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label3.Location = New System.Drawing.Point(415, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(142, 21)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "待确认数量:0"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Snow
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Red
        Me.GroupBox1.Location = New System.Drawing.Point(207, 57)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(591, 45)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        '
        'Timer1
        '
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.PowderBlue
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.La_id)
        Me.Panel1.Controls.Add(Me.La_value)
        Me.Panel1.Controls.Add(Me.La_time)
        Me.Panel1.Controls.Add(Me.La_kind)
        Me.Panel1.Controls.Add(Me.La_date)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Location = New System.Drawing.Point(185, 273)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(666, 149)
        Me.Panel1.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(71, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(0, 12)
        Me.Label4.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("微软雅黑", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Blue
        Me.Button1.Location = New System.Drawing.Point(253, 93)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(158, 40)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "点击确认"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("宋体", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label5.Location = New System.Drawing.Point(30, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(85, 19)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "发生时间"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("宋体", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label7.Location = New System.Drawing.Point(186, 22)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(47, 19)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "箱号"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("宋体", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label8.Location = New System.Drawing.Point(292, 22)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(85, 19)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "回路编号"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("宋体", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label9.Location = New System.Drawing.Point(423, 22)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(85, 19)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "报警类型"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("宋体", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label10.Location = New System.Drawing.Point(571, 22)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(66, 19)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "报警值"
        '
        'La_date
        '
        Me.La_date.AutoSize = True
        Me.La_date.Font = New System.Drawing.Font("宋体", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.La_date.ForeColor = System.Drawing.Color.Red
        Me.La_date.Location = New System.Drawing.Point(20, 55)
        Me.La_date.Name = "La_date"
        Me.La_date.Size = New System.Drawing.Size(109, 19)
        Me.La_date.TabIndex = 8
        Me.La_date.Text = "2017-5-12 "
        '
        'La_kind
        '
        Me.La_kind.AutoSize = True
        Me.La_kind.Font = New System.Drawing.Font("宋体", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.La_kind.ForeColor = System.Drawing.Color.Red
        Me.La_kind.Location = New System.Drawing.Point(398, 55)
        Me.La_kind.Name = "La_kind"
        Me.La_kind.Size = New System.Drawing.Size(123, 19)
        Me.La_kind.TabIndex = 9
        Me.La_kind.Text = "剩余电流报警"
        '
        'La_time
        '
        Me.La_time.AutoSize = True
        Me.La_time.Font = New System.Drawing.Font("宋体", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.La_time.ForeColor = System.Drawing.Color.Red
        Me.La_time.Location = New System.Drawing.Point(20, 83)
        Me.La_time.Name = "La_time"
        Me.La_time.Size = New System.Drawing.Size(89, 19)
        Me.La_time.TabIndex = 10
        Me.La_time.Text = "21:20:30"
        '
        'La_value
        '
        Me.La_value.AutoSize = True
        Me.La_value.Font = New System.Drawing.Font("宋体", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.La_value.ForeColor = System.Drawing.Color.Red
        Me.La_value.Location = New System.Drawing.Point(571, 55)
        Me.La_value.Name = "La_value"
        Me.La_value.Size = New System.Drawing.Size(59, 19)
        Me.La_value.TabIndex = 11
        Me.La_value.Text = "500.3"
        '
        'La_id
        '
        Me.La_id.AutoSize = True
        Me.La_id.Font = New System.Drawing.Font("宋体", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.La_id.ForeColor = System.Drawing.Color.Red
        Me.La_id.Location = New System.Drawing.Point(308, 55)
        Me.La_id.Name = "La_id"
        Me.La_id.Size = New System.Drawing.Size(49, 19)
        Me.La_id.TabIndex = 12
        Me.La_id.Text = "0001"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("宋体", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(174, 55)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 19)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "01ALE"
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Red
        Me.DataGridViewTextBoxColumn6.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewTextBoxColumn6.HeaderText = ""
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn6.Width = 30
        '
        'DataGridViewTextBoxColumn7
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("新宋体", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Red
        Me.DataGridViewTextBoxColumn7.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn7.HeaderText = "发生时间"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn7.Width = 140
        '
        'DataGridViewTextBoxColumn8
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Red
        Me.DataGridViewTextBoxColumn8.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewTextBoxColumn8.HeaderText = "箱号"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn8.Width = 120
        '
        'DataGridViewTextBoxColumn9
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Red
        Me.DataGridViewTextBoxColumn9.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewTextBoxColumn9.HeaderText = "回路编号"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn10
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Red
        Me.DataGridViewTextBoxColumn10.DefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridViewTextBoxColumn10.HeaderText = "报警类型"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn10.Width = 110
        '
        'Data
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Data.DefaultCellStyle = DataGridViewCellStyle7
        Me.Data.HeaderText = "报警值"
        Me.Data.Name = "Data"
        Me.Data.ReadOnly = True
        Me.Data.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'sure
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.sure.DefaultCellStyle = DataGridViewCellStyle8
        Me.sure.HeaderText = "是否确认"
        Me.sure.Name = "sure"
        Me.sure.ReadOnly = True
        Me.sure.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Form_Alarm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.SkyBlue
        Me.ClientSize = New System.Drawing.Size(1060, 628)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DGV1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Form_Alarm"
        Me.Text = "Form_Alarm"
        CType(Me.DGV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DGV1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents La_time As System.Windows.Forms.Label
    Friend WithEvents La_kind As System.Windows.Forms.Label
    Friend WithEvents La_date As System.Windows.Forms.Label
    Friend WithEvents La_id As System.Windows.Forms.Label
    Friend WithEvents La_value As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Data As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents sure As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
