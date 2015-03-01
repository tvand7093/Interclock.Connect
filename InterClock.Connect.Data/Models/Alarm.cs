using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace InterClock.Connect.Data.Models
{
	public class Alarm
	{
		[JsonProperty("alarmId")]
		public Guid Id {get; set;}

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

		[JsonProperty("isRunning")]
		public bool IsRunning { get; set; }

		[JsonProperty("isSnoozing")]
		public bool IsSnoozing { get; set; }

		public TimeSpan AlarmSpan { get; set; }

		public Alarm ()
		{
		}

		public void Copy(Alarm toCopy){
			Id = toCopy.Id;
			IsSnoozing = toCopy.IsSnoozing;
			IsRunning = toCopy.IsRunning;
			EndDay = toCopy.EndDay;
			BeginDay = toCopy.BeginDay;
			StationId = toCopy.StationId;
			Hour = toCopy.Hour;
			Minute = toCopy.Minute;
		}

		private string alarmString;
		public string AlarmString
		{
			get{
				if (String.IsNullOrEmpty (alarmString)) {
					var dayString = string.Empty;
					if (BeginDay == EndDay)
						dayString = BeginDay.ToString ();
					else if (BeginDay == AlarmSchedule.Weekdays) {
						dayString = "Weekdays (Monday - Friday)";
					} else if (BeginDay == AlarmSchedule.Weekends) {
						dayString = "Weekends (Saturday and Sunday)";
					} else {
						dayString = BeginDay.ToString() + " - " + EndDay.ToString();
					}

					alarmString = string.Format ("{0}:{1} {2}", Hour, Minute, dayString);
				}
				return alarmString;
			}

		}

		public IEnumerable<KeyValuePair<string, string>> ToFormData (){
			return new List<KeyValuePair<string, string>> {
				new KeyValuePair<string, string>("hour", this.Hour.ToString()),
				new KeyValuePair<string, string>("minute", this.Minute.ToString()),
				new KeyValuePair<string, string>("stationId", this.StationId.ToString()),
				new KeyValuePair<string, string>("isSnoozing", this.IsSnoozing.ToString()),
				new KeyValuePair<string, string>("beginDay", ((int)this.BeginDay).ToString()),
				new KeyValuePair<string, string>("endDay", ((int)this.EndDay).ToString()),
			};
		}
	}
}

