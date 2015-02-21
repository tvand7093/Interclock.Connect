using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using InterClock.Connect.Data.Repos;
using System.Collections.ObjectModel;
using InterClock.Connect.Data.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace InterClock.Connect.Data.Pages
{
	public class AlarmList : ContentPage
	{
		ApiRepo api = new ApiRepo();
		public static AlarmList Create(){
			return new AlarmList ();
		}

		private ObservableCollection<Alarm> foundItems;
		public AlarmList ()
		{
			Title = "TEST";
			var status = new Label {
				Text = "Checking status...",
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.Start,
			};

			api.Status ().ContinueWith ((t) => {
				status.Text = t.Result.Message;
			}, TaskScheduler.FromCurrentSynchronizationContext());

			foundItems = new ObservableCollection<Alarm> ();
			var dataTemplate = new DataTemplate (typeof(TextCell));
			dataTemplate.SetBinding (TextCell.TextProperty, "Name");
			dataTemplate.SetBinding (TextCell.DetailProperty, "AlarmString");

			ListView lv = new ListView () {
				ItemsSource = foundItems,
				ItemTemplate = dataTemplate,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			lv.ItemTapped += async (object sender, ItemTappedEventArgs e) => {
				var station = lv.SelectedItem as Alarm;

				//var playResult = await api.Play(station.Id);
				await api.CreateAlarm(
					new Alarm(){
						//StationId = station.Id,
						Name = "Radio Due @ 1:27am",
						BeginDay = AlarmSchedule.Tuesday,
						EndDay = AlarmSchedule.Tuesday,
						Hour = 3,
						Minute = 6
					});
				status.Text = "Created Alarm";
				(sender as ListView).SelectedItem = null;
			};
				
			Content = new StackLayout {
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Spacing = 10,
				Padding = new Thickness(5, 20, 5, 0),
				Children = {
					status
					//status, search, searchButton, stopButton, lv
				}
			};
		}
	}
}

