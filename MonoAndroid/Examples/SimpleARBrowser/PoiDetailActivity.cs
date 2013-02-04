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

namespace Wikitude.SDK.MonoAndroid
{
	[Activity(Label = "POI Details")]
	public class PoiDetailActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Create your application here
			SetContentView(Resource.Layout.PoiDetailLayout);

			FindViewById<TextView>(Resource.Id.tvPoiName).Text = Intent.GetStringExtra("POI_NAME");
			FindViewById<TextView>(Resource.Id.tvPoiDesc).Text = Intent.GetStringExtra("POI_DESC");
		}
	}
}