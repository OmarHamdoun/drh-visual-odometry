using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace CameraCalibrator.WinForm.Support
{
	public class TestFormModel : IDisposable
	{
		private Capture m_Capture;
		private IntrinsicCameraParameters m_IntrinsicCameraParameters;

		public Image<Bgr, Byte> OriginalImage { get; private set; }
		public Image<Gray, Byte> GrayImage { get; private set; }
		public Image<Bgr, Byte> CorrectedImage { get; private set; }

		private static Matrix<float> m_UndistortMapX;
		private static Matrix<float> m_UndistortMapY;

		public TestFormModel(IntrinsicCameraParameters intrinsicCameraParameters)
		{
			m_IntrinsicCameraParameters = intrinsicCameraParameters;
			m_Capture = new Capture();

			Initialize();
		}

		private void Initialize()
		{
			this.OriginalImage = m_Capture.QueryFrame();
			//this.GrayImage = this.OriginalImage.Convert<Gray, Byte>();

			m_IntrinsicCameraParameters.InitUndistortMap(
				this.OriginalImage.Width,
				this.OriginalImage.Height,
				out m_UndistortMapX,
				out m_UndistortMapY);
		}

		public void ProcessFrame()
		{
			if (m_Capture == null)
			{
				return;
			}
			this.OriginalImage = m_Capture.QueryFrame();
			this.GrayImage = this.OriginalImage.Convert<Gray, Byte>();

			Image<Gray, Byte> grayImage = this.OriginalImage.Convert<Gray, Byte>();
			this.CorrectedImage = m_IntrinsicCameraParameters.Undistort<Bgr, Byte>(this.OriginalImage);
		}

		public void Dispose()
		{
			if (m_Capture != null)
			{
				m_Capture.Dispose();
				m_Capture = null;
			}
		}
	}
}
