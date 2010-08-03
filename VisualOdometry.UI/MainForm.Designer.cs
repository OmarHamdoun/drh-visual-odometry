namespace VisualOdometry.UI
{
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			this.m_TopPanel = new System.Windows.Forms.Panel();
			this.m_DrawFeaturesCheckBox = new System.Windows.Forms.CheckBox();
			this.m_ApplyButton = new System.Windows.Forms.Button();
			this.m_GroundTopTextBox = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.m_SkyBottomTextBox = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.m_MinDistanceTextBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.m_QualityLevelTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.m_BlockSizeTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.m_MaxFeatureCountTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.m_BottomPanel = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.m_OtherViewsButton = new System.Windows.Forms.Button();
			this.m_CumulativeRotationTextBox = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.m_DetailsButton = new System.Windows.Forms.Button();
			this.m_NotTrackedFeaturesCount = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.m_TrackedFeaturesCountTextBox = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.m_FoundFeaturesCountTextBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.m_Timer = new System.Windows.Forms.Timer(this.components);
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.m_ImageBox = new Emgu.CV.UI.ImageBox();
			this.m_TopPanel.SuspendLayout();
			this.m_BottomPanel.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_ImageBox)).BeginInit();
			this.SuspendLayout();
			// 
			// m_TopPanel
			// 
			this.m_TopPanel.Controls.Add(this.m_ApplyButton);
			this.m_TopPanel.Controls.Add(this.m_GroundTopTextBox);
			this.m_TopPanel.Controls.Add(this.label8);
			this.m_TopPanel.Controls.Add(this.m_SkyBottomTextBox);
			this.m_TopPanel.Controls.Add(this.label9);
			this.m_TopPanel.Controls.Add(this.m_MinDistanceTextBox);
			this.m_TopPanel.Controls.Add(this.label4);
			this.m_TopPanel.Controls.Add(this.m_QualityLevelTextBox);
			this.m_TopPanel.Controls.Add(this.label3);
			this.m_TopPanel.Controls.Add(this.m_BlockSizeTextBox);
			this.m_TopPanel.Controls.Add(this.label2);
			this.m_TopPanel.Controls.Add(this.m_MaxFeatureCountTextBox);
			this.m_TopPanel.Controls.Add(this.label1);
			this.m_TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.m_TopPanel.Location = new System.Drawing.Point(0, 0);
			this.m_TopPanel.Name = "m_TopPanel";
			this.m_TopPanel.Size = new System.Drawing.Size(892, 83);
			this.m_TopPanel.TabIndex = 1;
			// 
			// m_DrawFeaturesCheckBox
			// 
			this.m_DrawFeaturesCheckBox.AutoSize = true;
			this.m_DrawFeaturesCheckBox.Checked = true;
			this.m_DrawFeaturesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.m_DrawFeaturesCheckBox.Location = new System.Drawing.Point(14, 25);
			this.m_DrawFeaturesCheckBox.Name = "m_DrawFeaturesCheckBox";
			this.m_DrawFeaturesCheckBox.Size = new System.Drawing.Size(122, 21);
			this.m_DrawFeaturesCheckBox.TabIndex = 13;
			this.m_DrawFeaturesCheckBox.Text = "Draw Features";
			this.m_DrawFeaturesCheckBox.UseVisualStyleBackColor = true;
			// 
			// m_ApplyButton
			// 
			this.m_ApplyButton.Location = new System.Drawing.Point(702, 38);
			this.m_ApplyButton.Name = "m_ApplyButton";
			this.m_ApplyButton.Size = new System.Drawing.Size(75, 23);
			this.m_ApplyButton.TabIndex = 4;
			this.m_ApplyButton.Text = "Apply";
			this.m_ApplyButton.UseVisualStyleBackColor = true;
			this.m_ApplyButton.Click += new System.EventHandler(this.OnApplyButtonClicked);
			// 
			// m_GroundTopTextBox
			// 
			this.m_GroundTopTextBox.Location = new System.Drawing.Point(587, 38);
			this.m_GroundTopTextBox.Name = "m_GroundTopTextBox";
			this.m_GroundTopTextBox.Size = new System.Drawing.Size(68, 22);
			this.m_GroundTopTextBox.TabIndex = 12;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(478, 41);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(89, 17);
			this.label8.TabIndex = 11;
			this.label8.Text = "Ground Top:";
			// 
			// m_SkyBottomTextBox
			// 
			this.m_SkyBottomTextBox.Location = new System.Drawing.Point(587, 10);
			this.m_SkyBottomTextBox.Name = "m_SkyBottomTextBox";
			this.m_SkyBottomTextBox.Size = new System.Drawing.Size(68, 22);
			this.m_SkyBottomTextBox.TabIndex = 10;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(478, 13);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(83, 17);
			this.label9.TabIndex = 9;
			this.label9.Text = "Sky Bottom:";
			// 
			// m_MinDistanceTextBox
			// 
			this.m_MinDistanceTextBox.Location = new System.Drawing.Point(371, 38);
			this.m_MinDistanceTextBox.Name = "m_MinDistanceTextBox";
			this.m_MinDistanceTextBox.Size = new System.Drawing.Size(68, 22);
			this.m_MinDistanceTextBox.TabIndex = 8;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(262, 41);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(93, 17);
			this.label4.TabIndex = 7;
			this.label4.Text = "Min Distance:";
			// 
			// m_QualityLevelTextBox
			// 
			this.m_QualityLevelTextBox.Location = new System.Drawing.Point(154, 38);
			this.m_QualityLevelTextBox.Name = "m_QualityLevelTextBox";
			this.m_QualityLevelTextBox.Size = new System.Drawing.Size(68, 22);
			this.m_QualityLevelTextBox.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 41);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(94, 17);
			this.label3.TabIndex = 5;
			this.label3.Text = "Quality Level:";
			// 
			// m_BlockSizeTextBox
			// 
			this.m_BlockSizeTextBox.Location = new System.Drawing.Point(371, 10);
			this.m_BlockSizeTextBox.Name = "m_BlockSizeTextBox";
			this.m_BlockSizeTextBox.Size = new System.Drawing.Size(68, 22);
			this.m_BlockSizeTextBox.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(262, 13);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 17);
			this.label2.TabIndex = 3;
			this.label2.Text = "Block Size:";
			// 
			// m_MaxFeatureCountTextBox
			// 
			this.m_MaxFeatureCountTextBox.Location = new System.Drawing.Point(154, 10);
			this.m_MaxFeatureCountTextBox.Name = "m_MaxFeatureCountTextBox";
			this.m_MaxFeatureCountTextBox.Size = new System.Drawing.Size(68, 22);
			this.m_MaxFeatureCountTextBox.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(131, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "Max Feature Count:";
			// 
			// m_BottomPanel
			// 
			this.m_BottomPanel.Controls.Add(this.groupBox3);
			this.m_BottomPanel.Controls.Add(this.groupBox2);
			this.m_BottomPanel.Controls.Add(this.groupBox1);
			this.m_BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.m_BottomPanel.Location = new System.Drawing.Point(0, 536);
			this.m_BottomPanel.Name = "m_BottomPanel";
			this.m_BottomPanel.Size = new System.Drawing.Size(892, 117);
			this.m_BottomPanel.TabIndex = 2;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.m_NotTrackedFeaturesCount);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.m_TrackedFeaturesCountTextBox);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.m_FoundFeaturesCountTextBox);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(203, 117);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Features";
			// 
			// m_OtherViewsButton
			// 
			this.m_OtherViewsButton.Location = new System.Drawing.Point(14, 79);
			this.m_OtherViewsButton.Name = "m_OtherViewsButton";
			this.m_OtherViewsButton.Size = new System.Drawing.Size(108, 23);
			this.m_OtherViewsButton.TabIndex = 13;
			this.m_OtherViewsButton.Text = "Other Views ...";
			this.m_OtherViewsButton.UseVisualStyleBackColor = true;
			this.m_OtherViewsButton.Click += new System.EventHandler(this.OnOtherViewsButtonClicked);
			// 
			// m_CumulativeRotationTextBox
			// 
			this.m_CumulativeRotationTextBox.Location = new System.Drawing.Point(83, 23);
			this.m_CumulativeRotationTextBox.Name = "m_CumulativeRotationTextBox";
			this.m_CumulativeRotationTextBox.ReadOnly = true;
			this.m_CumulativeRotationTextBox.Size = new System.Drawing.Size(68, 22);
			this.m_CumulativeRotationTextBox.TabIndex = 12;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(12, 26);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(65, 17);
			this.label10.TabIndex = 11;
			this.label10.Text = "Heading:";
			// 
			// m_DetailsButton
			// 
			this.m_DetailsButton.Location = new System.Drawing.Point(14, 53);
			this.m_DetailsButton.Name = "m_DetailsButton";
			this.m_DetailsButton.Size = new System.Drawing.Size(108, 23);
			this.m_DetailsButton.TabIndex = 10;
			this.m_DetailsButton.Text = "Rotation ...";
			this.m_DetailsButton.UseVisualStyleBackColor = true;
			this.m_DetailsButton.Click += new System.EventHandler(this.OnDetailsButtonClicked);
			// 
			// m_NotTrackedFeaturesCount
			// 
			this.m_NotTrackedFeaturesCount.Location = new System.Drawing.Point(112, 79);
			this.m_NotTrackedFeaturesCount.Name = "m_NotTrackedFeaturesCount";
			this.m_NotTrackedFeaturesCount.ReadOnly = true;
			this.m_NotTrackedFeaturesCount.Size = new System.Drawing.Size(68, 22);
			this.m_NotTrackedFeaturesCount.TabIndex = 9;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(12, 82);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(90, 17);
			this.label7.TabIndex = 8;
			this.label7.Text = "Not Tracked:";
			// 
			// m_TrackedFeaturesCountTextBox
			// 
			this.m_TrackedFeaturesCountTextBox.Location = new System.Drawing.Point(112, 51);
			this.m_TrackedFeaturesCountTextBox.Name = "m_TrackedFeaturesCountTextBox";
			this.m_TrackedFeaturesCountTextBox.ReadOnly = true;
			this.m_TrackedFeaturesCountTextBox.Size = new System.Drawing.Size(68, 22);
			this.m_TrackedFeaturesCountTextBox.TabIndex = 7;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 56);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(64, 17);
			this.label6.TabIndex = 6;
			this.label6.Text = "Tracked:";
			// 
			// m_FoundFeaturesCountTextBox
			// 
			this.m_FoundFeaturesCountTextBox.Location = new System.Drawing.Point(112, 23);
			this.m_FoundFeaturesCountTextBox.Name = "m_FoundFeaturesCountTextBox";
			this.m_FoundFeaturesCountTextBox.ReadOnly = true;
			this.m_FoundFeaturesCountTextBox.Size = new System.Drawing.Size(68, 22);
			this.m_FoundFeaturesCountTextBox.TabIndex = 5;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 26);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(52, 17);
			this.label5.TabIndex = 4;
			this.label5.Text = "Found:";
			// 
			// m_Timer
			// 
			this.m_Timer.Tick += new System.EventHandler(this.OnTimerTick);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.m_OtherViewsButton);
			this.groupBox2.Controls.Add(this.m_DrawFeaturesCheckBox);
			this.groupBox2.Controls.Add(this.m_DetailsButton);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
			this.groupBox2.Location = new System.Drawing.Point(203, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(152, 117);
			this.groupBox2.TabIndex = 9;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "View";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.m_CumulativeRotationTextBox);
			this.groupBox3.Controls.Add(this.label10);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
			this.groupBox3.Location = new System.Drawing.Point(355, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(200, 117);
			this.groupBox3.TabIndex = 10;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Odometry";
			// 
			// m_ImageBox
			// 
			this.m_ImageBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_ImageBox.Location = new System.Drawing.Point(0, 83);
			this.m_ImageBox.Name = "m_ImageBox";
			this.m_ImageBox.Size = new System.Drawing.Size(892, 453);
			this.m_ImageBox.TabIndex = 2;
			this.m_ImageBox.TabStop = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(892, 653);
			this.Controls.Add(this.m_ImageBox);
			this.Controls.Add(this.m_BottomPanel);
			this.Controls.Add(this.m_TopPanel);
			this.Name = "MainForm";
			this.Text = "Visual Odometry";
			this.m_TopPanel.ResumeLayout(false);
			this.m_TopPanel.PerformLayout();
			this.m_BottomPanel.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_ImageBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel m_TopPanel;
		private System.Windows.Forms.TextBox m_MinDistanceTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox m_QualityLevelTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox m_BlockSizeTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox m_MaxFeatureCountTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel m_BottomPanel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox m_NotTrackedFeaturesCount;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox m_TrackedFeaturesCountTextBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox m_FoundFeaturesCountTextBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox m_GroundTopTextBox;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox m_SkyBottomTextBox;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button m_ApplyButton;
		private System.Windows.Forms.Timer m_Timer;
		private System.Windows.Forms.Button m_DetailsButton;
		private System.Windows.Forms.TextBox m_CumulativeRotationTextBox;
		private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox m_DrawFeaturesCheckBox;
		private System.Windows.Forms.Button m_OtherViewsButton;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox2;
		private Emgu.CV.UI.ImageBox m_ImageBox;
	}
}

