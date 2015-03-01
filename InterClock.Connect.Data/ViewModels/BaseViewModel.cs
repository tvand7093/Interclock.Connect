using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using InterClock.Connect.Data.Interfaces;

namespace InterClock.Connect.Data.ViewModels
{
	public abstract class BaseViewModel : INotifyPropertyChanged
	{
		public bool IsBusy {
			get { return Application.Current.MainPage.IsBusy; }
			set {
				if(Application.Current.MainPage != null)
					Application.Current.MainPage.IsBusy = value; }
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this,
					new PropertyChangedEventArgs(propertyName));
			}
		}
	}

}

