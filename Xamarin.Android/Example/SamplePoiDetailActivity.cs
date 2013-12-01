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
using System.Json;
using Android.Util;

namespace Com.Wikitude.Samples
{
	[Activity (Label = "SamplePoiDetailActivity")]			
	public class SamplePoiDetailActivity : Activity
	{
		public const string EXTRAS_KEY_POIDATA = "poiData";

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.sample_5_1_poidetail);

			try
			{
				var poiData = JsonArray.Parse (Intent.Extras.GetString (EXTRAS_KEY_POIDATA));

				FindViewById<TextView> (Resource.Id.poi_title).Text = poiData ["name"].ToString ();
				FindViewById<TextView> (Resource.Id.poi_description).Text = poiData ["description"].ToString ();
				FindViewById<TextView> (Resource.Id.poi_latitude).Text = poiData ["latitude"].ToString ();
				FindViewById<TextView> (Resource.Id.poi_longitude).Text = poiData ["longitude"].ToString ();
			}
			catch (Exception ex)
			{
				Log.Error (Constants.LOG_TAG, ex.ToString());
				Toast.MakeText (this, "Unexepcted error: " + ex.Message, ToastLength.Short).Show ();
			}
		}
	}
}

