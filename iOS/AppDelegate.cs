using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Xamarin.Forms;
using InterClock.Connect.Data;
using InterClock.Connect.Data.Pages;

namespace InterClock.Connect.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Forms.Init ();

			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			window.RootViewController = (new NavigationPage(new AlarmEdit())).CreateViewController ();
			window.MakeKeyAndVisible ();
			
			return true;
		}


	}
}

