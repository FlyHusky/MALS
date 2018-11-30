<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormAlarmFault
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
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle23 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle24 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle21 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle22 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle25 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle31 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle32 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle26 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle27 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle28 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle29 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle30 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.ButFault = New System.Windows.Forms.Button
        Me.ButAlarm = New System.Windows.Forms.Button
        Me.dgridAlarm = New System.Windows.Forms.DataGridView
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.dgridFault = New System.Windows.Forms.DataGridView
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TreeView1 = New System.Windows.Forms.TreeView
        Me.Label1 = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.RichTextFile = New System.Windows.Forms.RichTextBox
        CType(Me.dgridAlarm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgridFault, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButFault
        '
        Me.ButFault.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ButFault.Font = New System.Drawing.Font("新宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ButFault.ForeColor = System.Drawing.Color.Red
        Me.ButFault.Location = New System.Drawing.Point(625, 23)
        Me.ButFault.Name = "ButFault"
        Me.ButFault.Size = New System.Drawing.Size(513, 52)
        Me.ButFault.TabIndex = 0
        Me.ButFault.Text = "当前故障信息 共计0条"
        Me.ButFault.UseVisualStyleBackColor = True
        '
        'ButAlarm
        '
        Me.ButAlarm.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ButAlarm.Font = New System.Drawing.Font("新宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ButAlarm.ForeColor = System.Drawing.Color.Red
        Me.ButAlarm.Location = New System.Drawing.Point(24, 23)
        Me.ButAlarm.Name = "ButAlarm"
        Me.ButAlarm.Size = New System.Drawing.Size(513, 52)
        Me.ButAlarm.TabIndex = 0
        Me.ButAlarm.Text = "当前报警信息 共计0条"
        Me.ButAlarm.UseVisualStyleBackColor = True
        '
        'dgridAlarm
        '
        Me.dgridAlarm.AllowUserToAddRows = False
        Me.dgridAlarm.AllowUserToDeleteRows = False
        Me.dgridAlarm.AllowUserToResizeColumns = False
        Me.dgridAlarm.AllowUserToResizeRows = False
        Me.dgridAlarm.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.dgridAlarm.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle17.Font = New System.Drawing.Font("新宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgridAlarm.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle17
        Me.dgridAlarm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgridAlarm.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5})
        DataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle23.Font = New System.Drawing.Font("新宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle23.ForeColor = System.Drawing.Color.Red
        DataGridViewCellStyle23.SelectionBackColor = System.Drawing.Color.PowderBlue
        DataGridViewCellStyle23.SelectionForeColor = System.Drawing.Color.Red
        DataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgridAlarm.DefaultCellStyle = DataGridViewCellStyle23
        Me.dgridAlarm.EnableHeadersVisualStyles = False
        Me.dgridAlarm.Location = New System.Drawing.Point(24, 123)
        Me.dgridAlarm.MultiSelect = False
        Me.dgridAlarm.Name = "dgridAlarm"
        Me.dgridAlarm.ReadOnly = True
        DataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle24.Font = New System.Drawing.Font("新宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle24.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgridAlarm.RowHeadersDefaultCellStyle = DataGridViewCellStyle24
        Me.dgridAlarm.RowHeadersVisible = False
        Me.dgridAlarm.RowTemplate.Height = 23
        Me.dgridAlarm.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgridAlarm.Size = New System.Drawing.Size(513, 121)
        Me.dgridAlarm.TabIndex = 3
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle18.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle18
        Me.DataGridViewTextBoxColumn1.HeaderText = ""
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn1.Width = 35
        '
        'DataGridViewTextBoxColumn2
        '
        DataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle19
        Me.DataGridViewTextBoxColumn2.HeaderText = "发生时间"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 130
        '
        'DataGridViewTextBoxColumn3
        '
        DataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle20
        Me.DataGridViewTextBoxColumn3.HeaderText = "所在位置"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn4
        '
        DataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle21
        Me.DataGridViewTextBoxColumn4.HeaderText = "编号"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'DataGridViewTextBoxColumn5
        '
        DataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn5.DefaultCellStyle = DataGridViewCellStyle22
        Me.DataGridViewTextBoxColumn5.HeaderText = "信息"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn5.Width = 122
        '
        'dgridFault
        '
        Me.dgridFault.AllowUserToAddRows = False
        Me.dgridFault.AllowUserToDeleteRows = False
        Me.dgridFault.AllowUserToResizeColumns = False
        Me.dgridFault.AllowUserToResizeRows = False
        Me.dgridFault.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.dgridFault.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken
        DataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle25.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle25.Font = New System.Drawing.Font("新宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgridFault.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle25
        Me.dgridFault.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgridFault.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn8, Me.DataGridViewTextBoxColumn9, Me.DataGridViewTextBoxColumn10})
        DataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle31.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle31.Font = New System.Drawing.Font("新宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle31.ForeColor = System.Drawing.Color.Red
        DataGridViewCellStyle31.SelectionBackColor = System.Drawing.Color.PowderBlue
        DataGridViewCellStyle31.SelectionForeColor = System.Drawing.Color.Red
        DataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgridFault.DefaultCellStyle = DataGridViewCellStyle31
        Me.dgridFault.EnableHeadersVisualStyles = False
        Me.dgridFault.Location = New System.Drawing.Point(625, 103)
        Me.dgridFault.MultiSelect = False
        Me.dgridFault.Name = "dgridFault"
        Me.dgridFault.ReadOnly = True
        DataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle32.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle32.Font = New System.Drawing.Font("新宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle32.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle32.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle32.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle32.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgridFault.RowHeadersDefaultCellStyle = DataGridViewCellStyle32
        Me.dgridFault.RowHeadersVisible = False
        Me.dgridFault.RowTemplate.Height = 23
        Me.dgridFault.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgridFault.Size = New System.Drawing.Size(513, 141)
        Me.dgridFault.TabIndex = 5
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle26.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.DataGridViewTextBoxColumn6.DefaultCellStyle = DataGridViewCellStyle26
        Me.DataGridViewTextBoxColumn6.HeaderText = ""
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn6.Width = 35
        '
        'DataGridViewTextBoxColumn7
        '
        DataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn7.DefaultCellStyle = DataGridViewCellStyle27
        Me.DataGridViewTextBoxColumn7.HeaderText = "发生时间"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Width = 130
        '
        'DataGridViewTextBoxColumn8
        '
        DataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn8.DefaultCellStyle = DataGridViewCellStyle28
        Me.DataGridViewTextBoxColumn8.HeaderText = "所在位置"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn9
        '
        DataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn9.DefaultCellStyle = DataGridViewCellStyle29
        Me.DataGridViewTextBoxColumn9.HeaderText = "编号/地址"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        '
        'DataGridViewTextBoxColumn10
        '
        DataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn10.DefaultCellStyle = DataGridViewCellStyle30
        Me.DataGridViewTextBoxColumn10.HeaderText = "信息"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn10.Width = 122
        '
        'TreeView1
        '
        Me.TreeView1.Location = New System.Drawing.Point(24, 275)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(224, 334)
        Me.TreeView1.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 256)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(168, 16)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "更多的历史报警信息："
        '
        'Timer1
        '
        '
        'RichTextFile
        '
        Me.RichTextFile.Location = New System.Drawing.Point(275, 275)
        Me.RichTextFile.Name = "RichTextFile"
        Me.RichTextFile.Size = New System.Drawing.Size(862, 333)
        Me.RichTextFile.TabIndex = 9
        Me.RichTextFile.Text = ""
        '
        'FormAlarmFault
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1179, 642)
        Me.Controls.Add(Me.RichTextFile)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TreeView1)
        Me.Controls.Add(Me.dgridFault)
        Me.Controls.Add(Me.dgridAlarm)
        Me.Controls.Add(Me.ButAlarm)
        Me.Controls.Add(Me.ButFault)
        Me.Name = "FormAlarmFault"
        Me.Text = "FormAlarmFault"
        CType(Me.dgridAlarm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgridFault, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButFault As System.Windows.Forms.Button
    Friend WithEvents ButAlarm As System.Windows.Forms.Button
    Friend WithEvents dgridAlarm As System.Windows.Forms.DataGridView
    Friend WithEvents dgridFault As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents RichTextFile As System.Windows.Forms.RichTextBox
End Class
