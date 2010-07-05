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
	public partial class CaptureSettingsDialog : Form
	{
		private CaptureSettings m_Model;

		public CaptureSettingsDialog(CaptureSettings captureSettings)
		{
			m_Model = captureSettings;
			InitializeComponent();

			UpdateFromModel();
			//m_Model.Changed += new EventHandler(OnModelChanged);
		}

		private void OnModelChanged(object sender, EventArgs e)
		{
			UpdateFromModel();
		}

		private void UpdateFromModel()
		{
			m_XCountTextBox.Text = m_Model.ChessBoardX.ToString();
			m_YCountTextBox.Text = m_Model.ChessBoardY.ToString();
			m_OutputFolderTextBox.Text = m_Model.OutputFolderPath;
			m_ImagesCountTextBox.Text = m_Model.ImagesCount.ToString();
			m_WaitBetweenCapturesTextBox.Text = m_Model.WaitBetweenCaptures.ToString();
		}

		private void OnCancelButtonClicked(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void OnOKButtonClicked(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();

			int chessBoardX;
			if (Int32.TryParse(m_XCountTextBox.Text, out chessBoardX))
			{
				m_Model.ChessBoardX = chessBoardX;
			}

			int chessBoardY;
			if (Int32.TryParse(m_YCountTextBox.Text, out chessBoardY))
			{
				m_Model.ChessBoardY = chessBoardY;
			}

			m_Model.OutputFolderPath = m_OutputFolderTextBox.Text;

			int imagesCount;
			if (Int32.TryParse(m_ImagesCountTextBox.Text, out imagesCount))
			{
				m_Model.ImagesCount = imagesCount;
			}

			TimeSpan waitBetweenCaptures;
			if (TimeSpan.TryParse(m_WaitBetweenCapturesTextBox.Text, out waitBetweenCaptures))
			{
				m_Model.WaitBetweenCaptures = waitBetweenCaptures;
			}
		}
	}
}
