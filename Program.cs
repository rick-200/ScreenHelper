


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
			bool createNew;
			Mutex mutex = new Mutex(true, MutexName, out createNew);
			if (!createNew)
			{
				MessageBox.Show("程序已在运行！", "程序已在运行", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();
			//appli
			Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
			Application.Run(new HotKeyForm());
		}
	}
}