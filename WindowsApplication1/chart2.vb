Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Math


Public Class chart2
    Dim Theta2() As Double
    Dim DiffX() As Double

    Public Sub New(ByVal _Theta2() As Double, ByVal _DiffX() As Double)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Theta2 = _Theta2
        DiffX = _DiffX
    End Sub

    Private Sub PathPlot_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Plot()
    End Sub

    Sub Plot()
        If IsNothing(Chart1.ChartAreas("ChartArea1")) Then
            Chart1.ChartAreas.Add("ChartArea1")
        End If
        Chart1.Series.Clear()
        Chart1.Series.Add("Error P Plot")
        With Me.Chart1.ChartAreas("ChartArea1")

            .Position.Width = 90
            .Position.Height = 90

            .AxisX.MaximumAutoSize = 90
            .AxisY.MaximumAutoSize = 90

            .AxisX.Maximum = Theta2.Max
            .AxisX.Minimum = Theta2.Min
            .AxisY.Maximum = DiffX.Max
            .AxisY.Minimum = DiffX.Min
            .AxisX.LabelStyle.Format = "0.00"
            .AxisY.LabelStyle.Format = "0.00"

        End With
        Chart1.Series("Error P Plot").Points.DataBindXY(Theta2, DiffX)
        Chart1.Series("Error P Plot").ChartType = SeriesChartType.Line
        Chart1.Series("Error P Plot").Color = Color.Yellow
        Chart1.Series("Error P Plot").BorderWidth = 2
    End Sub

End Class