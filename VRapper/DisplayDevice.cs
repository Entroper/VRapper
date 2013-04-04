using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VRapper
{
	public class DisplayDevice
	{
		[Flags]
		public enum StateFlags
		{
			Active = 0x00000001,
			MultiDriver = 0x00000002,
			PrimaryDevice = 0x00000004,
			MirroringDriver = 0x00000008,
			VgaCompatible = 0x00000010,
			Removable = 0x00000020,
			UnsafeModesOn = 0x00080000,
			TsCompatible = 0x00200000,
			Disconnect = 0x02000000,
			Remote = 0x04000000,
			ModesPruned = 0x08000000,
		}

		public string DeviceName { get; private set; }
		public string DeviceString { get; private set; }
		public string DeviceID { get; private set; }
		public string DeviceKey { get; private set; }

		public StateFlags Flags { get; private set; }

		internal DisplayDevice(Win32Display.DISPLAY_DEVICE device)
		{
			DeviceName = device.DeviceName;
			DeviceString = device.DeviceString;
			DeviceID = device.DeviceID;
			DeviceKey = device.DeviceKey;
			Flags = (StateFlags)device.StateFlags;
		}
	}
}
