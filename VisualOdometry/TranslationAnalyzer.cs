using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using System.Windows;

namespace VisualOdometry
{
	public class TranslationAnalyzer
	{
		private VisualOdometer m_VisualOdometer;
		private HomographyMatrix m_GroundProjectionTransformation;
		private List<Point> m_TranslationIncrements;
		private Point m_CurrentLocationChange;

		internal TranslationAnalyzer(VisualOdometer visualOdometer, HomographyMatrix groundProjectionTransformation)
		{
			m_VisualOdometer = visualOdometer;
			m_GroundProjectionTransformation = groundProjectionTransformation;
			m_TranslationIncrements = new List<Point>();
		}

		public Point LocationChange
		{
			get { return m_CurrentLocationChange; }
		}

		internal void CalculateTranslation(Angle headingChange)
		{
			double s = Math.Sin(headingChange.Rads);
			double c = Math.Cos(headingChange.Rads);

			m_TranslationIncrements.Clear();
			System.Drawing.PointF[] featurePointPair = new System.Drawing.PointF[2];

			double sumX = 0, sumY = 0;

			List<TrackedFeature> trackedFeatures = m_VisualOdometer.TrackedFeatures;
			for (int i = 0; i < trackedFeatures.Count; i++)
			{
				TrackedFeature trackedFeature = trackedFeatures[i];
				if (trackedFeature.Count < 2)
				{
					continue;
				}

				// previous and current feature points need to be in the ground region
				if (!(trackedFeature[-1].Y > m_VisualOdometer.GroundRegionTop && trackedFeature[0].Y > m_VisualOdometer.GroundRegionTop))
				{
					continue;
				}

				featurePointPair[0] = trackedFeature[-1]; // previous feature location
				featurePointPair[1] = trackedFeature[0];  // current featue location

				ProjectOnFloor(featurePointPair);
				// Remove rotation effect on current feature location. The center of the rotation is (0,0) on the ground plane
				Point rotationCorrectedEndPoint = new Point(
					c * featurePointPair[1].X - s * featurePointPair[1].Y,
					s * featurePointPair[1].X + c * featurePointPair[1].Y);

				Point translationIncrement = new Point(
					rotationCorrectedEndPoint.X - featurePointPair[0].X,
					rotationCorrectedEndPoint.Y - featurePointPair[0].Y);

				m_TranslationIncrements.Add(translationIncrement);
				sumX += translationIncrement.X;
				sumY += translationIncrement.Y;
			}

			if (m_TranslationIncrements.Count > 0)
			{
				m_CurrentLocationChange = new Point(sumX / m_TranslationIncrements.Count, sumY / m_TranslationIncrements.Count);
			}
		}

		private void ProjectOnFloor(System.Drawing.PointF[] featurePoints)
		{
			m_GroundProjectionTransformation.ProjectPoints(featurePoints);
		}
	}
}
