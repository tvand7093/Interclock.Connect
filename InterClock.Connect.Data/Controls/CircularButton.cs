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

			var gray = Color.FromHex ("#E8E8E8");
			Color enabledBorderColor = Color.Default;
			Color enabledTextColor = Color.Default;

			PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) => {
				if(e.PropertyName == "IsEnabled"){
					//is it enabled?
					if(IsEnabled){
						//enabled
						BorderColor = enabledBorderColor;
						TextColor = enabledTextColor;
					}
					else{
						BorderColor = gray;
						TextColor = gray;
					}
				}
			};

			PropertyChanging += (object sender, PropertyChangingEventArgs e) => {
				if(e.PropertyName == "IsEnabled" && enabledTextColor == Color.Default){
					enabledBorderColor = BorderColor;
					enabledTextColor = TextColor;
				}
			};
		}
	}
}

