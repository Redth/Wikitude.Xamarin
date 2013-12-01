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
using Wikitude.Architect;
using Android.Locations;
using Android.Hardware;
using Android.Util;

namespace Com.Wikitude.Samples
{
	[Activity (Label = "BaseArchitectActivity")]			
	public class BasicArchitectActivity : Activity, ArchitectView.ISensorAccuracyChangeListener, ILocationListener
	{
		public const string EXTRAS_KEY_ACTIVITY_TITLE_STRING = "activityTitle";
		public const string EXTRAS_KEY_ACTIVITY_ARCHITECT_WORLD_URL = "activityArchitectWorldUrl";

		protected ArchitectView architectView;

		protected Location lastKnownLocation;

		protected ILocationProvider locationProvider;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			this.VolumeControlStream = Android.Media.Stream.Music;

			SetContentView(Resource.Layout.sample_cam);

			var title = "Test World";

			if (Intent.Extras != null && Intent.Extras.Get (EXTRAS_KEY_ACTIVITY_TITLE_STRING) != null)
				title = Intent.Extras.GetString (EXTRAS_KEY_ACTIVITY_TITLE_STRING);
				
			Title = title;

			architectView = FindViewById<ArchitectView>(Resource.Id.architectView);

			//TODO: SDK KEY
			var config = new ArchitectView.ArchitectConfig (Constants.WIKITUDE_SDK_KEY);

			architectView.OnCreate (config);

			this.architectView.RegisterSensorAccuracyChangeListener (this);

			this.locationProvider = new LocationProvider (this, this);
		}

		#region ISensorAccuracyChangeListener implementation
		public void OnCompassAccuracyChanged (int accuracy)
		{
			/* UNRELIABLE = 0, LOW = 1, MEDIUM = 2, Height = 3 */
			if (accuracy < 2 && !this.IsFinishing) 
				Toast.MakeText(this, Resource.String.compass_accuracy_low, ToastLength.Long).Show();
		}
		#endregion

		#region ILocationListener implementation

		public void OnLocationChanged (Location location)
		{
			if (location != null)
				lastKnownLocation = location;

			if (location.HasAltitude)
				architectView.SetLocation (location.Latitude, location.Longitude, location.Altitude, location.HasAccuracy ? location.Accuracy : 1000);
			else
				architectView.SetLocation(location.Latitude, location.Longitude, location.HasAccuracy ? location.Accuracy : 1000);
		}

		public void OnProviderDisabled (string provider)
		{
		}

		public void OnProviderEnabled (string provider)
		{
		}

		public void OnStatusChanged (string provider, Availability status, Bundle extras)
		{
		}
		#endregion

		protected override void OnResume ()
		{
			base.OnResume ();

			if (architectView != null)
				architectView.OnResume ();

			if (locationProvider != null)
				locationProvider.OnResume ();
		}

		protected override void OnPause ()
		{
			base.OnPause ();

			if (architectView != null)
				architectView.OnPause ();

			if (locationProvider != null)
				locationProvider.OnPause ();
		}

		protected override void OnStop ()
		{
			base.OnStop ();
		}

		protected override void OnDestroy ()
		{
			base.OnDestroy ();

			if (architectView != null)
			{
				architectView.UnregisterSensorAccuracyChangeListener (this);

				architectView.OnDestroy ();
			}
		}

		public override void OnLowMemory ()
		{
			base.OnLowMemory ();

			if (architectView != null)
				architectView.OnLowMemory ();
		}

		protected override void OnPostCreate (Bundle savedInstanceState)
		{
			base.OnPostCreate (savedInstanceState);

			if (architectView != null)
				architectView.OnPostCreate ();

			try
			{
				var world = Intent.Extras.GetString(EXTRAS_KEY_ACTIVITY_ARCHITECT_WORLD_URL);

				architectView.Load(world);
			}
			catch (Exception ex)
			{
				Log.Error ("WIKITUDE_SAMPLE", ex.ToString ());
			}
		}
	}
}

