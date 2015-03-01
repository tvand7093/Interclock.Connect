using System;
using System.Collections.ObjectModel;
using InterClock.Connect.Data.Models;
using InterClock.Connect.Data.Pages;
using Xamarin.Forms;

namespace InterClock.Connect.Data.ViewModels
{
	public class AlarmScheduleViewModel : BaseViewModel
	{
		private INavigation navigation;

		private AlarmScheduleInfo schedule;
		public AlarmScheduleInfo Schedule {
			get { return schedule; }
			set {
				schedule = value;
				OnPropertyChanged ("Schedule");
				//MessagingCenter.Send (schedule, "ScheduleSelected");
				navigation.PopAsync ();
			}
		}

		ObservableCollection<AlarmScheduleInfo> days;
		public ObservableCollection<AlarmScheduleInfo> Days {
			get {
				return days;
			}
			set {
				days = value;
			}
		}

		public AlarmScheduleViewModel ()
		{
			navigation = (App.Current.MainPage as Root).CurrentPage.Navigation;
			Schedule = new AlarmScheduleInfo(AlarmSchedule.NotSpecified);
			days = new ObservableCollection<AlarmScheduleInfo> () {
					new AlarmScheduleInfo(AlarmSchedule.Monday),
					new AlarmScheduleInfo(AlarmSchedule.Tuesday),
					new AlarmScheduleInfo(AlarmSchedule.Wednesday),
					new AlarmScheduleInfo(AlarmSchedule.Thursday),
					new AlarmScheduleInfo(AlarmSchedule.Friday),
					new AlarmScheduleInfo(AlarmSchedule.Saturday),
					new AlarmScheduleInfo(AlarmSchedule.Sunday),
					new AlarmScheduleInfo(AlarmSchedule.Weekdays),
					new AlarmScheduleInfo(AlarmSchedule.Weekends),
					new AlarmScheduleInfo(AlarmSchedule.NotSpecified)
				};
		}
	}
}

