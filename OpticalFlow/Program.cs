using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OpticalFlow.Properties;

namespace OpticalFlow
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

			MainModel model = new MainModel();

			try
			{
				Application.Run(new MainForm(model));
			}
			finally
			{
				if (model != null)
				{
					model.Dispose();
				}
				Settings.Default.Save();
			}
		}
	}
}
