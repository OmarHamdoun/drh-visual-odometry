using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace VisualOdometry
{
	public class VisualOdometer : IDisposable
	{
		private Capture m_Capture;
		private int m_GroundRegionTop;
		private int m_SkyRegionBottom;
		private double m_HeadingRad;
		private bool m_SupportUI = true;
		private PointF[] m_TrackedFeaturePoints;
		private List<TrackedFeature> m_TrackedFeatures;
		private int m_HistoryLevel = 0; // Indicates how much history is available for tracked points
		private int m_NotTrackedFeaturesCount;

		public Image<Bgr, Byte> CurrentImage { get; private set; }
		private Image<Gray, Byte> m_CurrentGrayImage;
		private OpticalFlow m_OpticalFlow;
		public int FoundFeaturesCount { get; private set; }

		public event EventHandler Changed;

		public VisualOdometer(Capture capture, OpticalFlow opticalFlow)
		{
			m_Capture = capture;

			m_GroundRegionTop = OdometerSettings.Default.GroundRegionTop;
			m_SkyRegionBottom = OdometerSettings.Default.SkyRegionBottom;

			m_TrackedFeatures = new List<TrackedFeature>();
			this.OpticalFlow = opticalFlow;
		}

		public OpticalFlow OpticalFlow
		{
			get { return m_OpticalFlow; }
			set
			{
				m_OpticalFlow = value;
				m_HistoryLevel = 0;
			}
		}

		public int GroundRegionTop
		{
			get { return m_GroundRegionTop; }
			set
			{
				if (value != m_GroundRegionTop)
				{
					// We ensure that the ground ends below the sky
					if (value >= m_SkyRegionBottom)
					{
						value = m_SkyRegionBottom - 1;
					}
					m_GroundRegionTop = value;
					RaiseChangedEvent();
				}
			}
		}

		public int SkyRegionBottom
		{
			get { return m_SkyRegionBottom; }
			set
			{
				if (value != m_SkyRegionBottom)
				{
					// We ensure that the sky always end above the ground
					if (value <= m_GroundRegionTop)
					{
						value = m_GroundRegionTop + 1;
					}
					m_SkyRegionBottom = value;
					RaiseChangedEvent();
				}
			}
		}

		public double HeadingRad
		{
			get { return m_HeadingRad; }
			private set
			{
				if (value != m_HeadingRad)
				{
					m_HeadingRad = value;
					RaiseChangedEvent();
				}
			}
		}

		public double HeadingDegree
		{
			get { return m_HeadingRad * 180.0 / Math.PI;  }
		}

		public void ProcessFrame()
		{
			this.CurrentImage = m_Capture.QueryFrame();

			Image<Gray, Byte> previousGrayImage = m_CurrentGrayImage;
			m_CurrentGrayImage = this.CurrentImage.Convert<Gray, Byte>();

			if (m_HistoryLevel == 0)
			{
				// We are starting and need to find features to track
				InitializeTracking();
				m_HistoryLevel++;
			}
			else
			{
				TrackFeatures(previousGrayImage);
				if (m_HistoryLevel == TrackedFeature.HistoryCount)
				{
					// grade the smoothness
				}
				else
				{
					m_HistoryLevel++;
				}
			}


	 		if (m_SupportUI)
			{
				DrawRegionBounderies();
			}
		}

		private void InitializeTracking()
		{
			m_TrackedFeaturePoints = this.OpticalFlow.FindFeaturesToTrack(m_CurrentGrayImage);
			this.FoundFeaturesCount = m_TrackedFeaturePoints.Length;
			m_TrackedFeatures = new List<TrackedFeature>(m_TrackedFeaturePoints.Length);

			for (int i = 0; i < m_TrackedFeaturePoints.Length; i++)
			{
				TrackedFeature trackedFeature = new TrackedFeature();
				trackedFeature.Add(m_TrackedFeaturePoints[i]);
				m_TrackedFeatures.Add(trackedFeature);
			}
			this.OpticalFlow.ClearPyramids();
		}

		private void TrackFeatures(Image<Gray, Byte> previousGrayImage)
		{
			OpticalFlowResult opticalFlowResult = this.OpticalFlow.CalculateOpticalFlow(previousGrayImage, m_CurrentGrayImage, m_TrackedFeaturePoints);
			m_TrackedFeaturePoints = opticalFlowResult.TrackedFeaturePoints;

			m_NotTrackedFeaturesCount = 0;
			for (int i = 0; i < m_TrackedFeaturePoints.Length; i++)
			{
				bool isTracked = opticalFlowResult.TrackingStatusIndicators[i] == 1;
				m_TrackedFeatures[i].Add(m_TrackedFeaturePoints[i], isTracked);

				if (!isTracked || m_TrackedFeatures[i].IsOut)
				{
					m_NotTrackedFeaturesCount++;
				}
			}
		}

		private void DrawRegionBounderies()
		{
			DrawRegionBoundary(this.CurrentImage, m_SkyRegionBottom);
			DrawRegionBoundary(this.CurrentImage, m_GroundRegionTop);
		}

		public int HistoryLevel
		{
			get { return m_HistoryLevel; }
		}

		public List<TrackedFeature> TrackedFeatures
		{
			get { return m_TrackedFeatures; }
		}

		public int NotTrackedFeaturesCount
		{
			get { return m_NotTrackedFeaturesCount; }
		}

		private void DrawRegionBoundary(Image<Bgr, Byte> image, int yPos)
		{
			PointF start = new PointF(0, image.Height - yPos);
			PointF end = new PointF(image.Width, image.Height - yPos);
			LineSegment2DF lineSegment = new LineSegment2DF(start, end);
			image.Draw(lineSegment, new Bgr(Color.Red), 1);
		}

		private void RaiseChangedEvent()
		{
			EventHandler handler = this.Changed;
			if (handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}

		public void Dispose()
		{
			OdometerSettings.Default.GroundRegionTop = m_GroundRegionTop;
			OdometerSettings.Default.SkyRegionBottom= m_SkyRegionBottom;
			OdometerSettings.Default.Save();

			if (this.OpticalFlow != null)
			{
				this.OpticalFlow.Dispose();
			}
		}
	}
}
