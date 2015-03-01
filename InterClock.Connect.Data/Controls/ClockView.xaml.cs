using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace InterClock.Connect.Data.Controls
{
	public partial class ClockView : ContentView
	{
		public static readonly BindableProperty ShowSecondsProperty = 
			BindableProperty.Create<ClockView, bool>(
				getter: cv => cv.ShowSeconds,
				defaultValue: true,
				propertyChanging: (bindable, oldValue, newValue) => {
					var cv = (ClockView)bindable;
					cv.ShowSeconds = newValue;
				}
			);

		public bool ShowSeconds {
			get { return (bool)GetValue (ShowSecondsProperty); }
			set { SetValue (ShowSecondsProperty, value); }
		}
			
		public ClockView ()
		{
			InitializeComponent ();
			Device.StartTimer (new TimeSpan (0, 0, 1),
				() => {
					DateLabel.Text = DateTime.Now.ToString("d");
					TimeLabel.Text = ShowSeconds ? DateTime.Now.ToString("T") : DateTime.Now.ToString("t");
					return true;
				});
		}
	}
}

