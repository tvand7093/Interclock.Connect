using System;
using InterClock.Connect.Data.Models;
using InterClock.Connect.Data.Interfaces;
using Xamarin.Forms;
using System.Windows.Input;
using InterClock.Connect.Data.Pages;
using InterClock.Connect.Data.Repos;

namespace InterClock.Connect.Data.ViewModels
{
	internal sealed class AlarmEditViewModel : BaseViewModel, IMessenger
	{
		private Alarm alarm;

		public string Name{
			get { return alarm.Name; }
			set {
				alarm.Name = value;
				OnPropertyChanged ("Name");
				OnPropertyChanged ("CanSave");
			}
		}

		public AlarmScheduleInfo BeginDay {
			get { return new AlarmScheduleInfo(alarm.BeginDay); }
			set {
				alarm.BeginDay = value.Schedule;
				OnPropertyChanged ("BeginDay");
				OnPropertyChanged ("CanSave");
			}
		}

		public AlarmScheduleInfo EndDay{
			get { return new AlarmScheduleInfo(alarm.EndDay); }
			set {
				alarm.EndDay = value.Schedule;
				OnPropertyChanged ("EndDay");
				OnPropertyChanged ("CanSave");
			}
		}

		private StationInfo station;
		public StationInfo Station {
			get { return station; }
			set {
				station = value;
				alarm.StationId = station.Id;
				OnPropertyChanged ("Station");
				OnPropertyChanged ("StationName");
				OnPropertyChanged ("CanSave");
			}
		}

		public string StationName {
			get {
				return Station == null ? "Select Station" : Station.Name;
			}
		}

		public TimeSpan AlarmTime {
			get { return alarm.AlarmSpan; }
			set {
				alarm.AlarmSpan = value;
				OnPropertyChanged ("AlarmTime");
			}
		}

		public bool CanSave {
			get {
				return Station != null && EndDay.Schedule != AlarmSchedule.NotSpecified
				&& BeginDay.Schedule != AlarmSchedule.NotSpecified && !String.IsNullOrEmpty (Name);
			}
		}
		private bool isBeginSchedule = false;
		public void Subscribe() {
			MessagingCenter.Subscribe<StationInfo> (this, "StationSelected", (station) => {
				Station = station;
			});
			MessagingCenter.Subscribe<AlarmScheduleInfo> (this, "ScheduleSelected",
				(begin) => {
					if(isBeginSchedule) {
						BeginDay = begin;
					}
					else{
						EndDay = begin;
					}
			});
		}

		public void Unsubscribe() {
			MessagingCenter.Unsubscribe<StationInfo> (this, "StationSelected");
			MessagingCenter.Unsubscribe<AlarmScheduleInfo> (this, "BeginScheduleSelected");
			MessagingCenter.Unsubscribe<AlarmScheduleInfo> (this, "EndScheduleSelected");

		}

		public ICommand SelectStationCommand { get; private set; }
		public ICommand SelectScheduleCommand { get; private set; }
		public ICommand SaveCommand { get; private set; }

		public AlarmEditViewModel ()
		{
			alarm = new Alarm ();

			var api = new ApiRepo ();
//			api.CreateAlarm (new Alarm () {
//				Hour = 7,
//				Minute = 30,
//				StationId = 9876,
//				EndDay = AlarmSchedule.Weekdays,
//				BeginDay = AlarmSchedule.Weekdays
//			});

			SelectStationCommand = new Command (async () => {
				var tab = (App.Current.MainPage as Root).CurrentPage;
				await tab.Navigation.PushAsync (new StationSearch ());
			});

			SelectScheduleCommand = new Command<string> (async (begin) => {
				isBeginSchedule = begin == "begin";

				var tab = (App.Current.MainPage as Root).CurrentPage;
				await tab.Navigation.PushAsync (new AlarmScheduleSelection ());
			});
			SaveCommand = new Command (async() => {
				var api2 = new ApiRepo();
				this.IsBusy = true;
				var newAlarm = await api2.CreateAlarm(alarm);
				alarm.Id = newAlarm.Id;
				this.IsBusy = false;
			}, () => CanSave);
		}
	}
}

