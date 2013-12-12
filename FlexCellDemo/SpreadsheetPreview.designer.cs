// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace FlexCellDemo
{
	[Register ("SpreadsheetPreview")]
	partial class SpreadsheetPreview
	{
		[Outlet]
		MonoTouch.UIKit.UIWebView web { get; set; }

		[Action ("Close:")]
		partial void Close (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (web != null) {
				web.Dispose ();
				web = null;
			}
		}
	}
}
