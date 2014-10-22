using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace Laba5
{
    public class PotentialMethod
    {
        private readonly Series seriesClass1 = new Series {ChartType = SeriesChartType.Point};
        private readonly Series seriesClass2 = new Series {ChartType = SeriesChartType.Point};
        private readonly Series seriesNoClass = new Series {ChartType = SeriesChartType.Point};
        private readonly List<Point> vectors = new List<Point>();
        private readonly List<int> weigths = new List<int>();
        private List<PotentialClass> classes = new List<PotentialClass>();

        public void SetObjects(List<PotentialClass> classList)
        {
            classes.Clear();
            weigths.Clear();
            classes = classList;
            for (int i = 0; i < classes.Count; i++)
            {
                for (int j = 0; j < classes[i].vectors.Count; j++)
                {
                    weigths.Add(0);
                }
            }
            foreach (PotentialClass potentialClass in classes)
            {
                foreach (Point vector in potentialClass.vectors)
                {
                    vectors.Add(vector);
                }
            }
        }

        private void CorrectPotential(List<int> term, int sign)
        {
            for (int i = 0; i < weigths.Count; i++)
            {
                weigths[i] += sign*term[i];
            }
        }

        private int FindCorrectionFactor(Point vector)
        {
            int sum = 0;
            sum = weigths[0] + weigths[1]*vector.X + weigths[2]*vector.Y + weigths[3]*vector.X*vector.Y;
            if ((classes[0].vectors.Contains(vector)) && (sum <= 0))
            {
                return 1;
            }
            if ((classes[1].vectors.Contains(vector)) && (sum > 0))
            {
                return -1;
            }
            return 0;
        }

        private List<int> FindCurrPotential(Point vector)
        {
            var result = new List<int>();
            result.Add(1);
            result.Add(4*vector.X);
            result.Add(4*vector.Y);
            result.Add(16*vector.X*vector.Y);
            return result;
        }

        public List<int> CalculateFunction()
        {
            bool FunctionIsIncorrect = true;
            int sign = 1;
            while (FunctionIsIncorrect)
            {
                FunctionIsIncorrect = false;
                for (int i = 0; i < vectors.Count; i++)
                {
                    Point vector = vectors[i];
                    Point nextVector;
                    if (i == vectors.Count - 1)
                    {
                        nextVector = vectors[0];
                    }
                    else
                    {
                        nextVector = vectors[i + 1];
                    }
                    CorrectPotential(FindCurrPotential(vector), sign);
                    sign = FindCorrectionFactor(nextVector);
                    if (sign != 0)
                    {
                        FunctionIsIncorrect = true;
                    }
                }
            }
            return weigths;
        }

        private double FindY(double x)
        {
            if ((weigths[2] + weigths[3]*x) != 0)
            {
                return (-weigths[1]*x - weigths[0])/(weigths[2] + weigths[3]*x);
            }
            return 100;
        }

        private double FindX(double y)
        {
            if ((weigths[1] + weigths[3]*y) != 0)
            {
                return (-weigths[2]*y - weigths[0])/(weigths[1] + weigths[3]*y);
            }
            return 100;
        }

        private void SetUpChart(Chart chart)
        {
            chart.Series.Clear();
            chart.ChartAreas[0].AxisX.ArrowStyle = AxisArrowStyle.Lines;
            chart.ChartAreas[0].AxisX.Crossing = 0;
            chart.ChartAreas[0].AxisX.IsStartedFromZero = true;
            chart.ChartAreas[0].AxisX.Title = "X1";
            chart.ChartAreas[0].AxisX.Interval = 1;
            chart.ChartAreas[0].AxisX.LineWidth = 2;
            chart.ChartAreas[0].AxisY.ArrowStyle = AxisArrowStyle.Lines;
            chart.ChartAreas[0].AxisY.Crossing = 0;
            chart.ChartAreas[0].AxisY.IsStartedFromZero = true;
            chart.ChartAreas[0].AxisX.Minimum = -10;
            chart.ChartAreas[0].AxisX.Maximum = 10;
            chart.ChartAreas[0].AxisY.Maximum = 10;
            chart.ChartAreas[0].AxisY.Minimum = -10;
            chart.ChartAreas[0].AxisY.Title = "X2";
            chart.ChartAreas[0].AxisY.Interval = 1;
            chart.ChartAreas[0].AxisY.LineWidth = 2;
            seriesClass1.Points.AddXY(vectors[0].X, vectors[0].Y);
            seriesClass1.Points.AddXY(vectors[1].X, vectors[1].Y);
            seriesClass2.Points.AddXY(vectors[2].X, vectors[2].Y);
            seriesClass2.Points.AddXY(vectors[3].X, vectors[3].Y);
            seriesClass1.Name = "Класс 1";
            seriesClass2.Name = "Класс 2";
            seriesNoClass.Name = "Нейтральный класс";
            seriesClass1.Color = Color.Red;
            seriesClass2.Color = Color.Blue;
            seriesNoClass.Color = Color.Black;
            seriesClass1.MarkerSize = 10;
            seriesClass2.MarkerSize = 10;
            seriesNoClass.MarkerSize = 10;
            chart.Series.Add(seriesClass1);
            chart.Series.Add(seriesClass2);
        }

        public void DrawChartGraphics(Chart chart)
        {
            SetUpChart(chart);
            var series1 = new Series {ChartType = SeriesChartType.Spline};
            var series2 = new Series {ChartType = SeriesChartType.Spline};
            var series3 = new Series {ChartType = SeriesChartType.Spline};
            var series4 = new Series {ChartType = SeriesChartType.Spline};
            Series seriesTemp1;
            Series seriesTemp2;
            series1.YAxisType = AxisType.Primary;
            series1.Name = "Разделяющая функция1";
            series1.BorderWidth = 3;
            series1.Color = Color.CadetBlue;
            series2.YAxisType = AxisType.Primary;
            series2.Name = "Разделяющая функция2";
            series2.BorderWidth = 3;
            series2.Color = Color.CadetBlue;
            series3.YAxisType = AxisType.Primary;
            series3.Name = "Разделяющая функция3";
            series3.BorderWidth = 3;
            series3.Color = Color.CadetBlue;
            series4.YAxisType = AxisType.Primary;
            series4.Name = "Разделяющая функция4";
            series4.BorderWidth = 3;
            series4.Color = Color.CadetBlue;
            double x = -10;
            seriesTemp1 = series1;
            seriesTemp2 = series2;
            while (x < 10)
            {
                if (Math.Abs(weigths[2] + weigths[3]*x) < 0.001)
                {
                    seriesTemp1 = series3;
                }
                if (Math.Abs(weigths[1] + weigths[3]*x) < 0.001)
                {
                    seriesTemp2 = series4;
                }
                if (FindY(x) < 10 && FindY(x) > -10)
                {
                    double y = FindY(x);
                    seriesTemp1.Points.AddXY(x, y);
                }
                if (FindX(x) < 10 && FindX(x) > -10)
                {
                    double y = FindX(x);
                    seriesTemp2.Points.AddXY(y, x);
                }
                x += 0.01;
            }
            chart.Series.Add(series1);
            chart.Series.Add(series2);
            chart.Series.Add(series3);
            chart.Series.Add(series4);
        }

        public int DistributeVector(Point vector)
        {
            int sum = 0;
            sum = weigths[0] + weigths[1]*vector.X + weigths[2]*vector.Y + weigths[3]*vector.X*vector.Y;
            if (sum > 0)
            {
                seriesClass1.Points.AddXY(vector.X, vector.Y);
                return 1;
            }
            if (sum < 0)
            {
                seriesClass2.Points.AddXY(vector.X, vector.Y);
                return 2;
            }
            seriesNoClass.Points.AddXY(vector.X, vector.Y);
            return 0;
        }

        public class PotentialClass
        {
            public List<Point> vectors = new List<Point>();
        }
    }
}