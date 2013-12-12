using System;
using System.Collections.Generic;
using MonoTouch.CoreLocation;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace FlexCellDemo
{

    public sealed class LocationHelper
    {
		static readonly LocationHelper locationHelperInstance = new LocationHelper ();

        public static LocationHelper Instance {
            get { return locationHelperInstance; }
        }

		public List<CLLocation> Locations { get; set; }  

		CLLocationManager manager;

        LocationHelper ()
        {
			Locations = new List<CLLocation> ();
            
            manager = new CLLocationManager ();
            manager.DesiredAccuracy = CLLocation.AccuracyBest;
            manager.DistanceFilter = CLLocation.AccuracyBest;        
            manager.Delegate = new LocationDelegate (this);
        }

        public void StartLocationUpdates ()
        {
            if (CLLocationManager.LocationServicesEnabled)
                manager.StartUpdatingLocation ();
            else {
				var alert = new UIAlertView(
                    "Cannot determine location", 
                    "Location services are disabled",
                    null, "OK");
                
                alert.Show();
            }
        }

        public void StopLocationUpdates ()
        {
            manager.StopUpdatingLocation ();
        }

        class LocationDelegate : CLLocationManagerDelegate
        {
			LocationHelper h;

			public LocationDelegate (LocationHelper helper)
            {
				h = helper;
            }

			public override void LocationsUpdated (CLLocationManager manager, CLLocation[] locations)
			{
				h.Locations.AddRange (locations);
			}

            public override void Failed (CLLocationManager manager, NSError error)
            {
                if (error.Code == (int)CLError.Denied) {
                    Console.WriteLine ("Access to location services denied");
                    
                    manager.StopUpdatingLocation ();
                    manager.Delegate = null;
                }
            }
        }
    }
}

