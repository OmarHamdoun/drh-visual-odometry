namespace VisualOdometry.UI
{
	partial class MapForm
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
			this.m_PictureBox = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.m_PictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// m_PictureBox
			// 
			this.m_PictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_PictureBox.Location = new System.Drawing.Point(0, 0);
			this.m_PictureBox.Name = "m_PictureBox";
			this.m_PictureBox.Size = new System.Drawing.Size(689, 516);
			this.m_PictureBox.TabIndex = 0;
			this.m_PictureBox.TabStop = false;
			// 
			// MapForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(689, 516);
			this.Controls.Add(this.m_PictureBox);
			this.Name = "MapForm";
			this.Text = "Map";
			((System.ComponentModel.ISupportInitialize)(this.m_PictureBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox m_PictureBox;
	}
}