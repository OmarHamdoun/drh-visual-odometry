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
	public partial class CaptureDialog : Form
	{
		private CaptureDialogModel m_Model;

		public CaptureDialog(CaptureDialogModel model)
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
			m_TextBoxXCount.Text = m_Model.CaptureSettings.ChessBoard.XCount.ToString();
			m_TextBoxYCount.Text = m_Model.CaptureSettings.ChessBoard.YCount.ToString();

			m_CheckBoxHorizontal.Checked = m_Model.FlipHorizontal;
			m_CheckBoxVertical.Checked = m_Model.FlipVertical;

			m_TextBoxCapturedImagesCount.Text = m_Model.CurrentCapturedImagesCount.ToString();
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			Application.Idle -= ProcessFrame;
			m_Model.Dispose();
		}
	}
}
