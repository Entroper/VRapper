using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace VRapper
{
	internal static class Win32Display
	{
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		internal struct DISPLAY_DEVICE
		{
			internal int cb;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			internal string DeviceName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string DeviceString;
			internal int StateFlags;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string DeviceID;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string DeviceKey;
		}

		[DllImport("user32.dll", EntryPoint = "EnumDisplayDevicesW")]
		internal static extern bool EnumDisplayDevices([MarshalAs(UnmanagedType.LPTStr)]string lpDevice, int iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, int dwFlags);
	}
}
