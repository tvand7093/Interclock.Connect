using System;
using Newtonsoft.Json;

namespace InterClock.Connect.Data.Models
{
	public class SearchParams
	{
		[JsonProperty("deviceId")]
		public string DeviceId {get;set;}

		[JsonProperty("search")]
		public string Search {get;set;}

		public SearchParams (string deviceId, string search)
		{
			DeviceId = deviceId;
			Search = search;
		}
	}
}

