using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisualOdometry.UI
{
	public partial class MapForm : Form
	{
		private RobotPath m_RobotPath;

		public MapForm(RobotPath robotPath)
		{
			m_RobotPath = robotPath;
			InitializeComponent();
		}

		internal void UpdateMap()
		{
		}
	}
}
