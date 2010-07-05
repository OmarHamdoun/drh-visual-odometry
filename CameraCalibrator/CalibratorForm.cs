using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CameraCalibrator
{
	public partial class CalibratorForm : Form
	{
		private CalibratorFormModel m_Model;

		public CalibratorForm(CalibratorFormModel model)
		{
			m_Model = model;

			InitializeComponent();
			m_ImagesSplitter.SplitterDistance = m_ImagesSplitter.Width / 2;
			
			UpdateFromModel();
			Application.Idle += ProcessFrame;
			m_Model.Changed += new EventHandler(OnModelChanged);
		}

		private void ProcessFrame(object sender, EventArgs e)
		{
			m_Model.ProcessFrame();

			m_RawImageBoxWithHeading.ImageBox.Image = m_Model.OriginalImage;
			m_CornersImageBoxWithHeading.ImageBox.Image = m_Model.GrayImage;
		}

		private void OnFlipChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			if (sender == m_CheckBoxHorizontal)
			{
				m_Model.FlipHorizontal = m_CheckBoxHorizontal.Checked;
			}
			if (sender == m_CheckBoxVertical)
			{
				m_Model.FlipVertical = m_CheckBoxVertical.Checked;
			}
		}

		private void OnModelChanged(object sender, EventArgs e)
		{
			UpdateFromModel();
		}

		private void UpdateFromModel()
		{
			m_TextBoxXCount.Text = m_Model.ChessBoard.XCount.ToString();
			m_TextBoxYCount.Text = m_Model.ChessBoard.YCount.ToString();

			m_CheckBoxHorizontal.Checked = m_Model.FlipHorizontal;
			m_CheckBoxVertical.Checked = m_Model.FlipVertical;

			m_ButtonCapture.Enabled = m_Model.CanStartCapture;
			m_ButtonAccept.Enabled = m_Model.CanAccept;
			m_ButtonReject.Enabled = m_Model.CanReject;

			m_TextBoxCapturedImagesCount.Text = m_Model.AcceptedImagesCount.ToString();
		}

		private void OnCaptureButtonClicked(object sender, EventArgs e)
		{
			m_Model.StartCapture();
		}

		private void OnAcceptButtonClick(object sender, EventArgs e)
		{
			m_Model.Accept();
		}

		private void OnRejectButtonClick(object sender, EventArgs e)
		{
			m_Model.Reject();
		}

		private void OnCalibrateButtonClick(object sender, EventArgs e)
		{
			m_Model.Calibrate();
			CalibrationParametersFormModel calibrationParametersFormModel = m_Model.CreateCalibrationParametersFormModel();

			CalibrationParametersForm calibrationParametersForm = new CalibrationParametersForm(calibrationParametersFormModel);
			calibrationParametersForm.ShowDialog();
		}
	}
}
