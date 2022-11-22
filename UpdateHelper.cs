using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScreenHelper
{
	internal class UpdateHelper
	{
		static readonly string configURL = "https://raw.githubusercontent.com/rickwang2002/ScreenHelper/master/RemoteInfo.json";
		public class RemoteInfo
		{
			public string? NewestVersion;
		}
		public static async Task<RemoteInfo> GetRemoteInfo()
		{
			using HttpClient client = new HttpClient();
			using var res = await client.GetAsync(configURL);
			if (res == null) throw new Exception("res==null");
			var content = await res.Content.ReadAsStringAsync();
			if (content == null) throw new Exception("content==null");
			RemoteInfo? obj = JsonSerializer.Deserialize<RemoteInfo>(content);
			if (obj == null) throw new Exception("obj==null");
			return obj;
		}
		public static string GetDownloadURL(string version)
		{
			return $"https://github.com/rickwang2002/ScreenHelper/releases/download/v{version}/ScreenHelper.zip";
		}
		public static bool NeedUpdate(string newestVersionString)
		{
			var thisVersion = Application.ProductVersion.Split('-')[0].Trim().Split('.');
			var newestVersion = newestVersionString.Split('-')[0].Trim().Split('.');
			int len = thisVersion.Length < newestVersion.Length ? thisVersion.Length : newestVersion.Length;
			for (int i = 0; i < len; i++)
			{
				if (int.Parse(newestVersion[i]) > int.Parse(thisVersion[i])) return true;
			}
			return newestVersion.Length > thisVersion.Length;
		}
		public static async void Download(string version)
		{
			using HttpClient client = new HttpClient();
			using var res = await client.GetAsync(GetDownloadURL(version));
			using Stream s = await res.Content.ReadAsStreamAsync();
			using ZipArchive zip = new ZipArchive(s);
			zip.ExtractToDirectory("./update_temp");
		}
	}
}
