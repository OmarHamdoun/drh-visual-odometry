using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisualOdometry.UI.Properties;
using Emgu.CV;
using Emgu.CV.Structure;

namespace VisualOdometry.UI
{
	public partial class MainForm : Form
	{
		private Capture m_Capture;
		private VisualOdometer m_VisualOdometer;

		public MainForm()
		{
			InitializeComponent();
			m_Capture = new Capture();
			m_VisualOdometer = new VisualOdometer(m_Capture, new OpticalFlow());

			UpdateFromModel();

			m_VisualOdometer.Changed += new EventHandler(OnVisualOdometerChanged);
			Application.Idle += OnApplicationIdle;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			Size formSize = Settings.Default.Size;
			if (formSize.Height != 0)
			{
				this.Size = formSize;
				this.Location = Settings.Default.Location;
			}
		}

		private void OnVisualOdometerChanged(object sender, EventArgs e)
		{
			UpdateFromModel();
		}

		private void OnApplicationIdle(object sender, EventArgs e)
		{
			m_VisualOdometer.ProcessFrame();
			m_FoundFeaturesCountTextBox.Text = m_VisualOdometer.FoundFeaturesCount.ToString();
			m_TrackedFeaturesCountTextBox.Text = (m_VisualOdometer.TrackedFeatures.Count - m_VisualOdometer.NotTrackedFeaturesCount).ToString();
			m_NotTrackedFeaturesCount.Text = m_VisualOdometer.NotTrackedFeaturesCount.ToString();

			DrawFeatureLocationPreviousAndCurrent();
			m_FeaturesImageBox.ImageBox.Image = m_VisualOdometer.CurrentImage;
			//m_FlowImageBox.ImageBox.Image = m_VisualOdometer.OpticalFlow.FlowImage;
		}

		private void DrawFeatureLocationPreviousAndCurrent()
		{
			if (m_VisualOdometer.HistoryLevel < 2)
			{
				return;
			}
			// draw previous location
			foreach (TrackedFeature trackedFeature in m_VisualOdometer.TrackedFeatures)
			{
				if (!trackedFeature.IsOut)
				{
					CircleF circle = new CircleF(trackedFeature[-1], 3.0f);
					m_VisualOdometer.CurrentImage.Draw(circle, new Bgr(Color.Lime), 2);
				}
			}

			// draw current location
			foreach (TrackedFeature trackedFeature in m_VisualOdometer.TrackedFeatures)
			{
				if (!trackedFeature.IsOut)
				{
					CircleF circle = new CircleF(trackedFeature[0], 3.0f);
					m_VisualOdometer.CurrentImage.Draw(circle, new Bgr(Color.Red), 2);
				}
			}
		}

		private void UpdateFromModel()
		{
			m_MaxFeatureCountTextBox.Text = m_VisualOdometer.OpticalFlow.MaxFeatureCount.ToString();
			m_BlockSizeTextBox.Text = m_VisualOdometer.OpticalFlow.BlockSize.ToString();
			m_QaulityLevelTextBox.Text = m_VisualOdometer.OpticalFlow.QualityLevel.ToString();
			m_MinDistanceTextBox.Text = m_VisualOdometer.OpticalFlow.MinDistance.ToString();

			m_SkyBottomTextBox.Text = m_VisualOdometer.SkyRegionBottom.ToString();
			m_GroundTopTextBox.Text = m_VisualOdometer.GroundRegionTop.ToString();
		}

		private void OnApplyButtonClicked(object sender, EventArgs e)
		{
			int maxFeatureCount = m_VisualOdometer.OpticalFlow.MaxFeatureCount;
			Int32.TryParse(m_MaxFeatureCountTextBox.Text, out maxFeatureCount);

			int blockSize = m_VisualOdometer.OpticalFlow.BlockSize;
			Int32.TryParse(m_BlockSizeTextBox.Text, out blockSize);

			double qualityLevel = m_VisualOdometer.OpticalFlow.QualityLevel;
			Double.TryParse(m_QaulityLevelTextBox.Text, out qualityLevel);

			double minDistance = m_VisualOdometer.OpticalFlow.MinDistance;
			Double.TryParse(m_MinDistanceTextBox.Text, out minDistance);

			OpticalFlow opticalFlow = new OpticalFlow(maxFeatureCount, blockSize, qualityLevel, minDistance);
			m_VisualOdometer.OpticalFlow = opticalFlow;

			int skyBottom;
			if (Int32.TryParse(m_SkyBottomTextBox.Text, out skyBottom))
			{
				m_VisualOdometer.SkyRegionBottom = skyBottom;
			}

			int groundTop;
			if (Int32.TryParse(m_GroundTopTextBox.Text, out groundTop))
			{
				m_VisualOdometer.GroundRegionTop = groundTop;
			}
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			if (m_Capture != null)
			{
				m_Capture.Dispose();
			}
			if (m_VisualOdometer != null)
			{
				m_VisualOdometer.Dispose();
			}

			// If the MainForm is not minimized, save the current Size and 
			// location to the settings file.  Otherwise, the existing values 
			// in the settings file will not change...
			if (this.WindowState != FormWindowState.Minimized)
			{
				Settings.Default.Size = this.Size;
				Settings.Default.Location = this.Location;
			}
			Settings.Default.Save();
		}
	}
}
