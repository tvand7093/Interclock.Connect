using System;

namespace InterClock.Connect.Data.Interfaces
{
	public interface IMessenger
	{
		void Subscribe();
		void Unsubscribe();
	}
}

