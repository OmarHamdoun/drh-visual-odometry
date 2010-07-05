using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using System.IO;

namespace CameraCalibrator
{
	public class CaptureDialogModel : IDisposable
	{
		public CaptureSettings CaptureSettings { get; private set; }
		private Capture m_Capture;
		private bool m_FlipHorizontal;
		private bool m_FlipVertical;
		private DateTime m_TimeOfLastCapture = DateTime.MinValue;
		public int CurrentCapturedImagesCount { get; private set; }

		public Image<Bgr, Byte> OriginalImage { get; private set; }
		public Image<Gray, Byte> GrayImage { get; private set; }
		private PointF[] m_FoundCorners;

		public event EventHandler Changed;

		public CaptureDialogModel(CaptureSettings captureSettings)
		{
			this.CaptureSettings = captureSettings;
			InitializeCapture();
		}

		private void InitializeCapture()
		{
			m_Capture = new Capture();
		}

		public void ProcessFrame()
		{
			if (m_Capture == null)
			{
				return;
			}
			this.OriginalImage = m_Capture.QueryFrame();
			this.GrayImage = this.OriginalImage.Convert<Gray, Byte>();

			bool foundAllCorners = CameraCalibration.FindChessboardCorners(
				this.GrayImage,
				this.CaptureSettings.ChessBoard.PatternSize,
				Emgu.CV.CvEnum.CALIB_CB_TYPE.ADAPTIVE_THRESH | Emgu.CV.CvEnum.CALIB_CB_TYPE.FILTER_QUADS,
				out m_FoundCorners);

			PointF[][] foundPointsForChannels = new PointF[][] { m_FoundCorners };
			if (foundAllCorners)
			{
				MCvTermCriteria terminationCriteria;
				terminationCriteria.max_iter = 30;
				terminationCriteria.epsilon = 0.05;
				terminationCriteria.type = Emgu.CV.CvEnum.TERMCRIT.CV_TERMCRIT_EPS | Emgu.CV.CvEnum.TERMCRIT.CV_TERMCRIT_ITER;

				this.GrayImage.FindCornerSubPix(foundPointsForChannels, new Size(11, 11), new Size(-1, -1), terminationCriteria);

				CameraCalibration.DrawChessboardCorners(this.GrayImage, this.CaptureSettings.ChessBoard.PatternSize, m_FoundCorners, foundAllCorners);
			}

			// we are done with processing. If needed we flip the images for display purposes only.
			Emgu.CV.CvEnum.FLIP flipType = Emgu.CV.CvEnum.FLIP.NONE;
			if (this.FlipHorizontal)
			{
				flipType = Emgu.CV.CvEnum.FLIP.HORIZONTAL;
			}
			if (this.FlipVertical)
			{
				flipType = flipType |= Emgu.CV.CvEnum.FLIP.VERTICAL;
			}

			this.OriginalImage._Flip(flipType);
			this.GrayImage._Flip(flipType);

			if (!foundAllCorners)
			{
				return;
			}

			if (this.CurrentCapturedImagesCount >= this.CaptureSettings.ImagesCount)
			{
				// we got already all required images
				return;
			}

			DateTime utcNow = DateTime.UtcNow;
			if (utcNow.Ticks - m_TimeOfLastCapture.Ticks < this.CaptureSettings.WaitBetweenCaptures.Ticks)
			{
				// We need to wait longer
				return;
			}

			// We capture the image
			m_TimeOfLastCapture = utcNow;
			this.CurrentCapturedImagesCount++;
			this.OriginalImage.Save(this.CaptureSettings.GetFilePath(this.CurrentCapturedImagesCount));
			this.OriginalImage = this.OriginalImage.Not();

			RaiseChangedEvent();
		}

		public bool FlipHorizontal
		{
			get { return m_FlipHorizontal; }
			set
			{
				if (value != m_FlipHorizontal)
				{
					m_FlipHorizontal = value;
					RaiseChangedEvent();
				}
			}
		}

		public bool FlipVertical
		{
			get { return m_FlipVertical; }
			set
			{
				if (value != m_FlipVertical)
				{
					m_FlipVertical = value;
					RaiseChangedEvent();
				}
			}
		}

		private void RaiseChangedEvent()
		{
			EventHandler handler = this.Changed;
			if (handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}

		public void Dispose()
		{
			if (m_Capture != null)
			{
				m_Capture.Dispose();
			}
		}
	}
}
