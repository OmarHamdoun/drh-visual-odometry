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
		//private PointF[] m_TrackedFeaturePoints;
		private List<TrackedFeature> m_TrackedFeatures;
		private int m_NotTrackedFeaturesCount;

		public Image<Bgr, Byte> CurrentImage { get; private set; }
		private Image<Gray, Byte> m_CurrentGrayImage;
		private OpticalFlow m_OpticalFlow;
		public int InitialFeaturesCount { get; private set; }
		private int m_ThresholdForFeatureRepopulation;

		public event EventHandler Changed;

		public VisualOdometer(Capture capture, OpticalFlow opticalFlow)
		{
			m_Capture = capture;

			m_GroundRegionTop = OdometerSettings.Default.GroundRegionTop;
			m_SkyRegionBottom = OdometerSettings.Default.SkyRegionBottom;

			this.OpticalFlow = opticalFlow;
		}

		public OpticalFlow OpticalFlow
		{
			get { return m_OpticalFlow; }
			set
			{
				m_OpticalFlow = value;
				m_TrackedFeatures = new List<TrackedFeature>();
				m_ThresholdForFeatureRepopulation = int.MaxValue;
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
			if (this.CurrentImage == null)
			{
				// This occurs if we operate against a previously recorded video and the video has ended.
				return;
			}

			Image<Gray, Byte> previousGrayImage = m_CurrentGrayImage;
			m_CurrentGrayImage = this.CurrentImage.Convert<Gray, Byte>();

			if (previousGrayImage == null)
			{
				// This occurs the first time we process a frame.
				return;
			}
			TrackFeatures(previousGrayImage);

			//if (m_HistoryLevel == 0)
			//{
			//    // We are starting and need to find features to track
			//    PopulateFeaturePoints();
			//    m_HistoryLevel++;
			//}
			//else
			//{
			//    TrackFeatures(previousGrayImage);
			//    if (m_HistoryLevel == TrackedFeature.HistoryCount)
			//    {
			//        // grade the smoothness
			//    }
			//    else
			//    {
			//        m_HistoryLevel++;
			//    }
			//}
		}

		private void PopulateFeaturePoints()
		{
			PointF[] newTrackedFeaturePoints = this.OpticalFlow.FindFeaturesToTrack(
				m_CurrentGrayImage,
				m_TrackedFeatures,
				m_SkyRegionBottom,
				m_GroundRegionTop);

			for (int i = 0; i < newTrackedFeaturePoints.Length; i++)
			{
				TrackedFeature trackedFeature = new TrackedFeature();
				trackedFeature.Add(newTrackedFeaturePoints[i]);
				m_TrackedFeatures.Add(trackedFeature);
			}
			this.OpticalFlow.ClearPyramids();

			this.InitialFeaturesCount = m_TrackedFeatures.Count;
			m_ThresholdForFeatureRepopulation = this.InitialFeaturesCount * 9 / 10;
			m_NotTrackedFeaturesCount = 0;
		}

		private void TrackFeatures(Image<Gray, Byte> previousGrayImage)
		{
			PointF[] trackedFeaturePoints = new PointF[m_TrackedFeatures.Count];
			for (int i = 0; i < trackedFeaturePoints.Length; i++)
			{
				trackedFeaturePoints[i] = m_TrackedFeatures[i][0];
			}
			OpticalFlowResult opticalFlowResult = this.OpticalFlow.CalculateOpticalFlow(previousGrayImage, m_CurrentGrayImage, trackedFeaturePoints);
			trackedFeaturePoints = opticalFlowResult.TrackedFeaturePoints;

			for (int i = trackedFeaturePoints.Length - 1; i >= 0; i--)
			{
				bool isTracked = opticalFlowResult.TrackingStatusIndicators[i] == 1;
				if (isTracked && !m_TrackedFeatures[i].IsOut)
				{
					m_TrackedFeatures[i].Add(trackedFeaturePoints[i], isTracked);
				}
				else
				{
					m_NotTrackedFeaturesCount++;
					m_TrackedFeatures.RemoveAt(i);
				}
			}

			if (m_TrackedFeatures.Count < m_ThresholdForFeatureRepopulation)
			{
				PopulateFeaturePoints();
			}
		}

		public List<TrackedFeature> TrackedFeatures
		{
			get { return m_TrackedFeatures; }
		}

		public int NotTrackedFeaturesCount
		{
			get { return m_NotTrackedFeaturesCount; }
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
