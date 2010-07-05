using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using CameraCalibrator.WinForm.Support;

namespace CameraCalibrator
{
	public class CalibrationParametersFormModel
	{
		private Capture m_Capture;

		public CalibrationParametersFormModel(IntrinsicCameraParameters intrinsicCameraParameters, Capture capture)
		{
			m_Capture = capture;
			this.IntrinsicCameraParameters = intrinsicCameraParameters;
		}

		public IntrinsicCameraParameters IntrinsicCameraParameters { get; private set; }

		public TestFormModel CreateTestFormModel()
		{
			return new TestFormModel(this.IntrinsicCameraParameters);
		}
	}
}
