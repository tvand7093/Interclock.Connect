using System;
using InterClock.Connect.Data.Models;
using Newtonsoft.Json;
using InterClock.Connect.Data.Interfaces;

namespace InterClock.Connect.Data.Models
{
	[JsonObject]
	public class StationResult : ApiResult, IStatus<Station>
	{
		[JsonProperty("result")]
		public Station Payload { get; set; }

		public StationResult ()
		{
		}
	}
}

