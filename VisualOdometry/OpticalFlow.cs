using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using System.Drawing;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

namespace VisualOdometry
{
	public class OpticalFlow : IDisposable
	{
		private const int c_WinSize = 10;

		private int m_MaxFeatureCount;
		private int m_BlockSize;
		private double m_QualityLevel;
		private double m_MinDistance;

		private MCvTermCriteria m_SubCornerTerminationCriteria;
		private MCvTermCriteria m_OpticalFlowTerminationCriteria;

		public OpticalFlowResult OpticalFlowResult { get; private set; }

		private Image<Gray, Byte> m_PreviousPyrBufferParam;
		private Image<Gray, Byte> m_CurrentPyrBufferParam;

		public event EventHandler Changed;

		public OpticalFlow() : this(
			OpticalFlowSettings.Default.MaxFeatureCount,
			OpticalFlowSettings.Default.BlockSize,
			OpticalFlowSettings.Default.QualityLevel,
			OpticalFlowSettings.Default.MinDistance)
		{
		}

		public OpticalFlow(int maxFeatureCount, int blockSize, double qualityLevel, double minDistance)
		{
			m_MaxFeatureCount = maxFeatureCount;
			m_BlockSize = blockSize;
			m_QualityLevel = qualityLevel;
			m_MinDistance = minDistance;

			m_SubCornerTerminationCriteria.max_iter = 20;
			m_SubCornerTerminationCriteria.epsilon = 0.1;
			m_SubCornerTerminationCriteria.type = Emgu.CV.CvEnum.TERMCRIT.CV_TERMCRIT_EPS | Emgu.CV.CvEnum.TERMCRIT.CV_TERMCRIT_ITER;

			m_OpticalFlowTerminationCriteria.max_iter = 20;
			m_OpticalFlowTerminationCriteria.epsilon = 0.3;
			m_OpticalFlowTerminationCriteria.type = Emgu.CV.CvEnum.TERMCRIT.CV_TERMCRIT_EPS | Emgu.CV.CvEnum.TERMCRIT.CV_TERMCRIT_ITER;

			this.OpticalFlowResult = new OpticalFlowResult();
		}

		public int MaxFeatureCount
		{
			get { return m_MaxFeatureCount; }
			set
			{
				if (value != m_MaxFeatureCount)
				{
					m_MaxFeatureCount = value;
					RaiseChangedEvent();
				}
			}
		}

		public int BlockSize
		{
			get { return m_BlockSize; }
			set
			{
				if (value != m_BlockSize)
				{
					m_BlockSize = value;
					RaiseChangedEvent();
				}
			}
		}

		public double QualityLevel
		{
			get { return m_QualityLevel; }
			set
			{
				if (value != m_QualityLevel)
				{
					m_QualityLevel = value;
					RaiseChangedEvent();
				}
			}
		}

		public double MinDistance
		{
			get { return m_MinDistance; }
			set
			{
				if (value != m_MinDistance)
				{
					m_MinDistance = value;
					RaiseChangedEvent();
				}
			}
		}

		//public int PreviousFoundFeaturesCount
		//{
		//    get
		//    {
		//        if (this.PreviousFoundFeatures == null)
		//        {
		//            return 0;
		//        }
		//        return this.PreviousFoundFeatures.Length;
		//    }
		//}

		//public int CurrentFoundFeaturesCount
		//{
		//    get
		//    {
		//        if (this.CurrentFoundFeatures == null)
		//        {
		//            return 0;
		//        }
		//        return this.CurrentFoundFeatures.Length;
		//    }
		//}

		//public int TrackedFeaturesCount
		//{
		//    get
		//    {
		//        if (this.TrackedFeatures == null)
		//        {
		//            return 0;
		//        }
		//        return this.TrackedFeatures.Length;
		//    }
		//}

		//public int NotTrackedFeaturesCount { get; private set; }

		internal PointF[] FindFeaturesToTrack(Image<Gray, Byte> grayImage)
		{
			PointF[][] foundFeaturesInChannels = grayImage.GoodFeaturesToTrack(this.MaxFeatureCount, this.QualityLevel, this.MinDistance, this.BlockSize);
			// Next we refine the location of the found features
			grayImage.FindCornerSubPix(foundFeaturesInChannels, new Size(c_WinSize, c_WinSize), new Size(-1, -1), m_SubCornerTerminationCriteria);
			return foundFeaturesInChannels[0];
		}

		internal void ClearPyramids()
		{
			if (m_PreviousPyrBufferParam != null)
			{
				m_PreviousPyrBufferParam.Dispose();
			}
			m_PreviousPyrBufferParam = null;

			if (m_CurrentPyrBufferParam != null)
			{
				m_CurrentPyrBufferParam.Dispose();
			}
			m_CurrentPyrBufferParam = null;
		}

		//public void ProcessFrame()
		//{
		//    this.CurrentImage = m_Capture.QueryFrame();
		//    this.FlowImage = this.CurrentImage.Clone();

		//    this.PreviousGrayImage = this.CurrentGrayImage;
		//    this.CurrentGrayImage = this.CurrentImage.Convert<Gray, Byte>();


		//    DrawFoundFeaturesMarkers();

		//    if (this.PreviousGrayImage == null)
		//    {
		//        m_PreviousPyrBufferParam = new Image<Gray, byte>(this.CurrentImage.Width + 8, this.CurrentImage.Height / 3);
		//        m_CurrentPyrBufferParam = new Image<Gray, byte>(this.CurrentImage.Width + 8, this.CurrentImage.Height / 3);
		//    }
		//    else
		//    {
		//        CalculateOpticalFlow();
		//        DrawTrackedFeaturesMarkers();
		//        DrawFlowVectors();
		//    }
		//}

		//private void DrawFoundFeaturesMarkers()
		//{
		//    foreach (PointF foundFeature in this.CurrentFoundFeatures)
		//    {
		//        CircleF circle = new CircleF(foundFeature, 3.0f);
		//        this.CurrentImage.Draw(circle, new Bgr(Color.Lime), 2);
		//    }
		//}

		internal OpticalFlowResult CalculateOpticalFlow(Image<Gray, Byte> previousGrayImage, Image<Gray, Byte> currentGrayImage, PointF[] previousFoundFeaturePoints)
		{
			LKFLOW_TYPE flags = LKFLOW_TYPE.DEFAULT;
			if (m_PreviousPyrBufferParam != null)
			{
				// We have a prefilled pyramid
				m_PreviousPyrBufferParam = m_CurrentPyrBufferParam;
				flags = LKFLOW_TYPE.CV_LKFLOW_PYR_A_READY;
			}
			else
			{
				m_PreviousPyrBufferParam = new Image<Gray, byte>(currentGrayImage.Width + 8, currentGrayImage.Height / 3);
				m_CurrentPyrBufferParam = new Image<Gray, byte>(currentGrayImage.Width + 8, currentGrayImage.Height / 3);
			}

			PointF[] trackedFeaturePoints;
			float[] trackingErrors;
			byte[] trackingStatusIndicators;

			Emgu.CV.OpticalFlow.PyrLK(
				previousGrayImage,
				currentGrayImage,
				m_PreviousPyrBufferParam,
				m_CurrentPyrBufferParam,
				previousFoundFeaturePoints,
				new Size(c_WinSize, c_WinSize),
				5, // level
				m_OpticalFlowTerminationCriteria,
				flags,
				out trackedFeaturePoints,
				out trackingStatusIndicators,
				out trackingErrors);

			this.OpticalFlowResult.TrackedFeaturePoints = trackedFeaturePoints;
			this.OpticalFlowResult.TrackingStatusIndicators = trackingStatusIndicators;
			this.OpticalFlowResult.TrackingErrors = trackingErrors;

			return this.OpticalFlowResult;

			//int notTrackedFeatures = 0;
			//for (int i = 0; i < m_TrackingStatus.Length; i++)
			//{
			//    if (m_TrackingStatus[i] == 0)
			//    {
			//        notTrackedFeatures++;
			//    }
			//}
			//this.NotTrackedFeaturesCount = notTrackedFeatures;
		}

		//private void DrawTrackedFeaturesMarkers()
		//{
		//    for (int i = 0; i < this.TrackedFeatures.Length; i++)
		//    {
		//        if (m_TrackingStatus[i] == 1)
		//        {
		//            CircleF circle = new CircleF(this.TrackedFeatures[i], 3.0f);
		//            this.CurrentImage.Draw(circle, new Bgr(Color.Red), 2);
		//        }
		//    }
		//}

		//private void DrawFlowVectors()
		//{
		//    for (int i = 0; i < this.TrackedFeatures.Length; i++)
		//    {
		//        if (m_TrackingStatus[i] == 1)
		//        {
		//            LineSegment2DF lineSegment = new LineSegment2DF(this.PreviousFoundFeatures[i], this.TrackedFeatures[i]);
		//            this.FlowImage.Draw(lineSegment, new Bgr(Color.Red), 1);
		//            CircleF circle = new CircleF(this.TrackedFeatures[i], 2.0f);
		//            this.FlowImage.Draw(circle, new Bgr(Color.Red), 1);
		//        }
		//    }
		//}

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
			OpticalFlowSettings.Default.MaxFeatureCount = this.MaxFeatureCount;
			OpticalFlowSettings.Default.BlockSize = this.BlockSize;
			OpticalFlowSettings.Default.QualityLevel = this.QualityLevel;
			OpticalFlowSettings.Default.MinDistance = this.MinDistance;
			OpticalFlowSettings.Default.Save();
		}
	}
}
