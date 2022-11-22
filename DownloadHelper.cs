using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ScreenHelper
{
	internal class DownloadHelper
	{
		//由于众所周知的原因，在CN，github网络连接较差
		//缩小DownloadBufferSize有助于确保重试机制的有效进行
		static readonly int DownloadBufferSize = 1024 * 128;
		public static async void DownloadEntirly(HttpClient client, Uri url, string path)
		{
			using var res = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
			using var rs = await res.Content.ReadAsStreamAsync();
			using var fs = File.OpenWrite(path);
			rs.CopyTo(fs);
		}
		private static async Task<long> GetRemoteFileLength(HttpClient client, Uri url)
		{
			using var request = new HttpRequestMessage(HttpMethod.Get, url);
			request.Headers.Range = new System.Net.Http.Headers.RangeHeaderValue(0, 0);
			using var resRange = await client.SendAsync(request);
			if (resRange.StatusCode != System.Net.HttpStatusCode.PartialContent) return -1;
			var headers = resRange.Content.Headers;
			if (headers.ContentRange == null || !headers.ContentRange.Length.HasValue) return -1;
			return headers.ContentRange.Length.Value;
		}
		private static void WriteTemp(string path, Uri url, long total, long pos)
		{
			using var fs = File.OpenWrite(path);
			using BinaryWriter bw = new BinaryWriter(fs);
			bw.Write(url.ToString());
			bw.Write(total);
			bw.Write(pos);
		}
		private static string GetInfoFilePath(string path)
		{
			return Path.Combine(Path.GetDirectoryName(path)!, Path.GetFileName(path) + ".download");
		}
		public static (Uri url, long total, long pos) GetDownloadInfoInternal(string path)
		{
			using var fs = File.OpenRead(path);
			using BinaryReader br = new BinaryReader(fs);
			string fileUrl = br.ReadString();
			long total = br.ReadInt64();
			long pos = br.ReadInt64();
			return (new Uri(fileUrl), total, pos);
		}
		public static (Uri url, long total, long pos) GetDownloadInfo(string path)
		{
			return GetDownloadInfoInternal(GetInfoFilePath(path));
		}
		private static async Task<(Uri url, long total, long pos)> EnsureTemp(HttpClient client, string path, Uri? url)
		{
			string infoPath = GetInfoFilePath(path);
			if (File.Exists(infoPath))
			{
				var res = GetDownloadInfoInternal(infoPath);
				if (url == null || url == res.url)
				{
					return res;
				}
			}
			File.Delete(path);
			if (url == null) throw new Exception("download can't be resumed, need url.");
			long total = await GetRemoteFileLength(client, url);
			if (total == -1) throw new Exception("download can't be resumed, server not support byteservice.");
			WriteTemp(infoPath, url, total, 0);
			return (url, total, 0);
		}
		public static async Task DownloadPartial(HttpClient client, string path, Uri? url = null)
		{
			var (furl, total, initPos) = await EnsureTemp(client, path, url);
			url = furl;
			using var fs = File.OpenWrite(path);
			byte[] buffer = new byte[1048576];
			fs.Position = initPos;
			string infoPath = GetInfoFilePath(path);
			while (fs.Position < total)
			{
				var req = new HttpRequestMessage(HttpMethod.Get, url);
				req.Headers.Range = new System.Net.Http.Headers.RangeHeaderValue(fs.Position, fs.Position + DownloadBufferSize - 1 > total ? total : fs.Position + DownloadBufferSize - 1);
				using var res = await client.SendAsync(req, HttpCompletionOption.ResponseHeadersRead);
				if (res.StatusCode != System.Net.HttpStatusCode.PartialContent) throw new Exception("download can't be resumed, server not support byteservice.");
				long from = res.Content.Headers.ContentRange?.From ?? throw new Exception("download can't be resumed, server not support byteservice.");
				if (fs.Position != from) throw new Exception("download can't be resumed, server not support byteservice.");
				long to = res.Content.Headers.ContentRange?.To ?? throw new Exception("download can't be resumed, server not support byteservice.");

				using var s = await res.Content.ReadAsStreamAsync();
				fs.Flush();
				s.CopyTo(fs);
				WriteTemp(infoPath, url, total, fs.Position);
			}
		}
	}
}
