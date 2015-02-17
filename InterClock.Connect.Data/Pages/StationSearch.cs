using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using InterClock.Connect.Data.Models;
using InterClock.Connect.Data.Repos;

namespace InterClock.Connect.Data.Pages
{
	public class StationSearch : ContentPage
	{
		private ObservableCollection<StationInfo> stations;
		private ApiRepo api = new ApiRepo ();

		public StationSearch ()
		{
			Title = "Select Station";
			stations = new ObservableCollection<StationInfo> ();
			var searchBar = new SearchBar ();
			searchBar.Placeholder = "Search Stations";
			searchBar.SearchButtonPressed += async (object sender, EventArgs e) => {
				//do search, update results.
				var stationResult = await api.Search(searchBar.Text);
				if(stationResult != null && stationResult.Results != null){
					stations.Clear();
					stations.AddRange(stationResult.Results);
				}
			};

			var dataTemplate = new DataTemplate (typeof(TextCell));
			dataTemplate.SetBinding (TextCell.TextProperty, "Name");
			dataTemplate.SetBinding (TextCell.DetailProperty, "StreamUrl");

			var lv = new ListView () {
				ItemsSource = stations,
				ItemTemplate = dataTemplate,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			lv.ItemTapped += async (object sender, ItemTappedEventArgs e) => {
				var station = lv.SelectedItem as StationInfo;
				MessagingCenter.Send<StationInfo>(station, "StationSelected");
				(sender as ListView).SelectedItem = null;
				await Navigation.PopAsync();
			};

			Content = new StackLayout {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = 10,
				Spacing = 10,
				Children = {
					searchBar, lv
				}
			};
		}
	}
}

