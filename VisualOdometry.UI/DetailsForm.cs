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
		private Series m_Series1;
		private HistogramChartHelper m_HistogramHelper = new HistogramChartHelper();

		public DetailsForm()
		{
			InitializeComponent();
			this.ShowInTaskbar = false;

			m_Series1 = new Series();
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

			m_AnglesChart.Series.Clear();
			m_Series1.Points.Clear();
			for (int i = 0; i < headingChanges.Count; i++)
			{
				m_Series1.Points.AddY(headingChanges[i] * 180 / Math.PI);
			}

			m_AnglesChart.Series.Add(m_Series1);
			m_HistogramHelper.ShowPercentOnSecondaryYAxis = false;
			m_HistogramHelper.SegmentIntervalNumber = 5;
			//m_HistogramHelper.SegmentIntervalWidth = 5;
			m_HistogramHelper.CreateHistogram(m_AnglesChart, "Series1", "Histogram");
		}
	}
}
