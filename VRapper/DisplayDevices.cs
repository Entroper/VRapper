using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace VRapper
{
	public static class DisplayDevices
	{
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
	}
}
