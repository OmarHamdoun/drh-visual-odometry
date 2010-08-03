namespace VisualOdometry.UI
{
	partial class AuxiliaryViewsForm
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
			this.m_TopPanel = new System.Windows.Forms.Panel();
			this.m_ImageBox = new Emgu.CV.UI.ImageBox();
			this.m_BirdsEyeViewRadioButton = new System.Windows.Forms.RadioButton();
			this.m_FeaturesMaskRadioButton = new System.Windows.Forms.RadioButton();
			this.m_TopPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_ImageBox)).BeginInit();
			this.SuspendLayout();
			// 
			// m_TopPanel
			// 
			this.m_TopPanel.Controls.Add(this.m_FeaturesMaskRadioButton);
			this.m_TopPanel.Controls.Add(this.m_BirdsEyeViewRadioButton);
			this.m_TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.m_TopPanel.Location = new System.Drawing.Point(0, 0);
			this.m_TopPanel.Name = "m_TopPanel";
			this.m_TopPanel.Size = new System.Drawing.Size(851, 77);
			this.m_TopPanel.TabIndex = 0;
			// 
			// m_ImageBox
			// 
			this.m_ImageBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_ImageBox.Location = new System.Drawing.Point(0, 77);
			this.m_ImageBox.Name = "m_ImageBox";
			this.m_ImageBox.Size = new System.Drawing.Size(851, 487);
			this.m_ImageBox.TabIndex = 2;
			this.m_ImageBox.TabStop = false;
			// 
			// m_BirdsEyeViewRadioButton
			// 
			this.m_BirdsEyeViewRadioButton.AutoSize = true;
			this.m_BirdsEyeViewRadioButton.Checked = true;
			this.m_BirdsEyeViewRadioButton.Location = new System.Drawing.Point(13, 13);
			this.m_BirdsEyeViewRadioButton.Name = "m_BirdsEyeViewRadioButton";
			this.m_BirdsEyeViewRadioButton.Size = new System.Drawing.Size(122, 21);
			this.m_BirdsEyeViewRadioButton.TabIndex = 0;
			this.m_BirdsEyeViewRadioButton.TabStop = true;
			this.m_BirdsEyeViewRadioButton.Text = "Bird\'s eye view";
			this.m_BirdsEyeViewRadioButton.UseVisualStyleBackColor = true;
			// 
			// m_FeaturesMaskRadioButton
			// 
			this.m_FeaturesMaskRadioButton.AutoSize = true;
			this.m_FeaturesMaskRadioButton.Location = new System.Drawing.Point(13, 41);
			this.m_FeaturesMaskRadioButton.Name = "m_FeaturesMaskRadioButton";
			this.m_FeaturesMaskRadioButton.Size = new System.Drawing.Size(122, 21);
			this.m_FeaturesMaskRadioButton.TabIndex = 1;
			this.m_FeaturesMaskRadioButton.Text = "Features mask";
			this.m_FeaturesMaskRadioButton.UseVisualStyleBackColor = true;
			// 
			// AuxiliaryViewsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(851, 564);
			this.Controls.Add(this.m_ImageBox);
			this.Controls.Add(this.m_TopPanel);
			this.Name = "AuxiliaryViewsForm";
			this.Text = "Auxiliary Views";
			this.m_TopPanel.ResumeLayout(false);
			this.m_TopPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_ImageBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel m_TopPanel;
		private System.Windows.Forms.RadioButton m_FeaturesMaskRadioButton;
		private System.Windows.Forms.RadioButton m_BirdsEyeViewRadioButton;
		private Emgu.CV.UI.ImageBox m_ImageBox;
	}
}