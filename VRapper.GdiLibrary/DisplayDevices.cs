using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace VRapper
{
	public static class DisplayDevices
	{
		private static IList<MonitorInfoEx> _monitorInfo = null;
 
		public static IEnumerable<DisplayDevice> EnumerateDevices()
		{
			int i = 0;
			var iDevice = CreateDisplayDeviceData();
			while (Win32Display.EnumDisplayDevices(null, i, ref iDevice, 0))
			{
				i++;
				int j = 0;
				var jDevice = CreateDisplayDeviceData();
				while (Win32Display.EnumDisplayDevices(iDevice.DeviceName, j, ref jDevice, 0))
				{
					j++;
					yield return new DisplayDevice(jDevice);
				}
			}
		}

		private static Win32Display.DISPLAY_DEVICE CreateDisplayDeviceData()
		{
			var displayDeviceData = new Win32Display.DISPLAY_DEVICE();

			displayDeviceData.cb = Marshal.SizeOf(displayDeviceData);

			displayDeviceData.DeviceName = String.Empty;
			displayDeviceData.DeviceString = String.Empty;
			displayDeviceData.DeviceID = String.Empty;
			displayDeviceData.DeviceKey = String.Empty;

			return displayDeviceData;
		}

		public static IEnumerable<MonitorInfoEx> EnumerateMonitors()
		{
			_monitorInfo = new List<MonitorInfoEx>();
			Win32Display.EnumDisplayMonitors(0, IntPtr.Zero, MonitorEnumCallback, 0);
			
			var list = _monitorInfo;
			_monitorInfo = null;
			return list;
		}

		private static bool MonitorEnumCallback(int hMonitor, int hdcMonitor, Win32Display.RECT lprcRect, int dwData)
		{
			var monitorInfoData = CreateMonitorInfoData();
			Win32Display.GetMonitorInfo(hMonitor, ref monitorInfoData);

			_monitorInfo.Add(new MonitorInfoEx(monitorInfoData));

			return true;
		}

		private static Win32Display.MONITORINFOEX CreateMonitorInfoData()
		{
			var monitorInfoData = new Win32Display.MONITORINFOEX();

			monitorInfoData.cbSize = Marshal.SizeOf(monitorInfoData);

			monitorInfoData.rcMonitor = new Win32Display.RECT();
			monitorInfoData.rcWork = new Win32Display.RECT();

			monitorInfoData.dwFlags = 0;

			monitorInfoData.szDevice = String.Empty;

			return monitorInfoData;
		}
	}
}
