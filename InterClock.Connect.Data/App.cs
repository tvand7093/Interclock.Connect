using System;

using Xamarin.Forms;
using InterClock.Connect.Data.Pages;

namespace InterClock.Connect.Data
{
	public class App : Application
	{
		public App ()
		{
			MainPage = new Root ();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

	}
}


