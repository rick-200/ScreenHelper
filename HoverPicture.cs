using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.QrCode;
using ZXing.Client.Result;
using ZXing.Common;
using ZXing.Windows.Compatibility;
using System.Reflection;
using System.Windows.Forms.VisualStyles;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ScreenHelper
{
	public partial class HoverPicture : Form
	{
		Bitmap pic;
		bool doMove;
		Point rawPos;
		//bool fixedTop;
		int scale;
		Point initPos;
		bool modFlag;
		Stack<GraphicsPath> paths = new Stack<GraphicsPath>();
		GraphicsPath? curPath = null;
		public HoverPicture(Point pos, Bitmap bitmap)
		{
			InitializeComponent();
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.Opaque, true);//防止闪烁
			this.FormBorderStyle = FormBorderStyle.None;
			this.initPos = pos;
			pic = bitmap;
			Disposed += (object? sender, EventArgs e) => pic.Dispose();
		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.Location = initPos;
			this.MinimumSize = new Size(0, 0);
			this.Size = pic.Size + new Size(2, 2);
		}
		//protected override void OnShown(EventArgs e)
		//{
		//	base.OnShown(e);
		//	this.Location = pos;
		//}
		//protected override Pai
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			doMove = true;
			rawPos = e.Location;
			if (modFlag)
			{
				curPath = new GraphicsPath();
				curPath.AddLine(e.Location, e.Location);
			}

		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			doMove = false;
			if (modFlag)
			{
				if (curPath == null) throw new Exception("assert");
				paths.Push(curPath);
				curPath = null;
			}
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			doMove = false;
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (doMove && !modFlag)
			{
				this.Location += new Size(e.X - rawPos.X, e.Y - rawPos.Y);
			}
			if (modFlag && e.Button.HasFlag(MouseButtons.Left))
			{
				if (curPath == null) throw new Exception("assert");
				curPath.AddLine(curPath.GetLastPoint(), e.Location);
				Invalidate();
			}
		}
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			//e.Delta
			if (ModifierKeys.HasFlag(Keys.Control) && e.Delta != 0)
			{
				scale += e.Delta > 0 ? 1 : -1;
				Size = new Size((int)(pic.Width * (1 + scale * 0.05)) + 2, (int)(pic.Height * (1 + scale * 0.05) + 2));
				Invalidate();
			}
		}
		private void DoDraw(Graphics g)
		{
			Color borderColor = Color.Green;
			if (TopMost) borderColor = Color.Red;
			if (modFlag) borderColor = Color.Blue;
			using Pen pen = new Pen(borderColor, 3);
			g.DrawRectangle(pen, new Rectangle(0, 0, Width, Height));
			g.DrawImage(pic, new Rectangle(1, 1, Width - 2, Height - 2), new Rectangle(0, 0, pic.Width, pic.Height), GraphicsUnit.Pixel);
			using Pen penPath = new Pen(Color.Red, 3);
			foreach (var p in paths)
			{
				g.DrawPath(penPath, p);
			}
			if (curPath != null)
				g.DrawPath(penPath, curPath);
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			DoDraw(e.Graphics);
		}
		protected override void OnDoubleClick(EventArgs e)
		{
			base.OnDoubleClick(e);
			TopMost = !TopMost;
			Invalidate();
			//if (!TopMost)
			//{
			//	fixedTop = true;
			// this.
			//}
			//else
			//{
			//	fixedTop = false;
			//}
		}
		protected override async void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
			if (e.Control)
			{
				switch (e.KeyCode)
				{
					case Keys.C:
						Clipboard.SetImage(pic);
						break;
					case Keys.A:
						var s = await OCRHelper.DoOCRAsync(pic);
						var mr = MessageBox.Show(s, "OCR结果 点击确认复制到剪切板", MessageBoxButtons.OKCancel);
						if (mr == DialogResult.OK)
						{
							Clipboard.SetText(s);
						}
						break;
					case Keys.X:
						//QRCodeReader reader=new QRCodeReader();
						BarcodeReader reader = new BarcodeReader();
						var res = reader.DecodeMultiple(pic);
						if (res != null)
						{
							StringBuilder sb = new StringBuilder();
							foreach (var r in res)
							{
								sb.Append("result:\n").Append(r.Text).Append("\n\n");
							}
							MessageBox.Show(sb.ToString(), "二维码识别结果");
						}
						else
						{
							MessageBox.Show("图片不包含二维码！", "二维码识别结果", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						break;
					case Keys.M:
						modFlag = !modFlag;
						Invalidate();
						break;
					case Keys.Z:
						if (curPath != null)
						{
							curPath = new GraphicsPath();
						}
						else if (paths.Count > 0)
						{
							paths.Pop();
						}
						Invalidate();
						break;
					case Keys.D:
						string path = Path.GetTempPath() + "ScreenHelper" + DateTime.Now.Ticks;
						pic.Save(path);
						var p = new Process();
						p.StartInfo = new ProcessStartInfo("mspaint.exe", path);
						p.Start();
						break;
					case Keys.S:
						{
							SaveFileDialog sfd = new SaveFileDialog();
							sfd.Filter = "png|*.png|jpg|*.jpg|bmp|*.bmp";
							sfd.FileName = Path.GetRandomFileName() + ".png";
							var dr = sfd.ShowDialog();
							if (dr != DialogResult.OK) break;
							ImageFormat format;
							switch (Path.GetExtension(sfd.FileName))
							{
								default:
								case ".png":
									format = ImageFormat.Png;
									break;
								case ".jpg":
									format = ImageFormat.Jpeg;
									break;
								case ".bmp":
									format = ImageFormat.Bmp;
									break;

							}
							//switch(Path.GetExtension(sfd.FileName))
							using Bitmap imgCopy = new Bitmap(Width, Height);
							using Graphics g = Graphics.FromImage(imgCopy);
							DoDraw(g);
							using Bitmap imgSave = ImageHelper.Truncate(imgCopy, new Rectangle(1, 1, Width - 2, Height - 2));
							imgSave.Save(sfd.FileName, format);
							break;
						}

				}
			}
		}
	}
}
