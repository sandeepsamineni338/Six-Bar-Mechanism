Module Module1
    Function GetGraphicsObject(ByVal PictureBox1 As PictureBox) As Graphics

        'dx and dy are the minimum width and height of the picture box
        Dim dx As Integer = 100
        Dim dy As Integer = 100

        If PictureBox1.Width > dx Then
            dx = PictureBox1.Width
        End If

        If PictureBox1.Height > dy Then
            dy = PictureBox1.Height
        End If

        'Create a bitnap object to be displayed in PictureBox
        Dim bmp As Bitmap
        bmp = New Bitmap(dx, dy)

        'Set the image property of the PictureBox to bitmap object
        PictureBox1.Image = bmp

        'Create a Grapjics object
        Dim G As Graphics
        G = Graphics.FromImage(bmp)

        'Return the Graphics Object
        Return G
    End Function
End Module
