using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Locations;

namespace Com.Wikitude.Samples
{
	public class LocationProvider : ILocationProvider
	{
		ILocationListener locationListener;
		LocationManager locationManager;

		/** location updates should fire approximately every second */
		const int LOCATION_UPDATE_MIN_TIME_GPS = 1000;

		/** location updates should fire, even if last signal is same than current one (0m distance to last location is OK) */
		const int LOCATION_UPDATE_DISTANCE_GPS = 0;

		/** location updates should fire approximately every second */
		const int LOCATION_UPDATE_MIN_TIME_NW = 1000;

		/** location updates should fire, even if last signal is same than current one (0m distance to last location is OK) */
		const int LOCATION_UPDATE_DISTANCE_NW = 0;

		/** to faster access location, even use 10 minute old locations on start-up */
		const int LOCATION_OUTDATED_WHEN_OLDER_MS = 1000 * 60 * 10;

		/** is gpsProvider and networkProvider enabled in system settings */
		bool gpsProviderEnabled;
		bool networkProviderEnabled;

		/** the context in which we're running */
		Context context;

		public LocationProvider(Context context, ILocationListener locListener)
		{
			this.context = context;
			this.locationManager = LocationManager.FromContext (context);
			this.locationListener = locListener;
			this.gpsProviderEnabled = this.locationManager.IsProviderEnabled (LocationManager.GpsProvider);
			this.networkProviderEnabled = this.locationManager.IsProviderEnabled (LocationManager.NetworkProvider);
		}

		#region ILocationProvider implementation
		public void OnResume ()
		{
			if (this.locationManager != null && this.locationListener != null)
			{
				this.gpsProviderEnabled = this.locationManager.IsProviderEnabled (LocationManager.GpsProvider);
				this.networkProviderEnabled = this.locationManager.IsProviderEnabled (LocationManager.NetworkProvider);

				var currentTimeMillis = (DateTime.Now - new DateTime (1970, 1, 1)).TotalMilliseconds;

				if (gpsProviderEnabled)
				{
					var lastKnownGpsLocation = locationManager.GetLastKnownLocation (LocationManager.GpsProvider);

					if (lastKnownGpsLocation != null && lastKnownGpsLocation.Time > currentTimeMillis - LOCATION_OUTDATED_WHEN_OLDER_MS)
						locationListener.OnLocationChanged (lastKnownGpsLocation);

					if (locationManager.GetProvider (LocationManager.GpsProvider) != null)
						locationManager.RequestLocationUpdates (LocationManager.GpsProvider,
                           LOCATION_UPDATE_MIN_TIME_GPS, LOCATION_UPDATE_DISTANCE_GPS, this.locationListener);
				}

				if (networkProviderEnabled)
				{
					var lastKnownNWLocation = this.locationManager.GetLastKnownLocation (LocationManager.NetworkProvider);

					if (lastKnownNWLocation != null && lastKnownNWLocation.Time > currentTimeMillis - LOCATION_OUTDATED_WHEN_OLDER_MS)
						locationListener.OnLocationChanged (lastKnownNWLocation);

					if (locationManager.GetProvider (LocationManager.NetworkProvider) != null)
						locationManager.RequestLocationUpdates (LocationManager.NetworkProvider,
                           LOCATION_UPDATE_MIN_TIME_NW, LOCATION_UPDATE_DISTANCE_NW, this.locationListener);
				}

				if (!gpsProviderEnabled && !networkProviderEnabled)
					Toast.MakeText (context, "Please enable GPS and Network Positioning in your Settings", ToastLength.Short).Show ();
			}
		}

		public void OnPause ()
		{
			if (locationListener != null && locationManager != null && (gpsProviderEnabled || networkProviderEnabled))
				locationManager.RemoveUpdates (this.locationListener);
		}
		#endregion
	}
}

