using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualOdometry
{
	public class RobotPath
	{
		private List<Pose> m_Poses;
		private IList<Pose> m_ReadOnlyPoses;
		private List<DateTime> m_UtcTimeStamps;
		private IList<DateTime> m_ReadOnlyUtcTimeStamps;

		private double m_MinX, m_MaxX, m_MinY, m_MaxY;

		public RobotPath()
		{
			m_Poses = new List<Pose>();
			m_ReadOnlyPoses = m_Poses.AsReadOnly();

			m_UtcTimeStamps = new List<DateTime>();
			m_ReadOnlyUtcTimeStamps = m_UtcTimeStamps.AsReadOnly();
		}

		public void Add(Pose currentPose)
		{
			Add(DateTime.UtcNow, currentPose);
		}

		public void Add(DateTime timeStamp, Pose pose)
		{
			m_UtcTimeStamps.Add(timeStamp.ToUniversalTime());
			m_Poses.Add(pose);

			if (pose.X < m_MinX)
			{
				m_MinX = pose.X;
			}
			if (pose.X > m_MaxX)
			{
				m_MaxX = pose.X;
			}

			if (pose.Y < m_MinY)
			{
				m_MinY = pose.Y;
			}
			if (pose.Y > m_MaxY)
			{
				m_MaxY = pose.Y;
			}
		}

		public IList<Pose> Poses
		{
			get { return m_ReadOnlyPoses; }
		}

		public IList<DateTime> UtcTimeStamps
		{
			get { return m_ReadOnlyUtcTimeStamps; }
		}

		public double MinX { get { return m_MinX; } }
		public double MaxX { get { return m_MaxX; } }
		public double MinY { get { return m_MinY; } }
		public double MaxY { get { return m_MaxY; } }
	}
}
