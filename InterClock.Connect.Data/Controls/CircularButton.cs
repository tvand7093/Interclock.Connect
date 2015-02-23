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
			var previousColor = BorderColor;

			PropertyChanging += (object sender, PropertyChangingEventArgs e) => {
				if(e.PropertyName == "IsEnabled"){
					//is enabled changed, so set border accordingly.
					//is it about to disable?
					if(IsEnabled){
						//disabling, so set to gray
						previousColor = BorderColor;
						BorderColor = gray;
					}
					else{
						BorderColor = previousColor;
					}
				}
			};
		}
	}
}

