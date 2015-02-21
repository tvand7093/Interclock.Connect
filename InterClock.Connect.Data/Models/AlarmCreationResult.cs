using System;
using Newtonsoft.Json;

namespace InterClock.Connect.Data.Models
{
	public class AlarmCreationResult : ApiResult
	{
		[JsonProperty("result")]
		public Guid Results {get;set;}

		public AlarmCreationResult ()
		{
		}
	}
}

