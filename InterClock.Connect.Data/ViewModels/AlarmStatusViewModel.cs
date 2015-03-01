using System;
using InterClock.Connect.Data.Interfaces;
using InterClock.Connect.Data.Models;
using System.Windows.Input;
using InterClock.Connect.Data.Repos;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace InterClock.Connect.Data.ViewModels
{
	internal class AlarmStatusViewModel : BaseViewModel
	{
		private string status;
		public string Status {
			get { return status; }
			set {
				status = value;
				OnPropertyChanged ("Status");
			}
		}

		public bool IsAlarmRunning {
			get { return CurrentAlarm == null ? false : CurrentAlarm.IsRunning; }
		}

		public Color OffColor { get { return Color.FromHex("#D62F37"); } }
		public Color SnoozeColor { get { return Color.FromHex("#38BD2B"); } }
		public Color RefreshColor { get { return Color.FromHex("#5355E6"); } }

		public ICommand RefreshCommand { protected set; get; }
		public ICommand StopCommand { protected set; get; }
		public ICommand SnoozeCommand { protected set; get; }

		private Alarm alarm;
		private Alarm CurrentAlarm {
			get { return alarm; }
			set {
				alarm = value;
				OnPropertyChanged ("IsAlarmRunning");
			}
		} 

		private async void MakeApiCall<T,R>(string beginMessage,
			Func<Task<T>> apiCall, Action<R> success)
			where T : IDataResult<R>
			where  R : class{

			Status = beginMessage.T();
			IsBusy = true;
			var apiResult = await apiCall ();
			Status = apiResult.Message.T ();
			IsBusy = false;
			success (apiResult.Payload);
		}

		private void SetupCommands(){
			Action fetchStatus = () => {
				MakeApiCall<AlarmResult, Alarm>("Checking status...", 
					() => ApiService.FetchStatus(),
					(data) => {
						if(data != null) {
							CurrentAlarm = data;
							MessagingCenter.Send(CurrentAlarm, "AlarmStatus");
							this.OnPropertyChanged("IsAlarmRunning");
						}
					});
			};

			Action turnOff = () => {
				if(!CurrentAlarm.IsSnoozing){
					MakeApiCall<AlarmResult, Alarm>("Turning off alarm.",
						() => ApiService.StopAlarm(),
						(alarm) => {
							CurrentAlarm = alarm;
							OnPropertyChanged("IsAlarmRunning");
						});
				}
			};

			Action enableSnooze = () => {
				if (!CurrentAlarm.IsSnoozing && CurrentAlarm.IsRunning) {
					MakeApiCall<AlarmResult, Alarm>("Sleeping for 5 min.",
						() => ApiService.Snooze(),
						(alarm) => {
							CurrentAlarm = alarm;
							OnPropertyChanged("IsAlarmRunning");
						});
				}
			};

			this.RefreshCommand = new Command(fetchStatus);
			this.SnoozeCommand = new Command (enableSnooze);
			this.StopCommand = new Command (turnOff);
		}

		public AlarmStatusViewModel ()
		{
			SetupCommands ();
			//delay status check by 1 second to allow for init
			Device.StartTimer(new TimeSpan(0, 0, 1),
			() => {
				RefreshCommand.Execute (this);
				//don't re-execute
				return false; 
			});
		}
	}
}

