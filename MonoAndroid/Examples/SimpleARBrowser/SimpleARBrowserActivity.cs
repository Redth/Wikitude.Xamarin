using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Views;
using Android.Widget;
using Com.Wikitude.Architect;
using System.Collections.Specialized;

namespace Wikitude.SDK.MonoAndroid
{
	[Activity(Label = "Simple AR Browser", MainLauncher = true)]
	public class SimpleARBrowserActivity : Activity, ILocationListener
	{
		const string WIKITUDE_LICENSE_KEY = "";
		static float TEST_LATITUDE = 47.77318f;
		static float TEST_LONGITUDE = 13.069730f;
		static float TEST_ALTITUDE = 150;

		ArchitectView arView;
		
		UrlInvokedListener urlListener;
		LocationManager locationManager;
		Location lastLocation = null;
		
		void LoadWorld()
		{
			arView.Load("tutorial1.html");
			
			var pois = new List<string>();

			for (int i = 0; i < 50; i++)
			{
				var loc = CreateRandomLocation();
				var title = "POI #" + i;
				var desc = "Probably one of the best POIs you have ever seen. This is the description of Poi #" + i;
				var point = "{" + string.Format("\"latitude\":{0},\"longitude\":{1},\"altitude\":{2}", loc[0], loc[1], loc[2]) + "}";

				Console.WriteLine("POINT: " + point);

				var json = "{" + string.Format("\"id\":\"{0}\",\"name\":\"{1}\",\"description\":\"{2}\",\"type\":{3},\"Point\":{4}", i, title, desc, rnd.Next(1, 3), point) + "}";

				pois.Add(json);
			}

			var datajson = "[" + string.Join(",", pois) + "]";

			this.arView.CallJavascript("newData('" + datajson + "');");
		}

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

			if (!ArchitectView.IsDeviceSupported(this))
			{
				Toast.MakeText(this, "Unsupported Device", ToastLength.Short).Show();
				this.Finish();
				return;
			}
			
			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.SimpleARBrowserLayout);

			urlListener = new UrlInvokedListener(url =>
				{
					var queryParams = System.Web.HttpUtility.ParseQueryString(url);

					var name = queryParams["name"];
					var desc = queryParams["desc"];

					var intent = new Intent(this, typeof (PoiDetailActivity));
					intent.PutExtra("POI_NAME", name);
					intent.PutExtra("POI_DESC", desc);

					StartActivity(intent);
				});

			arView = this.FindViewById<Com.Wikitude.Architect.ArchitectView>(Resource.Id.architectView);

			var config = new ArchitectView.ArchitectConfig(WIKITUDE_LICENSE_KEY);

			arView.OnCreate(config);

			//locationManager = (LocationManager)this.GetSystemService (LocationService);
			//locationManager.RequestLocationUpdates (LocationManager.GpsProvider, 0, 0, this);
		}
		
		protected override void OnPostCreate(Bundle savedInstanceState)
		{
			arView.OnPostCreate();

			base.OnPostCreate(savedInstanceState);

			

			arView.RegisterUrlListener(urlListener);

			arView.SetLocation(TEST_LATITUDE, TEST_LONGITUDE, TEST_ALTITUDE, 1f);
		}

		protected override void OnResume()
		{
			base.OnResume();

			if (arView != null)
			{
				arView.OnResume();

				//if (lastLocation != null)
				//arView.SetLocation (lastLocation.Latitude, lastLocation.Longitude, lastLocation.Accuracy);

				arView.SetLocation(TEST_LATITUDE, TEST_LONGITUDE, TEST_ALTITUDE, 1f);
			}
			
			LoadWorld();
		}

		protected override void OnPause()
		{
			base.OnPause();

			if (arView != null)
				arView.OnPause();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			if (arView != null)
				arView.OnDestroy();
		}

		public override void OnLowMemory()
		{
			base.OnLowMemory();

			if (arView != null)
				arView.OnLowMemory();
		}

		#region ILocationListener implementation
		public void OnLocationChanged(Location location)
		{
			//lastLocation = location;
			//arView.SetLocation (location.Latitude, location.Longitude, location.Accuracy);
		}

		public void OnProviderDisabled(string provider)
		{
		}

		public void OnProviderEnabled(string provider)
		{
		}

		public void OnStatusChanged(string provider, Availability status, Bundle extras)
		{
		}
		#endregion

		Random rnd = new Random();

		double[] CreateRandomLocation()
		{
			return new double[] { TEST_LATITUDE + ((rnd.NextDouble() - 0.5) / 500), TEST_LONGITUDE + ((rnd.NextDouble() - 0.5) / 500), TEST_ALTITUDE + ((rnd.NextDouble() - 0.5) * 10) };
		}
	}
}