using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualOdometry
{
	public class PoseEventArgs : EventArgs
	{
		public PoseEventArgs(Pose pose)
		{
			this.Pose = pose;
		}

		public Pose Pose { get; private set; }
	}
}
