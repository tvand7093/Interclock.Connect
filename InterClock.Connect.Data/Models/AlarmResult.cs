using System;
using Newtonsoft.Json;

namespace InterClock.Connect.Data.Models
{
	public class AlarmResult
	{
		[JsonProperty("result")]
		public Guid Id { get; set; }

		public AlarmResult ()
		{
		}
	}
}

