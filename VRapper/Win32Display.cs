using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace VRapper
{
	internal static class Win32Display
	{
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
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
		internal static extern bool EnumDisplayDevices([MarshalAs(UnmanagedType.LPTStr)] string lpDevice, int iDevNum,
		                                               ref DISPLAY_DEVICE lpDisplayDevice, int dwFlags);

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal struct RECT
		{
			internal int left;
			internal int top;
			internal int right;
			internal int bottom;
		}

		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		internal struct MONITORINFOEX
		{
			internal int cbSize;
			internal RECT rcMonitor;
			internal RECT rcWork;
			internal int dwFlags;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			internal string szDevice;
		}

		internal delegate bool MONITORENUMPROC(int hMonitor, int hdcMonitor, RECT lprcRect, int dwData);

		[DllImport("user32.dll", EntryPoint = "GetMonitorInfoW")]
		internal static extern bool GetMonitorInfo(int hMonitor, ref MONITORINFOEX lpmi);

		[DllImport("user32.dll")]
		// lprcClip is a RECT, but we need to be able to pass NULL, and we can't make RECT a class, because then MONITORINFOEX doesn't marshal correctly.
		internal static extern bool EnumDisplayMonitors(int hdc, IntPtr lprcClip, MONITORENUMPROC lpfnEnum, int dwData);
	}
}
