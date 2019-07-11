Imports System.Drawing.Drawing2D
Imports System.Math
Public Class Form1
    Dim G As Graphics
    Dim m_InverseTransformation As Matrix
    Dim ClickPoints() As PointF = {New PointF(0.0F, 0.0F), New PointF(0.0F, 0.0F), New PointF(0.0F, 0.0F), New PointF(0.0F, 0.0F), New PointF(0.0F, 0.0F), New PointF(0.0F, 0.0F), New PointF(0.0F, 0.0F), New PointF(0.0F, 0.0F), New PointF(0.0F, 0.0F)}
    Dim ClickCount As Integer
    Dim Point_RB() As PointF = {New PointF(0.0F, 0.0F), New PointF(0.0F, 0.0F), New PointF(0.0F, 0.0F)}
    Dim Point_RA() As PointF = {New PointF(0.0F, 0.0F), New PointF(0.0F, 0.0F), New PointF(0.0F, 0.0F)}
    Dim Point_RD() As PointF = {New PointF(0.0F, 0.0F), New PointF(0.0F, 0.0F), New PointF(0.0F, 0.0F)}
    Dim Point_R() As PointF = {New PointF(0.0F, 0.0F), New PointF(0.0F, 0.0F), New PointF(0.0F, 0.0F), New PointF(0.0F, 0.0F), New PointF(0.0F, 0.0F), New PointF(0.0F, 0.0F), New PointF(0.0F, 0.0F), New PointF(0.0F, 0.0F)}
    Dim point_rs(8) As Double
    Dim Point_E6() As PointF = {New PointF(0.0F, 0.0F), New PointF(0.0F, 0.0F), New PointF(0.0F, 0.0F)}
    Dim Theta2a(100) As Double


    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        G = GetGraphicsObject(PictureBox1)
        CheckBox1.Checked = True
        RadioButton1.Checked = True
        MsgBox("Select Method & point P1 only")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Not (TextBox1.Text = Nothing Or
            TextBox2.Text = Nothing Or
            TextBox3.Text = Nothing Or
            TextBox4.Text = Nothing) Then
            SetScale(G, PictureBox1.Width, PictureBox1.Height, _
                CSng(TextBox1.Text), CSng(TextBox2.Text), CSng(TextBox3.Text), CSng(TextBox4.Text))
        Else
            MsgBox("Please fill the text boxes with xmin, xmax, ymin, ymax")
        End If

    End Sub

    Private Sub SetScale(ByVal G As Graphics, ByVal G_width _
       As Integer, ByVal G_height As Integer, ByVal left_x As _
       Single, ByVal right_x As Single, ByVal top_y As Single, _
       ByVal bottom_y As Single)
        ' Start from scratch.
        G.ResetTransform()

        ' Scale so the viewport's width and height
        ' map to the Graphics object's width and height.

        G.ScaleTransform( _
            G_width / (right_x - left_x), _
            G_height / (bottom_y - top_y))

        ' Translate (left_x, top_y) to the Graphics
        ' object's origin.
        G.TranslateTransform(-left_x, -top_y)

    End Sub

    Private Sub PictureBox1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove
        ' Apply the inverted transformation to the point.
        Dim ptfs() As PointF = {New PointF(e.X, e.Y)}

        If Not (TextBox1.Text = Nothing Or
            TextBox2.Text = Nothing Or
            TextBox3.Text = Nothing Or
            TextBox4.Text = Nothing) Then
            SetScale(G, PictureBox1.Width, PictureBox1.Height, _
                CSng(TextBox1.Text), CSng(TextBox2.Text), CSng(TextBox4.Text), CSng(TextBox3.Text))
            m_InverseTransformation = G.Transform
            m_InverseTransformation.Invert()
            m_InverseTransformation.TransformPoints(ptfs)
            ' Display the result.
            Label5.Text = "     (" & ptfs(0).X.ToString & "," & ptfs(0).Y.ToString & ")"

            Label5.Location = New Point(e.X + 10, e.Y + 10)

        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Public Sub PictureBox1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseClick

        Dim myBrush As New SolidBrush(Color.Blue)
        Dim myBrush2 As New SolidBrush(Color.Red)
        Dim myBrush3 As New SolidBrush(Color.Green)
        'Dim myPen As New Pen(Color.Blue, 0)
        Dim myPen As New Pen(Color.Blue, 1)
        Dim myPen2 As New Pen(Color.Green, 1)
        Dim myPen3 As New Pen(Color.Red, 1)

        G = GetGraphicsObject(PictureBox1)
        If Not (TextBox1.Text = Nothing Or
            TextBox2.Text = Nothing Or
            TextBox3.Text = Nothing Or
            TextBox4.Text = Nothing) Then
            SetScale(G, PictureBox1.Width, PictureBox1.Height, _
                CSng(TextBox1.Text), CSng(TextBox2.Text), CSng(TextBox4.Text), CSng(TextBox3.Text))
            Dim ptfs() As PointF = {New PointF(e.X, e.Y)}
            m_InverseTransformation = G.Transform
            m_InverseTransformation.Invert()
            m_InverseTransformation.TransformPoints(ptfs)

            If RadioButton1.Checked = True Then
                TextBox5.Text = ptfs(0).X.ToString
                TextBox6.Text = ptfs(0).Y.ToString
                ClickPoints(0) = New PointF(ptfs(0).X, ptfs(0).Y)
            ElseIf RadioButton2.Checked = True Then
                TextBox8.Text = ptfs(0).X.ToString
                TextBox9.Text = ptfs(0).Y.ToString
                ClickPoints(1) = New PointF(ptfs(0).X, ptfs(0).Y)
            ElseIf RadioButton3.Checked = True Then
                TextBox11.Text = ptfs(0).X.ToString
                TextBox12.Text = ptfs(0).Y.ToString
                ClickPoints(2) = New PointF(ptfs(0).X, ptfs(0).Y)
            ElseIf RadioButton4.Checked = True Then
                TextBox14.Text = ptfs(0).X.ToString
                TextBox15.Text = ptfs(0).Y.ToString
                ClickPoints(3) = New PointF(ptfs(0).X, ptfs(0).Y)
            ElseIf RadioButton5.Checked = True Then
                TextBox16.Text = ptfs(0).X.ToString
                TextBox17.Text = ptfs(0).Y.ToString
                ClickPoints(4) = New PointF(ptfs(0).X, ptfs(0).Y)
            ElseIf RadioButton6.Checked = True Then
                TextBox18.Text = ptfs(0).X.ToString
                TextBox19.Text = ptfs(0).Y.ToString
                ClickPoints(5) = New PointF(ptfs(0).X, ptfs(0).Y)
            ElseIf RadioButton7.Checked = True Then
                TextBox20.Text = ptfs(0).X.ToString
                TextBox21.Text = ptfs(0).Y.ToString
                ClickPoints(6) = New PointF(ptfs(0).X, ptfs(0).Y)
            ElseIf RadioButton13.Checked = True Then
                TextBox22.Text = ptfs(0).X.ToString
                TextBox23.Text = ptfs(0).Y.ToString
                ClickPoints(7) = New PointF(ptfs(0).X, ptfs(0).Y)
            ElseIf RadioButton14.Checked = True Then
                TextBox24.Text = ptfs(0).X.ToString
                TextBox25.Text = ptfs(0).Y.ToString
                ClickPoints(8) = New PointF(ptfs(0).X, ptfs(0).Y)
            End If
            'MsgBox("Select Method")
            ClickCount += 1
            If ClickCount = 1 Then
                RadioButton1.Checked = True
                TextBox5.Text = ptfs(0).X.ToString
                TextBox6.Text = ptfs(0).Y.ToString
                ClickPoints(0) = New PointF(ptfs(0).X, ptfs(0).Y)
                G.FillEllipse(myBrush, New Rectangle(ClickPoints(0).X, ClickPoints(0).Y, 0.6, 2))
                'G.DrawLine(myPen, ptfs(0).X, ptfs(0).Y, ptfs(0).X + 2, ptfs(0).Y)
                'G.DrawLine(myPen, ptfs(0).X, ptfs(0).Y, ptfs(0).X, ptfs(0).Y + 3)
                RadioButton2.Checked = True

            ElseIf ClickCount = 2 Then
                TextBox8.Text = ptfs(0).X.ToString
                TextBox9.Text = ptfs(0).Y.ToString
                ClickPoints(1) = New PointF(ptfs(0).X, ptfs(0).Y)
                RadioButton2.Checked = True
                G.FillEllipse(myBrush, New Rectangle(ClickPoints(0).X, ClickPoints(0).Y, 1, 2))
                G.FillEllipse(myBrush2, New Rectangle(ClickPoints(1).X, ClickPoints(1).Y, 1, 2))
                RadioButton3.Checked = True

            ElseIf ClickCount = 3 Then
                TextBox11.Text = ptfs(0).X.ToString
                TextBox12.Text = ptfs(0).Y.ToString
                ClickPoints(2) = New PointF(ptfs(0).X, ptfs(0).Y)
                G.FillEllipse(myBrush, New Rectangle(ClickPoints(0).X, ClickPoints(0).Y, 1, 2))
                G.FillEllipse(myBrush2, New Rectangle(ClickPoints(1).X, ClickPoints(1).Y, 1, 2))
                G.FillEllipse(myBrush3, New Rectangle(ClickPoints(2).X, ClickPoints(2).Y, 1, 2))
                RadioButton3.Checked = False
                CheckBox1.Checked = False
                If RadioButton8.Checked = True Then
                    RadioButton9.Checked = False
                    CheckBox2.Checked = True
                    RadioButton4.Checked = True
                ElseIf RadioButton9.Checked = True Then
                    RadioButton8.Checked = False
                    CheckBox3.Checked = True
                    RadioButton6.Checked = True
                    ClickCount = 5
                End If

            ElseIf ClickCount = 4 Then
                TextBox14.Text = ptfs(0).X.ToString
                TextBox15.Text = ptfs(0).Y.ToString
                ClickPoints(3) = New PointF(ptfs(0).X, ptfs(0).Y)
                G.FillEllipse(myBrush, New Rectangle(ClickPoints(0).X, ClickPoints(0).Y, 1, 2))
                G.FillEllipse(myBrush2, New Rectangle(ClickPoints(1).X, ClickPoints(1).Y, 1, 2))
                G.FillEllipse(myBrush3, New Rectangle(ClickPoints(2).X, ClickPoints(2).Y, 1, 2))
                OA_Fixed_Method()
                RadioButton5.Checked = True

            ElseIf ClickCount = 5 Then
                    'RadioButton5.Checked = False
                TextBox16.Text = ptfs(0).X.ToString
                TextBox17.Text = ptfs(0).Y.ToString
                ClickPoints(4) = New PointF(ptfs(0).X, ptfs(0).Y)
                G.FillEllipse(myBrush, New Rectangle(ClickPoints(0).X, ClickPoints(0).Y, 1, 2))
                G.FillEllipse(myBrush2, New Rectangle(ClickPoints(1).X, ClickPoints(1).Y, 1, 2))
                G.FillEllipse(myBrush3, New Rectangle(ClickPoints(2).X, ClickPoints(2).Y, 1, 2))
                OB_Fixed_Method()
                CheckBox2.Checked = False
                G.DrawLine(myPen, ClickPoints(4), ClickPoints(6))
                    'CheckBox3.Checked = True
                    'RadioButton6.Checked = True

            ElseIf ClickCount = 6 Then
                CheckBox3.Checked = True
                RadioButton6.Checked = True
                TextBox18.Text = ptfs(0).X.ToString
                TextBox19.Text = ptfs(0).Y.ToString
                ClickPoints(5) = New PointF(ptfs(0).X, ptfs(0).Y)
                    'G.DrawLine(myPen, ClickPoints(3), ClickPoints(4))
                A_Moving_pivot()
                    'G.DrawLine(myPen, ClickPoints(3), ClickPoints(5))
                G.FillEllipse(myBrush, New Rectangle(ClickPoints(0).X, ClickPoints(0).Y, 1, 2))
                G.FillEllipse(myBrush2, New Rectangle(ClickPoints(1).X, ClickPoints(1).Y, 1, 2))
                G.FillEllipse(myBrush3, New Rectangle(ClickPoints(2).X, ClickPoints(2).Y, 1, 2))
                RadioButton7.Checked = True

            ElseIf ClickCount = 7 Then
                TextBox20.Text = ptfs(0).X.ToString
                TextBox21.Text = ptfs(0).Y.ToString
                ClickPoints(6) = New PointF(ptfs(0).X, ptfs(0).Y)
                B_Moving_pivot()
                G.DrawLine(myPen, ClickPoints(3), ClickPoints(5))
                G.DrawLine(myPen, ClickPoints(5), ClickPoints(6))
                G.DrawLine(myPen, ClickPoints(6), ClickPoints(4))
                G.FillEllipse(myBrush, New Rectangle(ClickPoints(0).X, ClickPoints(0).Y, 1, 2))
                G.FillEllipse(myBrush2, New Rectangle(ClickPoints(1).X, ClickPoints(1).Y, 1, 2))
                G.FillEllipse(myBrush3, New Rectangle(ClickPoints(2).X, ClickPoints(2).Y, 1, 2))
                RadioButton7.Checked = False
                RadioButton13.Checked = True

            ElseIf ClickCount = 8 Then
                TextBox22.Text = ptfs(0).X.ToString
                TextBox23.Text = ptfs(0).Y.ToString
                ClickPoints(7) = New PointF(ptfs(0).X, ptfs(0).Y)
                ED_lastdial()
                G.DrawLine(myPen, ClickPoints(3), ClickPoints(5))
                G.DrawLine(myPen, ClickPoints(5), ClickPoints(6))
                G.DrawLine(myPen, ClickPoints(4), ClickPoints(6))
                G.DrawLine(myPen2, ClickPoints(8), ClickPoints(4))
                G.DrawLine(myPen2, ClickPoints(8), ClickPoints(6))
                G.DrawLine(myPen3, ClickPoints(0), ClickPoints(6))
                G.DrawLine(myPen2, ClickPoints(7), ClickPoints(8))
                G.DrawLine(myPen3, ClickPoints(5), ClickPoints(0))
                G.FillEllipse(myBrush, New Rectangle(ClickPoints(0).X, ClickPoints(0).Y, 1, 2))
                G.FillEllipse(myBrush2, New Rectangle(ClickPoints(1).X, ClickPoints(1).Y, 1, 2))
                G.FillEllipse(myBrush3, New Rectangle(ClickPoints(2).X, ClickPoints(2).Y, 1, 2))
                Analysis()

            ElseIf ClickCount > 8 Then
                'G.DrawLine(myPen, ClickPoints(3), ClickPoints(5))
                'G.DrawLine(myPen, ClickPoints(5), ClickPoints(6))
                'G.DrawLine(myPen, ClickPoints(4), ClickPoints(6))
                'G.DrawLine(myPen, ClickPoints(8), ClickPoints(4))
                'G.DrawLine(myPen, ClickPoints(8), ClickPoints(6))
                'G.DrawLine(myPen, ClickPoints(0), ClickPoints(6))
                'G.DrawLine(myPen, ClickPoints(7), ClickPoints(8))
                'G.DrawLine(myPen, ClickPoints(5), ClickPoints(0))
                G.DrawLine(myPen, ClickPoints(3), ClickPoints(5))
                G.DrawLine(myPen, ClickPoints(5), ClickPoints(6))
                G.DrawLine(myPen, ClickPoints(4), ClickPoints(6))
                G.DrawLine(myPen2, ClickPoints(8), ClickPoints(4))
                G.DrawLine(myPen2, ClickPoints(8), ClickPoints(6))
                G.DrawLine(myPen3, ClickPoints(0), ClickPoints(6))
                G.DrawLine(myPen2, ClickPoints(7), ClickPoints(8))
                G.DrawLine(myPen3, ClickPoints(5), ClickPoints(0))
                G.FillEllipse(myBrush, New Rectangle(ClickPoints(0).X, ClickPoints(0).Y, 1, 2))
                G.FillEllipse(myBrush2, New Rectangle(ClickPoints(1).X, ClickPoints(1).Y, 1, 2))
                G.FillEllipse(myBrush3, New Rectangle(ClickPoints(2).X, ClickPoints(2).Y, 1, 2))

            End If
        End If

    End Sub

    Private Sub PictureBox1_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel

        If RadioButton3.Checked = True Then
            If e.Delta > 0 Then
                TextBox13.Text += e.Delta / 120

            ElseIf e.Delta < 0 Then
                TextBox13.Text -= -e.Delta / 120
            End If
        ElseIf RadioButton2.Checked = True Then
            If e.Delta > 0 Then
                TextBox10.Text += e.Delta / 120

            ElseIf e.Delta < 0 Then
                TextBox10.Text -= -e.Delta / 120
            End If
        ElseIf RadioButton1.Checked = True Then
            If e.Delta > 0 Then
                TextBox7.Text += e.Delta / 120

            ElseIf e.Delta < 0 Then
                TextBox7.Text -= -e.Delta / 120
            End If

        End If
    End Sub

    Private Sub TextBox7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged

        If TextBox7.Text > 359 Then
            TextBox7.Text = 0
        ElseIf TextBox7.Text < -359 Then
            TextBox7.Text = 0
        End If

    End Sub

    Private Sub TextBox10_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox10.TextChanged


        If TextBox10.Text > 359 Then
            TextBox10.Text = 0
        ElseIf TextBox10.Text < -359 Then
            TextBox10.Text = 0
        End If

    End Sub

    Private Sub TextBox13_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox13.TextChanged

        If TextBox13.Text > 359 Then
            TextBox13.Text = 0
        ElseIf TextBox13.Text < -359 Then
            TextBox13.Text = 0
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        G = GetGraphicsObject(PictureBox1)
        If Not (TextBox1.Text = Nothing Or
            TextBox2.Text = Nothing Or
            TextBox3.Text = Nothing Or
            TextBox4.Text = Nothing) Then
            SetScale(G, PictureBox1.Width, PictureBox1.Height, _
                CSng(TextBox1.Text), CSng(TextBox2.Text), CSng(TextBox4.Text), CSng(TextBox3.Text))

            m_InverseTransformation = G.Transform
            m_InverseTransformation.Invert()

            Dim myBrush As New SolidBrush(Color.Blue)
            Dim myBrush2 As New SolidBrush(Color.Red)
            Dim myBrush3 As New SolidBrush(Color.Green)
            Dim myPen As New Pen(Color.Blue, 0)
            ClickPoints(0).X = CDbl(TextBox5.Text)
            ClickPoints(0).Y = CDbl(TextBox6.Text)
            ClickPoints(1).X = CDbl(TextBox8.Text)
            ClickPoints(1).Y = CDbl(TextBox9.Text)
            ClickPoints(2).X = CDbl(TextBox11.Text)
            ClickPoints(2).Y = CDbl(TextBox12.Text)
            ClickPoints(3).X = CDbl(TextBox14.Text)
            ClickPoints(3).Y = CDbl(TextBox15.Text)
            ClickPoints(4).X = CDbl(TextBox16.Text)
            ClickPoints(4).Y = CDbl(TextBox17.Text)
            ClickPoints(5).X = CDbl(TextBox18.Text)
            ClickPoints(5).Y = CDbl(TextBox19.Text)
            ClickPoints(6).X = CDbl(TextBox20.Text)
            ClickPoints(6).Y = CDbl(TextBox21.Text)
            G.DrawLine(myPen, ClickPoints(3), ClickPoints(5))
            G.DrawLine(myPen, ClickPoints(5), ClickPoints(6))
            G.DrawLine(myPen, ClickPoints(4), ClickPoints(6))
            G.FillEllipse(myBrush, New Rectangle(ClickPoints(0).X, ClickPoints(0).Y, 1, 2))
            G.FillEllipse(myBrush2, New Rectangle(ClickPoints(1).X, ClickPoints(1).Y, 1, 2))
            G.FillEllipse(myBrush3, New Rectangle(ClickPoints(2).X, ClickPoints(2).Y, 1, 2))
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            RadioButton1.Checked = True
            CheckBox2.Checked = False
            CheckBox3.Checked = False
            RadioButton4.Checked = False
            RadioButton5.Checked = False
            RadioButton6.Checked = False
            RadioButton7.Checked = False
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            RadioButton4.Checked = True Or
            CheckBox1.Checked = False
            CheckBox3.Checked = False
            RadioButton1.Checked = False
            RadioButton2.Checked = False
            RadioButton3.Checked = False
            RadioButton6.Checked = False
            RadioButton7.Checked = False
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            RadioButton6.Checked = True
            CheckBox1.Checked = False
            CheckBox2.Checked = False
            RadioButton1.Checked = False
            RadioButton2.Checked = False
            RadioButton3.Checked = False
            RadioButton4.Checked = False
            RadioButton5.Checked = False
        End If
    End Sub

    Private Sub RadioButton4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton4.CheckedChanged
        If RadioButton4.Checked = True Then
            CheckBox2.Checked = True
            CheckBox1.Checked = False
            CheckBox3.Checked = False
            RadioButton1.Checked = False
            RadioButton2.Checked = False
            RadioButton3.Checked = False
            RadioButton6.Checked = False
            RadioButton7.Checked = False
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            CheckBox1.Checked = True
            CheckBox2.Checked = False
            CheckBox3.Checked = False
            RadioButton4.Checked = False
            RadioButton5.Checked = False
            RadioButton6.Checked = False
            RadioButton7.Checked = False
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            CheckBox1.Checked = True
            CheckBox2.Checked = False
            CheckBox3.Checked = False
            RadioButton4.Checked = False
            RadioButton5.Checked = False
            RadioButton6.Checked = False
            RadioButton7.Checked = False
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = True Then
            CheckBox1.Checked = True
            CheckBox2.Checked = False
            CheckBox3.Checked = False
            RadioButton4.Checked = False
            RadioButton5.Checked = False
            RadioButton6.Checked = False
            RadioButton7.Checked = False
        End If
    End Sub

    Private Sub RadioButton5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton5.CheckedChanged
        If RadioButton5.Checked = True Then
            CheckBox2.Checked = True
            CheckBox1.Checked = False
            CheckBox3.Checked = False
            RadioButton1.Checked = False
            RadioButton2.Checked = False
            RadioButton3.Checked = False
            RadioButton6.Checked = False
            RadioButton7.Checked = False
        End If
    End Sub

    Private Sub RadioButton6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton6.CheckedChanged
        If RadioButton6.Checked = True Then
            CheckBox3.Checked = True
            CheckBox1.Checked = False
            CheckBox2.Checked = False
            RadioButton1.Checked = False
            RadioButton2.Checked = False
            RadioButton3.Checked = False
            RadioButton4.Checked = False
            RadioButton5.Checked = False
        End If
    End Sub

    Private Sub RadioButton7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton7.CheckedChanged
        If RadioButton7.Checked = True Then
            CheckBox3.Checked = True
            CheckBox1.Checked = False
            CheckBox2.Checked = False
            RadioButton1.Checked = False
            RadioButton2.Checked = False
            RadioButton3.Checked = False
            RadioButton4.Checked = False
            RadioButton5.Checked = False
        End If
    End Sub

    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        G = GetGraphicsObject(PictureBox1)
        If Not (TextBox1.Text = Nothing Or
            TextBox2.Text = Nothing Or
            TextBox3.Text = Nothing Or
            TextBox4.Text = Nothing) Then
            SetScale(G, PictureBox1.Width, PictureBox1.Height, _
                CSng(TextBox1.Text), CSng(TextBox2.Text), CSng(TextBox4.Text), CSng(TextBox3.Text))
            m_InverseTransformation = G.Transform
            m_InverseTransformation.Invert()

            Dim myBrush As New SolidBrush(Color.Blue)
            Dim myBrush2 As New SolidBrush(Color.Red)
            Dim myBrush3 As New SolidBrush(Color.Green)
            Dim myPen As New Pen(Color.Blue, 0)
            G.DrawLine(myPen, ClickPoints(3), ClickPoints(5))
            G.DrawLine(myPen, ClickPoints(5), ClickPoints(6))
            G.DrawLine(myPen, ClickPoints(4), ClickPoints(6))
            G.FillEllipse(myBrush, New Rectangle(ClickPoints(0).X, ClickPoints(0).Y, 1, 1))
            G.FillEllipse(myBrush2, New Rectangle(ClickPoints(1).X, ClickPoints(1).Y, 1, 2))
            G.FillEllipse(myBrush3, New Rectangle(ClickPoints(2).X, ClickPoints(2).Y, 1, 2))
        End If
    End Sub
    Private Sub OA_Fixed_Method()
        Dim myPen As New Pen(Color.Blue, 1)

        Dim rp1x = TextBox5.Text()
        Dim rp1y = TextBox6.Text()
        Dim rp2x = TextBox8.Text()
        Dim rp2y = TextBox9.Text()
        Dim rp3x = TextBox11.Text()
        Dim rp3y = TextBox12.Text()

        Dim xroa1 As Double = (Cos(CDbl(TextBox7.Text) * PI / 180) * (CDbl(TextBox14.Text) - rp1x)) + (Sin(CDbl(TextBox7.Text) * PI / 180) * (CDbl(TextBox15.Text) - rp1y))
        Dim yroa1 As Double = (-Sin(CDbl(TextBox7.Text) * PI / 180) * (CDbl(TextBox14.Text) - rp1x)) + (Cos(CDbl(TextBox7.Text) * PI / 180) * (CDbl(TextBox15.Text) - rp1y))
        Dim xroa2 As Double = (Cos(CDbl(TextBox10.Text) * PI / 180) * (CDbl(TextBox14.Text) - rp2x)) + (Sin(CDbl(TextBox10.Text) * PI / 180) * (CDbl(TextBox15.Text) - rp2y))
        Dim yroa2 As Double = (-Sin(CDbl(TextBox10.Text) * PI / 180) * (CDbl(TextBox14.Text) - rp2x)) + (Cos(CDbl(TextBox10.Text) * PI / 180) * (CDbl(TextBox15.Text) - rp2y))
        Dim xroa3 As Double = (Cos(CDbl(TextBox13.Text) * PI / 180) * (CDbl(TextBox14.Text) - rp3x)) + (Sin(CDbl(TextBox13.Text) * PI / 180) * (CDbl(TextBox15.Text) - rp3y))
        Dim yroa3 As Double = (-Sin(CDbl(TextBox13.Text) * PI / 180) * (CDbl(TextBox14.Text) - rp3x)) + (Cos(CDbl(TextBox13.Text) * PI / 180) * (CDbl(TextBox15.Text) - rp3y))


        Dim A As Double = 2 * (xroa2 - xroa1)
        Dim B As Double = 2 * (yroa2 - yroa1)
        Dim C As Double = ((xroa2) ^ 2) - ((xroa1) ^ 2) + ((yroa2) ^ 2) - ((yroa1) ^ 2)

        Dim D As Double = 2 * (xroa3 - xroa2)
        Dim E As Double = 2 * (yroa3 - yroa2)
        Dim F As Double = ((xroa3) ^ 2) - ((xroa2) ^ 2) + ((yroa3) ^ 2) - ((yroa2) ^ 2)

        Dim xra = (C - ((B * F) / E)) / (A - ((B * D) / E))
        Dim yra = (C - ((A * F) / D)) / (B - ((A * E) / D))

        Dim xRa1 = rp1x + ((Cos((CDbl(TextBox7.Text)) * PI / 180.0)) * (xra)) - ((Sin((CDbl(TextBox7.Text)) * PI / 180.0)) * (yra))
        Dim yRa1 = rp1y + ((Sin((CDbl(TextBox7.Text)) * PI / 180.0)) * (xra)) + ((Cos((CDbl(TextBox7.Text)) * PI / 180.0)) * (yra))

        Dim xRa2 = rp2x + ((Cos((CDbl(TextBox10.Text)) * PI / 180.0)) * (xra)) - ((Sin((CDbl(TextBox10.Text)) * PI / 180.0)) * (yra))
        Dim yRa2 = rp2y + ((Sin((CDbl(TextBox10.Text)) * PI / 180.0)) * (xra)) + ((Cos((CDbl(TextBox10.Text)) * PI / 180.0)) * (yra))

        Dim xRa3 = rp3x + ((Cos((CDbl(TextBox13.Text)) * PI / 180.0)) * (xra)) - ((Sin((CDbl(TextBox13.Text)) * PI / 180.0)) * (yra))
        Dim yRa3 = rp3y + ((Sin((CDbl(TextBox13.Text)) * PI / 180.0)) * (xra)) + ((Cos((CDbl(TextBox13.Text)) * PI / 180.0)) * (yra))
        Point_RA(0) = New PointF(xRa1, yRa1)
        Point_RA(1) = New PointF(xRa2, yRa2)
        Point_RA(2) = New PointF(xRa3, yRa3)

        If RadioButton4.Checked = True Then
            If RadioButton10.Checked = True Then
                TextBox18.Text = xRa1
                TextBox19.Text = yRa1
                ClickPoints(5).X = CDbl(TextBox18.Text)
                ClickPoints(5).Y = CDbl(TextBox19.Text)
            ElseIf RadioButton11.Checked = True Then
                TextBox18.Text = xRa2
                TextBox19.Text = yRa2
                ClickPoints(5).X = CDbl(TextBox18.Text)
                ClickPoints(5).Y = CDbl(TextBox19.Text)
            ElseIf RadioButton12.Checked = True Then
                TextBox18.Text = xRa3
                TextBox19.Text = yRa3
                ClickPoints(5).X = CDbl(TextBox18.Text)
                ClickPoints(5).Y = CDbl(TextBox19.Text)
            End If
        End If
        ClickPoints(5).X = CDbl(TextBox18.Text)
        ClickPoints(5).Y = CDbl(TextBox19.Text)

        G.DrawLine(myPen, ClickPoints(3), ClickPoints(5))

    End Sub
    Private Sub OB_Fixed_Method()

        Dim myPen As New Pen(Color.Blue, 1)

        Dim rp1x = TextBox5.Text()
        Dim rp1y = TextBox6.Text()
        Dim rp2x = TextBox8.Text()
        Dim rp2y = TextBox9.Text()
        Dim rp3x = TextBox11.Text()
        Dim rp3y = TextBox12.Text()

        Dim xrob1 As Double = (Cos(CDbl(TextBox7.Text) * PI / 180) * (CDbl(TextBox16.Text) - rp1x)) + (Sin(CDbl(TextBox7.Text) * PI / 180) * (CDbl(TextBox17.Text) - rp1y))
        Dim yrob1 As Double = (-Sin(CDbl(TextBox7.Text) * PI / 180) * (CDbl(TextBox16.Text) - rp1x)) + (Cos(CDbl(TextBox7.Text) * PI / 180) * (CDbl(TextBox17.Text) - rp1y))
        Dim xrob2 As Double = (Cos(CDbl(TextBox10.Text) * PI / 180) * (CDbl(TextBox16.Text) - rp2x)) + (Sin(CDbl(TextBox10.Text) * PI / 180) * (CDbl(TextBox17.Text) - rp2y))
        Dim yrob2 As Double = (-Sin(CDbl(TextBox10.Text) * PI / 180) * (CDbl(TextBox16.Text) - rp2x)) + (Cos(CDbl(TextBox10.Text) * PI / 180) * (CDbl(TextBox17.Text) - rp2y))
        Dim xrob3 As Double = (Cos(CDbl(TextBox13.Text) * PI / 180) * (CDbl(TextBox16.Text) - rp3x)) + (Sin(CDbl(TextBox13.Text) * PI / 180) * (CDbl(TextBox17.Text) - rp3y))
        Dim yrob3 As Double = (-Sin(CDbl(TextBox13.Text) * PI / 180) * (CDbl(TextBox16.Text) - rp3x)) + (Cos(CDbl(TextBox13.Text) * PI / 180) * (CDbl(TextBox17.Text) - rp3y))


        Dim A As Double = 2 * (xrob2 - xrob1)
        Dim B As Double = 2 * (yrob2 - yrob1)
        Dim C As Double = ((xrob2) ^ 2) - ((xrob1) ^ 2) + ((yrob2) ^ 2) - ((yrob1) ^ 2)

        Dim D As Double = 2 * (xrob3 - xrob2)
        Dim E As Double = 2 * (yrob3 - yrob2)
        Dim F As Double = ((xrob3) ^ 2) - ((xrob2) ^ 2) + ((yrob3) ^ 2) - ((yrob2) ^ 2)

        Dim xrb = (C - ((B * F) / E)) / (A - ((B * D) / E))
        Dim yrb = (C - ((A * F) / D)) / (B - ((A * E) / D))

        Dim xRb1 = rp1x + ((Cos((CDbl(TextBox7.Text)) * PI / 180.0)) * (xrb)) - ((Sin((CDbl(TextBox7.Text)) * PI / 180.0)) * (yrb))
        Dim yRb1 = rp1y + ((Sin((CDbl(TextBox7.Text)) * PI / 180.0)) * (xrb)) + ((Cos((CDbl(TextBox7.Text)) * PI / 180.0)) * (yrb))

        Dim xRb2 = rp2x + ((Cos((CDbl(TextBox10.Text)) * PI / 180.0)) * (xrb)) - ((Sin((CDbl(TextBox10.Text)) * PI / 180.0)) * (yrb))
        Dim yRb2 = rp2y + ((Sin((CDbl(TextBox10.Text)) * PI / 180.0)) * (xrb)) + ((Cos((CDbl(TextBox10.Text)) * PI / 180.0)) * (yrb))

        Dim xRb3 = rp3x + ((Cos((CDbl(TextBox13.Text)) * PI / 180.0)) * (xrb)) - ((Sin((CDbl(TextBox13.Text)) * PI / 180.0)) * (yrb))
        Dim yRb3 = rp3y + ((Sin((CDbl(TextBox13.Text)) * PI / 180.0)) * (xrb)) + ((Cos((CDbl(TextBox13.Text)) * PI / 180.0)) * (yrb))
        Point_RB(0) = New PointF(xRb1, yRb1)
        Point_RB(1) = New PointF(xRb2, yRb2)
        Point_RB(2) = New PointF(xRb3, yRb3)

        If RadioButton5.Checked = True Then
            If RadioButton10.Checked = True Then
                TextBox20.Text = xRb1
                TextBox21.Text = yRb1
                ClickPoints(6).X = CDbl(TextBox20.Text)
                ClickPoints(6).Y = CDbl(TextBox21.Text)
            ElseIf RadioButton11.Checked = True Then
                TextBox26.Text = xRb2
                TextBox27.Text = yRb2
                ClickPoints(6).X = CDbl(TextBox20.Text)
                ClickPoints(6).Y = CDbl(TextBox21.Text)
            ElseIf RadioButton12.Checked = True Then
                TextBox28.Text = xRb3
                TextBox29.Text = yRb3
                ClickPoints(6).X = CDbl(TextBox20.Text)
                ClickPoints(6).Y = CDbl(TextBox21.Text)
            End If

        End If
        TextBox26.Text = xRb2
        TextBox27.Text = yRb2
        TextBox28.Text = xRb3
        TextBox29.Text = yRb3
        ClickPoints(6).X = CDbl(TextBox20.Text)
        ClickPoints(6).Y = CDbl(TextBox21.Text)

        G.DrawLine(myPen, ClickPoints(4), ClickPoints(6))
        G.DrawLine(myPen, ClickPoints(3), ClickPoints(5))
        G.DrawLine(myPen, ClickPoints(5), ClickPoints(6))
        RadioButton5.Checked = False
        RadioButton13.Checked() = True

        ClickCount = 7

    End Sub


    Private Sub A_Moving_pivot()


        Dim myPen As New Pen(Color.Blue, 1)

        Dim rp1x = TextBox5.Text()
        Dim rp1y = TextBox6.Text()
        Dim rp2x = TextBox8.Text()
        Dim rp2y = TextBox9.Text()
        Dim rp3x = TextBox11.Text()
        Dim rp3y = TextBox12.Text()
        Dim Rxa1 = TextBox18.Text()
        Dim Rya1 = TextBox19.Text()

        Dim xra As Double = ((Cos((CDbl(TextBox7.Text) * PI) / 180)) * (Rxa1 - rp1x)) + ((Sin((CDbl(TextBox7.Text) * PI) / 180)) * (Rya1 - rp1y))
        Dim yra As Double = ((-Sin((CDbl(TextBox7.Text) * PI) / 180)) * (Rxa1 - rp1x)) + ((Cos((CDbl(TextBox7.Text) * PI) / 180)) * (Rya1 - rp1y))

        Dim Rxa2 As Double = rp2x + (Cos((CDbl(TextBox10.Text) * PI) / 180) * xra) - (Sin((CDbl(TextBox10.Text) * PI) / 180) * yra)
        Dim Rya2 As Double = rp2y + (Sin((CDbl(TextBox10.Text) * PI) / 180) * xra) + (Cos((CDbl(TextBox10.Text) * PI) / 180) * yra)
        Dim Rxa3 As Double = rp3x + (Cos((CDbl(TextBox13.Text) * PI) / 180) * xra) - (Sin((CDbl(TextBox13.Text) * PI) / 180) * yra)
        Dim Rya3 As Double = rp3y + (Sin((CDbl(TextBox13.Text) * PI) / 180) * xra) + (Cos((CDbl(TextBox13.Text) * PI) / 180) * yra)
        Point_RA(0) = New PointF(Rxa1, Rya1)
        Point_RA(1) = New PointF(Rxa2, Rya2)
        Point_RA(2) = New PointF(Rxa3, Rya3)


        Dim A As Double = 2 * (Rxa2 - Rxa1)
        Dim B As Double = 2 * (Rya2 - Rya1)
        Dim C As Double = ((Rxa2) ^ 2) - ((Rxa1) ^ 2) + ((Rya2) ^ 2) - ((Rya1) ^ 2)

        Dim D As Double = 2 * (Rxa3 - Rxa2)
        Dim E As Double = 2 * (Rya3 - Rya2)
        Dim F As Double = ((Rxa3) ^ 2) - ((Rxa2) ^ 2) + ((Rya3) ^ 2) - ((Rya2) ^ 2)

        Dim Roax = (C - ((B * F) / E)) / (A - ((B * D) / E))
        Dim Roay = (C - ((A * F) / D)) / (B - ((A * E) / D))

        If RadioButton6.Checked = True Then
            TextBox14.Text = Roax
            TextBox15.Text = Roay
        End If

        ClickPoints(3).X = CDbl(TextBox14.Text)
        ClickPoints(3).Y = CDbl(TextBox15.Text)

        G.DrawLine(myPen, ClickPoints(3), ClickPoints(5))

    End Sub
    Private Sub B_Moving_pivot()


        Dim myPen As New Pen(Color.Blue, 1)

        Dim rp1x = TextBox5.Text()
        Dim rp1y = TextBox6.Text()
        Dim rp2x = TextBox8.Text()
        Dim rp2y = TextBox9.Text()
        Dim rp3x = TextBox11.Text()
        Dim rp3y = TextBox12.Text()
        Dim Rxb1 = TextBox20.Text()
        Dim Ryb1 = TextBox21.Text()

        Dim xrb As Double = ((Cos((CDbl(TextBox7.Text) * PI) / 180)) * (Rxb1 - rp1x)) + ((Sin((CDbl(TextBox7.Text) * PI) / 180)) * (Ryb1 - rp1y))
        Dim yrb As Double = ((-Sin((CDbl(TextBox7.Text) * PI) / 180)) * (Rxb1 - rp1x)) + ((Cos((CDbl(TextBox7.Text) * PI) / 180)) * (Ryb1 - rp1y))

        Dim Rxb2 As Double = rp2x + (Cos((CDbl(TextBox10.Text) * PI) / 180) * xrb) - (Sin((CDbl(TextBox10.Text) * PI) / 180) * yrb)
        Dim Ryb2 As Double = rp2y + (Sin((CDbl(TextBox10.Text) * PI) / 180) * xrb) + (Cos((CDbl(TextBox10.Text) * PI) / 180) * yrb)
        Dim Rxb3 As Double = rp3x + (Cos((CDbl(TextBox13.Text) * PI) / 180) * xrb) - (Sin((CDbl(TextBox13.Text) * PI) / 180) * yrb)
        Dim Ryb3 As Double = rp3y + (Sin((CDbl(TextBox13.Text) * PI) / 180) * xrb) + (Cos((CDbl(TextBox13.Text) * PI) / 180) * yrb)

        TextBox26.Text = Rxb2
        TextBox27.Text = Ryb2
        TextBox28.Text = Rxb3
        TextBox29.Text = Ryb3
        Point_RB(0) = New PointF(Rxb1, Ryb1)
        Point_RB(1) = New PointF(Rxb2, Ryb2)
        Point_RB(2) = New PointF(Rxb3, Ryb3)

        Dim A As Double = 2 * (Rxb2 - Rxb1)
        Dim B As Double = 2 * (Ryb2 - Ryb1)
        Dim C As Double = ((Rxb2) ^ 2) - ((Rxb1) ^ 2) + ((Ryb2) ^ 2) - ((Ryb1) ^ 2)

        Dim D As Double = 2 * (Rxb3 - Rxb2)
        Dim E As Double = 2 * (Ryb3 - Ryb2)
        Dim F As Double = ((Rxb3) ^ 2) - ((Rxb2) ^ 2) + ((Ryb3) ^ 2) - ((Ryb2) ^ 2)

        Dim Robx = (C - ((B * F) / E)) / (A - ((B * D) / E))
        Dim Roby = (C - ((A * F) / D)) / (B - ((A * E) / D))

        If RadioButton7.Checked = True Then
            TextBox16.Text = Robx
            TextBox17.Text = Roby
        End If

        ClickPoints(4).X = CDbl(TextBox16.Text)
        ClickPoints(4).Y = CDbl(TextBox17.Text)

        'G.DrawLine(myPen, ClickPoints(3), ClickPoints(5))


    End Sub

    'Private Sub RadioButton10_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton10.CheckedChanged
    '    Dim myPen As New Pen(Color.Blue, 0)
    '    If RadioButton10.Checked = True Then
    '        TextBox20.Text = Point_B(0).X
    '        TextBox21.Text = Point_B(0).Y
    '        TextBox18.Text = Point_A(0).X
    '        TextBox19.Text = Point_A(0).Y

    '        'G.DrawLine(myPen, Point_B(0), ClickPoints(4))
    '        'G.DrawLine(myPen, ClickPoints(3), ClickPoints(5))
    '        'G.DrawLine(myPen, ClickPoints(5), ClickPoints(6))
    '    End If
    'End Sub

    'Private Sub RadioButton11_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton11.CheckedChanged
    '    If RadioButton11.Checked = True Then
    '        TextBox20.Text = Point_B(1).X
    '        TextBox21.Text = Point_B(1).Y
    '        TextBox18.Text = Point_A(1).X
    '        TextBox19.Text = Point_A(1).Y
    '    End If
    'End Sub

    'Private Sub RadioButton12_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton12.CheckedChanged
    '    If RadioButton12.Checked = True Then
    '        TextBox20.Text = Point_B(2).X
    '        TextBox21.Text = Point_B(2).Y
    '        TextBox18.Text = Point_A(2).X
    '        TextBox19.Text = Point_A(2).Y
    '    End If
    'End Sub
    'Private Sub DE_lastdial()

    '    Dim rp1x = TextBox5.Text()
    '    Dim rp1y = TextBox6.Text()
    '    Dim rp2x = TextBox8.Text()
    '    Dim rp2y = TextBox9.Text()
    '    Dim rp3x = TextBox11.Text()
    '    Dim rp3y = TextBox12.Text()
    '    Dim Robx = TextBox16.Text()
    '    Dim Roby = TextBox17.Text()
    '    Dim Rb1x = TextBox20.Text()
    '    Dim Rb1y = TextBox21.Text()
    '    Dim Rb2x = TextBox26.Text()
    '    Dim Rb2y = TextBox27.Text()
    '    Dim Rb3x = TextBox28.Text()
    '    Dim Rb3y = TextBox29.Text()
    '    Dim xRD1 = TextBox22.Text()
    '    Dim yRD1 = TextBox23.Text()

    '    Dim x1 As Double = Rb1x - Robx
    '    Dim x2 As Double = Rb2x - Robx
    '    Dim x3 As Double = Rb3x - Robx
    '    Dim y1 As Double = Rb1y - Roby
    '    Dim y2 As Double = Rb2y - Roby
    '    Dim y3 As Double = Rb3y - Roby

    '    Dim theta1 As Double = Atan2(y1, x1)
    '    Dim theta2 As Double = Atan2(y2, x2)
    '    Dim theta3 As Double = Atan2(y3, x3)

    '    Dim xrd4 As Double = (Cos(theta1) * (xRD1 - Robx)) + (Sin(theta1) * (yRD1 - Roby))
    '    Dim yrd4 As Double = (-Sin(theta1) * (xRD1 - Robx)) + (Cos(theta1) * (yRD1 - Roby))

    '    Dim r1xd6 As Double = (Robx - rp1x) + (Cos(theta1) * xrd4) - (Sin(theta1) * yrd4)
    '    Dim r2xd6 As Double = (Robx - rp2x) + (Cos(theta2) * xrd4) - (Sin(theta2) * yrd4)
    '    Dim r3xd6 As Double = (Robx - rp3x) + (Cos(theta3) * xrd4) - (Sin(theta3) * yrd4)
    '    Dim r1yd6 As Double = (Roby - rp1y) + (Sin(theta1) * xrd4) + (Cos(theta1) * yrd4)
    '    Dim r2yd6 As Double = (Roby - rp2y) + (Sin(theta2) * xrd4) + (Cos(theta2) * yrd4)
    '    Dim r3yd6 As Double = (Roby - rp3y) + (Sin(theta3) * xrd4) + (Cos(theta3) * yrd4)


    '    Dim A As Double = 2 * (r2xd6 - r1xd6)
    '    Dim B As Double = 2 * (r2yd6 - r1yd6)
    '    Dim C As Double = ((r2xd6) ^ 2) - ((r1xd6) ^ 2) + ((r2yd6) ^ 2) - ((r1yd6) ^ 2)

    '    Dim D As Double = 2 * (r3xd6 - r2xd6)
    '    Dim E As Double = 2 * (r3yd6 - r2yd6)
    '    Dim F As Double = ((r3xd6) ^ 2) - ((r2xd6) ^ 2) + ((r3yd6) ^ 2) - ((r2yd6) ^ 2)

    '    Dim roe6x = (C - ((B * F) / E)) / (A - ((B * D) / E))
    '    Dim roe6y = (C - ((A * F) / D)) / (B - ((A * E) / D))

    '    Dim REx = rp1x + roe6x
    '    Dim REy = rp1y + roe6y


    '    If RadioButton13.Checked = True Then
    '        TextBox24.Text = REx
    '        TextBox25.Text = REy
    '    End If

    '    ClickPoints(8).X = CDbl(TextBox24.Text)
    '    ClickPoints(8).Y = CDbl(TextBox25.Text)

    'End Sub
    Private Sub ED_lastdial()

        Dim rp1x = TextBox5.Text()
        Dim rp1y = TextBox6.Text()
        Dim rp2x = TextBox8.Text()
        Dim rp2y = TextBox9.Text()
        Dim rp3x = TextBox11.Text()
        Dim rp3y = TextBox12.Text()
        Dim Robx = TextBox16.Text()
        Dim Roby = TextBox17.Text()
        Dim Rb1x = TextBox20.Text()
        Dim Rb1y = TextBox21.Text()
        Dim Rb2x = TextBox26.Text()
        Dim Rb2y = TextBox27.Text()
        Dim Rb3x = TextBox28.Text()
        Dim Rb3y = TextBox29.Text()
        Dim xRE = TextBox22.Text()
        Dim yRE = TextBox23.Text()

        Dim x1 As Double = Rb1x - Robx
        Dim x2 As Double = Rb2x - Robx
        Dim x3 As Double = Rb3x - Robx
        Dim y1 As Double = Rb1y - Roby
        Dim y2 As Double = Rb2y - Roby
        Dim y3 As Double = Rb3y - Roby

        Dim theta1 As Double = Atan2(y1, x1)
        Dim theta2 As Double = Atan2(y2, x2)
        Dim theta3 As Double = Atan2(y3, x3)

        Dim xre6 As Double = xRE - rp1x
        Dim yre6 As Double = yRE - rp1y
        

        Dim r1xe4 As Double = (Cos(theta1) * (rp1x - Robx + xre6)) + (Sin(theta1) * (rp1y - Roby + yre6))
        Dim r2xe4 As Double = (Cos(theta2) * (rp2x - Robx + xre6)) + (Sin(theta2) * (rp2y - Roby + yre6))
        Dim r3xe4 As Double = (Cos(theta3) * (rp3x - Robx + xre6)) + (Sin(theta3) * (rp3y - Roby + yre6))
        Dim r1ye4 As Double = (-Sin(theta1) * (rp1x - Robx + xre6)) + (Cos(theta1) * (rp1y - Roby + yre6))
        Dim r2ye4 As Double = (-Sin(theta2) * (rp2x - Robx + xre6)) + (Cos(theta2) * (rp2y - Roby + yre6))
        Dim r3ye4 As Double = (-Sin(theta3) * (rp3x - Robx + xre6)) + (Cos(theta3) * (rp3y - Roby + yre6))


        Dim A As Double = 2 * (r2xe4 - r1xe4)
        Dim B As Double = 2 * (r2ye4 - r1ye4)
        Dim C As Double = ((r2xe4) ^ 2) - ((r1xe4) ^ 2) + ((r2ye4) ^ 2) - ((r1ye4) ^ 2)

        Dim D As Double = 2 * (r3xe4 - r2xe4)
        Dim E As Double = 2 * (r3ye4 - r2ye4)
        Dim F As Double = ((r3xe4) ^ 2) - ((r2xe4) ^ 2) + ((r3ye4) ^ 2) - ((r2ye4) ^ 2)

        Dim rd4x = (C - ((B * F) / E)) / (A - ((B * D) / E))
        Dim rd4y = (C - ((A * F) / D)) / (B - ((A * E) / D))

        Dim RD1x = Robx + (Cos(theta1) * rd4x) - (Sin(theta1) * rd4y)
        Dim RD1y = Roby + (Sin(theta1) * rd4x) + (Cos(theta1) * rd4y)
        Dim RD2x = Robx + (Cos(theta2) * rd4x) - (Sin(theta2) * rd4y)
        Dim RD2y = Roby + (Sin(theta2) * rd4x) + (Cos(theta2) * rd4y)
        Dim RD3x = Robx + (Cos(theta3) * rd4x) - (Sin(theta3) * rd4y)
        Dim RD3y = Roby + (Sin(theta3) * rd4x) + (Cos(theta3) * rd4y)

        Point_RD(0) = New PointF(RD1x, RD1y)
        Point_RD(1) = New PointF(RD2x, RD2y)
        Point_RD(2) = New PointF(RD3x, RD3y)

        If RadioButton13.Checked = True Then
            TextBox24.Text = RD1x
            TextBox25.Text = RD1y
        End If

        ClickPoints(8).X = CDbl(TextBox24.Text)
        ClickPoints(8).Y = CDbl(TextBox25.Text)

        Point_E6(0) = New PointF(CDbl(TextBox22.Text), CDbl(TextBox23.Text))
        Point_E6(1) = New PointF((Point_E6(0).X) + xre6, (Point_E6(0).Y) + yre6)
        Point_E6(2) = New PointF((Point_E6(1).X) + xre6, (Point_E6(1).Y) + yre6)

    End Sub

    Private Sub Analysis()

        Dim xR1 As Double = (CDbl(TextBox16.Text)) - (CDbl(TextBox14.Text))
        Dim yR1 As Double = (CDbl(TextBox17.Text)) - (CDbl(TextBox15.Text))
        Point_R(0) = New PointF(xR1, yR1)

        Dim xR2 As Double = Point_RA(0).X - (CDbl(TextBox14.Text))
        Dim yR2 As Double = Point_RA(0).Y - (CDbl(TextBox15.Text))
        Point_R(1) = New PointF(xR2, yR2)

        Dim point_R2(1) As PointF
        Dim xR2_2 As Double = Point_RA(1).X - (CDbl(TextBox14.Text))
        Dim yR2_2 As Double = Point_RA(1).Y - (CDbl(TextBox15.Text))
        Dim xR2_3 As Double = Point_RA(2).X - (CDbl(TextBox14.Text))
        Dim yR2_3 As Double = Point_RA(2).Y - (CDbl(TextBox15.Text))
        point_R2(0) = New PointF(xR2_2, yR2_2)
        point_R2(1) = New PointF(xR2_3, yR2_3)

        Dim xR3 As Double = Point_RB(0).X - Point_RA(0).X
        Dim yR3 As Double = Point_RB(0).Y - Point_RA(0).Y
        Point_R(2) = New PointF(xR3, yR3)

        Dim xR4 As Double = Point_RB(0).X - (CDbl(TextBox16.Text))
        Dim yR4 As Double = Point_RB(0).Y - (CDbl(TextBox17.Text))
        Point_R(3) = New Point(xR4, yR4)
        Dim point_RBB(1) As PointF
        Dim xRBB_2 As Double = Point_RB(1).X - (CDbl(TextBox16.Text))
        Dim yRBB_2 As Double = Point_RB(1).Y - (CDbl(TextBox17.Text))
        Dim xRBB_3 As Double = Point_RB(2).X - (CDbl(TextBox16.Text))
        Dim yRBB_3 As Double = Point_RB(2).Y - (CDbl(TextBox17.Text))
        point_RBB(0) = New PointF(xRBB_2, yRBB_2)
        point_RBB(1) = New PointF(xRBB_3, yRBB_3)

        point_rs(0) = Sqrt((xR1 ^ 2) + (yR1 ^ 2))
        point_rs(1) = Sqrt((xR2 ^ 2) + (yR2 ^ 2))
        Dim rs2 = Sqrt((xR2_2 ^ 2) + (yR2_2 ^ 2))
        Dim rs3 = Sqrt((xR2_3 ^ 2) + (yR2_3 ^ 2))

        point_rs(2) = Sqrt((xR3 ^ 2) + (yR3 ^ 2))
        point_rs(3) = Sqrt((xR4 ^ 2) + (yR4 ^ 2))
        Dim rsB2 = Sqrt((xRBB_2 ^ 2) + (yRBB_2 ^ 2))
        Dim rsB3 = Sqrt((xRBB_3 ^ 2) + (yRBB_3 ^ 2))
        point_rs(4) = Sqrt((((CDbl(TextBox5.Text)) - Point_RA(0).X) ^ 2) + (((CDbl(TextBox5.Text)) - Point_RA(0).X) ^ 2)) 'r of RP'

        Dim Theta_R(5) As Double
        Theta_R(0) = Atan2((yR1), (xR1))
        Theta_R(1) = Atan2((yR2), (xR2))
        Theta_R(2) = Atan2((yR2_2), (xR2_2))
        Theta_R(3) = Atan2((yR2_3), (xR2_3))
        Theta_R(4) = Atan2((Point_RB(0).Y - Point_RA(0).Y), (Point_RB(0).X - Point_RA(0).X))
        Theta_R(5) = Atan2(((CDbl(TextBox6.Text)) - Point_RA(0).Y), ((CDbl(TextBox5.Text)) - Point_RA(0).X))
        Dim Alpha As Double = Theta_R(5) - Theta_R(4)


        Dim xR5 As Double = (Point_RB(0).X - Point_RD(0).X)
        Dim yR5 As Double = (Point_RB(0).Y - Point_RD(0).Y)
        Point_R(4) = New PointF(xR5, yR5)

        Dim xR6 As Double = (CDbl(TextBox5.Text)) - Point_RB(0).X
        Dim yR6 As Double = (CDbl(TextBox6.Text)) - Point_RB(0).Y
        Point_R(5) = New PointF(xR6, yR6) '

        Dim point_R6(1) As PointF
        Dim xR6_2 As Double = (CDbl(TextBox8.Text)) - Point_RB(1).X
        Dim yR6_2 As Double = (CDbl(TextBox9.Text)) - Point_RB(1).Y
        Dim xR6_3 As Double = (CDbl(TextBox11.Text)) - Point_RB(2).X
        Dim yR6_3 As Double = (CDbl(TextBox12.Text)) - Point_RB(2).Y
        point_R6(0) = New PointF(xR6_2, yR6_2)
        point_R6(1) = New PointF(xR6_3, yR6_3)

        Dim xR7 As Double = (CDbl(TextBox5.Text)) - Point_E6(0).X
        Dim yR7 As Double = (CDbl(TextBox6.Text)) - Point_E6(0).Y
        Point_R(6) = New PointF(xR7, yR7)

        Dim xR8 As Double = Point_E6(0).X - Point_RD(0).X
        Dim yR8 As Double = Point_E6(0).Y - Point_RD(0).Y
        Point_R(7) = New Point(xR8, yR8)

        Dim point_R8(1) As PointF
        Dim xR8_2 As Double = Point_E6(1).X - Point_RD(1).X
        Dim yR8_2 As Double = Point_E6(1).Y - Point_RD(1).Y
        Dim xR8_3 As Double = Point_E6(2).X - Point_RD(2).X
        Dim yR8_3 As Double = Point_E6(2).Y - Point_RD(2).Y
        point_R8(0) = New PointF(xR8_2, yR8_2)
        point_R8(1) = New PointF(xR8_3, yR8_3)

        point_rs(5) = Sqrt((xR5 ^ 2) + (yR5 ^ 2))
        point_rs(6) = Sqrt((xR6 ^ 2) + (yR6 ^ 2))

        point_rs(7) = Sqrt((xR7 ^ 2) + (yR7 ^ 2))
        point_rs(8) = Sqrt((xR8 ^ 2) + (yR8 ^ 2))


        Dim Theta_R2(5) As Double
        Theta_R2(0) = Atan2((yR5), (xR5))
        Theta_R2(1) = Atan2((yR6), (xR6))
        Theta_R2(2) = Atan2((yR7), (xR7))
        Theta_R2(3) = Atan2((yR8), (xR8))
        Theta_R2(4) = Atan2(yR4, xR4)
        Dim Alpha2 As Double = Theta_R2(1) - Theta_R(2)
        Dim Betha As Double = Theta_R2(4) - Theta_R2(0)


        Dim Delta_Theta2 As Double = Theta_R(3) - Theta_R(1)

        Dim n As Integer = 100

        Dim SDelta_Theta As Double = Delta_Theta2 / n

        Dim Theta2(n) As Double
        Dim xRR(n) As Double
        Dim yRR(n) As Double
        Dim rr(n) As Double
        Dim thetaRR(n) As Double
        Dim Theta4minusThetaaRR(n) As Double
        Dim Theta41(n) As Double
        Dim Theta42(n) As Double
        Dim Theta31(n) As Double
        Dim xRR31(n) As Double
        Dim yRR31(n) As Double
        Dim Theta32(n) As Double
        Dim xRR32(n) As Double
        Dim yRR32(n) As Double
        Dim XP1(n) As Double
        Dim YP1(n) As Double
        Dim XP2(n) As Double
        Dim YP2(n) As Double
        Dim DiffX(n) As Double
        Dim Theta_Degree(n) As Double
        Dim Theta51(n) As Double
        Dim Theta52(n) As Double
        Dim Theta61(n) As Double
        Dim Theta62(n) As Double


        Dim xRRR1(n) As Double
        Dim yRRR1(n) As Double
        Dim rrr1(n) As Double
        Dim thetaRRR1(n) As Double
        Dim xRRR2(n) As Double
        Dim yRRR2(n) As Double
        Dim rrr2(n) As Double
        Dim thetaRRR2(n) As Double
        Dim Theta8minusThetaaRRR1(n) As Double
        Dim Theta8minusThetaaRRR2(n) As Double
        Dim Theta811(n) As Double
        Dim Theta812(n) As Double
        Dim Theta821(n) As Double
        Dim Theta822(n) As Double
        Dim Theta711(n) As Double
        Dim Theta712(n) As Double
        Dim Theta721(n) As Double
        Dim Theta722(n) As Double
        Dim xRR711(n) As Double
        Dim yRR711(n) As Double
        Dim xRR712(n) As Double
        Dim yRR712(n) As Double
        Dim xRR721(n) As Double
        Dim yRR721(n) As Double
        Dim xRR722(n) As Double
        Dim yRR722(n) As Double

        Dim XE1(n) As Double
        Dim YE1(n) As Double
        Dim XE2(n) As Double
        Dim YE2(n) As Double
        Dim DiffEX(n) As Double
        Dim ThetaE_Degree(n) As Double

        For j = 1 To n + 1

            Theta2(j - 1) = (Theta_R(1)) + ((j - 1) * SDelta_Theta)
            Theta2a(j - 1) = Theta2(j - 1)

            xRR(j - 1) = (point_rs(1) * Cos(Theta2a(j - 1))) - (point_rs(0) * Cos(Theta_R(0)))
            yRR(j - 1) = (point_rs(1) * Sin(Theta2a(j - 1))) - (point_rs(0) * Sin(Theta_R(0)))

            rr(j - 1) = Sqrt((xRR(j - 1) ^ 2) + (yRR(j - 1) ^ 2))
            thetaRR(j - 1) = Atan2(yRR(j - 1), xRR(j - 1))

            Theta4minusThetaaRR(j - 1) = Acos(((rr(j - 1) ^ 2) + (point_rs(3) ^ 2) - (point_rs(2) ^ 2)) / (2 * rr(j - 1) * point_rs(3)))
            Theta41(j - 1) = thetaRR(j - 1) + Theta4minusThetaaRR(j - 1)
            Theta42(j - 1) = thetaRR(j - 1) - Theta4minusThetaaRR(j - 1)

            xRR31(j - 1) = (rr(j - 1) * Cos(Theta41(j - 1))) - (point_rs(3) * Cos(thetaRR(j - 1)))
            yRR31(j - 1) = (rr(j - 1) * Sin(Theta41(j - 1))) - (point_rs(3) * Sin(thetaRR(j - 1)))
            Theta31(j - 1) = Atan2((yRR31(j - 1)), (xRR31(j - 1)))

            xRR32(j - 1) = (point_rs(3) * Cos(Theta42(j - 1))) - (rr(j - 1) * Cos(thetaRR(j - 1)))
            yRR32(j - 1) = (point_rs(3) * Sin(Theta42(j - 1))) - (rr(j - 1) * Sin(thetaRR(j - 1)))
            Theta32(j - 1) = Atan2((yRR32(j - 1)), (xRR32(j - 1)))

            XP1(j - 1) = (CDbl(TextBox14.Text)) + (point_rs(1) * Cos(Theta2a(j - 1))) + (point_rs(4) * Cos(Theta31(j - 1) + Alpha))
            YP1(j - 1) = (CDbl(TextBox15.Text)) + (point_rs(1) * Sin(Theta2a(j - 1))) + (point_rs(4) * Sin(Theta31(j - 1) + Alpha))

            XP2(j - 1) = (CDbl(TextBox14.Text)) + (point_rs(1) * Cos(Theta2a(j - 1))) + (point_rs(4) * Cos(Theta32(j - 1) + Alpha))
            YP2(j - 1) = (CDbl(TextBox15.Text)) + (point_rs(1) * Sin(Theta2a(j - 1))) + (point_rs(4) * Sin(Theta32(j - 1) + Alpha))

            DiffX(j - 1) = (Abs(XP2(j - 1) - ClickPoints(0).X) * 100) / (ClickPoints(0).X)
            Theta_Degree(j - 1) = (Theta2a(j - 1)) * (180 / PI)



            Theta51(j - 1) = Theta41(j - 1) - Betha
            Theta52(j - 1) = Theta42(j - 1) - Betha
            Theta61(j - 1) = Theta31(j - 1) + Alpha2
            Theta62(j - 1) = Theta32(j - 1) + Alpha2

            xRRR1(j - 1) = (point_rs(5) * Cos(Theta51(j - 1))) + (point_rs(6) * Cos(Theta61(j - 1)))
            yRRR1(j - 1) = (point_rs(5) * Sin(Theta51(j - 1))) + (point_rs(6) * Sin(Theta61(j - 1)))

            rrr1(j - 1) = Sqrt((xRRR1(j - 1) ^ 2) + (yRRR1(j - 1) ^ 2))
            thetaRRR1(j - 1) = Atan2(yRRR1(j - 1), xRRR1(j - 1))

            xRRR2(j - 1) = (point_rs(5) * Cos(Theta52(j - 1))) + (point_rs(6) * Cos(Theta62(j - 1)))
            yRRR2(j - 1) = (point_rs(5) * Sin(Theta52(j - 1))) + (point_rs(6) * Sin(Theta62(j - 1)))

            rrr2(j - 1) = Sqrt((xRRR2(j - 1) ^ 2) + (yRRR2(j - 1) ^ 2))
            thetaRRR2(j - 1) = Atan2(yRRR2(j - 1), xRRR2(j - 1))

            Theta8minusThetaaRRR1(j - 1) = Acos(((-rrr1(j - 1) ^ 2) + (point_rs(7) ^ 2) - (point_rs(8) ^ 2)) / (-2 * rrr1(j - 1) * point_rs(8)))
            Theta8minusThetaaRRR2(j - 1) = Acos(((-rrr2(j - 1) ^ 2) + (point_rs(7) ^ 2) - (point_rs(8) ^ 2)) / (-2 * rrr2(j - 1) * point_rs(8)))
            Theta811(j - 1) = thetaRRR1(j - 1) - Theta8minusThetaaRRR1(j - 1)
            Theta812(j - 1) = thetaRRR1(j - 1) + Theta8minusThetaaRRR1(j - 1)
            Theta821(j - 1) = thetaRRR2(j - 1) - Theta8minusThetaaRRR2(j - 1)
            Theta822(j - 1) = thetaRRR2(j - 1) + Theta8minusThetaaRRR2(j - 1)

            xRR722(j - 1) = (rrr2(j - 1) * Cos(thetaRRR2(j - 1))) - (point_rs(8) * Cos(Theta822(j - 1)))
            yRR722(j - 1) = (rrr2(j - 1) * Sin(thetaRRR2(j - 1))) - (point_rs(8) * Sin(Theta822(j - 1)))
            Theta722(j - 1) = Atan2(yRR722(j - 1), xRR722(j - 1))

            xRR721(j - 1) = (rrr2(j - 1) * Cos(thetaRRR2(j - 1))) - (point_rs(8) * Cos(Theta821(j - 1)))
            yRR721(j - 1) = (rrr2(j - 1) * Sin(thetaRRR2(j - 1))) - (point_rs(8) * Sin(Theta821(j - 1)))
            Theta721(j - 1) = Atan2(yRR721(j - 1), xRR721(j - 1))

            'xRR712(j - 1) = (rrr2(j - 1) * Cos(thetaRRR1(j - 1))) - (point_rs(8) * Cos(Theta812(j - 1)))
            'yRR712(j - 1) = (rrr2(j - 1) * Sin(thetaRRR1(j - 1))) - (point_rs(8) * Sin(Theta812(j - 1)))
            'Theta712(j - 1) = Atan2(yRR712(j - 1), xRR712(j - 1))

            'xRR721(j - 1) = (rrr1(j - 1) * Cos(thetaRRR2(j - 1))) - (point_rs(8) * Cos(Theta821(j - 1)))
            'yRR721(j - 1) = (rrr1(j - 1) * Sin(thetaRRR2(j - 1))) - (point_rs(8) * Sin(Theta821(j - 1)))
            'Theta721(j - 1) = Atan2(yRR721(j - 1), xRR721(j - 1))

            'xRR722(j - 1) = (rrr2(j - 1) * Cos(thetaRRR2(j - 1))) - (point_rs(8) * Cos(Theta822(j - 1)))
            'yRR722(j - 1) = (rrr2(j - 1) * Sin(thetaRRR2(j - 1))) - (point_rs(8) * Sin(Theta822(j - 1)))
            'Theta722(j - 1) = Atan2(yRR722(j - 1), xRR722(j - 1))

            XE1(j - 1) = XP1(j - 1) + xRR721(j - 1)
            YE1(j - 1) = YP1(j - 1) + yRR721(j - 1)

            'XE2(j - 1) = XP2(j - 1) + xRR721(j - 1)
            'YE2(j - 1) = YP2(j - 1) + yRR721(j - 1)

            DiffEX(j - 1) = (Abs(XE1(j - 1) - Point_E6(0).X) * 100) / (Point_E6(0).X)
            Theta_Degree(j - 1) = (Theta2a(j - 1)) * (180 / PI)

        Next j

        Dim myErrorPlot As New Chart(Theta_Degree, DiffX)
        myErrorPlot.Show()

        Dim myErrorPlot2 As New chart2(Theta_Degree, DiffEX)
        myErrorPlot2.Show()

        Dim myErrorPlot3 As New Chart3(XP2, YP2)
        myErrorPlot3.Show()

    End Sub
    Private Sub ReSet()

        G = GetGraphicsObject(PictureBox1)
        If Not (TextBox1.Text = Nothing Or
            TextBox2.Text = Nothing Or
            TextBox3.Text = Nothing Or
            TextBox4.Text = Nothing) Then

            SetScale(G, PictureBox1.Width, PictureBox1.Height, _
                CSng(TextBox1.Text), CSng(TextBox2.Text), CSng(TextBox4.Text), CSng(TextBox3.Text))

            m_InverseTransformation = G.Transform
            m_InverseTransformation.Invert()

            Dim myPen As New Pen(Color.Blue, 1)
            Dim myPen2 As New Pen(Color.Green, 1)
            Dim myPen3 As New Pen(Color.Red, 1)

            If RadioButton10.Checked = True Then

                G.DrawLine(myPen, ClickPoints(3), Point_RA(0))
                G.DrawLine(myPen, Point_RA(0), Point_RB(0))
                G.DrawLine(myPen, ClickPoints(4), Point_RB(0))
                G.DrawLine(myPen3, Point_RA(0).X, Point_RA(0).Y, ClickPoints(0).X, ClickPoints(0).Y)
                G.DrawLine(myPen3, Point_RB(0).X, Point_RB(0).Y, ClickPoints(0).X, ClickPoints(0).Y)
                G.DrawLine(myPen2, ClickPoints(4).X, ClickPoints(4).Y, Point_RD(0).X, Point_RD(0).X)
                G.DrawLine(myPen2, Point_RB(0).X, Point_RB(0).Y, Point_RD(0).X, Point_RD(0).X)
                G.DrawLine(myPen2, Point_E6(0).X, Point_E6(0).Y, Point_RD(0).X, Point_RD(0).X)

                

            ElseIf RadioButton11.Checked = True Then

                G.DrawLine(myPen, ClickPoints(3), Point_RA(1))
                G.DrawLine(myPen, Point_RA(1), Point_RB(1))
                G.DrawLine(myPen, ClickPoints(4), Point_RB(1))
                G.DrawLine(myPen3, Point_RA(1).X, Point_RA(1).Y, ClickPoints(1).X, ClickPoints(1).Y)
                G.DrawLine(myPen3, Point_RB(1).X, Point_RB(1).Y, ClickPoints(1).X, ClickPoints(1).Y)
                G.DrawLine(myPen2, ClickPoints(4).X, ClickPoints(4).Y, Point_RD(1).X, Point_RD(1).X)
                G.DrawLine(myPen2, Point_RB(1).X, Point_RB(1).Y, Point_RD(1).X, Point_RD(1).X)
                G.DrawLine(myPen2, Point_E6(1).X, Point_E6(1).Y, Point_RD(1).X, Point_RD(1).X)

               

            ElseIf RadioButton12.Checked = True Then

                G.DrawLine(myPen, ClickPoints(3), Point_RA(2))
                G.DrawLine(myPen, Point_RA(2), Point_RB(2))
                G.DrawLine(myPen, ClickPoints(4), Point_RB(2))
                G.DrawLine(myPen3, Point_RA(2).X, Point_RA(2).Y, ClickPoints(2).X, ClickPoints(2).Y)
                G.DrawLine(myPen3, Point_RB(2).X, Point_RB(2).Y, ClickPoints(2).X, ClickPoints(2).Y)
                G.DrawLine(myPen2, ClickPoints(4).X, ClickPoints(4).Y, Point_RD(2).X, Point_RD(2).X)
                G.DrawLine(myPen2, Point_RB(2).X, Point_RB(2).Y, Point_RD(2).X, Point_RD(2).X)
                G.DrawLine(myPen2, Point_E6(2).X, Point_E6(2).Y, Point_RD(2).X, Point_RD(2).X)

               

            End If

        End If

    End Sub

    Private Sub RadioButton10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton10.CheckedChanged

        Dim myPen As New Pen(Color.Blue, 1)

        If RadioButton10.Checked = True Then

            TextBox18.Text = Point_RA(0).X
            TextBox19.Text = Point_RA(0).Y
            TextBox20.Text = Point_RB(0).X
            TextBox21.Text = Point_RB(0).Y
            TextBox24.Text = Point_RD(0).X
            TextBox25.Text = Point_RD(0).Y

        End If
        ReSet()
    End Sub

    Private Sub RadioButton11_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton11.CheckedChanged

        If RadioButton11.Checked = True Then

            TextBox18.Text = Point_RA(1).X
            TextBox19.Text = Point_RA(1).Y
            TextBox20.Text = Point_RB(1).X
            TextBox21.Text = Point_RB(1).Y
            TextBox24.Text = Point_RD(1).X
            TextBox25.Text = Point_RD(1).Y
        End If
        ReSet()
    End Sub

    Private Sub RadioButton12_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton12.CheckedChanged

        If RadioButton12.Checked = True Then

            TextBox18.Text = Point_RA(2).X
            TextBox19.Text = Point_RA(2).Y
            TextBox20.Text = Point_RB(2).X
            TextBox21.Text = Point_RB(2).Y
            TextBox24.Text = Point_RD(2).X
            TextBox25.Text = Point_RD(2).Y

        End If
        ReSet()
    End Sub
End Class

