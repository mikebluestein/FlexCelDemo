using System;
using System.Drawing;
using System.IO;
using System.Linq;
using BigTed;
using FlexCel.Core;
using FlexCel.XlsAdapter;
using MonoTouch.Foundation;
using MonoTouch.MessageUI;
using MonoTouch.UIKit;

namespace FlexCellDemo
{
	public partial class FlexCellDemoViewController : UIViewController
	{
		string path;

		public FlexCellDemoViewController () : base ("FlexCellDemoViewController", null)
		{
			path = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "locations.xlsx");
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			locationSwitch.ValueChanged += delegate {
				if (locationSwitch.On) {
					LocationHelper.Instance.StartLocationUpdates ();
					BTProgressHUD.Show ("Tracking location");
				} else {
					LocationHelper.Instance.StopLocationUpdates ();
					BTProgressHUD.Dismiss ();

					CreateSpreadsheet ();
				}
			};

			emailButton.TouchUpInside += (sender, e) => {
				SendMail ();
			};

			previewButton.TouchUpInside += (sender, e) => {
				ShowPreview ();
			};
		}

		void CreateSpreadsheet ()
		{
			var locations = LocationHelper.Instance.Locations;

			var xls = new XlsFile (1, true);
			xls.SetCellValue (1, 1, "Location data from Xamarin.iOS");
			xls.SetCellValue (2, 1, "Latitude");
			xls.SetCellValue (2, 2, "Longitude");
			xls.SetCellValue (2, 3, "Altitude");
			int row = 2;

			xls.MergeCells(1, 1, 1, 3);

			SetTitleCellFormat (xls);

			locations.ForEach (l => {
				xls.SetCellValue (++row, 1, l.Coordinate.Latitude);
				xls.SetCellValue (row, 2, l.Coordinate.Longitude);
				xls.SetCellValue (row, 3, l.Altitude);
			});

			xls.Save (path);
		}

		void SetTitleCellFormat (XlsFile xls)
		{
			TFlxFormat cellFormat = xls.GetCellVisibleFormatDef (1, 1);
			cellFormat.Font.Size20 = 240;
			cellFormat.Font.Color = TExcelColor.FromTheme (TThemeColor.Accent1);
			cellFormat.Font.Style = TFlxFontStyles.Bold;
			cellFormat.FillPattern.Pattern = TFlxPatternStyle.Solid;
			cellFormat.FillPattern.FgColor = TExcelColor.FromTheme(TThemeColor.Accent3, 0.6);
			cellFormat.VAlignment = TVFlxAlignment.center;
			cellFormat.HAlignment = THFlxAlignment.center;

			xls.SetCellFormat (1, 1, xls.AddFormat (cellFormat));
		}

		void ShowPreview ()
		{
			var preview = new SpreadsheetPreview { Path = path };
			PresentViewController (preview, true, null);
		}

		void SendMail ()
		{
			if (MFMailComposeViewController.CanSendMail) {
				var mail = new MFMailComposeViewController ();
				mail.SetSubject ("Location data spreadsheet");
				mail.SetMessageBody ("Spreadsheet with location data attached.", false);
				mail.AddAttachmentData (NSData.FromFile (path), "application/vnd.ms-excel ", "locations.xlsx");
				mail.Finished += (object sender, MFComposeResultEventArgs e) => {
					e.Controller.DismissViewController (true, null);
				};

				PresentViewController (mail, true, null);
			}
		}
	}
}