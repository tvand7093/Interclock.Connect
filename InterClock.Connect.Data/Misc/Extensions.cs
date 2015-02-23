using System;
using System.Collections;
using InterClock.Connect.Data.Interfaces;
using Xamarin.Forms;

namespace InterClock.Connect.Data.Models
{
	public static class Extensions
	{
		static Extensions(){
			translator = DependencyService.Get<ITranslator> ();
		}

		public static void AddRange(this IList list, IEnumerable data, bool clearData = false){
			if (clearData)
				list.Clear ();

			foreach (var item in data) {
				list.Add (item);
			}
		}


		private static ITranslator translator;
		internal static string T(this string toTranslate){
			return translator.Translate (toTranslate);
		}
	}
}

