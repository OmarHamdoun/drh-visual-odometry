using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using System.Windows;
using System.Diagnostics;

namespace VisualOdometry
{
	public class TranslationAnalyzer
	{
		private VisualOdometer m_VisualOdometer;
		private HomographyMatrix m_GroundProjectionTransformation;
		private List<TrackedFeature> m_UsedGroundFeatures;
		private List<Point> m_TranslationIncrements;
		private Point m_CurrentLocationChange;
		private Angle m_AcceptedDirectionMisalignment;

		internal TranslationAnalyzer(VisualOdometer visualOdometer, HomographyMatrix groundProjectionTransformation)
		{
			m_VisualOdometer = visualOdometer;
			m_GroundProjectionTransformation = groundProjectionTransformation;
			m_UsedGroundFeatures = new List<TrackedFeature>();
			m_TranslationIncrements = new List<Point>();
			m_AcceptedDirectionMisalignment = Angle.FromDegrees(45);
		}

		public Angle AcceptedDirectionMisalignment
		{
			get { return m_AcceptedDirectionMisalignment; }
			set { m_AcceptedDirectionMisalignment = value; }
		}

		public Point LocationChange
		{
			get { return m_CurrentLocationChange; }
		}

		internal void CalculateTranslation(Angle headingChange)
		{
			m_UsedGroundFeatures.Clear();

			double s = Math.Sin(headingChange.Rads);
			double c = Math.Cos(headingChange.Rads);

			double acceptedDirectionMisaligment = m_AcceptedDirectionMisalignment.Rads;

			m_TranslationIncrements.Clear();
			System.Drawing.PointF[] featurePointPair = new System.Drawing.PointF[2];

			double sumX = 0, sumY = 0;
			int groundFeatureCount = 0;

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

				groundFeatureCount++;

				featurePointPair[0] = trackedFeature[-1]; // previous feature location
				featurePointPair[1] = trackedFeature[0];  // current featue location
				//Debug.WriteLine("Raw:");
				//Debug.WriteLine("\tPrevious dx_r: {0:0.000}  dy_r: {1:0.000}", featurePointPair[0].X, featurePointPair[0].Y);
				//Debug.WriteLine("\tCurrent  dx_r: {0:0.000}  dy_r: {1:0.000}", featurePointPair[1].X, featurePointPair[1].Y);

				ProjectOnFloor(featurePointPair);
				//Debug.WriteLine("Ground:");
				//Debug.WriteLine("\tPrevious dx_r: {0:0.000}  dy_r: {1:0.000}", featurePointPair[0].X, featurePointPair[0].Y);
				//Debug.WriteLine("\tCurrent  dx_r: {0:0.000}  dy_r: {1:0.000}", featurePointPair[1].X, featurePointPair[1].Y);

	
				// Remove rotation effect on current feature location. The center of the rotation is the previous feature location
				Point rotationCorrectedEndPoint = new Point(
					c * featurePointPair[1].X - s * featurePointPair[1].Y,
					s * featurePointPair[1].X + c * featurePointPair[1].Y);

				Point translationIncrement = new Point(
					featurePointPair[0].X - rotationCorrectedEndPoint.X,
					featurePointPair[0].Y - rotationCorrectedEndPoint.Y);

				double translationAngle = Math.Abs(Math.Atan2(translationIncrement.X, translationIncrement.Y));
				//Debug.WriteLine(translationAngle * 180 / Math.PI);
				if (translationAngle > acceptedDirectionMisaligment)
				{
					continue;
				}

				m_UsedGroundFeatures.Add(trackedFeature);
				m_TranslationIncrements.Add(translationIncrement);
				sumX += translationIncrement.X;
				sumY += translationIncrement.Y;
			}

			//Debug.WriteLine("Used ground features %: " + ((double)m_TranslationIncrements.Count/(double)groundFeatureCount).ToString());

			if (m_TranslationIncrements.Count > 0)
			{
				m_CurrentLocationChange = new Point(sumX / m_TranslationIncrements.Count, sumY / m_TranslationIncrements.Count);
			}
			//Debug.WriteLine("Average: dx_r: {0:0.000}  dy_r: {1:0.000}", m_CurrentLocationChange.X, m_CurrentLocationChange.Y);
		}

		public System.Drawing.PointF RemoveRotationEffect(Angle headingChange, System.Drawing.PointF point)
		{
			float s = (float)Math.Sin(headingChange.Rads);
			float c = (float)Math.Cos(headingChange.Rads);

			return new System.Drawing.PointF(
				c * point.X - s * point.Y,
				s * point.X + c * point.Y);
		}

		private void ProjectOnFloor(System.Drawing.PointF[] featurePoints)
		{
			m_GroundProjectionTransformation.ProjectPoints(featurePoints);
		}

		public List<TrackedFeature> UsedGroundFeatures
		{
			get { return m_UsedGroundFeatures; }
		}
	}
}
