using System;
using System.Collections;

namespace InterClock.Connect.Data.Models
{
	public static class Extensions
	{
		public static void AddRange(this IList list, IEnumerable data){
			foreach (var item in data) {
				list.Add (item);
			}
		}
	}
}

