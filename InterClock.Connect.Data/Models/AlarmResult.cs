using System;
using Newtonsoft.Json;

namespace InterClock.Connect.Data.Models
{
	public class AlarmResult : ApiResult
	{
		public const int Running = 0;
		public const int Inactive = -1;

		[JsonProperty("result")]
		public AlarmStatus Results { get; set; }

		public AlarmResult ()
		{
		}
	}
}

