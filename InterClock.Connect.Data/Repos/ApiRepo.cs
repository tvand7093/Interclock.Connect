using System;
using Newtonsoft.Json;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using InterClock.Connect.Data.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text;
using InterClock.Connect.Data.Interfaces;
using Xamarin.Forms;
using System.Net.Http;
using System.Collections.Generic;

namespace InterClock.Connect.Data.Repos
{
	internal sealed class ApiRepo
	{
		private const string ApiUrl = "http://10.0.1.100:3000/";
		private IDeviceProvider deviceInfo;

		private const string GetStationUrl = "station";
		private const string PlayStation = "play";
		private const string SearchStationUrl = "search";
		private const string StopPlay = "stop";
		private const string StatusEndpoint = "status";
		private const string ScheduleAlarmEndpoint = "schedule";
		private const string CancelAlarmEndpoint = "schedule/{0}";

		public ApiRepo ()
		{
			deviceInfo = DependencyService.Get<IDeviceProvider> ();
		}

		public async Task<AlarmResult> Status(){
			var status = new AlarmResult ();

			try{
				using (var client = new HttpClient ()) {
					client.BaseAddress = new Uri (ApiUrl);
					var result = await client.GetAsync(StatusEndpoint);
					if (result.IsSuccessStatusCode && result.StatusCode == System.Net.HttpStatusCode.OK) {
						//ok to process
						var json = await result.Content.ReadAsStringAsync ();
						status = JsonConvert.DeserializeObject<AlarmResult> (json);
					}
				}
			}
			catch(Exception ex){
				status.Message = "Could not connect to the server.";
				Debug.WriteLineIf (ex != null, ex.Message);
			}

			return status;
		}

		public async Task<ApiResult> CancelAlarm(Guid toCancel){
			using (var client = new HttpClient ()) {
				client.BaseAddress = new Uri (ApiUrl);

				var result = await client.DeleteAsync(string.Format(CancelAlarmEndpoint, toCancel.ToString()));

				if(result.IsSuccessStatusCode && result.StatusCode == System.Net.HttpStatusCode.OK){
					//ok to process
					var json = await result.Content.ReadAsStringAsync();
					return JsonConvert.DeserializeObject<ApiResult> (json);
				}
			}
			return null;
		}

		public async Task<Alarm> CreateAlarm(Alarm toSchedule){
			using (var client = new HttpClient ()) {
				client.BaseAddress = new Uri (ApiUrl);
				var result = await client.PutAsync(ScheduleAlarmEndpoint,
					new FormUrlEncodedContent(toSchedule.ToFormData()));
					
				if(result.IsSuccessStatusCode && result.StatusCode == System.Net.HttpStatusCode.OK){
					//ok to process
					var json = await result.Content.ReadAsStringAsync();
					var respData = JsonConvert.DeserializeObject<AlarmCreationResult> (json);
					//set our object id to that which the server assigned.
					toSchedule.Id = respData.Results;
					return toSchedule;
				}
				Debug.WriteLine ("Alarm set failed: " + result.ReasonPhrase);
			}
			return null;
		}

		public async Task<StationListResult> Search(string toSearch){
			using (var client = new HttpClient ()) {
				client.BaseAddress = new Uri (ApiUrl);
				var data = new List<KeyValuePair<string, string>> {
					new KeyValuePair<string, string>("deviceId", deviceInfo.DeviceId),
					new KeyValuePair<string, string>("search", toSearch)
				};
					
				var result = await client.PostAsync(SearchStationUrl, new FormUrlEncodedContent(data));
				if(result.IsSuccessStatusCode && result.StatusCode == System.Net.HttpStatusCode.OK){
					//ok to process
					var json = await result.Content.ReadAsStringAsync();
					return JsonConvert.DeserializeObject<StationListResult> (json);
				}
			}
			return null;
		}

		public async Task<AlarmResult> Snooze(Guid currentAlarm, int stationId) {
			throw new NotImplementedException ();
			await CancelAlarm (currentAlarm);
			return null;
//			var newAlarm = new Alarm () {
//				Hour = 
//				BeginDay = (AlarmSchedule)DateTime.Now.DayOfWeek,
//				EndDay = (AlarmSchedule)DateTime.Now.DayOfWeek,
//				StationId = stationId,
//			};
		}

		public async Task<ApiResult> Play(int stationId){
			using (var client = new HttpClient ()) {
				client.BaseAddress = new Uri (ApiUrl);
				var data = new List<KeyValuePair<string, string>> {
					new KeyValuePair<string, string>("deviceId", deviceInfo.DeviceId),
					new KeyValuePair<string, string>("stationId", stationId.ToString())
				};

				var result = await client.PostAsync(PlayStation, new FormUrlEncodedContent(data));
				if(result.IsSuccessStatusCode && result.StatusCode == System.Net.HttpStatusCode.OK){
					//ok to process
					var json = await result.Content.ReadAsStringAsync();
					return JsonConvert.DeserializeObject<ApiResult> (json);
				}
			}
			return null;
		}

	}
}

