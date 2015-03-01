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
	internal static class ApiService
	{
		private const string ApiUrl = "http://10.0.1.100:3000/";
		private const string GetStationUrl = "station";
		private const string PlayEndpoint = "play";
		private const string SearchEndpoint = "search";
		private const string StopEndpoint = "stop";
		private const string StatusEndpoint = "status";
		private const string ScheduleAlarmEndpoint = "schedule";
		private const string CancelAlarmEndpoint = "schedule/{0}";
		private const string SnoozeEndpoint = "snooze";

		private static async Task<T> SendData<T, K> (string endpoint, HttpMethod method,
			IEnumerable<KeyValuePair<string, string>> content = null)
			where T : class, IDataResult<K>, new()
			where K : class, new() {
			//create default for result
			var status = new T ();

			HttpClient client = null;
			try{
				//create REST client
				client = new HttpClient();
				client.BaseAddress = new Uri (ApiUrl);
				//set time out to just 5 seconds
				client.Timeout = new TimeSpan(0, 0, 5);
				HttpResponseMessage result = null;

				FormUrlEncodedContent data = null;
				if(content != null)
					data = new FormUrlEncodedContent(content);

				//determine how to send the data
				if(method == HttpMethod.Get)
					result = await client.GetAsync(endpoint);

				if(method == HttpMethod.Put)
					result = await client.PutAsync(endpoint, data);

				if(method == HttpMethod.Delete)
					result = await client.DeleteAsync(endpoint);

				if(method == HttpMethod.Post)
					result = await client.PostAsync(endpoint, data);

				if(result != null){
					//check if result was good.
					if (result.IsSuccessStatusCode 
						&& result.StatusCode == System.Net.HttpStatusCode.OK) {
						//ok to process
						var json = result.Content.ReadAsStringAsync ().Result;
						//assign payload to json value.
						status.Payload = JsonConvert.DeserializeObject<K> (json);
					}
				}

			}
			catch(AggregateException){
				status.Message = "Multiple errors occurred.";
			}
			catch(TimeoutException){
				status.Message = "Could not connect to the server.";
			}
			catch(Exception){
				status.Message = "An error occurred.";
			}
			finally {
				if (client != null)
					client.Dispose ();
			}

			return status;
		}

		public static Task<AlarmResult> FetchStatus(){
			return SendData<AlarmResult, Alarm> (StatusEndpoint, HttpMethod.Get);
		}

		public static Task<AlarmResult> DeleteAlarm(Guid toCancel){
			var deleteEndpoint = string.Format (CancelAlarmEndpoint, toCancel.ToString ());
			return SendData<AlarmResult, Alarm> (deleteEndpoint, HttpMethod.Delete);
		}

		public static Task<AlarmResult> StopAlarm(){
			return SendData<AlarmResult, Alarm> (StopEndpoint, HttpMethod.Put);
		}

		public static Task<AlarmResult> CreateAlarm(Alarm toSchedule){
			return SendData<AlarmResult, Alarm> (ScheduleAlarmEndpoint, HttpMethod.Post,
				toSchedule.ToFormData ());
		}

		public static Task<StationListResult> Search(string toSearch){
			var data = new List<KeyValuePair<string, string>> {
				new KeyValuePair<string, string>("search", toSearch)
			};
			return SendData<StationListResult, List<StationInfo>> (SearchEndpoint, HttpMethod.Post, data); 
		}

		/// <summary>
		/// Snoozes the alarm for 5 minutes.
		/// </summary>
		public static Task<AlarmResult> Snooze() {
			return SendData<AlarmResult, Alarm> (SnoozeEndpoint, HttpMethod.Put);
		}

		public static Task<AlarmResult> Play(int stationId){
			var data = new List<KeyValuePair<string, string>> {
				new KeyValuePair<string, string>("stationId", stationId.ToString())
			};
			return SendData<AlarmResult, Alarm> (PlayEndpoint, HttpMethod.Post, data);
		}

	}
}

