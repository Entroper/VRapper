using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HidLibrary;
using VRapper;

namespace TestConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			var hidDevices = HidDevices.Enumerate().ToList();
			var displayDevices = DisplayDevices.EnumerateDevices().ToList();
			var monitors = DisplayDevices.EnumerateMonitors().ToList();
			Console.ReadLine();
		}
	}
}
