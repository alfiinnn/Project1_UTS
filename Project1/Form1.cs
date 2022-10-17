using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Project1
{
    public partial class Form1 : Form
    {
        TideData tideData;

        public Form1()
        {
            InitializeComponent();
            tideData = new TideData();
            tideData.Request();
            Generate();
        }

        public void Generate()
        {
            var objChart = Chart.ChartAreas[0];
            // ini buat timedate axis x
            objChart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
            objChart.AxisX.Minimum = tideData.response.meta.start.ToOADate();
            objChart.AxisX.Maximum = tideData.response.meta.end.ToOADate();

            //ini buat timedate axis y 
            objChart.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            objChart.AxisY.Minimum = -3;
            objChart.AxisY.Maximum = 2;

            //clear 
            Chart.Series.Clear();

            //ini 
            Series newSeries = new Series("Tide");
            newSeries.ChartType = SeriesChartType.Column;
            newSeries.BorderWidth = 2;
            newSeries.Color = Color.DarkGreen;
            newSeries.XValueType = ChartValueType.DateTime;
            Chart.Series.Add(newSeries);

            for (int i = 0; i < tideData.response.data.Count; i++)
            {
                Chart.Series[0].Points.AddXY(tideData.response.data[i].time, tideData.response.data[i].height);
                Refresh();
            }
        }
    }
}
