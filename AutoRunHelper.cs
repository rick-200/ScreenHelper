using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenHelper
{
	internal class AutoRunHelper
	{
		public static readonly string RegKeyName = "RicksScreenHelper";
		public static void RegisterAutoRun(bool doAutoRun)
		{
			if (doAutoRun)
			{
				RegistryKey? key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
				if (key == null) throw new Exception("can't open HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run");
				key.SetValue(RegKeyName, Application.ExecutablePath, RegistryValueKind.String);
			}
			else
			{
				RegistryKey? key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
				if (key == null) throw new Exception("can't open HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run");
				key.DeleteValue(RegKeyName, false);
			}
		}
	}
}
