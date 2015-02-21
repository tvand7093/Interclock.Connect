using System;
using InterClock.Connect.Data.Interfaces;
using MonoTouch.Foundation;
using Interop;

[assembly: Xamarin.Forms.Dependency(typeof(Translator))]
namespace Interop
{
	public class Translator : ITranslator
	{
		public Translator ()
		{
		}

		public string Translate(string toTranslate){
			return NSBundle.MainBundle.LocalizedString (toTranslate, "translated");
		}
	}
}

