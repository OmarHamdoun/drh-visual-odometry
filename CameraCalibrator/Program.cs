using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CameraCalibrator.Properties;

namespace CameraCalibrator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

			MainModel mainModel = new MainModel();
			try
			{
				Application.Run(new MainForm(mainModel));
			}
			finally
			{
				if (mainModel != null)
				{
					mainModel.Dispose();
				}
				Settings.Default.Save();
			}
        }
    }
}
