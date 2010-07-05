using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Emgu.CV;
using CameraCalibrator.Support;
using CameraCalibrator.WinForm.Support;

namespace CameraCalibrator.Tester
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			IntrinsicCameraParameters intrinsicCameraParameters = IntrinsicParametersSupport.Load(@"C:\svnDev\Rainer\Robot\CameraCalibration\CameraCalibrator\bin\Debug\CameraParameters.txt");
			using (TestFormModel testFormModel = new TestFormModel(intrinsicCameraParameters))
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new TestForm(testFormModel));
			}
		}
	}
}
