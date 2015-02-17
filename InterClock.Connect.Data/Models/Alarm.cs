using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace InterClock.Connect.Data.Models
{
	public class Alarm
	{
		public Guid Id {get; set;}
		public string Name {get;set;}

		[JsonProperty("minute")]
		public int Minute {get;set;}

		[JsonProperty("hour")]
		public int Hour {get;set;}

		[JsonProperty("beginDay")]
		public AlarmSchedule BeginDay { get; set; }
		[JsonProperty("endDay")]
		public AlarmSchedule EndDay {get;set;}
		[JsonProperty("stationId")]
		public int StationId {get;set;}
		public Alarm ()
		{
		}



		public IEnumerable<KeyValuePair<string, string>> ToFormData (){
			return new List<KeyValuePair<string, string>> {
				new KeyValuePair<string, string>("hour", this.Hour.ToString()),
				new KeyValuePair<string, string>("minute", this.Minute.ToString()),
				new KeyValuePair<string, string>("stationId", this.StationId.ToString()),
				new KeyValuePair<string, string>("beginDay", ((int)this.BeginDay).ToString()),
				new KeyValuePair<string, string>("endDay", ((int)this.EndDay).ToString()),
			};
		}
	}
}

