using System;

namespace InterClock.Connect.Data.Interfaces
{
	public interface IPayload<T> where T : class
	{
		T Payload {get;set;}
	}
}

