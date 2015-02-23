using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Xamarin.Forms;
using InterClock.Connect.Data;
using InterClock.Connect.Data.Pages;
using Xamarin.Forms.Platform.iOS;

namespace InterClock.Connect.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Forms.Init ();

			LoadApplication (new App());
			
			return base.FinishedLaunching (app, options);;
		}


	}
}

