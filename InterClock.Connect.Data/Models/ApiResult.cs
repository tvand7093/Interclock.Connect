using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace InterClock.Connect.Data.Models
{
	[JsonObject]
	public class ApiResult
	{
		[JsonProperty("message")]
		public string Message { get; set; }

		public ApiResult ()
		{

		}
	}
}

