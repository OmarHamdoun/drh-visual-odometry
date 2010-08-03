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
using CameraCalibrator.Support;

namespace VisualOdometry.UI
{
	public partial class MainForm : Form
	{
		private Capture m_Capture;
		private VisualOdometer m_VisualOdometer;
		private RotationAnalysisForm m_RotationAnalysisForm = new RotationAnalysisForm();
		private AuxiliaryViewsForm m_AuxiliaryViewsForm;
		private HomographyMatrix m_BirdsEyeViewTransformationForUI;

		public MainForm()
		{
			InitializeComponent();
			bool useCamera = false;

			if (useCamera)
			{
				m_Capture = new Capture();
			}
			else
			{
				m_Capture = new Capture(@"C:\svnDev\oss\Google\drh-visual-odometry\TestVideos\2010-07-18 11-10-22.853.wmv");
				m_Timer.Interval = 33;
				m_Timer.Enabled = true;
			}

			CameraParameters cameraParameters = CameraParameters.Load(@"C:\svnDev\oss\Google\drh-visual-odometry\CalibrationFiles\MicrosoftCinema\Focus12\1280x720\MicrosoftCinemaFocus12_1280x720.txt");

			HomographyMatrix birdsEyeViewTransformation = HomographyMatrixSupport.Load(@"C:\svnDev\oss\Google\drh-visual-odometry\CalibrationFiles\MicrosoftCinema\Focus12\1280x720\BirdsEyeViewTransformationForCalculation.txt");
			m_BirdsEyeViewTransformationForUI = HomographyMatrixSupport.Load(@"C:\svnDev\oss\Google\drh-visual-odometry\CalibrationFiles\MicrosoftCinema\Focus12\1280x720\BirdsEyeViewTransformationForUI.txt");

			m_VisualOdometer = new VisualOdometer(m_Capture, cameraParameters, birdsEyeViewTransformation, new OpticalFlow());

			UpdateFromModel();

			m_VisualOdometer.Changed += new EventHandler(OnVisualOdometerChanged);
			Application.Idle += OnApplicationIdle;
		}

		private void OnTimerTick(object sender, EventArgs e)
		{
			ProcessFrame();
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
			ProcessFrame();
		}

		private void ProcessFrame()
		{
			m_VisualOdometer.ProcessFrame();
			if (m_VisualOdometer.CurrentImage == null)
			{
				return;
			}
			m_FoundFeaturesCountTextBox.Text = m_VisualOdometer.InitialFeaturesCount.ToString();
			m_TrackedFeaturesCountTextBox.Text = m_VisualOdometer.TrackedFeatures.Count.ToString();
			m_NotTrackedFeaturesCount.Text = m_VisualOdometer.NotTrackedFeaturesCount.ToString();

			DrawRegionBounderies();

			if (m_DrawFeaturesCheckBox.Checked)
			{
				DrawFeatureLocationsPreviousAndCurrent();
			}
			m_ImageBox.Image = m_VisualOdometer.CurrentImage;

			m_CumulativeRotationTextBox.Text = m_VisualOdometer.RotationAnalyzer.HeadingDegree.ToString();

			if (!m_RotationAnalysisForm.IsDisposed)
			{
				m_RotationAnalysisForm.Update(m_VisualOdometer);
			}
			if (!(m_AuxiliaryViewsForm == null || m_AuxiliaryViewsForm.IsDisposed))
			{
				m_AuxiliaryViewsForm.Update(m_VisualOdometer);
			}
		}

		private void DrawRegionBounderies()
		{
			DrawRegionBoundary(m_VisualOdometer.CurrentImage, m_VisualOdometer.SkyRegionBottom);
			DrawRegionBoundary(m_VisualOdometer.CurrentImage, m_VisualOdometer.GroundRegionTop);
		}

		private void DrawRegionBoundary(Image<Bgr, Byte> image, int yPos)
		{
			PointF start = new PointF(0, yPos);
			PointF end = new PointF(image.Width, yPos);
			LineSegment2DF lineSegment = new LineSegment2DF(start, end);
			image.Draw(lineSegment, new Bgr(Color.Red), 1);
		}

		private Bgr m_FeatureColorPreviousPartialHistory = new Bgr(Color.Yellow);
		private Bgr m_FeatureColorCurrentPartialHistory = new Bgr(Color.Orange);

		private Bgr m_FeatureColorPreviousFullHistory = new Bgr(Color.Lime);
		private Bgr m_FeatureColorCurrentFullHistory = new Bgr(Color.Red);

		private void DrawFeatureLocationsPreviousAndCurrent()
		{
			List<TrackedFeature> trackedFeatures = m_VisualOdometer.TrackedFeatures;
			// draw previous location
			for (int i = 0; i < trackedFeatures.Count; i++)
			{
				TrackedFeature trackedFeature = trackedFeatures[i];
				if (trackedFeature.Count > 1)
				{
					// We have a previous value
					CircleF circle = new CircleF(trackedFeature[-1], 3.0f);
					if (!trackedFeature.HasFullHistory)
					{
						m_VisualOdometer.CurrentImage.Draw(circle, m_FeatureColorPreviousPartialHistory, 2);
					}
					else
					{
						m_VisualOdometer.CurrentImage.Draw(circle, m_FeatureColorPreviousFullHistory, 2);
					}
				}
			}

			// draw current location
			for (int i = 0; i < trackedFeatures.Count; i++)
			{
				TrackedFeature trackedFeature = trackedFeatures[i];
				CircleF circle = new CircleF(trackedFeature[0], 3.0f);
				if (!trackedFeature.HasFullHistory)
				{
					m_VisualOdometer.CurrentImage.Draw(circle, m_FeatureColorCurrentPartialHistory, 2);
				}
				else
				{
					m_VisualOdometer.CurrentImage.Draw(circle, m_FeatureColorCurrentFullHistory, 2);
				}
			}
		}

		private void UpdateFromModel()
		{
			m_MaxFeatureCountTextBox.Text = m_VisualOdometer.OpticalFlow.MaxFeatureCount.ToString();
			m_BlockSizeTextBox.Text = m_VisualOdometer.OpticalFlow.BlockSize.ToString();
			m_QualityLevelTextBox.Text = m_VisualOdometer.OpticalFlow.QualityLevel.ToString();
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
			Double.TryParse(m_QualityLevelTextBox.Text, out qualityLevel);

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

		private void OnDetailsButtonClicked(object sender, EventArgs e)
		{
			if (m_RotationAnalysisForm.IsDisposed)
			{
				m_RotationAnalysisForm = new RotationAnalysisForm();
			}
			m_RotationAnalysisForm.Show(this);
		}

		private void OnOtherViewsButtonClicked(object sender, EventArgs e)
		{
			if (m_AuxiliaryViewsForm == null || m_AuxiliaryViewsForm.IsDisposed)
			{
				m_AuxiliaryViewsForm = new AuxiliaryViewsForm(m_BirdsEyeViewTransformationForUI);
			}
			m_AuxiliaryViewsForm.Show(this);		
		}
	}
}
