using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using System.Diagnostics;

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
		}

		private void RepopulateFeaturePoints()
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
			//this.OpticalFlow.ClearPyramids();

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

			int fullHistoryFeaturesCount = 0;
			int unsmoothFeaturesCount = 0;
			for (int i = trackedFeaturePoints.Length - 1; i >= 0; i--)
			{
				bool isTracked = opticalFlowResult.TrackingStatusIndicators[i] == 1;
				if (isTracked)
				{
					TrackedFeature trackedFeature = m_TrackedFeatures[i];
					trackedFeature.Add(trackedFeaturePoints[i]);

					if (trackedFeature.HasFullHistory)
					{
						fullHistoryFeaturesCount++;
						if (!trackedFeature.IsSmooth)
						{
							unsmoothFeaturesCount++;
						}
					}
				}
				else
				{
					RemoveTrackedFeature(i);
				}
			}

			if (unsmoothFeaturesCount < fullHistoryFeaturesCount / 2)
			{
				// The majority of features is smooth. We downgrade unsmooth features
				Debug.WriteLine("Consensus: Is smooth");
				ApplyUnsmoothGrades();
			}
			else
			{
				// Consensus not smooth; not downgrading unsmooth features.
				Debug.WriteLine("Consensus: Is not smooth");
			}

			if (m_TrackedFeatures.Count < m_ThresholdForFeatureRepopulation)
			{
				RepopulateFeaturePoints();
			}
		}

		private void RemoveTrackedFeature(int index)
		{
			m_NotTrackedFeaturesCount++;
			m_TrackedFeatures.RemoveAt(index);
		}

		private void ApplyUnsmoothGrades()
		{
			int unsmoothFeaturesOutCout = 0;
			for (int i = m_TrackedFeatures.Count - 1; i >= 0; i--)
			{
				TrackedFeature trackedFeature = m_TrackedFeatures[i];
				trackedFeature.ApplyScoreChange();
				if (trackedFeature.IsOut)
				{
					RemoveTrackedFeature(i);
					unsmoothFeaturesOutCout++;
				}
			}

			Debug.WriteLine("Number of unsmooth features weeded out: " + unsmoothFeaturesOutCout);
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
