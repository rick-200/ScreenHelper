using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing.Drawing2D;
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
		static readonly string configURL = "https://raw.githubusercontent.com/rickwang2002/ScreenHelper/dev/RemoteInfo.json";
		static readonly string downloadURL = "https://github.com/rickwang2002/ScreenHelper/releases/download/v{0}/ScreenHelper.zip";
		static readonly string updateCommand = $"cd \"{{0}}\"\n .\\update.ps1 -InstallDir \"{Application.StartupPath}\" -DeleteSelf $true";
		public class RemoteInfo
		{
			public string NewestVersion { get; set; } = "";
		}
		public delegate bool VersionCallback(bool needUpdate, RemoteInfo info);
		public delegate bool UpdateCallback(RemoteInfo info);
		public static async Task Update(VersionCallback versionCallback, UpdateCallback updateCallback)
		{
			using HttpClient client = new HttpClient();

			string downloadDir = Path.Combine(Path.GetTempPath(), "ScreenHelperUpdate");
			string downloadFilePath = Path.Combine(downloadDir, "ScreenHelper.zip");
			Directory.CreateDirectory(downloadDir);
			string extractPath = Path.Combine(downloadDir, "extract");
			if (Directory.Exists(extractPath))
				Directory.Delete(extractPath, true);
			Directory.CreateDirectory(extractPath);

			var info = await GetRemoteInfo(client);
			if (info.NewestVersion == null) throw new Exception("更新失败");

			if (!versionCallback(NeedUpdate(info.NewestVersion), info)) return;
			if (!NeedUpdate(info.NewestVersion)) return;

			string downloadUrl = string.Format(downloadURL, info.NewestVersion);
			await DownloadHelper.DownloadPartial(client, downloadFilePath, new Uri(downloadUrl));

			if (!updateCallback(info)) return;
			

			using var fs = File.OpenRead(downloadFilePath);
			using ZipArchive zip = new ZipArchive(fs);
			zip.ExtractToDirectory(extractPath, true);
			fs.Close();

			string updateTempPath = Path.Combine(extractPath, "ScreenHelper");

			using var proc = Process.Start(new ProcessStartInfo("powershell.exe")
			{
				RedirectStandardInput = true,
			})!;
			proc.StandardInput.WriteLine(string.Format(updateCommand, updateTempPath));
			Application.Exit();
		}

		private static async Task<RemoteInfo> GetRemoteInfo(HttpClient client)
		{
			using var res = await client.GetAsync(configURL);
			if (res == null) throw new Exception("res==null");
			var content = await res.Content.ReadAsStringAsync();
			if (content == null) throw new Exception("content==null");
			RemoteInfo? obj = JsonSerializer.Deserialize<RemoteInfo>(content);
			if (obj == null) throw new Exception("obj==null");
			return obj;
		}
		private static bool NeedUpdate(string newestVersionString)
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

		//public static async Task Download(string version, string storePath)
		//{
		//	using HttpClient client = new HttpClient();
		//	await DownloadHelper.DownloadPartial(client, storePath, new Uri(GetDownloadURL(version)));
		//	using var fs = File.OpenRead(storePath);
		//	using ZipArchive zip = new ZipArchive(fs);
		//	zip.ExtractToDirectory(storePath);
		//	//using var request = new HttpRequestMessage
		//	//{
		//	//	RequestUri = new Uri(GetDownloadURL(version)),
		//	//	Method = HttpMethod.Get
		//	//};
		//	//request.Headers.Range = new System.Net.Http.Headers.RangeHeaderValue(0, 0);
		//	////client.send
		//	//using var res = await client.SendAsync(request);
		//	//var headers = res.Content.Headers;
		//	//using Stream s = await res.Content.ReadAsStreamAsync();
		//	//using ZipArchive zip = new ZipArchive(s);
		//	//Directory.CreateDirectory(storePath);
		//	//zip.ExtractToDirectory(storePath);
		//}
	}
}
