using System;

using Xamarin.Forms;

namespace InterClock.Connect.Data.Pages
{
	public class Root : TabbedPage
	{
		public Root ()
		{
			Children.Add (new NavRoot(new AlarmStatus ()));
			Children.Add (new NavRoot(new AlarmList ()));
		}
		public static Root Create(){
			return new Root ();
		}
	}
}


