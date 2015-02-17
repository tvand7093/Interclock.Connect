using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace InterClock.Connect.Data.Models
{
	[JsonObject]
	public class ApiResult
	{
		[JsonProperty("code")]
		public int Code { get; set; }

		[JsonProperty("message")]
		public string Message { get; set; }

		[JsonProperty("deviceId")]
		public string DeviceId { get; set; }

		public ApiResult ()
		{

		}
	}
}

