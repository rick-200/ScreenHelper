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
	public partial class TextBoxDialog : Form
	{
		public static DialogResult Show(string text, string? title = null)
		{
			using TextBoxDialog dialog = new TextBoxDialog(text, title);
			dialog.Activate();
			return dialog.ShowDialog();
		}
		private TextBoxDialog(string text, string? title)
		{
			InitializeComponent();
			tb_main.Text = Util.CLf2Crlf(text);
			this.Text = title;
			this.TopMost = true;
			tb_main.KeyDown += (_, e) => { if (e.KeyCode == Keys.Escape) OnEscape(); };
		}
		private void OnEscape() {
			DialogResult = DialogResult.Cancel;
			Close();
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.Escape) OnEscape();
		}
	}
}
