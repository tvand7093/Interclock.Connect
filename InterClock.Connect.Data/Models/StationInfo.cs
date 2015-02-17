using System;
using Newtonsoft.Json;

namespace InterClock.Connect.Data.Models
{
	public class StationInfo : Station
	{
		[JsonProperty("website")]
		public string Website { get; set; }
		[JsonProperty("country")]
		public string Country { get; set; }
		[JsonProperty("bitrate")]
		public string Bitrate { get; set; }
		[JsonProperty("status")]
		public int Status { get; set; }

		public StationInfo ()
		{
		}
	}
}

