using System;

namespace InterClock.Connect.Data.Interfaces
{
	internal interface IRebindable<T> where T : class
	{
		void Rebind(T dataToBind);
	}
}

