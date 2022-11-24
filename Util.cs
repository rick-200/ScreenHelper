using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ScreenHelper
{
	//杂项工具
	internal class Util
	{
		public static string CLf2Crlf(string s) {
			StringBuilder sb = new StringBuilder();
			char prev = '\0';
			foreach (char c in s)
			{
				if (c == '\n' && prev != '\r') sb.Append("\r\n");
				sb.Append(c);
				prev = c;
			}
			return sb.ToString();
		}
	}
}
