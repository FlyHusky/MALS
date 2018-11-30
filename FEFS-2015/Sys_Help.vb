Imports System.IO
Public Class Form_Help


    Private Sub Form_Help_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.Top = Form1.Top
        Me.Left = Form1.Left
        Me.Width = Form1.Width
        Me.Height = Form1.Height

        Panel1.Top = 10
        Panel1.Left = (Me.Width - Panel1.Width) / 2

        Label1.Cursor = Cursors.Hand
        Label2.Cursor = Cursors.Hand

        Panel2.Top = Panel1.Top + Panel1.Height + 10
        Panel2.Left = 20
        Panel2.Width = Me.Width - 40
        Panel2.Height = Me.Height - Panel2.Top - 30

        'pdf1.Top = 0
        'pdf1.Left = 0
        'pdf1.Height = Panel2.Height
        'pdf1.Width = Panel2.Width

        'Dim help_file_path As String

        'help_file_path = My.Application.Info.DirectoryPath & "\FEFS电气火灾监控设备使用说明书.pdf"

        'If File.Exists(help_file_path) Then
        '    pdf1.LoadFile(help_file_path)
        'End If

    End Sub


    '点击查看 设备使用操作
    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click



        Dim help_file_path As String

        help_file_path = My.Application.Info.DirectoryPath & "\FEFS电气火灾监控设备使用说明书.pdf"

        If File.Exists(help_file_path) Then
            '  pdf1.LoadFile(help_file_path)
        Else
            MsgBox("未找到该文件！！！")
        End If


    End Sub

    '点击查看 软件使用说明
    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click
        '   pdf1.LoadFile("FEFS电气火灾系统软件说明书.pdf")

        Dim help_file_path As String

        help_file_path = My.Application.Info.DirectoryPath & "\FEFS电气火灾监控设备软件说明书.pdf"

        If File.Exists(help_file_path) Then
            ' pdf1.LoadFile(help_file_path)
        Else
            MsgBox("未找到该文件！！！")
        End If

    End Sub
End Class