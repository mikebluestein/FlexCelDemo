using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace FlexCellDemo
{
	public partial class SpreadsheetPreview : UIViewController
	{
		public string Path { get; set; }

		public SpreadsheetPreview () : base ("SpreadsheetPreview", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var url = new NSUrl (Path);
			var req = new NSUrlRequest (url);

			web.LoadRequest (req);
		}

		partial void Close (NSObject sender)
		{
			DismissViewController (true, null);
		}
	}
}