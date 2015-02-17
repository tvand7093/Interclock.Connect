using System;
using InterClock.Connect.Data.Interfaces;
using Interop;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceProvider))]
namespace Interop
{
	public class DeviceProvider : IDeviceProvider
	{
		public string DeviceId { get { return "TYLERS PHONE"; } }
		public DeviceProvider ()
		{
		}
	}
}

