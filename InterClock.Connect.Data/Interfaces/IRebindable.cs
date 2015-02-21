using System;

namespace InterClock.Connect.Data.Interfaces
{
	public interface IRebindable<T> where T : class
	{
		void Rebind(T dataToBind);
	}
}

