using System;
using InterClock.Connect.Data.Interfaces;
using System.Collections.ObjectModel;
using InterClock.Connect.Data.Models;
using System.Windows.Input;
using Xamarin.Forms;
using InterClock.Connect.Data.Repos;
using InterClock.Connect.Data.Pages;

namespace InterClock.Connect.Data.ViewModels
{
	internal sealed class StationSearchViewModel : BaseViewModel
	{
		private ObservableCollection<StationInfo> stations;
		public ObservableCollection<StationInfo> Stations {
			get { return stations; }
			set {
				stations.AddRange (value);
				OnPropertyChanged ("Stations");
			}
		}

		private string searchText;
		public string SearchText {
			get { return searchText; }
			set {
				searchText = value;
				OnPropertyChanged ("SearchText");
				OnPropertyChanged ("CanSearch");
			}
		}

		public bool CanSearch { get { return !String.IsNullOrEmpty (SearchText); } }

		public bool IsSearching { 
			get { return this.IsBusy; }
			set {
				this.IsBusy = value;
				OnPropertyChanged ("IsSearching");
			}
		}

		public ICommand SearchCommand { get; private set; }

		private StationInfo selectedStation;
		public StationInfo SelectedStation {
			get { return selectedStation; }
			set {
				selectedStation = value;
				OnPropertyChanged ("SelectedStation");
				if (selectedStation != null) {
					MessagingCenter.Send<StationInfo> (selectedStation, "StationSelected");
					var tab = (App.Current.MainPage as Root).CurrentPage;
					tab.Navigation.PopAsync();
				}
			}
		}
			
		public StationSearchViewModel ()
		{
			stations = new ObservableCollection<StationInfo> ();
			selectedStation = new StationInfo ();
			SearchCommand = new Command (async () => {
				IsSearching = true;
				var results = await ApiService.Search(SearchText);
				Stations.AddRange(results.Payload, true);
				IsSearching = false;
			}, () => CanSearch);
		}
	}
}

