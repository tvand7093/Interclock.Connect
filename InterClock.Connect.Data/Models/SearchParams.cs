using System;
using Newtonsoft.Json;

namespace InterClock.Connect.Data.Models
{
	public class SearchParams
	{
		[JsonProperty("search")]
		public string Search {get;set;}

		public SearchParams (string search)
		{
			Search = search;
		}
	}
}

