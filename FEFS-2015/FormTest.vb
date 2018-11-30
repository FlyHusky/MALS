Public Class FormTest

    Dim m_items As New ArrayList

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Controls.Item(0).Text = "集合中第一个元素" '集合中第一个元素是button
        Me.Controls.Item(1).Text = "集合中第2个元素"
        Me.Controls.Item(2).Text = "集合中第3个元素"
        Me.Controls.Item(3).Text = "集合中第4个元素"

        MessageBox.Show(Me.Controls.Item(0).Name)  '集合

    End Sub







End Class


