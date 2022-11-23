using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScreenHelper
{
	internal class UpdateHelper
	{
		//当前三位版本号
		public static string SelfVersion { get => Application.ProductVersion.Substring(0, Application.ProductVersion.LastIndexOf('.')); }
		//static readonly string assemblyInfoURL = "https://raw.githubusercontent.com/rickwang2002/ScreenHelper/dev/AssemblyInfo1.cs";
		//static readonly string configURL = "https://raw.githubusercontent.com/rickwang2002/ScreenHelper/dev/RemoteInfo.json";
		static readonly string downloadURL = "https://github.com/rickwang2002/ScreenHelper/releases/download/v{0}/ScreenHelper.zip";
		static readonly string updateCommand = $".\\update.ps1 -InstallDir \"{Application.StartupPath.TrimEnd('\\', '/')}\" -DeleteSelf $true *>\"{{0}}\"\n";
		public class RemoteInfo
		{
			public string NewestVersion { get; set; } = "";
		}
		public delegate bool VersionCallback(bool needUpdate, string newestVersion);
		public delegate bool UpdateCallback(string newestVersion);
		public static async Task Update(VersionCallback versionCallback, UpdateCallback updateCallback)
		{
			using HttpClient client = new HttpClient();
			client.Timeout = TimeSpan.FromSeconds(30);
			string downloadDir = Path.Combine(Path.GetTempPath(), "ScreenHelperUpdate");
			string downloadFilePath = Path.Combine(downloadDir, "ScreenHelper.zip");
			Directory.CreateDirectory(downloadDir);
			string extractPath = Path.Combine(downloadDir, "extract");
			if (Directory.Exists(extractPath))
				Directory.Delete(extractPath, true);
			Directory.CreateDirectory(extractPath);

			var newestVersion = await GetNewestVersion(client);

			if (!versionCallback(NeedUpdate(newestVersion), newestVersion)) return;
			if (!NeedUpdate(newestVersion)) return;

			string downloadUrl = string.Format(downloadURL, newestVersion);
			await DownloadHelper.DownloadPartial(client, downloadFilePath, new Uri(downloadUrl));

			if (!updateCallback(newestVersion)) return;

			try
			{
				using var fs = File.OpenRead(downloadFilePath);
				using ZipArchive zip = new ZipArchive(fs);
				zip.ExtractToDirectory(extractPath, true);
				fs.Close();
				zip.Dispose();
			}
			catch (Exception ex)
			{
				throw new DownloadFileDamageException("", ex);
			}
			MessageBox.Show("ExtractToDirectory");
			string updateTempPath = Path.Combine(extractPath, "ScreenHelper");
			string logFilePath = Path.Combine(downloadDir, "update.log");

			Process.Start(Path.Combine(updateTempPath, "ScreenHelper.exe"), $"update \"{Application.StartupPath.TrimEnd('\\', '/')}\"");
			//using var proc = Process.Start(new ProcessStartInfo("powershell.exe")
			//{
			//	RedirectStandardInput = true,
			//	WorkingDirectory = updateTempPath,
			//})!;
			//string command = string.Format(updateCommand, logFilePath);
			//MessageBox.Show(command);
			//proc.StandardInput.WriteLine(command);
			//proc.StandardInput.Flush();
			Application.Exit();
		}

		public static async Task<string> GetNewestVersion(HttpClient client)
		{
			var res = await client.GetAsync("https://github.com/rickwang2002/ScreenHelper/releases/latest", HttpCompletionOption.ResponseHeadersRead);
			string[] seg = res.RequestMessage!.RequestUri!.Segments;
			string v = seg[seg.Length - 1];
			v = v.Trim('\\', '/');
			v = v.TrimStart('v');
			v = v.Substring(0, v.LastIndexOfAny("0123456789".ToCharArray()) + 1);
			return v;
		}

		public static void DoUpdateReplace(string path)
		{
			var ps = Process.GetProcessesByName("ScreenHelper");
			foreach (var p in ps)
			{
				if (p.Id == Process.GetCurrentProcess().Id) continue;
				p.WaitForExit();
			}
			MessageBox.Show("Before Delete");
			Directory.Delete(path, true);
			CopyDirectory(Application.StartupPath, path);
			Process.Start(Path.Combine(path, "ScreenHelper.exe"));
			Application.Exit();
		}
		private static void CopyDirectory(string src, string dst)
		{
			Directory.CreateDirectory(dst);
			string[] files = Directory.GetFiles(src);
			foreach (var f in files)
			{
				File.Copy(f, Path.Combine(dst, Path.GetFileName(f)), true);
			}
			string[] dirs = Directory.GetDirectories(src);
			foreach (var d in dirs)
			{
				CopyDirectory(d, Path.Combine(dst, Path.GetFileName(d)!));
			}
		}

		//private static async Task<RemoteInfo> GetRemoteInfo(HttpClient client)
		//{
		//	using var res = await client.GetAsync(configURL);
		//	if (res == null) throw new Exception("res==null");
		//	var content = await res.Content.ReadAsStringAsync();
		//	if (content == null) throw new Exception("content==null");
		//	RemoteInfo? obj = JsonSerializer.Deserialize<RemoteInfo>(content);
		//	if (obj == null) throw new Exception("obj==null");
		//	return obj;
		//}

		private static bool NeedUpdate(string newestVersionString)
		{
			var thisVersion = SelfVersion.Split('-')[0].Trim().Split('.');
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
	class DownloadFileDamageException : Exception
	{
		public DownloadFileDamageException(string msg) : base(msg) { }
		public DownloadFileDamageException(string msg, Exception inner) : base(msg, inner) { }
	}
}
