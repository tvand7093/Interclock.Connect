using System;

namespace InterClock.Connect.Data.Models
{
	public class AlarmScheduleInfo
	{
		public AlarmSchedule Schedule { get; set; }
		public string Name {
			get {
				return Schedule == AlarmSchedule.NotSpecified ? "Not Specified" 
						: Enum.GetName (typeof(AlarmSchedule), Schedule);
			}
		}
		public AlarmScheduleInfo (AlarmSchedule day)
		{
			Schedule = day;
		}
		public AlarmScheduleInfo (int day)
		{
			Schedule = (AlarmSchedule)day;
		}
	}
}

