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

namespace Com.Wikitude.Samples
{
	[Activity (Label = "ARchitectUrlLauncherCamActivity")]			
	public class ARchitectUrlLauncherCamActivity : BasicArchitectActivity
	{
		public const string ARCHITECT_ACTIVITY_EXTRA_KEY_URL = "url2launch";

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			var decodedUrl = Java.Net.URLDecoder.Decode (Intent.Extras.GetString (ARCHITECT_ACTIVITY_EXTRA_KEY_URL), "UTF-8");

			architectView.Load (decodedUrl);
		}
	}
}

