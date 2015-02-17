﻿using System;
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
	public class ApiRepo
	{
		private const string ApiUrl = "http://127.0.0.1:3000/";
		private IDeviceProvider deviceInfo;

		private const string GetStationUrl = "station";
		private const string PlayStation = "play";
		private const string SearchStationUrl = "search";
		private const string StopPlay = "stop";
		private const string StatusEndpoint = "status";
		private const string ScheduleAlarmEndpoint = "schedule";
		private const string CancelAlarmEndpoint = "schedule/cancel/{0}";

		public ApiRepo ()
		{
			deviceInfo = DependencyService.Get<IDeviceProvider> ();
		}

		public async Task<ApiResult> Status(){
			using (var client = new HttpClient ()) {
				client.BaseAddress = new Uri (ApiUrl);
				var result = await client.GetAsync(StatusEndpoint);
				if(result.IsSuccessStatusCode && result.StatusCode == System.Net.HttpStatusCode.OK){
					//ok to process
					var json = await result.Content.ReadAsStringAsync();
					return JsonConvert.DeserializeObject<ApiResult> (json);
				}
			}
			return null;
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

				var result = await client.PostAsync(ScheduleAlarmEndpoint,
					new FormUrlEncodedContent(toSchedule.ToFormData()));

				if(result.IsSuccessStatusCode && result.StatusCode == System.Net.HttpStatusCode.OK){
					//ok to process
					var json = await result.Content.ReadAsStringAsync();
					var respData = JsonConvert.DeserializeObject<AlarmResult> (json);
					//set our object id to that which the server assigned.
					toSchedule.Id = respData.Id;

					return toSchedule;
				}
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

		public async Task<ApiResult> Stop(){
			using (var client = new HttpClient ()) {
				client.BaseAddress = new Uri (ApiUrl);
				var data = new List<KeyValuePair<string, string>> {
					new KeyValuePair<string, string>("deviceId", deviceInfo.DeviceId),
				};

				var result = await client.PostAsync(StopPlay, new FormUrlEncodedContent(data));
				if(result.IsSuccessStatusCode && result.StatusCode == System.Net.HttpStatusCode.OK){
					//ok to process
					var json = await result.Content.ReadAsStringAsync();
					return JsonConvert.DeserializeObject<ApiResult> (json);
				}
			}
			return null;
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
