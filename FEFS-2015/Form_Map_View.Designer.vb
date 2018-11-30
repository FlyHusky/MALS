<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_Map_View
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_Map_View))
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.La_com = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.La_box_name = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.La_id_str = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.La_t1 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.La_T2 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.La_IL = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.Button11 = New System.Windows.Forms.Button
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Button10 = New System.Windows.Forms.Button
        Me.Label_fe_key = New System.Windows.Forms.Label
        Me.Label_fe_id = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Button12 = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Button14 = New System.Windows.Forms.Button
        Me.Button13 = New System.Windows.Forms.Button
        Me.Button9 = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button5 = New System.Windows.Forms.Button
        Me.Button6 = New System.Windows.Forms.Button
        Me.Button7 = New System.Windows.Forms.Button
        Me.Com_Maps = New System.Windows.Forms.ComboBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel1)
        Me.Panel2.Controls.Add(Me.Panel5)
        Me.Panel2.Controls.Add(Me.Button12)
        Me.Panel2.Location = New System.Drawing.Point(2, 54)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(912, 546)
        Me.Panel2.TabIndex = 11
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.MediumAquamarine
        Me.Panel1.Controls.Add(Me.La_com)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.La_box_name)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.La_id_str)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.La_t1)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.La_T2)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.La_IL)
        Me.Panel1.Controls.Add(Me.Label28)
        Me.Panel1.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Panel1.Location = New System.Drawing.Point(399, 135)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(212, 109)
        Me.Panel1.TabIndex = 15
        '
        'La_com
        '
        Me.La_com.AutoSize = True
        Me.La_com.BackColor = System.Drawing.Color.Transparent
        Me.La_com.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.La_com.Location = New System.Drawing.Point(144, 17)
        Me.La_com.Name = "La_com"
        Me.La_com.Size = New System.Drawing.Size(35, 14)
        Me.La_com.TabIndex = 15
        Me.La_com.Text = "正常"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label16.Location = New System.Drawing.Point(104, 17)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(42, 14)
        Me.Label16.TabIndex = 14
        Me.Label16.Text = "通讯:"
        '
        'La_box_name
        '
        Me.La_box_name.AutoSize = True
        Me.La_box_name.BackColor = System.Drawing.Color.Transparent
        Me.La_box_name.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.La_box_name.Location = New System.Drawing.Point(55, 38)
        Me.La_box_name.Name = "La_box_name"
        Me.La_box_name.Size = New System.Drawing.Size(35, 14)
        Me.La_box_name.TabIndex = 13
        Me.La_box_name.Text = "0001"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label14.Location = New System.Drawing.Point(13, 38)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(42, 14)
        Me.Label14.TabIndex = 12
        Me.Label14.Text = "箱号:"
        '
        'La_id_str
        '
        Me.La_id_str.AutoSize = True
        Me.La_id_str.BackColor = System.Drawing.Color.Transparent
        Me.La_id_str.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.La_id_str.Location = New System.Drawing.Point(55, 17)
        Me.La_id_str.Name = "La_id_str"
        Me.La_id_str.Size = New System.Drawing.Size(35, 14)
        Me.La_id_str.TabIndex = 11
        Me.La_id_str.Text = "0001"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label10.Location = New System.Drawing.Point(13, 17)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(42, 14)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "编号:"
        '
        'La_t1
        '
        Me.La_t1.AutoSize = True
        Me.La_t1.BackColor = System.Drawing.Color.Transparent
        Me.La_t1.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.La_t1.Location = New System.Drawing.Point(55, 79)
        Me.La_t1.Name = "La_t1"
        Me.La_t1.Size = New System.Drawing.Size(35, 14)
        Me.La_t1.TabIndex = 9
        Me.La_t1.Text = "50.5"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label8.Location = New System.Drawing.Point(13, 79)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(49, 14)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "温度1:"
        '
        'La_T2
        '
        Me.La_T2.AutoSize = True
        Me.La_T2.BackColor = System.Drawing.Color.Transparent
        Me.La_T2.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.La_T2.Location = New System.Drawing.Point(151, 79)
        Me.La_T2.Name = "La_T2"
        Me.La_T2.Size = New System.Drawing.Size(35, 14)
        Me.La_T2.TabIndex = 7
        Me.La_T2.Text = "50.5"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label15.Location = New System.Drawing.Point(104, 79)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(49, 14)
        Me.Label15.TabIndex = 6
        Me.Label15.Text = "温度2:"
        '
        'La_IL
        '
        Me.La_IL.AutoSize = True
        Me.La_IL.BackColor = System.Drawing.Color.Transparent
        Me.La_IL.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.La_IL.Location = New System.Drawing.Point(82, 58)
        Me.La_IL.Name = "La_IL"
        Me.La_IL.Size = New System.Drawing.Size(42, 14)
        Me.La_IL.TabIndex = 4
        Me.La_IL.Text = "999.9"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label28.Location = New System.Drawing.Point(13, 58)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(70, 14)
        Me.Label28.TabIndex = 0
        Me.Label28.Text = "剩余电流:"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.TextBox2)
        Me.Panel5.Controls.Add(Me.Button11)
        Me.Panel5.Controls.Add(Me.TextBox1)
        Me.Panel5.Controls.Add(Me.Button10)
        Me.Panel5.Controls.Add(Me.Label_fe_key)
        Me.Panel5.Controls.Add(Me.Label_fe_id)
        Me.Panel5.Controls.Add(Me.Label4)
        Me.Panel5.Controls.Add(Me.Label3)
        Me.Panel5.Controls.Add(Me.Label2)
        Me.Panel5.Controls.Add(Me.Label1)
        Me.Panel5.Location = New System.Drawing.Point(33, 38)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(301, 206)
        Me.Panel5.TabIndex = 14
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(128, 97)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(76, 21)
        Me.TextBox2.TabIndex = 12
        '
        'Button11
        '
        Me.Button11.Location = New System.Drawing.Point(175, 146)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(57, 24)
        Me.Button11.TabIndex = 11
        Me.Button11.Text = "确定"
        Me.Button11.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(128, 65)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(76, 21)
        Me.TextBox1.TabIndex = 9
        '
        'Button10
        '
        Me.Button10.Location = New System.Drawing.Point(69, 148)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(57, 24)
        Me.Button10.TabIndex = 8
        Me.Button10.Text = "修改"
        Me.Button10.UseVisualStyleBackColor = True
        '
        'Label_fe_key
        '
        Me.Label_fe_key.AutoSize = True
        Me.Label_fe_key.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label_fe_key.Location = New System.Drawing.Point(125, 42)
        Me.Label_fe_key.Name = "Label_fe_key"
        Me.Label_fe_key.Size = New System.Drawing.Size(48, 16)
        Me.Label_fe_key.TabIndex = 5
        Me.Label_fe_key.Text = "Name:"
        '
        'Label_fe_id
        '
        Me.Label_fe_id.AutoSize = True
        Me.Label_fe_id.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label_fe_id.Location = New System.Drawing.Point(125, 16)
        Me.Label_fe_id.Name = "Label_fe_id"
        Me.Label_fe_id.Size = New System.Drawing.Size(48, 16)
        Me.Label_fe_id.TabIndex = 4
        Me.Label_fe_id.Text = "Name:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label4.Location = New System.Drawing.Point(74, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 16)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Sytle:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label3.Location = New System.Drawing.Point(46, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 16)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "keyValue:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label2.Location = New System.Drawing.Point(30, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "FeatureKey:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.Location = New System.Drawing.Point(38, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "featureid:"
        '
        'Button12
        '
        Me.Button12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button12.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Button12.Image = CType(resources.GetObject("Button12.Image"), System.Drawing.Image)
        Me.Button12.Location = New System.Drawing.Point(577, 27)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(26, 27)
        Me.Button12.TabIndex = 27
        Me.Button12.UseVisualStyleBackColor = False
        Me.Button12.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Aquamarine
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Button14)
        Me.GroupBox1.Controls.Add(Me.Button13)
        Me.GroupBox1.Controls.Add(Me.Button9)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.Button5)
        Me.GroupBox1.Controls.Add(Me.Button6)
        Me.GroupBox1.Controls.Add(Me.Button7)
        Me.GroupBox1.Controls.Add(Me.Com_Maps)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.GroupBox1.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Moccasin
        Me.GroupBox1.Location = New System.Drawing.Point(2, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(905, 45)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.White
        Me.Label7.Font = New System.Drawing.Font("幼圆", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Red
        Me.Label7.Location = New System.Drawing.Point(681, 24)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 14)
        Me.Label7.TabIndex = 33
        Me.Label7.Text = "红色:报警"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Silver
        Me.Label6.Font = New System.Drawing.Font("幼圆", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Yellow
        Me.Label6.Location = New System.Drawing.Point(767, 24)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 14)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "黄色:故障"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.White
        Me.Label5.Font = New System.Drawing.Font("幼圆", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(595, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 14)
        Me.Label5.TabIndex = 31
        Me.Label5.Text = "蓝色:正常"
        '
        'Button14
        '
        Me.Button14.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button14.BackgroundImage = CType(resources.GetObject("Button14.BackgroundImage"), System.Drawing.Image)
        Me.Button14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button14.Location = New System.Drawing.Point(534, 15)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(28, 27)
        Me.Button14.TabIndex = 30
        Me.Button14.UseVisualStyleBackColor = True
        Me.Button14.Visible = False
        '
        'Button13
        '
        Me.Button13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button13.BackgroundImage = CType(resources.GetObject("Button13.BackgroundImage"), System.Drawing.Image)
        Me.Button13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button13.Location = New System.Drawing.Point(534, 15)
        Me.Button13.Name = "Button13"
        Me.Button13.Size = New System.Drawing.Size(28, 27)
        Me.Button13.TabIndex = 29
        Me.Button13.UseVisualStyleBackColor = True
        '
        'Button9
        '
        Me.Button9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button9.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Button9.Image = CType(resources.GetObject("Button9.Image"), System.Drawing.Image)
        Me.Button9.Location = New System.Drawing.Point(330, 14)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(26, 27)
        Me.Button9.TabIndex = 26
        Me.Button9.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Button1.Image = Global.MALS.My.Resources.Resources.手
        Me.Button1.Location = New System.Drawing.Point(497, 14)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(26, 27)
        Me.Button1.TabIndex = 21
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button5
        '
        Me.Button5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button5.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Button5.Image = Global.MALS.My.Resources.Resources.适合窗口
        Me.Button5.Location = New System.Drawing.Point(455, 14)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(26, 27)
        Me.Button5.TabIndex = 20
        Me.Button5.UseVisualStyleBackColor = False
        '
        'Button6
        '
        Me.Button6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button6.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Button6.Image = Global.MALS.My.Resources.Resources.缩小
        Me.Button6.Location = New System.Drawing.Point(414, 14)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(26, 27)
        Me.Button6.TabIndex = 19
        Me.Button6.UseVisualStyleBackColor = False
        '
        'Button7
        '
        Me.Button7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button7.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Button7.Image = Global.MALS.My.Resources.Resources.放大
        Me.Button7.Location = New System.Drawing.Point(373, 14)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(26, 27)
        Me.Button7.TabIndex = 18
        Me.Button7.UseVisualStyleBackColor = False
        '
        'Com_Maps
        '
        Me.Com_Maps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Com_Maps.FormattingEnabled = True
        Me.Com_Maps.Location = New System.Drawing.Point(95, 17)
        Me.Com_Maps.Name = "Com_Maps"
        Me.Com_Maps.Size = New System.Drawing.Size(142, 24)
        Me.Com_Maps.TabIndex = 17
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(19, 22)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(80, 16)
        Me.Label11.TabIndex = 4
        Me.Label11.Text = "选择图纸:"
        '
        'Form_Map_View
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(916, 612)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Location = New System.Drawing.Point(180, 90)
        Me.Name = "Form_Map_View"
        Me.Text = "Form_Map_View"
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Com_Maps As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Button11 As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Button10 As System.Windows.Forms.Button
    Friend WithEvents Label_fe_key As System.Windows.Forms.Label
    Friend WithEvents Label_fe_id As System.Windows.Forms.Label
    Friend WithEvents Button12 As System.Windows.Forms.Button
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents La_IL As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents La_T2 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Button13 As System.Windows.Forms.Button
    Friend WithEvents Button14 As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents La_t1 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents La_box_name As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents La_id_str As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents La_com As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
End Class
