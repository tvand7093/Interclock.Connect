using System;
using Xamarin.Forms;
using System.Diagnostics;
using InterClock.Connect.Data.Models;
using InterClock.Connect.Data.Repos;
using InterClock.Connect.Data.Controls;

namespace InterClock.Connect.Data.Pages
{
	public class AlarmEdit : ContentPage
	{
		private Alarm viewModel;
		private ApiRepo api = new ApiRepo();

		public AlarmEdit (Alarm vm = null)
		{
			Title = "Alarm info";
			viewModel = vm ?? new Alarm ();
			ToolbarItems.Add(new ToolbarItem("Save", string.Empty, async () => {
				Focus();
				//click the save button.
				viewModel.Hour = viewModel.AlarmSpan.Hours;
				viewModel.Minute = viewModel.AlarmSpan.Minutes;

				var alarm = await api.CreateAlarm(viewModel);
				MessagingCenter.Send<Alarm>(alarm, "AlarmManaged");
				viewModel = null;
				await Navigation.PopAsync();
			}));

			var name = new EntryCell {
				BindingContext = viewModel,
				Placeholder = "Alarm name"
			};
			name.SetBinding (Entry.TextProperty, "Name");

			var station = new RightDisclosureCell {
				BindingContext = viewModel,
				Text = "Station"
			};


			station.SetBinding (TextCell.TextProperty, "Station");
			station.Tapped += async (object sender, EventArgs e) => {
				await Navigation.PushAsync(new StationSearch());
			};

			MessagingCenter.Subscribe<StationInfo>(this, "StationSelected", stationSelected => {
				viewModel.StationId = stationSelected.Id;
				station.Text = stationSelected.Name;
				station.Detail = stationSelected.StreamUrl;
			});

			PickerCell timePicker = new PickerCell () {
				Time = new TimeSpan (17, 30, 0),
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.Fill,
				BindingContext = viewModel,
			};

			timePicker.SetBinding (TimePicker.TimeProperty, "AlarmSpan");

			var beginDay = new RightDisclosureCell {
				BindingContext = viewModel,
				Text = "Select Start Day..."
			};
			beginDay.SetBinding (TextCell.TextProperty, "BeginDay");

			beginDay.Tapped += async (object sender, EventArgs e) => {
				await Navigation.PushAsync(new AlarmScheduleSelection(false));
			};

			var endDay = new RightDisclosureCell {
				BindingContext = viewModel,
				Text = "Select End Day..."
			};

			endDay.SetBinding (TextCell.TextProperty, "EndDay");

			endDay.Tapped += async (object sender, EventArgs e) => {
				await Navigation.PushAsync(new AlarmScheduleSelection(true));
			};

			MessagingCenter.Subscribe<String> (this, "BeginAlarmScheduleSelected", schedule => {
				viewModel.BeginDay = (AlarmSchedule)Enum.Parse(typeof(AlarmSchedule), schedule);
				beginDay.Text = viewModel.BeginDay.ToString();
				if(viewModel.BeginDay == AlarmSchedule.Weekdays || viewModel.BeginDay == AlarmSchedule.Weekends){
					//weekend or weekday, so disable secondary selection.
					endDay.IsEnabled = false;
					viewModel.EndDay = viewModel.BeginDay;
					endDay.Text = viewModel.EndDay.ToString();
				}
			});

			MessagingCenter.Subscribe<String> (this, "EndAlarmScheduleSelected", schedule => {
				viewModel.EndDay = (AlarmSchedule)Enum.Parse(typeof(AlarmSchedule), schedule);
				endDay.Text = viewModel.EndDay.ToString();
				if(viewModel.EndDay == AlarmSchedule.Weekdays || viewModel.EndDay == AlarmSchedule.Weekends){
					//weekend or weekday, so disable secondary selection.
					beginDay.IsEnabled = false;
					viewModel.BeginDay = viewModel.EndDay;
					beginDay.Text = viewModel.BeginDay.ToString();
				}
			});

			TableView tv = new TableView () {
				Root = new TableRoot() {
					new TableSection("Alarm info"){
						name,
						station,
						beginDay,
						endDay,
						new ViewCell {
							View = timePicker
						}
					},
				},
				Intent = TableIntent.Form,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.Fill
			};
			Content = tv;
//			Content = new StackLayout {
//				Children = {
//					tv, timePicker, 
//				},
//				Spacing = 10,
//				HorizontalOptions = LayoutOptions.FillAndExpand,
//				VerticalOptions = LayoutOptions.FillAndExpand
//			};
		}
	}
}

