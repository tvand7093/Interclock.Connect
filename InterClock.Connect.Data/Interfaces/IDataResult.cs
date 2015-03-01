using System;

namespace InterClock.Connect.Data.Interfaces
{
	internal interface IDataResult<T> : IStatus, IPayload<T> where T : class
	{
	}
}

