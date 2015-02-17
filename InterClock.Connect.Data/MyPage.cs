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

namespace InterClock.Connect.Data
{
	public class MyPage : ContentPage
	{
		ApiRepo api = new ApiRepo();
		public static MyPage Create(){
			return new MyPage ();
		}

		private ObservableCollection<StationInfo> foundItems;
		public MyPage ()
		{
			var status = new Label {
				Text = "Checking status...",
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.Start,
			};

			var stopButton = new Button {
				Text = "Stop",
				VerticalOptions = LayoutOptions.Fill,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				IsEnabled = false
			};

			Disappearing += (object sender, EventArgs e) => {
				status.Text = "Checking status...";
				stopButton.IsEnabled = false;
			};

			Appearing += (object sender, EventArgs e) => {
				api.Status ().ContinueWith ((t) => {
					status.Text = t.Result.Message;
					stopButton.IsEnabled = t.Result.Message.CompareTo("Playing audio.") == 0;
				}, TaskScheduler.FromCurrentSynchronizationContext());
			};


			var searchButton = new Button {
				Text = "Search",
				VerticalOptions = LayoutOptions.Fill,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
			};

			stopButton.Clicked += async (s, e) => {
				var result = await api.Stop ();
				if (result != null) {
					status.Text = result.Message;
				}
				stopButton.IsEnabled = false;
			};

			var search = new Entry {
				Placeholder = "Search..."
			};

			searchButton.Clicked += async (s, e) => {
				if(String.IsNullOrEmpty(search.Text)){
					//no search string, so just clear results.
					foundItems.Clear();
				}
				var result = await api.Search (search.Text);
				if (result != null) {
					foundItems.Clear();
					foreach (StationInfo item in result.Results) {
						foundItems.Add(item);
					}
				}
			};
			foundItems = new ObservableCollection<StationInfo> ();
			var dataTemplate = new DataTemplate (typeof(TextCell));
			dataTemplate.SetBinding (TextCell.TextProperty, "Name");
			dataTemplate.SetBinding (TextCell.DetailProperty, "Website");

			ListView lv = new ListView () {
				ItemsSource = foundItems,
				ItemTemplate = dataTemplate,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			lv.ItemTapped += async (object sender, ItemTappedEventArgs e) => {
				var station = lv.SelectedItem as StationInfo;

				//var playResult = await api.Play(station.Id);
				await api.CreateAlarm(
					new Alarm(){
						StationId = station.Id,
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
					status, search, searchButton, stopButton, lv
				}
			};
		}
	}
}

