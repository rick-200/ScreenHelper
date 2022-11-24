namespace ScreenHelper
{
	public partial class HotKeyForm : Form
	{
		bool captureFlag;
		public HotKeyForm()
		{
			InitializeComponent();
			this.WindowState = FormWindowState.Minimized;
			this.ShowInTaskbar = false;
		}

		protected override void OnShown(EventArgs e)
		{
			Hide();
			this.WindowState = FormWindowState.Normal;
			this.ShowInTaskbar = true;
			base.OnShown(e);
		}
		private const int WM_HOTKEY = 0x312; //窗口消息-热键
		private const int WM_CREATE = 0x1; //窗口消息-创建
		private const int WM_DESTROY = 0x2; //窗口消息-销毁
		private const int CaptureKey = 0x3573; //热键ID
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			if (DesignMode) return;
			switch (m.Msg)
			{
				case WM_HOTKEY: //窗口消息-热键ID
					switch (m.WParam.ToInt32())
					{
						case CaptureKey: //热键ID
							if (captureFlag) break;
							captureFlag = true;
							List<CaptureForm> cfs = new List<CaptureForm>();
							foreach (var sc in Screen.AllScreens)
							{
								CaptureForm cf = new CaptureForm(sc);
								cfs.Add(cf);
								cf.FormClosed += (object? sender, FormClosedEventArgs e) =>
								{
									captureFlag = false;
									cfs.ForEach((v) =>
									{
										if (v.IsDisposed || v.Disposing || !v.Visible) return;
										v.Hide();
										v.Close();
									});
								};
								cf.Show();
							}
							//Clipboard.SetDataObject(@"测试热键写入粘贴板");
							break;
						default:
							break;
					}
					break;
				case WM_CREATE: //窗口消息-创建
					AppHotKey.RegKey(Handle, CaptureKey, AppHotKey.KeyModifiers.Ctrl | AppHotKey.KeyModifiers.Shift, Keys.Q);
					break;
				case WM_DESTROY: //窗口消息-销毁
					AppHotKey.UnRegKey(Handle, CaptureKey); //销毁热键
					break;
				default:
					break;
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			//ToolStripMenuItem menuItem1 = new ToolStripMenuItem("显示窗口");

			//ToolStripMenuItem menuItem2 = new ToolStripMenuItem("隐藏窗口");

			//ToolStripMenuItem menuItem3 = new ToolStripMenuItem("执行程序");

			//ToolStripMenuItem menuItem4 = new ToolStripMenuItem("退出程序");

			//////分别为4个菜单项添加Click事件响应函数

			////menuItem1.Click += new System.EventHandler(this.menuItem1_Click);

			////menuItem2.Click += new System.EventHandler(this.menuItem2_Click);

			////menuItem3.Click += new System.EventHandler(this.menuItem3_Click);

			////menuItem4.Click += new System.EventHandler(this.menuItem4_Click);

			////设置NotifyIcon对象的ContextMenu属性为生面的弹出菜单对象

			//notifyIcon1.ContextMenuStrip = new ContextMenuStrip();
		}

		private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
		{

		}

		private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
		{
			//this.ContextMenuStrip.Show();
		}

		private void menuItemExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			AboutDialog.Show();
		}

		private void mi_settings_Click(object sender, EventArgs e)
		{
			Settings.ShowSettingsWindow();
		}

		//	private async void tsmi_checkUpdate_Click(object sender, EventArgs e)
		//	{
		//		using HttpClient client = new HttpClient();
		//		client.Timeout = TimeSpan.FromSeconds(30);
		//		try
		//		{
		//			string newestVersion = await UpdateHelper.GetNewestVersion(client);
		//			if (UpdateHelper.NeedUpdate(newestVersion))
		//			{
		//				string tip = Properties.Settings.Default.AutoUpdate ?
		//					"自动更新已开启，会自动在后台下载新版本，完成后将提示更新" :
		//					"自动更新已关闭，需手动更新";
		//				var res = MessageBox.Show(
		//					$"发现新版本: {newestVersion}\n" +
		//					$"当前版本{Application.ProductVersion}\n" +
		//					$"{tip}\n" +
		//					$"点击'确定'前往网站下载最新版", "ScreenHelper", MessageBoxButtons.OKCancel);
		//				if (res == DialogResult.OK)
		//				{
		//					System.Diagnostics.Process.Start("explorer.exe", "https://github.com/rickwang2002/ScreenHelper/releases/latest");
		//				}
		//			}
		//			else
		//			{
		//				MessageBox.Show($"已经是最新版本", "ScreenHelper");
		//			}
		//		}
		//		catch (Exception ex)
		//		{
		//			MessageBox.Show($"检查更新失败: {ex.Message}", "ScreenHelper");
		//		}


		//	}
	}
}