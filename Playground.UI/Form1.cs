using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Playground.UI
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void InitializeChart()
		{
			// Populate "Default" chart series with random data
			Random rand = new Random();
			for (int index = 1; index < 100; index++)
			{
				m_Chart.Series["Histogram"].Points.AddY(rand.Next(1, 1000));
			}

			// HistogramChartHelper is a helper class found in the samples Utilities folder. 
			HistogramChartHelper histogramHelper = new HistogramChartHelper();

			// Show the percent frequency on the right Y axis.
			histogramHelper.ShowPercentOnSecondaryYAxis = true;

			// Specify number of segment intervals
			histogramHelper.SegmentIntervalNumber = 10;

			// Or you can specify the exact length of the interval
			// histogramHelper.SegmentIntervalWidth = 15;

			// Create histogram series    
			histogramHelper.CreateHistogram(m_Chart, "Histogram", "Histogram");
		}
	}
}
