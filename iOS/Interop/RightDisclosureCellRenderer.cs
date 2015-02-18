using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using InterClock.Connect.Data.Controls;
using Interop;
using MonoTouch.UIKit;

[assembly: Xamarin.Forms.ExportRenderer(typeof(RightDisclosureCell), typeof(RightDisclosureCellRenderer))]
namespace Interop
{
	public class RightDisclosureCellRenderer : TextCellRenderer
	{
		public override UITableViewCell GetCell (Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var cell = base.GetCell(item, reusableCell, tv);

			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			return cell;
		}


		public RightDisclosureCellRenderer ()
		{

		}
	}
}

