using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisualOdometry.Utilities;

namespace VisualOdometry
{
	public class FramesCounter
	{
		private CircularBuffer<DateTime> m_FrameTimesBuffer = new CircularBuffer<DateTime>(10); // used for frames per seconds
		private double m_TicksPerSecond = (double)TimeSpan.FromSeconds(1).Ticks;

		internal FramesCounter()
		{
			this.FrameNumber = 0;
			this.FramesPerSecond = 0;
		}

		internal void Update()
		{
			m_FrameTimesBuffer.Add(DateTime.UtcNow);
			this.FrameNumber++;

			if (this.FrameNumber > 1)
			{
				this.FramesPerSecond = m_FrameTimesBuffer.Count * m_TicksPerSecond / (double)(m_FrameTimesBuffer[m_FrameTimesBuffer.Count - 1].Ticks - m_FrameTimesBuffer[0].Ticks);
			}
		}

		public int FrameNumber { get; private set; }
		public double FramesPerSecond { get; private set; }
	}
}
