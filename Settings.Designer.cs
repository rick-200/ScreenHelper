namespace ScreenHelper
{
	partial class Settings
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
			this.cb_autoRun = new System.Windows.Forms.CheckBox();
			this.cb_autoUpdate = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// cb_autoRun
			// 
			this.cb_autoRun.AutoSize = true;
			this.cb_autoRun.Location = new System.Drawing.Point(30, 23);
			this.cb_autoRun.Name = "cb_autoRun";
			this.cb_autoRun.Size = new System.Drawing.Size(91, 24);
			this.cb_autoRun.TabIndex = 0;
			this.cb_autoRun.Text = "开机自启";
			this.cb_autoRun.UseVisualStyleBackColor = true;
			this.cb_autoRun.CheckedChanged += new System.EventHandler(this.cb_autoRun_CheckedChanged);
			// 
			// cb_autoUpdate
			// 
			this.cb_autoUpdate.AutoSize = true;
			this.cb_autoUpdate.Location = new System.Drawing.Point(30, 64);
			this.cb_autoUpdate.Name = "cb_autoUpdate";
			this.cb_autoUpdate.Size = new System.Drawing.Size(91, 24);
			this.cb_autoUpdate.TabIndex = 1;
			this.cb_autoUpdate.Text = "自动更新";
			this.cb_autoUpdate.UseVisualStyleBackColor = true;
			this.cb_autoUpdate.CheckedChanged += new System.EventHandler(this.cb_autoUpdate_CheckedChanged);
			// 
			// Settings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.cb_autoUpdate);
			this.Controls.Add(this.cb_autoRun);
			this.Name = "Settings";
			this.Text = "ScreenHelper Settings";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private CheckBox cb_autoRun;
		private CheckBox cb_autoUpdate;
	}
}