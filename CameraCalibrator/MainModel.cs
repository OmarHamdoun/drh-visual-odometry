using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CameraCalibrator
{
	public class MainModel : IDisposable
	{
		public CaptureSettings CaptureSettings { get; private set; }

		public event EventHandler Changed;

		public MainModel()
		{
			this.CaptureSettings = new CameraCalibrator.CaptureSettings();
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
			this.CaptureSettings.Dispose();
		}
	}
}
