using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VRapper.OVR
{
	enum SensorID
	{
		VendorID  = 0x2833,
		ProductID = 0x0001,

		// ST's VID used originally; should be removed in the future
		OldVendorID  = 0x0483,
		OldProductID = 0x5750
	}

	public struct TrackerSample
	{
		public int AccelX, AccelY, AccelZ;
		public int GyroX, GyroY, GyroZ;
	}

	public class TrackerSensors
	{
		public byte SampleCount;
		public ushort Timestamp;
		public ushort LastCommandID;
		public short Temperature;

		public TrackerSample[] Samples;

		public short MagX, MagY, MagZ;

		public TrackerSensors(byte[] buffer)
		{
			if (buffer.Length < 62)
				throw new ArgumentException("Tracker sensor buffer too small");

			SampleCount = buffer[1];
			Timestamp = BitConverter.ToUInt16(buffer, 2);
			LastCommandID = BitConverter.ToUInt16(buffer, 4);
			Temperature = BitConverter.ToInt16(buffer, 6);

			// Only unpack as many samples as there actually are
			int iterationCount = (SampleCount > 2) ? 3 : SampleCount;
			Samples = new TrackerSample[iterationCount];
			for (int i = 0; i < iterationCount; i++)
			{
				UnpackSensor(buffer, i);
			}

			MagX = BitConverter.ToInt16(buffer, 56);
			MagY = BitConverter.ToInt16(buffer, 58);
			MagZ = BitConverter.ToInt16(buffer, 60);
		}

		/* Original C++ unpack implementation from OVR_Win32_Sensor.cpp follows.
		static void UnpackSensor(const UByte* buffer, SInt32* x, SInt32* y, SInt32* z)
		{
			// Sign extending trick
			// from http://graphics.stanford.edu/~seander/bithacks.html#FixedSignExtend
			struct {SInt32 x:21;} s;

			*x = s.x = (buffer[0] << 13) | (buffer[1] << 5) | ((buffer[2] & 0xF8) >> 3);
			*y = s.x = ((buffer[2] & 0x07) << 18) | (buffer[3] << 10) | (buffer[4] << 2) |
					   ((buffer[5] & 0xC0) >> 6);
			*z = s.x = ((buffer[5] & 0x3F) << 15) | (buffer[6] << 7) | (buffer[7] >> 1);
		}
		*/

		private void UnpackSensor(byte[] buffer, int i)
		{
			int baseIndex = 8 + i*16;

			// Read a signed integer from 4 bytes, shift it down and it will sign extend for us.
			// The left shifts for the Y and Z components will overflow the extra bits, and then
			// the right shift will sign extend from the new, correct MSB.

			Samples[i].AccelX = BitConverter.ToInt32(buffer, baseIndex) >> 11;
			Samples[i].AccelY = (BitConverter.ToInt32(buffer, baseIndex + 2) << 5) >> 11;
			Samples[i].AccelZ = (BitConverter.ToInt32(buffer, baseIndex + 5) << 2) >> 11;

			Samples[i].GyroX = BitConverter.ToInt32(buffer, baseIndex + 8) >> 11;
			Samples[i].GyroY = (BitConverter.ToInt32(buffer, baseIndex + 10) << 5) >> 11;
			Samples[i].GyroZ = (BitConverter.ToInt32(buffer, baseIndex + 13) << 2) >> 11;

			// When i = 2, we will actually read 4 bytes starting from position 53, meaning we
			// read past the packed sample.  But we know the buffer contains additional data
			// there, and we are discarding it anyway.
		}
	}

	public class SensorDevice
	{
	}
}
