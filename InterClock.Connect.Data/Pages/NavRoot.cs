﻿using System;

using Xamarin.Forms;
using InterClock.Connect.Data.Interfaces;

namespace InterClock.Connect.Data.Pages
{
	public class NavRoot : NavigationPage
	{
	
		public NavRoot (Page content) : base(content)
		{
			Init ();
		}

		private void Init(){
			this.BindingContext = CurrentPage;
			this.SetBinding (NavigationPage.TitleProperty, "Title");

			//check initial content for IMessenger
			if(CurrentPage.BindingContext is IMessenger){
				(CurrentPage.BindingContext as IMessenger).Subscribe();
			}

			//subscribe stuff.
			this.Pushed += (object sender, NavigationEventArgs e) => {
				if(e.Page.BindingContext is IMessenger){
					(e.Page.BindingContext as IMessenger).Subscribe();
				}
			};

			this.Popped += (object sender, NavigationEventArgs e) => {
				if(e.Page.BindingContext is IMessenger){
					(e.Page.BindingContext as IMessenger).Unsubscribe();
				}
			};
		}
	}
}


