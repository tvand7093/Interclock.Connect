using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace InterClock.Connect.Data.Models
{
	[JsonObject]
	public class StationListResult : ApiResult
	{
		[JsonProperty("result")]
		public List<StationInfo> Results { get; set; }

		public StationListResult ()
		{

		}
	}
}

