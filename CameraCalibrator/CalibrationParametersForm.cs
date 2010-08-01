using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using CameraCalibrator.WinForm.Support;
using CameraCalibrator.Support;

namespace CameraCalibrator
{
	public partial class CalibrationParametersForm : Form
	{
		private CalibrationParametersFormModel m_Model;

		public CalibrationParametersForm(CalibrationParametersFormModel model)
		{
			m_Model = model;
			InitializeComponent();
			UpdateFromModel();
		}

		private void UpdateFromModel()
		{
			m_TextBoxFx.Text = m_Model.IntrinsicCameraParameters.IntrinsicMatrix[0, 0].ToString();
			m_TextBoxFy.Text = m_Model.IntrinsicCameraParameters.IntrinsicMatrix[1, 1].ToString();

			m_TextBoxCx.Text = m_Model.IntrinsicCameraParameters.IntrinsicMatrix[0, 2].ToString();
			m_TextBoxCy.Text = m_Model.IntrinsicCameraParameters.IntrinsicMatrix[1, 2].ToString();

			m_TextBoxK1.Text = m_Model.IntrinsicCameraParameters.DistortionCoeffs[0, 0].ToString();
			m_TextBoxK2.Text = m_Model.IntrinsicCameraParameters.DistortionCoeffs[1, 0].ToString();
			m_TextBoxK3.Text = m_Model.IntrinsicCameraParameters.DistortionCoeffs[4, 0].ToString();

			m_TextBoxP1.Text = m_Model.IntrinsicCameraParameters.DistortionCoeffs[2, 0].ToString();
			m_TextBoxP2.Text = m_Model.IntrinsicCameraParameters.DistortionCoeffs[3, 0].ToString();
		}

		private void OnTestButtonClick(object sender, EventArgs e)
		{
			using (TestFormModel testFormModel = m_Model.CreateTestFormModel())
			{
				TestForm testForm = new TestForm(testFormModel);
				testForm.ShowDialog();
			}
		}

		private void OnButtonSaveClick(object sender, EventArgs e)
		{
			IntrinsicParametersSupport.Save(m_Model.IntrinsicCameraParameters, "CameraParameters.txt");
		}
	}
}
