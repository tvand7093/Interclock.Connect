using System;
using Newtonsoft.Json;

namespace InterClock.Connect.Data.Models
{
	[JsonObject]
	public class AlarmStatus : ApiResult
	{
		[JsonProperty("result")]
		public Guid AlarmId { get; set; }

		[JsonProperty("name")]
		public string Name {get;set;}

		[JsonProperty("url")]
		public string WebsiteUrl {get;set;}

		public AlarmStatus ()
		{
		}
	}
}

