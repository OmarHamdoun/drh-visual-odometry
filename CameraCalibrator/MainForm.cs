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
	public partial class MainForm : Form
	{
		private MainModel m_Model;

		public MainForm(MainModel mainModel)
		{
			InitializeComponent();
			m_Model = mainModel;

			UpdateFromModel();
			m_Model.Changed += new EventHandler(OnModelChanged);
		}

		private void OnModelChanged(object sender, EventArgs e)
		{
			UpdateFromModel();
		}

		private void UpdateFromModel()
		{
		}

		private void OnSettingsButtonClicked(object sender, EventArgs e)
		{
			//CaptureSettings captureSettings = m_Model.CaptureSettings.Clone();

			CaptureSettingsDialog captureSettingsDialog = new CaptureSettingsDialog(m_Model.CaptureSettings);
			DialogResult result = captureSettingsDialog.ShowDialog();
		}

		private void OnCaptureImagesButtonClicked(object sender, EventArgs e)
		{
			CaptureDialogModel captureDialogModel = new CaptureDialogModel(m_Model.CaptureSettings);
			CaptureDialog captureDialog = new CaptureDialog(captureDialogModel);

			captureDialog.ShowDialog();
		}

		private void OnCalibrateButtonClicked(object sender, EventArgs e)
		{
			CalibrateDialogModel calibrateDialogModel = new CalibrateDialogModel(m_Model);
			CalibrateDialog calibrateDialog = new CalibrateDialog(calibrateDialogModel);
			calibrateDialog.ShowDialog();
		}
	}
}
