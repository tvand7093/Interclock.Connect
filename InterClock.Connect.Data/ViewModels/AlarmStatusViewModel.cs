using System;
using InterClock.Connect.Data.Interfaces;
using InterClock.Connect.Data.Models;
using System.Windows.Input;
using InterClock.Connect.Data.Repos;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace InterClock.Connect.Data.ViewModels
{
	public class AlarmStatusViewModel : NotificationBase, IRebindable<AlarmResult>
	{
		private ApiRepo api;

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

		private bool alarmRunning;
		public bool IsAlarmRunning {
			get { return alarmRunning; }
			set {
				alarmRunning = value;
				OnPropertyChanged ("IsAlarmRunning");
			}
		}

		public Color OffColor { get { return Color.FromHex("#D62F37"); } }
		public Color SnoozeColor { get { return Color.FromHex("#38BD2B"); } }
		public Color RefreshColor { get { return Color.FromHex("#5355E6"); } }

		public ICommand RefreshCommand { protected set; get; }
		public ICommand StopCommand { protected set; get; }
		public ICommand SnoozeCommand { protected set; get; }


		public AlarmStatusViewModel ()
		{
			api = new ApiRepo ();

			Device.StartTimer(TimeSpan.FromSeconds(1), () =>
				{
					CurrentTime = DateTime.Now;
					return true;
				});
			api.CreateAlarm(new Alarm(){
				StationId = 9876,
				EndDay = AlarmSchedule.Saturday,
				BeginDay = AlarmSchedule.Saturday,
				Hour = DateTime.Now.Hour,
				Minute = DateTime.Now.Minute + 1
			});

			AlarmResult currentAlarm = null;
			this.RefreshCommand = new Command(async () =>
				{
					try{
						Status = "Checking Status...".T();
						var status = await api.Status();
						currentAlarm = status;
						Rebind(status);
					}
					catch(Exception){
						Status = "Error fetching status.".T();
						IsAlarmRunning = false;
					}
				});
					
			this.SnoozeCommand = new Command (async () => {
				Status = "Sleeping for 1 min...".T();
				await api.CancelAlarm(currentAlarm.Results.AlarmId);



				var alarm = await api.CreateAlarm(new Alarm(){
					StationId = 9876,
					EndDay = AlarmSchedule.Saturday,
					BeginDay = AlarmSchedule.Saturday, 
					Hour = DateTime.Now.Hour,
					Minute = DateTime.Now.Minute + 5
								});
				currentAlarm = new AlarmResult(){
					Results = new AlarmStatus(){
						AlarmId = alarm.Id
					}
				};

				//snooze somehow
			}, () => IsAlarmRunning);

			this.StopCommand = new Command (async () => {
				Status = "Turning off alarm".T();
				try{
					await api.CancelAlarm(currentAlarm.Results.AlarmId);
					Status = "Alarm disabled".T();
					IsAlarmRunning = false;
					currentAlarm = null;
				}
				catch(Exception){
					Status = "Error stopping alarm.".T();
					IsAlarmRunning = true;
				}
			}, () => IsAlarmRunning);

			RefreshCommand.Execute (this);
		}

		public void Rebind (AlarmResult toBind)
		{
			Status = toBind.Message;
			IsAlarmRunning = toBind.Code == AlarmResult.Running;
		}
	}
}

