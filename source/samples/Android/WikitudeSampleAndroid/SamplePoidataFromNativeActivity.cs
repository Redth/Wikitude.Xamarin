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
using System.Threading.Tasks;
using Android.Util;
using System.Threading;
using System.Json;

namespace Com.Wikitude.Samples
{
	[Activity (Label = "SamplePoidataFromNativeActivity")]			
	public class SamplePoidataFromNativeActivity : BasicArchitectActivity
	{
		bool isLoading = false;
		protected JsonArray poiData;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
		}

		protected override void OnPostCreate (Bundle savedInstanceState)
		{
			base.OnPostCreate (savedInstanceState);

			LoadData ();
		}

		protected void LoadData()
		{
			if (isLoading)
				return;

			Task.Factory.StartNew (() => {

				isLoading = true;

				while (lastKnownLocation == null && !this.IsFinishing)
					Thread.Sleep(2000);

				if (this.lastKnownLocation != null && !this.IsFinishing)
				{
					poiData = GeoUtils.GetPoiInformation(lastKnownLocation, 20);

					var js = "World.loadPoisFromJsonData(" + poiData.ToString() + ");";

					architectView.CallJavascript(js);
				}

				isLoading = false;

			}).ContinueWith (t => {

				isLoading = false;

				var ex = t.Exception;
				Log.Error(Constants.LOG_TAG, ex.ToString());

			}, TaskContinuationOptions.OnlyOnFaulted);
		}
	}
}

