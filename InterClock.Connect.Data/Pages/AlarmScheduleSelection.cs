using System;
using Xamarin.Forms;
using InterClock.Connect.Data.Models;

namespace InterClock.Connect.Data.Pages
{
	public class AlarmScheduleSelection : ContentPage
	{
		public AlarmScheduleSelection (bool isEndDay)
		{
			var title = isEndDay ? "End Day" : "Start Day";
			Title = title;

			TableView tv = new TableView () {
				Root = new TableRoot(){
					new TableSection("Select day"){

					}
				},
				Intent = TableIntent.Form
			};

			var section = tv.Root [0];

			var schedules = Enum.GetNames (typeof(AlarmSchedule));
			foreach (var item in schedules) {
				var row = new TextCell(){
					Text = item,
				};
				row.Tapped += async (object sender, EventArgs e) => {
					if(isEndDay){
						MessagingCenter.Send<String>(item, "EndAlarmScheduleSelected");
					}
					else{
						MessagingCenter.Send<String>(item, "BeginAlarmScheduleSelected");
					}
					await Navigation.PopAsync();
				};
				section.Add (row);
			}
			Content = tv;
		}
	}
}

