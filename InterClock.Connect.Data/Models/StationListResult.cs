using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using InterClock.Connect.Data.Interfaces;

namespace InterClock.Connect.Data.Models
{
	[JsonObject]
	public class StationListResult : ApiResult, IStatus<List<StationInfo>>
	{
		[JsonProperty("result")]
		public List<StationInfo> Payload { get; set; }

		public StationListResult ()
		{

		}
	}
}

