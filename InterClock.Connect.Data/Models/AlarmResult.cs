﻿using System;
using Newtonsoft.Json;
using InterClock.Connect.Data.Interfaces;

namespace InterClock.Connect.Data.Models
{
	internal class AlarmResult : ApiResult, IDataResult<Alarm>
	{
		[JsonProperty("result")]
		public Alarm Payload { get; set; }

		public AlarmResult ()
		{
		}
	}
}

