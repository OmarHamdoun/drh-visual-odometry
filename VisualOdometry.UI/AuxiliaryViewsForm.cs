using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace VisualOdometry.UI
{
	public partial class AuxiliaryViewsForm : Form
	{
		private HomographyMatrix m_BirdsEyeViewTransformation;
		private Image<Bgr, Byte> m_BirdsEyeViewImage;

		public AuxiliaryViewsForm(HomographyMatrix birdsEyeViewTransformation)
		{
			InitializeComponent();
			this.ShowInTaskbar = false;
			m_BirdsEyeViewTransformation = birdsEyeViewTransformation;
		}

		internal void Update(VisualOdometer visualOdometer)
		{
			if (!this.Created)
			{
				return;
			}

			if (m_FeaturesMaskRadioButton.Checked)
			{
				m_ImageBox.Image = visualOdometer.OpticalFlow.MaskImage.Clone();
			}
			if (m_BirdsEyeViewRadioButton.Checked)
			{
				if (m_BirdsEyeViewImage == null)
				{
					m_BirdsEyeViewImage = visualOdometer.CurrentImage.Clone();
				}
				CvInvoke.cvWarpPerspective(
					visualOdometer.CurrentImage.Ptr,
					m_BirdsEyeViewImage.Ptr,
					m_BirdsEyeViewTransformation.Ptr,
					(int)Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR | (int)Emgu.CV.CvEnum.WARP.CV_WARP_FILL_OUTLIERS,
					new MCvScalar());

				m_ImageBox.Image = m_BirdsEyeViewImage;
			}
		}
	}
}
