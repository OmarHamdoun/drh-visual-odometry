using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualOdometry
{
	public class HistogramBin
	{
		public double Min { get; private set; }
		public double Max { get; private set; }
		public int Count { get; set; }

		public HistogramBin(double min, double max)
		{
			this.Min = min;
			this.Max = max;
		}
	}
}
