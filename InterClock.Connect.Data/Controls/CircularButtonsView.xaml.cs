using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Windows.Input;

namespace InterClock.Connect.Data.Controls
{
	public partial class CircularButtonsView : ContentView
	{
		public static readonly BindableProperty LeftEnabledProperty = 
			BindableProperty.Create<CircularButtonsView, bool>(
				getter: cv => cv.LeftEnabled,
				defaultValue: true,
				propertyChanging: (bindable, oldValue, newValue) => {
					var cv = (CircularButtonsView)bindable;
					cv.LeftEnabled = newValue;
				}
			);

		public bool LeftEnabled {
			get { return (bool)GetValue (LeftEnabledProperty); }
			set { 
				SetValue (LeftEnabledProperty, value);
				LeftButton.IsEnabled = value;
				if (value == false) {
					LeftButton.TextColor = LeftButton.BorderColor = DisabledColor;
				} else {
					LeftButton.TextColor = LeftButton.BorderColor = LeftColor;
				}
			}
		}

		public static readonly BindableProperty RightEnabledProperty = 
			BindableProperty.Create<CircularButtonsView, bool>(
				getter: cv => cv.RightEnabled,
				defaultValue: true,
				propertyChanging: (bindable, oldValue, newValue) => {
					var cv = (CircularButtonsView)bindable;
					cv.RightEnabled = newValue;
				}
			);

		public bool RightEnabled {
			get { return (bool)GetValue (RightEnabledProperty); }
			set { 
				SetValue (RightEnabledProperty, value); 
				RightButton.IsEnabled = value;
				if (value == false) {
					RightButton.TextColor = RightButton.BorderColor = DisabledColor;
				} else {
					RightButton.TextColor = RightButton.BorderColor = RightColor;
				}
			}
		}

		public static readonly BindableProperty CenterEnabledProperty = 
			BindableProperty.Create<CircularButtonsView, bool>(
				getter: cv => cv.CenterEnabled,
				defaultValue: true,
				propertyChanging: (bindable, oldValue, newValue) => {
					var cv = (CircularButtonsView)bindable;
					cv.CenterEnabled = newValue;
				}
			);

		public bool CenterEnabled {
			get { return (bool)GetValue (CenterEnabledProperty); }
			set { 
				SetValue (CenterEnabledProperty, value);
				CenterButton.IsEnabled = value;
				if (value == false) {
					CenterButton.TextColor = CenterButton.BorderColor = DisabledColor;
				} else {
					CenterButton.TextColor = CenterButton.BorderColor = CenterColor;
				}
			}
		}

		public static readonly BindableProperty CenterTextProperty = 
			BindableProperty.Create<CircularButtonsView, string>(
				getter: cv => cv.CenterText,
				defaultValue: string.Empty,
				propertyChanging: (bindable, oldValue, newValue) => {
					var cv = (CircularButtonsView)bindable;
					cv.CenterText = newValue;
				}
			);

		public string CenterText {
			get { return (string)GetValue (CenterTextProperty); }
			set {
				SetValue (CenterTextProperty, value);
				CenterButton.Text = value;
			}
		}

		public static readonly BindableProperty LeftTextProperty = 
			BindableProperty.Create<CircularButtonsView, string>(
				getter: cv => cv.LeftText,
				defaultValue: string.Empty,
				propertyChanging: (bindable, oldValue, newValue) => {
					var cv = (CircularButtonsView)bindable;
					cv.LeftText = newValue;
				}
			);

		public string LeftText {
			get { return (string)GetValue (LeftTextProperty); }
			set { 
				SetValue (LeftTextProperty, value);
				LeftButton.Text = value;
			}
		}

		public static readonly BindableProperty RightTextProperty = 
			BindableProperty.Create<CircularButtonsView, string>(
				getter: cv => cv.RightText,
				defaultValue: string.Empty,
				propertyChanging: (bindable, oldValue, newValue) => {
					var cv = (CircularButtonsView)bindable;
					cv.RightText = newValue;
				}
			);

		public string RightText {
			get { return (string)GetValue (RightTextProperty); }
			set { 
				SetValue (RightTextProperty, value);
				RightButton.Text = value;
			}
		}

		public static readonly BindableProperty DisabledColorProperty = 
			BindableProperty.Create<CircularButtonsView, Color>(
				getter: cv => cv.DisabledColor,
				defaultValue: Color.Gray,
				propertyChanging: (bindable, oldValue, newValue) => {
					var cv = (CircularButtonsView)bindable;
					cv.DisabledColor = newValue;
				}
			);

		public Color DisabledColor {
			get { return (Color)GetValue (DisabledColorProperty); }
			set { 
				SetValue (DisabledColorProperty, value);
			}
		}

		public static readonly BindableProperty RightBorderColorProperty = 
			BindableProperty.Create<CircularButtonsView, Color>(
				getter: cv => cv.RightBorderColor,
				defaultValue: Color.Blue,
				propertyChanging: (bindable, oldValue, newValue) => {
					var cv = (CircularButtonsView)bindable;
					cv.RightBorderColor = newValue;
				}
			);

		public Color RightBorderColor {
			get { return (Color)GetValue (RightBorderColorProperty); }
			set { 
				SetValue (RightBorderColorProperty, value);
				RightButton.BorderColor = value;
			}
		}

		public static readonly BindableProperty LeftBorderColorProperty = 
			BindableProperty.Create<CircularButtonsView, Color>(
				getter: cv => cv.LeftBorderColor,
				defaultValue: Color.Blue,
				propertyChanging: (bindable, oldValue, newValue) => {
					var cv = (CircularButtonsView)bindable;
					cv.LeftBorderColor = newValue;
				}
			);

		public Color LeftBorderColor {
			get { return (Color)GetValue (LeftBorderColorProperty); }
			set { 
				SetValue (LeftBorderColorProperty, value);
				LeftButton.BorderColor = value;
			}
		}

		public static readonly BindableProperty CenterBorderColorProperty = 
			BindableProperty.Create<CircularButtonsView, Color>(
				getter: cv => cv.CenterBorderColor,
				defaultValue: Color.Blue,
				propertyChanging: (bindable, oldValue, newValue) => {
					var cv = (CircularButtonsView)bindable;
					cv.CenterBorderColor = newValue;
				}
			);

		public Color CenterBorderColor {
			get { return (Color)GetValue (CenterBorderColorProperty); }
			set { 
				SetValue (CenterBorderColorProperty, value);
				CenterButton.BorderColor = value;
			}
		}

		public static readonly BindableProperty RightTextColorProperty = 
			BindableProperty.Create<CircularButtonsView, Color>(
				getter: cv => cv.RightTextColor,
				defaultValue: Color.Blue,
				propertyChanging: (bindable, oldValue, newValue) => {
					var cv = (CircularButtonsView)bindable;
					cv.RightTextColor = newValue;
				}
			);

		public Color RightTextColor {
			get { return (Color)GetValue (RightTextColorProperty); }
			set { 
				SetValue (RightTextColorProperty, value);
				RightButton.TextColor = value;
			}
		}

		public static readonly BindableProperty LeftTextColorProperty = 
			BindableProperty.Create<CircularButtonsView, Color>(
				getter: cv => cv.LeftTextColor,
				defaultValue: Color.Blue,
				propertyChanging: (bindable, oldValue, newValue) => {
					var cv = (CircularButtonsView)bindable;
					cv.LeftTextColor = newValue;
				}
			);

		public Color LeftTextColor {
			get { return (Color)GetValue (LeftTextColorProperty); }
			set { 
				SetValue (LeftTextColorProperty, value);

				LeftButton.TextColor = value;
			}
		}

		public static readonly BindableProperty CenterTextColorProperty = 
			BindableProperty.Create<CircularButtonsView, Color>(
				getter: cv => cv.CenterTextColor,
				defaultValue: Color.Blue,
				defaultBindingMode: BindingMode.TwoWay,
				propertyChanging: (bindable, oldValue, newValue) => {
					var cv = (CircularButtonsView)bindable;
					cv.CenterTextColor = newValue;
				}
			);

		public Color CenterTextColor {
			get { return (Color)GetValue (CenterTextColorProperty); }
			set { 
				SetValue (CenterTextColorProperty, value);
				CenterButton.TextColor = value;
			}
		}

		public static readonly BindableProperty CenterColorProperty = 
			BindableProperty.Create<CircularButtonsView, Color>(
				getter: cv => cv.CenterColor,
				defaultValue: Color.Blue,
				defaultBindingMode: BindingMode.TwoWay,
				propertyChanging: (bindable, oldValue, newValue) => {
					var cv = (CircularButtonsView)bindable;
					cv.CenterColor = newValue;
				}
			);

		public Color CenterColor {
			get { return (Color)GetValue (CenterColorProperty); }
			set { 
				SetValue (CenterColorProperty, value);
				CenterBorderColor = CenterTextColor = value;
			}
		}

		public static readonly BindableProperty LeftColorProperty = 
			BindableProperty.Create<CircularButtonsView, Color>(
				getter: cv => cv.LeftColor,
				defaultValue: Color.Blue,
				propertyChanging: (bindable, oldValue, newValue) => {
					var cv = (CircularButtonsView)bindable;
					cv.LeftColor = newValue;
				}
			);

		public Color LeftColor {
			get { return (Color)GetValue (LeftColorProperty); }
			set { 
				SetValue (LeftColorProperty, value);
				LeftTextColor = LeftBorderColor = value;
			}
		}

		public static readonly BindableProperty RightColorProperty = 
			BindableProperty.Create<CircularButtonsView, Color>(
				getter: cv => cv.RightColor,
				defaultValue: Color.Blue,
				propertyChanging: (bindable, oldValue, newValue) => {
					var cv = (CircularButtonsView)bindable;
					cv.RightColor = newValue;
				}
			);

		public Color RightColor {
			get { return (Color)GetValue (RightColorProperty); }
			set { 
				SetValue (RightColorProperty, value);
				RightTextColor = RightBorderColor = value;
			}
		}

		public static readonly BindableProperty RightBackgroundProperty = 
			BindableProperty.Create<CircularButtonsView, Color>(
				getter: cv => cv.RightBackgroundColor,
				defaultValue: Color.White,
				propertyChanging: (bindable, oldValue, newValue) => {
					var cv = (CircularButtonsView)bindable;
					cv.RightBackgroundColor = newValue;
				}
			);

		public Color RightBackgroundColor {
			get { return (Color)GetValue (RightBackgroundProperty); }
			set { 
				SetValue (RightBackgroundProperty, value);
				RightButton.BackgroundColor = value;
			}
		}

		public static readonly BindableProperty LeftBackgroundProperty = 
			BindableProperty.Create<CircularButtonsView, Color>(
				getter: cv => cv.LeftBackgroundColor,
				defaultValue: Color.White,
				propertyChanging: (bindable, oldValue, newValue) => {
					var cv = (CircularButtonsView)bindable;
					cv.LeftBackgroundColor = newValue;
				}
			);

		public Color LeftBackgroundColor {
			get { return (Color)GetValue (LeftBackgroundProperty); }
			set { 
				SetValue (LeftBackgroundProperty, value);
				LeftButton.BackgroundColor = value;
			}
		}

		public static readonly BindableProperty CenterBackgroundProperty = 
			BindableProperty.Create<CircularButtonsView, Color>(
				getter: cv => cv.CenterBackgroundColor,
				defaultValue: Color.White,
				propertyChanging: (bindable, oldValue, newValue) => {
					var cv = (CircularButtonsView)bindable;
					cv.CenterBackgroundColor = newValue;
				}
			);

		public Color CenterBackgroundColor {
			get { return (Color)GetValue (CenterBackgroundProperty); }
			set { 
				SetValue (CenterBackgroundProperty, value);
				CenterButton.BackgroundColor = value;
			}
		}

		public static readonly BindableProperty CenterCommandProperty = 
			BindableProperty.Create<CircularButtonsView, ICommand>(
				getter: cv => cv.CenterCommand,
				defaultValue: null,
				propertyChanging: (bindable, oldValue, newValue) => {
					var cv = (CircularButtonsView)bindable;
					cv.CenterCommand = newValue;
				}
			);

		public ICommand CenterCommand {
			get { return (ICommand)GetValue (CenterCommandProperty); }
			set { 
				SetValue (CenterCommandProperty, value);
				CenterButton.Command = value;
			}
		}

		public static readonly BindableProperty LeftCommandProperty = 
			BindableProperty.Create<CircularButtonsView, ICommand>(
				getter: cv => cv.LeftCommand,
				defaultValue: null,
				propertyChanging: (bindable, oldValue, newValue) => {
					var cv = (CircularButtonsView)bindable;
					cv.LeftCommand = newValue;
				}
			);

		public ICommand LeftCommand {
			get { return (ICommand)GetValue (LeftCommandProperty); }
			set { 
				SetValue (LeftCommandProperty, value);
				LeftButton.Command = value;
			}
		}

		public static readonly BindableProperty RightCommandProperty = 
			BindableProperty.Create<CircularButtonsView, ICommand>(
				getter: cv => cv.RightCommand,
				defaultValue: null,
				propertyChanging: (bindable, oldValue, newValue) => {
					var cv = (CircularButtonsView)bindable;
					cv.RightCommand = newValue;
				}
			);

		public ICommand RightCommand {
			get { return (ICommand)GetValue (RightCommandProperty); }
			set { 
				SetValue (RightCommandProperty, value);
				RightButton.Command = value;
			}
		}

		public static readonly BindableProperty RightCommandParamProperty = 
			BindableProperty.Create<CircularButtonsView, object>(
				getter: cv => cv.RightCommandParam,
				defaultValue: null,
				propertyChanging: (bindable, oldValue, newValue) => {
					var cv = (CircularButtonsView)bindable;
					cv.RightCommandParam = newValue;
				}
			);

		public object RightCommandParam {
			get { return GetValue (RightCommandParamProperty); }
			set { 
				SetValue (RightCommandParamProperty, value);
				RightButton.CommandParameter = value;
			}
		}

		public static readonly BindableProperty LeftCommandParamProperty = 
			BindableProperty.Create<CircularButtonsView, object>(
				getter: cv => cv.LeftCommandParam,
				defaultValue: null,
				propertyChanging: (bindable, oldValue, newValue) => {
					var cv = (CircularButtonsView)bindable;
					cv.LeftCommandParam = newValue;
				}
			);

		public object LeftCommandParam {
			get { return GetValue (LeftCommandParamProperty); }
			set { 
				SetValue (LeftCommandParamProperty, value);
				LeftButton.CommandParameter = value;
			}
		}

		public static readonly BindableProperty CenterCommandParamProperty = 
			BindableProperty.Create<CircularButtonsView, object>(
				getter: cv => cv.CenterCommandParam,
				defaultValue: null,
				propertyChanging: (bindable, oldValue, newValue) => {
					var cv = (CircularButtonsView)bindable;
					cv.CenterCommandParam = newValue;
				}
			);

		public object CenterCommandParam {
			get { return GetValue (CenterCommandParamProperty); }
			set { 
				SetValue (CenterCommandParamProperty, value);
				CenterButton.CommandParameter = value;
			}
		}

		public CircularButtonsView ()
		{
			InitializeComponent ();
		}
	}
}

