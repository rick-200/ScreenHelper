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
			MessageBox.Show($"Screenhelper\n版本:{Application.ProductVersion}", "关于");
		}
	}
}