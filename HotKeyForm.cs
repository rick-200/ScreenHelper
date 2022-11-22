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
		private const int WM_HOTKEY = 0x312; //������Ϣ-�ȼ�
		private const int WM_CREATE = 0x1; //������Ϣ-����
		private const int WM_DESTROY = 0x2; //������Ϣ-����
		private const int CaptureKey = 0x3573; //�ȼ�ID
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			if (DesignMode) return;
			switch (m.Msg)
			{
				case WM_HOTKEY: //������Ϣ-�ȼ�ID
					switch (m.WParam.ToInt32())
					{
						case CaptureKey: //�ȼ�ID
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
							//Clipboard.SetDataObject(@"�����ȼ�д��ճ����");
							break;
						default:
							break;
					}
					break;
				case WM_CREATE: //������Ϣ-����
					AppHotKey.RegKey(Handle, CaptureKey, AppHotKey.KeyModifiers.Ctrl | AppHotKey.KeyModifiers.Shift, Keys.Q);
					break;
				case WM_DESTROY: //������Ϣ-����
					AppHotKey.UnRegKey(Handle, CaptureKey); //�����ȼ�
					break;
				default:
					break;
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			//ToolStripMenuItem menuItem1 = new ToolStripMenuItem("��ʾ����");

			//ToolStripMenuItem menuItem2 = new ToolStripMenuItem("���ش���");

			//ToolStripMenuItem menuItem3 = new ToolStripMenuItem("ִ�г���");

			//ToolStripMenuItem menuItem4 = new ToolStripMenuItem("�˳�����");

			//////�ֱ�Ϊ4���˵������Click�¼���Ӧ����

			////menuItem1.Click += new System.EventHandler(this.menuItem1_Click);

			////menuItem2.Click += new System.EventHandler(this.menuItem2_Click);

			////menuItem3.Click += new System.EventHandler(this.menuItem3_Click);

			////menuItem4.Click += new System.EventHandler(this.menuItem4_Click);

			////����NotifyIcon�����ContextMenu����Ϊ����ĵ����˵�����

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
			MessageBox.Show($"Screenhelper\n�汾:{Application.ProductVersion}", "����");
		}
	}
}