using System;
using Xamarin.Forms;

namespace InterClock.Connect.Data.Controls
{
	public class CircularButton : Button
	{
		public CircularButton ()
		{
			BorderWidth = 1;
			WidthRequest = HeightRequest = 65;
			BorderRadius = 32;
		}
	}
}

