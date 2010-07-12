using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace VisualOdometry
{
	public class OpticalFlowResult
	{
		public PointF[] TrackedFeaturePoints { get; set; }
		public byte[] TrackingStatusIndicators { get; set; }
		public float[] TrackingErrors { get; set; }
	}
}
