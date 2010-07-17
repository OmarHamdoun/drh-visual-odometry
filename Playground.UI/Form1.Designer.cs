namespace Playground.UI
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			this.m_Chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			((System.ComponentModel.ISupportInitialize)(this.m_Chart)).BeginInit();
			this.SuspendLayout();
			// 
			// m_Chart
			// 
			this.m_Chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.m_Chart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(223)))), ((int)(((byte)(193)))));
			this.m_Chart.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
			this.m_Chart.BorderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(64)))), ((int)(((byte)(1)))));
			this.m_Chart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
			this.m_Chart.BorderlineWidth = 2;
			this.m_Chart.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Emboss;
			chartArea1.Area3DStyle.Inclination = 15;
			chartArea1.Area3DStyle.IsClustered = true;
			chartArea1.Area3DStyle.IsRightAngleAxes = false;
			chartArea1.Area3DStyle.Perspective = 10;
			chartArea1.Area3DStyle.Rotation = 10;
			chartArea1.Area3DStyle.WallWidth = 0;
			chartArea1.AxisX.LabelAutoFitStyle = ((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles)((((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.IncreaseFont | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.DecreaseFont)
						| System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep90)
						| System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.WordWrap)));
			chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
			chartArea1.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			chartArea1.AxisX.Title = "Histogram (Frequency Diagram)";
			chartArea1.AxisX.TitleFont = new System.Drawing.Font("Trebuchet MS", 8F);
			chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
			chartArea1.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			chartArea1.AxisY2.IsLabelAutoFit = false;
			chartArea1.AxisY2.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
			chartArea1.BackColor = System.Drawing.Color.OldLace;
			chartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
			chartArea1.BackSecondaryColor = System.Drawing.Color.White;
			chartArea1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			chartArea1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
			chartArea1.Name = "HistogramArea";
			chartArea1.Position.Auto = false;
			chartArea1.Position.Height = 77F;
			chartArea1.Position.Width = 93F;
			chartArea1.Position.X = 3F;
			chartArea1.Position.Y = 18F;
			chartArea1.ShadowColor = System.Drawing.Color.Transparent;
			this.m_Chart.ChartAreas.Add(chartArea1);
			legend1.BackColor = System.Drawing.Color.Transparent;
			legend1.Enabled = false;
			legend1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
			legend1.IsTextAutoFit = false;
			legend1.Name = "Default";
			this.m_Chart.Legends.Add(legend1);
			this.m_Chart.Location = new System.Drawing.Point(12, 12);
			this.m_Chart.Name = "m_Chart";
			series1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
			series1.ChartArea = "HistogramArea";
			series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(64)))), ((int)(((byte)(10)))));
			series1.IsValueShownAsLabel = true;
			series1.Legend = "Default";
			series1.Name = "Histogram";
			series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
			series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
			this.m_Chart.Series.Add(series1);
			this.m_Chart.Size = new System.Drawing.Size(465, 310);
			this.m_Chart.TabIndex = 2;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(489, 334);
			this.Controls.Add(this.m_Chart);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.m_Chart)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataVisualization.Charting.Chart m_Chart;

	}
}

