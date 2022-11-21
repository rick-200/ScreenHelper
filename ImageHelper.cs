using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenHelper
{
	internal class ImageHelper
	{
		public static Bitmap Truncate(Image img, Rectangle rect)
		{
			Bitmap res = new Bitmap(rect.Width, rect.Height);
			using Graphics g = Graphics.FromImage(res);
			g.DrawImage(img, 0, 0, rect, GraphicsUnit.Pixel);
			return res;
		}
		public static (Bitmap, Graphics) Create(int Width, int Height)
		{
			Bitmap res = new Bitmap(Width, Height);
			Graphics g = Graphics.FromImage(res);
			return (res, g);
		}
	}
}
