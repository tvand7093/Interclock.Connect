using System;
using Newtonsoft.Json;

namespace InterClock.Connect.Data.Models
{
	public class Station
	{
		[JsonProperty("id")]
		public int Id {get;set;}

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("streamurl")]
		public string StreamUrl { get; set; }

		public Station ()
		{
		}
	}
}

