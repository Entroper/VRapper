using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VRapper
{
	public class MonitorInfoEx
	{
		public struct Rectangle
		{
			public long left;
			public long top;
			public long right;
			public long bottom;

			internal Rectangle(Win32Display.RECT rect)
			{
				left = rect.left;
				top = rect.top;
				right = rect.right;
				bottom = rect.bottom;
			}
		}

		[Flags]
		public enum MonitorFlags
		{
			Primary = 0x00000001,
		}

		public Rectangle MonitorRectangle { get; private set; }
		public Rectangle WorkRectangle { get; private set; }

		public MonitorFlags Flags { get; private set; }

		internal MonitorInfoEx(Win32Display.MONITORINFOEX monitor)
		{
			MonitorRectangle = new Rectangle(monitor.rcMonitor);
			WorkRectangle = new Rectangle(monitor.rcWork);

			Flags = (MonitorFlags)monitor.dwFlags;
		}
	}
}
