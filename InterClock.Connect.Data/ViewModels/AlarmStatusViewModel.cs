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

		private DateTime currentTime;
		public DateTime CurrentTime {
			get { return currentTime; }
			set {
				currentTime = value;
				OnPropertyChanged ("CurrentTime");
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

		private void SetupCommands(){
			Action fetchStatus = async () => {
				Status = "Checking Status...".T();
				var status = await ApiService.FetchStatus();
				CurrentAlarm = status.Payload;
				Status = status.Message;
				this.OnPropertyChanged("IsAlarmRunning");
			};

			Action turnOff = async () => {
				if(!CurrentAlarm.IsSnoozing){
					//only allow turning off if not snoozing.
					Status = "Turning off alarm".T();
					try{
						await ApiService.CancelAlarm(CurrentAlarm.Id);
						Status = "Alarm disabled".T();
						CurrentAlarm = null;
					}
					catch(Exception){
						Status = "Error stopping alarm.".T();
					}

					OnPropertyChanged("IsAlarmRunning");
				}
			};

			Action enableSnooze = async () => {
				if (!CurrentAlarm.IsSnoozing && CurrentAlarm.IsRunning) {
					Status = "Sleeping for 5 min...".T ();
					var response = await ApiService.Snooze();
					CurrentAlarm = response.Payload;
				}
			};

			this.RefreshCommand = new Command(fetchStatus);
			this.SnoozeCommand = new Command (enableSnooze, () => IsAlarmRunning);
			this.StopCommand = new Command (turnOff, () => IsAlarmRunning);
		}

		public AlarmStatusViewModel ()
		{
			Device.StartTimer(TimeSpan.FromSeconds(1), () =>
				{
					CurrentTime = DateTime.Now;
					return true;
				});

			SetupCommands ();
			RefreshCommand.Execute (this);
		}
	}
}

