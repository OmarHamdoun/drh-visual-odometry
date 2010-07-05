using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CameraCalibrator
{
	public enum CalibratorState
	{
		Initial,
		FreeRunning,
		WaitingForCornersRecognition,
		CornersRecognized,
		Calibrated
	}
}
