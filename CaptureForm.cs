using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

namespace ScreenHelper
{
	public partial class CaptureForm : Form
	{
		Screen screen;
		Bitmap screenBitmap;
		Point selPoint;
		Rectangle selRect;
		bool selFlag;
		public CaptureForm(Screen screen)
		{
			InitializeComponent();
			this.screen = screen;
			this.StartPosition = FormStartPosition.Manual;
			this.Left = screen.Bounds.Left;
			this.Top = screen.Bounds.Top;
			this.Width = screen.Bounds.Width;
			this.Height = screen.Bounds.Height;
			this.DoubleBuffered = true;//设置本窗体
									   //SetStyle(ControlStyles.UserPaint, true);
			//SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
			SetStyle(ControlStyles.Opaque, true);//防止闪烁
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲
			screenBitmap = new Bitmap(screen.Bounds.Width, screen.Bounds.Height);
			using Graphics g = Graphics.FromImage(screenBitmap);
			g.CopyFromScreen(new Point(screen.Bounds.Left, screen.Bounds.Top), new Point(0, 0), screen.Bounds.Size);
			using Pen pen = new Pen(Color.Red);
			Disposed += (_, _) => screenBitmap.Dispose();
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			var g = e.Graphics;
			using Pen pen = new Pen(Color.Blue, 3);
			pen.DashStyle = DashStyle.DashDot;
			g.Clear(Color.Red);
			g.DrawImage(screenBitmap, new Point(0, 0));
			g.DrawRectangle(pen, selRect);
			using Brush brush = new SolidBrush(Color.FromArgb(100, 0, 0, 0));
			//g.FillRectangle(brush, 0, 0, Width, Height);
			Region regionAll = new Region(new Rectangle(0, 0, Width, Height));
			Region regionSel = new Region(selRect);
			regionAll.Exclude(regionSel);
			g.FillRegion(brush, regionAll);
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}
		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);
			//Close();
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (!selFlag)
			{
				selFlag = true;
				selPoint = e.Location;
			}
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (!selFlag) return;
			int x1 = selPoint.X < e.X ? selPoint.X : e.X;
			int x2 = selPoint.X > e.X ? selPoint.X : e.X;
			int y1 = selPoint.Y < e.Y ? selPoint.Y : e.Y;
			int y2 = selPoint.Y > e.Y ? selPoint.Y : e.Y;
			selRect = new Rectangle(x1, y1, x2 - x1, y2 - y1);
			Invalidate();
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			selFlag = false;
			if (selRect.Width == 0 || selRect.Height == 0) return;
			Bitmap bitmap = new Bitmap(selRect.Width, selRect.Height);
			using Graphics g = Graphics.FromImage(bitmap);
			g.DrawImage(screenBitmap, 0, 0, selRect, GraphicsUnit.Pixel);
			//PointToScreen(selRect.Location);
			//MessageBox.Show(selRect.Location.ToString());
			new HoverPicture(PointToScreen(selRect.Location), bitmap).Show();
			Close();
			//try
			//{
			//	using var engine = new TesseractEngine("E:\\ScreenHelper\\tessdata", "chi_sim");
			//	using Bitmap bitmap = new Bitmap(selRect.Width, selRect.Height);
			//	using Graphics g = Graphics.FromImage(bitmap);
			//	g.DrawImage(screenBitmap, 0, 0, selRect, GraphicsUnit.Pixel);
			//	bitmap.Save("./sel.bmp");
			//	//using Pix pix = new BitmapToPixConverter().Convert(screenBitmap);
			//	using var page = engine.Process(bitmap);
			//	String s = page.GetText();
			//	MessageBox.Show(s);
			//	Clipboard.SetText(s);
			//}
			//catch (Exception ex) { MessageBox.Show(ex.Message); }

		}
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
		}
	}
}
