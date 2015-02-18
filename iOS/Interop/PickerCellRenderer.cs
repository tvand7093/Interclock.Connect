using System;
using Xamarin.Forms.Platform.iOS;
using InterClock.Connect.Data.Controls;
using Interop;

[assembly: Xamarin.Forms.ExportRenderer(typeof(PickerCell), typeof(PickerCellRenderer))]
namespace Interop
{
	public class PickerCellRenderer : TimePickerRenderer
	{
	
		public override void Draw (System.Drawing.RectangleF rect)
		{
			base.Layer.CornerRadius = 0;
			base.Layer.BorderWidth = 0;
			base.Draw (rect);

		}

		public override void DrawRect (System.Drawing.RectangleF area, MonoTouch.UIKit.UIViewPrintFormatter formatter)
		{

			base.DrawRect (area, formatter);

		}

		public PickerCellRenderer ()
		{
			base.Layer.CornerRadius = 0;
			base.Layer.BorderWidth = 0;
		}
	}
}

