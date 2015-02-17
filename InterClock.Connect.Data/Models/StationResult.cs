using System;
using InterClock.Connect.Data.Models;
using Newtonsoft.Json;

namespace InterClock.Connect.Data.Models
{
	[JsonObject]
	public class StationResult : ApiResult
	{
		[JsonProperty("result")]
		public Station Result { get; set; }

		public StationResult ()
		{
		}
	}
}

