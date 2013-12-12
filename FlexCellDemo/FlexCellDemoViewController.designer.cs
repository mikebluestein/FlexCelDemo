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
	[Register ("FlexCellDemoViewController")]
	partial class FlexCellDemoViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton emailButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch locationSwitch { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton previewButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (locationSwitch != null) {
				locationSwitch.Dispose ();
				locationSwitch = null;
			}

			if (emailButton != null) {
				emailButton.Dispose ();
				emailButton = null;
			}

			if (previewButton != null) {
				previewButton.Dispose ();
				previewButton = null;
			}
		}
	}
}
