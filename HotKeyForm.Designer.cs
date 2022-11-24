namespace ScreenHelper
{
	partial class HotKeyForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HotKeyForm));
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
			this.mi_settings = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmi_checkUpdate = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "ScreenHelper";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
			this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemExit,
            this.mi_settings,
            this.toolStripMenuItem1,
            this.tsmi_checkUpdate});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(211, 128);
			// 
			// menuItemExit
			// 
			this.menuItemExit.Name = "menuItemExit";
			this.menuItemExit.Size = new System.Drawing.Size(210, 24);
			this.menuItemExit.Text = "退出";
			this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
			// 
			// mi_settings
			// 
			this.mi_settings.Name = "mi_settings";
			this.mi_settings.Size = new System.Drawing.Size(210, 24);
			this.mi_settings.Text = "设置...";
			this.mi_settings.Click += new System.EventHandler(this.mi_settings_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(210, 24);
			this.toolStripMenuItem1.Text = "关于...";
			this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
			// 
			// tsmi_checkUpdate
			// 
			this.tsmi_checkUpdate.Name = "tsmi_checkUpdate";
			this.tsmi_checkUpdate.Size = new System.Drawing.Size(210, 24);
			this.tsmi_checkUpdate.Text = "检查更新...";
			this.tsmi_checkUpdate.Click += new System.EventHandler(this.tsmi_checkUpdate_Click);
			// 
			// HotKeyForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "HotKeyForm";
			this.Text = "HotKeyForm";
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private NotifyIcon notifyIcon1;
		private ContextMenuStrip contextMenuStrip1;
		private ToolStripMenuItem menuItemExit;
		private ToolStripMenuItem toolStripMenuItem1;
		private ToolStripMenuItem mi_settings;
		private ToolStripMenuItem tsmi_checkUpdate;
	}
}