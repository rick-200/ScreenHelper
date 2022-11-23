

namespace ScreenHelper
{

	internal static class Program
	{
		readonly static string MutexName = "Rick Screenhelper" + Application.ProductVersion;


		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]

		static void Main()
		{
			//UpdateHelper.Update((_,_) => true, () => true).Wait();
			bool createNew;
			Mutex mutex = new Mutex(true, MutexName, out createNew);
			if (!createNew)
			{
				MessageBox.Show("�����������У�", "������������", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();
			//appli
			Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);

			Task.Run(async () =>
			{
				bool flag = true;
				while (flag)
				{
					try
					{
						await UpdateHelper.Update((needUpdate, newestVersion) =>
						{
							if (!needUpdate) { flag = false; return false; }
							return true;
						},
						(newestVersion) =>
						{
							var res = MessageBox.Show($"���°汾{newestVersion}��Ҫ������?", "ScreenHelper", MessageBoxButtons.OKCancel);
							return res == DialogResult.OK;
						}
						);
					}
					catch (DownloadFileDamageException)
					{
						MessageBox.Show("����ʧ�ܣ����ֶ����¡�", "ScreenHelper");
					}
					catch (Exception) { }
					await Task.Delay(1000);
				}
			});

			Application.Run(new HotKeyForm());
		}
	}
}