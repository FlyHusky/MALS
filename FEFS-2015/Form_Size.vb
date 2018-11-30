''' <summary>
'''此模块用来计算所有的窗体及控件的位置和大小。。
''' </summary>
''' <remarks></remarks>
Module Form_Size
    Public Top_space As Integer  '窗体上部分留白
    Public left_right_space As Integer  '窗体左右两边留白
    Public down_space As Integer '窗体底部留白
    Public Scr_H As Integer   '屏幕高度
    Public Scr_W As Integer   '屏幕宽度
    Public Function Form_Size_Init() As Boolean
        Scr_H = Screen.PrimaryScreen.Bounds.Height
        Scr_W = Screen.PrimaryScreen.Bounds.Width
        Top_space = Scr_H * 0.0174
        left_right_space = Scr_W * 0.04
    End Function


End Module
