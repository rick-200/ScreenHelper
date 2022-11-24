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
	}
}
