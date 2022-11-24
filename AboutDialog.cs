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
	public partial class AboutDialog : Form
	{
		public static new DialogResult Show()
		{
			AboutDialog ad = new AboutDialog();
			return ad.ShowDialog();
		}
		private AboutDialog()
		{
			InitializeComponent();
			lab_version.Text = $"ScreenHelper v{Application.ProductVersion}";
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("explorer.exe", (sender as Label)!.Text);
		}

		private async void btn_update_Click(object sender, EventArgs e)
		{
			btn_update.Enabled = false;
			pgb_main.Visible = true;
			using HttpClient client = new HttpClient();
			client.Timeout = TimeSpan.FromSeconds(30);
			try
			{
				string newestVersion = await UpdateHelper.GetNewestVersion(client);
				if (UpdateHelper.NeedUpdate(newestVersion))
				{
					string tip = Properties.Settings.Default.AutoUpdate ?
						"自动更新已开启，会自动在后台下载新版本，完成后将提示更新" :
						"自动更新已关闭，需手动更新";
					var res = MessageBox.Show(
						$"发现新版本: {newestVersion}\n" +
						$"当前版本{Application.ProductVersion}\n" +
						$"{tip}\n" +
						$"点击'确定'前往网站下载最新版", "ScreenHelper", MessageBoxButtons.OKCancel);
					if (res == DialogResult.OK)
					{
						System.Diagnostics.Process.Start("explorer.exe", "https://github.com/rickwang2002/ScreenHelper/releases/latest");
					}
				}
				else
				{
					MessageBox.Show($"已经是最新版本", "ScreenHelper");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"检查更新失败: {ex.Message}", "ScreenHelper");
			}
			pgb_main.Visible = false;
			btn_update.Enabled = true;
		}
	}
}
