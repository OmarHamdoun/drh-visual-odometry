using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Emgu.CV;

namespace VisualOdometry
{
	public class TranslationAnalyzer
	{
		private VisualOdometer m_VisualOdometer;
		private HomographyMatrix m_BirdsEyeViewTransformation;
		private List<PointF> m_TranslationIncrements;
		private Point m_Location;

		internal TranslationAnalyzer(VisualOdometer visualOdometer, HomographyMatrix birdsEyeViewTransformation)
		{
			m_VisualOdometer = visualOdometer;
			m_BirdsEyeViewTransformation = birdsEyeViewTransformation;
			m_TranslationIncrements = new List<PointF>();
		}

		public Point Location
		{
			get { return m_Location; }
		}

		internal void CalculateTranslation(double currentRotationIncrement)
		{
			m_TranslationIncrements.Clear();

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

				if (currentFeatureLocation.Y > m_VisualOdometer.GroundRegionTop || previousFeatureLocation.Y > m_VisualOdometer.GroundRegionTop)
				{
					continue;
				}

				//double previousAngularPlacement = Math.Atan2(previousFeatureLocation.X - m_CenterX, m_FocalLengthX);
				//double currentAngularPlacement = Math.Atan2(currentFeatureLocation.X - m_CenterX, m_FocalLengthX);
				//double rotationIncrement = previousAngularPlacement - currentAngularPlacement;
				////Debug.WriteLine(headingChange * 180.0 / Math.PI);
				//m_RotationIncrements.Add(rotationIncrement);
			}

			//Debug.WriteLine("Max delta x: " + maxAbsDeltaX);
			if (m_TranslationIncrements.Count > 0)
			{
				//double meanRotationIncrement = DetermineBestRotationIncrement();
				//m_HeadingRad += meanRotationIncrement;
				//this.CurrentRotationIncrement = meanRotationIncrement;
			}
		}

		private PointF ProjectOnFloor(PointF featurePoint)
		{
			throw new NotImplementedException();
		}
	}
}
