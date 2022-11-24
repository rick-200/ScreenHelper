namespace ScreenHelper
{
	partial class AboutDialog
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
			this.lab_version = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.btn_update = new System.Windows.Forms.Button();
			this.pgb_main = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// lab_version
			// 
			this.lab_version.AutoSize = true;
			this.lab_version.Location = new System.Drawing.Point(35, 35);
			this.lab_version.Name = "lab_version";
			this.lab_version.Size = new System.Drawing.Size(156, 20);
			this.lab_version.TabIndex = 0;
			this.lab_version.Text = "ScreenHelper v0.0.0";
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new System.Drawing.Point(110, 69);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(360, 20);
			this.linkLabel1.TabIndex = 1;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "https://github.com/rickwang2002/ScreenHelper";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(35, 69);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(73, 20);
			this.label1.TabIndex = 2;
			this.label1.Text = "项目主页:";
			// 
			// btn_update
			// 
			this.btn_update.Location = new System.Drawing.Point(35, 106);
			this.btn_update.Name = "btn_update";
			this.btn_update.Size = new System.Drawing.Size(94, 29);
			this.btn_update.TabIndex = 3;
			this.btn_update.Text = "检查更新";
			this.btn_update.UseVisualStyleBackColor = true;
			this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
			// 
			// pgb_main
			// 
			this.pgb_main.Location = new System.Drawing.Point(12, 409);
			this.pgb_main.MarqueeAnimationSpeed = 20;
			this.pgb_main.Name = "pgb_main";
			this.pgb_main.Size = new System.Drawing.Size(776, 29);
			this.pgb_main.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.pgb_main.TabIndex = 4;
			this.pgb_main.Visible = false;
			// 
			// AboutDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.pgb_main);
			this.Controls.Add(this.btn_update);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.lab_version);
			this.Name = "AboutDialog";
			this.Text = "AboutDialog";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Label lab_version;
		private LinkLabel linkLabel1;
		private Label label1;
		private Button btn_update;
		private ProgressBar pgb_main;
	}
}