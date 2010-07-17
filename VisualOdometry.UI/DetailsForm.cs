using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;

namespace VisualOdometry.UI
{
	public partial class DetailsForm : Form
	{
		private static readonly double s_RadToDegree = 180.0 / Math.PI;

		private Histogram m_Histogram;
		private const string c_HistogramSeriesName = "Histogram";
		private Series m_HistogramSeries;
		private HistogramChartHelper m_HistogramHelper = new HistogramChartHelper();

		public DetailsForm()
		{
			InitializeComponent();
			this.ShowInTaskbar = false;

			InitializeHistogram();
		}

		private void InitializeHistogram()
		{
			m_Histogram = new Histogram(-5.0, 5.0, 200);
			InitializeHistogramChart();
		}

		private void InitializeHistogramChart()
		{
			// Add new series
			m_HistogramSeries = m_AnglesChart.Series.Add(c_HistogramSeriesName);

			// Set new series chart type and other attributes
			m_HistogramSeries.ChartType = SeriesChartType.Column;
			m_HistogramSeries.BorderColor = Color.Black;
			m_HistogramSeries.BorderWidth = 1;
			m_HistogramSeries.BorderDashStyle = ChartDashStyle.Solid;
			m_HistogramSeries["PointWidth"] = "1.0";
			m_HistogramSeries["BarLabelStyle"] = "Center";

			// Adjust chart area
			ChartArea chartArea = m_AnglesChart.ChartAreas[m_HistogramSeries.ChartArea];
			chartArea.AxisY.Title = "Frequency";
			chartArea.AxisX.Minimum = m_Histogram.Min;
			chartArea.AxisX.Maximum = m_Histogram.Max;

			// Set axis interval based on the histogram class interval
			// and do not allow more than 10 labels on the axis.
			double axisInterval = m_Histogram.BinWidth;
			while ((m_Histogram.Max - m_Histogram.Min) / axisInterval > 10.0)
			{
				axisInterval *= 2.0;
			}
			chartArea.AxisX.Interval = axisInterval;
			//chartArea.AxisX.MajorGrid = new Grid(.IsStartedFromZero = true;

			//// Set chart area secondary Y axis
			//chartArea.AxisY2.Enabled = AxisEnabled.Auto;
			//if (this.ShowPercentOnSecondaryYAxis)
			//{
			//    chartArea.RecalculateAxesScale();

			//    chartArea.AxisY2.Enabled = AxisEnabled.True;
			//    chartArea.AxisY2.LabelStyle.Format = "P0";
			//    chartArea.AxisY2.MajorGrid.Enabled = false;
			//    chartArea.AxisY2.Title = "Percent of Total";

			//    chartArea.AxisY2.Minimum = 0;
			//    chartArea.AxisY2.Maximum = chartArea.AxisY.Maximum / (pointCount / 100.0);
			//    double minStep = (chartArea.AxisY2.Maximum > 20.0) ? 5.0 : 1.0;
			//    chartArea.AxisY2.Interval = Math.Ceiling((chartArea.AxisY2.Maximum / 5.0 / minStep)) * minStep;
			//}
		}

		internal void Update(VisualOdometer visualOdometer)
		{
			if (!this.Created)
			{
				return;
			}

			List<double> headingChanges = visualOdometer.HeadingChanges;
			if (headingChanges.Count == 0)
			{
				return;
			}

			double[] degreeAngles = new double[headingChanges.Count];
			for (int i = 0; i < headingChanges.Count; i++)
			{
				degreeAngles[i] = headingChanges[i] * s_RadToDegree;
			}

			m_Histogram.Fill(degreeAngles);
			//m_AnglesChart.Series.Clear();
			m_HistogramSeries.Points.Clear();
			for (int i = 0; i < m_Histogram.Bins.Length; i++)
			{
				HistogramBin bin = m_Histogram.Bins[i];
				// Add data point into the histogram series
				double x = (bin.Min + bin.Max) / 2.0;
				m_HistogramSeries.Points.AddXY(x, bin.Count);
			}
		}
	}
}
