using System;

namespace InterClock.Connect.Data.Interfaces
{
	internal interface IStatus<T> where T : class
	{
		string Message {get;set;}
		T Payload {get;set;}
	}
}

