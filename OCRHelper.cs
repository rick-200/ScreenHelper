using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesseractOCR;
using TesseractOCR.Enums;

namespace ScreenHelper
{
	internal class OCRHelper
	{
		static Engine engine = new Engine(Path.Combine(Application.StartupPath, "./tessdata"), Language.ChineseSimplified);
		public static string DoOCR(Bitmap bitmap)
		{
			using MemoryStream ms = new MemoryStream();
			bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
			using var img = TesseractOCR.Pix.Image.LoadFromMemory(ms);
			lock (engine)
			{
				using var page = engine.Process(img);
				return page.Text;
			}
		}
		public static async Task<string> DoOCRAsync(Bitmap bitmap)
		{
			return await Task.Run(() =>
			{
				return DoOCR(bitmap);
			});
		}
	}
}
