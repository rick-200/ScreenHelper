using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ScreenHelper
{
	internal class AutoRunHelper
	{
		public static readonly string RegKeyName = "RicksScreenHelper";
		public static void RegisterAutoRun(bool doAutoRun)
		{
			if (doAutoRun == AutoRunCheck()) return;
			using RegistryKey? key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
			if (key == null) throw new Exception("can't open HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run");
			if (doAutoRun)
				key.SetValue(RegKeyName, Application.ExecutablePath, RegistryValueKind.String);
			else
				key.DeleteValue(RegKeyName, false);
		}
		public static bool AutoRunCheck()
		{
			using RegistryKey? key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", RegistryRights.QueryValues);
			if (key == null) throw new Exception("can't open HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run");
			if ((string?)key.GetValue(RegKeyName, null) != Application.ExecutablePath) return false;
			return true;
		}
	}
}
