using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace VisualOdometry
{
	public class RotationAnalyzer
	{
		private VisualOdometer m_VisualOdometer;
		private double m_FocalLengthX;
		private double m_CenterX;

		private double m_CumulativeRotationRad;
		private List<double> m_RotationIncrements;

		internal RotationAnalyzer(VisualOdometer visualOdometer)
		{
			m_VisualOdometer = visualOdometer;
			m_FocalLengthX = visualOdometer.CameraParameters.Intrinsic.Fx;
			m_CenterX = visualOdometer.CameraParameters.Intrinsic.Cx;

			m_RotationIncrements = new List<double>();
		}

		public double CurrentRotationIncrement { get; private set; }

		public double CumulativeRotationRad
		{
			get { return m_CumulativeRotationRad; }
		}

		public double CumulativeRotationDegree
		{
			get { return m_CumulativeRotationRad * VisualOdometer.RadToDegree; }
		}

		public List<double> HeadingChanges
		{
			get { return m_RotationIncrements; }
		}

		internal void CalculateRotation()
		{
			m_RotationIncrements.Clear();

			List<TrackedFeature> trackedFeatures = m_VisualOdometer.TrackedFeatures;
			for (int i = 0; i < trackedFeatures.Count; i++)
			{
				TrackedFeature trackedFeature = trackedFeatures[i];
				if (trackedFeature.Count < 2)
				{
					continue;
				}
				PointF previousFeatureLocation = trackedFeature[-1];
				PointF currentFeatureLocation = trackedFeature[0];
				//double absDeltaX = Math.Abs(currentFeatureLocation.X - previousFeatureLocation.X);
				//if (absDeltaX > maxAbsDeltaX)
				//{
				//    maxAbsDeltaX = absDeltaX;
				//}

				if (currentFeatureLocation.Y <= m_VisualOdometer.SkyRegionBottom)
				{
					double previousAngularPlacement = Math.Atan2(previousFeatureLocation.X - m_CenterX, m_FocalLengthX);
					double currentAngularPlacement = Math.Atan2(currentFeatureLocation.X - m_CenterX, m_FocalLengthX);
					double rotationIncrement = previousAngularPlacement - currentAngularPlacement;
					//Debug.WriteLine(headingChange * 180.0 / Math.PI);
					m_RotationIncrements.Add(rotationIncrement);
				}
			}

			//Debug.WriteLine("Max delta x: " + maxAbsDeltaX);
			if (m_RotationIncrements.Count > 0)
			{
				double meanRotationIncrement = CalculateMeanRotationIncrement();
				m_CumulativeRotationRad += meanRotationIncrement;
				this.CurrentRotationIncrement = meanRotationIncrement;
			}
		}

		private double CalculateMeanRotationIncrement()
		{
			m_RotationIncrements.Sort();
			double meanRotationIncrement = m_RotationIncrements[m_RotationIncrements.Count / 2];
			return meanRotationIncrement;
		}
	}
}
