using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenHelper
{
	public partial class Settings : Form
	{
		static Settings? instance = null;
		public static void ShowSettingsWindow()
		{
			if (instance == null)
			{
				instance = new Settings();
				instance.Show();
			}
			else
			{
				instance.Visible = true;
				instance.WindowState= FormWindowState.Normal;
				instance.Activate();
			}
		}

		public Settings()
		{
			InitializeComponent();
			cb_autoRun.Checked = Properties.Settings.Default.AutoRun;
			cb_autoUpdate.Checked = Properties.Settings.Default.AutoUpdate;
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			instance = null;
		}

		private void cb_autoRun_CheckedChanged(object sender, EventArgs e)
		{
			Properties.Settings.Default.AutoRun = cb_autoRun.Checked;
			Properties.Settings.Default.Save();
			try
			{
				AutoRunHelper.RegisterAutoRun(cb_autoRun.Checked);
			}
			catch (Exception ex) { if (cb_autoRun.Checked) MessageBox.Show("无法设置自动运行：" + ex.Message, "ScreenHelper"); }
		}

		private void cb_autoUpdate_CheckedChanged(object sender, EventArgs e)
		{
			Properties.Settings.Default.AutoUpdate = cb_autoUpdate.Checked;
			Properties.Settings.Default.Save();
		}
	}
}
